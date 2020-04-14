// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.SumOfSquaredDeviations
{
    /// <summary>
    /// Represents a sum of squared deviations operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class OnAllDataIsNullSumOfSquaredDeviations : 
        OnAllSumOfSquaredDeviations<ArgumentNullException>
    {
        protected OnAllDataIsNullSumOfSquaredDeviations() :
            base(
                expected: new ArgumentNullException(paramName: "data"),
                data: null)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllDataIsNullSumOfSquaredDeviations"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllDataIsNullSumOfSquaredDeviations"/> class.</returns>
        public static OnAllDataIsNullSumOfSquaredDeviations Get()
        {
            return new OnAllDataIsNullSumOfSquaredDeviations();
        }
    }
}
