// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Variance
{
    /// <summary>
    /// Represents a variance operation whose data operand is
    /// scalar.
    /// </summary>
    class OnAllDataIsScalarAdjustedVariance : OnAllVariance<ArgumentException>
    {
        protected OnAllDataIsScalarAdjustedVariance() :
            base(
                expected: new ArgumentException(
                    message: (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_STA_VARIANCE_ADJUST_FOR_BIAS_UNDEFINED" }),
                    paramName: null),
                data: TestableDoubleMatrix19.Get(),
                adjustForBias: true
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllDataIsScalarAdjustedVariance"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllDataIsScalarAdjustedVariance"/> class.</returns>
        public static OnAllDataIsScalarAdjustedVariance Get()
        {
            return new OnAllDataIsScalarAdjustedVariance();
        }
    }
}
