﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Min
{
    /// <summary>
    /// Represents a testable min which summarizes
    /// all column items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix49"/>.
    /// </summary>
    class OnColumnsMin06 :
        AlongDimensionMin<IndexValuePair[]>
    {
        protected OnColumnsMin06() :
                base(
                    expected: [
                        new() { index = 0, value = 0.0 },
                        new() { index = 0, value = 0.0 },
                        new() { index = 0, value = 0.0 },
                        new() { index = 0, value = 0.0 },
                        new() { index = 0, value = 0.0 }
                    ],
                    data: TestableDoubleMatrix49.Get(),
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsMin06"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsMin06"/> class.</returns>
        public static OnColumnsMin06 Get()
        {
            return new OnColumnsMin06();
        }
    }
}