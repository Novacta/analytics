// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Subtraction
{
    /// <summary>
    /// Represents a subtraction between matrices whose right operand is
    /// <b>null</b>.
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixDoubleMatrixSubtraction{ArgumentNullException}" />
    class RightIsNullDoubleMatrixDoubleMatrixSubtraction :
        TestableDoubleMatrixDoubleMatrixSubtraction<ArgumentNullException>
    {
        public RightIsNullDoubleMatrixDoubleMatrixSubtraction() :
            base(
            expected: new ArgumentNullException(paramName: "right"),
            left: TestableDoubleMatrix00.Get(),
            right: null
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNullDoubleMatrixDoubleMatrixSubtraction"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNullDoubleMatrixDoubleMatrixSubtraction"/> class.</returns>
        public static RightIsNullDoubleMatrixDoubleMatrixSubtraction Get()
        {
            return new RightIsNullDoubleMatrixDoubleMatrixSubtraction();
        }
    }
}
