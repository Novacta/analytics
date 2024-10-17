// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Threading;

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Interop;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents a pseudo-random number generator.
    /// </summary>
    public class RandomNumberGenerator
    {
        #region State, constructors and factory methods 

        private readonly VslSafeStreamStateDescriptor descriptor;

        private RandomNumberGenerator(VslSafeStreamStateDescriptor descriptor)
        {
            this.descriptor = descriptor;
        }

        /// <summary>
        /// Creates a single instruction, multiple data 
        /// Mersenne Twister 19937 generator having the
        /// specified seed.
        /// </summary>
        /// <param name="seed">The seed of the generator.</param>
        /// <returns>The SFMT 19937 generator having the specified seed.</returns>
        /// <remarks>
        /// <para>
        /// The SFMT 19937 generator has a period length equal 
        /// to <latex mode='inline'>2^{19937}</latex>.
        /// </para>
        /// </remarks>
        /// <seealso href="https://en.wikipedia.org/wiki/Mersenne_Twister"/>
        public static RandomNumberGenerator CreateSFMT19937(int seed)
        {
            var descriptor = VslSafeStreamStateDescriptor.Create(
                SafeNativeMethods.VSL.BRNG.VSL_BRNG_SFMT19937,
                seed);

            return new RandomNumberGenerator(descriptor);
        }

        /// <summary>
        /// Creates a  
        /// Mersenne Twister 19937 generator having the
        /// specified seed.
        /// </summary>
        /// <param name="seed">The seed of the generator.</param>
        /// <returns>The MT 19937 generator having the specified seed.</returns>
        /// <remarks>
        /// <para>
        /// The MT 19937 generator has a period length equal 
        /// to <latex mode='inline'>2^{19937}</latex>.
        /// </para>
        /// </remarks>
        /// <seealso href="https://en.wikipedia.org/wiki/Mersenne_Twister"/>
        public static RandomNumberGenerator CreateMT19937(int seed)
        {
            var descriptor = VslSafeStreamStateDescriptor.Create(
                SafeNativeMethods.VSL.BRNG.VSL_BRNG_MT19937,
                seed);

            return new RandomNumberGenerator(descriptor);
        }

        private static int mersenneTwister2203 = -1;

        /// <summary>
        /// Creates the next pseudo random generator in the sequence of 
        /// Mersenne Twister 2203 generators having the
        /// specified seed.
        /// </summary>
        /// <param name="seed">The seed of the generator.</param>
        /// <returns>The next MT 2203 generator having the specified seed.</returns>
        /// <remarks>
        /// <para>
        /// There are <c>6024</c> MT2203 pseudo random number generators.
        /// The MT 2203 generators guarantee mutual 
        /// independence of the corresponding random number sequences.
        /// Every MT 2203 generator has a period length equal 
        /// to <latex mode='inline'>2^{2203}</latex>.
        /// </para>
        /// <para>
        /// Method <see cref="CreateNextMT2203(int)"/> increments the iterator 
        /// to the next position in the sequence, or to the first position beyond the 
        /// end of sequence if the sequence has been completely traversed.
        /// </para>
        /// </remarks>
        /// <seealso href="https://en.wikipedia.org/wiki/Mersenne_Twister"/>
        public static RandomNumberGenerator CreateNextMT2203(int seed)
        {
            Interlocked.CompareExchange(ref mersenneTwister2203, -1, 6023);
            int currentGenerator = Interlocked.Increment(ref mersenneTwister2203);

            var descriptor = VslSafeStreamStateDescriptor.Create(
                SafeNativeMethods.VSL.BRNG.VSL_BRNG_MT2203 + currentGenerator,
                seed);

            return new RandomNumberGenerator(descriptor);
        }

        #endregion

        #region Sample

        #region Discrete uniform

        /// <summary>
        /// Draws a sample point from a discrete Uniform distribution defined on the interval
        /// having the specified bounds.
        /// </summary>
        /// <param name="lowerBound">The interval lower bound, inclusive.</param>
        /// <param name="upperBound">The interval upper bound, exclusive.</param>
        /// <returns>
        /// A sample point drawn from the specified 
        /// discrete Uniform distribution.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="upperBound"/> is not greater 
        /// than <paramref name="lowerBound"/>.
        /// </exception>
        /// <seealso href="https://en.wikipedia.org/wiki/Uniform_distribution_(discrete)"/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Performance",
            "CA1806:Do not ignore method results",
            Justification =
            "Parameters are expected to be well formed. " +
            "Additional error conditions should not be signaled since " +
            "no abstract, non-deterministic, or ARS-5 random number generators, " +
            "nor block-splitting methods are exploited by this assembly.")]
        public int DiscreteUniform(
            int lowerBound,
            int upperBound)
        {
            UniformDistribution.ValidateBounds(
                lowerBound,
                upperBound);

            int sample;

            unsafe
            {
                SafeNativeMethods.VSL.viRngUniform(
                    0,
                    this.descriptor.DangerousGetHandle().ToPointer(),
                    1,
                    &sample,
                    lowerBound,
                    upperBound);
            }

            return sample;
        }


        /// <summary>
        /// Draws a sample from a discrete Uniform distribution defined on the specified interval
        /// and returns it in a destination array.
        /// </summary>
        /// <param name="sampleSize">The sample size.</param>
        /// <param name="destinationArray">The destination array that 
        /// receives the sampled data.</param>
        /// <param name="destinationIndex">The index in 
        /// <paramref name="destinationArray"/> at which storing 
        /// begins.</param>
        /// <param name="lowerBound">The interval lower bound, inclusive.</param>
        /// <param name="upperBound">The interval upper bound, exclusive.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="destinationArray"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="sampleSize"/> is not positive.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Parameter <paramref name="sampleSize"/> must be less than or 
        /// equal to the 
        /// difference between the length of 
        /// parameter <paramref name="destinationArray"/> 
        /// and <paramref name="destinationIndex"/>.<br/>
        /// -or-<br/>
        /// <paramref name="destinationIndex"/> is 
        /// negative, or <paramref name="upperBound"/> is not greater 
        /// than <paramref name="lowerBound"/>.
        /// </exception>
        /// <seealso href="https://en.wikipedia.org/wiki/Uniform_distribution_(discrete)"/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Performance",
            "CA1806:Do not ignore method results",
            Justification =
            "Parameters are expected to be well formed. " +
            "Additional error conditions should not be signaled since " +
            "no abstract, non-deterministic, or ARS-5 random number generators, " +
            "nor block-splitting methods are exploited by this assembly.")]
        public void DiscreteUniform(
            int sampleSize,
            int[] destinationArray,
            int destinationIndex,
            int lowerBound,
            int upperBound)
        {
            ProbabilityDistribution.ValidateSampleInput(
                sampleSize,
                destinationArray,
                destinationIndex);

            UniformDistribution.ValidateBounds(
                lowerBound,
                upperBound);

            unsafe
            {
                fixed (int* destinationPointer = &destinationArray[destinationIndex]) {
                    SafeNativeMethods.VSL.viRngUniform(
                        0,
                        this.descriptor.DangerousGetHandle().ToPointer(),
                        sampleSize,
                        destinationPointer,
                        lowerBound,
                        upperBound);
                }
            }
        }

        #endregion

        #region Uniform

        /// <summary>
        /// Draws a sample from a Uniform distribution defined on the specified interval
        /// and returns it in a destination array.
        /// </summary>
        /// <param name="sampleSize">The sample size.</param>
        /// <param name="destinationArray">The destination array that 
        /// receives the sampled data.</param>
        /// <param name="destinationIndex">The index in 
        /// <paramref name="destinationArray"/> at which storing 
        /// begins.</param>
        /// <param name="lowerBound">The interval lower bound.</param>
        /// <param name="upperBound">The interval upper bound.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="destinationArray"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="sampleSize"/> is not positive.<br/>
        /// -or-<br/>
        /// <paramref name="destinationIndex"/> is 
        /// negative.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Parameter <paramref name="sampleSize"/> must be less than or 
        /// equal to the 
        /// difference between the length of 
        /// parameter <paramref name="destinationArray"/> 
        /// and <paramref name="destinationIndex"/>.<br/>
        /// -or-<br/>
        /// <paramref name="upperBound"/> is not greater 
        /// than <paramref name="lowerBound"/>.
        /// </exception>
        /// <seealso href="https://en.wikipedia.org/wiki/Uniform_distribution_(continuous)"/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Performance", 
            "CA1806:Do not ignore method results", 
            Justification =
            "Parameters are expected to be well formed. " +
            "Additional error conditions should not be signaled since " +
            "no abstract, non-deterministic, or ARS-5 random number generators, " +
            "nor block-splitting methods are exploited by this assembly.")]
        public void Uniform(
            int sampleSize,
            double[] destinationArray,
            int destinationIndex,
            double lowerBound,
            double upperBound)
        {
            ProbabilityDistribution.ValidateSampleInput(
                sampleSize, 
                destinationArray, 
                destinationIndex);

            UniformDistribution.ValidateBounds(
                lowerBound, 
                upperBound);

            unsafe
            {
                fixed (double* destinationPointer = &destinationArray[destinationIndex]) {
                    SafeNativeMethods.VSL.vdRngUniform(
                        0,
                        this.descriptor.DangerousGetHandle().ToPointer(),
                        sampleSize,
                        destinationPointer,
                        lowerBound,
                        upperBound);
                }
            }
        }


        /// <summary>
        /// Draws a sample from a Uniform distribution defined on the interval
        /// having <c>0</c> and <c>1</c> as bounds,
        /// and returns it in a destination array.
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
        /// <paramref name="destinationIndex"/> is 
        /// negative.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Parameter <paramref name="sampleSize"/> must be less than or 
        /// equal to the 
        /// difference between the length of 
        /// parameter <paramref name="destinationArray"/> 
        /// and <paramref name="destinationIndex"/>.
        /// </exception>
        /// <seealso href="https://en.wikipedia.org/wiki/Uniform_distribution_(continuous)"/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Performance",
            "CA1806:Do not ignore method results",
            Justification =
            "Parameters are expected to be well formed. " +
            "Additional error conditions should not be signaled since " +
            "no abstract, non-deterministic, or ARS-5 random number generators, " +
            "nor block-splitting methods are exploited by this assembly.")]
        public void DefaultUniform(
            int sampleSize,
            double[] destinationArray,
            int destinationIndex)
        {
            ProbabilityDistribution.ValidateSampleInput(
                sampleSize,
                destinationArray,
                destinationIndex);

            unsafe
            {
                fixed (double* destinationPointer = &destinationArray[destinationIndex]) {
                    SafeNativeMethods.VSL.vdRngUniform(
                        0,
                        this.descriptor.DangerousGetHandle().ToPointer(),
                        sampleSize,
                        destinationPointer,
                        0.0,
                        1.0);
                }
            }
        }

        /// <summary>
        /// Draws a sample point from a Uniform distribution defined on the interval
        /// having the specified bounds.
        /// </summary>
        /// <param name="lowerBound">The interval lower bound.</param>
        /// <param name="upperBound">The interval upper bound.</param>
        /// <returns>
        /// A sample point drawn from the specified 
        /// Uniform distribution.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="upperBound"/> is not greater 
        /// than <paramref name="lowerBound"/>.
        /// </exception>
        /// <seealso href="https://en.wikipedia.org/wiki/Uniform_distribution_(continuous)"/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Performance",
            "CA1806:Do not ignore method results",
            Justification =
                "Parameters are expected to be well formed. " +
                "Additional error conditions should not be signaled since " +
                "no abstract, non-deterministic, or ARS-5 random number generators, " +
                "nor block-splitting methods are exploited by this assembly.")]
        public double Uniform(
            double lowerBound,
            double upperBound)
        {
            UniformDistribution.ValidateBounds(
                lowerBound,
                upperBound);

            double sample;

            unsafe
            {
                SafeNativeMethods.VSL.vdRngUniform(
                    0,
                    this.descriptor.DangerousGetHandle().ToPointer(),
                    1,
                    &sample,
                    lowerBound,
                    upperBound);
            }

            return sample;
        }

        /// <summary>
        /// Draws a sample point from a Uniform distribution defined on the interval
        /// having <c>0</c> and <c>1</c> as bounds.
        /// </summary>
        /// <returns>
        /// A sample point drawn from a Uniform distribution defined on the interval
        /// having <c>0</c> and <c>1</c> as bounds.
        /// </returns>
        /// <seealso href="https://en.wikipedia.org/wiki/Uniform_distribution_(continuous)"/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Performance",
            "CA1806:Do not ignore method results",
            Justification =
            "Parameters are expected to be well formed. " +
            "Additional error conditions should not be signaled since " +
            "no abstract, non-deterministic, or ARS-5 random number generators, " +
            "nor block-splitting methods are exploited by this assembly.")]
        public double DefaultUniform()
        {
            double sample;

            unsafe
            {
                SafeNativeMethods.VSL.vdRngUniform(
                    0,
                    this.descriptor.DangerousGetHandle().ToPointer(),
                    1,
                    &sample,
                    0.0,
                    1.0);
            }

            return sample;
        }

        #endregion

        #region Gaussian

        /// <summary>
        /// Draws a sample point from a Gaussian distribution having the 
        /// specified parameters.
        /// </summary>
        /// <param name="sampleSize">The sample size.</param>
        /// <param name="destinationArray">The destination array that 
        /// receives the sampled data.</param>
        /// <param name="destinationIndex">The index in 
        /// <paramref name="destinationArray"/> at which storing 
        /// begins.</param>
        /// <param name="mu">The location parameter.</param>
        /// <param name="sigma">The scale parameter.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="destinationArray"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="sampleSize"/> is not positive.<br/>
        /// -or-<br/>
        /// <paramref name="destinationIndex"/> is 
        /// negative.<br/>
        /// -or-<br/>
        /// <paramref name="sigma"/> is not positive.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Parameter <paramref name="sampleSize"/> must be less than or 
        /// equal to the 
        /// difference between the length of 
        /// parameter <paramref name="destinationArray"/> 
        /// and <paramref name="destinationIndex"/>.
        /// </exception>
        /// <seealso href="https://en.wikipedia.org/wiki/Gaussian_distribution"/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Performance",
            "CA1806:Do not ignore method results",
            Justification =
                "Parameters are expected to be well formed. " +
                "Additional error conditions should not be signaled since " +
                "no abstract, non-deterministic, or ARS-5 random number generators, " +
                "nor block-splitting methods are exploited by this assembly.")]
        public void Gaussian(
        int sampleSize,
            double[] destinationArray,
            int destinationIndex,
            double mu,
            double sigma)
        {
            ProbabilityDistribution.ValidateSampleInput(
                sampleSize,
                destinationArray,
                destinationIndex);

            if (sigma <= 0) {
                throw new ArgumentOutOfRangeException(nameof(sigma),
                   ImplementationServices.GetResourceString(
                       "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            unsafe
            {
                fixed (double* destinationPointer = &destinationArray[destinationIndex]) {
                    SafeNativeMethods.VSL.vdRngGaussian(
                        0,
                        this.descriptor.DangerousGetHandle().ToPointer(),
                        sampleSize,
                        destinationPointer,
                        mu,
                        sigma);
                }
            }
        }

        /// <summary>
        /// Draws a sample point from a Gaussian distribution having the 
        /// specified parameters.
        /// </summary>
        /// <param name="mu">The location parameter.</param>
        /// <param name="sigma">The scale parameter.</param>
        /// <returns>
        /// A sample point drawn from the specified 
        /// Gaussian distribution.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="sigma"/> is not positive.
        /// </exception>
        /// <seealso href="https://en.wikipedia.org/wiki/Gaussian_distribution"/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Performance",
            "CA1806:Do not ignore method results",
            Justification =
                "Parameters are expected to be well formed. " +
                "Additional error conditions should not be signaled since " +
                "no abstract, non-deterministic, or ARS-5 random number generators, " +
                "nor block-splitting methods are exploited by this assembly.")]
        public double Gaussian(
            double mu,
            double sigma)
        {
            if (sigma <= 0) {
                throw new ArgumentOutOfRangeException(nameof(sigma),
                   ImplementationServices.GetResourceString(
                       "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            double sample;

            unsafe
            {
                SafeNativeMethods.VSL.vdRngGaussian(
                    0,
                    this.descriptor.DangerousGetHandle().ToPointer(),
                    1,
                    &sample,
                    mu,
                    sigma);
            }

            return sample;
        }

        #endregion

        #endregion
    }
}
