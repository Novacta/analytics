// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using System;
using System.Globalization;

namespace Novacta.Analytics
{

    /// <summary>
    /// Represents a probability distribution.
    /// </summary>
    public abstract class ProbabilityDistribution : RandomDevice
    {
        #region Distribution

        /// <summary>
        /// Computes the cumulative distribution function 
        /// of this instance at the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments at which the function
        /// is to be evaluated.</param>
        /// <returns>The values implied by the function at the specified
        /// arguments.</returns>
        /// <remarks>
        /// <para>
        /// The returned matrix has the same dimensions of 
        /// <paramref name="arguments"/>.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="arguments"/> is <b>null</b>.
        /// </exception>
        public virtual DoubleMatrix Cdf(DoubleMatrix arguments)
        {
            ArgumentNullException.ThrowIfNull(arguments);

            DoubleMatrix results = DoubleMatrix.Dense(
                arguments.NumberOfRows, arguments.NumberOfColumns);

            double[] resultsArray = results.GetStorage();
            for (int i = 0; i < resultsArray.Length; i++)
                resultsArray[i] = this.Cdf(arguments[i]);

            return results;
        }

        /// <inheritdoc cref="Cdf(DoubleMatrix)"/>
        public virtual DoubleMatrix Cdf(ReadOnlyDoubleMatrix arguments)
        {
            ArgumentNullException.ThrowIfNull(arguments);

            DoubleMatrix results = DoubleMatrix.Dense(
                arguments.NumberOfRows, arguments.NumberOfColumns);

            double[] resultsArray = results.GetStorage();
            for (int i = 0; i < resultsArray.Length; i++)
                resultsArray[i] = this.Cdf(arguments[i]);

            return results;
        }

        /// <summary>
        /// Computes the cumulative distribution function 
        /// of this instance at the specified argument.
        /// </summary>
        /// <param name="argument">The argument at which the function
        /// is to be evaluated.</param>
        /// <returns>The value implied by the function at the specified
        /// argument.</returns>
        public abstract double Cdf(double argument);

        /// <summary>
        /// Computes the inverse of the cumulative distribution function 
        /// of this instance at the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments at which the function
        /// is to be evaluated.</param>
        /// <returns>The values implied by the function at the specified
        /// arguments.</returns>
        /// <remarks>
        /// <para>
        /// The returned matrix has the same dimensions of 
        /// <paramref name="arguments"/>. If the cumulative distribution
        /// function cannot be inverted at a specific entry of 
        /// <paramref name="arguments"/>,
        /// then the corresponding entry in the returned matrix is 
        /// <see cref="System.Double.NaN"/>.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="arguments"/> is <b>null</b>.
        /// </exception>
        public virtual DoubleMatrix InverseCdf(DoubleMatrix arguments)
        {
            ArgumentNullException.ThrowIfNull(arguments);

            DoubleMatrix results = DoubleMatrix.Dense(
                arguments.NumberOfRows, arguments.NumberOfColumns);

            double[] resultsArray = results.GetStorage();
            for (int i = 0; i < resultsArray.Length; i++)
            {
                double argument = arguments[i];
                resultsArray[i] = (argument < 0.0 || 1.0 < argument) ?
                    Double.NaN : this.InverseCdf(arguments[i]);
            }
            return results;
        }

        /// <inheritdoc cref="ProbabilityDistribution.InverseCdf(DoubleMatrix)"/>
        public virtual DoubleMatrix InverseCdf(ReadOnlyDoubleMatrix arguments)
        {
            ArgumentNullException.ThrowIfNull(arguments);

            DoubleMatrix results = DoubleMatrix.Dense(
                arguments.NumberOfRows, arguments.NumberOfColumns);

            double[] resultsArray = results.GetStorage();
            for (int i = 0; i < resultsArray.Length; i++)
            {
                double argument = arguments[i];
                resultsArray[i] = (argument < 0.0 || 1.0 < argument) ?
                    Double.NaN : this.InverseCdf(arguments[i]);
            }
            return results;
        }

