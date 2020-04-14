// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a max operation whose data operand 
    /// has one column.
    /// </summary>
    class OnRowsDataIsColumnMax : 
        AlongDimensionMax<IndexValuePair[]>
    {
        protected OnRowsDataIsColumnMax() :
            base(
                expected: new IndexValuePair[3] {
                    new IndexValuePair() { index = 0, value = -5 },
                    new IndexValuePair() { index = 0, value = -4 },
                    new IndexValuePair() { index = 0, value = -3 },
                },
                data: TestableDoubleMatrix20.Get(),
                dataOperation: DataOperation.OnRows
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsDataIsColumnMax"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsDataIsColumnMax"/> class.</returns>
        public static OnRowsDataIsColumnMax Get()
        {
            return new OnRowsDataIsColumnMax();
        }
    }
}