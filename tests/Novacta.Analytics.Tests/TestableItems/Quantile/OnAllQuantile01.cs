// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Quantile
{
    /// <summary>
    /// Represents a testable quantile operation which summarizes
    /// all items in the matrix represented by <see cref="TestableDoubleMatrix52"/>.
    /// The quantiles correspond to known probabilities.
    /// </summary>
    class OnAllQuantile01 :
        OnAllQuantile<DoubleMatrixState>
    {
        static DoubleMatrix GetKnownProbabilities(int dataCount)
        {
            var knownProbabilities = DoubleMatrix.Dense(dataCount, 1);

            for (int l = 0; l < dataCount; l++)
            {
                knownProbabilities[l] = (l + 2.0 / 3.0) / (dataCount + 1.0 / 3.0);
            }

            return knownProbabilities;
        }

        protected OnAllQuantile01() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray: 
                            TestableDoubleMatrix52.Get().AsDense.AsColumnMajorDenseArray(),
                        numberOfRows: TestableDoubleMatrix52.Get().AsDense.Count,
                        numberOfColumns: 1),
                    data: TestableDoubleMatrix52.Get(),
                    probabilities: GetKnownProbabilities(
                        TestableDoubleMatrix52.Get().AsDense.Count))
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllQuantile01"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllQuantile01"/> class.</returns>
        public static OnAllQuantile01 Get()
        {
            return new OnAllQuantile01();
        }
    }
}
