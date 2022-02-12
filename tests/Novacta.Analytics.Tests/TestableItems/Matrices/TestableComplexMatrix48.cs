// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Matrices
{
    /// <summary>
    /// Provides methods to test implementations of matrix 
    /// <para /> 
    /// ( 0 , 0 )  (0 ,0 ) ( 0 , 0 )  0   0 <para /> 
    /// ( 0s, 0s)  (0 ,0 ) (-1 ,-1 )  0   0 <para />
    /// (-2 ,-2 )  (0 ,0 ) ( 0s, 0s)  0   0 <para />
    /// ( 0 , 0 )  (0s,0s) ( 0 , 0 )  0   0 <para />
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="TestableComplexMatrix.AsSparse"/> method is overridden
    /// in order to obtain that positions 1, 7, and 10 are a stored ones,
    /// even if they contain a zero value.
    /// </para>
    /// </remarks>
    class TestableComplexMatrix48 : TestableComplexMatrix
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[20]
            {
                0,  0, -2,  0,
                0,  0,  0,  0,
                0, -1,  0,  0,
                0,  0,  0,  0,
                0,  0,  0,  0
            };
            var asColumnMajorDenseArray = new Complex[20];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], d[i]);
            }
            return asColumnMajorDenseArray;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrix48" /> class.
        /// </summary>
        TestableComplexMatrix48() : base(
                asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                numberOfRows: 4,
                numberOfColumns: 5,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: false,
                isSkewSymmetric: false,
                isHermitian: false,
                isSkewHermitian: false,
                upperBandwidth: 1,
                lowerBandwidth: 2)
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

                // Force storage for position 7.
                sparse[7] = 1.0;
                // Set the expected value for position 7.
                sparse[7] = 0.0;

                // Force storage for position 10.
                sparse[10] = 1.0;
                // Set the expected value for position 10.
                sparse[10] = 0.0;

                return sparse;
            }
        }

        /// <summary>
        /// Gets an instance of the <see cref="TestableComplexMatrix48"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableComplexMatrix48"/> class.</returns>
        public static TestableComplexMatrix48 Get()
        {
            return new TestableComplexMatrix48();
        }
    }
}
