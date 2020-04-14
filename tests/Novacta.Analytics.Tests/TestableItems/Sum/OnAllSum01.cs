// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Sum
{
    /// <summary>
    /// Represents a testable sum which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix41"/>.
    /// </summary>
    class OnAllSum01 :
        OnAllSum<double>
    {
        protected OnAllSum01() :
                base(
                    expected: 16.0,
                    data: TestableDoubleMatrix41.Get())
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllSum01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllSum01"/> class.</returns>
        public static OnAllSum01 Get()
        {
            return new OnAllSum01();
        }
    }
}
