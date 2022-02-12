// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Negation
{
    /// <summary>
    /// Represents a negation operation whose operand is
    /// <b>null</b>.
    /// </summary>
    /// <seealso cref="TestableComplexMatrixNegation{ArgumentNullException}" />
    class OperandIsNullComplexMatrixNegation :
        TestableComplexMatrixNegation<ArgumentNullException>
    {
        OperandIsNullComplexMatrixNegation() :
            base(
            expected: new ArgumentNullException(paramName: "matrix"),
            operand: null
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OperandIsNullComplexMatrixNegation"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OperandIsNullComplexMatrixNegation"/> class.</returns>
        public static OperandIsNullComplexMatrixNegation Get()
        {
            return new OperandIsNullComplexMatrixNegation();
        }
    }
}
