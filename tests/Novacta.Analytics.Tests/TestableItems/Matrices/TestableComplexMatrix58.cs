// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    ///     [c0]     [c1]     [c2] <para /> 
    ///        0    (2,2)    (4,4)  <para /> 
    ///    (1,1)    (3,3)    (5,5)  <para /> 
    /// </summary>
    class TestableComplexMatrix58 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix58" /> class.
        /// </summary>
        TestableComplexMatrix58() : base(
                asColumnMajorDenseArray:
                [
                    new(0, 0),
                    new(1, 1),
                    new(2, 2),
                    new(3, 3),
                    new(4, 4),
                    new(5, 5)
                ],
                numberOfRows: 2,
                numberOfColumns: 3,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 2,
                lowerBandwidth: 1,
                name: "Target",
                rowNames: null,
                columnNames: new Dictionary<int, string>(3) { { 0, "c0" }, { 1, "c1" }, { 2, "c2" } })
        {
            //     [c0]     [c1]     [c2]
            //        0    (2,2)    (4,4)
            //    (1,1)    (3,3)    (5,5)
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix58"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix58"/> class.</returns>
        public static TestableComplexMatrix58 Get()
        {
            return new TestableComplexMatrix58();
        }
    }
}
