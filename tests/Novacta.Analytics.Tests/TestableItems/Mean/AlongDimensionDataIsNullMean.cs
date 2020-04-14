// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Mean
{
    /// <summary>
    /// Represents a sum operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class AlongDimensionDataIsNullMean :
        AlongDimensionMin<ArgumentNullException>
    {
        protected AlongDimensionDataIsNullMean(
            DataOperation dataOperation) :
            base(
                expected: new ArgumentNullException(paramName: "data"),
                data: null,
                dataOperation: dataOperation)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="AlongDimensionDataIsNullMean"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="AlongDimensionDataIsNullMean"/> class.</returns>
        public static AlongDimensionDataIsNullMean Get(
            DataOperation dataOperation)
        {
            return new AlongDimensionDataIsNullMean(
                dataOperation);
        }
    }
}
