// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Negation
{
    /// <summary>
    /// Represents a testable negation of a matrix operand.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result.</typeparam>
    /// <seealso cref="Tools.TestableMatrixOperation{TExpected}" />
    class TestableMatrixNegation<TExpected> :
        TestableMatrixOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableMatrixNegation{TExpected}"/> class.
        /// </summary>
        /// <param name="expected">The expected result or exception.</param>
        /// <param name="operand">The operand.</param>
        public TestableMatrixNegation(
            TExpected expected,
            TestableDoubleMatrix operand) :
            base(
                expected,
                operand,
                operandWritableOps:
                    new Func<DoubleMatrix, DoubleMatrix>[2] {
                        (o) => -o,
                        (o) => DoubleMatrix.Negate(o)
                    },
                operandReadOnlyOps:
                    new Func<ReadOnlyDoubleMatrix, DoubleMatrix>[2] {
                        (o) => -o,
                        (o) => ReadOnlyDoubleMatrix.Negate(o)
                    }
                )
        {
        }
    }

}
