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
    /// <seealso cref="TestableComplexMatrixDoubleMatrixDivision{ArgumentNullException}" />
    class LeftIsNullComplexMatrixDoubleMatrixDivision :
        TestableComplexMatrixDoubleMatrixDivision<ArgumentNullException>
    {
        LeftIsNullComplexMatrixDoubleMatrixDivision() :
            base(
            expected: new ArgumentNullException(paramName: "left"),
            left: null,
            right: TestableDoubleMatrix00.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsNullComplexMatrixDoubleMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsNullComplexMatrixDoubleMatrixDivision"/> class.</returns>
        public static LeftIsNullComplexMatrixDoubleMatrixDivision Get()
        {
            return new LeftIsNullComplexMatrixDoubleMatrixDivision();
        }
    }
}
