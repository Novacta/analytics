// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Quantile
{
    /// <summary>
    /// Represents a sum operation whose data operand is
    /// <b>null</b>.
    /// </summary>
    class AlongDimensionProbabilitiesIsNullQuantile :
        AlongDimensionQuantile<ArgumentNullException>
    {
        protected AlongDimensionProbabilitiesIsNullQuantile(
            DataOperation dataOperation) :
            base(
                expected: new ArgumentNullException(paramName: "probabilities"),
                data: TestableDoubleMatrix00.Get(),
                probabilities: null,
                dataOperation: dataOperation)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="AlongDimensionProbabilitiesIsNullQuantile"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="AlongDimensionProbabilitiesIsNullQuantile"/> class.</returns>
        public static AlongDimensionProbabilitiesIsNullQuantile Get(
            DataOperation dataOperation)
        {
            return new AlongDimensionProbabilitiesIsNullQuantile(
                dataOperation);
        }
    }
}