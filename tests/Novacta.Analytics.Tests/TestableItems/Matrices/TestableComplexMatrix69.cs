﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (1,0)       (0,0) <para /> 
    /// (0,0)s      (4,0) <para /> 
    /// </summary>
    class TestableComplexMatrix69 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix69" /> class.
        /// </summary>
        TestableComplexMatrix69() : base(
                asColumnMajorDenseArray:
                [
                    new(1, 0),
                    0,
                    0,
                    new(4, 0)
                ],
                numberOfRows: 2,
                numberOfColumns: 2,
                isUpperHessenberg: true,
                isLowerHessenberg: true,
                isUpperTriangular: true,
                isLowerTriangular: true,
                isSymmetric: true,
                isSkewSymmetric: false,
                isHermitian: true,
                isSkewHermitian: false,
                upperBandwidth: 0,
                lowerBandwidth: 0)
        {
            // (1,0)       (0,0)
            // (0,0)s      (4,0) 
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
        /// Gets an instance of the <see cref="TestableComplexMatrix69"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix69"/> class.</returns>
        public static TestableComplexMatrix69 Get()
        {
            return new TestableComplexMatrix69();
        }
    }
}
