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
    class RightIsNullDoubleMatrixComplexMatrixAddition :
        TestableDoubleMatrixComplexMatrixAddition<ArgumentNullException>
    {
        public RightIsNullDoubleMatrixComplexMatrixAddition() :
            base(
            expected: new ArgumentNullException(paramName: "right"),
            left: TestableDoubleMatrix00.Get(),
            right: null
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNullDoubleMatrixComplexMatrixAddition"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNullDoubleMatrixComplexMatrixAddition"/> class.</returns>
        public static RightIsNullDoubleMatrixComplexMatrixAddition Get()
        {
            return new RightIsNullDoubleMatrixComplexMatrixAddition();
        }
    }
}
