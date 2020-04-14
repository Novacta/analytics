// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents a testable binary operation.
    /// </summary>
    /// <typeparam name="Tleft">The type of the left operand.</typeparam>
    /// <typeparam name="TRight">The type of the right operand.</typeparam>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    class TestableBinaryOperation<Tleft, TRight, TExpected>
    {
        /// <summary>
        /// Gets the expected state of the operation result, if any;
        /// otherwise the expected exception.
        /// </summary>
        /// <value>The expected behavior of the operation.</value>
        public TExpected Expected { get; protected set; }

        /// <summary>
        /// Gets the left operand 
        /// of the operation.
        /// </summary>
        /// <value>The left operand.</value>
        public Tleft Left { get; protected set; }

        /// <summary>
        /// Gets the right operand 
        /// of the operation.
        /// </summary>
        /// <value>The right operand.</value>
        public TRight Right { get; protected set; }
    }
}
