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
    /// (1,1)    (1,1)   ( 1, 1)   ( 1, 1)   ( 1, 1)  <para /> 
    /// (1,1)    (2,2)   ( 3, 3)   ( 4, 4)   ( 5, 5)  <para /> 
    /// (0,0)    (3,3)   ( 6, 6)   (10,10)   (15,15)  <para />
    /// (0,0)    (0,0)   (10,10)   (20,20)   (35,35)  <para />
    /// (0,0)    (0,0)   ( 0, 0)   (35,35)   (70,70)  <para />
    ///
    /// l / r  =           <para />
    /// -0.999	    1.999	4.246E-15	-2.151E-16	-1.517E-16 <para /> 
    ///	 6.260E-15  1.999	5.398E-15	-6.203E-16	-1.674E-16 <para />   
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixComplexMatrixDivision{ComplexMatrixState}" />
    class RightIsHessenbergDoubleMatrixComplexMatrixDivision :
        TestableDoubleMatrixComplexMatrixDivision<ComplexMatrixState>
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[10]
            {
                -0.5,
                0,
                1,
                1,
                0,
                0,
                0,
                0,
                0,
                0
            };
            var asColumnMajorDenseArray = new Complex[10];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], -d[i]);
            }
            return asColumnMajorDenseArray;
        }

        RightIsHessenbergDoubleMatrixComplexMatrixDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                    numberOfRows: 2,
                    numberOfColumns: 5),
                left: TestableDoubleMatrix23.Get(),
                right: TestableComplexMatrix27.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsHessenbergDoubleMatrixComplexMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsHessenbergDoubleMatrixComplexMatrixDivision"/> class.</returns>
        public static RightIsHessenbergDoubleMatrixComplexMatrixDivision Get()
        {
            return new RightIsHessenbergDoubleMatrixComplexMatrixDivision();
        }
    }
}
