// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.SumOfSquaredDeviations
{
    /// <summary>
    /// Represents a sum of squared deviations operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class AlongDimensionDataIsNullSumOfSquaredDeviations :
        AlongDimensionSumOfSquaredDeviations<ArgumentNullException>
    {
        protected AlongDimensionDataIsNullSumOfSquaredDeviations(
            DataOperation dataOperation) :
            base(
                expected: new ArgumentNullException(paramName: "data"),
                data: null,
                dataOperation: dataOperation)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="AlongDimensionDataIsNullSumOfSquaredDeviations"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="AlongDimensionDataIsNullSumOfSquaredDeviations"/> class.</returns>
        public static AlongDimensionDataIsNullSumOfSquaredDeviations Get(
            DataOperation dataOperation)
        {
            return new AlongDimensionDataIsNullSumOfSquaredDeviations(
                dataOperation);
        }
    }
}
