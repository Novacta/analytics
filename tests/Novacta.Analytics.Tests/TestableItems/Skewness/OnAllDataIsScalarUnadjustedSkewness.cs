// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Skewness
{
    /// <summary>
    /// Represents a skewness operation whose data operand is
    /// scalar.
    /// </summary>
    class OnAllDataIsScalarUnadjustedSkewness : OnAllSkewness<double>
    {
        protected OnAllDataIsScalarUnadjustedSkewness() :
            base(
                expected: double.NaN,
                data: TestableDoubleMatrix19.Get(), 
                adjustForBias: false
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllDataIsScalarUnadjustedSkewness"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllDataIsScalarUnadjustedSkewness"/> class.</returns>
        public static OnAllDataIsScalarUnadjustedSkewness Get()
        {
            return new OnAllDataIsScalarUnadjustedSkewness();
        }
    }
}
