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
    /// <seealso cref="TestableMatrixMatrixDivision{ArgumentNullException}" />
    class RightIsNullMatrixMatrixDivision :
        TestableMatrixMatrixDivision<ArgumentNullException>
    {
        public RightIsNullMatrixMatrixDivision() :
            base(
            expected: new ArgumentNullException(paramName: "right"),
            left: TestableDoubleMatrix00.Get(),
            right: null
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNullMatrixMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNullMatrixMatrixDivision"/> class.</returns>
        public static RightIsNullMatrixMatrixDivision Get()
        {
            return new RightIsNullMatrixMatrixDivision();
        }
    }
}
