// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// [r0]   0     2     4  <para /> 
    /// [r1]   1     3     5  <para /> 
    /// </summary>
    class TestableDoubleMatrix57 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix57" /> class.
        /// </summary>
        TestableDoubleMatrix57() : base(
                asColumnMajorDenseArray: new double[6] { 0, 1, 2, 3, 4, 5 },
                numberOfRows: 2,
                numberOfColumns: 3,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                upperBandwidth: 2,
                lowerBandwidth: 1,
                name: "Target",
                rowNames: new Dictionary<int, string>(2) { { 0, "r0" }, { 1, "r1" } },
                columnNames: null)
        {
            //      
            // [r0]   0     2     4   
            // [r1]   1     3     5   
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix57"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix57"/> class.</returns>
        public static TestableDoubleMatrix57 Get()
        {
            return new TestableDoubleMatrix57();
        }
    }
}
