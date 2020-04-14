// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.CrossEntropy;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Advanced.Tests
{
    [TestClass()]
    public class ContinuousOptimizationContextTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            // optimizationGoal is not a field of OptimizationGoal
            {
                var STR_EXCEPT_NOT_FIELD_OF_OPTIMIZATION_GOAL =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_NOT_FIELD_OF_OPTIMIZATION_GOAL");

                string parameterName = "optimizationGoal";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(1, 3),
                            meanSmoothingCoefficient: .8,
                            standardDeviationSmoothingCoefficient: .7,
                            standardDeviationSmoothingExponent: 6,
                            initialStandardDeviation: 100.0,
                            optimizationGoal: (OptimizationGoal)(-1),
                            terminationTolerance: 1.0e-3,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage:
                        STR_EXCEPT_NOT_FIELD_OF_OPTIMIZATION_GOAL,
                    expectedParameterName: parameterName);
            }

            // objectiveFunction is null
            {
                string parameterName = "objectiveFunction";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: null,
                            initialArgument: DoubleMatrix.Dense(1, 3),
                            meanSmoothingCoefficient: .8,
                            standardDeviationSmoothingCoefficient: .7,
                            standardDeviationSmoothingExponent: 6,
                            initialStandardDeviation: 100.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            terminationTolerance: 1.0e-3,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // initialArgument is null
            {
                string parameterName = "initialArgument";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: null,
                            meanSmoothingCoefficient: .8,
                            standardDeviationSmoothingCoefficient: .7,
                            standardDeviationSmoothingExponent: 6,
                            initialStandardDeviation: 100.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            terminationTolerance: 1.0e-3,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // initialArgument is not a row vector
            {
                var STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR");

                string parameterName = "initialArgument";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(3, 1),
                            meanSmoothingCoefficient: .8,
                            standardDeviationSmoothingCoefficient: .7,
                            standardDeviationSmoothingExponent: 6,
                            initialStandardDeviation: 100.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            terminationTolerance: 1.0e-3,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR,
                    expectedParameterName: parameterName);
            }

            // initialStandardDeviation is zero
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "initialStandardDeviation";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(1, 3),
                            meanSmoothingCoefficient: .8,
                            standardDeviationSmoothingCoefficient: .7,
                            standardDeviationSmoothingExponent: 6,
                            initialStandardDeviation: 0.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            terminationTolerance: 1.0e-3,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // initialStandardDeviation is negative
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "initialStandardDeviation";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(1, 3),
                            meanSmoothingCoefficient: .8,
                            standardDeviationSmoothingCoefficient: .7,
                            standardDeviationSmoothingExponent: 6,
                            initialStandardDeviation: -100.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            terminationTolerance: 1.0e-3,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // minimumNumberOfIterations is zero
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "minimumNumberOfIterations";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(1, 3),
                            meanSmoothingCoefficient: .8,
                            standardDeviationSmoothingCoefficient: .7,
                            standardDeviationSmoothingExponent: 6,
                            initialStandardDeviation: 100.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            terminationTolerance: 1.0e-3,
                            minimumNumberOfIterations: 0,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // minimumNumberOfIterations is negative
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "minimumNumberOfIterations";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(1, 3),
                            meanSmoothingCoefficient: .8,
                            standardDeviationSmoothingCoefficient: .7,
                            standardDeviationSmoothingExponent: 6,
                            initialStandardDeviation: 100.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            terminationTolerance: 1.0e-3,
                            minimumNumberOfIterations: -1,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // maximumNumberOfIterations is zero
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "maximumNumberOfIterations";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(1, 3),
                            meanSmoothingCoefficient: .8,
                            standardDeviationSmoothingCoefficient: .7,
                            standardDeviationSmoothingExponent: 6,
                            initialStandardDeviation: 100.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            terminationTolerance: 1.0e-3,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // maximumNumberOfIterations is negative
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "maximumNumberOfIterations";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(1, 3),
                            meanSmoothingCoefficient: .8,
                            standardDeviationSmoothingCoefficient: .7,
                            standardDeviationSmoothingExponent: 6,
                            initialStandardDeviation: 100.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            terminationTolerance: 1.0e-3,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: -1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // minimumNumberOfIterations is greater than maximumNumberOfIterations
            {
                var STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER"),
                        "maximumNumberOfIterations",
                        "minimumNumberOfIterations");

                string parameterName = "maximumNumberOfIterations";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(1, 3),
                            meanSmoothingCoefficient: .8,
                            standardDeviationSmoothingCoefficient: .7,
                            standardDeviationSmoothingExponent: 6,
                            initialStandardDeviation: 100.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            terminationTolerance: 1.0e-3,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 2);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_OTHER,
                    expectedParameterName: parameterName);
            }

            // meanSmoothingCoefficient is zero
            {
                var STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL"),
                            "0.0", "1.0");

                string parameterName = "meanSmoothingCoefficient";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(1, 3),
                            meanSmoothingCoefficient: 0.0,
                            standardDeviationSmoothingCoefficient: 0.7,
                            standardDeviationSmoothingExponent: 6,
                            initialStandardDeviation: 100.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            terminationTolerance: 1.0e-3,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL,
                    expectedParameterName: parameterName);
            }

            // meanSmoothingCoefficient is negative
            {
                var STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL"),
                            "0.0", "1.0");

                string parameterName = "meanSmoothingCoefficient";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(1, 3),
                            meanSmoothingCoefficient: -.8,
                            standardDeviationSmoothingCoefficient: .7,
                            standardDeviationSmoothingExponent: 6,
                            initialStandardDeviation: 100.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            terminationTolerance: 1.0e-3,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL,
                    expectedParameterName: parameterName);
            }

            // meanSmoothingCoefficient is one
            {
                var STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL"),
                            "0.0", "1.0");

                string parameterName = "meanSmoothingCoefficient";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(1, 3),
                            meanSmoothingCoefficient: 1.0,
                            standardDeviationSmoothingCoefficient: 0.7,
                            standardDeviationSmoothingExponent: 6,
                            initialStandardDeviation: 100.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            terminationTolerance: 1.0e-3,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL,
                    expectedParameterName: parameterName);
            }

            // meanSmoothingCoefficient is greater than one
            {
                var STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL"),
                            "0.0", "1.0");

                string parameterName = "meanSmoothingCoefficient";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(1, 3),
                            meanSmoothingCoefficient: 1.1,
                            standardDeviationSmoothingCoefficient: .7,
                            standardDeviationSmoothingExponent: 6,
                            initialStandardDeviation: 100.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            terminationTolerance: 1.0e-3,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL,
                    expectedParameterName: parameterName);
            }

            // terminationTolerance is zero
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "terminationTolerance";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(1, 3),
                            meanSmoothingCoefficient: .8,
                            standardDeviationSmoothingCoefficient: .7,
                            standardDeviationSmoothingExponent: 6,
                            initialStandardDeviation: 100.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            terminationTolerance: 0.0,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // terminationTolerance is negative
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "terminationTolerance";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(1, 3),
                            meanSmoothingCoefficient: .8,
                            standardDeviationSmoothingCoefficient: .7,
                            standardDeviationSmoothingExponent: 6,
                            initialStandardDeviation: 100.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            terminationTolerance: -1.0e-3,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // standardDeviationSmoothingCoefficient is zero
            {
                var STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL"),
                            "0.0", "1.0");

                string parameterName = "standardDeviationSmoothingCoefficient";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(1, 3),
                            meanSmoothingCoefficient: .8,
                            standardDeviationSmoothingCoefficient: 0.0,
                            standardDeviationSmoothingExponent: 6,
                            initialStandardDeviation: 100.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            terminationTolerance: 1.0e-3,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL,
                    expectedParameterName: parameterName);
            }

            // standardDeviationSmoothingCoefficient is negative
            {
                var STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL"),
                            "0.0", "1.0");

                string parameterName = "standardDeviationSmoothingCoefficient";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(1, 3),
                            meanSmoothingCoefficient: .8,
                            standardDeviationSmoothingCoefficient: -.7,
                            standardDeviationSmoothingExponent: 6,
                            initialStandardDeviation: 100.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            terminationTolerance: 1.0e-3,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL,
                    expectedParameterName: parameterName);
            }

            // standardDeviationSmoothingCoefficient is one
            {
                var STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL"),
                            "0.0", "1.0");

                string parameterName = "standardDeviationSmoothingCoefficient";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(1, 3),
                            meanSmoothingCoefficient: .8,
                            standardDeviationSmoothingCoefficient: 1.0,
                            standardDeviationSmoothingExponent: 6,
                            initialStandardDeviation: 100.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            terminationTolerance: 1.0e-3,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL,
                    expectedParameterName: parameterName);
            }

            // standardDeviationSmoothingCoefficient is greater than one
            {
                var STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL =
                    string.Format(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL"),
                            "0.0", "1.0");

                string parameterName = "standardDeviationSmoothingCoefficient";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(1, 3),
                            meanSmoothingCoefficient: .8,
                            standardDeviationSmoothingCoefficient: 1.1,
                            standardDeviationSmoothingExponent: 6,
                            initialStandardDeviation: 100.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            terminationTolerance: 1.0e-3,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_NOT_IN_OPEN_INTERVAL,
                    expectedParameterName: parameterName);
            }

            // standardDeviationSmoothingExponent is zero
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "standardDeviationSmoothingExponent";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(1, 3),
                            meanSmoothingCoefficient: .8,
                            standardDeviationSmoothingCoefficient: .7,
                            standardDeviationSmoothingExponent: 0,
                            initialStandardDeviation: 100.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            terminationTolerance: 1.0e-3,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // standardDeviationSmoothingExponent is negative
            {
                var STR_EXCEPT_PAR_MUST_BE_POSITIVE =
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_POSITIVE");

                string parameterName = "standardDeviationSmoothingExponent";

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        new ContinuousOptimizationContext(
                            objectiveFunction: (x) => Stat.Mean(x),
                            initialArgument: DoubleMatrix.Dense(1, 3),
                            meanSmoothingCoefficient: .8,
                            standardDeviationSmoothingCoefficient: .7,
                            standardDeviationSmoothingExponent: -1,
                            initialStandardDeviation: 100.0,
                            optimizationGoal: OptimizationGoal.Minimization,
                            terminationTolerance: 1.0e-3,
                            minimumNumberOfIterations: 3,
                            maximumNumberOfIterations: 1000);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        STR_EXCEPT_PAR_MUST_BE_POSITIVE,
                    expectedParameterName: parameterName);
            }

            // valid input - LowerThanLevel
            {
                var testableContext =
                    TestableContinuousOptimizationContext00.Get();

                var context = testableContext.Context;

                Assert.AreEqual(
                    expected: testableContext.StateDimension,
                    actual: context.StateDimension);

                Assert.AreEqual(
                    expected: testableContext.TraceExecution,
                    actual: context.TraceExecution);

                Assert.AreEqual(
                    expected: testableContext.EliteSampleDefinition,
                    actual: context.EliteSampleDefinition);

                Assert.AreEqual(
                    expected: testableContext.OptimizationGoal,
                    actual: context.OptimizationGoal);

                DoubleMatrixAssert.AreEqual(
                    expected: testableContext.InitialParameter,
                    actual: context.InitialParameter,
                    DoubleMatrixTest.Accuracy);

                Assert.AreEqual(
                    expected: testableContext.MinimumNumberOfIterations,
                    actual: context.MinimumNumberOfIterations);

                Assert.AreEqual(
                    expected: testableContext.MaximumNumberOfIterations,
                    actual: context.MaximumNumberOfIterations);

                DoubleMatrixAssert.AreEqual(
                    expected: testableContext.InitialArgument,
                    actual: context.InitialArgument,
                    DoubleMatrixTest.Accuracy);

                Assert.AreEqual(
                    expected: testableContext.InitialStandardDeviation,
                    actual: context.InitialStandardDeviation,
                    delta: DoubleMatrixTest.Accuracy);

                Assert.AreEqual(
                    expected: testableContext.MeanSmoothingCoefficient,
                    actual: context.MeanSmoothingCoefficient,
                    delta: DoubleMatrixTest.Accuracy);

                Assert.AreEqual(
                    expected: testableContext.StandardDeviationSmoothingCoefficient,
                    actual: context.StandardDeviationSmoothingCoefficient,
                    delta: DoubleMatrixTest.Accuracy);

                Assert.AreEqual(
                    expected: testableContext.StandardDeviationSmoothingExponent,
                    actual: context.StandardDeviationSmoothingExponent);

                Assert.AreEqual(
                    expected: testableContext.TerminationTolerance,
                    actual: context.TerminationTolerance,
                    delta: DoubleMatrixTest.Accuracy);
            }

            // valid input - HigherThanLevel
            {
                var testableContext = TestableContinuousOptimizationContext01.Get();

                var context = testableContext.Context;

                Assert.AreEqual(
                    expected: testableContext.StateDimension,
                    actual: context.StateDimension);

                Assert.AreEqual(
                    expected: testableContext.TraceExecution,
                    actual: context.TraceExecution);

                Assert.AreEqual(
                    expected: testableContext.EliteSampleDefinition,
                    actual: context.EliteSampleDefinition);

                Assert.AreEqual(
                    expected: testableContext.OptimizationGoal,
                    actual: context.OptimizationGoal);

                DoubleMatrixAssert.AreEqual(
                    expected: testableContext.InitialParameter,
                    actual: context.InitialParameter,
                    DoubleMatrixTest.Accuracy);

                Assert.AreEqual(
                    expected: testableContext.MinimumNumberOfIterations,
                    actual: context.MinimumNumberOfIterations);

                Assert.AreEqual(
                    expected: testableContext.MaximumNumberOfIterations,
                    actual: context.MaximumNumberOfIterations);

                DoubleMatrixAssert.AreEqual(
                    expected: testableContext.InitialArgument,
                    actual: context.InitialArgument,
                    DoubleMatrixTest.Accuracy);

                Assert.AreEqual(
                    expected: testableContext.InitialStandardDeviation,
                    actual: context.InitialStandardDeviation,
                    delta: DoubleMatrixTest.Accuracy);

                Assert.AreEqual(
                    expected: testableContext.MeanSmoothingCoefficient,
                    actual: context.MeanSmoothingCoefficient,
                    delta: DoubleMatrixTest.Accuracy);

                Assert.AreEqual(
                    expected: testableContext.StandardDeviationSmoothingCoefficient,
                    actual: context.StandardDeviationSmoothingCoefficient,
                    delta: DoubleMatrixTest.Accuracy);

                Assert.AreEqual(
                    expected: testableContext.StandardDeviationSmoothingExponent,
                    actual: context.StandardDeviationSmoothingExponent);

                Assert.AreEqual(
                    expected: testableContext.TerminationTolerance,
                    actual: context.TerminationTolerance,
                    delta: DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void RunTest()
        {
            // Valid input - Minimization
            {
                var optimizer = new SystemPerformanceOptimizer();

                // Create the context.
                var testableContext =
                    TestableContinuousOptimizationContext00.Get();

                var context = testableContext.Context;

                // Set optimization parameters.
                int sampleSize = 100;
                double rarity = 0.09;

                // Solve the problem.
                var results = optimizer.Optimize(
                    context,
                    rarity,
                    sampleSize);

                Assert.AreEqual(
                    expected: true,
                    actual: results.HasConverged);

                DoubleMatrixAssert.AreEqual(
                    expected: testableContext.OptimalState,
                    actual: results.OptimalState,
                    delta: .03);

                Assert.AreEqual(
                    expected: testableContext.OptimalPerformance,
                    actual: results.OptimalPerformance,
                    DoubleMatrixTest.Accuracy);
            }

            // Valid input - Maximization 
            {
                var optimizer = new SystemPerformanceOptimizer();

                // Create the context.
                var testableContext =
                    TestableContinuousOptimizationContext01.Get();

                var context = testableContext.Context;

                // Set optimization parameters.
                int sampleSize = 100;
                double rarity = 0.1;

                // Solve the problem.
                var results = optimizer.Optimize(
                    context,
                    rarity,
                    sampleSize);

                Assert.AreEqual(
                    expected: true,
                    actual: results.HasConverged);

                DoubleMatrixAssert.AreEqual(
                    expected: testableContext.OptimalState,
                    actual: results.OptimalState,
                    DoubleMatrixTest.Accuracy);

                Assert.AreEqual(
                    expected: testableContext.OptimalPerformance,
                    actual: results.OptimalPerformance,
                    DoubleMatrixTest.Accuracy);
            }

            // Valid input - Maximization - not converging
            {
                var optimizer = new SystemPerformanceOptimizer();

                // Create the context.
                var testableContext =
                    TestableContinuousOptimizationContext01.Get();

                var context = new ContinuousOptimizationContext(
                    objectiveFunction: testableContext.ObjectiveFunction,
                    initialArgument: testableContext.InitialArgument,
                    meanSmoothingCoefficient: testableContext.MeanSmoothingCoefficient,
                    standardDeviationSmoothingCoefficient: testableContext.StandardDeviationSmoothingCoefficient,
                    standardDeviationSmoothingExponent: testableContext.StandardDeviationSmoothingExponent,
                    initialStandardDeviation: testableContext.InitialStandardDeviation,
                    terminationTolerance: testableContext.TerminationTolerance,
                    optimizationGoal: testableContext.OptimizationGoal,
                    minimumNumberOfIterations: testableContext.MinimumNumberOfIterations,
                    maximumNumberOfIterations: testableContext.MinimumNumberOfIterations + 1);

                // Set optimization parameters.
                int sampleSize = 100;
                double rarity = 0.1;

                // Solve the problem.
                var results = optimizer.Optimize(
                    context,
                    rarity,
                    sampleSize);

                Assert.AreEqual(
                    expected: false,
                    actual: results.HasConverged);
            }
        }
    }
}