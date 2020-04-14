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
    /// <seealso cref="TestableMatrixMatrixMultiplication{ArgumentNullException}" />
    class LeftIsNullMatrixScalarMultiplication :
        TestableMatrixScalarMultiplication<ArgumentNullException>
    {
        public LeftIsNullMatrixScalarMultiplication() :
            base(
            expected: new ArgumentNullException(paramName: "left"),
            left: null,
            right: -1.0
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsNullMatrixScalarMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsNullMatrixScalarMultiplication"/> class.</returns>
        public static LeftIsNullMatrixScalarMultiplication Get()
        {
            return new LeftIsNullMatrixScalarMultiplication();
        }
    }
}
