// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Reflection;
using System.Linq;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Contains methods to retrieve information about instances 
    /// and types, to manipulate instances and invoke methods.
    /// </summary>
    static class Reflector
    {
        /// <summary>
        /// Executes the specified static method in the given type.
        /// </summary>
        /// <param name="type">The type supporting the method.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="methodArgs">The method arguments.</param>
        /// <returns>Object.</returns>
        public static Object ExecuteStaticMember(
            Type type, 
            string methodName,
            object[] methodArgs)
        {
            var typeInfo = type.GetTypeInfo();
            var members = typeInfo.DeclaredMethods;
            var query = from m in members where m.Name == methodName && m.IsStatic select m;
            var method = query.First();
            return method.Invoke(null, methodArgs);
        }

        /// <summary>
        /// Executes the specified method in the given object.
        /// </summary>
        /// <param name="obj">The object whose type supports the method.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="methodArgs">The method arguments.</param>
        /// <returns>Object.</returns>
        public static Object ExecuteMember(
            object obj,
            string methodName,
            object[] methodArgs)
        {
            Type targetType = obj.GetType();

            var typeInfo = targetType.GetTypeInfo();
            var members = typeInfo.DeclaredMethods;
            var query = from m in members where m.Name == methodName select m;
            var method = query.First();
            return method.Invoke(obj, methodArgs);
        }

        /// <summary>
        /// Gets the value of a property having the specified name
        /// in the given object.
        /// </summary>
        /// <param name="obj">The object whose type supports the property.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The value of the property.</returns>
        public static Object GetProperty(object obj, string propertyName)
        {
            Type targetType = obj.GetType();
            var typeInfo = targetType.GetTypeInfo();
            var properties = typeInfo.DeclaredProperties;
            var query = from p in properties where p.Name == propertyName select p;
            var property = query.First();
            MethodInfo getMethod = property.GetMethod;
            return getMethod.Invoke(obj, null);
        }

        /// <summary>
        /// Gets the value of a field having the specified name
        /// in the given object.
        /// </summary>
        /// <param name="obj">The object whose type supports the field.</param>
        /// <param name="fieldName">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        public static Object GetField(object obj, string fieldName)
        {
            Type targetType = obj.GetType();
            var typeInfo = targetType.GetTypeInfo();
            var fields = typeInfo.DeclaredFields;
            var query = from f in fields where f.Name == fieldName select f;
            var field = query.First();
            return field.GetValue(obj);
        }

        /// <summary>
        /// Executes the specified method in the base of the given object.
        /// </summary>
        /// <param name="obj">The object whose base type supports the method.</param>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="methodArgs">The method arguments.</param>
        /// <returns>Object.</returns>
        public static Object ExecuteBaseMember(
            object obj,
            string methodName,
            object[] methodArgs)
        {
            Type targetType = obj.GetType();

            var typeInfo = targetType.GetTypeInfo();
            var members = typeInfo.BaseType.GetTypeInfo().DeclaredMethods;
            var query = from m in members where m.Name == methodName select m;
            var method = query.First();
            return method.Invoke(obj, methodArgs);
        }

        /// <summary>
        /// Gets the value of a field having the specified name
        /// in the base of the given object.
        /// </summary>
        /// <param name="obj">The object whose base type supports the field.</param>
        /// <param name="fieldName">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        public static object GetBaseField(object obj, string FieldName)
        {
            Type targetType = obj.GetType();
            var typeInfo = targetType.GetTypeInfo();
            var fields = typeInfo.BaseType.GetTypeInfo().DeclaredFields;
            var query = from f in fields where f.Name == FieldName select f;
            var field = query.First();
            return field.GetValue(obj);
        }

        /// <summary>
        /// Sets the value of a field having the specified name
        /// in the base of the given object.
        /// </summary>
        /// <param name="obj">The object whose type supports the field.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="fieldName">The name of the field.</param>
        public static void SetField(object obj, object value, string fieldName)
        {
            Type targetType = obj.GetType();
            var typeInfo = targetType.GetTypeInfo();
            var fields = typeInfo.DeclaredFields;
            var query = from f in fields where f.Name == fieldName select f;
            var field = query.First();
            field.SetValue(obj, value);
        }
    }
}
