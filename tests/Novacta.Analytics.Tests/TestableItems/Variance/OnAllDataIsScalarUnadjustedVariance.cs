// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Variance
{
    /// <summary>
    /// Represents a variance operation whose data operand is
    /// scalar.
    /// </summary>
    class OnAllDataIsScalarUnadjustedVariance : OnAllVariance<double>
    {
        protected OnAllDataIsScalarUnadjustedVariance() :
            base(
                expected: 0.0,
                data: TestableDoubleMatrix19.Get(), 
                adjustForBias: false
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllDataIsScalarUnadjustedVariance"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllDataIsScalarUnadjustedVariance"/> class.</returns>
        public static OnAllDataIsScalarUnadjustedVariance Get()
        {
            return new OnAllDataIsScalarUnadjustedVariance();
        }
    }
}
