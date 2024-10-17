// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Min
{
    /// <summary>
    /// Represents a min operation whose data operand 
    /// has one column.
    /// </summary>
    class OnRowsDataIsColumnMin : 
        AlongDimensionMin<IndexValuePair[]>
    {
        protected OnRowsDataIsColumnMin() :
            base(
                expected: [
                    new() { index = 0, value = -5 },
                    new() { index = 0, value = -4 },
                    new() { index = 0, value = -3 },
                ],
                data: TestableDoubleMatrix20.Get(),
                dataOperation: DataOperation.OnRows
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsDataIsColumnMin"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsDataIsColumnMin"/> class.</returns>
        public static OnRowsDataIsColumnMin Get()
        {
            return new OnRowsDataIsColumnMin();
        }
    }
}