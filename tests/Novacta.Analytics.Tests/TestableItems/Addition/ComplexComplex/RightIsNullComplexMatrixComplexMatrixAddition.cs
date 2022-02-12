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
    /// <seealso cref="TestableComplexMatrixComplexMatrixAddition{ArgumentNullException}" />
    class RightIsNullComplexMatrixComplexMatrixAddition :
        TestableComplexMatrixComplexMatrixAddition<ArgumentNullException>
    {
        public RightIsNullComplexMatrixComplexMatrixAddition() :
            base(
            expected: new ArgumentNullException(paramName: "right"),
            left: TestableComplexMatrix00.Get(),
            right: null
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNullComplexMatrixComplexMatrixAddition"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNullComplexMatrixComplexMatrixAddition"/> class.</returns>
        public static RightIsNullComplexMatrixComplexMatrixAddition Get()
        {
            return new RightIsNullComplexMatrixComplexMatrixAddition();
        }
    }
}
