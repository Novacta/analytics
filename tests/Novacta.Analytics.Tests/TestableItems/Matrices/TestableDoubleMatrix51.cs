// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// -6     0    -1  -13  -4 <para /> 
    /// -5   -11   -10   -8  -3 <para />
    /// -1    -7    -6  -12  -2 <para />
    /// -4   -14   -15   -1   0 <para />
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="TestableDoubleMatrix.Sparse"/> method is overridden
    /// in order to obtain that position 19 is a stored one,
    /// even if it contains a zero value.
    /// </para>
    /// </remarks>
    class TestableDoubleMatrix51 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix51" /> class.
        /// </summary>
        TestableDoubleMatrix51() : base(
                asColumnMajorDenseArray: new double[20] {
                   -6,  -5,  -1,  -4,
                    0, -11,  -7, -14,
                   -1, -10,  -6, -15,
                  -13,  -8, -12,  -1,
                   -4,  -3,  -2,   0
                },
                numberOfRows: 4,
                numberOfColumns: 5,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 4,
                lowerBandwidth: 3)
        {
        }

        /// <inheritdoc/>
        public override DoubleMatrix Sparse
        {
            get
            {
                var sparse = base.Sparse;

                // Force storage for position 19.
                sparse[19] = 1.0;
                // Set the expected value for position 19.
                sparse[19] = 0.0;

                return sparse;
            }
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix51"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix51"/> class.</returns>
        public static TestableDoubleMatrix51 Get()
        {
            return new TestableDoubleMatrix51();
        }
    }
}
