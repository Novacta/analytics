// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Kurtosis
{
    /// <summary>
    /// Represents a testable kurtosis which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix42"/>.
    /// </summary>
    class OnAllNotAdjustedKurtosis02 :
        OnAllKurtosis<double>
    {
        protected OnAllNotAdjustedKurtosis02() :
                base(
                    expected: Double.NaN,
                    data: TestableDoubleMatrix42.Get(),
                    adjustForBias: false)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllNotAdjustedKurtosis02"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllNotAdjustedKurtosis02"/> class.</returns>
        public static OnAllNotAdjustedKurtosis02 Get()
        {
            return new OnAllNotAdjustedKurtosis02();
        }
    }
}
