// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;

using Novacta.Analytics.Infrastructure;

namespace Novacta.Analytics.Advanced
{
    /// <summary>
    /// Represents a collection of weighted multi-dimensional 
    /// points, whose coordinates are
    /// taken with respect to a given basis.
    /// </summary>
    /// <remarks>
    /// <para><b>Points and variables</b></para>
    /// <para>
    /// Let us consider <latex>n</latex> points <latex>x_1,\dots,x_n</latex> in
    /// <latex>\R^K</latex>, and assume that a <i>weighting scheme</i> has 
    /// been imposed to them to control their contributions to the 
    /// statistical characteristics of their ensemble: that is, a
    /// relative weight <latex>w_i>0</latex> is assigned to 
    /// point <latex>x_i</latex>, with <latex>\sum_{i=1}^{n}w_i=1</latex>.
    /// Such set of weighted points can thus 
    /// be represented
    /// by means of the pair <latex>S\equiv\round{X_S,w_S}</latex>, 
    /// referred to as a
    /// <i>weighted multidimensional structure</i>
    /// where
    /// <latex mode="display">
    /// X_S =\mx{
    ///     \T{x_1} \\ \vdots \\ \T{x_n}
    ///     }
    /// </latex>
    /// is the <latex>n \times K</latex> matrix whose <latex>i</latex>-th 
    /// row is <latex>\T{x_i}</latex>, and <latex>w_S</latex> is 
    /// the weighting scheme expressed as a sequence:
    /// <latex mode="display">
    /// w_S=\T{\round{w_1,\dots, w_n}}. 
    /// </latex>
    /// </para>
    /// <para>
    /// Given a basis <latex>\A</latex> of <latex>\R^K</latex>, a 
    /// structure 
    /// <latex>S</latex> can be represented by a 
    /// <i>cloud</i>, say <latex>\C__{S,\A}</latex>, which 
    /// can be formally defined as the
    /// triplet <latex>(\A,X_{S,\A},w_S)</latex>, where
    /// <latex mode="display">
    /// X_{S,\A} =\mx{
    ///     \T{x_{1,\A}} \\ \vdots \\ \T{x_{n,\A}}
    ///     }
    /// </latex>
    /// is the coordinates matrix w.r.t. <latex>\A</latex> of the points
    /// in <latex>S</latex>, i.e., its <latex>i</latex>-th row, 
    /// <latex>\T{x_{i,\A}}</latex>, represents the coordinates 
    /// of point <latex>x_i</latex>.
    /// </para>
    /// <para>
    /// Given a <see cref="Cloud"/> instance representing 
    /// <latex>{\C_{S,\A}=(\A,{X_{S,\A},w_S)</latex>, you can inspect 
    /// its <see cref="Basis"/>, <latex>\A</latex>, 
    /// the <see cref="Coordinates"/>
    /// of the points <latex>X_{S,\A}</latex>, and 
    /// their <see cref="Weights"/>, <latex>w_S</latex>.
    /// </para>
    /// <para>
    /// The <i>mean point</i> of <latex>S</latex> 
    /// is the vector <latex>m_S</latex> 
    /// whose <latex>\A</latex> coordinates are given by:
    /// <latex mode="display">
    /// m_{S,\A} = \T{X_{S,\A}}\,w_S.
    /// </latex>
    /// Such coordinates are returned by method <see cref="Mean"/>.
    /// The <i>variance</i> of <latex>S</latex>, 
    /// say <latex>\Var_S</latex>, is returned by 
    /// method <see cref="Variance()"/> 
    /// and defined as follows:
    /// <latex mode="display">\label{eq:Var_S}
    ///     \Var_S = \sum_{i=1}^{n} w_i \round{\bdist{A}{x_i,m_S}}^2,    
    /// </latex>
    /// where
    /// <latex>
    /// \bdist{A}{\cdot,\cdot} 
    /// </latex>
    /// is 
    /// the <see cref="Basis.Distance(DoubleMatrix, DoubleMatrix)">distance</see>
    /// induced by basis <latex>\A</latex>.
    /// </para>
    /// <para>
    /// The columns of <latex>X_{S,\A}</latex> represent 
    /// the <i>active</i> variables 
    /// observed at the <latex>S</latex> points w.r.t. basis 
    /// <latex>\A</latex>. The covariance matrix of such variables 
    /// can be defined 
    /// as follows
    /// <latex mode="display">
    /// \Cov_{S,\A} = \T{X_{S,\A}}\,W_S\,{X_{S,\A},
    /// </latex>
    /// where 
    /// <latex mode="display">
    ///     W_S = 
    ///      \mx{
    ///         w_{S,1}  &amp; 0              &amp; \cdots &amp; 0\\ 
    ///         0               &amp; w_{S,2} &amp; \cdots &amp; 0\\
    ///         \vdots          &amp; \vdots         &amp; \ddots &amp; \vdots \\
    ///         0               &amp; 0              &amp; \cdots &amp; w_{S,n}
    ///     } - w_S\,\T{w_S}.
    /// </latex>
    /// Such matrix can be inspected by calling method 
    /// <see cref="Covariance"/>.
    /// </para>
    /// <para>
    /// A variable which is not active is said <i>supplementary</i>.
    /// Supplementary variables have as many rows as the number of points 
    /// in the cloud, while
    /// the number of columns is the number of supplementary variables.
    /// Their <see cref="GetVariances(DoubleMatrix)">variances</see>
    /// can be computed, which means that the weighting scheme of the cloud
    /// will be applied.
    /// </para>
    /// <para><b>Cloud modifications</b></para>
    /// <para>
    /// A cloud can be <see cref="Rebase">re-based</see>, so that
    /// the cloud coordinates are updated to be referable
    /// to a new basis. 
    /// Clouds can also be <see cref="Center">centered</see> 
    /// so as to have zero mean coordinates, or
    /// <see cref="Standardize">standardized</see>
    /// by subtracting to each variable its mean
    /// and then dividing the difference by its standard deviation.
    /// </para>
    /// </remarks>
    public class Cloud
    {
        #region State

