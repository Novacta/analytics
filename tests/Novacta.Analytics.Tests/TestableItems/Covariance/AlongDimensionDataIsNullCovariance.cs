// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Covariance
{
    /// <summary>
    /// Represents a covariance operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class AlongDimensionDataIsNullCovariance :
        AlongDimensionCovariance<ArgumentNullException>
    {
        protected AlongDimensionDataIsNullCovariance(
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
        /// Gets an instance of the <see cref="AlongDimensionDataIsNullCovariance"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="AlongDimensionDataIsNullCovariance"/> class.</returns>
        public static AlongDimensionDataIsNullCovariance Get(
            bool adjustForBias,
            DataOperation dataOperation)
        {
            return new AlongDimensionDataIsNullCovariance(
                adjustForBias,
                dataOperation);
        }
    }
}
