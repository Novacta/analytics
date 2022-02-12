// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Addition
{
    /// <summary>
    /// Represents an addition between a scalar and a matrix 
    /// whose right operand is <b>null</b>.
    /// </summary>
    /// <seealso cref="TestableComplexMatrixComplexMatrixAddition{ArgumentNullException}" />
    class RightIsNullComplexScalarDoubleMatrixAddition :
        TestableComplexScalarDoubleMatrixAddition<ArgumentNullException>
    {
        public RightIsNullComplexScalarDoubleMatrixAddition() :
            base(
            expected: new ArgumentNullException(paramName: "right"),
            left: -1.0,
            right: null
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNullComplexScalarDoubleMatrixAddition"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNullComplexScalarDoubleMatrixAddition"/> class.</returns>
        public static RightIsNullComplexScalarDoubleMatrixAddition Get()
        {
            return new RightIsNullComplexScalarDoubleMatrixAddition();
        }
    }
}
