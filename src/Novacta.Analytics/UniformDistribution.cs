// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using Novacta.Analytics.Infrastructure;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents a Uniform distribution.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Uniform density function can be written as follows:
    /// <latex mode='display'>
    /// f_{l,u}\round{x} = 
    /// \begin{cases}
    /// \frac{1}{u-l} &amp; \mbox{if } l \leq x \leq u \\
    /// 0 &amp; \mbox{otherwise},
    /// \end{cases}
    /// </latex>
    /// where <latex>x \in \R</latex>, while <latex>l</latex> 
    /// and <latex>u</latex> 
    /// are the lower and upper bounding parameters of the distribution,
    /// with <latex>l &lt; u</latex>.
    /// </para>
    /// <para>
    /// The lower bound parameter, <latex>l</latex>, and the upper bound one, 
    /// <latex>u</latex>, can be get through the properties 
    /// <see cref="LowerBound"/> and <see cref="UpperBound"/>, respectively.
    /// </para>
    /// </remarks>
    /// <seealso cref="ProbabilityDistribution" />
    /// <seealso href="https://en.wikipedia.org/wiki/Uniform_distribution_(continuous)"/>
    public class UniformDistribution : ProbabilityDistribution
    {
        #region State

        private readonly double lowerBound;

        /// <summary>
        /// Gets the lower bound of the interval on which
        /// this instance is defined.
        /// </summary>
        /// <value>The interval lower bound.</value>
        public double LowerBound { get { return this.lowerBound; } } 

        private readonly double upperBound;

        /// <summary>
        /// Gets the upper bound of the interval on which
        /// this instance is defined.
        /// </summary>
        /// <value>The interval upper bound.</value>
        public double UpperBound { get { return this.upperBound; } }

        #endregion

        #region Constructors and factory methods

        /// <summary>
        /// Initializes a new instance of the <see cref="UniformDistribution"/> class
        /// defined on an interval having the specified lower and upper bounds.
        /// </summary>
        /// <param name="lowerBound">The interval lower bound.</param>
        /// <param name="upperBound">The interval upper bound.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="upperBound"/> is not greater 
        /// than <paramref name="lowerBound"/>.
        /// </exception>
        public UniformDistribution(double lowerBound, double upperBound)
        {
            UniformDistribution.ValidateBounds(lowerBound, upperBound);

            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }

        internal static void ValidateBounds(
             double lowerBound,
             double upperBound)
        {
            if (upperBound <= lowerBound) {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER"),
                        nameof(upperBound),
                        nameof(lowerBound)),
                    nameof(upperBound));
            }
        }

        /// <summary>
        /// Creates a Uniform distribution on the interval 
        /// having lower and upper bounds equal to <c>0</c> and <c>1</c>,
        /// respectively.
        /// </summary>
        /// <returns>A default Uniform distribution.</returns>
        public static UniformDistribution Default()
        {
            return new UniformDistribution(0.0, 1.0);
        }

        #endregion

        #region Distribution

        /// <inheritdoc/>
        public override bool CanInvertCdf
        {
            get
            {
                return true;
            }
        }

        /// <inheritdoc/>
        public override double Cdf(double argument)
        {
            double lowerBound = this.lowerBound;
            double upperBound = this.upperBound;

            if (argument < lowerBound) {
                return 0.0;
            }
            if (upperBound <= argument) {
                return 1.0;
            }

            return (argument - lowerBound) / (upperBound - lowerBound);
        }

        /// <inheritdoc/>
        public override double InverseCdf(double argument)
        {
            if (argument < 0.0 || 1.0 < argument) {
                return Double.NaN;
            }

            double lowerBound = this.lowerBound;
            return lowerBound + argument * (this.upperBound - lowerBound);
        }

        /// <inheritdoc/>
        public override double Pdf(double argument)
        {
            double lowerBound = this.lowerBound;
            double upperBound = this.upperBound;

            if (argument < lowerBound || upperBound < argument) {
                return 0.0;
            }

            return 1.0 / (upperBound - lowerBound);
        }

        #endregion

        #region Moments

        /// <inheritdoc/>
        public override double Mean()
        {
            return 0.5 * (this.upperBound + this.lowerBound);
        }

        /// <inheritdoc/>
        public override double Variance()
        {
            return Math.Pow(this.upperBound - this.lowerBound, 2.0) / 12.0;
        }

        #endregion

        #region Sample

        /// <inheritdoc/>
        public override double Sample()
        {
            return this.RandomNumberGenerator.Uniform(
                this.lowerBound,
                this.upperBound);
        }

        /// <inheritdoc/>
        protected override void OnSample(
            int sampleSize,
            double[] destinationArray,
            int destinationIndex)
        {
            this.RandomNumberGenerator.Uniform(
                sampleSize,
                destinationArray,
                destinationIndex,
                this.lowerBound,
                this.upperBound);
        }

        #endregion
    }
}
