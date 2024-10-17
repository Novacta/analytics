// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Negation
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    /// o =                    <para />
    ///  0  4 -7          <para /> 
    /// -4  0  8          <para /> 
    ///  7 -8  0          <para />
    ///  
    /// -o  =        <para />
    ///  0 -4  7     <para /> 
    ///	 4  0 -8     <para />   
    /// -7  8  0     <para />
    ///  </summary>
    /// <seealso cref="TestableDoubleMatrixNegation{TExpected}{Novacta.Analytics.Tests.Tools.DoubleMatrixState}" />
    class TypicalDoubleMatrixNegation :
        TestableDoubleMatrixNegation<DoubleMatrixState>
    {
        TypicalDoubleMatrixNegation() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: [0, 4, -7, -4, 0, 8, 7, -8, 0],
                    numberOfRows: 3,
                    numberOfColumns: 3),
                operand: TestableDoubleMatrix15.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalDoubleMatrixNegation"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalDoubleMatrixNegation"/> class.</returns>
        public static TypicalDoubleMatrixNegation Get()
        {
            return new TypicalDoubleMatrixNegation();
        }
    }
}