        /// <summary>
        /// Computes the inverse of the cumulative distribution function 
        /// of this instance at the specified argument.
        /// </summary>
        /// <param name="argument">The argument at which the function
        /// is to be evaluated.</param>
        /// <returns>The value implied by the function at the specified
        /// argument.</returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="argument"/> is outside
        /// the interval <c>[0, 1]</c>, the returned value is
        /// <see cref="System.Double.NaN"/>.
        /// </para>
        /// </remarks>
        public abstract double InverseCdf(double argument);

        /// <summary>
        /// Gets a value indicating whether this instance can invert its
        /// cumulative distribution function.
        /// </summary>
        /// <value><c>true</c> if this instance can invert the cumulative
        /// distribution function; otherwise, <c>false</c>.</value>
        public abstract bool CanInvertCdf { get; }

        /// <summary>
        /// Computes the probability density function 
        /// of this instance at the specified arguments.
        /// </summary>
        /// <param name="arguments">The arguments at which the function
        /// is to be evaluated.</param>
        /// <returns>The values implied by the function at the specified
        /// arguments.</returns>
        /// <remarks>
        /// <para>
        /// The returned matrix has the same dimensions of 
        /// <paramref name="arguments"/>.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="arguments"/> is <b>null</b>.
        /// </exception>
        public virtual DoubleMatrix Pdf(DoubleMatrix arguments)
        {
            ArgumentNullException.ThrowIfNull(arguments);

            DoubleMatrix results = DoubleMatrix.Dense(
                arguments.NumberOfRows, arguments.NumberOfColumns);

            double[] resultsArray = results.GetStorage();
            for (int i = 0; i < resultsArray.Length; i++)
                resultsArray[i] = this.Pdf(arguments[i]);

            return results;
        }

        /// <inheritdoc cref="ProbabilityDistribution.Pdf(DoubleMatrix)"/>
        public virtual DoubleMatrix Pdf(ReadOnlyDoubleMatrix arguments)
        {
            ArgumentNullException.ThrowIfNull(arguments);

            DoubleMatrix results = DoubleMatrix.Dense(
                arguments.NumberOfRows, arguments.NumberOfColumns);

            double[] resultsArray = results.GetStorage();
            for (int i = 0; i < resultsArray.Length; i++)
                resultsArray[i] = this.Pdf(arguments[i]);

            return results;
        }

        /// <summary>
        /// Computes the probability density function 
        /// of this instance at the specified argument.
        /// </summary>
        /// <param name="argument">The argument at which the function
        /// is to be evaluated.</param>
        /// <returns>The value implied by the function at the specified
        /// argument.</returns>
        public abstract double Pdf(double argument);

        #endregion

        #region Moments

        /// <summary>
        /// Computes the mean of this instance.
        /// </summary>
        /// <returns>The mean of this instance.</returns>
        public abstract double Mean();

        /// <summary>
        /// Computes the standard deviation of this instance.
        /// </summary>
        /// <returns>The standard deviation of this instance.</returns>
        public virtual double StandardDeviation()
        {
            return Math.Sqrt(this.Variance());
        }

        /// <summary>
        /// Computes the variance of this instance.
        /// </summary>
        /// <returns>The variance of this instance.</returns>
        public abstract double Variance();

        #endregion

        #region Sample

        /// <summary>
        /// Draws a sample point from this instance.
        /// </summary>
        /// <returns>The sampled point.</returns>
        public abstract double Sample();

        /// <summary>
        /// Draws a sample from this instance having the specified size
        /// and returns it as a matrix.
        /// </summary>
        /// <param name="sampleSize">The sample size.</param>
        /// <returns>The matrix whose entries store the sample.</returns>
        /// <remarks>
        /// <para>
        /// The returned matrix has one column and a number of rows
        /// equal to <paramref name="sampleSize"/>.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="sampleSize"/> is not positive.
        /// </exception>
        public DoubleMatrix Sample(int sampleSize)
        {
            ProbabilityDistribution.ValidateSampleInput(sampleSize);

            DoubleMatrix results = DoubleMatrix.Dense(
                    sampleSize,
                    1);

            double[] resultsArray = results.GetStorage();

            this.OnSample(sampleSize, resultsArray, 0);

            return results;
        }

