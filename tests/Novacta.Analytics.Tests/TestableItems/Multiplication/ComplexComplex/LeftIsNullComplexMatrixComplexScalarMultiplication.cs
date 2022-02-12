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
    /// <seealso cref="TestableComplexMatrixComplexMatrixMultiplication{ArgumentNullException}" />
    class LeftIsNullComplexMatrixComplexScalarMultiplication :
        TestableComplexMatrixComplexScalarMultiplication<ArgumentNullException>
    {
        public LeftIsNullComplexMatrixComplexScalarMultiplication() :
            base(
            expected: new ArgumentNullException(paramName: "left"),
            left: null,
            right: -1.0
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsNullComplexMatrixComplexScalarMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsNullComplexMatrixComplexScalarMultiplication"/> class.</returns>
        public static LeftIsNullComplexMatrixComplexScalarMultiplication Get()
        {
            return new LeftIsNullComplexMatrixComplexScalarMultiplication();
        }
    }
}
