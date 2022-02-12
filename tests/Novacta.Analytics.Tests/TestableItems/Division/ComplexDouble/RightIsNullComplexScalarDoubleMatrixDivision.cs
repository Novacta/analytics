// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Division
{
    /// <summary>
    /// Represents a division between a scalar and a matrix 
    /// whose right operand is <b>null</b>.
    /// </summary>
    /// <seealso cref="TestableComplexMatrixComplexMatrixDivision{ArgumentNullException}" />
    class RightIsNullComplexScalarDoubleMatrixDivision :
        TestableComplexScalarDoubleMatrixDivision<ArgumentNullException>
    {
        public RightIsNullComplexScalarDoubleMatrixDivision() :
            base(
            expected: new ArgumentNullException(paramName: "right"),
            left: -1.0,
            right: null
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNullComplexScalarDoubleMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNullComplexScalarDoubleMatrixDivision"/> class.</returns>
        public static RightIsNullComplexScalarDoubleMatrixDivision Get()
        {
            return new RightIsNullComplexScalarDoubleMatrixDivision();
        }
    }
}
