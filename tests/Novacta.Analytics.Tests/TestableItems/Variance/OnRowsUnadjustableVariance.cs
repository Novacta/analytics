﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests.TestableItems.Variance
{
    /// <summary>
    /// Represents a variance operation whose data operand 
    /// has not enough items to enable adjusting for bias.
    /// </summary>
    class OnRowsUnadjustableVariance : AlongDimensionVariance<ArgumentException>
    {
        protected OnRowsUnadjustableVariance() :
            base(
                expected: new ArgumentException(
                    message: (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_STA_VARIANCE_ADJUST_FOR_BIAS_UNDEFINED" }),
                    paramName: null),
                data: TestableDoubleMatrix20.Get(),
                adjustForBias: true,
                dataOperation: DataOperation.OnRows
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnRowsUnadjustableVariance"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnRowsUnadjustableVariance"/> class.</returns>
        public static OnRowsUnadjustableVariance Get()
        {
            return new OnRowsUnadjustableVariance();
        }
    }
}
