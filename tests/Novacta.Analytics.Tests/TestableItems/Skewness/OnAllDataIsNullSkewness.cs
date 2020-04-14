// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Skewness
{
    /// <summary>
    /// Represents a skewness operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class OnAllDataIsNullSkewness : OnAllSkewness<ArgumentNullException>
    {
        protected OnAllDataIsNullSkewness(bool adjustForBias) :
            base(
                expected: new ArgumentNullException(paramName: "data"),
                data: null, adjustForBias: adjustForBias)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllDataIsNullSkewness"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllDataIsNullSkewness"/> class.</returns>
        public static OnAllDataIsNullSkewness Get(
            bool adjustForBias)
        {
            return new OnAllDataIsNullSkewness(adjustForBias);
        }
    }
}
