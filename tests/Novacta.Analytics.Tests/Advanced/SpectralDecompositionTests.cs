// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Tests.Tools;
using Novacta.Analytics.Tests.TestableItems.SD;


namespace Novacta.Analytics.Advanced.Tests
{
    [TestClass()]
    public class SpectralDecompositionTests
    {
        [TestMethod]
        public void DecomposeTest()
        {
            // matrix is null
            SpectralDecompositionTest.Decompose.MatrixIsNull();

            // matrix is not square
            SpectralDecompositionTest.Decompose.MatrixIsNotSquare();

            // matrix is real
            SpectralDecompositionTest.Decompose.Succeed(
                TestableSpectralDecomposition00.Get());

            // matrix is complex
            SpectralDecompositionTest.Decompose.Succeed(
                TestableSpectralDecomposition01.Get());
        }

        [TestMethod]
        public void GetEigenvaluesTest()
        {
            // matrix is null
            SpectralDecompositionTest.GetEigenvalues.MatrixIsNull();

            // matrix is not square
            SpectralDecompositionTest.GetEigenvalues.MatrixIsNotSquare();

            // matrix is real
            SpectralDecompositionTest.GetEigenvalues.Succeed(
                TestableSpectralDecomposition00.Get());

            // matrix is complex
            SpectralDecompositionTest.GetEigenvalues.Succeed(
                TestableSpectralDecomposition01.Get());
        }
    }
}
