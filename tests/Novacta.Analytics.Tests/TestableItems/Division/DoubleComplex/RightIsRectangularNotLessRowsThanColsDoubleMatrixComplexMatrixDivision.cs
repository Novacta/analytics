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
    /// 1  3  5      <para /> 
    /// 2  4  6      <para /> 
    ///
    /// r   =       <para /> 
    /// (50,50)   (1,1)   ( 1, 1)    <para /> 
    /// (40,40)   (2,2)   ( 3, 3)    <para /> 
    /// (30,30)   (3,3)   ( 6, 6)    <para />
    /// (20,20)   (4,4)   (10,10)    <para />
    /// (10,10)   (5,5)   (15,15)    <para />
    /// 
    /// l / r  =           <para />
    ///    -0.6207    0.2429    0.5790    0.3879   -0.3307 <para /> 
    ///	   -0.9129    0.3714    0.8629    0.5614   -0.5329 <para />   
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixComplexMatrixDivision{ComplexMatrixState}" />
    class RightIsRectangularNotLessRowsThanColsDoubleMatrixComplexMatrixDivision :
        TestableDoubleMatrixComplexMatrixDivision<ComplexMatrixState>
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[10]
            {
                -0.620714286000000,
                -0.912857143000000,
                 0.242857143000000,
                 0.371428571000000,
                 0.579047619000000,
                 0.862857143000000,
                 0.387857143000000,
                 0.561428571000000,
                -0.330714286000000,
                -0.532857143000000
            };
            var asColumnMajorDenseArray = new Complex[10];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], -d[i]);
            }
            return asColumnMajorDenseArray;
        }

        RightIsRectangularNotLessRowsThanColsDoubleMatrixComplexMatrixDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                    numberOfRows: 2,
                    numberOfColumns: 5),
                left: TestableDoubleMatrix34.Get(),
                right: TestableComplexMatrix30.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsRectangularNotLessRowsThanColsDoubleMatrixComplexMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsRectangularNotLessRowsThanColsDoubleMatrixComplexMatrixDivision"/> class.</returns>
        public static RightIsRectangularNotLessRowsThanColsDoubleMatrixComplexMatrixDivision Get()
        {
            return new RightIsRectangularNotLessRowsThanColsDoubleMatrixComplexMatrixDivision();
        }
    }
}
