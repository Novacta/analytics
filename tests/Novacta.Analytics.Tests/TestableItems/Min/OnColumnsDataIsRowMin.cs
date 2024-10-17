// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Min
{
    /// <summary>
    /// Represents a min operation whose data operand 
    /// has one row.
    /// </summary>
    class OnColumnsDataIsRowMin :
        AlongDimensionMin<IndexValuePair[]>
    {
        protected OnColumnsDataIsRowMin() :
            base(
                expected: [
                    new() { index = 0, value = -5 },
                    new() { index = 0, value = -4 },
                    new() { index = 0, value = -3 },
                ],
                data: TestableDoubleMatrix21.Get(),
                dataOperation: DataOperation.OnColumns
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsDataIsRowMin"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsDataIsRowMin"/> class.</returns>
        public static OnColumnsDataIsRowMin Get()
        {
            return new OnColumnsDataIsRowMin();
        }
    }
}