// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Kurtosis
{
    /// <summary>
    /// Represents a testable kurtosis which summarizes
    /// all items in a matrix.
    /// </summary>
    class OnAllKurtosis<TExpected> :
        OnAllAdjustableForBiasSummaryOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="KurtosisOnAll" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="data">The data operand.</param>
        /// <param name="adjustForBias">A value to signal if the operation is 
        /// adjusted for bias.</param>
        protected OnAllKurtosis(
                    TExpected expected,
                    TestableDoubleMatrix data,
                    bool adjustForBias) :
            base(
                expected: expected,
                data: data,
                adjustForBias: adjustForBias,
                dataWritableOps:
                    new Func<DoubleMatrix, bool, double>[1] { Stat.Kurtosis },
                dataReadOnlyOps:
                   new Func<ReadOnlyDoubleMatrix, bool, double>[1] { Stat.Kurtosis })
        {
        }
    }
}