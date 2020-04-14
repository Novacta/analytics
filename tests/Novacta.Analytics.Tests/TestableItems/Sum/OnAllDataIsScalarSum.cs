// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Sum
{
    /// <summary>
    /// Represents a sum operation whose data operand is
    /// scalar.
    /// </summary>
    class OnAllDataIsScalarSum : 
        OnAllSum<double>
    {
        protected OnAllDataIsScalarSum() :
            base(
                expected: 2.0,
                data: TestableDoubleMatrix19.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllDataIsScalarSum"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllDataIsScalarSum"/> class.</returns>
        public static OnAllDataIsScalarSum Get()
        {
            return new OnAllDataIsScalarSum();
        }
    }
}
