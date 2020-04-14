// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Skewness
{
    /// <summary>
    /// Represents a skewness operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class AlongDimensionDataIsNullSkewness :
        AlongDimensionSkewness<ArgumentNullException>
    {
        protected AlongDimensionDataIsNullSkewness(
            bool adjustForBias,
            DataOperation dataOperation) :
            base(
                expected: new ArgumentNullException(paramName: "data"),
                data: null,
                adjustForBias: adjustForBias,
                dataOperation: dataOperation)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="AlongDimensionDataIsNullSkewness"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="AlongDimensionDataIsNullSkewness"/> class.</returns>
        public static AlongDimensionDataIsNullSkewness Get(
            bool adjustForBias,
            DataOperation dataOperation)
        {
            return new AlongDimensionDataIsNullSkewness(
                adjustForBias,
                dataOperation);
        }
    }
}
