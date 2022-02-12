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
    /// 1    0     0     0    0  <para /> 
    /// 1    2     0     0    0  <para /> 
    /// 1    3     6     0    0  <para />
    /// 1    4    10    20    0  <para />
    /// 1    5    15    35   70  <para />
    ///
    /// l / r  =           <para />
    /// (-0.0304,-0.0304) (0.4732,0.4732) (0.3036,0.3036) (0.1250,0.1250) (0.1286,0.1286) <para /> 
    ///	( 0.5607, 0.5607) (0.7536,0.7536) (0.3929,0.3929) (0.1500,0.1500) (0.1429,0.1429) <para />   
    /// </summary>
    /// <seealso cref="TestableComplexMatrixDoubleMatrixDivision{ComplexMatrixState}" />
    class RightIsLowerTriangularComplexMatrixDoubleMatrixDivision :
        TestableComplexMatrixDoubleMatrixDivision<ComplexMatrixState>
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[10]
            {
              -0.030357142857143,
               0.560714285714286,
               0.473214285714286,
               0.753571428571429,
               0.303571428571429,
               0.392857142857143,
               0.125000000000000,
               0.150000000000000,
               0.128571428571429,
               0.142857142857143
            };
            var asColumnMajorDenseArray = new Complex[10];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], d[i]);
            }
            return asColumnMajorDenseArray;
        }

        RightIsLowerTriangularComplexMatrixDoubleMatrixDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                    numberOfRows: 2,
                    numberOfColumns: 5),
                left: TestableComplexMatrix23.Get(),
                right: TestableDoubleMatrix24.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsLowerTriangularComplexMatrixDoubleMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsLowerTriangularComplexMatrixDoubleMatrixDivision"/> class.</returns>
        public static RightIsLowerTriangularComplexMatrixDoubleMatrixDivision Get()
        {
            return new RightIsLowerTriangularComplexMatrixDoubleMatrixDivision();
        }
    }
}
