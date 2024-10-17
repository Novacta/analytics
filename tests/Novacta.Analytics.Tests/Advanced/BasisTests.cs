// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Advanced.Tests
{
    [TestClass]
    public class BasisTests
    {
        [TestMethod]
        public void StandardTest()
        {
            // dimension is not positive
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var basis = Basis.Standard(0);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage: ImplementationServices.GetResourceString(
                                                "STR_EXCEPT_PAR_MUST_BE_POSITIVE"),
                    expectedParameterName: "dimension");
            }

            // dimension is valid
            {
                var basis = Basis.Standard(2);
                var actual = basis.GetBasisMatrix();
                var expected = DoubleMatrix.Identity(2);

                DoubleMatrixAssert.AreEqual(
                    expected,
                    actual,
                    DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void BasisTest()
        {
            // basisMatrix is null
            {
                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var basis = new Basis(null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "basisMatrix");
            }

            // basisMatrix is not square
            {
                var basisMatrix = DoubleMatrix.Dense(2, 3);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var basis = new Basis(basisMatrix);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        ImplementationServices.GetResourceString(
                    "STR_EXCEPT_PAR_MUST_BE_SQUARE"),
                    expectedParameterName: "basisMatrix");
            }

            // basisMatrix is singular
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 2, 2, 4]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var basis = new Basis(basisMatrix);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        ImplementationServices.GetResourceString(
                    "STR_EXCEPT_PAR_MUST_BE_NON_SINGULAR"),
                    expectedParameterName: "basisMatrix");
            }

            // basisMatrix is valid (dense)
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 1, -1, 2]);

                var basis = new Basis(basisMatrix);

                var actual = basis.GetBasisMatrix();
                var expected = basisMatrix;

                DoubleMatrixAssert.AreEqual(
                    expected,
                    actual,
                    DoubleMatrixTest.Accuracy);

                Assert.AreEqual(2, basis.Dimension);
            }

            // basisMatrix is valid (sparse)
            {
                var basisMatrix = DoubleMatrix.Identity(2);

                var basis = new Basis(basisMatrix);

                var actual = basis.GetBasisMatrix();
                var expected = basisMatrix;

                DoubleMatrixAssert.AreEqual(
                    expected,
                    actual,
                    DoubleMatrixTest.Accuracy);

                Assert.AreEqual(2, basis.Dimension);
            }
        }

        [TestMethod]
        public void GetCoordinatesTest()
        {
            // vectors is null
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 1, -1, 2]);
                var basis = new Basis(basisMatrix);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var coordinates = basis.GetCoordinates(null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "vectors");
            }

            // vectors is not a basis compliant matrix
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 1, -1, 2]);
                var basis = new Basis(basisMatrix);

                var vectors = DoubleMatrix.Dense(2, 3);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var coordinates = basis.GetCoordinates(vectors);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        ImplementationServices.GetResourceString(
                    "STR_EXCEPT_PAR_MUST_BE_BASIS_COMPLIANT_MATRIX"),
                    expectedParameterName: "vectors");
            }

            // vectors is valid
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 1, -1, 2]);
                var basis = new Basis(basisMatrix);

                var vectors = DoubleMatrix.Dense(1, 2, [1, 0]);

                var coordinates = basis.GetCoordinates(vectors);

                var expected = DoubleMatrix.Dense(1, 2,
                    [2.0 / 3.0, -1.0 / 3.0]);
                var actual = coordinates;

                DoubleMatrixAssert.AreEqual(
                    expected,
                    actual,
                    DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void ChangeCoordinatesTest()
        {
            // newBasis is null
            {
                Basis newBasis = null;
                var currentCoordinates = DoubleMatrix.Dense(1, 2,
                    [2.0 / 3.0, -1.0 / 3.0]);
                var currentBasisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 1, -1, 2]);
                Basis currentBasis = new(currentBasisMatrix);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var newCoordinates = Basis.ChangeCoordinates(
                            newBasis,
                            currentCoordinates,
                            currentBasis);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "newBasis");
            }

            // currentCoordinates is null
            {
                Basis newBasis = Basis.Standard(2);
                DoubleMatrix currentCoordinates = null;
                var currentBasisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 1, -1, 2]);
                Basis currentBasis = new(currentBasisMatrix);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var newCoordinates = Basis.ChangeCoordinates(
                            newBasis,
                            currentCoordinates,
                            currentBasis);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "currentCoordinates");
            }

            // currentBasis is null
            {
                Basis newBasis = Basis.Standard(2);
                var currentCoordinates = DoubleMatrix.Dense(1, 2,
                    [2.0 / 3.0, -1.0 / 3.0]);
                Basis currentBasis = null;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var newCoordinates = Basis.ChangeCoordinates(
                            newBasis,
                            currentCoordinates,
                            currentBasis);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "currentBasis");
            }

            // newBasis and currentBasis do not share the same dimension
            {
                Basis newBasis = Basis.Standard(1);
                var currentCoordinates = DoubleMatrix.Dense(1, 2,
                    [2.0 / 3.0, -1.0 / 3.0]);
                var currentBasisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 1, -1, 2]);
                Basis currentBasis = new(currentBasisMatrix);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var newCoordinates = Basis.ChangeCoordinates(
                            newBasis,
                            currentCoordinates,
                            currentBasis);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_BASES_MUST_SHARE_DIMENSION"),
                    expectedParameterName: "newBasis");
            }

            // currentCoordinates has count unequal to basis dimension
            {
                Basis newBasis = Basis.Standard(2);
                var currentCoordinates = DoubleMatrix.Dense(1, 3);
                var currentBasisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 1, -1, 2]);
                Basis currentBasis = new(currentBasisMatrix);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var newCoordinates = Basis.ChangeCoordinates(
                            newBasis,
                            currentCoordinates,
                            currentBasis);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_BASIS_COMPLIANT_MATRIX"),
                    expectedParameterName: "currentCoordinates");
            }

            // all parameters are valid
            {
                // See example 4, p. 44, Linear Algebra, Lang.

                var newBasis = Basis.Standard(2);

                var currentCoordinates = DoubleMatrix.Dense(1, 2,
                    [2.0 / 3.0, -1.0 / 3.0]);

                var currentBasisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 1, -1, 2]);
                var currentBasis = new Basis(currentBasisMatrix);

                var newCoordinates = Basis.ChangeCoordinates(
                    newBasis,
                    currentCoordinates,
                    currentBasis);

                var expected = DoubleMatrix.Dense(1, 2, [1, 0]);
                var actual = newCoordinates;

                DoubleMatrixAssert.AreEqual(
                    expected,
                    actual,
                    DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void GetVectorsTest()
        {
            // coordinates is null
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 1, -1, 2]);
                var basis = new Basis(basisMatrix);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var vectors = basis.GetVectors(null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "coordinates");
            }

            // coordinates is not a basis compliant matrix
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 1, -1, 2]);
                var basis = new Basis(basisMatrix);

                var coordinates = DoubleMatrix.Dense(2, 3);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var vectors = basis.GetVectors(coordinates);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        ImplementationServices.GetResourceString(
                    "STR_EXCEPT_PAR_MUST_BE_BASIS_COMPLIANT_MATRIX"),
                    expectedParameterName: "coordinates");
            }

            // coordinates is valid
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 1, -1, 2]);
                var basis = new Basis(basisMatrix);

                var coordinates = DoubleMatrix.Dense(1, 2,
                     [2.0 / 3.0, -1.0 / 3.0]);

                var vectors = basis.GetVectors(coordinates);

                var expected = DoubleMatrix.Dense(1, 2,
                    [1, 0]);
                var actual = vectors;

                DoubleMatrixAssert.AreEqual(
                    expected,
                    actual,
                    DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void NormTest()
        {
            // coordinates is null
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 0, 0, 2]);
                var basis = new Basis(basisMatrix);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var norm = basis.Norm(null);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "coordinates");
            }

            // coordinates is not a row vector
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 0, 0, 2]);
                var basis = new Basis(basisMatrix);

                var coordinates = DoubleMatrix.Dense(4, 2);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var norm = basis.Norm(coordinates);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        ImplementationServices.GetResourceString(
                    "STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR"),
                    expectedParameterName: "coordinates");
            }

            // coordinates is not a basis compliant vector
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 0, 0, 2]);
                var basis = new Basis(basisMatrix);

                var coordinates = DoubleMatrix.Dense(1, 3);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var norm = basis.Norm(coordinates);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        ImplementationServices.GetResourceString(
                    "STR_EXCEPT_PAR_MUST_BE_BASIS_COMPLIANT_VECTOR"),
                    expectedParameterName: "coordinates");
            }

            // coordinates is valid
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 0, 0, 2]);
                var basis = new Basis(basisMatrix);

                var coordinates = DoubleMatrix.Dense(1, 2, 
                    [2.0, -1.0]);

                // [ 2 -1 ] * [ 1  0   *  [ 2    = [ 2 -1 ] * [ 2   = 8
                //              0  4 ]     -1 ]                -4 ]
                var norm = basis.Norm(coordinates);

                var expected = 8.0;
                var actual = norm;

                Assert.AreEqual(
                    expected,
                    actual,
                    DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void ScalarProductTest()
        {
            // left is null
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 0, 0, 2]);
                var basis = new Basis(basisMatrix);

                DoubleMatrix left = null;
                var right = DoubleMatrix.Dense(1, 2, 
                    [3, 2]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var scalarProduct = basis.ScalarProduct(left, right);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "left");
            }

            // left is not a row vector
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 0, 0, 2]);
                var basis = new Basis(basisMatrix);

                var left = DoubleMatrix.Dense(4, 2);
                var right = DoubleMatrix.Dense(1, 2,
                    [3, 2]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var scalarProduct = basis.ScalarProduct(left, right);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        ImplementationServices.GetResourceString(
                    "STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR"),
                    expectedParameterName: "left");
            }

            // left is not a basis compliant vector
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 0, 0, 2]);
                var basis = new Basis(basisMatrix);

                var left = DoubleMatrix.Dense(1, 3);
                var right = DoubleMatrix.Dense(1, 2,
                    [3, 2]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var scalarProduct = basis.ScalarProduct(left, right);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        ImplementationServices.GetResourceString(
                    "STR_EXCEPT_PAR_MUST_BE_BASIS_COMPLIANT_VECTOR"),
                    expectedParameterName: "left");
            }

            // right is null
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 0, 0, 2]);
                var basis = new Basis(basisMatrix);

                var left = DoubleMatrix.Dense(1, 2,
                    [3, 2]);
                DoubleMatrix right = null;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var scalarProduct = basis.ScalarProduct(left, right);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "right");
            }

            // right is not a row vector
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 0, 0, 2]);
                var basis = new Basis(basisMatrix);

                var left = DoubleMatrix.Dense(1, 2,
                    [3, 2]);
                var right = DoubleMatrix.Dense(4, 2);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var scalarProduct = basis.ScalarProduct(left, right);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        ImplementationServices.GetResourceString(
                    "STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR"),
                    expectedParameterName: "right");
            }

            // right is not a basis compliant vector
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 0, 0, 2]);
                var basis = new Basis(basisMatrix);

                var left = DoubleMatrix.Dense(1, 2,
                    [3, 2]);
                var right = DoubleMatrix.Dense(1, 3);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var scalarProduct = basis.ScalarProduct(left, right);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        ImplementationServices.GetResourceString(
                    "STR_EXCEPT_PAR_MUST_BE_BASIS_COMPLIANT_VECTOR"),
                    expectedParameterName: "right");
            }

            // valid input
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 0, 0, 2]);
                var basis = new Basis(basisMatrix);

                var left = DoubleMatrix.Dense(1, 2, [2.0, -1.0]);
                var right = DoubleMatrix.Dense(1, 2, [3, -2]);

                // [ 2 -1 ] * [ 1  0   *  [ 3    = [ 2 -1 ] * [ 3   = 14
                //              0  4 ]     -2 ]                -8 ]
                var scalarProduct = basis.ScalarProduct(left, right);

                var expected = 14.0;
                var actual = scalarProduct;

                Assert.AreEqual(
                    expected, 
                    actual, 
                    DoubleMatrixTest.Accuracy);
            }
        }

        [TestMethod]
        public void DistanceTest()
        {
            // left is null
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 0, 0, 2]);
                var basis = new Basis(basisMatrix);

                DoubleMatrix left = null;
                var right = DoubleMatrix.Dense(1, 2,
                    [3, 2]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var distance = basis.Distance(left, right);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "left");
            }

            // left is not a row vector
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 0, 0, 2]);
                var basis = new Basis(basisMatrix);

                var left = DoubleMatrix.Dense(4, 2);
                var right = DoubleMatrix.Dense(1, 2,
                    [3, 2]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var distance = basis.Distance(left, right);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        ImplementationServices.GetResourceString(
                    "STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR"),
                    expectedParameterName: "left");
            }

            // left is not a basis compliant vector
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 0, 0, 2]);
                var basis = new Basis(basisMatrix);

                var left = DoubleMatrix.Dense(1, 3);
                var right = DoubleMatrix.Dense(1, 2,
                    [3, 2]);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var distance = basis.Distance(left, right);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        ImplementationServices.GetResourceString(
                    "STR_EXCEPT_PAR_MUST_BE_BASIS_COMPLIANT_VECTOR"),
                    expectedParameterName: "left");
            }

            // right is null
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 0, 0, 2]);
                var basis = new Basis(basisMatrix);

                var left = DoubleMatrix.Dense(1, 2,
                    [3, 2]);
                DoubleMatrix right = null;

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var distance = basis.Distance(left, right);
                    },
                    expectedType: typeof(ArgumentNullException),
                    expectedPartialMessage:
                        ArgumentExceptionAssert.NullPartialMessage,
                    expectedParameterName: "right");
            }

            // right is not a row vector
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 0, 0, 2]);
                var basis = new Basis(basisMatrix);

                var left = DoubleMatrix.Dense(1, 2,
                    [3, 2]);
                var right = DoubleMatrix.Dense(4, 2);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var distance = basis.Distance(left, right);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        ImplementationServices.GetResourceString(
                    "STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR"),
                    expectedParameterName: "right");
            }

            // right is not a basis compliant vector
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 0, 0, 2]);
                var basis = new Basis(basisMatrix);

                var left = DoubleMatrix.Dense(1, 2,
                    [3, 2]);
                var right = DoubleMatrix.Dense(1, 3);

                ArgumentExceptionAssert.Throw(
                    () =>
                    {
                        var distance = basis.Distance(left, right);
                    },
                    expectedType: typeof(ArgumentOutOfRangeException),
                    expectedPartialMessage:
                        ImplementationServices.GetResourceString(
                    "STR_EXCEPT_PAR_MUST_BE_BASIS_COMPLIANT_VECTOR"),
                    expectedParameterName: "right");
            }

            // valid input
            {
                var basisMatrix = DoubleMatrix.Dense(2, 2,
                    [1, 0, 0, 2]);
                var basis = new Basis(basisMatrix);

                var left = DoubleMatrix.Dense(1, 2, [2.0, -1.0]);
                var right = DoubleMatrix.Dense(1, 2, [3, -2]);

                // ([ 2 -1 ] - [ 3 -2 ]) * [ 1  0   * ( [ 2   - [ 3   ) =
                //                           0  4 ]      -1 ]    -2 ]    
                //
                // [-1 1 ] * [ 1  0   * [ -1   = [-1 1 ] * [ -1    = 5
                //             0  4 ]      1 ]                4 ]
                var distance = basis.Distance(left, right);

                var expected = Math.Sqrt(5.0);
                var actual = distance;

                Assert.AreEqual(
                    expected,
                    actual,
                    DoubleMatrixTest.Accuracy);
            }
        }
    }
}