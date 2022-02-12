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
    /// 1  3  5  7   9    <para /> 
    /// 2  4  6  8  10    <para /> 
    ///
    /// r   =                  <para /> 
    /// (1,1)    (0,0)    ( 0, 0)    ( 0, 0)   ( 0, 0)  <para /> 
    /// (1,1)    (2,2)    ( 0, 0)    ( 0, 0)   ( 0, 0)  <para /> 
    /// (1,1)    (3,3)    ( 6, 6)    ( 0, 0)   ( 0, 0)  <para />
    /// (1,1)    (4,4)    (10,10)    (20,20)   ( 0, 0)  <para />
    /// (1,1)    (5,5)    (15,15)    (35,35)   (70,70)  <para />
    ///
    /// l / r  =           <para />
    /// (-0.0152, 0.0152)  (0.2366,-0.2366)  (0.1518,-0.1518)  (0.0625,-0.0625)  (0.0643,-0.0643) <para /> 
    ///	( 0.2804,-0.2804)  (0.3768,-0.3768)  (0.1964,-0.1964)  (0.0750,-0.0750)  (0.0714,-0.0714) <para />   
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixComplexMatrixDivision{ComplexMatrixState}" />
    class RightIsLowerTriangularDoubleMatrixComplexMatrixDivision :
        TestableDoubleMatrixComplexMatrixDivision<ComplexMatrixState>
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[10]
            {
                -0.015178571428571,
                0.280357142857143,
                0.236607142857143,
                0.376785714285714,
                0.151785714285714,
                0.196428571428571,
                0.062500000000000,
                0.075000000000000,
                0.064285714285714,
                0.071428571428571
            };
            var asColumnMajorDenseArray = new Complex[10];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], -d[i]);
            }
            return asColumnMajorDenseArray;
        }

        RightIsLowerTriangularDoubleMatrixComplexMatrixDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                    numberOfRows: 2,
                    numberOfColumns: 5),
                left: TestableDoubleMatrix23.Get(),
                right: TestableComplexMatrix24.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsLowerTriangularDoubleMatrixComplexMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsLowerTriangularDoubleMatrixComplexMatrixDivision"/> class.</returns>
        public static RightIsLowerTriangularDoubleMatrixComplexMatrixDivision Get()
        {
            return new RightIsLowerTriangularDoubleMatrixComplexMatrixDivision();
        }
    }
}
