// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Tests.Tools;
using Novacta.Analytics.Tests.TestableItems.SVD;


namespace Novacta.Analytics.Advanced.Tests
{
    [TestClass()]
    public class SingularValueDecompositionTests
    {
        [TestMethod]
        public void DecomposeTest()
        {
            // matrix is null
            SingularValueDecompositionTest.Decompose.MatrixIsNull();

            // matrix is real
            SingularValueDecompositionTest.Decompose.Succeed(
                TestableSingularValueDecomposition00.Get());

            // matrix is complex
            SingularValueDecompositionTest.Decompose.Succeed(
                TestableSingularValueDecomposition01.Get());
        }

        [TestMethod]
        public void GetSingularValuesTest()
        {
            // matrix is null
            SingularValueDecompositionTest.GetSingularValues.MatrixIsNull();

            // matrix is real
            SingularValueDecompositionTest.GetSingularValues.Succeed(
                TestableSingularValueDecomposition00.Get());

            // matrix is complex
            SingularValueDecompositionTest.GetSingularValues.Succeed(
                TestableSingularValueDecomposition01.Get());
        }
    }
}
