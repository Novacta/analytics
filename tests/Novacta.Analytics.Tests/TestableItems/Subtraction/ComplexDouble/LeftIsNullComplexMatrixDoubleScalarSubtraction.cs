// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Subtraction
{
    /// <summary>
    /// Represents a subtraction operation between a matrix and a scalar 
    /// whose left operand is <b>null</b>.
    /// </summary>
    /// <seealso cref="TestableComplexMatrixDoubleMatrixSubtraction{ArgumentNullException}" />
    class LeftIsNullComplexMatrixDoubleScalarSubtraction :
        TestableComplexMatrixDoubleScalarSubtraction<ArgumentNullException>
    {
        public LeftIsNullComplexMatrixDoubleScalarSubtraction() :
            base(
            expected: new ArgumentNullException(paramName: "left"),
            left: null,
            right: -1.0
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsNullComplexMatrixDoubleScalarSubtraction"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsNullComplexMatrixDoubleScalarSubtraction"/> class.</returns>
        public static LeftIsNullComplexMatrixDoubleScalarSubtraction Get()
        {
            return new LeftIsNullComplexMatrixDoubleScalarSubtraction();
        }
    }
}
