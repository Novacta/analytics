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
    /// (50,50)   (1,1)   ( 1, 1)   ( 1, 1)   ( 1, 1)  <para /> 
    /// (40,40)   (2,2)   ( 3, 3)   ( 4, 4)   ( 5, 5)  <para /> 
    /// (30,30)   (3,3)   ( 6, 6)   (10,10)   (15,15)  <para />
    ///
    /// l / r  =           <para />
    /// (-1.0185,1.0185)  (1.3479,-1.3479)  (-0.0831,0.0831) <para /> 
    ///	(-1.3945,1.3945)  (1.9358,-1.9358)  (-0.2236,0.2236) <para />   
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixComplexMatrixDivision{ComplexMatrixState}" />
    class RightIsRectangularLessRowsThanColsDoubleMatrixComplexMatrixDivision :
        TestableDoubleMatrixComplexMatrixDivision<ComplexMatrixState>
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[6]
            {
                -1.018471299810247,
                -1.394509013282733,
                 1.347940385831752,
                 1.935839658444025,
                -0.083115907653384,
                -0.223553130929792
            };
            var asColumnMajorDenseArray = new Complex[6];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], -d[i]);
            }
            return asColumnMajorDenseArray;
        }

        RightIsRectangularLessRowsThanColsDoubleMatrixComplexMatrixDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableDoubleMatrix23.Get(),
                right: TestableComplexMatrix29.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsRectangularLessRowsThanColsDoubleMatrixComplexMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsRectangularLessRowsThanColsDoubleMatrixComplexMatrixDivision"/> class.</returns>
        public static RightIsRectangularLessRowsThanColsDoubleMatrixComplexMatrixDivision Get()
        {
            return new RightIsRectangularLessRowsThanColsDoubleMatrixComplexMatrixDivision();
        }
    }
}
