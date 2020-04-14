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
    /// <seealso cref="TestableMatrixMatrixMultiplication{ArgumentNullException}" />
    class RightIsNullScalarMatrixMultiplication :
        TestableScalarMatrixMultiplication<ArgumentNullException>
    {
        public RightIsNullScalarMatrixMultiplication() :
            base(
            expected: new ArgumentNullException(paramName: "right"),
            left: -1.0,
            right: null
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNullScalarMatrixMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNullScalarMatrixMultiplication"/> class.</returns>
        public static RightIsNullScalarMatrixMultiplication Get()
        {
            return new RightIsNullScalarMatrixMultiplication();
        }
    }
}
