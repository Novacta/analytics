// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Addition
{
    /// <summary>
    /// Represents an addition operation between a matrix and a scalar 
    /// whose left operand is <b>null</b>.
    /// </summary>
    class LeftIsNullDoubleMatrixComplexScalarAddition :
        TestableDoubleMatrixComplexScalarAddition<ArgumentNullException>
    {
        public LeftIsNullDoubleMatrixComplexScalarAddition() :
            base(
            expected: new ArgumentNullException(paramName: "left"),
            left: null,
            right: -1.0
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsNullDoubleMatrixComplexScalarAddition"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsNullDoubleMatrixComplexScalarAddition"/> class.</returns>
        public static LeftIsNullDoubleMatrixComplexScalarAddition Get()
        {
            return new LeftIsNullDoubleMatrixComplexScalarAddition();
        }
    }
}
