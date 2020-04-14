// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

using Novacta.Analytics.Infrastructure;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents an Exponential distribution.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Exponential density function can be written as follows:
    /// <latex mode='display'>
    /// f_{\lambda}\round{x} = 
    /// \begin{cases}
    /// \lambda\,\exp\round{-\lambda\,x} &amp; \mbox{if } x \geq 0 \\
    /// 0 &amp; \mbox{otherwise},
    /// \end{cases}
    /// </latex>
    /// where <latex>x \in \R</latex> and <latex>\lambda > 0</latex> 
    /// is the rate parameter of the distribution.
    /// </para>
    /// <para>
    /// The rate parameter <latex>\lambda</latex> can be get or set 
    /// through property <see cref="Rate"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="ProbabilityDistribution" />
    /// <seealso href="https://en.wikipedia.org/wiki/Exponential_distribution"/>
    public class ExponentialDistribution : ProbabilityDistribution
    {
        #region State

        private double lambda;

        /// <summary>
        /// Gets or sets the rate parameter of this
        /// instance.
        /// </summary>
        /// <value>The rate parameter.</value>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is not positive.
        /// </exception>
        public double Rate
        {
            get { return this.lambda; }
            set
            {
                if (value <= 0) {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
                }

                this.lambda = value;
            }
        }

        #endregion

        #region Constructors and factory methods

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="ExponentialDistribution"/> class
        /// having the specified rate parameter.
        /// </summary>
        /// <param name="rate">The rate.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="rate"/> is not positive.
        /// </exception>
        public ExponentialDistribution(double rate)
        {
            if (rate <= 0) {
                throw new ArgumentOutOfRangeException(nameof(rate),
                   ImplementationServices.GetResourceString(
                       "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            this.lambda = rate;
        }

        #endregion

        #region Distribution

        /// <inheritdoc/>
        public override double Cdf(double argument)
        {
            if (argument >= 0.0)
                return 1.0 - Math.Exp(-this.lambda * argument);

            return 0.0;
        }

        /// <inheritdoc/>
        public override double InverseCdf(double argument)
        {
            return (argument < 0 || 1 < argument) ? 
                Double.NaN : - Math.Log(1.0 - argument) / this.lambda;
        }

        /// <inheritdoc/>
        public override bool CanInvertCdf
        {
            get { return true; }
        }

        /// <inheritdoc/>
        public override double Pdf(double argument)
        {
            double lambda = this.lambda;

            if (argument >= 0.0)
                return lambda * Math.Exp(-lambda * argument);

            return 0.0;
        }

        #endregion

        #region Moments

        /// <inheritdoc/>
        public override double Mean()
        {
            return 1.0 / this.lambda;
        }

        /// <inheritdoc/>
        public override double Variance()
        {
            double lambda = this.lambda;
            return 1.0 / (lambda * lambda);
        }

        #endregion

        #region Sample

        /// <inheritdoc/>
        public override double Sample()
        {
            return -Math.Log(this.RandomNumberGenerator.DefaultUniform()) / this.lambda;
        }


        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Design", 
            "CA1062:Validate arguments of public methods", 
            Justification = "Input validation delegated to ProbabilityDistribution.ValidateSampleInput.")]
        protected sealed override void OnSample(
            int sampleSize, double[] destinationArray, int destinationIndex)
        {
            double constant = -1.0 / this.lambda;

            for (int i = 0; i < sampleSize; i++) {
                destinationArray[i + destinationIndex] =
                    constant * Math.Log(this.RandomNumberGenerator.DefaultUniform());
            }
        }

        #endregion
    }
}
