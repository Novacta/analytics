// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Interop;

namespace Novacta.Analytics.Advanced
{
    /// <summary>
    /// Represents a basis for finite dimensional real vector spaces.
    /// </summary>
    /// <remarks>
    /// <para>
    /// In a finite vector space having dimension <latex>K</latex>, 
    /// a collection <latex>\A</latex> of <latex>K</latex> linearly 
    /// independent vectors
    /// <latex>\alpha_1,\dots,\alpha_K</latex> is a basis of that space. 
    /// Each basis can 
    /// be represented by a matrix whose columns are given by the vectors
    /// in the basis:
    /// <latex mode="display">
    /// A = \mx{ \alpha_1 &amp; \cdots &amp;\alpha_K}.
    /// </latex>
    /// The identity matrix <latex>I_K</latex> represents the <i>standard</i> 
    /// basis of the vector space, a kind of basis which can be instantiated by 
    /// calling the static method <see cref="Standard">Standard</see>.
    /// Or, given a matrix representation <latex>A</latex>, the 
    /// corresponding <see cref="Basis"/> 
    /// can be instantiated by calling
    /// its <see cref="Basis(DoubleMatrix)">constructor</see>.
    /// The matrix representation of an instance is returned 
    /// by method <see cref="GetBasisMatrix"/>.
    /// </para>
    /// <para><b>Vectors and coordinates</b></para>
    /// <para>
    /// Each vector can be represented by its coordinates with respect to
    /// the <see cref="Basis"/>. You can evaluate the coordinates of a given
    /// set of vectors by 
    /// calling <see cref="GetCoordinates">GetCoordinates</see>,
    /// or determine 
    /// the vectors corresponding to given coordinates by calling 
    /// <see cref="GetVectors">GetVectors</see>. 
    /// </para>
    /// <para>
    /// If both the coordinates of a vector 
    /// and the corresponding basis representation are known, then the 
    /// vector coordinates 
    /// with respect to a new basis can be computed by calling the
    /// static method <see cref="ChangeCoordinates">ChangeCoordinates</see>.
    /// </para>
    /// <para><b>Scalar products, norms, distances</b></para>
    /// A <see cref="Basis"/> endows a vector space with a scalar product, 
    /// which in turn induces the definition of a norm for a vector 
    /// and that of a distance between vectors. 
    /// Such quantities can be computed by
    /// calling methods <see cref="ScalarProduct">ScalarProduct</see>,
    /// <see cref="Distance">Distance</see>, and 
    /// <see cref="Norm">Norm</see>, respectively. All such functions
    /// require, as arguments, the coordinates of the vectors under study.
    /// </remarks>
    /// <seealso href="http://en.wikipedia.org/wiki/Basis_(linear_algebra)"/>
    public class Basis
    {
        #region State

        /// <summary>
        /// The transposed of the basis representation matrix.
        /// </summary>
        /// <remarks>The basis representation matrix is the one whose 
        /// columns are given by the basis vectors.</remarks>
        internal DoubleMatrix basisMatrixT;  

        /// <summary>
        /// The matrix of the scalar products among basis vectors.
        /// </summary>
        internal DoubleMatrix basisScalarProducts;

        /// <summary>
        /// Gets the dimension of the <see cref="Basis"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If this instance represents a basis for a vector space having 
        /// dimension <latex>K</latex>,
        /// then property <see cref="Dimension"/> returns
        /// <latex>K</latex>. 
        /// </para>
        /// </remarks>
        /// <value>The dimension of the vector space
        /// for which this instance is a basis.</value>
        public int Dimension { get { return this.basisMatrixT.NumberOfRows; } }

        /// <summary>
        /// Gets the matrix representation of the <see cref="Basis"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If this instance represents a basis,
        /// say <latex>\A</latex>, formed by 
        /// the <latex>K</latex> linearly independent vectors
        /// <latex>\alpha_1,\dots,\alpha_K</latex>, then 
        /// method <see cref="GetBasisMatrix"/> returns matrix
        /// <latex mode="display">
        /// A = \mx{ \alpha_1 &amp; \hdots &amp;\alpha_K}.
        /// </latex>
        /// </para>
        /// </remarks>
        /// <returns>A matrix whose columns are the basis vectors.</returns>
        public DoubleMatrix GetBasisMatrix()
        {
            return this.basisMatrixT.Transpose();
        }

