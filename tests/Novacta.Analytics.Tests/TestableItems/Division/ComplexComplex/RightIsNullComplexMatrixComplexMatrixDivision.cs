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
    /// <seealso cref="TestableComplexMatrixComplexMatrixDivision{ArgumentNullException}" />
    class RightIsNullComplexMatrixComplexMatrixDivision :
        TestableComplexMatrixComplexMatrixDivision<ArgumentNullException>
    {
        public RightIsNullComplexMatrixComplexMatrixDivision() :
            base(
            expected: new ArgumentNullException(paramName: "right"),
            left: TestableComplexMatrix00.Get(),
            right: null
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNullComplexMatrixComplexMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNullComplexMatrixComplexMatrixDivision"/> class.</returns>
        public static RightIsNullComplexMatrixComplexMatrixDivision Get()
        {
            return new RightIsNullComplexMatrixComplexMatrixDivision();
        }
    }
}
