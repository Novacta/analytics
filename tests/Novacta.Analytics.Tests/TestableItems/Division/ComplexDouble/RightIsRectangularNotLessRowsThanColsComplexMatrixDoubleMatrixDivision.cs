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
    /// l =          <para />
    /// (1,1)  (3,3)  (5,5)      <para /> 
    /// (2,2)  (4,4)  (6,6)      <para /> 
    ///
    /// r   =       <para /> 
    /// 50   1    1    <para /> 
    /// 40   2    3    <para /> 
    /// 30   3    6    <para />
    /// 20   4   10    <para />
    /// 10   5   15    <para />
    /// 
    /// l / r  =           <para />
    /// (-1.2414,-1.2414) (0.4857,0.4857) (1.1581,1.1581) (0.7757,0.7757) (-0.6614,-0.6614) <para /> 
    ///	(-1.8257,-1.8257) (0.7429,0.7429) (1.7257,1.7257) (1.1229,1.1229) (-1.0657,-1.0657) <para />   
    /// </summary>
    /// <seealso cref="TestableComplexMatrixDoubleMatrixDivision{ComplexMatrixState}" />
    class RightIsRectangularNotLessRowsThanColsComplexMatrixDoubleMatrixDivision :
        TestableComplexMatrixDoubleMatrixDivision<ComplexMatrixState>
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[10]
            {
                -1.241428570000000,
                -1.825714290000000,
                 0.485714286000000,
                 0.742857143000000,
                 1.158095240000000,
                 1.725714290000000,
                 0.775714286000000,
                 1.122857140000000,
                -0.661428571000000,
                -1.065714290000000 
            };
            var asColumnMajorDenseArray = new Complex[10];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], d[i]);
            }
            return asColumnMajorDenseArray;
        }

        RightIsRectangularNotLessRowsThanColsComplexMatrixDoubleMatrixDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                    numberOfRows: 2,
                    numberOfColumns: 5),
                left: TestableComplexMatrix34.Get(),
                right: TestableDoubleMatrix30.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsRectangularNotLessRowsThanColsComplexMatrixDoubleMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsRectangularNotLessRowsThanColsComplexMatrixDoubleMatrixDivision"/> class.</returns>
        public static RightIsRectangularNotLessRowsThanColsComplexMatrixDoubleMatrixDivision Get()
        {
            return new RightIsRectangularNotLessRowsThanColsComplexMatrixDoubleMatrixDivision();
        }
    }
}
