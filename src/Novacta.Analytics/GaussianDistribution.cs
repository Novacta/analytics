// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

using Novacta.Analytics.Interop;
using Novacta.Analytics.Infrastructure;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents a Gaussian distribution.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The Gaussian density function can be written as follows:
    /// <latex mode='display'>\frac{1}{\sqrt{2\,\pi\,\sigma^2}}\exp\round{-\frac{\round{x-\mu}^2}{2\,\sigma^2}}</latex>
    /// where <latex>x \in \R</latex>. 
    /// </para>
    /// <para>
    /// The location parameter, <latex>\mu \in \R</latex>, and the scale parameter, 
    /// <latex>\sigma > 0</latex> can be get or set through the properties 
    /// <see cref="Mu"/> and <see cref="Sigma"/>, respectively.
    /// </para>
    /// </remarks>
    /// <seealso cref="ProbabilityDistribution" />
    /// <seealso href="https://en.wikipedia.org/wiki/Gaussian_distribution"/>
    public sealed class GaussianDistribution : ProbabilityDistribution
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
                if (value <= 0) {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
                }

                this.sigma = value;
            }
        }

        #endregion

        #region Constructors and factory methods

        /// <summary>
        /// Initializes a new instance of the <see cref="GaussianDistribution"/> class
        /// having the specified parameters.
        /// </summary>
        /// <param name="mu">The location parameter.</param>
        /// <param name="sigma">The scale parameter.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="sigma"/> is not positive.
        /// </exception>
        public GaussianDistribution(double mu, double sigma)
        {
            if (sigma <= 0) {
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

        #region Distribution

        private static readonly double[] cdfConstants
            = new double[9] {
                1.25331413731550025,
                0.421369229288054473,
                0.236652382913560671,
                0.162377660896867462,
                0.123131963257932296,
                0.0990285964717319214,
                0.0827662865013691773,
                0.0710695805388521071,
                0.0622586659950261958 };

    private static double Phi(double x)
        {
            double abs_x = Math.Abs(x);
            int i;
            double s = 1.0;
            if (abs_x < 17) {
                int j = Convert.ToInt32(Math.Floor(.5 * (abs_x + 1)));
                double p = 1;
                double a = cdfConstants[j];  
                double z = 2 * j; 
                double b = a * z - 1; 
                double h = abs_x - z;  
                s = a + h * b;  

                double t = a; 
                double q = h * h; 

                for (i = 2; s != t; i += 2) {
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
            return 1.0 - GaussianDistribution.Phi((argument - this.Mu) / this.sigma);
        }

        /// <inheritdoc/>
        public override DoubleMatrix InverseCdf(DoubleMatrix arguments)
        {
            if (arguments is null) {
                throw new ArgumentNullException(nameof(arguments));
            }

            double[] a = arguments.AsColumnMajorDenseArray();

            int numberOfArguments = arguments.Count;

            var results = DoubleMatrix.Dense(
                arguments.NumberOfRows,
                arguments.NumberOfColumns);

            double[] y = results.GetStorage();

            unsafe
            {
                fixed (double* yPointer = &y[0], aPointer = &a[0]) 
                {
                    SafeNativeMethods.VML_vdCdfNormInv(
                        numberOfArguments, 
                        aPointer, 
                        yPointer);
                }
            }

            var mu = this.Mu;
            for (int i = 0; i < y.Length; i++) {
                y[i] = y[i] * this.sigma + mu;
            }

            return results;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// <para>
        /// If <paramref name="argument"/> is outside
        /// the interval <c>[0, 1]</c>, the returned value is
        /// <see cref="System.Double.NaN"/>.
        /// </para>
        /// </remarks>
        public override double InverseCdf(double argument)
        {
            double mu = this.Mu;
            double sigma = this.sigma;

            double y;

            unsafe
            {
                SafeNativeMethods.VML_vdCdfNormInv(1, &argument, &y);
            }

            return y * sigma + mu;
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

            double mu = this.Mu;
            double sigma = this.sigma;

            DoubleMatrix results = DoubleMatrix.Dense(
                arguments.NumberOfRows, arguments.NumberOfColumns);
            double[] resultsArray = results.GetStorage();
            double z;
            double c = Math.Sqrt(2.0 * Math.PI) * sigma;

            for (int i = 0; i < resultsArray.Length; i++) {
                z = (arguments[i] - mu) / sigma;

                resultsArray[i] = Math.Exp(-0.5 * z * z) / c;
            }

            return results;
        }

        /// <inheritdoc/>
        public override double Pdf(double argument)
        {
            double sigma = this.sigma;

            double c = Math.Sqrt(2.0 * Math.PI) * sigma;

            double z = (argument - this.Mu) / sigma;

            return Math.Exp(-0.5 * z * z) / c;
        }

        #endregion

        #region Moments

        /// <inheritdoc/>
        public sealed override double Mean()
        {
            return this.Mu;
        }

        /// <inheritdoc/>
        public sealed override double Variance()
        {
            return Math.Pow(this.sigma, 2.0);
        }

        /// <inheritdoc/>
        public override double StandardDeviation()
        {
            return this.sigma;
        }

        #endregion

        #region Sample

        /// <inheritdoc/>
        public sealed override double Sample()
        {
            return this.RandomNumberGenerator.Gaussian(this.Mu, this.sigma);
        }

        /// <inheritdoc/>
        protected sealed override void OnSample(
            int sampleSize, double[] destinationArray, int destinationIndex)
        {
            this.RandomNumberGenerator.Gaussian(
                sampleSize, 
                destinationArray, 
                destinationIndex, 
                this.Mu, 
                this.sigma);
        }

        #endregion
    }
}
