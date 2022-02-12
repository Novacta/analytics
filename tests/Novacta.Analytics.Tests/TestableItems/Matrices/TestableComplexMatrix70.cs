// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (0,1)       (0,0) <para /> 
    /// (0,0)s      (0,4) <para /> 
    /// </summary>
    class TestableComplexMatrix70 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix70" /> class.
        /// </summary>
        TestableComplexMatrix70() : base(
                asColumnMajorDenseArray: new Complex[4]
                {
                    new Complex( 0, 1),
                    0,
                    0,
                    new Complex( 0, 4)
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
                isSkewHermitian: true,
                upperBandwidth: 0,
                lowerBandwidth: 0)
        {
            // (0,1)       (0,0)
            // (0,0)s      (0,4) 
        }

        /// <inheritdoc/>
        public override ComplexMatrix AsSparse
        {
            get
            {
                var sparse = base.AsSparse;

                // Force storage for position 1.
                sparse[1] = 1.0;
                // Set the expected value for position 1.
                sparse[1] = 0.0;

                return sparse;
            }
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix70"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix70"/> class.</returns>
        public static TestableComplexMatrix70 Get()
        {
            return new TestableComplexMatrix70();
        }
    }
}
