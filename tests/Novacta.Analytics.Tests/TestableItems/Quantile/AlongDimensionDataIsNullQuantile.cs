// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Quantile
{
    /// <summary>
    /// Represents a sum operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class AlongDimensionDataIsNullQuantile :
        AlongDimensionQuantile<ArgumentNullException>
    {
        protected AlongDimensionDataIsNullQuantile(
            DataOperation dataOperation) :
            base(
                expected: new ArgumentNullException(paramName: "data"),
                data: null,
                probabilities: DoubleMatrix.Identity(2),
                dataOperation: dataOperation)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="AlongDimensionDataIsNullQuantile"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="AlongDimensionDataIsNullQuantile"/> class.</returns>
        public static AlongDimensionDataIsNullQuantile Get(
            DataOperation dataOperation)
        {
            return new AlongDimensionDataIsNullQuantile(
                dataOperation);
        }
    }
}
