// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using Novacta.Analytics.Infrastructure;

namespace Novacta.Analytics
{
    /// <summary>
    /// Provides methods representing some functions
    /// commonly accepted as special.
    /// </summary>
    /// <seealso href="https://en.wikipedia.org/wiki/Special_functions"/>
    public static class SpecialFunctions
    {
        #region Log of Gamma

        private static readonly double[] _GammaLanczosCoefficients =
            new double[6] {
                 76.18009172947146,
                -86.50532032941677,
                 24.01409824083091,
                -01.231739572450155,
                  0.1208650973866179e-2,
                -00.5395239384953e-5 };

        private static readonly double _SqrtPiBy2 =
            Math.Sqrt(2.0 * Math.PI);

        /// <summary>
        /// Computes the natural logarithm of the Gamma function.
        /// </summary>
        /// <param name="x">
        /// The argument at which the function must be evaluated.
        /// </param>
        /// <returns>
        /// The value taken on by the function.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="x"/> is not positive.
        /// </exception>
        /// <seealso href="https://en.wikipedia.org/wiki/Gamma_function#The_log-gamma_function"/>
        public static double LogGamma(double x)
        {
            if (x <= 0.0)
            {
                throw new ArgumentOutOfRangeException(
                   nameof(x),
                   ImplementationServices.GetResourceString(
                       "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            // This is based on Lanczos, C., 1964, 
            // SIAM Journal on Numerical Analysis, Series B, 
            // Vol. 1, pp. 86-96.
            //
            // Sketch of the methodology:
            // Approximate log(T(x+1)), hence subtract log(x) 
            // so exploiting the relation T(x+1) = x*T(x).   

            double[] c = SpecialFunctions._GammaLanczosCoefficients;

            double t = x + 5.5;
            double series = 1.000000000190015; // first coefficient

            for (int j = 0; j < c.Length; j++)
            {
                series += c[j] / (x + j + 1);
            }

            return -t + (x + 0.5) * Math.Log(t) +
                Math.Log(_SqrtPiBy2 * series / x);
        }

        #endregion

        #region Factorial

        /// <summary>
        /// Computes the Factorial function of the 
        /// specified integer.
        /// </summary>
        /// <param name="n">
        /// The argument at which the function must be evaluated.
        /// </param>
        /// <returns>
        /// The value taken on by the function.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="n"/> is negative.
        /// </exception>
        /// <seealso href="https://en.wikipedia.org/wiki/Factorial"/>
        public static double Factorial(int n)
        {
            if (n < 0.0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(n),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE"));
            }

            // Returns exact values for n <= 170,
            // 170! being the largest factorial whose floating-point 
            // approximation can be represented as a 64-bit IEEE 754 
            // floating -point value. 
            if (n <= 170)
            {
                double f = 1.0;
                int i = 2;
                while (i <= n)
                {
                    f *= i++;
                }
                return f;
            }

            // Otherwise, return +Inf
            return Double.PositiveInfinity;
        }

        #endregion

        #region Binomial coefficient

        /// <summary>
        /// Computes the Binomial coefficient of the specified
        /// pair of integers.
        /// </summary>
        /// <param name="n">
        /// The number of available items.
        /// </param>
        /// <param name="k">
        /// The number of items to be taken at a time.
        /// </param>
        /// <remarks>
        /// This is the number of combinations 
        /// of <paramref name="n"/> items taken
        /// <paramref name="k"/> at a time.
        /// </remarks>
        /// <returns>
        /// The value taken on by the Binomial coefficient.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="n"/> is negative.<br/>
        /// -or-<br/>
        /// <paramref name="k"/> is negative.<br/>
        /// -or-<br/>
        /// <paramref name="k"/> is greater than 
        /// <paramref name="n"/>.
        /// </exception>
        /// <seealso href="https://en.wikipedia.org/wiki/Binomial_coefficient"/>
        public static double BinomialCoefficient(int n, int k)
        {
            if (n < 0.0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(n),
                   ImplementationServices.GetResourceString(
                       "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE"));
            }

            if (k < 0.0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(k),
                   ImplementationServices.GetResourceString(
                       "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE"));
            }

            if (k > n)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(k),
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_NOT_GREATER_THAN_OTHER"),
                        nameof(k),
                        nameof(n)));
            }

            return Math.Floor(0.5 + Math.Exp(LogGamma(n + 1.0)
                - LogGamma(k + 1.0) - LogGamma(n - k + 1.0)));
        }

        #endregion
    }
}
