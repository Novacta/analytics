// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Min
{
    /// <summary>
    /// Represents a min operation whose data operand is
    /// scalar.
    /// </summary>
    class OnAllDataIsScalarMin : 
        OnAllMin<IndexValuePair>
    {
        protected OnAllDataIsScalarMin() :
            base(
                expected: new IndexValuePair() { index = 0, value = 2.0 },
                data: TestableDoubleMatrix19.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllDataIsScalarMin"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllDataIsScalarMin"/> class.</returns>
        public static OnAllDataIsScalarMin Get()
        {
            return new OnAllDataIsScalarMin();
        }
    }
}
