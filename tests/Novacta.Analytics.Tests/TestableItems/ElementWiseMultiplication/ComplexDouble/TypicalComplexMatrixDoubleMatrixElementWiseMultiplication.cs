// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.ElementWiseMultiplication
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    /// l =                    <para />
    /// (0,0)  (2,2)  (4,4)          <para /> 
    /// (1,1)  (3,3)  (5,5)          <para /> 
    ///
    /// r   =                  <para /> 
    /// -5  -3  -1       <para /> 
    /// -4  -2  -0       <para /> 
    ///
    /// ComplexMatrix.ElementWiseMultiply(l, r)  => <para />
    /// ( 0, 0)  (-6,-6)  (-4,-4)    <para /> 
    ///	(-4,-4)  (-6,-6)  ( 0, 0)    <para />   
    /// </summary>
    /// <seealso cref="TestableComplexMatrixDoubleMatrixElementWiseMultiplication{ComplexMatrixState}" />
    class TypicalComplexMatrixDoubleMatrixElementWiseMultiplication :
        TestableComplexMatrixDoubleMatrixElementWiseMultiplication<ComplexMatrixState>
    {
        TypicalComplexMatrixDoubleMatrixElementWiseMultiplication() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[6] 
                    { 
                        0, 
                        new Complex(-4, -4), 
                        new Complex(-6, -6), 
                        new Complex(-6, -6),
                        new Complex(-4, -4), 
                        0 
                    },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableComplexMatrix16.Get(),
                right: TestableDoubleMatrix18.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalComplexMatrixDoubleMatrixElementWiseMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalComplexMatrixDoubleMatrixElementWiseMultiplication"/> class.</returns>
        public static TypicalComplexMatrixDoubleMatrixElementWiseMultiplication Get()
        {
            return new TypicalComplexMatrixDoubleMatrixElementWiseMultiplication();
        }
    }
}
