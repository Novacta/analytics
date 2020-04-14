// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Quantile
{
    /// <summary>
    /// Represents a quantile operation whose probabilities operand is
    /// <b>null</b>.
    /// </summary>
    class OnAllProbabilitiesIsNullQuantile :
        OnAllQuantile<ArgumentNullException>
    {
        protected OnAllProbabilitiesIsNullQuantile() :
            base(
                expected: new ArgumentNullException(paramName: "probabilities"),
                data: TestableDoubleMatrix00.Get(),
                probabilities: null)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllProbabilitiesIsNullQuantile"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllProbabilitiesIsNullQuantile"/> class.</returns>
        public static OnAllProbabilitiesIsNullQuantile Get()
        {
            return new OnAllProbabilitiesIsNullQuantile();
        }
    }
}
