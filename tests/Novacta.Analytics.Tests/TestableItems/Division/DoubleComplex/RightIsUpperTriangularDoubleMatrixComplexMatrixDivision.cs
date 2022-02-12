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
    /// 1  3  5  7   9   <para /> 
    /// 2  4  6  8  10   <para /> 
    ///
    /// r   =                  <para /> 
    /// (1,1)    (1,1)   (1,1)   ( 1, 1)   ( 1, 1)  <para /> 
    /// (0,0)    (2,2)   (3,3)   ( 4, 4)   ( 5, 5)  <para /> 
    /// (0,0)    (0,0)   (6,6)   (10,10)   (15,15)  <para />
    /// (0,0)    (0,0)   (0,0)   (20,20)   (35,35)  <para />
    /// (0,0)    (0,0)   (0,0)   ( 0, 0)   (70,70)  <para />
    /// 
    /// l / r  =           <para />
    /// (0.5,-0.5)   (0.5,-0.5)   (0.0833,-0.0833)   (0.0083,-0.0083)   (-0.0006,0.0006) <para /> 
    ///	(1.0,-1.0)   (0.5,-0.5)   (0.0833,-0.0833)   (0.0083,-0.0083)   (-0.0006,0.0006) <para />   
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixComplexMatrixDivision{ComplexMatrixState}" />
    class RightIsUpperTriangularDoubleMatrixComplexMatrixDivision :
        TestableDoubleMatrixComplexMatrixDivision<ComplexMatrixState>
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[10]
            {
                 0.500000000000000,
                 1.000000000000000,
                 0.500000000000000,
                 0.500000000000000,
                 0.083333333333333,
                 0.083333333333333,
                 0.008333333333333,
                 0.008333333333333,
                -0.000595238095238,
                -0.000595238095238
            };
            var asColumnMajorDenseArray = new Complex[10];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], -d[i]);
            }
            return asColumnMajorDenseArray;
        }

        RightIsUpperTriangularDoubleMatrixComplexMatrixDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                    numberOfRows: 2,
                    numberOfColumns: 5),
                left: TestableDoubleMatrix23.Get(),
                right: TestableComplexMatrix25.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsUpperTriangularDoubleMatrixComplexMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsUpperTriangularDoubleMatrixComplexMatrixDivision"/> class.</returns>
        public static RightIsUpperTriangularDoubleMatrixComplexMatrixDivision Get()
        {
            return new RightIsUpperTriangularDoubleMatrixComplexMatrixDivision();
        }
    }
}
