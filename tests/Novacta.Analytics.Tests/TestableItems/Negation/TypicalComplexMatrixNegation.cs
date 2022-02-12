// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Negation
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    /// o =                         <para />
    /// 0       (4,4)  (-7,-7)       <para /> 
    /// (-4,-4)       0  (8,8)       <para /> 
    /// (7,7)  (-8,-8)       0           <para />
    ///  
    /// -o  =                      <para />
    /// 0       (-4,-4)  (7,7)     <para /> 
    ///	(4,4)       0  (-8,-8)     <para />   
    /// (-7,-7)  (8,8)       0     <para />
    ///  </summary>
    /// <seealso cref="TestableComplexMatrixNegation{TExpected}{Novacta.Analytics.Tests.Tools.ComplexMatrixState}" />
    class TypicalComplexMatrixNegation :
        TestableComplexMatrixNegation<ComplexMatrixState>
    {
        TypicalComplexMatrixNegation() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[9] { 
                        0,
                        new Complex(4, 4),
                        new Complex(-7, -7),
                        new Complex(-4, -4),
                        0,
                        new Complex(8, 8),
                        new Complex(7, 7),
                        new Complex(-8, -8), 
                        0 },
                    numberOfRows: 3,
                    numberOfColumns: 3),
                operand: TestableComplexMatrix15.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalComplexMatrixNegation"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalComplexMatrixNegation"/> class.</returns>
        public static TypicalComplexMatrixNegation Get()
        {
            return new TypicalComplexMatrixNegation();
        }
    }
}
