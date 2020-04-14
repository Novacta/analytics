// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Sum
{
    /// <summary>
    /// Represents a testable sum which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix42"/>.
    /// </summary>
    class OnAllSum02 :
        OnAllSum<double>
    {
        protected OnAllSum02() :
                base(
                    expected: 0.0,
                    data: TestableDoubleMatrix42.Get())
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllSum02"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllSum02"/> class.</returns>
        public static OnAllSum02 Get()
        {
            return new OnAllSum02();
        }
    }
}
