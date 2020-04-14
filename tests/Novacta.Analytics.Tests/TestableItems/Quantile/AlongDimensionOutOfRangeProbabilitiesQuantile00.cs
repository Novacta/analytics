// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Quantile
{
    /// <summary>
    /// Represents a quantile operation whose probabilities operand contains
    /// a negative entry.
    /// </summary>
    class AlongDimensionOutOfRangeProbabilitiesQuantile00 :
        AlongDimensionQuantile<ArgumentException>
    {
        protected AlongDimensionOutOfRangeProbabilitiesQuantile00(
            DataOperation dataOperation) :
            base(
                expected: new ArgumentException(
                    message: String.Format((string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_PAR_ENTRIES_NOT_IN_CLOSED_INTERVAL" }), 0, 1),
                    paramName: "probabilities"),
                data: TestableDoubleMatrix32.Get(),
                probabilities: DoubleMatrix.Dense(1, 2, new double[2] { 0, -1 }),
                dataOperation: dataOperation)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="AlongDimensionOutOfRangeProbabilitiesQuantile00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="AlongDimensionOutOfRangeProbabilitiesQuantile00"/> class.</returns>
        public static AlongDimensionOutOfRangeProbabilitiesQuantile00 Get(
            DataOperation dataOperation)
        {
            return new AlongDimensionOutOfRangeProbabilitiesQuantile00(
                dataOperation);
        }
    }
}