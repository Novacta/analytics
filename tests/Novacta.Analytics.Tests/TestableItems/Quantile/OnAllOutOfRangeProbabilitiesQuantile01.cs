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
    /// an entry greater than 1.
    /// </summary>
    class OnAllOutOfRangeProbabilitiesQuantile01 :
        OnAllQuantile<ArgumentException>
    {
        protected OnAllOutOfRangeProbabilitiesQuantile01() :
            base(
                expected: new ArgumentException(
                    message: String.Format((string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_PAR_ENTRIES_NOT_IN_CLOSED_INTERVAL" }), 0, 1),
                    paramName: "probabilities"),
                data: TestableDoubleMatrix00.Get(),
                probabilities: DoubleMatrix.Dense(2, 1, [1, 1.1]))
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllOutOfRangeProbabilitiesQuantile01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllOutOfRangeProbabilitiesQuantile01"/> class.</returns>
        public static OnAllOutOfRangeProbabilitiesQuantile01 Get()
        {
            return new OnAllOutOfRangeProbabilitiesQuantile01();
        }
    }
}