// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using Novacta.Analytics.Infrastructure;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents a finite discrete distribution.
    /// </summary>
    /// <remarks>
    /// <para id='description'>
    /// A <see cref="FiniteDiscreteDistribution"/> instance represents
    /// the probability distribution of a random variable which
    /// can take only a finite number of values. 
    /// Property <see cref="Values"/> returns
    /// the matrix of such distinct values,
    /// say <latex mode="inline">V</latex>,
    /// whose probabilities are explicitly assigned
    /// by a <see cref="FiniteDiscreteDistribution"/> instance.
    /// </para>
    /// <para>
    /// Property <see cref="Masses"/> returns a matrix, 
    /// say <latex mode="inline">M</latex>, having the same dimensions
    /// <see cref="Values"/>, whose nonnegative entries sum up to <c>1</c> 
    /// and represent the probabilities of the
    /// corresponding entries in <see cref="Values"/>.
    /// More thoroughly, method <see cref="Pdf(double)"/> represents a
    /// probability mass function satisfying:
    /// <latex mode="display">
    /// p_{V,M}\round{V_{i,j}} = M_{i,j}.
    /// </latex>
    /// </para>
    /// <para>
    /// The <see cref="Values"/> of a <see cref="FiniteDiscreteDistribution"/> 
    /// instance are immutable. Their <see cref="Masses"/> can be updated
    /// via method <see cref="SetMasses(DoubleMatrix)"/>.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// In the following example, a <see cref="FiniteDiscreteDistribution"/> instance
    /// is exploited to execute some statistical tasks.
    /// </para>
    /// <para>
    /// <code source="..\Novacta.Analytics.CodeExamples\FiniteDiscreteDistributionExample0.cs.txt" 
    /// language="cs" />
    /// </para>
    /// </example>
    /// <seealso cref="ProbabilityDistribution" />
    /// <seealso href="https://en.wikipedia.org/wiki/Probability_distribution#Discrete_probability_distribution"/>
    public sealed class FiniteDiscreteDistribution : ProbabilityDistribution
    {
        #region State

        private int[] aliases;
        private DoubleMatrix cutoffs;
        private ReadOnlyDoubleMatrix masses;
        private readonly ReadOnlyDoubleMatrix values;

        /// <summary>
        /// Gets the distinct values whose probabilities are explicitly 
        /// assigned by this instance.
        /// </summary>
        /// <value>The values whose probabilities are explicitly assigned 
        /// by this instance.</value>
        public ReadOnlyDoubleMatrix Values
        {
            get
            {
                return this.values;
            }
        }

        /// <summary>
        /// Gets the probabilities of the <see cref="Values"/> of 
        /// this instance.
        /// </summary>
        /// <value>The probabilities of the <see cref="Values"/>.</value>
        /// <remarks>
        /// <para>
        /// Entries in <see cref="Masses"/> can be <c>0</c> and their
        /// sum must be equal to <c>1</c>.
        /// </para>
        /// </remarks>
        public ReadOnlyDoubleMatrix Masses
        {
            get
            {
                return this.masses;
            }
        }


        /// <summary>
        /// Sets the probabilities of the <see cref="Values"/> of 
        /// this instance.
        /// </summary>
        /// <param name="masses">The probabilities assigned to the <see cref="Values"/>
        /// of this instance.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="masses"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="masses"/> contains at least an entry which
        /// does not belong to the interval <c>[0, 1]</c>.<br/>
        /// -or-<br/>
        /// The sum of the entries in <paramref name="masses"/> is not equal to 
        /// <c>1</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="masses"/> has not the same 
        /// dimensions of  
        /// <see cref="Values"/>.
        /// </exception>
        public void SetMasses(DoubleMatrix masses)
        {
            FiniteDiscreteDistribution.ValidateMasses(masses, this.values.matrix);
            this.masses = masses.Clone().AsReadOnly();
            this.InitializeAliasTables();
        }


        #endregion

        #region Constructors and factory methods

        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="FiniteDiscreteDistribution"/> class
        /// having the specified values and masses.
        /// </summary>
        /// <param name="values">The distinct values whose probabilities 
        /// are explicitly assigned.</param>
        /// <param name="masses">The probabilities assigned to the <paramref name="values"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="values"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="masses"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="values"/> contains at least two identical entries.<br/>
        /// -or-<br/>
        /// <paramref name="masses"/> contains at least an entry which
        /// does not belong to the interval <c>[0, 1]</c>.<br/>
        /// -or-<br/>
        /// The sum of the entries in <paramref name="masses"/> is not equal to 
        /// <c>1</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="masses"/> has not the same 
        /// dimensions of  
        /// <see cref="Values"/>.
        /// </exception>
        public FiniteDiscreteDistribution(
            DoubleMatrix values, DoubleMatrix masses)
            : this(values, masses, fromPublicAPI: true)
        {
        }

        internal FiniteDiscreteDistribution(
            DoubleMatrix values,
            DoubleMatrix masses,
            bool fromPublicAPI)
        {
            if (fromPublicAPI)
            {
                FiniteDiscreteDistribution.ValidateValues(values);

                FiniteDiscreteDistribution.ValidateMasses(masses, values);

                this.values = values.Clone().AsReadOnly();

                this.masses = masses.Clone().AsReadOnly();
            }

            this.values = values.AsReadOnly();

            this.masses = masses.AsReadOnly();

            this.InitializeAliasTables();
        }

        /// <summary>
        /// Creates a finite discrete distribution having the
        /// specified values and assigns to them equal masses.
        /// </summary>
        /// <param name="values">The values whose probabilities 
        /// are explicitly assigned.</param>
        /// <returns>A uniform finite discrete distribution.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="values"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="values"/> contains at least two identical entries.
        /// </exception>
        public static FiniteDiscreteDistribution Uniform(DoubleMatrix values)
        {
            ArgumentNullException.ThrowIfNull(values);

            double numberOfValues = values.Count;

            var masses = DoubleMatrix.Dense(
                values.NumberOfRows, values.NumberOfColumns, 1.0 / numberOfValues);

            return new FiniteDiscreteDistribution(values, masses);
        }

        private void InitializeAliasTables()
        {
            int i, j, minI, maxJ;

            int numberOfValues = this.values.Count;
            this.cutoffs = DoubleMatrix.Dense(numberOfValues, 1, 1.0);

            DoubleMatrix difference = (DoubleMatrix)this.masses;
            this.aliases = new int[numberOfValues];

            LinkedList<int> s = new(IndexCollection.Range(0, numberOfValues - 1));
            IndexCollection currentIndex;
            DoubleMatrix currentDifference;

            while (true)
            {
                if (s.Count <= 0)
                {
                    break;
                }
                currentIndex = new IndexCollection([.. s], false);
                currentDifference = difference.Vec(currentIndex);

                IndexValuePair pair = Stat.Min(currentDifference);
                double minDifference = pair.value;
                minI = pair.index;
                i = currentIndex[minI];

                maxJ = Stat.Max(currentDifference).index;
                j = currentIndex[maxJ];
                this.aliases[i] = j;
                this.cutoffs[i] = numberOfValues * minDifference;
                difference[j] -= (1.0 - this.cutoffs[i]) / ((double)numberOfValues);
                s.Remove(i);
            }

            for (i = 0; i < numberOfValues; i++)
            {
                this.cutoffs[i] += i;
            }
        }

        #endregion

        #region Distribution

        /// <inheritdoc/>
        public override double Cdf(double argument)
        {
            var found = this.values.FindWhile(v => v <= argument);
            if (null != found)
            {
                return Stat.Sum(this.masses.Vec(found));
            }
            return 0.0;
        }

        /// <summary>
        /// Throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="arguments">The arguments at which the function
        /// is to be evaluated.</param>
        /// <returns>The <see cref="NotSupportedException"/>.</returns>
        /// <exception cref="NotSupportedException">
        /// The cumulative distribution function cannot be inverted.
        /// </exception>
        public override DoubleMatrix InverseCdf(DoubleMatrix arguments)
        {
            throw new NotSupportedException(
               ImplementationServices.GetResourceString(
                   "STR_EXCEPT_PDF_INVERSE_CDF_NOT_SUPPORTED"));
        }

        /// <summary>
        /// Throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="argument">The argument at which the function
        /// is to be evaluated.</param>
        /// <returns>The <see cref="NotSupportedException"/>.</returns>
        /// <exception cref="NotSupportedException">
        /// The cumulative distribution function cannot be inverted.
        /// </exception>
        public override double InverseCdf(double argument)
        {
            throw new NotSupportedException(
               ImplementationServices.GetResourceString(
                   "STR_EXCEPT_PDF_INVERSE_CDF_NOT_SUPPORTED"));
        }

        /// <inheritdoc/>
        public override bool CanInvertCdf
        {
            get { return false; }
        }

        /// <inheritdoc/>
        public override double Pdf(double argument)
        {
            var values = this.values.matrix;
            var masses = this.masses;

            for (int i = 0; i < values.Count; i++)
            {
                if (values[i] == argument)
                {
                    return masses[i];
                }
            }

            return 0.0;
        }

        #endregion

        #region Moments

        /// <inheritdoc/>
        public override double Mean()
        {
            var values = this.values.matrix;
            var masses = this.masses;

            double result = 0.0;
            for (int i = 0; i < values.Count; i++)
            {
                result += values[i] * masses[i];
            }
            return result;
        }

        /// <inheritdoc/>
        public override double Variance()
        {
            var values = this.values.matrix;
            var masses = this.masses;

            double result = 0.0;
            double expectedMean = this.Mean();
            double deviation;
            for (int i = 0; i < values.Count; i++)
            {
                deviation = values[i] - expectedMean;
                result += deviation * deviation * masses[i];
            }
            return result;
        }

        #endregion

        #region Sample

        /// <inheritdoc/>
        public override double Sample()
        {
            double[] cutoffs = this.cutoffs.GetStorage();
            double[] values = this.values.matrix.GetStorage();
            int[] aliases = this.aliases;

            double sample = this.RandomNumberGenerator.DefaultUniform();
            double numberOfValues = values.Length;

            double u = sample * numberOfValues;
            int l = (int)Math.Floor(u);
            sample = (u <= cutoffs[l]) ? values[l] : values[aliases[l]];

            return sample;
        }

        /// <inheritdoc/>
        protected sealed override void OnSample(
            int sampleSize, double[] destinationArray, int destinationIndex)
        {
            this.RandomNumberGenerator.DefaultUniform(
                sampleSize,
                destinationArray,
                destinationIndex);

            double u;
            int l, j;

            double[] cutoffs = this.cutoffs.GetStorage();
            double[] values = this.values.matrix.GetStorage();
            int[] aliases = this.aliases;
            double numberOfValues = values.Length;

            for (int i = 0; i < sampleSize; i++)
            {
                j = i + destinationIndex;
                u = destinationArray[j] * numberOfValues;
                l = (int)Math.Floor(u);
                destinationArray[j] = (u <= cutoffs[l])
                    ? values[l] : values[aliases[l]];
            }
        }

        #endregion

        #region Input validation

        private static void ValidateValues(DoubleMatrix values)
        {
            ArgumentNullException.ThrowIfNull(values);

            int numberOfDistinctValues = values.Distinct().Count();
            if (numberOfDistinctValues != values.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(values),
                   ImplementationServices.GetResourceString(
                       "STR_EXCEPT_PAR_ENTRIES_MUST_BE_DISTINCT"));
            }
        }

        private static void ValidateMasses(
            DoubleMatrix masses, DoubleMatrix values)
        {
            ArgumentNullException.ThrowIfNull(masses);

            if (values.NumberOfRows != masses.NumberOfRows)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS"),
                        nameof(values)),
                    nameof(masses));
            }

            if (values.NumberOfColumns != masses.NumberOfColumns)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_COLUMNS"),
                        nameof(values)),
                    nameof(masses));
            }

            var found = masses.FindWhile(p => p < 0.0 || p > 1.0);
            if (null != found)
            {
                throw new ArgumentOutOfRangeException(nameof(masses),
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_ENTRIES_NOT_IN_CLOSED_INTERVAL"),
                            "0", 
                            "1"));
            }

            double sum = Stat.Sum(masses);
            if (Math.Round(sum, 2) != 1.0)
            {
                throw new ArgumentOutOfRangeException(nameof(masses),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_1"));
            }
        }

        #endregion
    }
}

