// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a max operation whose data operand is
    /// scalar.
    /// </summary>
    class OnAllDataIsScalarMax : 
        OnAllMax<IndexValuePair>
    {
        protected OnAllDataIsScalarMax() :
            base(
                expected: new IndexValuePair() { index = 0, value = 2.0 },
                data: TestableDoubleMatrix19.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllDataIsScalarMax"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllDataIsScalarMax"/> class.</returns>
        public static OnAllDataIsScalarMax Get()
        {
            return new OnAllDataIsScalarMax();
        }
    }
}