        /// <summary>
        /// Coordinates of the cloud points.
        /// </summary>
        /// <remarks>
        /// <para>
        /// A matrix having as many rows as the number of points in the cloud.
        /// The number of columns is the dimension of the space on which the
        /// points lie.
        /// </para>
        /// <para>
        /// Each row represents the coordinates of a given point in the cloud. 
        /// Points are thus well ordered, and hence thoroughly identified,
        /// by the index of the row
        /// in which the corresponding coordinates are stored.
        /// </para>
        /// </remarks>
        private readonly DoubleMatrix coordinates;

        /// <summary>
        /// Weights assigned to the cloud points.
        /// </summary>
        /// <remarks>
        /// Points are well ordered, and hence thoroughly identified, 
        /// by the index of the row
        /// in which their coordinates are stored. As a consequence, 
        /// the same order must be followed
        /// when inserting weights in this column vector.
        /// </remarks>
        private readonly DoubleMatrix weights;

        /// <summary>
        /// Gets the coordinates of the cloud points.
        /// </summary>
        /// <value>
        /// A matrix whose rows represent the coordinates of 
        /// the cloud points.
        /// </value>
        public ReadOnlyDoubleMatrix Coordinates
        {
            get { return this.coordinates.AsReadOnly(); }
        }

        /// <summary>
        /// Gets the basis of the cloud.
        /// </summary>
        /// <value>
        /// The basis which the cloud coordinates are referred to.
        /// </value>
        public Basis Basis { get; }

        /// <summary>
        /// Gets the weights of the cloud points.
        /// </summary>
        /// <value>
        /// A column vector whose entries are the weights of 
        /// the corresponding cloud points.
        /// </value>
        public ReadOnlyDoubleMatrix Weights
        {
            get { return this.weights.AsReadOnly(); }
        }

        #endregion

        #region Constructors and factory methods

