// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Novacta.Analytics.Infrastructure
{
    /// <summary>
    /// Represents a block of a numerical variable to be discretized for 
    /// classification learning.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Classification learning algorithms which involve the 
    /// discretization of numerical variables, i.e. those variables
    /// which are not representable as a finite collection of categories, 
    /// often make use of the concepts of numerical blocks and bins.
    /// </para>
    /// <inheritdoc cref="NumericalBin" 
    /// path="para[@id='Numerical.Bins.1']"/>
    /// <para>
    /// Numerical bins are numerical blocks if and only if their 
    /// target distribution is heterogeneous (i.e. at least two target 
    /// categories have nonzero frequency), or
    /// the distribution is homogeneous and no contiguous bins are 
    /// homogeneous and share the same target mode (For a discussion on 
    /// how blocks relate to bins, see 
    /// Elomaa and Rousu, 1999)<cite>elomaa-rousu-1999</cite>. Otherwise, 
    /// such contiguous bins are merged to define additional blocks. 
    /// When a <see cref="NumericalBlock"/> instance is formed by merging 
    /// operations of existing consecutive <see cref="NumericalBin"/> 
    /// instances, property <see cref="FirstValue"/> returns the numerical 
    /// value of the first merged bin,
    /// while <see cref="LastValue"/> returns the numerical value of the 
    /// last merged one.
    /// </para>
    /// <inheritdoc cref="NumericalBin" 
    /// path="para[@id='Numerical.Bins.3']"/>
    /// <para>
    /// Given a set of numerical and target data, the corresponding blocks 
    /// can be sequenced by calling method 
    /// <see cref="GetNumericalBlocks(DoubleMatrix, DoubleMatrix)">
    /// GetNumericalBlocks</see>.
    /// </para>
    /// </remarks>
    /// <seealso cref="NumericalBin"/>
    /// <seealso cref="NumericalBlockRange"/>
    internal class NumericalBlock
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="NumericalBlock"/> class.
        /// </summary>
        /// <param name="firstPosition">
        /// The first block position.</param>
        /// <param name="lastAttributeValue">
        /// The block last attribute value.</param>
        /// <param name="targetCodes">
        /// The codes observed for the target variable.</param>
        internal NumericalBlock(
            int firstPosition,
            double lastAttributeValue,
            double[] targetCodes)
        {
            this.firstPosition = firstPosition;
            this.lastValue = lastAttributeValue;
            this.targetFrequencyDistribution =
                new Dictionary<double, int>(targetCodes.Length);
            for (int i = 0; i < targetCodes.Length; i++) {
                this.targetFrequencyDistribution.Add(targetCodes[i], 0);
            }
        }

        #region State

        protected int firstPosition;
        protected int lastPosition;
        protected double firstValue;
        protected double lastValue;
        protected Dictionary<double, int> targetFrequencyDistribution;

        /// <summary>
        /// Gets the first position of this <see cref="NumericalBlock"/>.
        /// </summary>
        /// <value>
        /// The first position of this <see cref="NumericalBlock"/>.</value>
        public int FirstPosition { get { return this.firstPosition; } }

        /// <summary>
        /// Gets the last position of this <see cref="NumericalBlock"/>.
        /// </summary>
        /// <value>
        /// The last position of this <see cref="NumericalBlock"/>.</value>
        public int LastPosition { get { return this.lastPosition; } }

        /// <summary>
        /// Gets the first value of this <see cref="NumericalBlock"/>.
        /// </summary>
        /// <value>
        /// The first value of this <see cref="NumericalBlock"/>.</value>
        public double FirstValue { get { return this.firstValue; } }

        /// <summary>
        /// Gets the last value of this <see cref="NumericalBlock"/>.
        /// </summary>
        /// <value>
        /// The last value of this <see cref="NumericalBlock"/>.</value>
        public double LastValue { get { return this.lastValue; } }

        /// <summary>
        /// Gets the target frequency distribution corresponding to 
        /// this <see cref="NumericalBlock"/>.
        /// </summary>
        /// <value>
        /// The target frequency distribution of this <see cref="NumericalBlock"/>.
        /// </value>
        public IReadOnlyDictionary<double, int> TargetFrequencyDistribution
        {
            get { return this.targetFrequencyDistribution; }
        }

        #endregion

        /// <summary>
        /// Gets the blocks for a sequence of numerical and target data.
        /// </summary>
        /// <param name="numericalData">The numerical data.</param>
        /// <param name="targetData">The target data.</param>
        /// <returns>
        /// The collection of <see cref="NumericalBlock"/> instances 
        /// corresponding to the specified numerical and target data.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="numericalData"/>  is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="targetData"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Parameter <paramref name="targetData"/> has not the 
        /// same <see cref="DoubleMatrix.Count"/> of
        /// parameter <paramref name="numericalData"/>.
        /// </exception>
        public static List<NumericalBlock> GetNumericalBlocks(
            DoubleMatrix numericalData,
            DoubleMatrix targetData)
        {
            List<NumericalBin> bins = 
                NumericalBin.GetNumericalBins(numericalData, targetData);

            List<NumericalBlock> blocks;

            // Numerical bins are numerical blocks if and only if
            // A. Their target distribution is heterogeneous
            //    (i.e. there are two or more target values having nonzero frequency)
            // OR
            // not(A). Their class distribution is not heterogeneous
            //    AND
            // B. No contiguous bins are not heterogeneous and share
            //    the same target mode.

            int numberOfBins = bins.Count;
            blocks = new List<NumericalBlock>(numberOfBins);

            if (1==numberOfBins) {
                blocks.Add(bins[0]);
                return blocks;
            }

            var targetCodes = bins[0].targetFrequencyDistribution.Keys.ToArray();

            for (int i = 0; i < numberOfBins; i++) {
                var currentBin = bins[i];

                // The following is equivalent to condition not(A) if index i 
                // is not the last index.
                // Otherwise, i.e. if index i reaches the last possible value, 
                // which is numberOfBins - 1,
                // this implies that the last bin is a block, since it has not
                // been previously merged.
                bool reviseCurrentBinForMerging = 
                    !currentBin.IsTargetDistributionHeterogeneous;

                if (reviseCurrentBinForMerging) { 
                    // Here if not(A) is true: verify condition B.
                    double referenceMode = currentBin.Mode;
                    int j;
                    int mergeLastIndex = currentBin.lastPosition;
                    int mergeModeFrequency = 
                        currentBin.targetFrequencyDistribution[referenceMode];

                    // This will become true if and only if B holds true.
                    bool mergeRequired = false; 
                    double mergeAttributeValue = currentBin.lastValue;
                    for (j = i + 1; j < numberOfBins; j++) {
                        NumericalBlock nextBin = bins[j];
                        if ((!nextBin.IsTargetDistributionHeterogeneous)
                            && (nextBin.Mode == referenceMode)) {
                            // Here if both currentBin and nextBin 
                            // are not heterogeneous 
                            // and they share the same class mode
                            mergeRequired = true;
                            mergeAttributeValue = nextBin.lastValue;
                            mergeLastIndex = nextBin.lastPosition;
                            mergeModeFrequency +=
                                nextBin.targetFrequencyDistribution[referenceMode];
                        }
                        else { // No subsequent bins are not heterogeneous and share 
                               // the same mode of the current bin
                            break;
                        }
                    } // End for
                    if (mergeRequired) {
                        i = j - 1; // Skip merged bins
                        NumericalBlock mergedBlock = new
(
                            currentBin.firstPosition,
                            mergeAttributeValue,
                            targetCodes)
                        {
                            firstValue = currentBin.firstValue,
                            lastPosition = mergeLastIndex
                        };
                        mergedBlock.targetFrequencyDistribution[referenceMode] = 
                            mergeModeFrequency;
                        blocks.Add(mergedBlock);
                    }
                    else { // The current bin is a block.
                        blocks.Add(currentBin);
                    }
                }
                else { // The current bin is a block.
                    blocks.Add(currentBin);
                }
            }

            return blocks;
        }

        /// <summary>
        /// Gets the mode of the target frequency distribution of this instance.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Each target value having maximal frequency is a target distribution
        /// mode. 
        /// </para>
        /// </remarks>
        /// <value>The target mode.</value>
        public double Mode
        {
            get
            {
                double mode = Double.NaN;
                int maximalFrequency = -1;
                foreach (var pair in this.targetFrequencyDistribution) {
                    int frequency = pair.Value;
                    if (frequency > maximalFrequency) {
                        maximalFrequency = frequency;
                        mode = pair.Key;
                    }
                }
                return mode;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has a heterogeneous 
        /// target distribution.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The target distribution is heterogeneous if and only if there are 
        /// two or more classes having nonzero frequency.
        /// </para>
        /// </remarks>
        /// <value><c>true</c> if this instance has a target distribution not 
        /// maximally homogeneous; otherwise, <c>false</c>.</value>
        public bool IsTargetDistributionHeterogeneous
        {
            get
            {
                int numberOfObservedClasses = 0;
                foreach (var frequency in this.targetFrequencyDistribution.Values) {
                    if (frequency > 0) {
                        numberOfObservedClasses++;
                    }
                    if (numberOfObservedClasses > 1) {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents 
        /// this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            StringBuilder builder = new();

            builder.AppendFormat(
                CultureInfo.InvariantCulture,
                "Values: [{0}, {1}]. ", 
                this.firstValue, 
                this.lastValue);

            builder.AppendFormat(
                CultureInfo.InvariantCulture,
                "Positions: [{0}, {1}]. ", 
                this.firstPosition, 
                this.lastPosition);
            builder.Append("Target Distribution: [ ");
            foreach (var pair in this.targetFrequencyDistribution) {
                builder.Append("(" + pair.Key + ", " + pair.Value + ") ");
            }
            builder.Append("]");

            return builder.ToString();
        }
    }
}
