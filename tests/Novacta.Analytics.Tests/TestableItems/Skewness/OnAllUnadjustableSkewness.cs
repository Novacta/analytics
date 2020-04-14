// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Skewness
{
    /// <summary>
    /// Represents a skewness operation whose data operand 
    /// has not enough items to enable adjusting for bias.
    /// </summary>
    class OnAllUnadjustableSkewness : OnAllSkewness<ArgumentException>
    {
        protected OnAllUnadjustableSkewness() : 
            base(
                expected: new ArgumentException(
                    message: (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_STA_SKEWNESS_ADJUST_FOR_BIAS_UNDEFINED" }),
                    paramName: null), 
                data: TestableDoubleMatrix43.Get(), 
                adjustForBias: true
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllUnadjustableSkewness"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllUnadjustableSkewness"/> class.</returns>
        public static OnAllUnadjustableSkewness Get()
        {
            return new OnAllUnadjustableSkewness();
        }
    }
}
