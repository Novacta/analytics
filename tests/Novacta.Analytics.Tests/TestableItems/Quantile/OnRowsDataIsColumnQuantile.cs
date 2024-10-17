// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Quantile
{
    /// <summary>
    /// Represents a quantile operation whose data operand 
    /// has one column.
    /// </summary>
    class OnRowsDataIsColumnQuantile : 
        AlongDimensionQuantile<DoubleMatrixState[]>
    {
        protected OnRowsDataIsColumnQuantile() :
            base(
                expected: [
                    new(
                        asColumnMajorDenseArray: [
                            TestableDoubleMatrix20.Get().AsDense[0] ],
                        numberOfRows: 1,
                        numberOfColumns: 1),
                    new(
                        asColumnMajorDenseArray: [
                            TestableDoubleMatrix20.Get().AsDense[1] ],
                        numberOfRows: 1,
                        numberOfColumns: 1),
                    new(
                        asColumnMajorDenseArray: [
                            TestableDoubleMatrix20.Get().AsDense[2] ],
                        numberOfRows: 1,
                        numberOfColumns: 1)
                ],
                data: TestableDoubleMatrix20.Get(),
                probabilities: DoubleMatrix.Identity(1),
                dataOperation: DataOperation.OnRows
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsDataIsColumnQuantile"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsDataIsColumnQuantile"/> class.</returns>
        public static OnRowsDataIsColumnQuantile Get()
        {
            return new OnRowsDataIsColumnQuantile();
        }
    }
}