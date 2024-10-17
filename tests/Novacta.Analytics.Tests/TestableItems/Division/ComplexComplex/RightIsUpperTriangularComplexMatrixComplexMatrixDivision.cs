// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

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
    /// 1.0000    1.0000    0.1667    0.0167   -0.0012 <para /> 
    ///	2.0000    1.0000    0.1667    0.0167   -0.0012 <para />   
    /// </summary>
    /// <seealso cref="TestableComplexMatrixComplexMatrixDivision{ComplexMatrixState}" />
    class RightIsUpperTriangularComplexMatrixComplexMatrixDivision :
        TestableComplexMatrixComplexMatrixDivision<ComplexMatrixState>
    {
        RightIsUpperTriangularComplexMatrixComplexMatrixDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: [1, 2, 1, 1, 0.1667, 0.1667, 0.0167, 0.0167, -0.0012, -0.0012],
                    numberOfRows: 2,
                    numberOfColumns: 5),
                left: TestableComplexMatrix23.Get(),
                right: TestableComplexMatrix25.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsUpperTriangularComplexMatrixComplexMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsUpperTriangularComplexMatrixComplexMatrixDivision"/> class.</returns>
        public static RightIsUpperTriangularComplexMatrixComplexMatrixDivision Get()
        {
            return new RightIsUpperTriangularComplexMatrixComplexMatrixDivision();
        }
    }
}
