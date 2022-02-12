// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Division
{
    /// <summary>
    /// Represents a division between matrices whose right operand is
    /// <b>null</b>.
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixDoubleMatrixDivision{ArgumentNullException}" />
    class RightIsNullDoubleMatrixDoubleMatrixDivision :
        TestableDoubleMatrixDoubleMatrixDivision<ArgumentNullException>
    {
        public RightIsNullDoubleMatrixDoubleMatrixDivision() :
            base(
            expected: new ArgumentNullException(paramName: "right"),
            left: TestableDoubleMatrix00.Get(),
            right: null
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNullDoubleMatrixDoubleMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNullDoubleMatrixDoubleMatrixDivision"/> class.</returns>
        public static RightIsNullDoubleMatrixDoubleMatrixDivision Get()
        {
            return new RightIsNullDoubleMatrixDoubleMatrixDivision();
        }
    }
}
