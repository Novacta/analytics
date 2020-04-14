// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Variance
{
    /// <summary>
    /// Represents a variance operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class OnAllDataIsNullVariance : OnAllVariance<ArgumentNullException>
    {
        protected OnAllDataIsNullVariance(bool adjustForBias) :
            base(
                expected: new ArgumentNullException(paramName: "data"),
                data: null, adjustForBias: adjustForBias)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllDataIsNullVariance"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllDataIsNullVariance"/> class.</returns>
        public static OnAllDataIsNullVariance Get(
            bool adjustForBias)
        {
            return new OnAllDataIsNullVariance(adjustForBias);
        }
    }
}
