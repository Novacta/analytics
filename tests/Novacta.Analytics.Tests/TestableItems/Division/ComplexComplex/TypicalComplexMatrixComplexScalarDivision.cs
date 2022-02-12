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
    /// (0,0)  (2,2)  (4,4)         <para /> 
    /// (1,1)  (3,3)  (5,5)         <para /> 
    ///
    /// r   =      <para /> 
    /// (2,2)       <para /> 
    ///
    /// l / r  =         <para />
    ///  0.0  1.0  2     <para /> 
    ///	 0.5  1.5  2.5   <para />       
    ///	</summary>
    class TypicalComplexMatrixComplexScalarDivision :
        TestableComplexMatrixComplexScalarDivision<ComplexMatrixState>
    {
        TypicalComplexMatrixComplexScalarDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[6] { 0, .5, 1, 1.5, 2, 2.5 },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableComplexMatrix16.Get(),
                right: new Complex(2, 2)
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalComplexMatrixComplexScalarDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalComplexMatrixComplexScalarDivision"/> class.</returns>
        public static TypicalComplexMatrixComplexScalarDivision Get()
        {
            return new TypicalComplexMatrixComplexScalarDivision();
        }
    }
}
