// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.Mean
{
    /// <summary>
    /// Represents a mean operation whose data operand is
    /// scalar.
    /// </summary>
    class OnAllDataIsScalarMean : 
        OnAllMean<double>
    {
        protected OnAllDataIsScalarMean() :
            base(
                expected: 2.0,
                data: TestableDoubleMatrix19.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllDataIsScalarMean"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllDataIsScalarMean"/> class.</returns>
        public static OnAllDataIsScalarMean Get()
        {
            return new OnAllDataIsScalarMean();
        }
    }
}
