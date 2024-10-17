// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics
{
    /// <summary>
    /// Provides methods to numerically approximate
    /// first or second order derivatives of
    /// functions having multidimensional 
    /// arguments.
    /// </summary>
    public static class NumericalDifferentiation
    {
        #region Differencing delta

        /// <summary>
        /// Machine epsilon.
        /// </summary>
        private static readonly double machineEpsilon = Math.Pow(2.0, -53.0);

        /// <summary>
        /// Dummy function needed to avoid rounding errors in 
        /// approximations.
        /// </summary>
        /// <param name="_">
        /// A value representing the sum of
        /// a function argument to a small increment.
        /// </param>
        /// <remarks>
        /// See <see href="http://en.wikipedia.org/wiki/Numerical_differentiation"/> 
        /// and Section 5.7 of 
        /// <see href="https://en.wikipedia.org/wiki/Numerical_Recipes">
        /// Numerical Recipes</see>.
        /// </remarks>
        private static void NoOperation(double _)
        {
        }

        /// <summary>
        /// Gets the differencing delta applied while
        /// approximating the first order derivative at the
        /// specified argument.
        /// </summary>
        /// <param name="x">
        /// The argument at which a function need to be differenced.
        /// </param>
        /// <returns>
        /// The differencing delta applied while approximating
        /// the first order derivative at the specified argument.
        /// </returns>
        private static double GetFirstOrderDelta(double x)
        {
            double scale = (x == 0.0) ? 1.0 : Math.Abs(x);

            double h;

            // The power to which the machine epsilon has been raised below
            // is compliant with Section 5.7 of Numerical Recipes.
            h = Math.Pow(machineEpsilon, 1.0 / 3.0) * scale;

            // As discussed in numerical recipes.
            double x_plus_h = x + h;
            NoOperation(x_plus_h);
            return x_plus_h - x;
        }

        /// <summary>
        /// Gets the differencing delta applied while
        /// approximating the second order derivative at the
        /// specified argument.
        /// </summary>
        /// <param name="x">
        /// The argument at which a function need to be differenced.
        /// </param>
        /// <returns>
        /// The differencing delta applied while approximating
        /// the second order derivative at the specified argument.
        /// </returns>
        private static double GetSecondOrderDelta(double x)
        {
            double scale = (x == 0.0) ? 1.0 : Math.Abs(x);

            double h;

            // The power to which the machine epsilon has been raised below
            // is compliant with Section 5.7 of Numerical Recipes.
            h = Math.Pow(machineEpsilon, .25) * scale;

            // As discussed in numerical recipes.
            double x_plus_h = x + h;
            NoOperation(x_plus_h);
            return x_plus_h - x;
        }

        #endregion

        #region Differentiation of nonparametric functions

        /// <summary>
        /// Returns the gradient of the specified nonparametric function 
        /// at the given argument.
        /// </summary>
        /// <param name="function">
        /// The function to be differentiated.
        /// </param>
        /// <param name="argument">
        /// The argument at which the gradient must be evaluated.
        /// </param>
        /// <returns>
        /// The gradient of the specified function evaluated 
        /// at the given argument.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="function"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="argument"/> is <b>null</b>.
        /// </exception>
        public static DoubleMatrix Gradient(
            Func<DoubleMatrix, double> function,
            DoubleMatrix argument)
        {
            ArgumentNullException.ThrowIfNull(function);

            ArgumentNullException.ThrowIfNull(argument);

            int numberOfArguments = argument.Count;
            DoubleMatrix gradient = DoubleMatrix.Dense(
                numberOfArguments, 1);
            double h, h_by_2, arg_i, partialDerivative;

            DoubleMatrix x = (DoubleMatrix)argument.Clone();

            // Using central differences for an accuracy of order 4
            // See also http://en.wikipedia.org/wiki/Finite_difference_coefficients
            //
            // Points:       -2    -1  0  1    2
            // Coefficients: 1/12 −2/3 0 2/3 −1/12 
            for (int i = 0; i < numberOfArguments; i++)
            {
                arg_i = argument[i];
                h = NumericalDifferentiation.GetFirstOrderDelta(arg_i);
                h_by_2 = h * 2.0;
                partialDerivative = 0.0;

                // argument - 2*h
                x[i] -= h_by_2;
                partialDerivative += function(x);
                x[i] = arg_i;

                // argument - h
                x[i] -= h;
                partialDerivative -= 8.0 * function(x);
                x[i] = arg_i;

                // argument + h
                x[i] += h;
                partialDerivative += 8.0 * function(x);
                x[i] = arg_i;

                // argument + 2*h
                x[i] += h_by_2;
                partialDerivative -= function(x);
                x[i] = arg_i;

                partialDerivative /= (12.0 * h);

                gradient[i] = partialDerivative;
            }
            return gradient;
        }

        /// <summary>
        /// Returns the Hessian matrix of the specified nonparametric function 
        /// at the given argument.
        /// </summary>
        /// <param name="function">
        /// The function to be differentiated.
        /// </param>
        /// <param name="argument">
        /// The argument at which the Hessian must be evaluated.
        /// </param>
        /// <returns>
        /// The Hessian matrix of the specified function evaluated 
        /// at the given argument.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="function"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="argument"/> is <b>null</b>.
        /// </exception>
        public static DoubleMatrix Hessian(Func<DoubleMatrix, double> function,
            DoubleMatrix argument)
        {
            ArgumentNullException.ThrowIfNull(function);

            ArgumentNullException.ThrowIfNull(argument);

            int numberOfArguments = argument.Count;
            DoubleMatrix hessian = DoubleMatrix.Dense(
                numberOfArguments, numberOfArguments);
            double h, arg_i;

            DoubleMatrix x = (DoubleMatrix)argument.Clone();

            double secondPartialDerivative;
            double h2;
            for (int i = 0; i < numberOfArguments; i++)
            {
                arg_i = argument[i];
                h = NumericalDifferentiation.GetSecondOrderDelta(arg_i);
                //                h_by_2 = h * 2.0;
                h2 = h * h;
                secondPartialDerivative = 0.0;

                // argument - h
                x[i] -= h;
                secondPartialDerivative += function(x);
                x[i] = arg_i;

                // argument
                secondPartialDerivative -= 2.0 * function(x);

                // argument + h
                x[i] += h;
                secondPartialDerivative += function(x);
                x[i] = arg_i;

                secondPartialDerivative /= h2;

                hessian[i, i] = secondPartialDerivative;
            }

            // Mixed partial derivatives
            // Points:        -1,-1  -1,1  0  1,-1   1,1
            // Coefficients:   1/4   -1/4  0  -1/4   1/4 
            double mixedPartialDerivative, k, arg_j, denominator;

            for (int i = 0; i < numberOfArguments; i++)
            {
                arg_i = argument[i];
                h = NumericalDifferentiation.GetSecondOrderDelta(arg_i);

                for (int j = i + 1; j < numberOfArguments; j++)
                {
                    arg_j = argument[j];
                    k = NumericalDifferentiation.GetSecondOrderDelta(arg_j);
                    denominator = h * k;
                    mixedPartialDerivative = 0.0;

                    // -1, -1
                    x[i] -= h;
                    x[j] -= k;
                    mixedPartialDerivative += .25 * function(x);
                    x[i] = arg_i;
                    x[j] = arg_j;
                    // Points:        -1,-1  -1,1  0  1,-1   1,1
                    // Coefficients:   1/4   -1/4  0  -1/4   1/4 

                    // -1, 1
                    x[i] -= h;
                    x[j] += k;
                    mixedPartialDerivative -= .25 * function(x);
                    x[i] = arg_i;
                    x[j] = arg_j;

                    // 1,-1
                    x[i] += h;
                    x[j] -= k;
                    mixedPartialDerivative -= .25 * function(x);
                    x[i] = arg_i;
                    x[j] = arg_j;

                    // 1, 1
                    x[i] += h;
                    x[j] += k;
                    mixedPartialDerivative += .25 * function(x);
                    x[i] = arg_i;
                    x[j] = arg_j;

                    mixedPartialDerivative /= denominator;
                    hessian[i, j] = mixedPartialDerivative;
                    hessian[j, i] = mixedPartialDerivative;
                }
            }

            return hessian;
        }

        #endregion

        #region Differentiation of parametric functions

        /// <summary>
        /// Returns the gradient of the specified parametric function 
        /// at the given argument.
        /// </summary>
        /// <param name="function">
        /// The function to be differentiated.
        /// </param>
        /// <param name="argument">
        /// The argument at which the gradient must be evaluated.
        /// </param>
        /// <param name="parameter">
        /// The function parameter.
        /// </param>
        /// <typeparam name="TFunctionParameter">
        /// The type of the function parameter.
        /// </typeparam>
        /// <returns>
        /// The gradient of the specified function evaluated 
        /// at the given argument and parameter.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="function"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="argument"/> is <b>null</b>.
        /// </exception>
        public static DoubleMatrix Gradient<TFunctionParameter>(
            Func<DoubleMatrix, TFunctionParameter, double> function,
            DoubleMatrix argument,
            TFunctionParameter parameter)
        {
            ArgumentNullException.ThrowIfNull(function);

            ArgumentNullException.ThrowIfNull(argument);

            int numberOfArguments = argument.Count;
            DoubleMatrix gradient = DoubleMatrix.Dense(
                numberOfArguments, 1);
            double h, h_by_2, arg_i, partialDerivative;

            DoubleMatrix x = (DoubleMatrix)argument.Clone();

            // Using central differences for an accuracy of order 4
            // See also http://en.wikipedia.org/wiki/Finite_difference_coefficients
            //
            // Points:       -2    -1  0  1    2
            // Coefficients: 1/12 −2/3 0 2/3 −1/12 
            for (int i = 0; i < numberOfArguments; i++)
            {
                arg_i = argument[i];
                h = NumericalDifferentiation.GetFirstOrderDelta(arg_i);
                h_by_2 = h * 2.0;
                partialDerivative = 0.0;

                // argument - 2*h
                x[i] -= h_by_2;
                partialDerivative += function(x, parameter);
                x[i] = arg_i;

                // argument - h
                x[i] -= h;
                partialDerivative -= 8.0 * function(x, parameter);
                x[i] = arg_i;

                // argument + h
                x[i] += h;
                partialDerivative += 8.0 * function(x, parameter);
                x[i] = arg_i;

                // argument + 2*h
                x[i] += h_by_2;
                partialDerivative -= function(x, parameter);
                x[i] = arg_i;

                partialDerivative /= (12.0 * h);

                gradient[i] = partialDerivative;
            }
            return gradient;
        }

        /// <summary>
        /// Returns the Hessian matrix of the specified parametric function 
        /// at the given argument.
        /// </summary>
        /// <param name="function">
        /// The function to be differentiated.
        /// </param>
        /// <param name="argument">
        /// The argument at which the Hessian must be evaluated.
        /// </param>
        /// <param name="parameter">
        /// The function parameter.
        /// </param>
        /// <typeparam name="TFunctionParameter">
        /// The type of the function parameter.
        /// </typeparam>
        /// <returns>
        /// The Hessian matrix of the specified function evaluated 
        /// at the given argument.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="function"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="argument"/> is <b>null</b>.
        /// </exception>
        public static DoubleMatrix Hessian<TFunctionParameter>(
            Func<DoubleMatrix, TFunctionParameter, double> function,
            DoubleMatrix argument, 
            TFunctionParameter parameter)
        {
            ArgumentNullException.ThrowIfNull(function);

            ArgumentNullException.ThrowIfNull(argument);

            int numberOfArguments = argument.Count;
            DoubleMatrix hessian = DoubleMatrix.Dense(
                numberOfArguments, numberOfArguments);
            double h, arg_i;

            DoubleMatrix x = (DoubleMatrix)argument.Clone();

            double secondPartialDerivative;
            double h2;
            for (int i = 0; i < numberOfArguments; i++)
            {
                arg_i = argument[i];
                h = NumericalDifferentiation.GetSecondOrderDelta(arg_i);
                h2 = h * h;
                secondPartialDerivative = 0.0;

                // argument - h
                x[i] -= h;
                secondPartialDerivative += function(x, parameter);
                x[i] = arg_i;

                // argument
                secondPartialDerivative -= 2.0 * function(x, parameter);

                // argument + h
                x[i] += h;
                secondPartialDerivative += function(x, parameter);
                x[i] = arg_i;

                secondPartialDerivative /= h2;

                hessian[i, i] = secondPartialDerivative;
            }

            // Mixed partial derivatives
            // Points:        -1,-1  -1,1  0  1,-1   1,1
            // Coefficients:   1/4   -1/4  0  -1/4   1/4 
            double mixedPartialDerivative, k, arg_j, denominator;

            for (int i = 0; i < numberOfArguments; i++)
            {
                arg_i = argument[i];
                h = NumericalDifferentiation.GetSecondOrderDelta(arg_i);

                for (int j = i + 1; j < numberOfArguments; j++)
                {
                    arg_j = argument[j];
                    k = NumericalDifferentiation.GetSecondOrderDelta(arg_j);
                    denominator = h * k;
                    mixedPartialDerivative = 0.0;

                    // -1, -1
                    x[i] -= h;
                    x[j] -= k;
                    mixedPartialDerivative += .25 * function(x, parameter);
                    x[i] = arg_i;
                    x[j] = arg_j;
                    // Points:        -1,-1  -1,1  0  1,-1   1,1
                    // Coefficients:   1/4   -1/4  0  -1/4   1/4 

                    // -1, 1
                    x[i] -= h;
                    x[j] += k;
                    mixedPartialDerivative -= .25 * function(x, parameter);
                    x[i] = arg_i;
                    x[j] = arg_j;

                    // 1,-1
                    x[i] += h;
                    x[j] -= k;
                    mixedPartialDerivative -= .25 * function(x, parameter);
                    x[i] = arg_i;
                    x[j] = arg_j;

                    // 1, 1
                    x[i] += h;
                    x[j] += k;
                    mixedPartialDerivative += .25 * function(x, parameter);
                    x[i] = arg_i;
                    x[j] = arg_j;

                    mixedPartialDerivative /= denominator;
                    hessian[i, j] = mixedPartialDerivative;
                    hessian[j, i] = mixedPartialDerivative;
                }
            }

            return hessian;
        }

        #endregion
    }
}
