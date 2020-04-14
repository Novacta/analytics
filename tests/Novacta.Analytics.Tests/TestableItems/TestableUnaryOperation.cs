// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents a testable unary operation.
    /// </summary>
    /// <typeparam name="TOperand">The type of the operand.</typeparam>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    class TestableUnaryOperation<TOperand, TExpected>
    {
        /// <summary>
        /// Gets the expected state of the operation result, if any;
        /// otherwise the expected exception.
        /// </summary>
        /// <value>The expected behavior of the operation.</value>
        public TExpected Expected { get; protected set; }

        /// <summary>
        /// Gets the operand 
        /// of the operation.
        /// </summary>
        /// <value>The operand.</value>
        public TOperand Operand { get; protected set; }
    }
}
