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
    /// (0,0)    (1,1)   ( 1, 1)   ( 1, 1)   ( 1, 1)  <para /> 
    /// (1,1)    (2,2)   ( 3, 3)   ( 4, 4)   ( 5, 5)  <para /> 
    /// (1,1)    (3,3)   ( 6, 6)   (10,10)   (15,15)  <para />
    /// (1,1)    (4,4)   (10,10)   (20,20)   (35,35)  <para />
    /// (1,1)    (5,5)   (15,15)   (35,35)   (70,70)  <para />
    ///
    /// l / r  =           <para />
    /// 0.2500   -0.5000    2.5000   -1.2500    0.2500 <para /> 
    ///	0         2.0000         0         0         0 <para />   
    /// </summary>
    /// <seealso cref="TestableComplexMatrixComplexMatrixDivision{ComplexMatrixState}" />
    class RightIsSymmetricAndNonPosDefiniteComplexMatrixComplexMatrixDivision :
        TestableComplexMatrixComplexMatrixDivision<ComplexMatrixState>
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[10]
            {
                0.25, 0, -0.5, 2, 2.5, 0, -1.25, 0, 0.25, 0
            };
            var asColumnMajorDenseArray = new Complex[10];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], 0);
            }
            return asColumnMajorDenseArray;
        }

        RightIsSymmetricAndNonPosDefiniteComplexMatrixComplexMatrixDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                    numberOfRows: 2,
                    numberOfColumns: 5),
                left: TestableComplexMatrix23.Get(),
                right: TestableComplexMatrix31.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsSymmetricAndNonPosDefiniteComplexMatrixComplexMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsSymmetricAndNonPosDefiniteComplexMatrixComplexMatrixDivision"/> class.</returns>
        public static RightIsSymmetricAndNonPosDefiniteComplexMatrixComplexMatrixDivision Get()
        {
            return new RightIsSymmetricAndNonPosDefiniteComplexMatrixComplexMatrixDivision();
        }
    }
}
