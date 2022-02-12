// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Addition
{
    /// <summary>
    /// Represents an addition between matrices whose right operand is
    /// <b>null</b>.
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixDoubleMatrixAddition{ArgumentNullException}" />
    class RightIsNullDoubleMatrixDoubleMatrixAddition :
        TestableDoubleMatrixDoubleMatrixAddition<ArgumentNullException>
    {
        public RightIsNullDoubleMatrixDoubleMatrixAddition() :
            base(
            expected: new ArgumentNullException(paramName: "right"),
            left: TestableDoubleMatrix00.Get(),
            right: null
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNullDoubleMatrixDoubleMatrixAddition"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNullDoubleMatrixDoubleMatrixAddition"/> class.</returns>
        public static RightIsNullDoubleMatrixDoubleMatrixAddition Get()
        {
            return new RightIsNullDoubleMatrixDoubleMatrixAddition();
        }
    }
}
