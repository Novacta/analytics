// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Multiplication
{
    /// <summary>
    /// Represents a multiplication operation between matrices whose left operand is
    /// <b>null</b>.
    /// </summary>
    /// <seealso cref="TestableComplexMatrixDoubleMatrixMultiplication{ArgumentNullException}" />
    class LeftIsNullComplexMatrixDoubleMatrixMultiplication :
        TestableComplexMatrixDoubleMatrixMultiplication<ArgumentNullException>
    {
        LeftIsNullComplexMatrixDoubleMatrixMultiplication() :
            base(
            expected: new ArgumentNullException(paramName: "left"),
            left: null,
            right: TestableDoubleMatrix00.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsNullComplexMatrixDoubleMatrixMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsNullComplexMatrixDoubleMatrixMultiplication"/> class.</returns>
        public static LeftIsNullComplexMatrixDoubleMatrixMultiplication Get()
        {
            return new LeftIsNullComplexMatrixDoubleMatrixMultiplication();
        }
    }
}
