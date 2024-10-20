﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Negation
{
    /// <summary>
    /// Represents a testable negation of a double matrix operand.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result.</typeparam>
    /// <seealso cref="Tools.TestableMatrixOperation{TExpected}" />
    class TestableDoubleMatrixNegation<TExpected> :
        TestableDoubleMatrixOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrixNegation{TExpected}"/> class.
        /// </summary>
        /// <param name="expected">The expected result or exception.</param>
        /// <param name="operand">The operand.</param>
        public TestableDoubleMatrixNegation(
            TExpected expected,
            TestableDoubleMatrix operand) :
            base(
                expected,
                operand,
                operandWritableOps:
                    [
                        (o) => -o,
                        (o) => DoubleMatrix.Negate(o)
                    ],
                operandReadOnlyOps:
                    [
                        (o) => -o,
                        (o) => ReadOnlyDoubleMatrix.Negate(o)
                    ]
                )
        {
        }
    }

}
