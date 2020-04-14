// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Multiplication
{
    /// <summary>
    /// Represents a multiplication between matrices whose right operand is
    /// <b>null</b>.
    /// </summary>
    /// <seealso cref="TestableMatrixMatrixMultiplication{ArgumentNullException}" />
    class RightIsNullMatrixMatrixMultiplication :
        TestableMatrixMatrixMultiplication<ArgumentNullException>
    {
        public RightIsNullMatrixMatrixMultiplication() :
            base(
            expected: new ArgumentNullException(paramName: "right"),
            left: TestableDoubleMatrix00.Get(),
            right: null
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNullMatrixMatrixMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNullMatrixMatrixMultiplication"/> class.</returns>
        public static RightIsNullMatrixMatrixMultiplication Get()
        {
            return new RightIsNullMatrixMatrixMultiplication();
        }
    }
}
