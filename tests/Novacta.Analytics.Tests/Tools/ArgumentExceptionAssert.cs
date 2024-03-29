﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Verifies conditions about expected argument exceptions in 
    /// unit tests using true/false propositions.
    /// </summary>
    static class ArgumentExceptionAssert
    {
        /// <summary>
        /// Represents the partial message corresponding to
        /// an <see cref="ArgumentNullException"/> instance.
        /// </summary>
        public static string NullPartialMessage = "Value cannot be null.";

        /// <summary>
        /// Determines whether the specified target throws an expected 
        /// argument exception.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This method joins together <paramref name="expectedPartialMessage"/> and
        /// <paramref name="expectedParameterName"/> to encode the 
        /// message returned by the exception.
        /// </para>
        /// </remarks>
        /// <param name="action">The action expected to throw
        /// an <see cref="ArgumentException"/>.</param>
        /// <param name="expectedType">The expected type.</param>
        /// <param name="expectedPartialMessage">The expected partial message, without 
        /// mentioning the parameter name.</param>
        /// <param name="expectedParameterName">The expected parameter name.</param>
        public static void Throw(
            Action action,
            Type expectedType,
            string expectedPartialMessage,
            string expectedParameterName
            )
        {
            Assert.IsNotNull(expectedType);
            Assert.IsNotNull(expectedPartialMessage);

            var expectedMessage = expectedPartialMessage;

            if (expectedParameterName is not null)
            {
                expectedMessage +=
                    " (Parameter '" + expectedParameterName + "')";

                //expectedMessage += 
                //    Environment.NewLine + "Parameter name: " + expectedParameterName;
            }

            bool isThrown = false;
            string actualMessage = null;
            Type actualType = null;
            string actualParameter = null;

            try {
                action();
            }
            catch (ArgumentException e) {
                isThrown = true;
                actualType = e.GetType();
                actualMessage = e.Message;
                actualParameter = e.ParamName;
            }

            Assert.IsTrue(isThrown,
                "An expected exception has not been thrown.");
            Assert.AreEqual(expectedMessage, actualMessage);
            Assert.AreEqual(expectedType, actualType);
            Assert.AreEqual(expectedParameterName, actualParameter);
        }
    }
}