        /// <summary>
        /// Initializes a new instance of the <see cref="Cloud"/> class
        /// that contains points having the specified weights and whose 
        /// coordinates are taken with respect to the given basis, with
        /// coordinates and weights eventually copied before instantiation.
        /// </summary>
        /// <param name="coordinates">
        /// The coordinates of the cloud points.</param>
        /// <param name="weights">
        /// The weights of the cloud points.</param>
        /// <param name="basis">
        /// The basis which the point coordinates are 
        /// referred to.</param>
        /// <param name="copyData">
        /// <c>true</c> if <paramref name="coordinates"/> and <paramref name="weights"/>
        /// must be copied before instantiation; otherwise <c>false</c>.
        /// </param>
        /// <remarks>
        /// <para>
        /// Matrix <paramref name="coordinates"/> has as many rows as the 
        /// number of points in the cloud.
        /// The number of columns is the dimension of the space in which 
        /// the points lie.
        /// </para>
        /// <para>
        /// Each row represents the coordinates of a given point in the 
        /// cloud. Points are thus well ordered, and hence thoroughly 
        /// identified, by the index of the row in which its coordinates 
        /// are stored. 
        /// As a consequence, the same order must be followed
        /// when inserting entries in the vector of weights.
        /// </para>
        /// <para>
        /// The <see cref="Cloud(DoubleMatrix, DoubleMatrix, Basis, bool)"/> 
        /// constructor prevents the copy
        /// of the elements in <paramref name="coordinates"/> and
        /// <paramref name="weights"/> before instantiation 
        /// if <paramref name="copyData"/> evaluates to <c>false</c>: the 
        /// returned <see cref="Cloud"/> instance will instead use a direct 
        /// reference to <paramref name="coordinates"/> and <paramref name="weights"/>.
        /// </para>
        /// <para>
        /// <note type="caution">
        /// This constructor is intended for advanced users and must always be used 
        /// carefully. 
        /// Do not use this constructor if you do not have complete control of 
        /// the <paramref name="coordinates"/> and <paramref name="weights"/> instances.
        /// Once such instances are passed to the constructor as arguments, they must be 
        /// treated as read-only objects outside the returned <see cref="Cloud"/> instance: 
        /// you shouldn't manipulate entries via 
        /// a direct reference to <paramref name="coordinates"/> or <paramref name="weights"/>; 
        /// otherwise, the 
        /// behavior of the returned <see cref="Cloud"/> instance 
        /// must be considered as undefined and  
        /// almost surely prone to errors.
        /// </note>
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="coordinates"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="weights"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="basis"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="weights"/> is not a column vector.<br/>
        /// -or-<br/> 
        /// <paramref name="coordinates"/>
        /// and <paramref name="weights"/> have unequal numbers of rows.<br/>
        /// -or-<br/> 
        /// The <see cref="Basis.Dimension"/> of <paramref name="basis"/> 
        /// is not equal to the number of columns 
        /// of <paramref name="coordinates"/>.
        /// </exception>
        public Cloud(
            DoubleMatrix coordinates,
            DoubleMatrix weights,
            Basis basis,
            bool copyData)
        {
            #region Input validation

            ArgumentNullException.ThrowIfNull(coordinates);

            ArgumentNullException.ThrowIfNull(weights);

            ArgumentNullException.ThrowIfNull(basis);

            if (!weights.IsColumnVector)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(weights),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_COLUMN_VECTOR"));
            }

            int n = coordinates.NumberOfRows;

