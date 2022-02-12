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
    class LeftIsNullComplexMatrixComplexMatrixAddition :
        TestableComplexMatrixComplexMatrixAddition<ArgumentNullException>
    {
        LeftIsNullComplexMatrixComplexMatrixAddition() :
            base(
            expected: new ArgumentNullException(paramName: "left"),
            left: null,
            right: TestableComplexMatrix00.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsNullComplexMatrixComplexMatrixAddition"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsNullComplexMatrixComplexMatrixAddition"/> class.</returns>
        public static LeftIsNullComplexMatrixComplexMatrixAddition Get()
        {
            return new LeftIsNullComplexMatrixComplexMatrixAddition();
        }
    }
}
