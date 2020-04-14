// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Tests.Tools;
using System;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class RandomDeviceTests
    {
        [TestMethod()]
        public void SetRandomNumberGeneratorTest()
        {
            // Valid input
            {
                var distribution = GaussianDistribution.Standard();

                distribution.RandomNumberGenerator =
                    RandomNumberGenerator.CreateNextMT2203(77777);

                int sampleSize = 10000;
                var sample = distribution.Sample(sampleSize);

                RandomNumberGeneratorTest.CheckChebyshevInequality(
                    distributionMean: 0.0,
                    distributionVariance: 1.0,
                    sampleMean: Stat.Mean(sample),
                    sampleSize: sampleSize,
                    delta: .01);
            }

            // value is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        GaussianDistribution.Standard()
                            .RandomNumberGenerator = null;
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "value");
            }
        }
    }
}
