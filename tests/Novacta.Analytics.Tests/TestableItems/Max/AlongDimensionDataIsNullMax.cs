// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Max
{
    /// <summary>
    /// Represents a sum operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class AlongDimensionDataIsNullMax :
        AlongDimensionMax<ArgumentNullException>
    {
        protected AlongDimensionDataIsNullMax(
            DataOperation dataOperation) :
            base(
                expected: new ArgumentNullException(paramName: "data"),
                data: null,
                dataOperation: dataOperation)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="AlongDimensionDataIsNullMax"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="AlongDimensionDataIsNullMax"/> class.</returns>
        public static AlongDimensionDataIsNullMax Get(
            DataOperation dataOperation)
        {
            return new AlongDimensionDataIsNullMax(
                dataOperation);
        }
    }
}
