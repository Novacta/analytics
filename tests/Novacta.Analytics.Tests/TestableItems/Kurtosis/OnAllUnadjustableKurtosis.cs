﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Kurtosis
{
    /// <summary>
    /// Represents a kurtosis operation whose data operand 
    /// has not enough items to enable adjusting for bias.
    /// </summary>
    class OnAllUnadjustableKurtosis : OnAllKurtosis<ArgumentException>
    {
        protected OnAllUnadjustableKurtosis() : 
            base(
                expected: new ArgumentException(
                    message: (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_STA_KURTOSIS_ADJUST_FOR_BIAS_UNDEFINED" }),
                    paramName: null), 
                data: TestableDoubleMatrix20.Get(), 
                adjustForBias: true
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnAllUnadjustableKurtosis"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnAllUnadjustableKurtosis"/> class.</returns>
        public static OnAllUnadjustableKurtosis Get()
        {
            return new OnAllUnadjustableKurtosis();
        }
    }
}