            if (weights.NumberOfRows != n)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(weights),
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS"),
                        nameof(coordinates)));
            }

            double sum = 0.0, weight;
            for (int i = 0; i < weights.Count; i++)
            {
                weight = weights[i];
                if (weight < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(weights),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_ENTRIES_MUST_BE_NON_NEGATIVE"));
                }
                sum += weight;
            }

            if (Math.Abs(sum - 1.0) > 1.0e-3)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(weights),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_1"));
            }

            int k = coordinates.NumberOfColumns;

            if (basis.Dimension != k)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(basis),
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_COLUMNS"),
                        nameof(coordinates)));
            }

            #endregion

            if (copyData)
            {
                this.coordinates = coordinates.Clone();
                this.weights = weights.Clone();
            }
            else
            {
                this.coordinates = coordinates;
                this.weights = weights;
            }
            this.Basis = basis;

            #region Statistics

            // Mean
            var w_s = this.weights;
            var x_sa = this.coordinates;
            var m_sa_t = w_s.Transpose() * x_sa;
            this.Mean = m_sa_t.AsReadOnly();

            // Variance
            var m_sa = m_sa_t.Transpose();
            var q_a = this.Basis.basisScalarProducts;
            var variance = 0.0;
            DoubleMatrix coords;
            for (int i = 0; i < w_s.Count; i++)
            {
                coords = x_sa[i, ":"];
                variance += w_s[i] * (coords * q_a * coords.Transpose())[0];
            }
            variance -= (m_sa_t * q_a * m_sa)[0];
            this.Variance = variance;

            // Covariance
            var diag_w_s = DoubleMatrix.Diagonal(w_s);
            var x_sa_t = x_sa.Transpose();
            this.Covariance = ((x_sa_t * diag_w_s * x_sa)
                - (m_sa * m_sa_t)).AsReadOnly();

            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cloud"/> class
        /// that contains points having the specified weights and whose 
        /// coordinates are taken with respect to the given basis.
        /// </summary>
        /// <param name="coordinates">
        /// The coordinates of the cloud points.</param>
        /// <param name="weights">
        /// The weights of the cloud points.</param>
        /// <param name="basis">
        /// The basis which the point coordinates are 
        /// referred to.</param>
        /// <remarks>
        /// <para>
        /// Matrix <paramref name="coordinates"/> has as many rows as the 
        /// number of points in the cloud.
        /// The number of columns is the dimension of the space in which 
        /// the points lie.
        /// </para>
        /// <para>
        /// Each row represents the coordinates of a given point in the 
        /// cloud. Points are thus well ordered, and hence thoroughly 
        /// identified, by the index of the row in which its coordinates 
        /// are stored. 
        /// As a consequence, the same order must be followed
        /// when inserting entries in the vector of weights.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="coordinates"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="weights"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="basis"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="weights"/> is not a column vector.<br/>
        /// -or-<br/> 
        /// <paramref name="coordinates"/>
        /// and <paramref name="weights"/> have unequal numbers of rows.<br/>
        /// -or-<br/> 
        /// The <see cref="Basis.Dimension"/> of <paramref name="basis"/> 
        /// is not equal to the number of columns 
        /// of <paramref name="coordinates"/>.
        /// </exception>
        public Cloud(
            DoubleMatrix coordinates,
            DoubleMatrix weights,
            Basis basis)
            : this(coordinates, weights, basis, true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cloud"/> class that
        /// contains points having the specified weights and whose coordinates 
        /// are taken with respect to
        /// the standard basis.
        /// </summary>
        /// <param name="coordinates">The coordinates of the cloud points.</param>
        /// <param name="weights">The weights of the cloud points.</param>
        /// <remarks>
        /// <para>Matrix <paramref name="coordinates"/> has as many rows as the 
        /// number of points in the cloud.
        /// The number of columns is the dimension of the space in which the
        /// points lie.</para>
        /// <para>Each row represents the coordinates of a given point in the cloud. 
        /// Points are thus well ordered, and hence thoroughly identified, by the 
        /// index of the row
        /// in which its coordinates are stored. As a consequence, the same order 
        /// must be followed
        /// when inserting entries in the vector of weights. The coordinates are 
        /// automatically referred to 
        /// the standard basis having dimension equal to the number of columns of 
        /// <paramref name="coordinates"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="coordinates"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="weights"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="weights"/> is not a column vector.<br/>
        /// -or-<br/>
        /// <paramref name="coordinates"/>
        /// and <paramref name="weights"/> have not the same numbers of rows.
        /// </exception>        
        public Cloud(DoubleMatrix coordinates, DoubleMatrix weights)
            : this(
                  coordinates,
                  weights,
                  coordinates is null ? null : Basis.Standard(
                      coordinates.NumberOfColumns))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cloud"/> class that
        /// contains points whose coordinates 
        /// are taken with respect to 
        /// the standard basis. To each point is assigned a weight equal to 
        /// the reciprocal of the
        /// number of points.
        /// </summary>
        /// <param name="coordinates">The coordinates of the cloud points.</param>
        /// <remarks>
        /// <para>Matrix <paramref name="coordinates"/> has as many rows as the 
        /// number of points in the cloud.
        /// The number of columns is the dimension of the space in which the
        /// points lie.</para>
        /// <para>Each row represents the coordinates of a given point in the cloud. 
        /// Points are thus well ordered, and hence thoroughly identified, by the 
        /// index of the row
        /// in which its coordinates are stored. Cloud points are automatically 
        /// weighted using 
        /// as elementary weight the reciprocal of the number of points. The 
        /// coordinates are automatically 
        /// referred to the standard basis having dimension equal to the number 
        /// of columns of 
        /// <paramref name="coordinates"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="coordinates"/> is <b>null</b>.
        /// </exception>
        public Cloud(DoubleMatrix coordinates)
             : this(
                   coordinates,
                   coordinates is null ? null : DoubleMatrix.Dense(
                       coordinates.NumberOfRows,
                       1,
                       1.0 / coordinates.NumberOfRows),
                   coordinates is null ? null : Basis.Standard(
                       coordinates.NumberOfColumns))
        {
        }

        #endregion

        #region Basic statistics

        /// <summary>
        /// Gets the mean coordinates of the <see cref="Cloud"/>
        /// with respect to its <see cref="Basis"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The coordinates of the mean point are returned, with respect
        /// the cloud <see cref="Basis"/>.
        /// To obtain the corresponding mean vector, 
        /// execute method 
        /// <see cref="Basis.GetVectors(DoubleMatrix)">
        /// GetVectors</see>
        /// on <see cref="Basis"/>.
        /// </para>
        /// <para>
        /// Let this <see cref="Cloud"/> instance represent the cloud 
        /// <latex>(\A,X_{S,\A},w_S)</latex>, where
        /// <latex>\A</latex> is its <see cref="Basis"/>, 
        /// the <see cref="Coordinates"/>
        /// of the points are given by <latex>X_{S,\A}</latex>, and 
        /// <latex>w_S</latex> contains the <see cref="Weights"/>.
        /// </para>
        /// <para>
        /// Then property <see cref="Mean"/> returns the coordinates 
        /// w.r.t. basis <latex>\A</latex> of 
        /// the <i>mean point</i>:
        /// <latex mode="display">
        /// m_{S,\A} = \T{X_{S,\A}}\,w_S.
        /// </latex>
        /// </para>
        /// </remarks>
        /// <value>
        /// The cloud mean coordinates as a row vector.
        /// </value>
        public ReadOnlyDoubleMatrix Mean { get; }

        /// <summary>
        /// Gets the variance of the points 
        /// represented in the <see cref="Cloud"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Let this <see cref="Cloud"/> instance represent the cloud 
        /// <latex>(\A,X_{S,\A},w_S)</latex>, where
        /// <latex>\A</latex> is its <see cref="Basis"/>, 
        /// the <see cref="Coordinates"/>
        /// of the points are given by <latex>X_{S,\A}</latex>, and 
        /// <latex>w_S</latex> contains the <see cref="Weights"/>.
        /// </para>
        /// <para>
        /// Then property <see cref="Variance()"/> 
        /// returns the variance of the cloud:
        /// <latex mode="display">\label{eq:Var_S}
        ///     \Var_S = \sum_{i=1}^{n} w_i \round{\bdist{A}{x_i,m_S}}^2,    
        /// </latex>
        /// where
        /// <latex>
        /// \bdist{A}{\cdot,\cdot} 
        /// </latex>
        /// is 
        /// the <see cref="Basis.Distance(DoubleMatrix, DoubleMatrix)">distance</see>
        /// induced by basis <latex>\A</latex>.
        /// </para>
        /// </remarks>
        /// <value>The cloud variance.</value>
        public double Variance { get; }

        /// <summary>
        /// Gets the covariance matrix of the 
        /// active variables in the <see cref="Cloud"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Let this <see cref="Cloud"/> instance represent the cloud 
        /// <latex>(\A,X_{S,\A},w_S)</latex>, where
        /// <latex>\A</latex> is its <see cref="Basis"/>, 
        /// the <see cref="Coordinates"/>
        /// of the points are given by <latex>X_{S,\A}</latex>, and 
        /// <latex>w_S</latex> contains the <see cref="Weights"/>.
        /// </para>
        /// <para>
        /// The columns of <latex>X_{S,\A}</latex> represent 
        /// the <i>active</i> variables 
        /// observed at the <latex>S</latex> points w.r.t. basis 
        /// <latex>\A</latex>. The covariance matrix of such variables 
        /// is returned by method <see cref="Covariance"/> 
        /// as follows
        /// <latex mode="display">
        /// \Cov_{S,\A} = \T{X_{S,\A}}\,W_S\,X_{S,\A},
        /// </latex>
        /// where 
        /// <latex mode="display">
        ///     W_S = 
        ///      \mx{
        ///         w_{S,1}  &amp; 0              &amp; \cdots &amp; 0\\ 
        ///         0               &amp; w_{S,2} &amp; \cdots &amp; 0\\
        ///         \vdots          &amp; \vdots         &amp; \ddots &amp; \vdots \\
        ///         0               &amp; 0              &amp; \cdots &amp; w_{S,n}
        ///     } - w_S\,\T{w_S}.
        /// </latex>
        /// </para>
        /// </remarks>
        /// <value>The covariance matrix of the cloud.</value>
        public ReadOnlyDoubleMatrix Covariance { get; }

        /// <summary>
        /// Computes the variances of the specified supplementary variables.
        /// </summary>
        /// <param name="supplementaryVariables">
        /// The supplementary variables.
        /// </param>
        /// <remarks>
        /// <para><b>Points and variables</b></para>
        /// <para>
        /// The coordinates of a given point 
        /// have length equal to the dimension of the cloud basis.
        /// The collection of the coordinates of all points on a given axis 
        /// is referred to as an <i>active variable</i>, as opposed to 
        /// <i>supplementary</i> ones. 
        /// The matrix passed as <paramref name="supplementaryVariables"/>
        /// must have as many rows as the number of points in the cloud, while
        /// the number of columns is the number of supplementary variables.
        /// </para>        
        /// </remarks>
        /// <returns>
        /// The supplementary variances as a row vector.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="supplementaryVariables"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The number of rows in <paramref name="supplementaryVariables"/> is 
        /// not equal to the number of points in the cloud.
        /// </exception>
        public DoubleMatrix GetVariances(DoubleMatrix supplementaryVariables)
        {
            ArgumentNullException.ThrowIfNull(supplementaryVariables);

            int numberOfVariables = supplementaryVariables.NumberOfRows;

            if (this.coordinates.NumberOfRows != numberOfVariables)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(supplementaryVariables),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_ROW_DIM_MUST_MATCH_INDIVIDUALS_COUNT"));
            }

            var w_s_t = this.weights.Transpose();

            var squaredCoordinates = DoubleMatrix.ElementWiseMultiply(
                supplementaryVariables,
                supplementaryVariables);
            var meanCoordinates = w_s_t * supplementaryVariables;

            var squaredMeanCoordinates = DoubleMatrix.ElementWiseMultiply(
                meanCoordinates,
                meanCoordinates);

            return w_s_t * squaredCoordinates - squaredMeanCoordinates;
        }

        /// <inheritdoc cref="GetVariances(DoubleMatrix)"/>
        public DoubleMatrix GetVariances(ReadOnlyDoubleMatrix supplementaryVariables)
        {
            ArgumentNullException.ThrowIfNull(supplementaryVariables);

            return this.GetVariances(supplementaryVariables.matrix);
        }

        /// <summary>
        /// Gets the principal projections of this instance.
        /// </summary>
        /// <returns>The principal projections of this instance.</returns>
        /// <seealso cref="PrincipalProjections"/>
        /// <exception cref="InvalidOperationException">
        /// The Singular Value Decomposition needed to acquire 
        /// the principal information 
        /// of this instance cannot be executed or does not converge.<br/>
        /// -or-<br/>
        /// No principal variable has positive variance. 
        /// The principal information cannot be acquired.
        /// </exception>
        public PrincipalProjections GetPrincipalProjections()
        {
            return new PrincipalProjections(this);
        }

        #endregion

        #region Cloud modifications

        /// <summary>
        /// Returns a <see cref="Cloud"/> representing the same points
        /// of this instance, using coordinates referred to the 
        /// specified basis.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The point coordinates are updated to be referable
        /// to the new basis.
        /// </para>
        /// </remarks>
        /// <returns>
        /// A <see cref="Cloud"/> representing the same points
        /// of this instance, using coordinates referred to the 
        /// specified basis.</returns>
        /// <param name="newBasis">The new basis.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="newBasis" /> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException"> 
        /// <paramref name="newBasis" /> has a <see cref="Basis.Dimension"/>
        /// not equal to that of the current one.
        /// </exception>
        public Cloud Rebase(Basis newBasis)
        {
            #region Input validation

            ArgumentNullException.ThrowIfNull(newBasis);

            int k = this.Basis.Dimension;

            if (newBasis.Dimension != k)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(newBasis),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_BASES_MUST_SHARE_DIMENSION"));
            }

            #endregion

            var a = this.Basis;
            var b = newBasis;
            var x_sa = this.coordinates;
            var x_sb = Basis.ChangeCoordinates(
                newBasis: b,
                currentCoordinates: x_sa,
                currentBasis: a);

            return new Cloud(
                coordinates: x_sb,
                weights: this.weights.Clone(),
                basis: b,
                copyData: false);
        }

        /// <summary>
        /// Returns a modified <see cref="Cloud"/> having zero mean 
        /// coordinates.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The collection of the coordinates of all points on a given direction 
        /// is referred to as an <i>active variable</i>.
        /// The returned cloud is forced to have zero mean coordinates by subtracting
        /// to each active variable its corresponding mean.
        /// </para>
        /// </remarks>
        /// <returns>
        /// A <see cref="Cloud"/> having the same base and weights
        /// of this instance, with zero mean 
        /// coordinates.
        /// </returns>
        public Cloud Center()
        {
            var centred_x_sa = this.coordinates.Clone();
            var m_sa = this.Mean;

            for (int i = 0; i < centred_x_sa.NumberOfRows; i++)
            {
                centred_x_sa[i, ":"] -= m_sa;
            }

            return new Cloud(
                coordinates: centred_x_sa, 
                weights: this.weights.Clone(),
                basis: this.Basis,
                copyData: false);
        }

        /// <summary>
        /// Returns a standardized <see cref="Cloud"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The collection of the coordinates of all points on a given direction 
        /// is referred to as an <i>active variable</i>.
        /// Each active variable is standardized by subtracting its mean
        /// and then dividing the difference by its standard deviation.
        /// </para>
        /// </remarks>
        /// <returns>
        /// A <see cref="Cloud"/> having the same base and weights
        /// of this instance, with zero mean 
        /// coordinates and unit variances.
        /// </returns>
        public Cloud Standardize()
        {            
            var activeVariableVariances = this.GetVariances(this.coordinates);
            var centredCloud = this.Center();

            var centred_x_sa = centredCloud.coordinates;

            for (int j = 0; j < centred_x_sa.NumberOfColumns; j++)
            {
                double standardDeviation = Math.Sqrt(activeVariableVariances[j]);
                centred_x_sa[":", j] /= standardDeviation;
            }

            return new Cloud(
                coordinates: centred_x_sa,
                weights: centredCloud.weights,
                basis: centredCloud.Basis,
                copyData: false);
        }

        #endregion
    }
}