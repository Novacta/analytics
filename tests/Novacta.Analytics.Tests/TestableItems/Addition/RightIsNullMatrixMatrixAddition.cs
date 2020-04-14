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
    /// <seealso cref="TestableMatrixMatrixAddition{ArgumentNullException}" />
    class RightIsNullMatrixMatrixAddition :
        TestableMatrixMatrixAddition<ArgumentNullException>
    {
        public RightIsNullMatrixMatrixAddition() :
            base(
            expected: new ArgumentNullException(paramName: "right"),
            left: TestableDoubleMatrix00.Get(),
            right: null
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNullMatrixMatrixAddition"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNullMatrixMatrixAddition"/> class.</returns>
        public static RightIsNullMatrixMatrixAddition Get()
        {
            return new RightIsNullMatrixMatrixAddition();
        }
    }
}
