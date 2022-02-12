// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Division
{
    /// <summary>
    /// Represents a division operation between a matrix and a scalar 
    /// whose left operand is <b>null</b>.
    /// </summary>
    /// <seealso cref="TestableComplexMatrixDoubleMatrixDivision{ArgumentNullException}" />
    class LeftIsNullComplexMatrixDoubleScalarDivision :
        TestableComplexMatrixDoubleScalarDivision<ArgumentNullException>
    {
        public LeftIsNullComplexMatrixDoubleScalarDivision() :
            base(
            expected: new ArgumentNullException(paramName: "left"),
            left: null,
            right: -1.0
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsNullComplexMatrixDoubleScalarDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsNullComplexMatrixDoubleScalarDivision"/> class.</returns>
        public static LeftIsNullComplexMatrixDoubleScalarDivision Get()
        {
            return new LeftIsNullComplexMatrixDoubleScalarDivision();
        }
    }
}
