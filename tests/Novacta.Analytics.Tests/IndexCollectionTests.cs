// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.Tools;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Reflection;

namespace Novacta.Analytics.Tests
{
    [TestClass]
    public class IndexCollectionTests
    {
        [TestMethod]
        public void GetObjectDataTest()
        {
            // info is null
            {
                var indexes = IndexCollection.FromArray(
                    new int[] { 0, 3, 5, 8 });

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        indexes.GetObjectData(
                            info: null,
                            context: new System.Runtime.Serialization.StreamingContext());
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "info");
            }
        }

        [TestMethod]
        public void SerializableTest()
        {
            // info is null
            {
                string parameterName = "info";
                string innerMessage =
                    ArgumentExceptionAssert.NullPartialMessage +
                        Environment.NewLine + "Parameter name: " + parameterName;

                ConstructorInfo serializationCtor = null;
                TypeInfo t = typeof(IndexCollection).GetTypeInfo();
                IEnumerable<ConstructorInfo> ctors = t.DeclaredConstructors;
                foreach (var ctor in ctors)
                {
                    var parameters = ctor.GetParameters();
                    if (parameters.Length == 2)
                    {
                        if ((parameters[0].ParameterType == typeof(SerializationInfo))
                            &&
                            (parameters[1].ParameterType == typeof(StreamingContext)))
                        {
                            serializationCtor = ctor;
                            break;
                        }
                    }
                }

                ExceptionAssert.InnerThrow(
                    () =>
                    {
                        serializationCtor.Invoke(
                            new object[] { null, new StreamingContext() });
                    },
                    expectedInnerType: typeof(ArgumentNullException),
                    expectedInnerMessage: innerMessage);
            }

            {
                MemoryStream ms = new MemoryStream();

                var serializedIndexes = IndexCollection.FromArray(
                    new int[] { 0, 3, 5, 8 });

                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(ms, serializedIndexes);

                // Reset stream position
                ms.Position = 0;

                var deserializedIndexes = (IndexCollection)formatter.Deserialize(ms);

                IndexCollectionAssert.AreEqual(
                    expected: serializedIndexes,
                    actual: deserializedIndexes);
            }
        }

        [TestMethod]
        public void CloneTest()
        {
            var index = IndexCollection.Range(3, 5);
            var clone = (IndexCollection)index.Clone();

            IndexCollectionAssert.IsStateAsExpected(
                expectedIndexes: new int[3] { 3, 4, 5 },
                expectedMaxIndex: 5,
                actual: clone);
        }

