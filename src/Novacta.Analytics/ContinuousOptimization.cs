// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Advanced;

namespace Novacta.Analytics
{
    /// <summary>
    /// Provides methods for minimizing or maximizing a function.
    /// </summary>
    /// <remarks>
    /// <para><b>Scope</b></para>
    /// <para>
    /// This class exposes methods aimed to solve continuous 
    /// optimization problems.
    /// It can be exploited to treat, among the others, 
    /// non-convex functions or functions characterized by 
    /// many local extrema.
    /// </para>
    /// <para><b>Objective functions</b></para>
    /// <para>
    /// Objective functions are passed to optimization methods 
    /// as delegates.
    /// All optimization methods are overloaded to accept two 
    /// different types of delegates. The first type is  
    /// <see cref="Func{DoubleMatrix, Double}"/>, i.e. it must return a 
    /// <see cref="double"/>, while its argument is represented by a 
    /// <see cref="DoubleMatrix"/> object: such delegate can be used if the 
    /// objective function needs no information other than its 
    /// argument to be evaluated.
    /// If, on the contrary, additional information is needed, the function 
    /// can be treated as a parametric one, and the delegate will have the 
    /// generic type 
    /// <see cref="Func{DoubleMatrix, TFunctionParameter, Double}"/>, where 
    /// <i>TFunctionParameter</i> is the type 
    /// which will represent the type through which the additional 
    /// information is passed to the objective function. 
    /// </para>
    /// <para>
    /// It is expected that the objective function to be optimized
    /// will accept a row 
    /// vector as a valid representation of an argument.
    /// </para>
    /// <para><b>Optimizing a function</b></para>
    /// <para>
    /// The <see cref="ContinuousOptimization"/> class supplies methods 
    /// for unconstrained optimization, such as 
    /// <see cref="Minimize"/> or
    /// <see cref="Maximize"/>. 
    /// Such methods assume that the feasible region in which the search 
    /// for optimization must be directed
    /// is the Cartesian product of as many copies of the real line as is the 
    /// dimension of an argument in the objective function.
    /// </para>
    /// <para><b>Advanced scenarios</b></para>
    /// <para>
    /// Internally, every continuous optimization problem is solved 
    /// via a default 
    /// Cross-Entropy context of type
    /// <see cref="ContinuousOptimizationContext"/>.
    /// For advanced scenarios, in which additional control on the 
    /// parameters of the underlying algorithm is needed, a specialized 
    /// context can be instantiated and hence exploited executing
    /// method <see cref="SystemPerformanceOptimizer.Optimize(
    /// SystemPerformanceOptimizationContext, double, int)">Optimize</see>
    /// on a <see cref="SystemPerformanceOptimizer"/> 
    /// object. See the documentation 
    /// about <see cref="ContinuousOptimizationContext"/>
    /// for additional examples.
    /// </para>
    /// </remarks>
    /// <seealso cref="ContinuousOptimizationContext"/>
    /// <seealso cref="SystemPerformanceOptimizer"/>
    public static class ContinuousOptimization
    {
        #region Minimize

        /// <summary>
        /// Finds the minimum of the specified  
        /// objective function.
        /// </summary>
        /// <param name="objectiveFunction">
        /// The objective function to be minimized.
        /// </param>
        /// <param name="initialArgument">
        /// The argument at which the method starts the search 
        /// for optimality.
        /// </param>
        /// <returns>
        /// The argument at which the function is minimized.
        /// </returns>
        /// <remarks>
        /// <para>
        /// It is assumed that the <paramref name="objectiveFunction"/> 
        /// will accept row 
        /// vectors as valid representations of an argument.
        /// As a consequence, <paramref name="initialArgument"/> is 
        /// expected to be a row vector.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, function
        /// <latex mode='display'>
        /// f\round{x}=\exp\round{-\round{x-2}^2} 
        /// + \round{.8} \exp\round{-\round{x+2}^2} 
        /// </latex>
        /// is minimized in the sub-domain 
        /// <latex>[-3, 3]</latex>.
        /// To obtain such result, arguments outside
        /// that interval are penalized accordingly.
        /// </para>
        /// <para>
        /// The search for the minimizer starts from
        /// the initial argument <latex>-3</latex>.
        /// </para>
        /// <para>
        /// <code title="Minimizing a function in a sub-domain."
        /// source="..\Novacta.Analytics.CodeExamples\ContinuousOptimizationExample0.cs.txt" 
        /// language="cs" />
        /// </para> 
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="objectiveFunction"/> is 
        /// <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="initialArgument"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="initialArgument"/> is not a row 
        /// vector.
        /// </exception>
        public static DoubleMatrix Minimize(
            Func<DoubleMatrix, double> objectiveFunction,
            DoubleMatrix initialArgument)
        {
            #region Input validation

            if (objectiveFunction is null)
            {
                throw new ArgumentNullException(nameof(objectiveFunction));
            }

            if (initialArgument is null)
            {
                throw new ArgumentNullException(nameof(initialArgument));
            }

            if (!initialArgument.IsRowVector)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR"),
                    nameof(initialArgument));
            }

