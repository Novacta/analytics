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
    /// 0  2  4         <para /> 
    /// 1  3  5         <para /> 
    ///
    /// r   =      <para /> 
    /// (2,2)       <para /> 
    ///
    /// l / r  =         <para />
    /// (0.00,-0.00)  (.50,-.50)  (1.00,-1.00)   <para /> 
    ///	(0.25,-0.25)  (.75,-.75)  (1.25,-1.25)   <para />       
    ///	</summary>
    class TypicalDoubleMatrixComplexScalarDivision :
        TestableDoubleMatrixComplexScalarDivision<ComplexMatrixState>
    {
        static Complex[] GetAsColumnMajorDenseArray()
        {
            var d = new double[6]
            {
                0,
                 .25,
                 .5,
                 .75,
                1,
                1.25
            };
            var asColumnMajorDenseArray = new Complex[6];

            for (int i = 0; i < asColumnMajorDenseArray.Length; i++)
            {
                asColumnMajorDenseArray[i] = new Complex(d[i], -d[i]);
            }
            return asColumnMajorDenseArray;
        }

        TypicalDoubleMatrixComplexScalarDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: GetAsColumnMajorDenseArray(),
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableDoubleMatrix16.Get(),
                right: new Complex(2, 2)
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalDoubleMatrixComplexScalarDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalDoubleMatrixComplexScalarDivision"/> class.</returns>
        public static TypicalDoubleMatrixComplexScalarDivision Get()
        {
            return new TypicalDoubleMatrixComplexScalarDivision();
        }
    }
}
