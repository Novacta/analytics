// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Novacta.Analytics.Infrastructure
{
    /// <summary>
    /// Represents a bin of a numerical variable to be discretized for 
    /// classification learning.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="NumericalBin"/> objects raise in the context of 
    /// classification learning algorithms which involve the discretization 
    /// of numerical variables 
    /// (see Fayyad and Irani, 1993)<cite>fayyad-irani-1993</cite>,
    /// where a variable is considered as numerical if it is not 
    /// representable as a finite collection of categories.
    /// </para>
    /// <para id='Numerical.Bins.1'>
    /// If instances are classified in terms of a target 
    /// <see cref="CategoricalVariable"/> by exploiting observations 
    /// of a given numerical variable, then its discretization 
    /// is typically obtained through the identification of its bins: 
    /// the observed numerical data are sorted in ascending order and 
    /// the target data are arranged accordingly.
    /// A <see cref="NumericalBin"/> is a collection of consecutive 
    /// positions in the ordering which are occupied by a same numerical 
    /// value.
    /// </para>
    /// <para>
    /// Numerical bins can be considered as special 
    /// <see cref="NumericalBlock"/> instances.
    /// In fact, numerical blocks also represent collections of 
    /// consecutive positions in the numerical data ordering, but this 
    /// time the positions can be occupied by different numerical values 
    /// (For a discussion on how blocks relate to bins, see 
    /// Elomaa and Rousu, 1999)<cite>elomaa-rousu-1999</cite>. 
    /// <see cref="NumericalBlock"/> instances report the first and the 
    /// last of such values through properties 
    /// <see cref="NumericalBlock.FirstValue"/> and 
    /// <see cref="NumericalBlock.LastValue"/>,
    /// respectively. In the case of a <see cref=" NumericalBin"/>, both 
    /// such properties return the same numerical value.
    /// </para>
    /// <para id='Numerical.Bins.3'>
    /// Properties <see cref="NumericalBlock.FirstPosition"/> and 
    /// <see cref="NumericalBlock.LastPosition"/> 
    /// return the first and the last of the block positions. The 
    /// frequency distribution of the target
    /// data occupying the same positions of the block can be inspected 
    /// through property
    /// <see cref="NumericalBlock.TargetFrequencyDistribution"/>.
    /// </para>
    /// <para>
    /// Given a set of numerical and target data, the corresponding bins 
    /// can be sequenced by calling
    /// method <see cref="GetNumericalBins(DoubleMatrix, DoubleMatrix)">
    /// GetNumericalBins</see>.
    /// </para>
    /// </remarks>
    /// <seealso cref="NumericalBlock"/>
    internal class NumericalBin : NumericalBlock
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumericalBin"/> class.
        /// </summary>
        /// <param name="firstPosition">
        /// The first bin position.</param>
        /// <param name="numericalValue">
        /// The numerical value corresponding to the bin.</param>
        /// <param name="targetCodes">
        /// The codes observed for the target variable.</param>
        internal NumericalBin(
            int firstPosition,
            double numericalValue,
            double[] targetCodes) : base(firstPosition, numericalValue, targetCodes)
        {
            this.firstValue = numericalValue;
        }

        /// <summary>
        /// Gets the bins for a sequence of numerical and target data.
        /// </summary>
        /// <param name="numericalData">The numerical data.</param>
        /// <param name="targetData">The target data.</param>
        /// <returns>The collection of <see cref="NumericalBin"/> instances 
        /// corresponding to the specified numerical and target data.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="numericalData"/> is <b>null</b>. <br/>
        /// -or- <br/>
        /// <paramref name="targetData"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Parameter <paramref name="targetData"/> has not the 
        /// same <see cref="DoubleMatrix.Count"/> of
        /// parameter <paramref name="numericalData"/>.
        /// </exception>
        public static List<NumericalBin> GetNumericalBins(
            DoubleMatrix numericalData,
            DoubleMatrix targetData)
        {
            #region Input validation

            if (numericalData is null) {
                throw new ArgumentNullException(nameof(numericalData));
            }

            if (targetData is null) {
                throw new ArgumentNullException(nameof(targetData));
            }

            if (numericalData.Count != targetData.Count) {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_HAVE_SAME_COUNT"),
                        nameof(numericalData)),
                    nameof(targetData));
            }

            #endregion

            List<NumericalBin> bins;

            if (numericalData.Count == 1) {
                bins = new List<NumericalBin>(1);
                var bin = new NumericalBin(
                    0, 
                    numericalData[0], 
                    targetData.GetStorage())
                {
                    lastPosition = 0
                };
                bin.targetFrequencyDistribution[targetData[0]]++;
                bins.Add(bin);

                return bins;
            }

            bins = new List<NumericalBin>();

            // Identify boundary points
            SortIndexResults sortResults = Stat.SortIndex(
                numericalData, SortDirection.Ascending);
            var sortedAttributeData = sortResults.SortedData;
            var sortedClassData = targetData.Vec(sortResults.SortedIndexes);

            var targetCodes = sortedClassData.Distinct().OrderBy(
                (code) => { return code; }).ToArray();

            double currentClass, currentAttributeValue, 
                nextAttributeValue = Double.NaN;
            int lastcycledPosition = sortedAttributeData.Count - 2;
            bool createBin = true;
            NumericalBin currentBin = null;

            // Create attribute bins (a bin is a collection of positions
            // in the attribute ordering which are occupied by a same 
            // attribute value 
            for (int i = 0; i < lastcycledPosition + 1; i++) {
                // Create a new bin if needed.
                currentAttributeValue = sortedAttributeData[i];
                if (createBin) {
                    currentBin = new NumericalBin(
                        i, 
                        currentAttributeValue, 
                        targetCodes);
                    createBin = false;
                }
                // Update the class distribution in the current bin.
                currentClass = sortedClassData[i];
                currentBin.targetFrequencyDistribution[currentClass]++;

                int nextPosition = i + 1;
                nextAttributeValue = sortedAttributeData[nextPosition];

                bool cutPointDetected = currentAttributeValue != nextAttributeValue;
                if (i < lastcycledPosition) {
                    if (cutPointDetected) {
                        currentBin.lastPosition = i;
                        bins.Add(currentBin);
                        createBin = true;
                    }
                }
                else {
                    // A cut point exists between the last two positions 
                    // (final cut point)
                    if (cutPointDetected) {
                        // Finalize the current bin
                        currentBin.lastPosition = i;
                        bins.Add(currentBin);

                        // Add a last bin consisting of the last position
                        currentBin = new NumericalBin(
                            nextPosition, 
                            nextAttributeValue, 
                            targetCodes)
                        {
                            lastPosition = nextPosition
                        };
                        currentBin.targetFrequencyDistribution[
                            sortedClassData[nextPosition]]++;
                        bins.Add(currentBin);
                    }
                    else { // No final cut point
                        currentBin.lastPosition = nextPosition;
                        currentBin.targetFrequencyDistribution[
                            sortedClassData[nextPosition]]++;
                        bins.Add(currentBin);
                    }
                }
            }

            return bins;
        }
    }
}
