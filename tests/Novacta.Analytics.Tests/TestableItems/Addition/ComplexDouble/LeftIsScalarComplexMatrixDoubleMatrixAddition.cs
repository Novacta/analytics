// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Addition
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    /// l   =      <para /> 
    /// (-1,-1)       <para /> 
    /// 
    /// r =                    <para />
    /// 0  2  4            <para /> 
    /// 1  3  5            <para /> 
    ///
    /// l + r  =         <para />
    /// (-1,-1)  (1,-1)  (3,-1)   <para /> 
    ///	( 0,-1)  (2,-1)  (4,-1)   <para />       
    ///	</summary>
    class LeftIsScalarComplexMatrixDoubleMatrixAddition :
        TestableComplexMatrixDoubleMatrixAddition<ComplexMatrixState>
    {
        LeftIsScalarComplexMatrixDoubleMatrixAddition() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[6] 
                    { 
                        new Complex(-1, -1),
                        new Complex(0, -1),
                        new Complex(1, -1),
                        new Complex(2, -1),
                        new Complex(3, -1),
                        new Complex(4, -1),
                    },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableComplexMatrix17.Get(),
                right: TestableDoubleMatrix16.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsScalarComplexMatrixDoubleMatrixAddition"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsScalarComplexMatrixDoubleMatrixAddition"/> class.</returns>
        public static LeftIsScalarComplexMatrixDoubleMatrixAddition Get()
        {
            return new LeftIsScalarComplexMatrixDoubleMatrixAddition();
        }
    }

}
