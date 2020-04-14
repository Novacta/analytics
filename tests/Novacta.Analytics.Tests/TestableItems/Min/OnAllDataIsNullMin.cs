// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Min
{
    /// <summary>
    /// Represents a min operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class OnAllDataIsNullMin : 
        OnAllMin<ArgumentNullException>
    {
        protected OnAllDataIsNullMin() :
            base(
                expected: new ArgumentNullException(paramName: "data"),
                data: null)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllDataIsNullMin"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllDataIsNullMin"/> class.</returns>
        public static OnAllDataIsNullMin Get()
        {
            return new OnAllDataIsNullMin();
        }
    }
}
