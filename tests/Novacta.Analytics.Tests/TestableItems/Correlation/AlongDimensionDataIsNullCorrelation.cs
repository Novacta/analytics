// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Correlation
{
    /// <summary>
    /// Represents a covariance operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class AlongDimensionDataIsNullCorrelation :
        AlongDimensionCorrelation<ArgumentNullException>
    {
        protected AlongDimensionDataIsNullCorrelation(
            DataOperation dataOperation) :
            base(
                expected: new ArgumentNullException(paramName: "data"),
                data: null,
                dataOperation: dataOperation)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="AlongDimensionDataIsNullCorrelation"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="AlongDimensionDataIsNullCorrelation"/> class.</returns>
        public static AlongDimensionDataIsNullCorrelation Get(
            DataOperation dataOperation)
        {
            return new AlongDimensionDataIsNullCorrelation(
                dataOperation);
        }
    }
}
