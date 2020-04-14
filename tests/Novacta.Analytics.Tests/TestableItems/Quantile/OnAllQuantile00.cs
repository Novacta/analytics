// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Quantile
{
    /// <summary>
    /// Represents a testable quantile which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix52"/>.
    /// </summary>
    class OnAllQuantile00 :
        OnAllQuantile<DoubleMatrixState>
    {
        protected OnAllQuantile00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray: new double[4] { 1.0, 50.5, 75.583333, 100.0 },
                        numberOfRows: 2,
                        numberOfColumns: 2),
                    data: TestableDoubleMatrix52.Get(),
                    probabilities: DoubleMatrix.Dense(2, 2, new double[4] { .005, .5, .75, .999 }))
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllQuantile00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllQuantile00"/> class.</returns>
        public static OnAllQuantile00 Get()
        {
            return new OnAllQuantile00();
        }
    }
}
