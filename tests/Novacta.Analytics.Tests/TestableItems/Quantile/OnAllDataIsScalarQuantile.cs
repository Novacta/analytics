// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Quantile
{
    /// <summary>
    /// Represents a quantile operation whose data operand is
    /// scalar.
    /// </summary>
    class OnAllDataIsScalarQuantile : 
        OnAllQuantile<DoubleMatrixState>
    {
        protected OnAllDataIsScalarQuantile() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[1] {
                        TestableDoubleMatrix19.Get().AsDense[0] },
                    numberOfRows: 1,
                    numberOfColumns: 1),
                data: TestableDoubleMatrix19.Get(),
                probabilities: DoubleMatrix.Identity(1)
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllDataIsScalarQuantile"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllDataIsScalarQuantile"/> class.</returns>
        public static OnAllDataIsScalarQuantile Get()
        {
            return new OnAllDataIsScalarQuantile();
        }
    }
}
