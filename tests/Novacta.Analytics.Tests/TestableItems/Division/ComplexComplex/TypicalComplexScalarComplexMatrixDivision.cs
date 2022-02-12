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
    /// (0,0)  (2,2)  (4,4)          <para /> 
    /// (1,1)  (3,3)  (5,5)          <para /> 
    /// 
    /// l / r  =         <para />
    /// NaN        (5,         0)  (2.5,0)  <para /> 
    ///	( 10,  0)  (3.33333333,0)  (2.0,0)  <para />       
    ///	</summary>
    class TypicalComplexScalarComplexMatrixDivision :
        TestableComplexScalarComplexMatrixDivision<ComplexMatrixState>
    {
        TypicalComplexScalarComplexMatrixDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[6] 
                        { Complex.NaN, 10, 5, 3.3333333, 2.5, 2 },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: new Complex(10, 10),
                right: TestableComplexMatrix16.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalComplexScalarComplexMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalComplexScalarComplexMatrixDivision"/> class.</returns>
        public static TypicalComplexScalarComplexMatrixDivision Get()
        {
            return new TypicalComplexScalarComplexMatrixDivision();
        }
    }
}
