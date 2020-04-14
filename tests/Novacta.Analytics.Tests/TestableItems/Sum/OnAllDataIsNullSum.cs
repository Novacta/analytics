﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Sum
{
    /// <summary>
    /// Represents a sum operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class OnAllDataIsNullSum : 
        OnAllSum<ArgumentNullException>
    {
        protected OnAllDataIsNullSum() :
            base(
                expected: new ArgumentNullException(paramName: "data"),
                data: null)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllDataIsNullSum"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllDataIsNullSum"/> class.</returns>
        public static OnAllDataIsNullSum Get()
        {
            return new OnAllDataIsNullSum();
        }
    }
}
