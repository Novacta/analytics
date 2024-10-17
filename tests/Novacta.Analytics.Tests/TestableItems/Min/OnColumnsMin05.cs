// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Min
{
    /// <summary>
    /// Represents a testable min which summarizes
    /// all column items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix48"/>.
    /// </summary>
    class OnColumnsMin05 :
        AlongDimensionMin<IndexValuePair[]>
    {
        protected OnColumnsMin05() :
                base(
                    expected: [
                        new() { index = 2, value = -2.0 },
                        new() { index = 0, value = 0.0 },
                        new() { index = 1, value = -1.0 },
                        new() { index = 0, value = 0.0 },
                        new() { index = 0, value = 0.0 }
                    ],
                    data: TestableDoubleMatrix48.Get(),
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsMin05"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsMin05"/> class.</returns>
        public static OnColumnsMin05 Get()
        {
            return new OnColumnsMin05();
        }
    }
}