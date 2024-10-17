// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a max operation whose data operand 
    /// has one row.
    /// </summary>
    class OnColumnsDataIsRowMax :
        AlongDimensionMax<IndexValuePair[]>
    {
        protected OnColumnsDataIsRowMax() :
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
        /// Gets an instance of the <see cref="OnColumnsDataIsRowMax"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsDataIsRowMax"/> class.</returns>
        public static OnColumnsDataIsRowMax Get()
        {
            return new OnColumnsDataIsRowMax();
        }
    }
}