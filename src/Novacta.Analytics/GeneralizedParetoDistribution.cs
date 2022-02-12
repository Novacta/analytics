// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using Novacta.Analytics.Infrastructure;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents a Generalized Pareto distribution.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Generalized Pareto density function can be written as
    /// <latex mode='display'>
    /// f_{\xi,\mu,\sigma}\round{x} = 
    /// \frac{1}{\sigma}\round{1+\frac{\xi\, \round{ x-\mu}}{\sigma}}^{\round{-\frac{1}{\xi}-1}}
    /// </latex>
    /// where <latex>x \geq \mu</latex> if <latex>\xi \geq 0</latex>, and
    /// <latex>\mu \leq x \leq \mu - \sigma/\xi</latex> if <latex>\xi &lt; 0</latex>. 
    /// The location parameter, <latex>\mu \in \R</latex>, the scale parameter, 
    /// <latex>\sigma>0</latex>, and the shape parameter, <latex>\xi \in \R</latex>
    /// can be get or set through the properties 
    /// <see cref="Mu"/>, <see cref="Sigma"/>, and <see cref="Xi"/>, respectively.
    /// </para>
    /// </remarks>
    /// <seealso cref="ProbabilityDistribution" />
    /// <seealso href="https://en.wikipedia.org/wiki/Generalized_Pareto_distribution"/>
    public class GeneralizedParetoDistribution : ProbabilityDistribution
    {
        #region State

        private double xi;
        private double mu;
        private double sigma;

        /// <summary>
        /// Gets or sets the scale parameter of this instance.
        /// </summary>
        /// <value>The scale parameter.</value>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is not positive.
        /// </exception>
        public double Sigma
        {
            get
            {
                return this.sigma;
            }
            set
            {
                if (value <= 0) {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
                }

                this.sigma = value;
            }
        }

        /// <summary>
        /// Gets or sets the shape parameter of this instance.
        /// </summary>
        /// <value>The shape parameter.</value>
        public double Xi
        {
            get
            {
                return this.xi;
            }
            set
            {
                this.xi = value;
            }
        }

        /// <summary>
        /// Gets or sets the location parameter of this instance.
        /// </summary>
        /// <value>The location parameter.</value>
        public double Mu
        {
            get
            {
                return this.mu;
            }
            set
            {
                this.mu = value;
            }
        }

        #endregion

        #region Constructors and Factory Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralizedParetoDistribution"/> class
        /// having the specified parameters.
        /// </summary>
        /// <param name="mu">The location parameter.</param>
        /// <param name="sigma">The scale parameter.</param>
        /// /// <param name="xi">The shape parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="sigma"/> is not positive.
        /// </exception>
        public GeneralizedParetoDistribution(
            double mu,
            double sigma,
            double xi)
        {
            if (sigma <= 0) {
                throw new ArgumentOutOfRangeException(nameof(sigma),
                   ImplementationServices.GetResourceString(
                       "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            this.mu = mu;
            this.sigma = sigma;
            this.xi = xi;
        }

        #endregion

        #region Distribution

        /// <inheritdoc/>
        public override DoubleMatrix Cdf(DoubleMatrix arguments)
        {
            if (arguments is null) {
                throw new ArgumentNullException(nameof(arguments));
            }

            double xi = this.xi;
            double mu = this.mu;
            double sigma = this.sigma;
            double z;

            DoubleMatrix results = DoubleMatrix.Dense(
                arguments.NumberOfRows, arguments.NumberOfColumns);

            if (xi == 0.0) {
                for (int i = 0; i < results.Count; i++) {
                    z = (arguments[i] - mu) / sigma;

                    if (z < 0.0)
                        continue; // Out of support: the CDF is 0.0

                    results[i] = 1.0 - Math.Exp(-z);
                }
            }
            else { // Here xi != 0.0
                double exponent = -1.0 / xi;
                for (int i = 0; i < results.Count; i++) {
                    z = (arguments[i] - mu) / sigma;

                    if (xi > 0.0) {
                        // Support for z is the interval [0, +Inf]
                        if (z < 0.0)
                            continue; 
                    }
                    else { // Here if xi < 0.0
                        // Support for z is the interval [0, exponent]
                        if (z < 0.0) 
                            continue;
                        else if (z > exponent) {
                            results[i] = 1.0;
                            continue;
                        }
                    }

                    results[i] = 1.0 - Math.Pow(1 + xi * z, exponent);
                }
            }

            return results;
        }

        /// <inheritdoc/>
        public override double Cdf(double argument)
        {
            double xi = this.xi;
            double mu = this.mu;
            double sigma = this.sigma;
            double z;

            if (xi == 0.0) {
                z = (argument - mu) / sigma;

                if (z < 0.0)
                    return 0.0; // Out of support: the CDF is 0.0

                return 1.0 - Math.Exp(-z);
            }
            else { // Here xi != 0.0
                double exponent = -1.0 / xi;
                z = (argument - mu) / sigma;

                if (xi > 0.0) {
                    // Support for z is the interval [0, +Inf]
                    if (z < 0.0)
                        return 0.0; 
                }
                else { // Here if xi < 0.0
                    // Support for z is the interval [0, exponent]
                    if (z < 0.0) {
                        return 0.0; 
                    }
                    else if (z > exponent) {
                        return 1.0; 
                    }
                }

                return 1.0 - Math.Pow(1 + xi * z, exponent);
            }
        }

        /// <inheritdoc/>
        public override DoubleMatrix InverseCdf(DoubleMatrix arguments)
        {
            if (arguments is null) {
                throw new ArgumentNullException(nameof(arguments));
            }

            double xi = this.xi;
            double mu = this.mu;
            double sigma = this.sigma;

            DoubleMatrix results = DoubleMatrix.Dense(
                arguments.NumberOfRows, arguments.NumberOfColumns, Double.NaN);

            double argument;
            if (xi == 0.0) {
                //sample = mu - sigma * Math.Log(u);
                for (int i = 0; i < arguments.Count; i++) {
                    argument = arguments[i];
                    if (argument <= 0.0 || 1.0 <= argument) {
                        results[i] = Double.NaN;
                    }
                    else {
                        results[i] = mu - sigma * Math.Log(1.0 - argument);
                    }
                }
            }
            else {
                double c = sigma / xi;

                for (int i = 0; i < arguments.Count; i++) {
                    argument = arguments[i];
                    if (argument <= 0.0 || 1.0 <= argument) {
                        results[i] = Double.NaN;
                    }
                    else {
                        results[i] =
                            mu + (Math.Pow(1.0 - arguments[i], -xi) - 1.0) * c;
                    }
                }
            }

            return results;

        }

        /// <inheritdoc/>
        /// <remarks>
        /// <para>
        /// If <paramref name="argument"/> is outside
        /// the interval <c>(0, 1)</c>, the returned value is
        /// <see cref="System.Double.NaN"/>.
        /// </para>
        /// </remarks>
        public override double InverseCdf(double argument)
        {
            double xi = this.xi;
            double mu = this.mu;
            double sigma = this.sigma;

            if (argument <= 0.0 || 1.0 <= argument) {
                return Double.NaN;
            }

            if (xi == 0.0)
                return mu - sigma * Math.Log(1.0 - argument);
            else
                return mu + (Math.Pow(1.0 - argument, -xi) - 1.0) * sigma / xi;
        }

        /// <inheritdoc/>
        public override bool CanInvertCdf
        {
            get { return true; }
        }

        /// <inheritdoc/>
        public override DoubleMatrix Pdf(DoubleMatrix arguments)
        {
            if (arguments is null) {
                throw new ArgumentNullException(nameof(arguments));
            }

            double xi = this.xi;
            double mu = this.mu;
            double sigma = this.sigma;
            double z;

            DoubleMatrix results = DoubleMatrix.Dense(
                arguments.NumberOfRows, arguments.NumberOfColumns);

            if (xi == 0.0) {
                for (int i = 0; i < results.Count; i++) {
                    z = (arguments[i] - mu) / sigma;

                    if (z < 0.0)
                        continue; // Out of support: the PDF is 0.0

                    results[i] = Math.Exp(-z) / sigma;
                }
            }
            else { // Here xi != 0.0
                double exponent = -1.0 / xi;
                for (int i = 0; i < results.Count; i++) {
                    z = (arguments[i] - mu) / sigma;

                    if (xi > 0.0) {
                        if (z < 0.0)
                            continue; // Out of support: the PDF is 0.0
                    }
                    else { // Here if xi < 0.0
                        if (z < 0.0 || z > exponent)
                            continue; // Out of support: the PDF is 0.0
                    }

                    results[i] = Math.Pow(1 + xi * z, exponent - 1.0) / sigma;
                }
            }

            return results;
        }

        /// <inheritdoc/>
        public override double Pdf(double argument)
        {
            double xi = this.xi;
            double mu = this.mu;
            double sigma = this.sigma;
            double z;

            if (xi == 0.0) {
                z = (argument - mu) / sigma;

                if (z < 0.0)
                    return 0.0; // Out of support: the PDF is 0.0

                return Math.Exp(-z) / sigma;
            }
            else { // Here xi != 0.0
                double exponent = -1.0 / xi;
                z = (argument - mu) / sigma;

                if (xi > 0.0) {
                    if (z < 0.0)
                        return 0.0; // Out of support: the PDF is 0.0
                }
                else { // Here if xi < 0.0
                    if (z < 0.0 || z > exponent)
                        return 0.0; // Out of support: the PDF is 0.0
                }

                return Math.Pow(1 + xi * z, exponent - 1.0) / sigma;
            }
        }

        #endregion

        #region Moments

        /// <inheritdoc/>
        public override double Mean()
        {
            double xi = this.xi;
            double mu = this.mu;
            double sigma = this.sigma;

            // The mean of the GP is not finite when Xi ≥ 1, 
            // Xi being the shape parameter.
            return (xi < 1.0) ? mu + sigma / (1.0 - xi) : Double.PositiveInfinity;
        }

        /// <inheritdoc/>
        public override double Variance()
        {
            double xi = this.xi;
            double sigma = this.sigma;
            double variance;

            // The variance of the GP is not finite when Xi ≥ 1/2, 
            // Xi being the shape parameter.
            if (xi < .5) {
                double oneMinusXi = 1.0 - xi;
                variance = sigma * sigma / (oneMinusXi * oneMinusXi *
                    (1.0 - 2.0 * xi));
            }
            else
                variance = Double.PositiveInfinity;

            return variance;
        }

        #endregion

        #region Sample

        /// <inheritdoc/>
        public override Double Sample()
        {
            double xi = this.xi;
            double mu = this.mu;
            double sigma = this.sigma;
            double u, sample;

            u = this.RandomNumberGenerator.DefaultUniform();

            if (xi == 0.0)
                sample = mu - sigma * Math.Log(u);
            else
                sample = mu + (Math.Pow(u, -xi) - 1.0) * sigma / xi;

            return sample;
        }


        /// <inheritdoc/>
        protected sealed override void OnSample(
            int sampleSize, double[] destinationArray, int destinationIndex)
        {
            double xi = this.xi;
            double mu = this.mu;
            double sigma = this.sigma;

            double u;

            if (xi == 0.0) {
                //sample = mu - sigma * Math.Log(u);
                for (int i = 0; i < sampleSize; i++) {
                    u = this.RandomNumberGenerator.DefaultUniform();
                    destinationArray[i + destinationIndex] = 
                        mu - sigma * Math.Log(u);
                }
            }
            else {
                double c = sigma / xi;
                //sample = mu + (Math.Pow(u, -xi) - 1.0) * sigma / xi;
                for (int i = 0; i < sampleSize; i++) {
                    u = this.RandomNumberGenerator.DefaultUniform();
                    destinationArray[i + destinationIndex] = 
                        mu + (Math.Pow(u, -xi) - 1.0) * c;
                }
            }
        }

        #endregion
    }
}
