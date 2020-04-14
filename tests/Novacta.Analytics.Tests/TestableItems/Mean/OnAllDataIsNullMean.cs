// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Mean
{
    /// <summary>
    /// Represents a mean operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class OnAllDataIsNullMean : 
        OnAllMean<ArgumentNullException>
    {
        protected OnAllDataIsNullMean() :
            base(
                expected: new ArgumentNullException(paramName: "data"),
                data: null)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllDataIsNullMean"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllDataIsNullMean"/> class.</returns>
        public static OnAllDataIsNullMean Get()
        {
            return new OnAllDataIsNullMean();
        }
    }
}
