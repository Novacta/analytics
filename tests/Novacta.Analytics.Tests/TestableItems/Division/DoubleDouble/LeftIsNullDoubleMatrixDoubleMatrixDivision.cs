// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Division
{
    /// <summary>
    /// Represents a division operation between matrices whose left operand is
    /// <b>null</b>.
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixDoubleMatrixDivision{ArgumentNullException}" />
    class LeftIsNullDoubleMatrixDoubleMatrixDivision :
        TestableDoubleMatrixDoubleMatrixDivision<ArgumentNullException>
    {
        LeftIsNullDoubleMatrixDoubleMatrixDivision() :
            base(
            expected: new ArgumentNullException(paramName: "left"),
            left: null,
            right: TestableDoubleMatrix00.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsNullDoubleMatrixDoubleMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsNullDoubleMatrixDoubleMatrixDivision"/> class.</returns>
        public static LeftIsNullDoubleMatrixDoubleMatrixDivision Get()
        {
            return new LeftIsNullDoubleMatrixDoubleMatrixDivision();
        }
    }
}