        /// <summary>
        /// Draws a sample from this instance having the specified size 
        /// and returns it 
        /// in a destination array.
        /// </summary>
        /// <param name="sampleSize">The sample size.</param>
        /// <param name="destinationArray">The destination array that 
        /// receives the sampled data.</param>
        /// <param name="destinationIndex">The index in 
        /// <paramref name="destinationArray"/> at which storing 
        /// begins.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="destinationArray"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="sampleSize"/> is not positive.<br/>
        /// -or-<br/>
        /// <paramref name="destinationIndex"/> is negative.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Parameter <paramref name="sampleSize"/> must be less than or 
        /// equal to the 
        /// difference between the length of 
        /// parameter <paramref name="destinationArray"/> and 
        /// <paramref name="destinationIndex"/>.
        /// </exception>
        public void Sample(
            int sampleSize,
            double[] destinationArray,
            int destinationIndex)
        {
            ProbabilityDistribution.ValidateSampleInput(
                sampleSize, destinationArray, destinationIndex);

            this.OnSample(sampleSize, destinationArray, destinationIndex);
        }


        /// <summary>
        /// Called when drawing a sample from this instance having the given size 
        /// and returns it 
        /// in a given destination array.
        /// </summary>
        /// <param name="sampleSize">The sample size.</param>
        /// <param name="destinationArray">The destination array that 
        /// receives the sampled data.</param>
        /// <param name="destinationIndex">The index in 
        /// <paramref name="destinationArray"/> at which storing 
        /// begins.</param>
        /// <remarks>
        /// <para>
        /// The <see cref="OnSample(int, double[], int)"/> method allows 
        /// derived classes to
        /// implement their own algorithm to draw a sample. 
        /// </para>
        /// <para><b>Notes to Inheritors</b></para>
        /// <para>
        /// When overriding <see cref="OnSample(int, double[], int)"/>, 
        /// you can take for 
        /// granted that <paramref name="sampleSize"/> is greater than <c>0</c>
        /// and less than or equal to the 
        /// difference between the length of 
        /// parameter <paramref name="destinationArray"/> and 
        /// <paramref name="destinationIndex"/>, and
        /// consists, on input, of zeroed entries.
        /// </para>
        /// </remarks>
        protected virtual void OnSample(
            int sampleSize,
            double[] destinationArray,
            int destinationIndex)
        {
            int j;

            for (int i = 0; i < sampleSize; i++)
            {
                j = i + destinationIndex;
                destinationArray[j] = this.Sample();
            }
        }

        #endregion

        #region Input validation

        internal static void ValidateSampleInput(int sampleSize)
        {
            if (sampleSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sampleSize),
                   ImplementationServices.GetResourceString(
                       "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }
        }

        internal static void ValidateSampleInput(
            int sampleSize,
            double[] destinationArray,
            int destinationIndex)
        {
            ProbabilityDistribution.ValidateSampleInput(sampleSize);

            ArgumentNullException.ThrowIfNull(destinationArray);

            if (destinationIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(destinationIndex),
                   ImplementationServices.GetResourceString(
                       "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE"));
            }

            int length = destinationArray.Length;
            if (sampleSize > (length - destinationIndex))
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                           "STR_EXCEPT_PDF_SAMPLESIZE_ARRAYLENGTH_MISMATCH"),
                        nameof(sampleSize),
                        nameof(destinationArray),
                        nameof(destinationIndex)),
                    nameof(sampleSize));
            }
        }

        internal static void ValidateSampleInput(
            int sampleSize,
            int[] destinationArray,
            int destinationIndex)
        {
            ProbabilityDistribution.ValidateSampleInput(sampleSize);

            ArgumentNullException.ThrowIfNull(destinationArray);

            if (destinationIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(destinationIndex),
                   ImplementationServices.GetResourceString(
                       "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE"));
            }

            int length = destinationArray.Length;
            if (sampleSize > (length - destinationIndex))
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PDF_SAMPLESIZE_ARRAYLENGTH_MISMATCH"),
                        nameof(sampleSize),
                        nameof(destinationArray),
                        nameof(destinationIndex)),
                    nameof(sampleSize));
            }
        }

        #endregion
    }
}

