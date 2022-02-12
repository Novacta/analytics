// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Subtraction
{
    /// <summary>
    /// Represents a subtraction between a scalar and a matrix 
    /// whose right operand is <b>null</b>.
    /// </summary>
    /// <seealso cref="TestableComplexMatrixComplexMatrixSubtraction{ArgumentNullException}" />
    class RightIsNullComplexScalarDoubleMatrixSubtraction :
        TestableComplexScalarDoubleMatrixSubtraction<ArgumentNullException>
    {
        public RightIsNullComplexScalarDoubleMatrixSubtraction() :
            base(
            expected: new ArgumentNullException(paramName: "right"),
            left: -1.0,
            right: null
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNullComplexScalarDoubleMatrixSubtraction"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNullComplexScalarDoubleMatrixSubtraction"/> class.</returns>
        public static RightIsNullComplexScalarDoubleMatrixSubtraction Get()
        {
            return new RightIsNullComplexScalarDoubleMatrixSubtraction();
        }
    }
}
