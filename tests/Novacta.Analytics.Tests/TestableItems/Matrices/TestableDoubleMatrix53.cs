// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// 1     4    7    10    13 <para /> 
    /// 2     5    8    11    14 <para /> 
    /// 3     6    9    12    15 <para /> 
    /// </summary>
    class TestableDoubleMatrix53 : TestableDoubleMatrix
    {
        static double[] GetAsColumnMajorDenseArray()
        {
            var asColumnMajorDenseArray = new double[15];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = i + 1;
            }
            return asColumnMajorDenseArray;
        }

        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="TestableDoubleMatrix53" /> class.
        /// </summary>
        TestableDoubleMatrix53() : base(
                asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                numberOfRows: 3,
                numberOfColumns: 5,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 4,
                lowerBandwidth: 2)
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix53"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix53"/> class.</returns>
        public static TestableDoubleMatrix53 Get()
        {
            return new TestableDoubleMatrix53();
        }
    }
}
