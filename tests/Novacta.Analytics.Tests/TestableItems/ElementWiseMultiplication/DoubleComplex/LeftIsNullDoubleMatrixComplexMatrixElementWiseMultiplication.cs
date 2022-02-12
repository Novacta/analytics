// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using System;

namespace Novacta.Analytics.Tests.TestableItems.ElementWiseMultiplication
{
    /// <summary>
    /// Represents an element-wise multiplication operation between matrices whose left operand is
    /// <b>null</b>.
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixComplexMatrixElementWiseMultiplication{ArgumentNullException}" />
    class LeftIsNullDoubleMatrixComplexMatrixElementWiseMultiplication :
        TestableDoubleMatrixComplexMatrixElementWiseMultiplication<ArgumentNullException>
    {
        LeftIsNullDoubleMatrixComplexMatrixElementWiseMultiplication() :
            base(
            expected: new ArgumentNullException(paramName: "left"),
            left: null,
            right: TestableComplexMatrix00.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsNullDoubleMatrixComplexMatrixElementWiseMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsNullDoubleMatrixComplexMatrixElementWiseMultiplication"/> class.</returns>
        public static LeftIsNullDoubleMatrixComplexMatrixElementWiseMultiplication Get()
        {
            return new LeftIsNullDoubleMatrixComplexMatrixElementWiseMultiplication();
        }
    }
}
