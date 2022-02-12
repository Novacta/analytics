// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Multiplication
{
    /// <summary>
    /// Represents a multiplication operation between matrices whose left operand is
    /// <b>null</b>.
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixDoubleMatrixMultiplication{ArgumentNullException}" />
    class LeftIsNullDoubleMatrixDoubleMatrixMultiplication :
        TestableDoubleMatrixDoubleMatrixMultiplication<ArgumentNullException>
    {
        LeftIsNullDoubleMatrixDoubleMatrixMultiplication() :
            base(
            expected: new ArgumentNullException(paramName: "left"),
            left: null,
            right: TestableDoubleMatrix00.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsNullDoubleMatrixDoubleMatrixMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsNullDoubleMatrixDoubleMatrixMultiplication"/> class.</returns>
        public static LeftIsNullDoubleMatrixDoubleMatrixMultiplication Get()
        {
            return new LeftIsNullDoubleMatrixDoubleMatrixMultiplication();
        }
    }
}
