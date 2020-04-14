// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Kurtosis
{
    /// <summary>
    /// Represents a kurtosis operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class OnAllDataIsNullKurtosis : OnAllKurtosis<ArgumentNullException>
    {
        protected OnAllDataIsNullKurtosis(bool adjustForBias) :
            base(
                expected: new ArgumentNullException(paramName: "data"),
                data: null, adjustForBias: adjustForBias)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllDataIsNullKurtosis"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllDataIsNullKurtosis"/> class.</returns>
        public static OnAllDataIsNullKurtosis Get(
            bool adjustForBias)
        {
            return new OnAllDataIsNullKurtosis(adjustForBias);
        }
    }
}
