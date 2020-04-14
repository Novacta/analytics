// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;

namespace Novacta.Analytics.Tests.TestableItems.SumOfSquaredDeviations
{
    /// <summary>
    /// Represents a sum of squared deviations operation whose data operand is
    /// scalar.
    /// </summary>
    class OnAllDataIsScalarSumOfSquaredDeviations : 
        OnAllSumOfSquaredDeviations<double>
    {
        protected OnAllDataIsScalarSumOfSquaredDeviations() :
            base(
                expected: 0.0,
                data: TestableDoubleMatrix19.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllDataIsScalarSumOfSquaredDeviations"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllDataIsScalarSumOfSquaredDeviations"/> class.</returns>
        public static OnAllDataIsScalarSumOfSquaredDeviations Get()
        {
            return new OnAllDataIsScalarSumOfSquaredDeviations();
        }
    }
}
