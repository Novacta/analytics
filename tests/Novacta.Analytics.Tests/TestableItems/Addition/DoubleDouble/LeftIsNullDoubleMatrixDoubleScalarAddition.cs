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
    /// <seealso cref="TestableDoubleMatrixDoubleMatrixAddition{ArgumentNullException}" />
    class LeftIsNullDoubleMatrixDoubleScalarAddition :
        TestableDoubleMatrixDoubleScalarAddition<ArgumentNullException>
    {
        public LeftIsNullDoubleMatrixDoubleScalarAddition() :
            base(
            expected: new ArgumentNullException(paramName: "left"),
            left: null,
            right: -1.0
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsNullDoubleMatrixDoubleScalarAddition"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsNullDoubleMatrixDoubleScalarAddition"/> class.</returns>
        public static LeftIsNullDoubleMatrixDoubleScalarAddition Get()
        {
            return new LeftIsNullDoubleMatrixDoubleScalarAddition();
        }
    }
}
