// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class CategoricalVariableTests
    {
        [TestMethod]
        public void GetObjectDataTest()
        {
            // info is null
            {
                CategoricalVariable variable = new CategoricalVariable("Variable")
                {
                    { 0, "A" },
                    { 1, "B" },
                    { 2, "C" },
                    { 3, "D" },
                    { 4, "E" }
                };

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        variable.GetObjectData(
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
                TypeInfo t = typeof(CategoricalVariable).GetTypeInfo();
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

            // Has not categories - IsReadOnly is true
            {
                MemoryStream ms = new MemoryStream();

                CategoricalVariable serializedVariable = new CategoricalVariable("Variable");

                serializedVariable.SetAsReadOnly();

                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(ms, serializedVariable);

                // Reset stream position
                ms.Position = 0;

                var deserializedVariable = (CategoricalVariable)formatter.Deserialize(ms);

                var deserializedCategories =
                    (List<Category>)
                        Reflector.GetField(deserializedVariable, "categories");

                Assert.AreEqual(
                    deserializedVariable.Categories.Count,
                    deserializedCategories.Count);

                for (int i = 0; i < deserializedCategories.Count; i++)
                {
                    CategoryAssert.AreEqual(
                        expected: deserializedCategories[i],
                        actual: deserializedVariable.Categories[i]);
                }

                CategoricalVariableAssert.AreEqual(
                    expected: serializedVariable,
                    actual: deserializedVariable);
            }

            // Has not categories - IsReadOnly is false
            {
                MemoryStream ms = new MemoryStream();

                CategoricalVariable serializedVariable = new CategoricalVariable("Variable");

                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(ms, serializedVariable);

                // Reset stream position
                ms.Position = 0;

                var deserializedVariable = (CategoricalVariable)formatter.Deserialize(ms);

                var deserializedCategories =
                    (List<Category>)
                        Reflector.GetField(deserializedVariable, "categories");

                Assert.AreEqual(
                    deserializedVariable.Categories.Count,
                    deserializedCategories.Count);

                for (int i = 0; i < deserializedCategories.Count; i++)
                {
                    CategoryAssert.AreEqual(
                        expected: deserializedCategories[i],
                        actual: deserializedVariable.Categories[i]);
                }
                CategoricalVariableAssert.AreEqual(
                    expected: serializedVariable,
                    actual: deserializedVariable);
            }

            // Has categories - IsReadOnly is true
            {
                MemoryStream ms = new MemoryStream();

                CategoricalVariable serializedVariable = new CategoricalVariable("Variable")
                {
                    { 0, "A" },
                    { 1, "B" },
                    { 2, "C" },
                    { 3, "D" },
                    { 4, "E" }
                };
                serializedVariable.SetAsReadOnly();

                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(ms, serializedVariable);

                // Reset stream position
                ms.Position = 0;

                var deserializedVariable = (CategoricalVariable)formatter.Deserialize(ms);

                var deserializedCategories =
                    (List<Category>)
                        Reflector.GetField(deserializedVariable, "categories");

                Assert.AreEqual(
                    deserializedVariable.Categories.Count,
                    deserializedCategories.Count);

                for (int i = 0; i < deserializedCategories.Count; i++)
                {
                    CategoryAssert.AreEqual(
                        expected: deserializedCategories[i],
                        actual: deserializedVariable.Categories[i]);
                }

                CategoricalVariableAssert.AreEqual(
                    expected: serializedVariable,
                    actual: deserializedVariable);
            }

            // Has categories - IsReadOnly is false
            {
                MemoryStream ms = new MemoryStream();

                CategoricalVariable serializedVariable = new CategoricalVariable("Variable")
                {
                    { 0, "A" },
                    { 1, "B" },
                    { 2, "C" },
                    { 3, "D" },
                    { 4, "E" }
                };

                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(ms, serializedVariable);

                // Reset stream position
                ms.Position = 0;

                var deserializedVariable = (CategoricalVariable)formatter.Deserialize(ms);

                var deserializedCategories =
                    (List<Category>)
                        Reflector.GetField(deserializedVariable, "categories");

                Assert.AreEqual(
                    deserializedVariable.Categories.Count,
                    deserializedCategories.Count);

                for (int i = 0; i < deserializedCategories.Count; i++)
                {
                    CategoryAssert.AreEqual(
                        expected: deserializedCategories[i],
                        actual: deserializedVariable.Categories[i]);
                }

                CategoricalVariableAssert.AreEqual(
                    expected: serializedVariable,
                    actual: deserializedVariable);
            }
        }

        [TestMethod()]
        public void ConstructorTest()
        {
            // name is null
            {
                ArgumentExceptionAssert.Throw(
                    () => { new CategoricalVariable(null); },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "name");
            }

            // name is white space
            {
                ArgumentExceptionAssert.Throw(
                    () => { new CategoricalVariable(" "); },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE"),
                    expectedParameterName: "name");
            }

            // name is empty
            {
                ArgumentExceptionAssert.Throw(
                    () => { new CategoricalVariable(string.Empty); },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE"),
                    expectedParameterName: "name");
            }

            // Valid name
            {
                string expectedName = "The name";

                var target = new CategoricalVariable(expectedName);

                CategoricalVariableAssert.IsStateAsExpected(
                    target,
                    expectedName,
                    expectedCategories: new List<Category>(),
                    expectedReadOnlyFlag: false);
            }
        }

        [TestMethod]
        public void GetEnumeratorTest()
        {
            // IEnumerable.GetEnumerator
            {
                var target = new CategoricalVariable("V") { 0, 1, 2 };
                IEnumerable enumerable = (IEnumerable)target;

                IEnumerator enumerator = enumerable.GetEnumerator();
                object current;
                int index = 0;

                while (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    Assert.AreEqual(target.Categories[index], (Category)current);
                    index++;
                }

                // reset 
                enumerator.Reset();

                // dispose
                enumerator = null;
                GC.Collect(10, GCCollectionMode.Forced);
            }

            // IEnumerable<Category>.GetEnumerator
            {
                var target = new CategoricalVariable("V") { 0, 1, 2 };
                IEnumerable<Category> enumerable = (IEnumerable<Category>)target;

                IEnumerator<Category> enumerator = enumerable.GetEnumerator();

                int index = 0;
                Category current;

                while (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    Assert.AreEqual(target.Categories[index], current);
                    index++;
                }

                // reset 
                enumerator.Reset();

                // dispose
                enumerator.Dispose();
            }

            // IEnumerable<Category>.Current returns null
            {
                var target = new CategoricalVariable("V") { 0, 1, 2 };
                var enumerable = (IEnumerable<Category>)target;

                var enumerator = enumerable.GetEnumerator();

                Assert.IsNull(enumerator.Current);
            }

            // IEnumerable.Current fails
            {
                string STR_EXCEPT_ENU_OUT_OF_BOUNDS =
                    "Enumeration has either not started or has already finished.";
                var target = new CategoricalVariable("V") { 0, 1, 2 };
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

            // valid input
            {
                var target = new CategoricalVariable("V")
                {
                    { 0, "A" },
                    { 1, "B" },
                    { 2, "C" }
                };

                Category[] expected = new Category[3] {
                    new Category(0, "A" ),
                    new Category(1, "B" ),
                    new Category(2, "C" )
                };

                int index = 0;
                foreach (var category in target)
                {
                    CategoryAssert.AreEqual(
                        expected: expected[index],
                        actual: category);
                    index++;
                }
            }
        }

        [TestMethod()]
        public void NameSetTest()
        {
            // value is null
            {
                string name = "The name";
                var target = new CategoricalVariable(name);

                ArgumentExceptionAssert.Throw(
                    () => { target.Name = null; },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "value");
            }

            // name is white space
            {
                string name = "The name";
                var target = new CategoricalVariable(name);

                ArgumentExceptionAssert.Throw(
                    () => { target.Name = " "; },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE"),
                    expectedParameterName: "value");
            }

            // name is empty
            {
                string name = "The name";
                var target = new CategoricalVariable(name);

                ArgumentExceptionAssert.Throw(
                    () => { target.Name = string.Empty; },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE"),
                    expectedParameterName: "value");
            }

            // target is read only
            {
                string name = "The name";
                var target = new CategoricalVariable(name);
                target.SetAsReadOnly();

                ExceptionAssert.Throw(
                    () => { target.Name = "New name"; },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CAT_VARIABLE_IS_READONLY"));
            }

            // Valid input
            {
                string name = "The name";
                var target = new CategoricalVariable(name);

                string expected, actual;

                expected = "New name";

                target.Name = expected;

                actual = target.Name;

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void ToStringTest()
        {
            // Without categories 
            {
                string name = "The name";
                var target = new CategoricalVariable(name);

                string expected, actual;

                expected = name + ": []";
                actual = target.ToString();

                Assert.AreEqual(expected, actual);
            }

            // With categories
            {
                string name = "The name";
                var target = new CategoricalVariable(name);

                string expected, actual;

                target.Add(0.0, "Zero");
                target.Add(1.0, "One");
                target.Add(2.0, "Two");

                expected = name + ": [0 - Zero, 1 - One, 2 - Two]";
                actual = target.ToString();

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod()]
        public void SetAsReadOnlyTest()
        {
            string expectedName = "The name";

            var target = new CategoricalVariable(expectedName);

            target.SetAsReadOnly();

            CategoricalVariableAssert.IsStateAsExpected(
                target,
                expectedName,
                expectedCategories: new List<Category>(),
                expectedReadOnlyFlag: true);
        }

        [TestMethod()]
        public void AddByCodeOnlyTest()
        {
            // code already exists
            {
                string name = "The name";
                var target = new CategoricalVariable(name)
                {
                    2.0
                };

                ExceptionAssert.Throw(
                    () => { target.Add(2.0); },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CAT_ALREADY_EXISTS_IN_VARIABLE_LIST"));
            }

            // target is read only
            {
                string name = "The name";
                var target = new CategoricalVariable(name);

                target.SetAsReadOnly();

                ExceptionAssert.Throw(
                    () => { target.Add(3.0); },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CAT_VARIABLE_IS_READONLY"));
            }

            // Valid input
            {
                string name = "The name";
                var target = new CategoricalVariable(name);

                double code;
                Category category0, category1;

                code = 1.0;
                target.Add(code);
                category0 = new Category(code);

                code = 2.0;
                target.Add(code);
                category1 = new Category(code);

                CategoricalVariableAssert.IsStateAsExpected(
                    target,
                    name,
                    expectedCategories: new List<Category>(2) {
                        category0,
                        category1 },
                    expectedReadOnlyFlag: false);
            }
        }

        [TestMethod()]
        public void AddTest()
        {
            // code already exists
            {
                string name = "The name";
                var target = new CategoricalVariable(name)
                {
                    2.0
                };

                ExceptionAssert.Throw(
                    () => { target.Add(2.0, "second two"); },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CAT_ALREADY_EXISTS_IN_VARIABLE_LIST"));
            }

            // label already exists
            {
                string name = "The name";
                var target = new CategoricalVariable(name)
                {
                    { 2.0, "two" }
                };

                ExceptionAssert.Throw(
                    () => { target.Add(2.000001, "two"); },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CAT_ALREADY_EXISTS_IN_VARIABLE_LIST"));
            }

            // label is null
            {
                string name = "The name";
                var target = new CategoricalVariable(name);

                ArgumentExceptionAssert.Throw(
                    () => { target.Add(3.0, null); },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "label");
            }

            // label is white space
            {
                string name = "The name";
                var target = new CategoricalVariable(name);

                ArgumentExceptionAssert.Throw(
                    () => { target.Add(3.0, " "); },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE"),
                    expectedParameterName: "label");
            }

            // label is empty
            {
                string name = "The name";
                var target = new CategoricalVariable(name);

                ArgumentExceptionAssert.Throw(
                    () => { target.Add(3.0, string.Empty); },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_STRING_IS_EMPTY_OR_WHITESPACE"),
                    expectedParameterName: "label");
            }

            // target is read only
            {
                string name = "The name";
                var target = new CategoricalVariable(name);

                target.SetAsReadOnly();

                ExceptionAssert.Throw(
                    () => { target.Add(3.0, "three"); },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage: ImplementationServices.GetResourceString(
                        "STR_EXCEPT_CAT_VARIABLE_IS_READONLY"));
            }

            // Valid input
            {
                string name = "The name";
                var target = new CategoricalVariable(name);

                double code;
                string label;
                Category category0, category1;

                code = 1.0;
                label = "one";
                target.Add(code, label);
                category0 = new Category(code, label);

                code = 2.0;
                label = "two";
                target.Add(code, label);
                category1 = new Category(code, label);

                CategoricalVariableAssert.IsStateAsExpected(
                    target,
                    name,
                    expectedCategories: new List<Category>(2) {
                        category0,
                        category1 },
                    expectedReadOnlyFlag: false);
            }
        }

        [TestMethod()]
        public void TryGetByCodeTest()
        {
            // Getting existing category
            {
                string name = "The name";
                var target = new CategoricalVariable(name)
                {
                    { 0.0, "Zero" },
                    { 1.0, "One" },
                    { 2.0, "Two" }
                };

                bool actualFlag, expectedFlag;
                double categoryCode;

                categoryCode = 1.0;

                actualFlag = target.TryGet(
                    categoryCode, out Category actualCategory);

                expectedFlag = true;
                Category expectedCategory = new Category(categoryCode, "One");

                Assert.AreEqual(expectedFlag, actualFlag);
                CategoryAssert.AreEqual(expectedCategory, actualCategory);
            }

            // Getting not existing category
            {
                string name = "The name";
                var target = new CategoricalVariable(name)
                {
                    { 0.0, "Zero" },
                    { 1.0, "One" },
                    { 2.0, "Two" }
                };

                Category expectedCategory;
                bool actualFlag, expectedFlag;
                double categoryCode;

                categoryCode = 3.0;

                actualFlag = target.TryGet(
                    categoryCode, out Category actualCategory);

                expectedFlag = false;
                expectedCategory = null;

                Assert.AreEqual(expectedFlag, actualFlag);
                CategoryAssert.AreEqual(expectedCategory, actualCategory);
            }
        }

        [TestMethod()]
        public void TryGetByLabelTest()
        {
            // Getting existing category
            {
                string name = "The name";
                var target = new CategoricalVariable(name)
                {
                    { 0.0, "Zero" },
                    { 1.0, "One" },
                    { 2.0, "Two" }
                };

                Category expectedCategory;
                bool actualFlag, expectedFlag;
                string categoryLabel;

                categoryLabel = "One";

                actualFlag = target.TryGet(
                    categoryLabel, out Category actualCategory);

                expectedFlag = true;
                expectedCategory = new Category(1.0, categoryLabel);

                Assert.AreEqual(expectedFlag, actualFlag);
                CategoryAssert.AreEqual(expectedCategory, actualCategory);
            }

            // Getting not existing category
            {
                string name = "The name";
                var target = new CategoricalVariable(name)
                {
                    { 0.0, "Zero" },
                    { 1.0, "One" },
                    { 2.0, "Two" }
                };

                Category expectedCategory;
                bool actualFlag, expectedFlag;
                string categoryLabel;

                categoryLabel = "Three";

                actualFlag = target.TryGet(
                    categoryLabel, out Category actualCategory);

                expectedFlag = false;
                expectedCategory = null;

                Assert.AreEqual(expectedFlag, actualFlag);
                CategoryAssert.AreEqual(expectedCategory, actualCategory);
            }
        }

        [TestMethod()]
        public void RemoveByCodeTest()
        {
            // Removing existing category
            {
                string name = "The name";
                var target = new CategoricalVariable(name)
                {
                    { 0.0, "Zero" },
                    { 1.0, "One" },
                    { 2.0, "Two" }
                };

                CategoricalVariable actualVariable, expectedVariable;
                bool actualFlag, expectedFlag;
                double categoryCode;

                categoryCode = 1.0;

                actualFlag = target.Remove(categoryCode);
                actualVariable = target;

                expectedFlag = true;

                expectedVariable = new CategoricalVariable(name)
                {
                    { 0.0, "Zero" },
                    { 2.0, "Two" }
                };

                Assert.AreEqual(expectedFlag, actualFlag);
                CategoricalVariableAssert.AreEqual(expectedVariable, actualVariable);
            }

            // Removing not existing category
            {
                string name = "The name";
                var target = new CategoricalVariable(name)
                {
                    { 0.0, "Zero" },
                    { 1.0, "One" },
                    { 2.0, "Two" }
                };

                CategoricalVariable actualVariable, expectedVariable;
                bool actualFlag, expectedFlag;
                double categoryCode;

                categoryCode = 3.0;

                actualFlag = target.Remove(categoryCode);
                actualVariable = target;

                expectedFlag = false;

                expectedVariable = new CategoricalVariable(name)
                {
                    { 0.0, "Zero" },
                    { 1.0, "One" },
                    { 2.0, "Two" }
                };

                Assert.AreEqual(expectedFlag, actualFlag);
                CategoricalVariableAssert.AreEqual(expectedVariable, actualVariable);
            }

            // Removing categories in read-only variables
            {
                string name = "The name";
                var target = new CategoricalVariable(name)
                {
                    { 0.0, "Zero" },
                    { 1.0, "One" },
                    { 2.0, "Two" }
                };

                target.SetAsReadOnly();

                ExceptionAssert.Throw(
                    () => { target.Remove(3.0); },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage: ImplementationServices.GetResourceString(
                           "STR_EXCEPT_CAT_VARIABLE_IS_READONLY"));

                ExceptionAssert.Throw(
                    () => { target.Remove(1.0); },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage: ImplementationServices.GetResourceString(
                           "STR_EXCEPT_CAT_VARIABLE_IS_READONLY"));
            }
        }

        [TestMethod()]
        public void RemoveByLabelTest()
        {

            // Removing existing category
            {
                string name = "The name";
                var target = new CategoricalVariable(name)
                {
                    { 0.0, "Zero" },
                    { 1.0, "One" },
                    { 2.0, "Two" }
                };

                CategoricalVariable actualVariable, expectedVariable;
                bool actualFlag, expectedFlag;
                string categoryLabel;

                categoryLabel = "One";

                actualFlag = target.Remove(categoryLabel);
                actualVariable = target;

                expectedFlag = true;

                expectedVariable = new CategoricalVariable(name)
                {
                    { 0.0, "Zero" },
                    { 2.0, "Two" }
                };

                Assert.AreEqual(expectedFlag, actualFlag);
                CategoricalVariableAssert.AreEqual(expectedVariable, actualVariable);
            }

            // Removing not existing category
            {
                string name = "The name";
                var target = new CategoricalVariable(name)
                {
                    { 0.0, "Zero" },
                    { 1.0, "One" },
                    { 2.0, "Two" }
                };

                CategoricalVariable actualVariable, expectedVariable;
                bool actualFlag, expectedFlag;
                string categoryLabel;

                categoryLabel = "Three";

                actualFlag = target.Remove(categoryLabel);
                actualVariable = target;

                expectedFlag = false;

                expectedVariable = new CategoricalVariable(name)
                {
                    { 0.0, "Zero" },
                    { 1.0, "One" },
                    { 2.0, "Two" }
                };

                Assert.AreEqual(expectedFlag, actualFlag);
                CategoricalVariableAssert.AreEqual(expectedVariable, actualVariable);
            }

            // label is null
            {
                string name = "The name";
                var target = new CategoricalVariable(name)
                {
                    { 0.0, "Zero" },
                    { 1.0, "One" },
                    { 2.0, "Two" }
                };

                ArgumentExceptionAssert.Throw(
                    () => { target.Remove(null); },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "label");
            }

            // Removing categories in read-only variables
            {
                string name = "The name";
                var target = new CategoricalVariable(name)
                {
                    { 0.0, "Zero" },
                    { 1.0, "One" },
                    { 2.0, "Two" }
                };

                target.SetAsReadOnly();

                ExceptionAssert.Throw(
                    () => { target.Remove("One"); },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage: ImplementationServices.GetResourceString(
                           "STR_EXCEPT_CAT_VARIABLE_IS_READONLY"));

                ExceptionAssert.Throw(
                    () => { target.Remove("Three"); },
                    expectedType: typeof(InvalidOperationException),
                    expectedMessage: ImplementationServices.GetResourceString(
                           "STR_EXCEPT_CAT_VARIABLE_IS_READONLY"));
            }
        }

        [TestMethod()]
        public void ToDoubleMatrixByOperatorTest()
        {
            // value is null
            {
                CategoricalVariable target = null;

                ArgumentExceptionAssert.Throw(
                    () => { DoubleMatrix result = (DoubleMatrix)target; },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "value");
            }

            // Valid input
            {
                string name = "The name";
                var target = new CategoricalVariable(name)
                {
                    { 0.0, "Zero" },
                    { 1.0, "One" },
                    { 2.0, "Two" }
                };

                var expected = DoubleMatrix.Dense(3, 1,
                    new double[3] { 0.0, 1.0, 2.0 });
                expected.Name = target.Name;
                expected.SetRowName(0, "Zero");
                expected.SetRowName(1, "One");
                expected.SetRowName(2, "Two");

                DoubleMatrix actual = (DoubleMatrix)target;

                DoubleMatrixAssert.AreEqual(expected, actual, 1e-2);
            }
        }

        [TestMethod()]
        public void FromDoubleMatrixByOperatorTest()
        {
            // With named rows
            {
                string name = "The name";

                var target = DoubleMatrix.Dense(3, 1,
                    new double[3] { 0.0, 1.0, 2.0 });
                target.Name = name;
                target.SetRowName(0, "Zero");
                target.SetRowName(1, "One");
                target.SetRowName(2, "Two");

                CategoricalVariable actual = (CategoricalVariable)target;

                var expected = new CategoricalVariable(name)
                {
                    { 0.0, "Zero" },
                    { 1.0, "One" },
                    { 2.0, "Two" }
                };

                CategoricalVariableAssert.AreEqual(expected, actual);
            }

            // With unnamed rows
            {
                string name = "The name";

                var target = DoubleMatrix.Dense(3, 1,
                    new double[3] { 0.0, 1.0, 2.0 });
                target.Name = name;

                var actual = (CategoricalVariable)target;

                var expected = new CategoricalVariable(name)
                {
                    0.0,
                    1.0,
                    2.0
                };

                CategoricalVariableAssert.AreEqual(expected, actual);
            }

            // value is null
            {
                DoubleMatrix target = null;

                ArgumentExceptionAssert.Throw(
                    () => { CategoricalVariable result = (CategoricalVariable)target; },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "value");
            }

            // value is not a column vector
            {
                var target = DoubleMatrix.Dense(2, 3);

                ArgumentExceptionAssert.Throw(
                    () => { CategoricalVariable result = (CategoricalVariable)target; },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_COLUMN_VECTOR"),
                    expectedParameterName: "value");
            }
        }

        [TestMethod()]
        public void ToDoubleMatrixByMethodTest()
        {
            // value is null
            {
                CategoricalVariable target = null;

                ArgumentExceptionAssert.Throw(
                    () => { var result = CategoricalVariable.ToDoubleMatrix(target); },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "value");
            }

            // Valid input
            {
                string name = "The name";
                var target = new CategoricalVariable(name)
                {
                    { 0.0, "Zero" },
                    { 1.0, "One" },
                    { 2.0, "Two" }
                };

                var expected = DoubleMatrix.Dense(3, 1,
                    new double[3] { 0.0, 1.0, 2.0 });
                expected.Name = target.Name;
                expected.SetRowName(0, "Zero");
                expected.SetRowName(1, "One");
                expected.SetRowName(2, "Two");

                DoubleMatrix actual = CategoricalVariable.ToDoubleMatrix(target);

                DoubleMatrixAssert.AreEqual(expected, actual, 1e-2);
            }
        }

        [TestMethod()]
        public void FromDoubleMatrixByMethodTest()
        {
            // With named rows
            {
                string name = "The name";

                var target = DoubleMatrix.Dense(3, 1,
                    new double[3] { 0.0, 1.0, 2.0 });
                target.Name = name;
                target.SetRowName(0, "Zero");
                target.SetRowName(1, "One");
                target.SetRowName(2, "Two");

                var actual = CategoricalVariable.FromDoubleMatrix(target);

                var expected = new CategoricalVariable(name)
                {
                    { 0.0, "Zero" },
                    { 1.0, "One" },
                    { 2.0, "Two" }
                };

                CategoricalVariableAssert.AreEqual(expected, actual);
            }

            // With unnamed rows
            {
                string name = "The name";

                var target = DoubleMatrix.Dense(3, 1,
                    new double[3] { 0.0, 1.0, 2.0 });
                target.Name = name;

                var actual = CategoricalVariable.FromDoubleMatrix(target);

                var expected = new CategoricalVariable(name)
                {
                    0.0,
                    1.0,
                    2.0
                };

                CategoricalVariableAssert.AreEqual(expected, actual);
            }

            // value is null
            {
                DoubleMatrix target = null;

                ArgumentExceptionAssert.Throw(
                    () => { var result = CategoricalVariable.FromDoubleMatrix(target); },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage: ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "value");
            }

            // value is not a column vector
            {
                var target = DoubleMatrix.Dense(2, 3);

                ArgumentExceptionAssert.Throw(
                    () => { var result = CategoricalVariable.FromDoubleMatrix(target); },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_COLUMN_VECTOR"),
                    expectedParameterName: "value");
            }
        }
    }
}