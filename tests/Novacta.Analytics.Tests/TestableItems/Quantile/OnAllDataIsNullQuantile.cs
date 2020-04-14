// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Quantile
{
    /// <summary>
    /// Represents a quantile operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class OnAllDataIsNullQuantile : 
        OnAllQuantile<ArgumentNullException>
    {
        protected OnAllDataIsNullQuantile() :
            base(
                expected: new ArgumentNullException(paramName: "data"),
                data: null,
                probabilities: DoubleMatrix.Identity(2))
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllDataIsNullQuantile"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllDataIsNullQuantile"/> class.</returns>
        public static OnAllDataIsNullQuantile Get()
        {
            return new OnAllDataIsNullQuantile();
        }
    }
}
