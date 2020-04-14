// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using System;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Provides methods to test that 
    /// the <see cref="ITabularCollection{TValue, TCollection}"/> 
    /// interface has been properly implemented.
    /// </summary>
    static class TabularCollectionTest
    {
        /// <summary>
        /// Provides methods to test use cases of property 
        /// <see cref="ITabularCollection{TValue, TCollection}.NumberOfRows"/>.
        /// </summary>
        public static class NumberOfRows
        {
            /// <summary>
            /// Tests getting property
            /// <see cref="ITabularCollection{TValue, TCollection}.NumberOfRows" />.
            /// </summary>
            /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
            /// <typeparam name="TCollection">The type of the collection.</typeparam>
            /// <param name="expected">The value expected to be returned by the property getter.</param>
            /// <param name="source">The source instance on which to invoke the property getter.</param>
            public static void Get<TValue, TCollection>(
                int expected,
                ITabularCollection<TValue, TCollection> source)
            where TCollection : ITabularCollection<TValue, TCollection>
            {
                Assert.IsNotNull(source);

                Assert.AreEqual(expected, source.NumberOfRows);
            }
        }

        /// <summary>
        /// Provides methods to test use cases of property 
        /// <see cref="ITabularCollection{TValue, TCollection}.NumberOfColumns"/>.
        /// </summary>
        public static class NumberOfColumns
        {
            /// <summary>
            /// Tests getting property 
            /// <see cref="ITabularCollection{TValue, TCollection}.NumberOfColumns"/>.
            /// </summary>
            /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
            /// <typeparam name="TCollection">The type of the collection.</typeparam>
            /// <param name="expected">The value expected to be returned by the property getter.</param>
            /// <param name="source">The source instance on which to invoke the property getter.</param>
            public static void Get<TValue, TCollection>(
                int expected,
                ITabularCollection<TValue, TCollection> source)
            where TCollection : ITabularCollection<TValue, TCollection>
            {
                Assert.IsNotNull(source);

                Assert.AreEqual(expected, source.NumberOfColumns);
            }
        }

        /// <summary>
        /// Provides methods to test use cases of property 
        /// <see cref="ITabularCollection{TValue, TCollection}.this"/>.
        /// </summary>
        public static class Indexer
        {
            /// <summary>
            /// Provides methods to test use cases of getting property 
            /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/>.
            /// </summary>
            public static class Get
            {
                #region Input Validation

                /// <summary>
                /// Tests getting property 
                /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
                /// when a row index is out of range.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="source">The source instance on which to invoke the property getter.</param>
                public static void AnyRowIndexIsOutOfRange<TValue, TCollection>(
                    ITabularCollection<TValue, TCollection> source)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(source);

                    string STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS" });

                    string parameterName = null;

                    #region Int32

                    parameterName = "rowIndex";

                    // Int32

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[-1, 0];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[source.NumberOfRows, 0];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[-1, IndexCollection.Range(0, source.NumberOfColumns - 1)];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[source.NumberOfRows, IndexCollection.Range(0, source.NumberOfColumns - 1)];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[-1, ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[source.NumberOfRows, ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    #endregion

                    #region IndexCollection

                    parameterName = "rowIndexes";

                    // Int32

                    //          IndexCollection instances cannot contain negative elements

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[IndexCollection.Range(0, source.NumberOfRows), 0];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    //          IndexCollection instances cannot contain negative elements

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[IndexCollection.Range(0, source.NumberOfRows), IndexCollection.Range(0, source.NumberOfColumns - 1)];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    // String

                    //          IndexCollection instances cannot contain negative elements

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[IndexCollection.Range(0, source.NumberOfRows), ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    #endregion

                    #region String

                    parameterName = "rowIndexes";

                    var STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX" });

                    // Int32

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source["", 0];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source["end", 0];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source["", IndexCollection.Range(0, source.NumberOfColumns - 1)];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source["end", IndexCollection.Range(0, source.NumberOfColumns - 1)];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source["", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source["end", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    #endregion
                }

                /// <summary>
                /// Tests getting property 
                /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
                /// when a column index is out of range.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="source">The source instance on which to invoke the property getter.</param>
                public static void AnyColumnIndexIsOutOfRange<TValue, TCollection>(
                    ITabularCollection<TValue, TCollection> source)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(source);

                    string STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS" });

                    string parameterName = null;

                    #region Int32

                    parameterName = "columnIndex";

                    // Int32

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[0, -1];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[0, source.NumberOfColumns];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[IndexCollection.Range(0, source.NumberOfRows - 1), -1];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[IndexCollection.Range(0, source.NumberOfRows - 1), source.NumberOfColumns];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[":", -1];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[":", source.NumberOfColumns];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    #endregion

                    #region IndexCollection

                    parameterName = "columnIndexes";

                    // Int32

                    //          IndexCollection instances cannot contain negative elements

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[0, IndexCollection.Range(0, source.NumberOfColumns)];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    //          IndexCollection instances cannot contain negative elements

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[IndexCollection.Range(0, source.NumberOfRows - 1), IndexCollection.Range(0, source.NumberOfColumns)];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    // String

                    //          IndexCollection instances cannot contain negative elements

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[":", IndexCollection.Range(0, source.NumberOfColumns)];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    #endregion

                    #region String

                    parameterName = "columnIndexes";

                    var STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX" });

                    // Int32

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[0, ""];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[0, "end"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[IndexCollection.Range(0, source.NumberOfRows - 1), ""];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[IndexCollection.Range(0, source.NumberOfRows - 1), "end"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[":", ""];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[":", "end"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    #endregion
                }

                /// <summary>
                /// Tests getting property 
                /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
                /// when row indexes are represented by a <b>null</b> instance.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="source">The source instance on which to invoke the property getter.</param>
                public static void RowIndexesIsNull<TValue, TCollection>(
                    ITabularCollection<TValue, TCollection> source)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(source);

                    string parameterName = null;

                    #region IndexCollection

                    parameterName = "rowIndexes";

                    // Int32

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[(IndexCollection)null, 0];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[(IndexCollection)null, IndexCollection.Range(0, source.NumberOfColumns - 1)];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[(IndexCollection)null, ":"];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    #endregion

                    #region String

                    parameterName = "rowIndexes";

                    // Int32

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[(string)null, 0];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[(string)null, IndexCollection.Range(0, source.NumberOfColumns - 1)];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[(string)null, ":"];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    #endregion
                }

                /// <summary>
                /// Tests getting property 
                /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
                /// when column indexes are represented by a <b>null</b> instance.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="source">The source instance on which to invoke the property getter.</param>
                public static void ColumnIndexesIsNull<TValue, TCollection>(
                    ITabularCollection<TValue, TCollection> source)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(source);

                    string parameterName = null;

                    #region IndexCollection

                    parameterName = "columnIndexes";

                    // Int32

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[0, (IndexCollection)null];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[IndexCollection.Range(0, source.NumberOfColumns - 1), (IndexCollection)null];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[":", (IndexCollection)null];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    #endregion

                    #region String

                    parameterName = "columnIndexes";

                    // Int32

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[0, (string)null];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[IndexCollection.Range(0, source.NumberOfColumns - 1), (string)null];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var sub =
                                source[":", (string)null];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    #endregion
                }

                #endregion

                #region SubCollection

                /// <summary>
                /// Tests property 
                /// <see cref="ITabularCollection{TValue, TCollection}.this[int,int]"/> by 
                /// comparing the actual result of its getter method to an expected <typeparamref name="TValue"/>.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="areEqual">A method that verifies if its 
                /// arguments can be considered having equivalent states, i.e. they 
                /// represent the same <typeparamref name="TValue"/>.</param>
                /// <param name="expected">The <typeparamref name="TValue"/> expected to be returned by the property getter.</param>
                /// <param name="source">The source instance on which to invoke the property getter.</param>
                /// <param name="rowIndex">The row index to get.</param>
                /// <param name="columnIndexes">The column index to get.</param>
                public static void SubCollection<TValue, TCollection>(
                    Action<TValue, TValue> areEqual,
                    TValue expected,
                    ITabularCollection<TValue, TCollection> source,
                    int rowIndex,
                    int columnIndex)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(expected);
                    Assert.IsNotNull(source);

                    var actual = source[rowIndex, columnIndex];
                    areEqual(expected, actual);
                }

                /// <summary>
                /// Tests property 
                /// <see cref="ITabularCollection{TValue, TCollection}.this[int,IndexCollection]"/> by 
                /// comparing the actual result of its getter method to an expected collection.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="areEqual">A method that verifies if its 
                /// arguments can be considered having equivalent states, i.e. they 
                /// represent the same <typeparamref name="TCollection"/>.</param>
                /// <param name="expected">The <typeparamref name="TCollection"/> expected to be returned by the property getter.</param>
                /// <param name="source">The source instance on which to invoke the property getter.</param>
                /// <param name="rowIndex">The row index to get.</param>
                /// <param name="columnIndexes">The column indexes to get.</param>
                public static void SubCollection<TValue, TCollection>(
                    Action<TCollection, TCollection> areEqual,
                    TCollection expected,
                    ITabularCollection<TValue, TCollection> source,
                    int rowIndex,
                    IndexCollection columnIndexes)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(expected);
                    Assert.IsNotNull(source);
                    Assert.IsNotNull(columnIndexes);

                    var actual = source[rowIndex, columnIndexes];
                    areEqual(expected, actual);
                }

                /// <summary>
                /// Tests property 
                /// <see cref="ITabularCollection{TValue, TCollection}.this[int,string]"/> by 
                /// comparing the actual result of its getter method to an expected collection.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="areEqual">A method that verifies if its 
                /// arguments can be considered having equivalent states, i.e. they 
                /// represent the same <typeparamref name="TCollection"/>.</param>
                /// <param name="expected">The <typeparamref name="TCollection"/> expected to be returned by the property getter.</param>
                /// <param name="source">The source instance on which to invoke the property getter.</param>
                /// <param name="rowIndex">The row indexes to get.</param>
                /// <param name="columnIndexes">The column indexes to get.</param>
                public static void SubCollection<TValue, TCollection>(
                    Action<TCollection, TCollection> areEqual,
                    TCollection expected,
                    ITabularCollection<TValue, TCollection> source,
                    int rowIndex,
                    string columnIndexes)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(expected);
                    Assert.IsNotNull(source);
                    Assert.IsNotNull(columnIndexes);

                    var actual = source[rowIndex, columnIndexes];
                    areEqual(expected, actual);
                }

                /// <summary>
                /// Tests property 
                /// <see cref="ITabularCollection{TValue, TCollection}.this[IndexCollection,int]"/> by 
                /// comparing the actual result of its getter method to an expected collection.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="areEqual">A method that verifies if its 
                /// arguments can be considered having equivalent states, i.e. they 
                /// represent the same <typeparamref name="TCollection"/>.</param>
                /// <param name="expected">The <typeparamref name="TCollection"/> expected to be returned by the property getter.</param>
                /// <param name="source">The source instance on which to invoke the property getter.</param>
                /// <param name="rowIndexes">The row indexes to get.</param>
                /// <param name="columnIndex">The column index to get.</param>
                public static void SubCollection<TValue, TCollection>(
                    Action<TCollection, TCollection> areEqual,
                    TCollection expected,
                    ITabularCollection<TValue, TCollection> source,
                    IndexCollection rowIndexes,
                    int columnIndex)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(expected);
                    Assert.IsNotNull(source);
                    Assert.IsNotNull(rowIndexes);

                    var actual = source[rowIndexes, columnIndex];
                    areEqual(expected, actual);
                }

                /// <summary>
                /// Tests property 
                /// <see cref="ITabularCollection{TValue, TCollection}.this[IndexCollection,IndexCollection]"/> by 
                /// comparing the actual result of its getter method to an expected collection.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="areEqual">A method that verifies if its 
                /// arguments can be considered having equivalent states, i.e. they 
                /// represent the same <typeparamref name="TCollection"/>.</param>
                /// <param name="expected">The <typeparamref name="TCollection"/> expected to be returned by the property getter.</param>
                /// <param name="source">The source instance on which to invoke the property getter.</param>
                /// <param name="rowIndexes">The row indexes to get.</param>
                /// <param name="columnIndexes">The column indexes to get.</param>
                public static void SubCollection<TValue, TCollection>(
                    Action<TCollection, TCollection> areEqual,
                    TCollection expected,
                    ITabularCollection<TValue, TCollection> source,
                    IndexCollection rowIndexes,
                    IndexCollection columnIndexes)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(expected);
                    Assert.IsNotNull(source);
                    Assert.IsNotNull(rowIndexes);
                    Assert.IsNotNull(columnIndexes);

                    var actual = source[rowIndexes, columnIndexes];
                    areEqual(expected, actual);
                }

                /// <summary>
                /// Tests property 
                /// <see cref="ITabularCollection{TValue, TCollection}.this[IndexCollection,string]"/> by 
                /// comparing the actual result of its getter method to an expected collection.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="areEqual">A method that verifies if its 
                /// arguments can be considered having equivalent states, i.e. they 
                /// represent the same <typeparamref name="TCollection"/>.</param>
                /// <param name="expected">The <typeparamref name="TCollection"/> expected to be returned by the property getter.</param>
                /// <param name="source">The source instance on which to invoke the property getter.</param>
                /// <param name="rowIndexes">The row indexes to get.</param>
                /// <param name="columnIndexes">The column indexes to get.</param>
                public static void SubCollection<TValue, TCollection>(
                    Action<TCollection, TCollection> areEqual,
                    TCollection expected,
                    ITabularCollection<TValue, TCollection> source,
                    IndexCollection rowIndexes,
                    string columnIndexes)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(expected);
                    Assert.IsNotNull(source);
                    Assert.IsNotNull(rowIndexes);
                    Assert.IsNotNull(columnIndexes);

                    var actual = source[rowIndexes, columnIndexes];
                    areEqual(expected, actual);
                }

                /// <summary>
                /// Tests property 
                /// <see cref="ITabularCollection{TValue, TCollection}.this[string,int]"/> by 
                /// comparing the actual result of its getter method to an expected collection.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="areEqual">A method that verifies if its 
                /// arguments can be considered having equivalent states, i.e. they 
                /// represent the same <typeparamref name="TCollection"/>.</param>
                /// <param name="expected">The <typeparamref name="TCollection"/> expected to be returned by the property getter.</param>
                /// <param name="source">The source instance on which to invoke the property getter.</param>
                /// <param name="rowIndexes">The row indexes to get.</param>
                /// <param name="columnIndex">The column index to get.</param>
                public static void SubCollection<TValue, TCollection>(
                    Action<TCollection, TCollection> areEqual,
                    TCollection expected,
                    ITabularCollection<TValue, TCollection> source,
                    string rowIndexes,
                    int columnIndex)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(expected);
                    Assert.IsNotNull(source);
                    Assert.IsNotNull(rowIndexes);

                    var actual = source[rowIndexes, columnIndex];
                    areEqual(expected, actual);
                }

                /// <summary>
                /// Tests property 
                /// <see cref="ITabularCollection{TValue, TCollection}.this[string,IndexCollection]"/> by 
                /// comparing the actual result of its getter method to an expected collection.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="areEqual">A method that verifies if its 
                /// arguments can be considered having equivalent states, i.e. they 
                /// represent the same <typeparamref name="TCollection"/>.</param>
                /// <param name="expected">The <typeparamref name="TCollection"/> expected to be returned by the property getter.</param>
                /// <param name="source">The source instance on which to invoke the property getter.</param>
                /// <param name="rowIndexes">The row indexes to get.</param>
                /// <param name="columnIndexes">The column indexes to get.</param>
                public static void SubCollection<TValue, TCollection>(
                    Action<TCollection, TCollection> areEqual,
                    TCollection expected,
                    ITabularCollection<TValue, TCollection> source,
                    string rowIndexes,
                    IndexCollection columnIndexes)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(expected);
                    Assert.IsNotNull(source);
                    Assert.IsNotNull(rowIndexes);
                    Assert.IsNotNull(columnIndexes);

                    var actual = source[rowIndexes, columnIndexes];
                    areEqual(expected, actual);
                }

                /// <summary>
                /// Tests property 
                /// <see cref="ITabularCollection{TValue, TCollection}.this[string,string]"/> by 
                /// comparing the actual result of its getter method to an expected collection.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="areEqual">A method that verifies if its 
                /// arguments can be considered having equivalent states, i.e. they 
                /// represent the same <typeparamref name="TCollection"/>.</param>
                /// <param name="expected">The <typeparamref name="TCollection"/> expected to be returned by the property getter.</param>
                /// <param name="source">The source instance on which to invoke the property getter.</param>
                /// <param name="rowIndexes">The row indexes to get.</param>
                /// <param name="columnIndexes">The column indexes to get.</param>
                public static void SubCollection<TValue, TCollection>(
                    Action<TCollection, TCollection> areEqual,
                    TCollection expected,
                    ITabularCollection<TValue, TCollection> source,
                    string rowIndexes,
                    string columnIndexes)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(expected);
                    Assert.IsNotNull(source);
                    Assert.IsNotNull(rowIndexes);
                    Assert.IsNotNull(columnIndexes);

                    var actual = source[rowIndexes, columnIndexes];
                    areEqual(expected, actual);
                }

                #endregion
            }

            /// <summary>
            /// Provides methods to test use cases of setting property 
            /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/>.
            /// </summary>
            public static class Set
            {
                #region Input Validation

                /// <summary>
                /// Tests setting property 
                /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
                /// when a row index is out of range.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="source">The source instance on which to invoke the property setter.</param>
                public static void AnyRowIndexIsOutOrRange<TValue, TCollection>(
                    ITabularCollection<TValue, TCollection> source)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(source);

                    string STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS" });

                    string parameterName = null;

                    #region Int32

                    parameterName = "rowIndex";

                    // Int32

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[-1, 0] = source[0, 0];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[source.NumberOfRows, 0] = source[0, 0];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[-1, IndexCollection.Range(0, source.NumberOfColumns - 1)] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[source.NumberOfRows, IndexCollection.Range(0, source.NumberOfColumns - 1)] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[-1, ":"] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[source.NumberOfRows, ":"] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    #endregion

                    #region IndexCollection

                    parameterName = "rowIndexes";

                    // Int32

                    //          IndexCollection instances cannot contain negative elements

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[IndexCollection.Range(0, source.NumberOfRows), 0] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    //          IndexCollection instances cannot contain negative elements

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[
                                IndexCollection.Range(0, source.NumberOfRows),
                                IndexCollection.Range(0, source.NumberOfColumns - 1)] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    // String

                    //          IndexCollection instances cannot contain negative elements

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[IndexCollection.Range(0, source.NumberOfRows), ":"] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    #endregion

                    #region String

                    parameterName = "rowIndexes";

                    var STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX" });

                    // Int32

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source["", 0] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source["end", 0] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source["", IndexCollection.Range(0, source.NumberOfColumns - 1)] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source["end", IndexCollection.Range(0, source.NumberOfColumns - 1)] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source["", ":"] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source["end", ":"] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    #endregion
                }

                /// <summary>
                /// Tests setting property 
                /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
                /// when a column index is out of range.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="source">The source instance on which to invoke the property setter.</param>
                public static void AnyColumnIndexIsOutOrRange<TValue, TCollection>(
                    ITabularCollection<TValue, TCollection> source)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(source);

                    string STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS" });

                    string parameterName = null;

                    #region Int32

                    parameterName = "columnIndex";

                    // Int32

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[0, -1] = source[0, 0];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[0, source.NumberOfColumns] = source[0, 0];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[IndexCollection.Range(0, source.NumberOfRows - 1), -1] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[IndexCollection.Range(0, source.NumberOfRows - 1), source.NumberOfColumns] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[":", -1] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[":", source.NumberOfColumns] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    #endregion

                    #region IndexCollection

                    parameterName = "columnIndexes";

                    // Int32

                    //          IndexCollection instances cannot contain negative elements

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[0, IndexCollection.Range(0, source.NumberOfColumns)] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    //          IndexCollection instances cannot contain negative elements

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[
                                IndexCollection.Range(0, source.NumberOfRows - 1),
                                IndexCollection.Range(0, source.NumberOfColumns)] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    // String

                    //          IndexCollection instances cannot contain negative elements

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[":", IndexCollection.Range(0, source.NumberOfColumns)] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INDEX_EXCEEDS_DIMS,
                        expectedParameterName: parameterName);

                    #endregion

                    #region String

                    parameterName = "columnIndexes";

                    var STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX" });

                    // Int32

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[0, ""] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[0, "end"] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[IndexCollection.Range(0, source.NumberOfRows - 1), ""] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[IndexCollection.Range(0, source.NumberOfRows - 1), "end"] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[":", ""] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[":", "end"] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: STR_EXCEPT_TAB_UNSUPPORTED_SUBREF_SYNTAX,
                        expectedParameterName: parameterName);

                    #endregion
                }

                /// <summary>
                /// Tests setting property 
                /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
                /// when row indexes are represented by a <b>null</b> instance.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="source">The source instance on which to invoke the property setter.</param>
                public static void RowIndexesIsNull<TValue, TCollection>(
                    ITabularCollection<TValue, TCollection> source)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(source);

                    string parameterName = null;

                    #region IndexCollection

                    parameterName = "rowIndexes";

                    // Int32

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[(IndexCollection)null, 0] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[
                                (IndexCollection)null,
                                IndexCollection.Range(0, source.NumberOfColumns - 1)] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[(IndexCollection)null, ":"] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    #endregion

                    #region String

                    parameterName = "rowIndexes";

                    // Int32

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[(string)null, 0] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[
                                (string)null,
                                IndexCollection.Range(0, source.NumberOfColumns - 1)] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[(string)null, ":"] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    #endregion
                }

                /// <summary>
                /// Tests setting property 
                /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
                /// when column indexes are represented by a <b>null</b> instance.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="source">The source instance on which to invoke the property setter.</param>
                public static void ColumnIndexesIsNull<TValue, TCollection>(
                    ITabularCollection<TValue, TCollection> source)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(source);

                    string parameterName = null;

                    #region IndexCollection

                    parameterName = "columnIndexes";

                    // Int32

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[0, (IndexCollection)null] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[
                                IndexCollection.Range(0, source.NumberOfColumns - 1),
                                (IndexCollection)null] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[":", (IndexCollection)null] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    #endregion

                    #region String

                    parameterName = "columnIndexes";

                    // Int32

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[0, (string)null] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[
                                IndexCollection.Range(0, source.NumberOfColumns - 1),
                                (string)null] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[":", (string)null] = source[":", ":"];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    #endregion
                }

                /// <summary>
                /// Tests setting property 
                /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
                /// when an item is set through a value represented by a <b>null</b> instance.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="source">The source instance on which to invoke the property setter.</param>
                public static void ItemValueIsNull<TValue, TCollection>(
                    ITabularCollection<TValue, TCollection> source)
                    where TCollection : ITabularCollection<TValue, TCollection>
                    where TValue : class
                {
                    Assert.IsNotNull(source);

                    string parameterName = "value";

                    #region Int32

                    // Int32

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[0, 0] = (TValue)null;
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    #endregion
                }

                /// <summary>
                /// Tests setting property 
                /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
                /// when a sub referenced collection is set through a value represented by a <b>null</b> instance.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="source">The source instance on which to invoke the property setter.</param>
                public static void CollectionValueIsNull<TValue, TCollection>(
                    ITabularCollection<TValue, TCollection> source)
                    where TCollection : class, ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(source);

                    string parameterName = "value";

                    #region Int32

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[IndexCollection.Range(0, source.NumberOfRows - 1), 0] = (TCollection)null;
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[":", 0] = (TCollection)null;
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    #endregion

                    #region IndexCollection

                    // Int32

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[0, IndexCollection.Range(0, source.NumberOfColumns - 1)] = (TCollection)null;
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[
                                IndexCollection.Range(0, source.NumberOfRows - 1),
                                IndexCollection.Range(0, source.NumberOfColumns - 1)] = (TCollection)null;
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[":", IndexCollection.Range(0, source.NumberOfColumns - 1)] = (TCollection)null;
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    #endregion

                    #region String

                    // Int32

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[0, ":"] = (TCollection)null;
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[IndexCollection.Range(0, source.NumberOfRows - 1), ":"] = (TCollection)null;
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[":", ":"] = (TCollection)null;
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: parameterName);

                    #endregion
                }

                /// <summary>
                /// Tests setting property 
                /// <see cref="O:ITabularCollection{TValue, TCollection}.this"/> 
                /// when there is an input size mismatch: the collection to assign and 
                /// the sub-referenced source are inconsistent.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="source">The source instance on which to invoke the property setter.</param>
                public static void MismatchedCollectionDimensions<TValue, TCollection>(
                    ITabularCollection<TValue, TCollection> source)
                    where TCollection : class, ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(source);

                    string STR_EXCEPT_TAB_INCONSISTENT_SUBASSIGNMENT =
                        (string)Reflector.ExecuteStaticMember(
                            typeof(ImplementationServices),
                            "GetResourceString",
                            new string[] { "STR_EXCEPT_TAB_INCONSISTENT_SUBASSIGNMENT" });

                    string parameterName = "value";

                    int[] rowMismatchingIndexes = new int[source.NumberOfRows + 1];
                    TCollection rowMismatchingCollection =
                        source[IndexCollection.FromArray(rowMismatchingIndexes), ":"];

                    int[] columnMismatchingIndexes = new int[source.NumberOfColumns + 1];
                    TCollection columnMismatchingCollection =
                        source[":", IndexCollection.FromArray(columnMismatchingIndexes)];

                    #region Int32

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[IndexCollection.Range(0, source.NumberOfRows - 1), 0] =
                                rowMismatchingCollection;
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INCONSISTENT_SUBASSIGNMENT,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[IndexCollection.Range(0, source.NumberOfRows - 1), 0] =
                                columnMismatchingCollection;
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INCONSISTENT_SUBASSIGNMENT,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[":", 0] = rowMismatchingCollection;
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INCONSISTENT_SUBASSIGNMENT,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[":", 0] = columnMismatchingCollection;
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INCONSISTENT_SUBASSIGNMENT,
                        expectedParameterName: parameterName);

                    #endregion

                    #region IndexCollection

                    // Int32

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[0, IndexCollection.Range(0, source.NumberOfColumns - 1)] =
                                rowMismatchingCollection;
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INCONSISTENT_SUBASSIGNMENT,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[0, IndexCollection.Range(0, source.NumberOfColumns - 1)] =
                                columnMismatchingCollection;
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INCONSISTENT_SUBASSIGNMENT,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[
                                IndexCollection.Range(0, source.NumberOfRows - 1),
                                IndexCollection.Range(0, source.NumberOfColumns - 1)] =
                                    rowMismatchingCollection;
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INCONSISTENT_SUBASSIGNMENT,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[
                                IndexCollection.Range(0, source.NumberOfRows - 1),
                                IndexCollection.Range(0, source.NumberOfColumns - 1)] =
                                    columnMismatchingCollection;
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INCONSISTENT_SUBASSIGNMENT,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[":", IndexCollection.Range(0, source.NumberOfColumns - 1)] =
                                rowMismatchingCollection;
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INCONSISTENT_SUBASSIGNMENT,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[":", IndexCollection.Range(0, source.NumberOfColumns - 1)] =
                                columnMismatchingCollection;
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INCONSISTENT_SUBASSIGNMENT,
                        expectedParameterName: parameterName);

                    #endregion

                    #region String

                    // Int32

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[0, ":"] = rowMismatchingCollection;
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INCONSISTENT_SUBASSIGNMENT,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[0, ":"] = columnMismatchingCollection;
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INCONSISTENT_SUBASSIGNMENT,
                        expectedParameterName: parameterName);

                    // IndexCollection

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[IndexCollection.Range(0, source.NumberOfRows - 1), ":"] =
                                rowMismatchingCollection;
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INCONSISTENT_SUBASSIGNMENT,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[IndexCollection.Range(0, source.NumberOfRows - 1), ":"] =
                                columnMismatchingCollection;
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INCONSISTENT_SUBASSIGNMENT,
                        expectedParameterName: parameterName);

                    // String

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[":", ":"] = rowMismatchingCollection;
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INCONSISTENT_SUBASSIGNMENT,
                        expectedParameterName: parameterName);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            source[":", ":"] = columnMismatchingCollection;
                        },
                        expectedType: typeof(ArgumentException),
                        expectedPartialMessage: STR_EXCEPT_TAB_INCONSISTENT_SUBASSIGNMENT,
                        expectedParameterName: parameterName);

                    #endregion
                }

                #endregion

                #region SubCollection

                /// <summary>
                /// Tests property 
                /// <see cref="ITabularCollection{TValue, TCollection}.this[int,int]"/> by 
                /// setting an expected <typeparamref name="TValue"/> at the specified 
                /// row and column indexes and then comparing it with the actual result of getting 
                /// at the same indexes.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="areEqual">A method that verifies if its 
                /// arguments can be considered having equivalent states, i.e. they 
                /// represent the same <typeparamref name="TValue"/>.</param>
                /// <param name="expected">The <typeparamref name="TValue"/> expected to be returned by the property getter.</param>
                /// <param name="source">The source instance on which to invoke the property setter and getter.</param>
                /// <param name="rowIndex">The row index to set and get.</param>
                /// <param name="columnIndex">The column index to set and get.</param>
                public static void SubCollection<TValue, TCollection>(
                    Action<TValue, TValue> areEqual,
                    TValue expected,
                    ITabularCollection<TValue, TCollection> source,
                    int rowIndex,
                    int columnIndex)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(expected);
                    Assert.IsNotNull(source);

                    source[rowIndex, columnIndex] = expected;
                    var actual = source[rowIndex, columnIndex];
                    areEqual(expected, actual);
                }

                /// <summary>
                /// Tests property 
                /// <see cref="ITabularCollection{TValue, TCollection}.this[int,IndexCollection]"/> by 
                /// setting an expected <typeparamref name="TCollection"/> at the specified 
                /// row and column indexes and then comparing it with the actual result of getting 
                /// at the same indexes.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="areEqual">A method that verifies if its 
                /// arguments can be considered having equivalent states, i.e. they 
                /// represent the same <typeparamref name="TCollection"/>.</param>
                /// <param name="expected">The <typeparamref name="TCollection"/> expected to be returned by the property getter.</param>
                /// <param name="source">The source instance on which to invoke the property setter and getter.</param>
                /// <param name="rowIndex">The row index to set and get.</param>
                /// <param name="columnIndexes">The column indexes to set and get.</param>
                public static void SubCollection<TValue, TCollection>(
                    Action<TCollection, TCollection> areEqual,
                    TCollection expected,
                    ITabularCollection<TValue, TCollection> source,
                    int rowIndex,
                    IndexCollection columnIndexes)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(expected);
                    Assert.IsNotNull(source);
                    Assert.IsNotNull(columnIndexes);

                    source[rowIndex, columnIndexes] = expected;
                    var actual = source[rowIndex, columnIndexes];
                    areEqual(expected, actual);
                }

                /// <summary>
                /// Tests property 
                /// <see cref="ITabularCollection{TValue, TCollection}.this[int,string]"/> by 
                /// setting an expected <typeparamref name="TCollection"/> at the specified 
                /// row and column indexes and then comparing it with the actual result of getting 
                /// at the same indexes.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="areEqual">A method that verifies if its 
                /// arguments can be considered having equivalent states, i.e. they 
                /// represent the same <typeparamref name="TCollection"/>.</param>
                /// <param name="expected">The <typeparamref name="TCollection"/> expected to be returned by the property getter.</param>
                /// <param name="source">The source instance on which to invoke the property setter and getter.</param>
                /// <param name="rowIndex">The row index to set and get.</param>
                /// <param name="columnIndexes">The column indexes to set and get.</param>
                public static void SubCollection<TValue, TCollection>(
                    Action<TCollection, TCollection> areEqual,
                    TCollection expected,
                    ITabularCollection<TValue, TCollection> source,
                    int rowIndex,
                    string columnIndexes)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(expected);
                    Assert.IsNotNull(source);
                    Assert.IsNotNull(columnIndexes);

                    source[rowIndex, columnIndexes] = expected;
                    var actual = source[rowIndex, columnIndexes];
                    areEqual(expected, actual);
                }

                /// <summary>
                /// Tests property 
                /// <see cref="ITabularCollection{TValue, TCollection}.this[IndexCollection,int]"/> by 
                /// setting an expected <typeparamref name="TCollection"/> at the specified 
                /// row and column indexes and then comparing it with the actual result of getting 
                /// at the same indexes.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="areEqual">A method that verifies if its 
                /// arguments can be considered having equivalent states, i.e. they 
                /// represent the same <typeparamref name="TCollection"/>.</param>
                /// <param name="expected">The <typeparamref name="TCollection"/> expected to be returned by the property getter.</param>
                /// <param name="source">The source instance on which to invoke the property setter and getter.</param>
                /// <param name="rowIndexes">The row indexes to set and get.</param>
                /// <param name="columnIndex">The column index to set and get.</param>
                public static void SubCollection<TValue, TCollection>(
                    Action<TCollection, TCollection> areEqual,
                    TCollection expected,
                    ITabularCollection<TValue, TCollection> source,
                    IndexCollection rowIndexes,
                    int columnIndex)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(expected);
                    Assert.IsNotNull(source);
                    Assert.IsNotNull(rowIndexes);

                    source[rowIndexes, columnIndex] = expected;
                    var actual = source[rowIndexes, columnIndex];
                    areEqual(expected, actual);
                }

                /// <summary>
                /// Tests property 
                /// <see cref="ITabularCollection{TValue, TCollection}.this[IndexCollection,IndexCollection]"/> by 
                /// setting an expected <typeparamref name="TCollection"/> at the specified 
                /// row and column indexes and then comparing it with the actual result of getting 
                /// at the same indexes.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="areEqual">A method that verifies if its 
                /// arguments can be considered having equivalent states, i.e. they 
                /// represent the same <typeparamref name="TCollection"/>.</param>
                /// <param name="expected">The <typeparamref name="TCollection"/> expected to be returned by the property getter.</param>
                /// <param name="source">The source instance on which to invoke the property setter and getter.</param>
                /// <param name="rowIndexes">The row indexes to set and get.</param>
                /// <param name="columnIndexes">The column indexes to set and get.</param>
                public static void SubCollection<TValue, TCollection>(
                    Action<TCollection, TCollection> areEqual,
                    TCollection expected,
                    ITabularCollection<TValue, TCollection> source,
                    IndexCollection rowIndexes,
                    IndexCollection columnIndexes)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(expected);
                    Assert.IsNotNull(source);
                    Assert.IsNotNull(rowIndexes);
                    Assert.IsNotNull(columnIndexes);

                    source[rowIndexes, columnIndexes] = expected;
                    var actual = source[rowIndexes, columnIndexes];
                    areEqual(expected, actual);
                }

                /// <summary>
                /// Tests property 
                /// <see cref="ITabularCollection{TValue, TCollection}.this[IndexCollection,string]"/> by 
                /// setting an expected <typeparamref name="TCollection"/> at the specified 
                /// row and column indexes and then comparing it with the actual result of getting 
                /// at the same indexes.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="areEqual">A method that verifies if its 
                /// arguments can be considered having equivalent states, i.e. they 
                /// represent the same <typeparamref name="TCollection"/>.</param>
                /// <param name="expected">The <typeparamref name="TCollection"/> expected to be returned by the property getter.</param>
                /// <param name="source">The source instance on which to invoke the property setter and getter.</param>
                /// <param name="rowIndexes">The row indexes to set and get.</param>
                /// <param name="columnIndexes">The column indexes to set and get.</param>
                public static void SubCollection<TValue, TCollection>(
                    Action<TCollection, TCollection> areEqual,
                    TCollection expected,
                    ITabularCollection<TValue, TCollection> source,
                    IndexCollection rowIndexes,
                    string columnIndexes)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(expected);
                    Assert.IsNotNull(source);
                    Assert.IsNotNull(rowIndexes);
                    Assert.IsNotNull(columnIndexes);

                    source[rowIndexes, columnIndexes] = expected;
                    var actual = source[rowIndexes, columnIndexes];
                    areEqual(expected, actual);
                }

                /// <summary>
                /// Tests property 
                /// <see cref="ITabularCollection{TValue, TCollection}.this[string,int]"/> by 
                /// setting an expected <typeparamref name="TCollection"/> at the specified 
                /// row and column indexes and then comparing it with the actual result of getting 
                /// at the same indexes.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="areEqual">A method that verifies if its 
                /// arguments can be considered having equivalent states, i.e. they 
                /// represent the same <typeparamref name="TCollection"/>.</param>
                /// <param name="expected">The <typeparamref name="TCollection"/> expected to be returned by the property getter.</param>
                /// <param name="source">The source instance on which to invoke the property setter and getter.</param>
                /// <param name="rowIndexes">The row indexes to set and get.</param>
                /// <param name="columnIndex">The column index to set and get.</param>
                public static void SubCollection<TValue, TCollection>(
                    Action<TCollection, TCollection> areEqual,
                    TCollection expected,
                    ITabularCollection<TValue, TCollection> source,
                    string rowIndexes,
                    int columnIndex)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(expected);
                    Assert.IsNotNull(source);
                    Assert.IsNotNull(rowIndexes);

                    source[rowIndexes, columnIndex] = expected;
                    var actual = source[rowIndexes, columnIndex];
                    areEqual(expected, actual);
                }

                /// <summary>
                /// Tests property 
                /// <see cref="ITabularCollection{TValue, TCollection}.this[string,IndexCollection]"/> by 
                /// setting an expected <typeparamref name="TCollection"/> at the specified 
                /// row and column indexes and then comparing it with the actual result of getting 
                /// at the same indexes.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="areEqual">A method that verifies if its 
                /// arguments can be considered having equivalent states, i.e. they 
                /// represent the same <typeparamref name="TCollection"/>.</param>
                /// <param name="expected">The <typeparamref name="TCollection"/> expected to be returned by the property getter.</param>
                /// <param name="source">The source instance on which to invoke the property setter and getter.</param>
                /// <param name="rowIndexes">The row indexes to set and get.</param>
                /// <param name="columnIndexes">The column indexes to set and get.</param>
                public static void SubCollection<TValue, TCollection>(
                    Action<TCollection, TCollection> areEqual,
                    TCollection expected,
                    ITabularCollection<TValue, TCollection> source,
                    string rowIndexes,
                    IndexCollection columnIndexes)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(expected);
                    Assert.IsNotNull(source);
                    Assert.IsNotNull(rowIndexes);
                    Assert.IsNotNull(columnIndexes);

                    source[rowIndexes, columnIndexes] = expected;
                    var actual = source[rowIndexes, columnIndexes];
                    areEqual(expected, actual);
                }

                /// <summary>
                /// Tests property 
                /// <see cref="ITabularCollection{TValue, TCollection}.this[string,string]"/> by 
                /// setting an expected <typeparamref name="TCollection"/> at the specified 
                /// row and column indexes and then comparing it with the actual result of getting 
                /// at the same indexes.
                /// </summary>
                /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
                /// <typeparam name="TCollection">The type of the collection.</typeparam>
                /// <param name="areEqual">A method that verifies if its 
                /// arguments can be considered having equivalent states, i.e. they 
                /// represent the same <typeparamref name="TCollection"/>.</param>
                /// <param name="expected">The <typeparamref name="TCollection"/> expected to be returned by the property getter.</param>
                /// <param name="source">The source instance on which to invoke the property setter and getter.</param>
                /// <param name="rowIndexes">The row indexes to set and get.</param>
                /// <param name="columnIndexes">The column indexes to set and get.</param>
                public static void SubCollection<TValue, TCollection>(
                    Action<TCollection, TCollection> areEqual,
                    TCollection expected,
                    ITabularCollection<TValue, TCollection> source,
                    string rowIndexes,
                    string columnIndexes)
                    where TCollection : ITabularCollection<TValue, TCollection>
                {
                    Assert.IsNotNull(expected);
                    Assert.IsNotNull(source);
                    Assert.IsNotNull(rowIndexes);
                    Assert.IsNotNull(columnIndexes);

                    source[rowIndexes, columnIndexes] = expected;
                    var actual = source[rowIndexes, columnIndexes];
                    areEqual(expected, actual);
                }

                #endregion
            }
        }
    }
}
