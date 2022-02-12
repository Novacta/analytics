// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Division
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    /// l =                    <para />
    /// (1,1)  (3,3)  (5,5)  (7,7)  ( 9, 9)    <para /> 
    /// (2,2)  (4,4)  (6,6)  (8,8)  (10,10)    <para /> 
    ///
    /// r   =                  <para /> 
    /// (1,1)    (0,0)    ( 0, 0)    ( 0, 0)   ( 0, 0)  <para /> 
    /// (1,1)    (2,2)    ( 0, 0)    ( 0, 0)   ( 0, 0)  <para /> 
    /// (1,1)    (3,3)    ( 6, 6)    ( 0, 0)   ( 0, 0)  <para />
    /// (1,1)    (4,4)    (10,10)    (20,20)   ( 0, 0)  <para />
    /// (1,1)    (5,5)    (15,15)    (35,35)   (70,70)  <para />
    ///
    /// l / r  =           <para />
    /// -0.0303571428571429	0.473214285714286	0.303571428571429	0.125	0.128571428571429 <para /> 
    ///	 0.560714285714286	0.753571428571428	0.392857142857143	0.15	0.142857142857143 <para />   
    /// </summary>
    /// <seealso cref="TestableComplexMatrixComplexMatrixDivision{ComplexMatrixState}" />
    class RightIsLowerTriangularComplexMatrixComplexMatrixDivision :
        TestableComplexMatrixComplexMatrixDivision<ComplexMatrixState>
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[10]
            {
                -0.0303571428571429,
                0.560714285714286,
                0.473214285714286,
                0.753571428571428,
                0.303571428571429,
                0.392857142857143,
                0.125,
                0.15,
                0.128571428571429,
                0.142857142857143
            };
            var asColumnMajorDenseArray = new Complex[10];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], 0);
            }
            return asColumnMajorDenseArray;
        }

        RightIsLowerTriangularComplexMatrixComplexMatrixDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                    numberOfRows: 2,
                    numberOfColumns: 5),
                left: TestableComplexMatrix23.Get(),
                right: TestableComplexMatrix24.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsLowerTriangularComplexMatrixComplexMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsLowerTriangularComplexMatrixComplexMatrixDivision"/> class.</returns>
        public static RightIsLowerTriangularComplexMatrixComplexMatrixDivision Get()
        {
            return new RightIsLowerTriangularComplexMatrixComplexMatrixDivision();
        }
    }
}
