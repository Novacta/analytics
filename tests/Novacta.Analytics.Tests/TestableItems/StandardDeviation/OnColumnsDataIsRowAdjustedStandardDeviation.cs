﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests.TestableItems.StandardDeviation
{
    /// <summary>
    /// Represents a standard deviation operation whose data operand 
    /// has not enough rows to enable adjusting for bias.
    /// </summary>
    class OnColumnsDataIsRowAdjustedStandardDeviation : AlongDimensionStandardDeviation<ArgumentException>
    {
        protected OnColumnsDataIsRowAdjustedStandardDeviation() :
            base(
                expected: new ArgumentException(
                    message: (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_STA_VARIANCE_ADJUST_FOR_BIAS_UNDEFINED" }),
                    paramName: null),
                data: TestableDoubleMatrix21.Get(),
                adjustForBias: true,
                dataOperation: DataOperation.OnColumns
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="OnColumnsDataIsRowAdjustedStandardDeviation"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="OnColumnsDataIsRowAdjustedStandardDeviation"/> class.</returns>
        public static OnColumnsDataIsRowAdjustedStandardDeviation Get()
        {
            return new OnColumnsDataIsRowAdjustedStandardDeviation();
        }
    }
}