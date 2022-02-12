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
    /// <seealso cref="TestableComplexMatrixDoubleMatrixDivision{ArgumentNullException}" />
    class RightIsNullComplexMatrixDoubleMatrixDivision :
        TestableComplexMatrixDoubleMatrixDivision<ArgumentNullException>
    {
        public RightIsNullComplexMatrixDoubleMatrixDivision() :
            base(
            expected: new ArgumentNullException(paramName: "right"),
            left: TestableComplexMatrix00.Get(),
            right: null
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNullComplexMatrixDoubleMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNullComplexMatrixDoubleMatrixDivision"/> class.</returns>
        public static RightIsNullComplexMatrixDoubleMatrixDivision Get()
        {
            return new RightIsNullComplexMatrixDoubleMatrixDivision();
        }
    }
}
