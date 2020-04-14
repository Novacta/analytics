// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Min
{
    /// <summary>
    /// Represents a sum operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class AlongDimensionDataIsNullMin :
        AlongDimensionMin<ArgumentNullException>
    {
        protected AlongDimensionDataIsNullMin(
            DataOperation dataOperation) :
            base(
                expected: new ArgumentNullException(paramName: "data"),
                data: null,
                dataOperation: dataOperation)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="AlongDimensionDataIsNullMin"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="AlongDimensionDataIsNullMin"/> class.</returns>
        public static AlongDimensionDataIsNullMin Get(
            DataOperation dataOperation)
        {
            return new AlongDimensionDataIsNullMin(
                dataOperation);
        }
    }
}
