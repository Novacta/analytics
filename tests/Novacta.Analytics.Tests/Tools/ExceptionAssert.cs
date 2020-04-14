// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Verifies conditions about expected exceptions in 
    /// unit tests using true/false propositions.
    /// </summary>
    static class ExceptionAssert
    {
        /// <summary>
        /// Determines whether the specified action throws an 
        /// expected exception.
        /// </summary>
        /// <param name="action">The action expected to
        /// throw an <see cref="Exception"/>.</param>
        /// <param name="expectedType">The expected type.</param>
        /// <param name="expectedMessage">The expected message.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Design", 
            "CA1031:Do not catch general exception types", 
            Justification = "By design: needed for testing purposes.")]
        public static void Throw(
            Action action,
            Type expectedType,
            string expectedMessage)
        {
            Assert.IsNotNull(expectedType);
            Assert.IsNotNull(expectedMessage);

            bool isThrown = false;
            string actualMessage = null;
            Type actualType = null;
            try {
                action();
            }
            catch (Exception e) {
                isThrown = true;
                actualType = e.GetType();
                actualMessage = e.Message;
            }

            Assert.IsTrue(isThrown,
                "An expected exception has not been thrown.");
            Assert.AreEqual(expectedMessage, actualMessage);
            Assert.AreEqual(expectedType, actualType);
        }

        /// <summary>
        /// Determines whether the specified action throws an 
        /// expected exception.
        /// </summary>
        /// <param name="action">The action expected to
        /// throw an <see cref="Exception"/>.</param>
        /// <param name="expectedType">The expected type.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Design",
            "CA1031:Do not catch general exception types",
            Justification = "By design: needed for testing purposes.")]
        public static void Throw(
            Action action,
            Type expectedType)
        {
            Assert.IsNotNull(expectedType);

            bool isThrown = false;
            Type actualType = null;
            try
            {
                action();
            }
            catch (Exception e)
            {
                isThrown = true;
                actualType = e.GetType();
            }

            Assert.IsTrue(isThrown,
                "An expected exception has not been thrown.");
            Assert.AreEqual(expectedType, actualType);
        }

        /// <summary>
        /// Determines whether the specified target throws an 
        /// expected exception caused by a given expected inner exception.
        /// </summary>
        /// <param name="action">The action expected to
        /// throw an <see cref="Exception"/>.</param>
        /// <param name="expectedType">The expected type.</param>
        /// <param name="expectedMessage">The expected message.</param>
        /// <param name="expectedInnerType">The expected inner type.</param>
        /// <param name="expectedInnerMessage">The expected inner message.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Design",
            "CA1031:Do not catch general exception types",
            Justification = "By design: needed for testing purposes.")]
        public static void Throw(
            Action action,
            Type expectedType,
            string expectedMessage,
            Type expectedInnerType,
            string expectedInnerMessage)
        {
            bool isThrown = false;
            bool isInnerThrown = false;
            string actualMessage = null;
            Type actualType = null;
            Type actualInnerType = null;
            string actualInnerMessage = null;
            try
            {
                action();
            }
            catch (Exception e)
            {
                isThrown = true;
                if (e.InnerException != null)
                {
                    isInnerThrown = true;
                    actualInnerMessage = e.InnerException.Message;
                    actualInnerType = e.InnerException.GetType();
                }
                actualType = e.GetType();
                actualMessage = e.Message;
            }

            Assert.IsTrue(isThrown,
                "An expected exception has not been thrown.");
            Assert.IsTrue(isInnerThrown,
                "An expected exception has not been caused by the expected inner one.");
            Assert.AreEqual(expectedMessage, actualMessage);
            Assert.AreEqual(expectedType, actualType);
            Assert.AreEqual(expectedInnerMessage, actualInnerMessage);
            Assert.AreEqual(expectedInnerType, actualInnerType);
        }

        /// <summary>
        /// Determines whether the specified target throws an 
        /// exception caused by a given expected inner exception.
        /// </summary>
        /// <param name="action">The action expected to
        /// throw an <see cref="Exception"/>.</param>
        /// <param name="expectedInnerType">The expected inner type.</param>
        /// <param name="expectedInnerMessage">The expected inner message.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Design",
            "CA1031:Do not catch general exception types",
            Justification = "By design: needed for testing purposes.")]
        public static void InnerThrow(
            Action action,
            Type expectedInnerType,
            string expectedInnerMessage)
        {
            bool isThrown = false;
            bool isInnerThrown = false;
            Type actualInnerType = null;
            string actualInnerMessage = null;
            try {
                action();
            }
            catch (Exception e) {
                isThrown = true;
                if (e.InnerException != null) {
                    isInnerThrown = true;
                    actualInnerMessage = e.InnerException.Message;
                    actualInnerType = e.InnerException.GetType();
                }
            }

            Assert.IsTrue(isThrown,
                "An expected exception has not been thrown.");
            Assert.IsTrue(isInnerThrown,
                "An expected exception has not been caused by the expected inner one.");
            Assert.AreEqual(expectedInnerMessage, actualInnerMessage);
            Assert.AreEqual(expectedInnerType, actualInnerType);
        }
    }
}
