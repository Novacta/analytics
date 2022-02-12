// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (1,1)    (0,0)  <para />
    /// (0,0)    (0,0)
    /// </summary>
    class TestableComplexMatrix09 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix09" /> class.
        /// </summary>
        TestableComplexMatrix09() : base(
                asColumnMajorDenseArray: new Complex[4] 
                {
                    new Complex(1, 1),
                    0,
                    0,
                    0
                },
                numberOfRows: 2,
                numberOfColumns: 2,
                isUpperHessenberg: true,
                isLowerHessenberg: true,
                isUpperTriangular: true,
                isLowerTriangular: true,
                isSymmetric: true,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 0,
                lowerBandwidth: 0)
        {
            // (1,1)    (0,0)  
            // (0,0)    (0,0)
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix09"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix09"/> class.</returns>
        public static TestableComplexMatrix09 Get()
        {
            return new TestableComplexMatrix09();
        }

        public override ComplexMatrix AsSparse
        {
            get
            {
                var c = new Complex(1, 1);
                var matrix = ComplexMatrix.Sparse(2, 2, 0);
                matrix[0, 0] = c;
                matrix[0, 1] = c;
                matrix[0, 1] = 0;

                return matrix;
            }
        }
    }

}