        #endregion

        #region Constructors and factory methods

        /// <summary>
        /// Prevents a default instance of the <see cref="Basis"/> class 
        /// from being created.
        /// </summary>
        /// <remarks>
        /// It is used only in the static 
        /// method <seealso cref="Standard"/>, to avoid 
        /// unnecessary input validation.
        /// </remarks>
        private Basis()
        { }

        /// <summary>
        /// Throws if a basis matrix is singular.
        /// </summary>
        /// <param name="basisMatrix">The basis matrix.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="basisMatrix"/> is singular.
        /// </exception>
        private static void ThrowIfBasisMatrixIsSingular(
            DoubleMatrix basisMatrix)
        {
            double[] basisArray = basisMatrix.AsColumnMajorDenseArray();
            int k = basisMatrix.NumberOfColumns;
            int[] ipiv = new int[k];

            int lapackInfo;
            lapackInfo = SafeNativeMethods.LAPACK.DGETRF(
                SafeNativeMethods.LAPACK.ORDER.ColMajor,
                k,
                k,
                basisArray,
                k,
                ipiv);

            if (0 < lapackInfo) {
                throw new ArgumentOutOfRangeException(
                    nameof(basisMatrix),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_NON_SINGULAR"));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Basis"/> class.
        /// </summary>
        /// <param name="basisMatrix">The matrix representation of the basis.
        /// </param>
        /// <remarks>
        /// <para>
        /// In a finite vector space having dimension <latex>K</latex>, a basis,
        /// say <latex>\A</latex>, 
        /// is a collection of <latex>K</latex> linearly independent vectors
        /// <latex>\alpha_1,\dots,\alpha_K</latex>. Each basis can 
        /// be represented by a matrix whose columns are given by the vectors
        /// in the basis:
        /// <latex mode="display">
        /// A = \mx{ \alpha_1 &amp; \cdots &amp;\alpha_K}.
        /// </latex>
        /// Given a matrix representation <latex>A</latex>, a 
        /// corresponding <see cref="Basis"/> instance
        /// can be created by passing <latex>A</latex> as 
        /// parameter <paramref name="basisMatrix"/>.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="basisMatrix"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="basisMatrix"/> is not square.<br/>
        /// -or-<br/>
        /// <paramref name="basisMatrix"/> is singular.
        /// </exception>
        public Basis(DoubleMatrix basisMatrix)
        {
            #region Input validation

            if (basisMatrix is null) {
                throw new ArgumentNullException(
                    nameof(basisMatrix));
            }

            if (!basisMatrix.IsSquare) {
                throw new ArgumentOutOfRangeException(
                    nameof(basisMatrix),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_SQUARE"));
            }

            Basis.ThrowIfBasisMatrixIsSingular(basisMatrix);

            #endregion

            this.basisMatrixT = basisMatrix.Transpose();
            this.basisScalarProducts = this.basisMatrixT * basisMatrix;
        }

        /// <summary>
        /// Returns the standard basis of the specified dimension.
        /// </summary>
        /// <param name="dimension">
        /// The dimension of the <see cref="Basis"/>.</param>
        /// <remarks>
        /// A basis is standard for a space having a given dimension if it can be
        /// represented by the <see cref="DoubleMatrix.Identity"/> 
        /// matrix having the same dimension.
        /// </remarks>
        /// <returns>
        /// The standard basis of the given dimension.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="dimension"/> is not positive.
        /// </exception>
        public static Basis Standard(int dimension)
        {
            if (dimension < 1) {
                throw new ArgumentOutOfRangeException(
                    nameof(dimension),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            var standard = new Basis
            {
                basisMatrixT = DoubleMatrix.Identity(dimension),
                basisScalarProducts = DoubleMatrix.Identity(dimension)
            };
            return standard;
        }

        #endregion

        #region Scalar products, norms, distances

        /// <summary>
        /// Return the scalar product of the vectors having the specified 
        /// coordinates.
        /// </summary>
        /// <param name="left">The coordinates of the first vector.</param>
        /// <param name="right">The coordinates of the second vector.</param>
        /// <returns>The scalar product of the vectors whose coordinates are 
        /// represented by <paramref name="left"/> and <paramref name="right"/>, 
        /// respectively.</returns>
        /// <remarks>
        /// <para>
        /// Coordinates must be passed as row vectors. 
        /// Let <latex>A</latex> be the 
        /// <see cref="GetBasisMatrix">matrix representation</see>
        /// of this instance, and let <latex>\bc{A}{l}</latex> and
        /// <latex>\bc{A}{r}</latex> be 
        /// <paramref name="left"/> and <paramref name="right"/>,
        /// respectively. Then 
        /// method <see cref="ScalarProduct(DoubleMatrix, DoubleMatrix)"/>
        /// returns
        /// <latex mode="display">
        /// \bsprod{A}{l|r} = \T{\bc{A}{l}}\,\Q{A}\,\bc{A}{r},
        /// </latex>
        /// where <latex>\bc{A}{l},\bc{A}{r} \in \R^K</latex> are 
        /// coordinates w.r.t. basis
        /// <latex>\A</latex>, and 
        /// <latex mode="display">
        /// \Q{A} = \T{A}\,A.
        /// </latex>
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="left"/> is not a row vector.<br/>
        /// -or-<br/>
        /// <paramref name="left"/> has <see cref="DoubleMatrix.Count"/>
        /// not equal to the <see cref="Dimension"/> of 
        /// the <see cref="Basis"/>.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> is not a row vector.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> has <see cref="DoubleMatrix.Count"/>
        /// not equal to the <see cref="Dimension"/> of 
        /// the <see cref="Basis"/>.
        /// </exception>
        public double ScalarProduct(DoubleMatrix left, DoubleMatrix right)
        {
            #region Input validation

            if (left is null) {
                throw new ArgumentNullException(nameof(left));
            }

            if (!left.IsRowVector) {
                throw new ArgumentOutOfRangeException(
                    nameof(left),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR"));
            }

            int k = this.Dimension;

            if (left.Count != k) {
                throw new ArgumentOutOfRangeException(
                    nameof(left),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_BASIS_COMPLIANT_VECTOR"));
            }

            if (right is null) {
                throw new ArgumentNullException(nameof(right));
            }

            if (!right.IsRowVector) {
                throw new ArgumentOutOfRangeException(
                    nameof(right),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR"));
            }

            if (right.Count != k) {
                throw new ArgumentOutOfRangeException(
                    nameof(right),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_BASIS_COMPLIANT_VECTOR"));
            }

            #endregion

            var q = this.basisScalarProducts;

            return (double)(left * q * right.Transpose());
        }

        /// <summary>
        /// Returns the norm of the vector having the specified coordinates.
        /// </summary>
        /// <param name="coordinates">The vector coordinates.</param>
        /// <returns>
        /// The norm of the vector having the specified coordinates.</returns>
        /// <remarks>
        /// <para>
        /// Parameter <paramref name="coordinates"/> must be passed as 
        /// a row vector.
        /// Let <latex>A</latex> be the 
        /// <see cref="GetBasisMatrix">matrix representation</see>
        /// of this instance, and let <latex>\bc{A}{x}</latex> be 
        /// equal to <paramref name="coordinates"/>. Then 
        /// method <see cref="Norm(DoubleMatrix)"/>
        /// returns
        /// <latex mode="display">
        /// \bnorm{A}{x} = \bsprod{A}{x|x}^{1/2},
        /// </latex>
        /// where <latex>\bsprod{A}{\cdot|\cdot}</latex> is the 
        /// <see cref="ScalarProduct(DoubleMatrix, DoubleMatrix)">
        /// scalar product</see> induced by
        /// basis <latex>\A</latex>.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="coordinates"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="coordinates"/> is not a row vector.<br/>
        /// -or-<br/>
        /// <paramref name="coordinates"/> has <see cref="DoubleMatrix.Count"/>
        /// not equal to the <see cref="Dimension"/> of 
        /// the <see cref="Basis"/>.
        /// </exception>
        public double Norm(DoubleMatrix coordinates)
        {
            #region Input validation

            if (coordinates is null) {
                throw new ArgumentNullException(nameof(coordinates));
            }

            if (!coordinates.IsRowVector) {
                throw new ArgumentOutOfRangeException(
                    nameof(coordinates),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR"));
            }

            int k = this.Dimension;

            if (coordinates.Count != k) {
                throw new ArgumentOutOfRangeException(
                    nameof(coordinates),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_BASIS_COMPLIANT_VECTOR"));
            }

            #endregion

            var q = this.basisScalarProducts;

            return (double)(coordinates * q * coordinates.Transpose());
        }

        /// <summary>
        /// Computes the distance between vectors having the specified coordinates.
        /// </summary>
        /// <param name="left">The coordinates of the first vector.</param>
        /// <param name="right">The coordinates of the second vector.</param>
        /// <remarks>
        /// <para>
        /// Coordinates must be passed as row vectors.
        /// Let <latex>A</latex> be the 
        /// <see cref="GetBasisMatrix">matrix representation</see>
        /// of this instance, and let <latex>\bc{A}{l}</latex> and
        /// <latex>\bc{A}{r}</latex> be 
        /// <paramref name="left"/> and <paramref name="right"/>,
        /// respectively. Then 
        /// method <see cref="Distance(DoubleMatrix, DoubleMatrix)"/>
        /// returns
        /// <latex mode="display">
        /// \bdist{A}{l,r} = \bnorm{A}{l-r},
        /// </latex>
        /// where <latex>\bnorm{A}{\cdot}</latex> is the 
        /// <see cref="Norm(DoubleMatrix)">
        /// norm</see> defined by
        /// basis <latex>\A</latex>.
        /// </para>
        /// </remarks>
        /// <returns>
        /// The distance between the vectors having as coordinates
        /// <paramref name="left"/> and <paramref name="right"/>, 
        /// respectively.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="left"/> is not a row 
        /// vector.<br/>
        /// -or-<br/>
        /// <paramref name="left"/> has <see cref="DoubleMatrix.Count"/>
        /// not equal to the <see cref="Dimension"/> of 
        /// the <see cref="Basis"/>.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> is not a row vector.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> has <see cref="DoubleMatrix.Count"/>
        /// not equal to the <see cref="Dimension"/> of 
        /// the <see cref="Basis"/>.
        /// </exception>
        public double Distance(DoubleMatrix left, DoubleMatrix right)
        {
            #region Input validation

            if (left is null) {
                throw new ArgumentNullException(nameof(left));
            }

            if (!left.IsRowVector) {
                throw new ArgumentOutOfRangeException(
                    nameof(left),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR"));
            }

            int k = this.Dimension;

            if (left.Count != k) {
                throw new ArgumentOutOfRangeException(
                    nameof(left),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_BASIS_COMPLIANT_VECTOR"));
            }

            if (right is null) {
                throw new ArgumentNullException(nameof(right));
            }

            if (!right.IsRowVector) {
                throw new ArgumentOutOfRangeException(
                    nameof(right),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR"));
            }

            if (right.Count != k) {
                throw new ArgumentOutOfRangeException(
                    nameof(right),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_BASIS_COMPLIANT_VECTOR"));
            }

            #endregion

            var s = left - right;
            var q = this.basisScalarProducts;

            return Math.Sqrt((double)(s * q * s.Transpose()));
        }

        #endregion

        #region Coordinates

        /// <summary>
        /// Gets coordinates of vectors with respect to a new basis 
        /// given the coordinates
        /// with respect to another basis.
        /// </summary>
        /// <param name="newBasis">
        /// The basis which the new coordinates must be 
        /// referred to.
        /// </param>
        /// <param name="currentCoordinates">
        /// The current coordinates.
        /// </param>
        /// <param name="currentBasis">
        /// The current basis.
        /// </param>
        /// <remarks>
        /// <para>
        /// Each row in <paramref name="currentCoordinates"/> is interpreted as 
        /// the coordinates of a point.
        /// Hence, a matrix is returned having the same dimensions of 
        /// <paramref name="currentCoordinates"/>, in which the <i>i</i>-th row  
        /// represents the
        /// coordinates of the <i>i</i>-th point with respect to the new basis.
        /// </para>
        /// <para>
        /// Let <latex>C</latex> and <latex>N</latex> be the 
        /// <see cref="GetBasisMatrix">matrix representations</see>
        /// of <paramref name="currentBasis"/>, and <paramref name="newBasis"/>,
        /// respectively, and let <latex>\bc{C}{X}</latex> be 
        /// <paramref name="currentCoordinates"/>, i.e. the 
        /// coordinates matrix w.r.t. basis <latex>\basis{C}</latex> of the points
        /// <latex>x_1,\dots,x_n</latex> under study:
        /// <latex mode="display">
        /// \bc{C}{X}=\mx{
        ///    \T{\bc{C}{x_1}} \\
        ///    \vdots  \\
        ///    \T{\bc{C}{x_n}} }.
        /// </latex>
        /// Then 
        /// method <see cref="ChangeCoordinates(Basis, DoubleMatrix, Basis)"/>
        /// returns the matrix
        /// <latex mode="display">
        /// \bc{N}{X} = \bc{C}{X}\, \T{C}\, \InvT{N}.
        /// </latex>
        /// </para>
        /// </remarks>
        /// <returns>
        /// A matrix of coordinates in the new basis.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="currentCoordinates"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="newBasis"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="currentBasis"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The <see cref="Dimension"/> of
        /// <paramref name="currentBasis"/> is
        /// not equal to the dimension of <paramref name="newBasis"/>.<br/>
        /// -or-<br/>
        /// The number of columns of <paramref name="currentCoordinates"/> is
        /// not equal to the <see cref="Dimension"/> 
        /// of <paramref name="newBasis"/>.
        /// </exception>
        public static DoubleMatrix ChangeCoordinates(Basis newBasis,
            DoubleMatrix currentCoordinates, Basis currentBasis)
        {
            #region Input validation

            if (newBasis is null) {
                throw new ArgumentNullException(nameof(newBasis));
            }

            if (currentCoordinates is null) {
                throw new ArgumentNullException(nameof(currentCoordinates));
            }

            if (currentBasis is null) {
                throw new ArgumentNullException(nameof(currentBasis));
            }

            int k = newBasis.Dimension;

            if (currentBasis.Dimension != k) {
                throw new ArgumentOutOfRangeException(
                    nameof(newBasis),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_BASES_MUST_SHARE_DIMENSION"));
            }

            if (currentCoordinates.NumberOfColumns != k) {
                throw new ArgumentOutOfRangeException(
                    nameof(currentCoordinates),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_BASIS_COMPLIANT_MATRIX"));
            }

            #endregion

            return currentCoordinates * 
                currentBasis.basisMatrixT / newBasis.basisMatrixT;
        }

        /// <summary>
        /// Gets the vectors represented by the specified coordinates 
        /// with respect to the <see cref="Basis"/>.
        /// </summary>
        /// <param name="coordinates">
        /// A matrix whose rows are the coordinates of 
        /// the vectors.
        /// </param>
        /// <remarks>
        /// <para>
        /// Each row in <paramref name="coordinates"/> is interpreted as 
        /// the coordinates of a point.
        /// Hence, a matrix is returned having the same dimensions of 
        /// <paramref name="coordinates"/>, in which the <i>i</i>-th row 
        /// represents
        /// the vector corresponding to the <i>i</i>-th row of
        /// <paramref name="coordinates"/>.
        /// </para>
        /// <para>
        /// Let <latex>A</latex> be the 
        /// <see cref="GetBasisMatrix">matrix representation</see>
        /// of this instance, 
        /// and let <latex>\bc{A}{X}</latex> be 
        /// <paramref name="coordinates"/>, the 
        /// coordinates matrix w.r.t. basis <latex>\A</latex> of the points
        /// <latex>x_1,\dots,x_n</latex> under study:
        /// <latex mode="display">
        /// X_{\A}=\mx{
        ///    \T{\bc{A}{x_1}} \\
        ///    \vdots  \\
        ///    \T{\bc{A}{x_n}} }.
        /// </latex>
        /// Then 
        /// method <see cref="GetVectors(DoubleMatrix)"/>
        /// returns the matrix
        /// <latex mode="display">
        /// \bc{I}{X} = \bc{A}{X}\,\T{A}.
        /// </latex>
        /// </para>
        /// </remarks>
        /// <returns>
        /// A matrix whose rows are the vectors corresponding to the 
        /// specified coordinates.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="coordinates"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="coordinates"/> has a number of columns 
        /// not equal to the <see cref="Dimension"/> of 
        /// the <see cref="Basis"/>.
        /// </exception>
        public DoubleMatrix GetVectors(DoubleMatrix coordinates)
        {
            #region Input validation

            if (coordinates is null) {
                throw new ArgumentNullException(nameof(coordinates));
            }

            int k = this.Dimension;

            if (coordinates.NumberOfColumns != k) {
                throw new ArgumentOutOfRangeException(
                    nameof(coordinates),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_BASIS_COMPLIANT_MATRIX"));
            }

            #endregion

            return coordinates * this.basisMatrixT;
        }

        /// <summary>
        /// Gets the coordinates of the given vectors with respect to
        /// the <see cref="Basis"/>.
        /// </summary>
        /// <param name="vectors">
        /// A matrix whose rows are the vectors whose coordinates
        /// are to be computed.
        /// </param>
        /// <returns>
        /// A matrix whose rows are the corresponding coordinates 
        /// with respect to
        /// the <see cref="Basis"/>.
        /// </returns>
        /// <remarks>
        /// <para>
        /// Each row in <paramref name="vectors"/> is interpreted as 
        /// a vector.
        /// Hence, a matrix is returned having the same dimensions of 
        /// <paramref name="vectors"/>, in which the <i>i</i>-th row  
        /// represents the coordinates of the <i>i</i>-th row of
        /// <paramref name="vectors"/>.
        /// </para>
        /// <para>
        /// Let <latex>A</latex> be the 
        /// <see cref="GetBasisMatrix">matrix representation</see>
        /// of this instance, 
        /// and let <latex>\bc{I}{X}</latex> be 
        /// <paramref name="vectors"/>, a matrix whose rows 
        /// are transposed versions of the points
        /// <latex>x_1,\dots,x_n</latex> under study:
        /// <latex mode="display">
        /// X_{\basis{I}}=\mx{
        ///    \T{x_1} \\
        ///    \vdots  \\
        ///    \T{x_n} }.
        /// </latex>
        /// Then 
        /// method <see cref="GetCoordinates(DoubleMatrix)"/>
        /// returns the coordinates matrix w.r.t. basis <latex>\A</latex>
        /// of such points:
        /// <latex mode="display">
        /// \bc{A}{X} = \bc{I}{X}\,\InvT{A}.
        /// </latex>
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="vectors"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="vectors"/> has a number of columns 
        /// not equal to the <see cref="Dimension"/> of 
        /// the <see cref="Basis"/>.
        /// </exception>
        public DoubleMatrix GetCoordinates(DoubleMatrix vectors)
        {
            #region Input validation

            if (vectors is null) {
                throw new ArgumentNullException(nameof(vectors));
            }

            int k = this.Dimension;

            if (vectors.NumberOfColumns != k) {
                throw new ArgumentOutOfRangeException(
                    nameof(vectors),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_BASIS_COMPLIANT_MATRIX"));
            }

            #endregion

            return vectors / this.basisMatrixT;
        }

        #endregion
    }
}