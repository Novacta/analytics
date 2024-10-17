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
    ///
    /// l   =      <para /> 
    /// (10,10)         <para /> 
    ///
    /// r =                    <para />
    /// 0  2  4           <para /> 
    /// 1  3  5           <para /> 
    /// 
    /// l / r  =         <para />
    /// NaN       (5,         5         )  (2.5,2.5)   <para /> 
    ///	( 10,10)  (3.33333333,3.33333333)  (2.0,2.0)   <para />       
    ///	</summary>
    class TypicalComplexScalarDoubleMatrixDivision :
        TestableComplexScalarDoubleMatrixDivision<ComplexMatrixState>
    {
        TypicalComplexScalarDoubleMatrixDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: [ 
                        Complex.NaN,
                        new(10, 10),
                        new(5, 5),
                        new(3.3333333, 3.3333333),
                        new(2.5, 2.5),
                        new(2, 2)
                    ],
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: new Complex(10, 10),
                right: TestableDoubleMatrix16.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalComplexScalarDoubleMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalComplexScalarDoubleMatrixDivision"/> class.</returns>
        public static TypicalComplexScalarDoubleMatrixDivision Get()
        {
            return new TypicalComplexScalarDoubleMatrixDivision();
        }
    }
}
