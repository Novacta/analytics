﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.SumOfSquaredDeviations
{
    /// <summary>
    /// Represents a testable sum of squared deviations which summarizes
    /// all items along a specific dimension of a matrix.
    /// </summary>
    class AlongDimensionSumOfSquaredDeviations<TExpected> :
    AlongDimensionNotAdjustableForBiasSummaryOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="AlongDimensionSumOfSquaredDeviations" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        protected AlongDimensionSumOfSquaredDeviations(
                    TExpected expected,
                    TestableDoubleMatrix data,
                    DataOperation dataOperation) :
            base(
                expected: expected,
                data: data,
                dataOperation: dataOperation,
                dataWritableOps:
                    new Func<DoubleMatrix, DataOperation, DoubleMatrix>[1] { Stat.SumOfSquaredDeviations },
                dataReadOnlyOps:
                    new Func<ReadOnlyDoubleMatrix, DataOperation, DoubleMatrix>[1] { Stat.SumOfSquaredDeviations })
        {
        }
    }
}