// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Variance
{
    /// <summary>
    /// Represents a variance operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class AlongDimensionDataIsNullVariance :
        AlongDimensionVariance<ArgumentNullException>
    {
        protected AlongDimensionDataIsNullVariance(
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
        /// Gets an instance of the <see cref="AlongDimensionDataIsNullVariance"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="AlongDimensionDataIsNullVariance"/> class.</returns>
        public static AlongDimensionDataIsNullVariance Get(
            bool adjustForBias,
            DataOperation dataOperation)
        {
            return new AlongDimensionDataIsNullVariance(
                adjustForBias,
                dataOperation);
        }
    }
}
