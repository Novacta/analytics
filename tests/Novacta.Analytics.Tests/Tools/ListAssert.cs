// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Verifies conditions about lists in 
    /// unit tests using true/false propositions.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the elements in the lists.
    /// </typeparam>
    static class ListAssert<T>
    {
        /// <summary>
        /// Checks that the specified lists 
        /// contain the same items, irrespective of the corresponding
        /// positions or of different item multiplicities.
        /// </summary>
        /// <param name="expected">The list containing the expected items.</param>
        /// <param name="actual">The list containing the actual items.</param>
        /// <param name="areEqual">A method that asserts that two items
        /// are equal, and throws an <see cref="AssertFailedException"/> if
        /// they are not equal.</param>
        public static void ContainSameItems(
            IList<T> expected,
            IList<T> actual,
            Action<T, T> areEqual)
        {
            if (null == expected && null == actual)
                return;

            if (((null == expected) && (null != actual))
                ||
                ((null != expected) && (null == actual)))
                throw new AssertFailedException(
                    "One List instance is null, the other is not.");

            if (expected.Count == 0)
            {
                if (actual.Count != 0)
                {
                    throw new AssertFailedException(
                        "The expected instance is empty, the other is not.");
                }
            }
            else
            {
                if (actual.Count == 0)
                {
                    throw new AssertFailedException(
                        "The expected instance is nonempty, the other is not.");
                }
                else
                {
                    // Check that expected is a subset of actual
                    var uncheckedActualPositions =
                        IndexCollection.Default(actual.Count - 1).ToList();

                    for (int i = 0; i < expected.Count; i++)
                    {
                        bool expectedItemIsMissing = true;
                        var expectedItem = expected[i];
                        for (int j = 0; j < actual.Count; j++)
                        {
                            var actualItem = actual[j];
                            try
                            {
                                areEqual(expectedItem, actualItem);
                                uncheckedActualPositions.Remove(j);
                                expectedItemIsMissing = false;
                                break;
                            }
                            catch (AssertFailedException)
                            {
                            }
                        }
                        if (expectedItemIsMissing)
                        {
                            throw new AssertFailedException(
                                msg: string.Format(
                                    "Missing expected item {0}.",
                                    expectedItem));
                        }
                    }

                    // Check that actual is a subset of expected
                    for (int i = 0; i < uncheckedActualPositions.Count; i++)
                    {
                        bool actualItemIsMissing = true;
                        var actualItem = actual[uncheckedActualPositions[i]];
                        for (int j = 0; j < expected.Count; j++)
                        {
                            var expectedItem = expected[j];
                            try
                            {
                                areEqual(expectedItem, actualItem);
                                actualItemIsMissing = false;
                                break;
                            }
                            catch (AssertFailedException)
                            {
                            }
                        }
                        if (actualItemIsMissing)
                        {
                            throw new AssertFailedException(
                                msg: string.Format(
                                    "Missing actual item {0}.",
                                    actualItem));
                        }
                    }
                }
            }
        }
    }
}