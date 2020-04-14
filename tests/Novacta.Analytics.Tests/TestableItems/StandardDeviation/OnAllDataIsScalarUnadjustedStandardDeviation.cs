// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.StandardDeviation
{
    /// <summary>
    /// Represents a standard deviation operation whose data operand is
    /// scalar.
    /// </summary>
    class OnAllDataIsScalarUnadjustedStandardDeviation : OnAllStandardDeviation<double>
    {
        protected OnAllDataIsScalarUnadjustedStandardDeviation() :
            base(
                expected: 0.0,
                data: TestableDoubleMatrix19.Get(), 
                adjustForBias: false
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllDataIsScalarUnadjustedStandardDeviation"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllDataIsScalarUnadjustedStandardDeviation"/> class.</returns>
        public static OnAllDataIsScalarUnadjustedStandardDeviation Get()
        {
            return new OnAllDataIsScalarUnadjustedStandardDeviation();
        }
    }
}
