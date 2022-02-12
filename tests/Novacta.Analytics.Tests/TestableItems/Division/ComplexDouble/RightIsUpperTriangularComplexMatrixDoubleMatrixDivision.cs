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
    /// (1,1)    (1,1)   (1,1)   ( 1, 1)   ( 1, 1)  <para /> 
    /// (0,0)    (2,2)   (3,3)   ( 4, 4)   ( 5, 5)  <para /> 
    /// (0,0)    (0,0)   (6,6)   (10,10)   (15,15)  <para />
    /// (0,0)    (0,0)   (0,0)   (20,20)   (35,35)  <para />
    /// (0,0)    (0,0)   (0,0)   ( 0, 0)   (70,70)  <para />
    /// 
    /// l / r  =           <para />
    /// (1,1)   (1,1)   (0.166,0.166)   (0.016,0.016)   (-0.0012,-0.0012) <para /> 
    ///	(2,2)   (1,1)   (0.166,0.166)   (0.016,0.016)   (-0.0012,-0.0012) <para />   
    /// </summary>
    /// <seealso cref="TestableComplexMatrixDoubleMatrixDivision{ComplexMatrixState}" />
    class RightIsUpperTriangularComplexMatrixDoubleMatrixDivision :
        TestableComplexMatrixDoubleMatrixDivision<ComplexMatrixState>
    {
        RightIsUpperTriangularComplexMatrixDoubleMatrixDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[10]
                    { 
                        new Complex(1, 1), 
                        new Complex(2, 2), 
                        new Complex(1, 1), 
                        new Complex(1, 1),
                        new Complex(0.1667, 0.1667), 
                        new Complex(0.1667, 0.1667), 
                        new Complex(0.0167, 0.0167), 
                        new Complex(0.0167, 0.0167),
                        new Complex(-0.0012, -0.0012), 
                        new Complex(-0.0012,  -0.0012)
                    },
                    numberOfRows: 2,
                    numberOfColumns: 5),
                left: TestableComplexMatrix23.Get(),
                right: TestableDoubleMatrix25.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsUpperTriangularComplexMatrixDoubleMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsUpperTriangularComplexMatrixDoubleMatrixDivision"/> class.</returns>
        public static RightIsUpperTriangularComplexMatrixDoubleMatrixDivision Get()
        {
            return new RightIsUpperTriangularComplexMatrixDoubleMatrixDivision();
        }
    }
}
