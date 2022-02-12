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
    /// 1    1    1    1    1  <para /> 
    /// 1    2    3    4    5  <para /> 
    /// 0    3    6   10   15  <para />
    /// 0    0   10   20   35  <para />
    /// 0    0    0   35   70  <para />
    ///
    /// l / r  =           <para />
    /// (-1,-1) (2,2) (0,0) (0,0) (0,0) <para /> 
    ///	( 0, 0) (2,2) (0,0) (0,0) (0,0) <para />   
    /// </summary>
    /// <seealso cref="TestableComplexMatrixDoubleMatrixDivision{ComplexMatrixState}" />
    class RightIsHessenbergComplexMatrixDoubleMatrixDivision :
        TestableComplexMatrixDoubleMatrixDivision<ComplexMatrixState>
    {
        RightIsHessenbergComplexMatrixDoubleMatrixDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[10]
                    { 
                        new Complex(-1, -1),
                        0,
                        new Complex(2, 2),
                        new Complex(2, 2),
                        0,
                        0,
                        0,
                        0,
                        0,
                        0
                    },
                    numberOfRows: 2,
                    numberOfColumns: 5),
                left: TestableComplexMatrix23.Get(),
                right: TestableDoubleMatrix27.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsHessenbergComplexMatrixDoubleMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsHessenbergComplexMatrixDoubleMatrixDivision"/> class.</returns>
        public static RightIsHessenbergComplexMatrixDoubleMatrixDivision Get()
        {
            return new RightIsHessenbergComplexMatrixDoubleMatrixDivision();
        }
    }
}
