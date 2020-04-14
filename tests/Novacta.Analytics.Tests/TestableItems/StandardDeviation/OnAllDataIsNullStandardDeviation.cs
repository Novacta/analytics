// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.StandardDeviation
{
    /// <summary>
    /// Represents a standard deviation operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class OnAllDataIsNullStandardDeviation : OnAllStandardDeviation<ArgumentNullException>
    {
        protected OnAllDataIsNullStandardDeviation(bool adjustForBias) :
            base(
                expected: new ArgumentNullException(paramName: "data"),
                data: null, adjustForBias: adjustForBias)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllDataIsNullStandardDeviation"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllDataIsNullStandardDeviation"/> class.</returns>
        public static OnAllDataIsNullStandardDeviation Get(
            bool adjustForBias)
        {
            return new OnAllDataIsNullStandardDeviation(adjustForBias);
        }
    }
}
