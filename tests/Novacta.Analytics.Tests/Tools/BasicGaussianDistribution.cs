// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Interop;
using System;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Represents the Gaussian distribution by
    /// implementing only the <see cref="ProbabilityDistribution" />
    /// abstract methods. No overrides for virtual methods.
    /// </summary>
    /// <seealso cref="ProbabilityDistribution" />
    class BasicGaussianDistribution : ProbabilityDistribution
    {
        #region State

        private double sigma;

        /// <summary>
        /// Gets or sets the location parameter of this instance.
        /// </summary>
        /// <value>The location parameter.</value>
        public double Mu { get; set; }

        /// <summary>
        /// Gets or sets the scale parameter of this
        /// instance.
        /// </summary>
        /// <value>The scale parameter.</value>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is not positive.
        /// </exception>
        public double Sigma
        {
            get { return this.sigma; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
                }

                this.sigma = value;
            }
        }

        #endregion

        #region Constructors and Factory Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="GaussianDistribution"/> class
        /// having the specified parameters.
        /// </summary>
        /// <param name="mu">The location parameter.</param>
        /// <param name="sigma">The scale parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="sigma"/> is not positive.
        /// </exception>
        public BasicGaussianDistribution(double mu, double sigma)
        {
            if (sigma <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sigma),
                   ImplementationServices.GetResourceString(
                       "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            this.Mu = mu;
            this.sigma = sigma;
        }

        /// <summary>
        /// Creates a Gaussian distribution having zero mean
        /// and unit variance.
        /// </summary>
        /// <returns>A Standard Gaussian distribution.</returns>
        public static GaussianDistribution Standard()
        {
            return new GaussianDistribution(0.0, 1.0);
        }

        #endregion

        public override bool CanInvertCdf { get => true; }

        private static readonly double[] CdfConstants = 
            [
                1.25331413731550025,
                0.421369229288054473,
                0.236652382913560671,
                0.162377660896867462,
                0.123131963257932296,
                0.0990285964717319214,
                0.0827662865013691773,
                0.0710695805388521071,
                0.0622586659950261958 ];

        private static double Phi(double x)
        {
            double abs_x = Math.Abs(x);
            int i;
            double s = 1.0;
            if (abs_x < 17)
            {
                int j = Convert.ToInt32(Math.Floor(.5 * (abs_x + 1)));
                double p = 1;
                double a = CdfConstants[j];
                double z = 2 * j;
                double b = a * z - 1;
                double h = abs_x - z;
                s = a + h * b;

                double t = a;
                double q = h * h;

                for (i = 2; s != t; i += 2)
                {
                    a = (a + z * b) / i;
                    b = (b + z * a) / (i + 1);
                    p *= q;
                    t = s;
                    s += p * (a + h * b);
                }

                s *= Math.Exp(-.5 * x * x - .91893853320467274178);
            }

            if (x >= 0)
                return s;

            return 1.0 - s;
        }

        /// <inheritdoc/>
        public override double Cdf(double argument)
        {
            return 1.0 - BasicGaussianDistribution.Phi((argument - this.Mu) / this.sigma);
        }

        public override double InverseCdf(double argument)
        {
            double mu = this.Mu;
            double sigma = this.sigma;

            double y;

            unsafe
            {
                SafeNativeMethods.VML.vdCdfNormInv(1, &argument, &y);
            }

            return y * sigma + mu;
        }

        public override double Mean()
        {
            return this.Mu;
        }

        public override double Pdf(double argument)
        {
            double sigma = this.sigma;

            double c = Math.Sqrt(2.0 * Math.PI) * sigma;

            double z = (argument - this.Mu) / sigma;

            return Math.Exp(-0.5 * z * z) / c;
        }

        public override double Sample()
        {
            return this.RandomNumberGenerator.Gaussian(this.Mu, this.sigma);
        }

        public override double Variance()
        {
            return Math.Pow(this.sigma, 2.0);
        }
    }
}
