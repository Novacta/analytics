// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Addition
{
    /// <summary>
    /// Represents an addition operation between matrices whose left operand is
    /// <b>null</b>.
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixDoubleMatrixAddition{ArgumentNullException}" />
    class LeftIsNullDoubleMatrixDoubleMatrixAddition :
        TestableDoubleMatrixDoubleMatrixAddition<ArgumentNullException>
    {
        LeftIsNullDoubleMatrixDoubleMatrixAddition() :
            base(
            expected: new ArgumentNullException(paramName: "left"),
            left: null,
            right: TestableDoubleMatrix00.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsNullDoubleMatrixDoubleMatrixAddition"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsNullDoubleMatrixDoubleMatrixAddition"/> class.</returns>
        public static LeftIsNullDoubleMatrixDoubleMatrixAddition Get()
        {
            return new LeftIsNullDoubleMatrixDoubleMatrixAddition();
        }
    }
}
