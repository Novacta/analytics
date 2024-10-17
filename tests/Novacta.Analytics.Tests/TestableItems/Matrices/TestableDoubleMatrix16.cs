// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    ///      [c0]  [c1]  [c2] <para /> 
    /// [r0]   0     2     4  <para /> 
    /// [r1]   1     3     5  <para /> 
    /// </summary>
    class TestableDoubleMatrix16 : TestableDoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrix16" /> class.
        /// </summary>
        TestableDoubleMatrix16() : base(
                asColumnMajorDenseArray: [0, 1, 2, 3, 4, 5],
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
                columnNames: new Dictionary<int, string>(3) { { 0, "c0" }, { 1, "c1" }, { 2, "c2" } })
        {
            //      [c0]  [c1]  [c2]  
            // [r0]   0     2     4   
            // [r1]   1     3     5   
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableDoubleMatrix16"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableDoubleMatrix16"/> class.</returns>
        public static TestableDoubleMatrix16 Get()
        {
            return new TestableDoubleMatrix16();
        }
    }
}
