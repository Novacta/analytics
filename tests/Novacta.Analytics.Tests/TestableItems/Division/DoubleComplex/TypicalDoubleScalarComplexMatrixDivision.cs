// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Division
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    ///
    /// l   =      <para /> 
    /// 10         <para /> 
    ///
    /// r =                    <para />
    /// (0,0)  (2,2)  (4,4)          <para /> 
    /// (1,1)  (3,3)  (5,5)          <para /> 
    /// 
    /// l / r  =         <para />
    /// NaN     (2.50,-2.50)  (1.25, 1.25)  <para /> 
    ///	(5,-5)  (1.66,-1.66)  (1.00,-1.00)  <para />       
    ///	</summary>
    class TypicalDoubleScalarComplexMatrixDivision :
        TestableDoubleScalarComplexMatrixDivision<ComplexMatrixState>
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[6]
            {
                Double.NaN,
                5,
                2.5,
                1.6667,
                1.25,
                1
            };
            var asColumnMajorDenseArray = new Complex[6];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], -d[i]);
            }
            return asColumnMajorDenseArray;
        }

        TypicalDoubleScalarComplexMatrixDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: 10,
                right: TestableComplexMatrix16.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalDoubleScalarComplexMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalDoubleScalarComplexMatrixDivision"/> class.</returns>
        public static TypicalDoubleScalarComplexMatrixDivision Get()
        {
            return new TypicalDoubleScalarComplexMatrixDivision();
        }
    }
}
