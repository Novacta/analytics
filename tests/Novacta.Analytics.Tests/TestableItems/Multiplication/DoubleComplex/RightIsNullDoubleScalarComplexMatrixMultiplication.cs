// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Multiplication
{
    /// <summary>
    /// Represents a multiplication between a scalar and a matrix 
    /// whose right operand is <b>null</b>.
    /// </summary>
    /// <seealso cref="TestableComplexMatrixComplexMatrixMultiplication{ArgumentNullException}" />
    class RightIsNullDoubleScalarComplexMatrixMultiplication :
        TestableDoubleScalarComplexMatrixMultiplication<ArgumentNullException>
    {
        public RightIsNullDoubleScalarComplexMatrixMultiplication() :
            base(
            expected: new ArgumentNullException(paramName: "right"),
            left: -1.0,
            right: null
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNullDoubleScalarComplexMatrixMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNullDoubleScalarComplexMatrixMultiplication"/> class.</returns>
        public static RightIsNullDoubleScalarComplexMatrixMultiplication Get()
        {
            return new RightIsNullDoubleScalarComplexMatrixMultiplication();
        }
    }
}
