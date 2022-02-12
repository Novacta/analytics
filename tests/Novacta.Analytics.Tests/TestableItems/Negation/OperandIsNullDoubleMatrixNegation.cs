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
    /// <seealso cref="TestableDoubleMatrixNegation{ArgumentNullException}" />
    class OperandIsNullDoubleMatrixNegation :
        TestableDoubleMatrixNegation<ArgumentNullException>
    {
        OperandIsNullDoubleMatrixNegation() :
            base(
            expected: new ArgumentNullException(paramName: "matrix"),
            operand: null
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OperandIsNullDoubleMatrixNegation"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OperandIsNullDoubleMatrixNegation"/> class.</returns>
        public static OperandIsNullDoubleMatrixNegation Get()
        {
            return new OperandIsNullDoubleMatrixNegation();
        }
    }
}
