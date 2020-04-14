// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 2  1 -2  <para /> 
    /// 0  0 -1  
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="TestableDoubleMatrix.Sparse"/> method is overridden
    /// in order to obtain that position 1 is a stored one,
    /// even if it contains a zero value.
    /// </para>
    /// </remarks>
    class TestableDoubleMatrix55 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix55" /> class.
        /// </summary>
        TestableDoubleMatrix55() : base(
                asColumnMajorDenseArray: new double[6] { 2, 0, 1, 0, -2, -1 },
                numberOfRows: 2,
                numberOfColumns: 3,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 2,
                lowerBandwidth: 0)
        {
        }

        /// <inheritdoc/>
        public override DoubleMatrix Sparse
        {
            get
            {
                var sparse = base.Sparse;

                // Force storage for position 1.
                sparse[1] = 1.0;
                // Set the expected value for position 1.
                sparse[1] = 0.0;

                return sparse;
            }
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix55"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix55"/> class.</returns>
        public static TestableDoubleMatrix55 Get()
        {
            return new TestableDoubleMatrix55();
        }
    }
}
