// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Kurtosis
{
    /// <summary>
    /// Represents a kurtosis operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class AlongDimensionDataIsNullKurtosis :
        AlongDimensionKurtosis<ArgumentNullException>
    {
        protected AlongDimensionDataIsNullKurtosis(
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
        /// Gets an instance of the <see cref="AlongDimensionDataIsNullKurtosis"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="AlongDimensionDataIsNullKurtosis"/> class.</returns>
        public static AlongDimensionDataIsNullKurtosis Get(
            bool adjustForBias,
            DataOperation dataOperation)
        {
            return new AlongDimensionDataIsNullKurtosis(
                adjustForBias,
                dataOperation);
        }
    }
}
