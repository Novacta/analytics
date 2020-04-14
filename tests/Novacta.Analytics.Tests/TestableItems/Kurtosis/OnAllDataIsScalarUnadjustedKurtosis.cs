// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Kurtosis
{
    /// <summary>
    /// Represents a kurtosis operation whose data operand is
    /// scalar.
    /// </summary>
    class OnAllDataIsScalarUnadjustedKurtosis : OnAllKurtosis<double>
    {
        protected OnAllDataIsScalarUnadjustedKurtosis() :
            base(
                expected: double.NaN,
                data: TestableDoubleMatrix19.Get(), 
                adjustForBias: false
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllDataIsScalarUnadjustedKurtosis"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllDataIsScalarUnadjustedKurtosis"/> class.</returns>
        public static OnAllDataIsScalarUnadjustedKurtosis Get()
        {
            return new OnAllDataIsScalarUnadjustedKurtosis();
        }
    }
}
