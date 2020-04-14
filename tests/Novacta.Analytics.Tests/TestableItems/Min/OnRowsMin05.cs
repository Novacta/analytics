// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Min
{
    /// <summary>
    /// Represents a testable min which summarizes
    /// all row items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix48"/>.
    /// </summary>
    class OnRowsMin05 :
        AlongDimensionMin<IndexValuePair[]>
    {
        protected OnRowsMin05() :
                base(
                    expected: new IndexValuePair[4] {
                        new IndexValuePair() { index = 0, value = 0.0 },
                        new IndexValuePair() { index = 2, value = -1.0 },
                        new IndexValuePair() { index = 0, value = -2.0 },
                        new IndexValuePair() { index = 0, value = 0.0 }
                    },
                    data: TestableDoubleMatrix48.Get(),
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsMin05"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsMin05"/> class.</returns>
        public static OnRowsMin05 Get()
        {
            return new OnRowsMin05();
        }
    }
}