// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// (2,2)   (1,1)   (-2,-2)  <para /> 
    /// (0,0)   (0,0)   (-1,-1)  
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="TestableComplexMatrix.AsSparse"/> method is overridden
    /// in order to obtain that position 1 is a stored one,
    /// even if it contains a zero value.
    /// </para>
    /// </remarks>
    class TestableComplexMatrix55 : TestableComplexMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix55" /> class.
        /// </summary>
        TestableComplexMatrix55() : base(
                asColumnMajorDenseArray:
                [
                    new(2, 2),
                    new(0, 0),
                    new(1, 1),
                    new(0, 0),
                    new(-2, -2),
                    new(-1, -1)
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
                lowerBandwidth: 0)
        {
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
        /// Gets an instance of the <see cref="TestableComplexMatrix55"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix55"/> class.</returns>
        public static TestableComplexMatrix55 Get()
        {
            return new TestableComplexMatrix55();
        }
    }
}