        [TestMethod()]
        public void CompareToTest()
        {
            int expected, actual;
            IndexCollection thisCollection, otherCollection;

            var collections = new IndexCollection[3] {
                IndexCollection.Range(1,3),
                IndexCollection.Range(4,6),
                IndexCollection.Range(1,3) };

            //  collections[0]:     1  2  3    
            //  collections[1]:     4  5  6
            //  collections[2]:     1  2  3

            // STRONGLY TYPED COMPARISON

            // Length difference > 0

            thisCollection = collections[0];
            otherCollection = IndexCollection.FromArray(
                new int[4] { 0, 0, 0, 0 });

            expected = -1;
            actual = thisCollection.CompareTo(otherCollection);

            Assert.AreEqual(expected, actual);
            Assert.IsTrue(thisCollection < otherCollection);
            Assert.IsTrue(thisCollection <= otherCollection);
            Assert.IsFalse(thisCollection > otherCollection);
            Assert.IsFalse(thisCollection >= otherCollection);

            Assert.IsTrue(thisCollection != otherCollection);
            Assert.IsFalse(thisCollection == otherCollection);
            Assert.IsFalse(thisCollection.Equals(otherCollection));

            // Length difference < 0

            thisCollection = collections[0];
            otherCollection = IndexCollection.FromArray(
                new int[2] { 0, 0 });

            expected = 1;
            actual = thisCollection.CompareTo(otherCollection);

            Assert.AreEqual(expected, actual);

            Assert.AreEqual(expected, actual);
            Assert.IsFalse(thisCollection < otherCollection);
            Assert.IsFalse(thisCollection <= otherCollection);
            Assert.IsTrue(thisCollection > otherCollection);
            Assert.IsTrue(thisCollection >= otherCollection);

            Assert.IsTrue(thisCollection != otherCollection);
            Assert.IsFalse(thisCollection == otherCollection);
            Assert.IsFalse(thisCollection.Equals(otherCollection));

            // Length difference equals 0

            // ----- collections are equal

            thisCollection = collections[0];
            otherCollection = collections[2];

            expected = 0;
            actual = thisCollection.CompareTo(otherCollection);

            Assert.AreEqual(expected, actual);

            Assert.AreEqual(expected, actual);
            Assert.IsFalse(thisCollection < otherCollection);
            Assert.IsTrue(thisCollection <= otherCollection);
            Assert.IsFalse(thisCollection > otherCollection);
            Assert.IsTrue(thisCollection >= otherCollection);

            Assert.IsFalse(thisCollection != otherCollection);
            Assert.IsTrue(thisCollection == otherCollection);
            Assert.IsTrue(thisCollection.Equals(otherCollection));

            // ----- thisCollection less than otherCollection

            thisCollection = collections[0];
            otherCollection = collections[1];

            expected = -1;
            actual = thisCollection.CompareTo(otherCollection);

            Assert.AreEqual(expected, actual);

            Assert.IsTrue(thisCollection < otherCollection);
            Assert.IsTrue(thisCollection <= otherCollection);
            Assert.IsFalse(thisCollection > otherCollection);
            Assert.IsFalse(thisCollection >= otherCollection);

            Assert.IsTrue(thisCollection != otherCollection);
            Assert.IsFalse(thisCollection == otherCollection);
            Assert.IsFalse(thisCollection.Equals(otherCollection));

            // ----- thisCollection greater than otherCollection

            thisCollection = collections[1];
            otherCollection = collections[2];

            expected = 1;
            actual = thisCollection.CompareTo(otherCollection);

            Assert.AreEqual(expected, actual);

            Assert.IsFalse(thisCollection < otherCollection);
            Assert.IsFalse(thisCollection <= otherCollection);
            Assert.IsTrue(thisCollection > otherCollection);
            Assert.IsTrue(thisCollection >= otherCollection);

            Assert.IsTrue(thisCollection != otherCollection);
            Assert.IsFalse(thisCollection == otherCollection);
            Assert.IsFalse(thisCollection.Equals(otherCollection));

            // WEAKLY TYPED COMPARISON

            object weakOther;

            // null other

            thisCollection = collections[1];
            weakOther = null;

            expected = 1;
            actual = thisCollection.CompareTo(weakOther);

            Assert.AreEqual(expected, actual);

            Assert.IsFalse(thisCollection.Equals(weakOther));

            // IndexCollection other 

            thisCollection = collections[0];
            weakOther = collections[2];

            expected = 0;
            actual = thisCollection.CompareTo(weakOther);

            Assert.AreEqual(expected, actual);

            Assert.IsTrue(thisCollection.Equals(weakOther));

            // other not of type IndexCollection

            thisCollection = collections[1];
            weakOther = DoubleMatrix.Identity(2);

            ArgumentExceptionAssert.Throw(
                () =>
                {
                    thisCollection.CompareTo(weakOther);
                },
                expectedType: typeof(ArgumentException),
                expectedPartialMessage: String.Format(
                ImplementationServices.GetResourceString(
                    "STR_EXCEPT_OBJ_HAS_WRONG_TYPE"), "IndexCollection"),
                expectedParameterName: "obj");

            Assert.IsFalse(thisCollection.Equals(weakOther));

            // COMPARISONS INVOLVING NULL OBJECTS

            ComparableObjectTest.CompareToWithNulls(thisCollection);

            ComparableObjectTest.EqualsWithNulls(thisCollection);
        }

        [TestMethod]
        public void ConvertToTabularIndexesTest()
        {
            int leadingDimension = 3;

            //  ColMajor = [ 0  3  6   9    
            //               1  4  7  10    
            //               2  5  8  11 ]  

            int[] expectedColumnIndexes = new int[12] { 0, 0, 0, 1, 1, 1, 2, 2, 2, 3, 3, 3 };
            int[] expectedRowIndexes = new int[12] { 0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2 };

            int[] linearIndexes = new int[12] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

            for (int i = 0; i < linearIndexes.Length; i++)
            {
                IndexCollection.ConvertToTabularIndexes(
                    linearIndexes[i],
                    leadingDimension,
                    out int actualRowIndex,
                    out int actualColumnIndex);

                Assert.AreEqual(expectedRowIndexes[i], actualRowIndex);

                Assert.AreEqual(expectedColumnIndexes[i], actualColumnIndex);
            }
        }

