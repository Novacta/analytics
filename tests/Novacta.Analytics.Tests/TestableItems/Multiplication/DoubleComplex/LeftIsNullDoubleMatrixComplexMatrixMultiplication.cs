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
    /// <seealso cref="TestableDoubleMatrixComplexMatrixMultiplication{ArgumentNullException}" />
    class LeftIsNullDoubleMatrixComplexMatrixMultiplication :
        TestableDoubleMatrixComplexMatrixMultiplication<ArgumentNullException>
    {
        LeftIsNullDoubleMatrixComplexMatrixMultiplication() :
            base(
            expected: new ArgumentNullException(paramName: "left"),
            left: null,
            right: TestableComplexMatrix00.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsNullDoubleMatrixComplexMatrixMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsNullDoubleMatrixComplexMatrixMultiplication"/> class.</returns>
        public static LeftIsNullDoubleMatrixComplexMatrixMultiplication Get()
        {
            return new LeftIsNullDoubleMatrixComplexMatrixMultiplication();
        }
    }
}
