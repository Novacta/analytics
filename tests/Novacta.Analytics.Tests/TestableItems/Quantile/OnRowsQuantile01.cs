// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Quantile
{
    /// <summary>
    /// Represents a testable quantile operation which summarizes
    /// all row items in the matrix represented 
    /// by <see cref="TestableDoubleMatrix53"/>.
    /// The quantiles correspond to known probabilities.
    /// </summary>
    class OnRowsQuantile01 :
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

        protected OnRowsQuantile01() :
                base(
                    expected: [
                        new(
                            asColumnMajorDenseArray:
                                TestableDoubleMatrix53.Get().AsDense[0,":"].AsColumnMajorDenseArray(),
                            numberOfRows: TestableDoubleMatrix53.Get().AsDense.NumberOfColumns,
                            numberOfColumns: 1),
                        new(
                            asColumnMajorDenseArray:
                                TestableDoubleMatrix53.Get().AsDense[1,":"].AsColumnMajorDenseArray(),
                            numberOfRows: TestableDoubleMatrix53.Get().AsDense.NumberOfColumns,
                            numberOfColumns: 1),
                        new(
                            asColumnMajorDenseArray:
                                TestableDoubleMatrix53.Get().AsDense[2,":"].AsColumnMajorDenseArray(),
                            numberOfRows: TestableDoubleMatrix53.Get().AsDense.NumberOfColumns,
                            numberOfColumns: 1)
                    ],
                    data: TestableDoubleMatrix53.Get(),
                    probabilities: GetKnownProbabilities(
                        TestableDoubleMatrix53.Get().AsDense.NumberOfColumns),
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsQuantile01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsQuantile01"/> class.</returns>
        public static OnRowsQuantile01 Get()
        {
            return new OnRowsQuantile01();
        }
    }
}