            #endregion

            int numberOfArguments = initialArgument.Count;

            var initialParameter = DoubleMatrix.Dense(
                2,
                numberOfArguments);

            initialParameter[0, ":"] = initialArgument;
            initialParameter[1, ":"] += 1.0e2;

            var optimizer =
                new SystemPerformanceOptimizer();

            var context = new ContinuousOptimizationContext(
                objectiveFunction: objectiveFunction,
                initialArgument: initialArgument,
                meanSmoothingCoefficient: .8,
                standardDeviationSmoothingCoefficient: .7,
                standardDeviationSmoothingExponent: 6,
                initialStandardDeviation: 100.0,
                optimizationGoal: OptimizationGoal.Minimization,
                terminationTolerance: 1.0e-3,
                minimumNumberOfIterations: 3,
                maximumNumberOfIterations: 1000);

            int sampleSize = 100 * numberOfArguments;

            double rarity = .01;

            var results = optimizer.Optimize(
                context,
                rarity,
                sampleSize);

            return results.OptimalState;
        }

        /// <summary>
        /// Finds the minimum of the specified 
        /// parametric objective function.
        /// </summary>
        /// <param name="objectiveFunction">
        /// The objective function to be minimized.
        /// </param>
        /// <param name="initialArgument">
        /// The argument at which the method starts the search 
        /// for optimality.
        /// </param>
        /// <param name="functionParameter">
        /// The function parameter.
        /// </param>
        /// <typeparam name="TFunctionParameter">
        /// The type of the function parameter.
        /// </typeparam>
        /// <returns>
        /// The argument at which the function is minimized.
        /// </returns>
        /// <remarks>
        /// <para>
        /// It is assumed that the <paramref name="objectiveFunction"/> 
        /// will accept row 
        /// vectors as valid representations of an argument.
        /// As a consequence, <paramref name="initialArgument"/> is 
        /// expected to be a row vector.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="objectiveFunction"/> is 
        /// <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="initialArgument"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="initialArgument"/> is not a row 
        /// vector.
        /// </exception>
        public static DoubleMatrix Minimize<TFunctionParameter>(
            Func<DoubleMatrix, TFunctionParameter, double> objectiveFunction,
            DoubleMatrix initialArgument,
            TFunctionParameter functionParameter)
        {
            #region Input validation

            if (objectiveFunction is null)
            {
                throw new ArgumentNullException(nameof(objectiveFunction));
            }

            if (initialArgument is null)
            {
                throw new ArgumentNullException(nameof(initialArgument));
            }

            if (!initialArgument.IsRowVector)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR"),
                    nameof(initialArgument));
            }

            #endregion

            double func(DoubleMatrix x) => objectiveFunction(x, functionParameter);

            int numberOfArguments = initialArgument.Count;

            var initialParameter = DoubleMatrix.Dense(
                2,
                numberOfArguments);

            initialParameter[0, ":"] = initialArgument;
            initialParameter[1, ":"] += 1.0e2;

            var optimizer =
                new SystemPerformanceOptimizer();

            var context = new ContinuousOptimizationContext(
                objectiveFunction: func,
                initialArgument: initialArgument,
                meanSmoothingCoefficient: .8,
                standardDeviationSmoothingCoefficient: .7,
                standardDeviationSmoothingExponent: 6,
                initialStandardDeviation: 100.0,
                optimizationGoal: OptimizationGoal.Minimization,
                terminationTolerance: 1.0e-3,
                minimumNumberOfIterations: 3,
                maximumNumberOfIterations: 1000);

            int sampleSize = 100 * numberOfArguments;

            double rarity = .01;

            var results = optimizer.Optimize(
                context,
                rarity,
                sampleSize);

            return results.OptimalState;
        }

        #endregion

        #region Maximize

        /// <summary>
        /// Finds the maximum of the specified 
        /// objective function.
        /// </summary>
        /// <param name="objectiveFunction">
        /// The objective function to be maximized.
        /// </param>
        /// <param name="initialArgument">
        /// The argument at which the method starts the search 
        /// for optimality.
        /// </param>
        /// <returns>
        /// The argument at which the function is maximized.
        /// </returns>
        /// <remarks>
        /// <para>
        /// It is assumed that the <paramref name="objectiveFunction"/> 
        /// will accept row 
        /// vectors as valid representations of an argument.
        /// As a consequence, <paramref name="initialArgument"/> is 
        /// expected to be a row vector.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="objectiveFunction"/> is 
        /// <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="initialArgument"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="initialArgument"/> is not a row 
        /// vector.
        /// </exception>
        public static DoubleMatrix Maximize(
            Func<DoubleMatrix, double> objectiveFunction,
            DoubleMatrix initialArgument)
        {
            #region Input validation

            if (objectiveFunction is null)
            {
                throw new ArgumentNullException(nameof(objectiveFunction));
            }

            if (initialArgument is null)
            {
                throw new ArgumentNullException(nameof(initialArgument));
            }

            if (!initialArgument.IsRowVector)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR"),
                    nameof(initialArgument));
            }

            #endregion

            int numberOfArguments = initialArgument.Count;

            var initialParameter = DoubleMatrix.Dense(
                2,
                numberOfArguments);

            initialParameter[0, ":"] = initialArgument;
            initialParameter[1, ":"] += 1.0e2;

            var optimizer =
                new SystemPerformanceOptimizer();

            var context = new ContinuousOptimizationContext(
                objectiveFunction: objectiveFunction,
                initialArgument: initialArgument,
                meanSmoothingCoefficient: .8,
                standardDeviationSmoothingCoefficient: .7,
                standardDeviationSmoothingExponent: 6,
                initialStandardDeviation: 100.0,
                optimizationGoal: OptimizationGoal.Maximization,
                terminationTolerance: 1.0e-3,
                minimumNumberOfIterations: 3,
                maximumNumberOfIterations: 1000);

            int sampleSize = 100 * numberOfArguments;

            double rarity = .01;

            var results = optimizer.Optimize(
                context,
                rarity,
                sampleSize);

            return results.OptimalState;
        }

        /// <summary>
        /// Finds the maximum of the specified 
        /// parametric objective function.
        /// </summary>
        /// <param name="objectiveFunction">
        /// The objective function to be maximized.
        /// </param>
        /// <param name="initialArgument">
        /// The argument at which the method starts the search 
        /// for optimality.
        /// </param>
        /// <param name="functionParameter">
        /// The function parameter.
        /// </param>
        /// <typeparam name="TFunctionParameter">
        /// The type of the function parameter.
        /// </typeparam>
        /// <returns>
        /// The argument at which the function is maximized.
        /// </returns>
        /// <remarks>
        /// <para>
        /// It is assumed that the <paramref name="objectiveFunction"/> 
        /// will accept row 
        /// vectors as valid representations of an argument.
        /// As a consequence, <paramref name="initialArgument"/> is 
        /// expected to be a row vector.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, function
        /// <latex mode='display'>
        /// f\round{x,\alpha}=\exp\round{-\round{x-2}^2} 
        /// + \round{\alpha} \exp\round{-\round{x+2}^2} 
        /// </latex>
        /// is maximized.
        /// </para>
        /// <para>
        /// The search for the maximizer starts from
        /// the initial argument <latex>-6</latex>.
        /// </para>
        /// <para>
        /// <code title="Maximizing a parametric function."
        /// source="..\Novacta.Analytics.CodeExamples\ContinuousOptimizationExample1.cs.txt" 
        /// language="cs" />
        /// </para> 
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="objectiveFunction"/> is 
        /// <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="initialArgument"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="initialArgument"/> is not a row 
        /// vector.
        /// </exception>
        public static DoubleMatrix Maximize<TFunctionParameter>(
            Func<DoubleMatrix, TFunctionParameter, double> objectiveFunction,
            DoubleMatrix initialArgument,
            TFunctionParameter functionParameter)
        {
            #region Input validation

            if (objectiveFunction is null)
            {
                throw new ArgumentNullException(nameof(objectiveFunction));
            }

            if (initialArgument is null)
            {
                throw new ArgumentNullException(nameof(initialArgument));
            }

            if (!initialArgument.IsRowVector)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR"),
                    nameof(initialArgument));
            }

            #endregion

            double func(DoubleMatrix x) => objectiveFunction(x, functionParameter);

            int numberOfArguments = initialArgument.Count;

            var initialParameter = DoubleMatrix.Dense(
                2,
                numberOfArguments);

            initialParameter[0, ":"] = initialArgument;
            initialParameter[1, ":"] += 1.0e2;

            var optimizer =
                new SystemPerformanceOptimizer();

            var context = new ContinuousOptimizationContext(
                objectiveFunction: func,
                initialArgument: initialArgument,
                meanSmoothingCoefficient: .8,
                standardDeviationSmoothingCoefficient: .7,
                standardDeviationSmoothingExponent: 6,
                initialStandardDeviation: 100.0,
                optimizationGoal: OptimizationGoal.Maximization,
                terminationTolerance: 1.0e-3,
                minimumNumberOfIterations: 3,
                maximumNumberOfIterations: 1000);

            int sampleSize = 100 * numberOfArguments;

            double rarity = .01;

            var results = optimizer.Optimize(
                context,
                rarity,
                sampleSize);

            return results.OptimalState;
        }

        #endregion
    }
}