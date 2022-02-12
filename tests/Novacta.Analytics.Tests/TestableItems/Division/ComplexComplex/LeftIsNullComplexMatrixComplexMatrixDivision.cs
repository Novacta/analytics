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
    /// <seealso cref="TestableComplexMatrixComplexMatrixDivision{ArgumentNullException}" />
    class LeftIsNullComplexMatrixComplexMatrixDivision :
        TestableComplexMatrixComplexMatrixDivision<ArgumentNullException>
    {
        LeftIsNullComplexMatrixComplexMatrixDivision() :
            base(
            expected: new ArgumentNullException(paramName: "left"),
            left: null,
            right: TestableComplexMatrix00.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsNullComplexMatrixComplexMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsNullComplexMatrixComplexMatrixDivision"/> class.</returns>
        public static LeftIsNullComplexMatrixComplexMatrixDivision Get()
        {
            return new LeftIsNullComplexMatrixComplexMatrixDivision();
        }
    }
}