        [TestMethod]
        public void DangerousFromArrayTest()
        {
            string parameterName = "indexes";

            // indexes is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        IndexCollection.FromArray(null, true);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // indexes is empty
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        IndexCollection.FromArray(Array.Empty<int>(), true);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_NON_EMPTY"),
                    expectedParameterName: parameterName);
            }

            // indexes contains a negative element
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        IndexCollection.FromArray(new int[2] { 1, -1 }, true);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_ENTRIES_MUST_BE_NON_NEGATIVE"),
                    expectedParameterName: parameterName);

                ArgumentExceptionAssert.Throw(
                   () =>
                   {
                       IndexCollection.FromArray(new int[2] { 1, -1 }, false);
                   },
                   expectedType: typeof(ArgumentException),
                   expectedPartialMessage: ImplementationServices.GetResourceString(
                       "STR_EXCEPT_PAR_ENTRIES_MUST_BE_NON_NEGATIVE"),
                   expectedParameterName: parameterName);
            }

            // Valid indexes - copyIndexes is true
            {
                var expectedIndexes = new int[5] { 3, 4, 2, 1, 0 };

                var actual = IndexCollection.FromArray(expectedIndexes, true);

                IndexCollectionAssert.IsStateAsExpected(
                    expectedIndexes: expectedIndexes,
                    expectedMaxIndex: expectedIndexes.Max(),
                    actual: actual);

                Assert.AreNotSame(
                    notExpected: expectedIndexes,
                    actual: actual.Indexes);
            }

            // Valid indexes - copyIndexes is false
            {
                var expectedIndexes = new int[5] { 3, 4, 2, 1, 0 };

                var actual = IndexCollection.FromArray(expectedIndexes, false);

                IndexCollectionAssert.IsStateAsExpected(
                    expectedIndexes: expectedIndexes,
                    expectedMaxIndex: expectedIndexes.Max(),
                    actual: actual);

                Assert.AreSame(
                    expected: expectedIndexes,
                    actual: actual.Indexes);
            }
        }

        [TestMethod]
        public void DefaultTest()
        {
            // lastIndex is negative 
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        IndexCollection.Default(
                            lastIndex: -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE"),
                    expectedParameterName: "lastIndex");
            }

            // IndexCollection(0, 7)
            {
                var actual = IndexCollection.Default(
                    lastIndex: 7);

                var expectedIndexes = new int[8] { 0, 1, 2, 3, 4, 5, 6, 7 };

                IndexCollectionAssert.IsStateAsExpected(
                    expectedIndexes: expectedIndexes,
                    expectedMaxIndex: expectedIndexes.Max(),
                    actual: actual);
            }

            // IndexCollection(0, 0)
            {
                var actual = IndexCollection.Default(
                    lastIndex: 0);

                var expectedIndexes = new int[1] { 0 };

                IndexCollectionAssert.IsStateAsExpected(
                    expectedIndexes: expectedIndexes,
                    expectedMaxIndex: expectedIndexes.Max(),
                    actual: actual);
            }
        }

        [TestMethod]
        public void FromArrayTest()
        {
            string parameterName = "indexes";

            // indexes is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        IndexCollection.FromArray(null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // indexes is empty
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        IndexCollection.FromArray(Array.Empty<int>());
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_NON_EMPTY"),
                    expectedParameterName: parameterName);
            }

            // indexes contains a negative element
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        IndexCollection.FromArray(new int[2] { 1, -1 });
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_ENTRIES_MUST_BE_NON_NEGATIVE"),
                    expectedParameterName: parameterName);
            }

            // Valid indexes
            {
                var expectedIndexes = new int[5] { 3, 4, 2, 1, 0 };

                var actual = IndexCollection.FromArray(expectedIndexes);

                IndexCollectionAssert.IsStateAsExpected(
                    expectedIndexes: expectedIndexes,
                    expectedMaxIndex: expectedIndexes.Max(),
                    actual: actual);
            }
        }

        [TestMethod]
        public void GetEnumeratorTest()
        {
            // IEnumerable.GetEnumerator
            {
                var target = IndexCollection.Default(10);
                IEnumerable enumerable = (IEnumerable)target;

                IEnumerator enumerator = enumerable.GetEnumerator();
                object current;
                int index = 0;

                while (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    Assert.AreEqual(target[index], (int)current);
                    index++;
                }

                // reset 
                enumerator.Reset();

                Assert.AreEqual(-1, (int)Reflector.GetField(enumerator, "position"));

                // dispose
                enumerator = null;
                GC.Collect(10, GCCollectionMode.Forced);
            }

            // IEnumerable<int>.GetEnumerator
            {
                var target = IndexCollection.Default(10);
                IEnumerable<int> enumerable = (IEnumerable<int>)target;

                IEnumerator<int> enumerator = enumerable.GetEnumerator();

                int index = 0;
                double current;

                while (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    Assert.AreEqual(target[index], current);
                    index++;
                }

                // reset 
                enumerator.Reset();

                Assert.AreEqual(-1, (int)Reflector.GetField(enumerator, "position"));

                // dispose
                enumerator.Dispose();
            }

            // IEnumerable<int>.Current fails
            {
                string STR_EXCEPT_ENU_OUT_OF_BOUNDS =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_ENU_OUT_OF_BOUNDS" });

                var target = IndexCollection.Default(10);
                var enumerable = (IEnumerable<int>)target;

                var enumerator = enumerable.GetEnumerator();

                ExceptionAssert.Throw(
                    () =>
                    {
                        int current = enumerator.Current;
                    },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage: STR_EXCEPT_ENU_OUT_OF_BOUNDS);
            }

            // IEnumerable.Current fails
            {
                string STR_EXCEPT_ENU_OUT_OF_BOUNDS =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_ENU_OUT_OF_BOUNDS" });

                var target = IndexCollection.Default(10);
                var enumerable = (IEnumerable)target;

                var enumerator = enumerable.GetEnumerator();

                ExceptionAssert.Throw(
                    () =>
                    {
                        object current = enumerator.Current;
                    },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage: STR_EXCEPT_ENU_OUT_OF_BOUNDS);
            }
        }

        [TestMethod]
        public void GetHashCodeTest()
        {
            var target = IndexCollection.Default(2);

            var expected = (3).GetHashCode()
                ^ (0).GetHashCode()
                ^ (1).GetHashCode()
                ^ (2).GetHashCode();

            var actual = target.GetHashCode();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IndexerGetTest()
        {
            // IndexCollection.Get(IndexCollection positions)
            {
                // positions is null
                {
                    var target = IndexCollection.Default(10);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var actual = target[null];
                        },
                        expectedType: typeof(ArgumentNullException),
                        expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                        expectedParameterName: "positions");
                }

                // any item in positions exceeds collection's dimension
                {
                    var target = IndexCollection.Default(10);
                    var positions = IndexCollection.Default(11);

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var actual = target[positions];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: ImplementationServices.GetResourceString(
                            "STR_EXCEPT_IND_POSITION_EXCEEDS_DIMS"),
                        expectedParameterName: "positions");
                }

                // positions { 2, 3 } from IndexCollection({ 3, 4, 2, 1, 0 })
                {
                    var target = IndexCollection.FromArray(new int[5] { 3, 4, 2, 1, 0 });
                    var positions = IndexCollection.Range(2, 3);

                    var actual = target[positions];

                    var expectedIndexes = new int[2] { 2, 1 };

                    IndexCollectionAssert.IsStateAsExpected(
                        expectedIndexes: expectedIndexes,
                        expectedMaxIndex: expectedIndexes.Max(),
                        actual: actual);
                }

                // positions { 0 : 2 : 5 } from IndexCollection({ 3, 4, 2, 1, 0 })
                {
                    var target = IndexCollection.FromArray(new int[5] { 3, 4, 2, 1, 0 });
                    var positions = IndexCollection.Sequence(0, 2, 5);

                    var actual = target[positions];

                    var expectedIndexes = new int[3] { 3, 2, 0 };

                    IndexCollectionAssert.IsStateAsExpected(
                        expectedIndexes: expectedIndexes,
                        expectedMaxIndex: expectedIndexes.Max(),
                        actual: actual);
                }

                // positions { 1, 0, 3 } from IndexCollection({ 3, 4, 2, 1, 0 })
                {
                    var target = IndexCollection.FromArray(new int[5] { 3, 4, 2, 1, 0 });
                    var positions = IndexCollection.FromArray(new int[3] { 1, 0, 3 });

                    var actual = target[positions];

                    var expectedIndexes = new int[3] { 4, 3, 1 };

                    IndexCollectionAssert.IsStateAsExpected(
                        expectedIndexes: expectedIndexes,
                        expectedMaxIndex: expectedIndexes.Max(),
                        actual: actual);
                }

                // positions { 2 : 2 } from IndexCollection.Range(3, 10)
                {
                    var target = IndexCollection.Range(3, 10);
                    var positions = IndexCollection.Range(2, 2);

                    var actual = target[positions];

                    var expectedIndexes = new int[1] { 5 };

                    IndexCollectionAssert.IsStateAsExpected(
                        expectedIndexes: expectedIndexes,
                        expectedMaxIndex: expectedIndexes.Max(),
                        actual: actual);
                }

                // positions { 2 : 4 } from IndexCollection.Range(3, 10)
                {
                    var target = IndexCollection.Range(3, 10);
                    var positions = IndexCollection.Range(2, 4);

                    var actual = target[positions];

                    var expectedIndexes = new int[3] { 5, 6, 7 };

                    IndexCollectionAssert.IsStateAsExpected(
                        expectedIndexes: expectedIndexes,
                        expectedMaxIndex: expectedIndexes.Max(),
                        actual: actual);
                }

                // positions { 1 : 2 } from IndexCollection.Sequence(3, 4, 12)
                {
                    var target = IndexCollection.Sequence(3, 4, 12);
                    var positions = IndexCollection.Range(1, 2);

                    var actual = target[positions];

                    var expectedIndexes = new int[2] { 7, 11 };

                    IndexCollectionAssert.IsStateAsExpected(
                        expectedIndexes: expectedIndexes,
                        expectedMaxIndex: expectedIndexes.Max(),
                        actual: actual);
                }

                // positions { 0 : 2 : 2 } from IndexCollection.Sequence(3, 4, 12)
                {
                    var target = IndexCollection.Sequence(3, 4, 12);
                    var positions = IndexCollection.Sequence(0, 2, 2);

                    var actual = target[positions];

                    var expectedIndexes = new int[2] { 3, 11 };

                    IndexCollectionAssert.IsStateAsExpected(
                        expectedIndexes: expectedIndexes,
                        expectedMaxIndex: expectedIndexes.Max(),
                        actual: actual);
                }

                // positions { 1, 0, 2, 2 } from IndexCollection.Sequence(3, 4, 12 )
                {
                    var target = IndexCollection.Sequence(3, 4, 12);
                    var positions = IndexCollection.FromArray(new int[4] { 1, 0, 2, 2 });

                    var actual = target[positions];

                    var expectedIndexes = new int[4] { 7, 3, 11, 11 };

                    IndexCollectionAssert.IsStateAsExpected(
                        expectedIndexes: expectedIndexes,
                        expectedMaxIndex: expectedIndexes.Max(),
                        actual: actual);
                }

                // positions { 0 : 3 } from IndexCollection.Sequence(15, -3, 2)          
                {
                    var target = IndexCollection.Sequence(15, -3, 2);
                    var positions = IndexCollection.Default(3);

                    var actual = target[positions];

                    var expectedIndexes = new int[4] { 15, 12, 9, 6 };

                    IndexCollectionAssert.IsStateAsExpected(
                        expectedIndexes: expectedIndexes,
                        expectedMaxIndex: expectedIndexes.Max(),
                        actual: actual);
                }

                // positions { 0 : 2 : 4 } from IndexCollection.Sequence(15, -3, 2)          
                {
                    var target = IndexCollection.Sequence(15, -3, 2);
                    var positions = IndexCollection.Sequence(0, 2, 4);

                    var actual = target[positions];

                    var expectedIndexes = new int[3] { 15, 9, 3 };

                    IndexCollectionAssert.IsStateAsExpected(
                        expectedIndexes: expectedIndexes,
                        expectedMaxIndex: expectedIndexes.Max(),
                        actual: actual);
                }

                // positions { 1, 0, 2, 2 } from IndexCollection.Sequence(15, -3, 2)          
                {
                    var target = IndexCollection.Sequence(15, -3, 2);
                    var positions = IndexCollection.FromArray(new int[4] { 1, 0, 2, 2 });

                    var actual = target[positions];

                    var expectedIndexes = new int[4] { 12, 15, 9, 9 };

                    IndexCollectionAssert.IsStateAsExpected(
                        expectedIndexes: expectedIndexes,
                        expectedMaxIndex: expectedIndexes.Max(),
                        actual: actual);
                }
            }

            // IndexCollection.Get(int position)
            {
                // position exceeds collection's dimension
                {
                    var target = IndexCollection.Default(10);
                    int position = target.Count;

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var actual = target[position];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: ImplementationServices.GetResourceString(
                            "STR_EXCEPT_IND_POSITION_EXCEEDS_DIMS"),
                        expectedParameterName: "position");
                }

                // position is negative
                {
                    var target = IndexCollection.Default(10);
                    int position = -1;

                    ArgumentExceptionAssert.Throw(
                        () =>
                        {
                            var actual = target[position];
                        },
                        expectedType: typeof(ArgumentOutOfRangeException),
                        expectedPartialMessage: ImplementationServices.GetResourceString(
                            "STR_EXCEPT_IND_POSITION_EXCEEDS_DIMS"),
                        expectedParameterName: "position");
                }

                // IndexCollection: { 1, 12, 9, 6, 3 }
                {
                    var targetArray = new int[5] { 1, 12, 9, 6, 3 };
                    var target = IndexCollection.FromArray(
                        targetArray);

                    for (int i = 0; i < target.Count; i++)
                    {
                        var actual = target[i];
                        var expected = targetArray[i];
                        Assert.AreEqual(expected, actual);
                    }
                }
            }
        }

        [TestMethod]
        public void IndexerSetTest()
        {
            // position exceeds collection's dimension
            {
                var target = IndexCollection.Default(10);
                int position = target.Count;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target[position] = 0;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_IND_POSITION_EXCEEDS_DIMS"),
                    expectedParameterName: "position");
            }

            // position is negative
            {
                var target = IndexCollection.Default(10);
                int position = -1;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target[position] = 0;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_IND_POSITION_EXCEEDS_DIMS"),
                    expectedParameterName: "position");
            }

            // value is negative
            {
                var target = IndexCollection.Default(
                            lastIndex: 1);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target[0] = -1;
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE"),
                    expectedParameterName: "value");
            }

            // value < this.indexes.Min() && this.maxIndex == this.indexes.Min()
            {
                var actual = IndexCollection.Range(1, 1);

                actual[0] = 0;

                IndexCollectionAssert.IsStateAsExpected(
                    expectedIndexes: new int[1] { 0 },
                    expectedMaxIndex: 0,
                    actual: actual);
            }

            // IndexCollection: { 1, 12, 9, 6, 3 }
            {
                var actual = IndexCollection.FromArray(
                    new int[5] { 1, 12, 9, 6, 3 });

                for (int i = 0; i < actual.Count; i++)
                {
                    actual[i] = 2 * actual[i];
                }
                var expectedIndexes = new int[5] { 2, 24, 18, 12, 6 };

                IndexCollectionAssert.IsStateAsExpected(
                    expectedIndexes: expectedIndexes,
                    expectedMaxIndex: expectedIndexes.Max(),
                    actual: actual);
            }

            // IndexCollection: { 2, 3, 4, 5, 6 } 
            // - set at position whose index is the max index
            {
                var actual = IndexCollection.FromArray(
                    new int[5] { 2, 3, 4, 5, 6 });

                actual[4] = 5;
                var expectedIndexes = new int[5] { 2, 3, 4, 5, 5 };

                IndexCollectionAssert.IsStateAsExpected(
                    expectedIndexes: expectedIndexes,
                    expectedMaxIndex: expectedIndexes.Max(),
                    actual: actual);
            }
        }

        [TestMethod]
        public void IndexesGetTest()
        {
            IndexCollection i = IndexCollection.Range(0, 1);

            int[] indexes = i.Indexes;

            Assert.AreEqual(0, indexes[0]);
            Assert.AreEqual(1, indexes[1]);
        }

        [TestMethod]
        public void RangeTest()
        {
            // firstIndex is negative 
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        IndexCollection.Range(-2, 0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE"),
                    expectedParameterName: "firstIndex");
            }

            // lastIndex is less than firstIndex
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        IndexCollection.Range(1, 0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_IND_LAST_LESS_THAN_FIRST"),
                    expectedParameterName: "lastIndex");
            }

            // IndexCollection(3, 10)
            {
                var actual = IndexCollection.Range(
                    firstIndex: 3,
                    lastIndex: 10);

                var expectedIndexes = new int[8] { 3, 4, 5, 6, 7, 8, 9, 10 };

                IndexCollectionAssert.IsStateAsExpected(
                    expectedIndexes: expectedIndexes,
                    expectedMaxIndex: expectedIndexes.Max(),
                    actual: actual);
            }

            // IndexCollection(2, 2)
            {
                var actual = IndexCollection.Range(
                    firstIndex: 2,
                    lastIndex: 2);

                var expectedIndexes = new int[1] { 2 };

                IndexCollectionAssert.IsStateAsExpected(
                    expectedIndexes: expectedIndexes,
                    expectedMaxIndex: expectedIndexes.Max(),
                    actual: actual);
            }
        }

        [TestMethod]
        public void SequenceTest()
        {
            // firstIndex is negative 
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        IndexCollection.Sequence(
                            firstIndex: -1,
                            increment: 1,
                            indexBound: 1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE"),
                    expectedParameterName: "firstIndex");
            }

            // increment is zero 
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        IndexCollection.Sequence(
                            firstIndex: 1,
                            increment: 0,
                            indexBound: 2);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_NONZERO"),
                    expectedParameterName: "increment");
            }

            // indexBound is negative 
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        IndexCollection.Sequence(
                            firstIndex: 0,
                            increment: 1,
                            indexBound: -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE"),
                    expectedParameterName: "indexBound");
            }

            // increment is negative and indexBound is greater than firstIndex 
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        IndexCollection.Sequence(
                            firstIndex: 1,
                            increment: -1,
                            indexBound: 2);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_IND_NEGATIVE_INCREMENT_FIRST_LESS_THAN_LAST"),
                    expectedParameterName: "indexBound");
            }

            // increment is positive and indexBound is less than firstIndex 
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        IndexCollection.Sequence(
                            firstIndex: 2,
                            increment: 1,
                            indexBound: 1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_IND_POSITIVE_INCREMENT_LAST_LESS_THAN_FIRST"),
                    expectedParameterName: "indexBound");
            }

            // IndexCollection(1, 4, 3)
            {
                var actual = IndexCollection.Sequence(
                    firstIndex: 1,
                    increment: 4,
                    indexBound: 3);

                var expectedIndexes = new int[1] { 1 };

                IndexCollectionAssert.IsStateAsExpected(
                    expectedIndexes: expectedIndexes,
                    expectedMaxIndex: expectedIndexes.Max(),
                    actual: actual);
            }

            // IndexCollection( 1, 1, 3)
            {
                var actual = IndexCollection.Sequence(
                    firstIndex: 1,
                    increment: 1,
                    indexBound: 3);

                var expectedIndexes = new int[3] { 1, 2, 3 };

                IndexCollectionAssert.IsStateAsExpected(
                    expectedIndexes: expectedIndexes,
                    expectedMaxIndex: expectedIndexes.Max(),
                    actual: actual);
            }

            // IndexCollection(3, 4, 12)
            {
                var actual = IndexCollection.Sequence(
                    firstIndex: 3,
                    increment: 4,
                    indexBound: 12);

                var expectedIndexes = new int[3] { 3, 7, 11 };

                IndexCollectionAssert.IsStateAsExpected(
                    expectedIndexes: expectedIndexes,
                    expectedMaxIndex: expectedIndexes.Max(),
                    actual: actual);
            }

            // IndexCollection(6, -2, 1)
            {
                var actual = IndexCollection.Sequence(
                    firstIndex: 6,
                    increment: -2,
                    indexBound: 1);

                var expectedIndexes = new int[3] { 6, 4, 2 };

                IndexCollectionAssert.IsStateAsExpected(
                    expectedIndexes: expectedIndexes,
                    expectedMaxIndex: expectedIndexes.Max(),
                    actual: actual);
            }

            // IndexCollection(15, -3, 2)
            {
                var actual = IndexCollection.Sequence(
                    firstIndex: 15,
                    increment: -3,
                    indexBound: 2);

                var expectedIndexes = new int[5] { 15, 12, 9, 6, 3 };

                IndexCollectionAssert.IsStateAsExpected(
                    expectedIndexes: expectedIndexes,
                    expectedMaxIndex: expectedIndexes.Max(),
                    actual: actual);
            }

            // IndexCollection(1, -1, 1)
            {
                var actual = IndexCollection.Sequence(
                    firstIndex: 1,
                    increment: -1,
                    indexBound: 1);

                var expectedIndexes = new int[1] { 1 };

                IndexCollectionAssert.IsStateAsExpected(
                    expectedIndexes: expectedIndexes,
                    expectedMaxIndex: expectedIndexes.Max(),
                    actual: actual);
            }

            // IndexCollection(0, -1, 0)
            {
                var actual = IndexCollection.Sequence(
                    firstIndex: 0,
                    increment: -1,
                    indexBound: 0);

                var expectedIndexes = new int[1] { 0 };

                IndexCollectionAssert.IsStateAsExpected(
                    expectedIndexes: expectedIndexes,
                    expectedMaxIndex: expectedIndexes.Max(),
                    actual: actual);
            }
        }

        [TestMethod]
        public void SortTest()
        {
            // Valid indexes
            {
                var expectedIndexes = new int[5] { 0, 1, 2, 3, 4 };

                var actual = IndexCollection.FromArray(new int[5] { 3, 4, 2, 1, 0 });

                actual.Sort();

                IndexCollectionAssert.IsStateAsExpected(
                    expectedIndexes: expectedIndexes,
                    expectedMaxIndex: expectedIndexes.Max(),
                    actual: actual);
            }
        }

        [TestMethod]
        public void ToStringTest()
        {
            // IndexCollection(0, 7).ToString()
            {
                var actual = IndexCollection.Default(
                    lastIndex: 7);

                var expected = "0, 1, 2, 3, 4, 5, 6, 7";

                Assert.AreEqual(expected, actual.ToString());
            }

            // IndexCollection(0, 0).ToString()
            {
                var actual = IndexCollection.Default(
                    lastIndex: 0);

                var expected = "0";

                Assert.AreEqual(expected, actual.ToString());
            }

        }

        #region IList<int>

        [TestMethod()]
        public void ContainsTest()
        {
            var target = IndexCollection.FromArray(
                new int[6] { 0, 1, 2, 1, 3, 4 });

            Assert.AreEqual(
                expected: true,
                actual: target.Contains(4));

            Assert.AreEqual(
                expected: true,
                actual: target.Contains(0));

            Assert.AreEqual(
                expected: true,
                actual: target.Contains(1));

            Assert.AreEqual(
                expected: true,
                actual: target.Contains(3));

            Assert.AreEqual(
                expected: false,
                actual: target.Contains(5));
        }

        [TestMethod()]
        public void CopyToTest()
        {
            // array is null
            {
                string parameterName = "array";

                var target = IndexCollection.FromArray(
                    new int[6] { 0, 1, 2, 1, 3, 4 });

                // Dense
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target.CopyTo(null, 0);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: parameterName);
            }

            // arrayIndex is negative
            {
                string STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE" });

                string parameterName = "arrayIndex";

                var target = IndexCollection.FromArray(
                    new int[6] { 0, 1, 2, 1, 3, 4 });

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target.CopyTo(new int[1], -1);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE,
                    expectedParameterName: parameterName);
            }

            // array has not enough space
            {
                string STR_EXCEPT_PAR_NOT_ENOUGH_SPACE_IN_ARRAY =
                    (string)Reflector.ExecuteStaticMember(
                        typeof(ImplementationServices),
                        "GetResourceString",
                        new string[] { "STR_EXCEPT_PAR_NOT_ENOUGH_SPACE_IN_ARRAY" });

                var target = IndexCollection.FromArray(
                    new int[6] { 0, 1, 2, 1, 3, 4 });

                int count = target.Count;

                int arrayIndex = 1;
                int arrayLength = count - arrayIndex;
                int[] array = new int[arrayLength];

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        target.CopyTo(array, arrayIndex);
                    },
                    expectedType: typeof(ArgumentException),
                    expectedPartialMessage: STR_EXCEPT_PAR_NOT_ENOUGH_SPACE_IN_ARRAY,
                    expectedParameterName: null);
            }

            // Valid input
            {
                {
                    var target = IndexCollection.FromArray(
                        new int[4] { 1, 0, 0, 0 });

                    var array = new int[6] { 10, 20, 30, 40, 50, 60 };

                    int arrayIndex = 1;

                    target.CopyTo(array, arrayIndex);

                    ArrayAssert<int>.AreEqual(
                        expected: new int[6] { 10, 1, 0, 0, 0, 60 },
                        actual: array);
                }

                {
                    var target = IndexCollection.FromArray(
                        new int[4] { 11, 0, 0, 44 });

                    var array = new int[6] { 10, 20, 30, 40, 50, 60 };

                    int arrayIndex = 2;

                    target.CopyTo(array, arrayIndex);

                    ArrayAssert<int>.AreEqual(
                        expected: new int[6] { 10, 20, 11, 0, 0, 44 },
                        actual: array);
                }
            }
        }

        [TestMethod()]
        public void IListGetTest()
        {
            var target = IndexCollection.FromArray(
                new int[6] { 0, 1, 2, 1, 3, 4 });

            Assert.AreEqual(
                expected: 4,
                actual: ((IList<int>)target)[5]);

            Assert.AreEqual(
                expected: 0,
                actual: ((IList<int>)target)[0]);

            Assert.AreEqual(
                expected: 1,
                actual: ((IList<int>)target)[1]);

            Assert.AreEqual(
                expected: 3,
                actual: ((IList<int>)target)[4]);
        }

        [TestMethod()]
        public void IListSetTest()
        {
            var target = IndexCollection.FromArray(
                new int[6] { 0, 1, 2, 1, 3, 4 });

            ((IList<int>)target)[5] = 40;

            Assert.AreEqual(
                expected: 40,
                actual: ((IList<int>)target)[5]);

            ((IList<int>)target)[0] = 10;

            Assert.AreEqual(
                expected: 10,
                actual: ((IList<int>)target)[0]);

            ((IList<int>)target)[1] = 11;

            Assert.AreEqual(
                expected: 11,
                actual: ((IList<int>)target)[1]);

            ((IList<int>)target)[4] = 30;

            Assert.AreEqual(
                expected: 30,
                actual: ((IList<int>)target)[4]);
        }

        [TestMethod()]
        public void IListInsertTest()
        {
            var target = IndexCollection.Default(4);
            ExceptionAssert.Throw(
                () =>
                {
                    ((IList<int>)target).Insert(0, 0);
                },
                expectedType: typeof(NotSupportedException),
                expectedMessage: "Specified method is not supported.");
        }

        [TestMethod()]
        public void IListRemoveAtTest()
        {
            var target = IndexCollection.Default(4);
            ExceptionAssert.Throw(
                () =>
                {
                    ((IList<int>)target).RemoveAt(0);
                },
                expectedType: typeof(NotSupportedException),
                expectedMessage: "Specified method is not supported.");
        }

        [TestMethod()]
        public void IndexOfTest()
        {
            var target = IndexCollection.FromArray(
                new int[6] { 0, 1, 2, 1, 3, 4 });

            Assert.AreEqual(
                expected: 5,
                actual: target.IndexOf(4));

            Assert.AreEqual(
                expected: 0,
                actual: target.IndexOf(0));

            Assert.AreEqual(
                expected: 1,
                actual: target.IndexOf(1));

            Assert.AreEqual(
                expected: 4,
                actual: target.IndexOf(3));

            Assert.AreEqual(
                expected: -1,
                actual: target.IndexOf(5));
        }

        #endregion

        #region ICollection<int>

        [TestMethod()]
        public void ICollectionAddTest()
        {
            var target = IndexCollection.Default(4);
            ExceptionAssert.Throw(
                () =>
                {
                    ((ICollection<int>)target).Add(0);
                },
                expectedType: typeof(NotSupportedException),
                expectedMessage: "Specified method is not supported.");
        }

        [TestMethod()]
        public void ICollectionClearTest()
        {
            var target = IndexCollection.Default(4);
            ExceptionAssert.Throw(
                () =>
                {
                    ((ICollection<int>)target).Clear();
                },
                expectedType: typeof(NotSupportedException),
                expectedMessage: "Specified method is not supported.");
        }

        [TestMethod()]
        public void IsReadOnlyTest()
        {
            var target = IndexCollection.Default(4);
            Assert.AreEqual(
                expected: false,
                actual: target.IsReadOnly);
        }

        [TestMethod()]
        public void ICollectionRemoveTest()
        {
            var target = IndexCollection.Default(4);
            ExceptionAssert.Throw(
                () =>
                {
                    ((ICollection<int>)target).Remove(0);
                },
                expectedType: typeof(NotSupportedException),
                expectedMessage: "Specified method is not supported.");
        }

        #endregion
    }
}
