// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Correlation
{
    /// <summary>
    /// Represents a testable covariance which summarizes
    /// all row items in the matrix represented by <see cref="TestableDoubleMatrix40"/>.
    /// </summary>
    class OnRowsCorrelation00 :
        AlongDimensionCorrelation<DoubleMatrixState>
    {
        protected OnRowsCorrelation00() :
                base(
                    expected: new DoubleMatrixState(
                        asColumnMajorDenseArray:
                            [
                                 1.00000000000000,
                                -0.69457793721244,
                                -0.64992883353843,
                                 0.40130872048658,
                                 0.78749923095816,
                                -0.69457793721244,
                                 1.00000000000000,
                                 0.99286999263187,
                                -0.90688707511655,
                                -0.79504639199993,
                                -0.64992883353843,
                                 0.99286999263187,
                                 1.00000000000000,
                                -0.94719607858054,
                                -0.82530726124983,
                                 0.40130872048658,
                                -0.90688707511655,
                                -0.94719607858054,
                                 1.00000000000000,
                                 0.75847274102396,
                                 0.78749923095816,
                                -0.79504639199993,
                                -0.82530726124983,
                                 0.75847274102396,
                                 1.00000000000000 ],
                        numberOfRows: 5,
                        numberOfColumns: 5
                    ),
                    data: TestableDoubleMatrix45.Get(),
                    dataOperation: DataOperation.OnRows
                )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsCorrelation00"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsCorrelation00"/> class.</returns>
        public static OnRowsCorrelation00 Get()
        {
            return new OnRowsCorrelation00();
        }
    }
}