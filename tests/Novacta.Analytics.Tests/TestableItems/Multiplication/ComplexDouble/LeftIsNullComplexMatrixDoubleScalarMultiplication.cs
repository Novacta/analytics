// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Multiplication
{
    /// <summary>
    /// Represents a multiplication operation between a matrix and a scalar 
    /// whose left operand is <b>null</b>.
    /// </summary>
    /// <seealso cref="TestableComplexMatrixDoubleMatrixMultiplication{ArgumentNullException}" />
    class LeftIsNullComplexMatrixDoubleScalarMultiplication :
        TestableComplexMatrixDoubleScalarMultiplication<ArgumentNullException>
    {
        public LeftIsNullComplexMatrixDoubleScalarMultiplication() :
            base(
            expected: new ArgumentNullException(paramName: "left"),
            left: null,
            right: -1.0
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsNullComplexMatrixDoubleScalarMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsNullComplexMatrixDoubleScalarMultiplication"/> class.</returns>
        public static LeftIsNullComplexMatrixDoubleScalarMultiplication Get()
        {
            return new LeftIsNullComplexMatrixDoubleScalarMultiplication();
        }
    }
}
