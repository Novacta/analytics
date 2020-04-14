// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a max operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class OnAllDataIsNullMax : 
        OnAllMax<ArgumentNullException>
    {
        protected OnAllDataIsNullMax() :
            base(
                expected: new ArgumentNullException(paramName: "data"),
                data: null)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllDataIsNullMax"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllDataIsNullMax"/> class.</returns>
        public static OnAllDataIsNullMax Get()
        {
            return new OnAllDataIsNullMax();
        }
    }
}
