// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Sum
{
    /// <summary>
    /// Represents a sum operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class AlongDimensionDataIsNullSum :
        AlongDimensionSum<ArgumentNullException>
    {
        protected AlongDimensionDataIsNullSum(
            DataOperation dataOperation) :
            base(
                expected: new ArgumentNullException(paramName: "data"),
                data: null,
                dataOperation: dataOperation)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="AlongDimensionDataIsNullSum"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="AlongDimensionDataIsNullSum"/> class.</returns>
        public static AlongDimensionDataIsNullSum Get(
            DataOperation dataOperation)
        {
            return new AlongDimensionDataIsNullSum(
                dataOperation);
        }
    }
}
