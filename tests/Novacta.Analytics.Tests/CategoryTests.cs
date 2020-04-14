// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Tests.Tools;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Novacta.Analytics.Tests
{
    [TestClass()]
    public class CategoryTests
    {
        [TestMethod]
        public void GetObjectDataTest()
        {
            // info is null
            {
                Category category = new Category(0, "A");

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        category.GetObjectData(
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
                TypeInfo t = typeof(Category).GetTypeInfo();
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

                Category serializedCategory = new Category(0, "A");

                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(ms, serializedCategory);

                // Reset stream position
                ms.Position = 0;

                var deserializedCategory = (Category)formatter.Deserialize(ms);

                CategoryAssert.AreEqual(
                    expected: serializedCategory,
                    actual: deserializedCategory);
            }
        }

        [TestMethod()]
        public void ConstructorTest()
        {
            double expectedCode = 1.2;
            string expectedLabel = "The label";

            var target = new Category(expectedCode, expectedLabel);

            CategoryAssert.IsStateAsExpected(
                target,
                expectedCode,
                expectedLabel);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            double code = 1.2;
            string label = "The label";

            var target = new Category(code, label);

            string expected, actual;

            expected = target.Code.ToString(CultureInfo.InvariantCulture) +
                " - " + target.Label;
            actual = target.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}