// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Sum
{
    /// <summary>
    /// Represents a testable sum which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnAllSum00 :
        OnAllSum<double>
    {
        protected OnAllSum00() :
                base(
                    expected: 136.0,
                    data: TestableDoubleMatrix40.Get())
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllSum00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllSum00"/> class.</returns>
        public static OnAllSum00 Get()
        {
            return new OnAllSum00();
        }
    }
}
