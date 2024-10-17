// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Quantile
{
    /// <summary>
    /// Represents a testable quantile operation which summarizes
    /// all column items in the matrix represented
    /// by <see cref="TestableDoubleMatrix52"/>.
    /// The quantiles correspond to known probabilities.
    /// </summary>
    class OnColumnsQuantile01 :
        AlongDimensionQuantile<DoubleMatrixState[]>
    {
        static DoubleMatrix GetKnownProbabilities(int numberOfKnownProbabilities)
        {
            var knownProbabilities = DoubleMatrix.Dense(numberOfKnownProbabilities, 1);

            for (int l = 0; l < numberOfKnownProbabilities; l++)
            {
                knownProbabilities[l] = (l + 2.0 / 3.0) / (numberOfKnownProbabilities + 1.0 / 3.0);
            }

            return knownProbabilities;
        }

        protected OnColumnsQuantile01() :
                base(
                    expected: [
                        new(
                            asColumnMajorDenseArray:
                                TestableDoubleMatrix52.Get().AsDense[":", 0].AsColumnMajorDenseArray(),
                            numberOfRows: TestableDoubleMatrix52.Get().AsDense.NumberOfRows,
                            numberOfColumns: 1),
                        new(
                            asColumnMajorDenseArray:
                                TestableDoubleMatrix52.Get().AsDense[":", 1].AsColumnMajorDenseArray(),
                            numberOfRows: TestableDoubleMatrix52.Get().AsDense.NumberOfRows,
                            numberOfColumns: 1),
                        new(
                            asColumnMajorDenseArray:
                                TestableDoubleMatrix52.Get().AsDense[":", 2].AsColumnMajorDenseArray(),
                            numberOfRows: TestableDoubleMatrix52.Get().AsDense.NumberOfRows,
                            numberOfColumns: 1),
                        new(
                            asColumnMajorDenseArray:
                                TestableDoubleMatrix52.Get().AsDense[":", 3].AsColumnMajorDenseArray(),
                            numberOfRows: TestableDoubleMatrix52.Get().AsDense.NumberOfRows,
                            numberOfColumns: 1)
                    ],
                    data: TestableDoubleMatrix52.Get(),
                    probabilities: GetKnownProbabilities(
                        TestableDoubleMatrix52.Get().AsDense.NumberOfRows),
                    dataOperation: DataOperation.OnColumns
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsQuantile01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsQuantile01"/> class.</returns>
        public static OnColumnsQuantile01 Get()
        {
            return new OnColumnsQuantile01();
        }
    }
}