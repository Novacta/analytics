// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using Novacta.Analytics.Infrastructure;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents a Bernoulli distribution.
    /// </summary>
    /// <remarks>
    /// <para id='description'>
    /// A <see cref="BernoulliDistribution"/> instance acts for
    /// the probability distribution of a random variable which
    /// takes value <c>1</c> with a probability equal to 
    /// <latex>\theta</latex> and value <c>0</c>
    /// with probability <latex>1-\theta</latex>,
    /// for <latex>0 \leq \theta \leq 1</latex>. 
    /// </para>
    /// <para>
    /// Method <see cref="Pdf(double)"/> thus implements a
    /// probability function satisfying:
    /// <latex mode="display">
    /// p_{\theta}\round{x} = \begin{cases}
    ///                       \theta &amp; \mbox{if } x=1 \\
    ///                       1-\theta &amp; \mbox{if } x=0 \\
    ///                       0 &amp; \mbox{otherwise}.
    ///                       \end{cases}
    /// </latex>
    /// </para>
    /// <para>
    /// Parameter <latex>\theta</latex> can be get or set
    /// via property <see cref="SuccessProbability"/>.</para>
    /// </remarks>
    /// <seealso cref="ProbabilityDistribution" />
    /// <seealso href="https://en.wikipedia.org/wiki/Bernoulli_distribution"/>
    public sealed class BernoulliDistribution : ProbabilityDistribution
    {
        #region State

        private double successProbability;

        /// <summary>
        /// Gets or sets the success probability of this instance.
        /// </summary>
        /// <value>The success probability.</value>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is less than <c>0</c> or 
        /// greater than <c>1</c>.
        /// </exception>        
        public double SuccessProbability
        {
            get
            {
                return this.successProbability;
            }
            set
            {
                if (value < 0 || 1 < value) {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        string.Format(
                            CultureInfo.InvariantCulture,
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL"),
                            "0", 
                            "1"));
                }

                this.successProbability = value;
            }
        }

        #endregion

        #region Constructors and factory methods

        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="BernoulliDistribution"/> class
        /// having the specified success probability.
        /// </summary>
        /// <param name="successProbability">The success probability.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="successProbability"/> is less than <c>0</c> or 
        /// greater than <c>1</c>.
        /// </exception>
        public BernoulliDistribution(double successProbability)
        {
            if (successProbability < 0 || 1 < successProbability) {
                throw new ArgumentOutOfRangeException(nameof(successProbability),
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_CLOSED_INTERVAL"),
                        "0", 
                        "1"));
            }

            this.successProbability = successProbability;
        }

        /// <summary>
        /// Creates a Bernoulli distribution having success
        /// probability equal to <c>1/2</c>.
        /// </summary>
        /// <returns>A balanced Bernoulli distribution.</returns>
        public static BernoulliDistribution Balanced()
        {
            return new BernoulliDistribution(.5);
        }

        #endregion

        #region Distribution 

        /// <inheritdoc/>
        public override double Cdf(double argument)
        {
            double p = this.successProbability;

            if (argument >= 1.0) { // Here 1.0 <= argument
                return 1.0;
            }
            else { // Here argument < 1.0
                if (argument >= 0.0) { // Here 0 <= argument < 1.0
                    return 1.0 - p;
                }
            }

            // Here argument < 0.0
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
            double p = this.successProbability;

            if (argument == 1.0) {
                return p;
            }
            else {
                if (argument == 0.0) {
                    return 1.0 - p;
                }
            }

            // Here argument is not 0.0 nor 1.0
            return 0.0;
        }

        #endregion

        #region Moments

        /// <inheritdoc/>
        public override double Mean()
        {
            return this.successProbability;
        }

        /// <inheritdoc/>
        public override double Variance()
        {
            double p = this.successProbability;

            return p * (1 - p);
        }

        #endregion

        #region Sample

        /// <inheritdoc/>
        public override double Sample()
        {
            return (this.RandomNumberGenerator.DefaultUniform() <=
                this.successProbability) ? 1.0 : 0.0;
        }

        /// <inheritdoc/>
        protected sealed override void OnSample(
            int sampleSize,
            double[] destinationArray,
            int destinationIndex)
        {
            this.RandomNumberGenerator.DefaultUniform(
                sampleSize,
                destinationArray,
                destinationIndex);

            double p = this.successProbability;
            int j;

            for (int i = 0; i < sampleSize; i++) {
                j = i + destinationIndex;
                destinationArray[j] = 
                    (destinationArray[j] <= p) ? 1.0 : 0.0;
            }
        }

        #endregion
    }
}

