// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;
using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Interop;
using System;
using System.Globalization;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents the principal projections of a set of
    /// multidimensional, weighted points,
    /// and supports their elaboration in terms of 
    /// directions, coordinates, variances, and point contributions 
    /// to the overall variability.
    /// </summary>
    /// <remarks>
    /// <para><b>Multidimensional weighted points and their 
    /// statistics</b></para>
    /// <para>
    /// Let us consider <latex>n</latex> points <latex>x_1,\dots,x_n</latex> in
    /// <latex>\R^K</latex>.
    /// Their statistical characteristics are usually analyzed
    /// by imposing a <i>weighting scheme</i> to them, that is, a
    /// relative weight <latex>w_i>0</latex> (possibly
    /// elementary, i.e. <latex>w_i=1/n</latex>) is assigned to 
    /// point <latex>x_i</latex>, 
    /// and controls how the point contributes to the
    /// overall statistics, provided that the weights sum
    /// up to <latex>1</latex>. Such set of weighted points can thus 
    /// be expressed
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
    /// row is <latex>\T{x_i}</latex> (the transpose of <latex>x_i</latex>), 
    /// and <latex>w_S</latex> is 
    /// the weighting scheme expressed as a sequence:
    /// <latex mode="display">
    /// w_S=\T{\round{w_1,\dots, w_n}}.   
    /// </latex>
    /// </para>
    /// <para>
    /// Given a basis <latex>\A</latex> of <latex>\R^K</latex>, a 
    /// structure 
    /// <latex>S</latex> can be represented by a 
    /// <i>cloud</i>, say <latex>\bc{A}{\C_S}</latex>, which 
    /// can be formally defined as the
    /// triplet <latex>(\A,\bc{A}{X_S},w_S)</latex>, where
    /// <latex mode="display">
    /// \bc{A}{X_S} =\mx{
    ///     \T{\bc{A}{x_1}} \\ \vdots \\ \T{\bc{A}{x_n}}
    ///     }
    /// </latex>
    /// is the coordinates matrix w.r.t. <latex>\A</latex> of the points
    /// in <latex>S</latex>, i.e., its <latex>i</latex>-th row, 
    /// <latex>\T{\bc{A}{x_i}}</latex>, stands for the coordinates 
    /// of point <latex>x_i</latex>.
    /// </para>
    /// <para>
    /// The basic statistics of <latex>S</latex> can be defined 
    /// as follows. Its 
    /// <i>mean point</i>, the vector <latex>m_S</latex>, 
    /// whose <latex>\A</latex> coordinates are given by
    /// <latex>\bc{A}{m_S} = \T{\bc{A}{X_S}}\,w_S</latex>,
    /// is
    /// <latex mode="display">\label{eq:Mean_S}
    /// m_S = A\,\T{\bc{A}{X_S}}\,w_S.
    /// </latex>
    /// Furthermore, the <i>variance</i> of <latex>S</latex>, 
    /// say <latex>\Var_S</latex>, is defined 
    /// as follows:
    /// <latex mode="display">\label{eq:Var_S}
    ///     \Var_S = \sum_{i=1}^{n} w_i \round{\bdist{A}{x_i,m_S}}^2,    
    /// </latex>
    /// where
    /// <latex mode="display">
    /// \bdist{A}{x,y} 
    /// = \bnorm{A}{x-y} \\
    /// = \round{
    ///     \T{\round{\bc{A}{x} - \bc{A}{y}}}\,
    ///     \Q{A}\,
    ///     \round{\bc{A}{x} - \bc{A}{y}}
    ///     }^{1/2}\\
    /// </latex>
    /// is the distance induced by basis <latex>\A</latex> via 
    /// its <i>norm</i>
    /// <latex mode="display">
    /// \bnorm{A}{x} = \bsprod{A}{x|x}^{1/2},
    /// </latex>
    /// and <i>scalar product</i>
    /// <latex mode="display">
    /// \bsprod{A}{x|y} = \T{\bc{A}{x}}\,\Q{A}\,\bc{A}{y},
    /// </latex>
    /// where <latex>x,y \in \R^K</latex> and
    /// <latex mode="display">
    /// \Q{A} = \T{A}\,A.
    /// </latex>
    /// The columns of <latex>\bc{A}{X_S}</latex> are referred to as 
    /// the <i>active</i> variables 
    /// observed at the <latex>S</latex> points w.r.t. basis 
    /// <latex>\A</latex>. The covariance matrix of such variables 
    /// can be defined 
    /// as follows,
    /// <latex mode="display">
    /// \Cov_{S,\A} = \T{\bc{A}{X_S}}\,W_S\,\bc{A}{X_S},
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
    /// <para>
    /// Notice how the variance of <latex>S</latex> can be characterized 
    /// in terms of the covariances
    /// w.r.t. to <latex>\A</latex>,
    /// <latex mode="display">
    ///     \Var_S = \trace{ \Cov_{S,\A}\,\Q{A} },
    /// </latex>
    /// with <latex>\Trace</latex> being the trace operator.
    /// </para>
    /// <para><b>Principal projections</b></para>
    /// <para>
    /// A set of weighted points in a vector space
    /// can be represented by different clouds, simply by selecting different
    /// bases for that space,
    /// but the basic statistics of <latex>S</latex>, its mean and
    /// variance constants, are the same irrespective of
    /// the cloud selected to analyze <latex>S</latex>.
    /// On the contrary, the covariances are not coordinate-free, i.e.
    /// they depend on the basis chosen to represent the points 
    /// in <latex>S</latex>.
    /// As a consequence, a question raises
    /// about the choice of the basis to apply when observing the data
    /// and trying to analyze their variability.
    /// Is it possible
    /// to select a basis that enhances our comprehension of the variance 
    /// of <latex>S</latex>, better figuring out how it can 
    /// be explained given the available evidence?  
    /// </para>
    /// <para>
    /// A possible approach is that of seeking a basis such that the variables 
    /// in the corresponding cloud become uncorrelated, so simplifying the 
    /// interpretation of <latex>S</latex>. In addition, if <latex>S</latex> is a
    /// high-dimensional structure, another 
    /// useful goal would be that of approximating the cloud in a lower 
    /// dimensional space, defined so as to
    /// minimize the lost of information due to a representation 
    /// of <latex>S</latex> obtained
    /// in a reduced space.
    /// Such tasks can be approached by projecting the points 
    /// in <latex>S</latex> along
    /// its <i>principal directions</i><cite>leroux-rouanet-2004</cite>. 
    /// </para>
    /// <para>
    /// When projecting the <latex>S</latex> points along a direction 
    /// in <latex>\R^K</latex>, the collection of points resulting from 
    /// the projection defines a new projected structure,
    /// whose variance can contribute to the explanation 
    /// of <latex>\Var_S</latex>.
    /// The <i>first principal direction</i> of <latex>S</latex>, 
    /// say <latex>d_1</latex>, is thus selected
    /// so that the variance of its projected structure, 
    /// say <latex>S\round{d_1}</latex>, is maximal.
    /// With a first projected structure, there is associated a
    /// residual structure, say <latex>R_1</latex>, whose points have a 
    /// variance that is the
    /// difference between <latex>\Var_S</latex> 
    /// and <latex>\Var_{S\round{d_1}}</latex>, i.e. it
    /// represents a measure of the <latex>S</latex> variability not 
    /// yet explained exploiting
    /// the first principal direction.
    /// If such a measure is not considered sufficiently low,
    /// one determines the
    /// <i>second principal direction</i> of <latex>S</latex>, 
    /// say <latex>d_2</latex>, obtained by
    /// maximizing the variance of <latex>R_1\round{d_2}</latex> under 
    /// the constraint that
    /// <latex>d_2</latex> is orthogonal to <latex>d_1</latex>.
    /// From the second
    /// residual structure <latex>R_2</latex>, one proceeds in the same 
    /// way to determine a
    /// <it>third principal direction</it>, perpendicular to the previous ones, 
    /// and so on, until the determination of, say, <latex>L</latex> principal 
    /// directions able
    /// to guarantee the required explanation of the <latex>S</latex> variance.
    /// </para>
    /// <para><b>Instantiation</b></para>
    /// <para>
    /// The cloud <latex>\C_{S,\A}</latex> exploited to initially represent the 
    /// structure <latex>S</latex> is referred to as the 
    /// <it>active</it> cloud.
    /// Given a <see cref="Cloud"/> instance representing
    /// <latex>\C_{S,\A}</latex>,
    /// a corresponding <see cref="PrincipalProjections"/> object can be obtained 
    /// by calling on it method <see cref="Cloud.GetPrincipalProjections"/>.
    /// </para>
    /// <para>
    /// Projecting points in lower dimensional spaces characterizes several
    /// statistical multidimensional methods, such as the 
    /// <see cref="PrincipalComponents">
    /// Principal Components</see> Analysis, the 
    /// <see cref="Correspondence"/>, and 
    /// the <see cref="MultipleCorrespondence">
    /// Multiple Correspondence</see> Analyses. All these classes provides
    /// methods to create the required <see cref="PrincipalProjections"/>
    /// instances, and properties to access them.
    /// </para>
    /// <para>
    /// Once created, the <see cref="PrincipalProjections"/> instance enables 
    /// the access to the initial cloud via its
    /// property <see cref="ActiveCloud"/>.
    /// </para>
    /// <para>
    /// The identification of the principal directions
    /// of <latex>S</latex> is provided by the eigendecomposition 
    /// of matrix 
    /// <latex>\Cov_{S,\A}\,Q_{\A}</latex>. 
    /// In fact, 
    /// let <latex>\Delta</latex> be the <latex>K \times K</latex> matrix 
    /// whose columns are the
    /// normalized eigenvectors of <latex>\Cov_{S,\A}\,\Q{A}</latex> w.r.t.
    /// basis <latex>\A</latex>, 
    /// that is <latex>\Delta</latex> satisfies the conditions
    /// <latex mode="display">
    /// \Cov_{S,\A}\,\Q{A}\,\Delta = \Delta\,\Lambda, 
    /// </latex>
    /// and
    /// <latex mode="display">
    /// \T{\Delta}\,\Q{A}\,\Delta = I_K, 
    /// </latex>
    /// where <latex depth="-2">\Lambda</latex> is the diagonal matrix whose entries 
    /// are the corresponding
    /// eigenvalues of <latex>\Cov_{S,\A}\,\Q{A}</latex> in decreasing order. 
    /// Then the
    /// principal directions <latex>d_1,\dots, d_K</latex> of <latex>S</latex> 
    /// satisfy
    /// <latex mode="display">
    ///     \mx{
    ///         \bc{A}{d_1} \quad \hdots \quad \bc{A}{d_K}
    ///     } = \Delta.
    /// </latex>
    /// The rows of <latex>\T{\Delta}</latex> can be interpreted
    /// as the <it>coordinates</it> w.r.t. basis <latex>\A</latex> 
    /// of the basis, say <latex>\basis{P}</latex>,
    /// whose matrix is given by
    /// <latex mode="display">
    /// P = A\,\Delta,
    /// </latex>
    /// and the points in <latex>S</latex> admit the following 
    /// coordinates matrix w.r.t. <latex>\basis{P}</latex>:
    /// <latex mode="display">
    ///     \bc{P}{X_S} = \bc{A}{X_S}\,\Q{A}\,\Delta.
    /// </latex>
    /// This argument suggests that a <i>principal cloud</i> of 
    /// <latex>S\equiv\round{X_S,w_S}</latex> can be defined as the cloud 
    /// <latex>\C_{S,\basis{P}}=\round{\basis{P},\bc{P}{X_S},w_S}</latex>.
    /// </para>
    /// <para><b>Approximations in lower dimensional spaces</b></para> 
    /// <para>
    /// The dimension of the <i>active</i> cloud is equal to that of the 
    /// corresponding
    /// <it>principal</it> one. However, often not all the <latex>K</latex> principal
    /// variables are taken 
    /// into account: by keeping only some of the first ones, 
    /// say <latex>L &lt; K</latex>, 
    /// a dimensionality reduction can be achieved, while simultaneously 
    /// preserving 
    /// - as much as possible - the original variance of the points 
    /// in <latex>S</latex>. 
    /// </para>
    /// <para>
    /// A <see cref="PrincipalProjections"/> instance reports information about
    /// such <latex>L</latex> variables. First of all, <latex>L</latex> is 
    /// returned by property <see cref="NumberOfDirections"/>. Additional insights
    /// are exposed as follows.
    /// </para>
    /// <para><b><i>Variance breakdowns</i></b></para>
    /// <para>
    /// The covariance matrix of a principal cloud is 
    /// characterized as follows:
    /// <latex mode="display">
    ///   \Cov_{S,\basis{P}} = \Lambda,
    /// </latex>
    /// hence the variance of <latex>S</latex> can be 
    /// factorized as follows:
    /// <latex mode="display">
    /// \Var_{S} 
    /// =
    ///   \trace{\Cov_{C_{S,\basis{P}}} \Q{P}} 
    /// =
    ///   \trace{\Cov_{C_{S,\basis{P}}}} 
    /// =
    ///   \sum_{j=1}^{K}{\Lambda_{j,j}}
    /// \simeq \sum_{j=1}^{L}{\Lambda_{j,j}}.
    /// </latex>
    /// </para>
    /// <para>
    /// A finer factorization of <latex>\Var_S</latex> is 
    /// obtained by taking 
    /// into account the specific contributions of each point in <latex>S</latex> 
    /// to the overall variance. 
    /// Remember that the <i>active</i> and <i>principal</i>
    /// clouds represent the same weighted structure <latex>S</latex>, hence 
    /// the distances among
    /// cloud points and the corresponding means are also preserved.
    /// Thus one has
    /// <latex mode="display">
    ///     \Var_S
    ///     =
    ///     \sum_{i=1}^{n}w_i\,\bdist{P}{x_i,m_S}^2\\
    ///     =
    ///     \sum_{i=1}^{n}\sum_{j=1}^{K}w_i\,
    ///          \round{ x_{i,\basis{P},j} - m_{S,\basis{P},j} }^2.  
    /// </latex>
    /// The quantity 
    /// <latex mode="display">
    ///     \Psi_{i,j} = w_i\,
    ///     \round{ x_{i,\basis{P},j} - m_{S,\basis{P},j} }^2
    /// </latex>
    /// can thus be interpreted as the amount of the 
    /// the <latex>j</latex>-th principal variable's variance
    /// due to the <latex>i</latex>-th point of the cloud, since
    /// <latex mode="display">
    ///     \Var_S
    ///     \simeq
    ///     \sum_{i=1}^{n}\sum_{j=1}^{L}\Psi_{i,j}.
    /// </latex>
    /// Such values can also be exploited to supply aids to the 
    /// interpretation of the principal cloud. In particular, 
    /// the relative <i>contribution</i> of the <latex>i</latex>-th point 
    /// to the variance of the 
    /// <latex>j</latex>-th principal variable is defined as the quantity
    /// <latex mode="display">
    ///     \frac{\Psi_{i,j}}{\Lambda_{j,j}},
    /// </latex>
    /// which is the generic entry of the 
    /// matrix returned by property <see cref="Contributions"/>,
    /// while the quality of representation of the <latex>i</latex>-th 
    /// point of <latex>S</latex>
    /// on the <latex>j</latex>-th principal direction as
    /// <latex mode="display">
    ///     \frac{\Psi_{i,j}}{w_i\,\bdist{A}{x_i,m_S}^2}
    ///     \equiv \cos^2\round{\theta_{i,j}},
    /// </latex>
    /// where <latex>\theta_{i,j}</latex> is the angle between the 
    /// vectors <latex>x_i</latex> and 
    /// <latex>d_j</latex>. You can inspect the squared cosines 
    /// by getting property <see cref="RepresentationQualities"/>.
    /// <note>
    /// Directions are added 
    /// until the corresponding projected variance is greater than 1e-6. 
    /// </note>
    /// </para>
    /// <para><b><i>Relationships between active and principal variables
    /// </i></b></para>
    /// <para>
    /// Given an <i>active</i> cloud <latex>\C_{S,\A}</latex> and a 
    /// corresponding principal 
    /// cloud <latex>\C_{S,\basis{P}}</latex>, one can regress the
    /// active variables on the first <latex>L</latex> standardized 
    /// principal variables 
    /// as follows.
    /// Let <latex>\bc{A}{y_j}</latex> be the <latex>j</latex>-th 
    /// column of <latex>\bc{A}{X_S}</latex>, i.e., 
    /// the <latex>j</latex>-th <i>active</i> variable; furthermore, define
    /// <latex>
    ///     \bc{P}{\tilde{X}_S}
    /// </latex>
    /// as the matrix representing the first 
    /// <latex>L</latex> columns of <latex>\bc{P}{X_S}</latex>, and
    /// <latex>
    ///     \tilde{\Lambda}
    /// </latex>
    /// as the sub-matrix of  
    /// <latex depth="-2">\Lambda</latex> given by deleting its last
    /// <latex>K - L</latex> rows and columns.
    /// Since the points are weighted, the regression can be achieved 
    /// by applying the principle of Weighted Least Squares to define the 
    /// following optimization
    /// problem:
    /// <latex mode="display">
    ///     \hat{\beta}_j = \argmin_{\beta \in \R^K}
    ///     \sum_{i=1}^{n} w_i \,
    ///     \Dist_{\mathcal{I}_n} \round{ \bc{A}{y_j}, Z\,\beta }^2,    
    /// </latex>
    /// where, <latex>1_n</latex> being a column vector of <latex>n</latex> ones,
    /// <latex mode="display">
    /// Z=\round{ I_n - 1_n\,\T{w_S} }
    ///   \bc{P}{\tilde{X}_S}\,\tilde{\Lambda}^{-1/2}
    /// </latex>
    /// is the matrix of the first <latex>L</latex> standardized 
    /// principal coordinates.
    /// Thus, for <latex>j=1,\dots,K</latex>,
    /// <latex mode="display">
    /// \hat{\beta}_j = 
    ///    \Inv{\round{\T{Z}\,W_S\,Z }} \T{Z}\,W_S\,\bc{A}{y_j}.
    /// </latex>
    /// It can be noted that 
    /// <latex mode="display">
    ///     \T{Z}\,W_S\,Z = I_L,
    /// </latex>
    /// hence one can define the following <latex>K \times L</latex> matrix 
    /// of regression coefficients:
    /// <latex mode="display">
    /// B \equiv \mx{
    ///                     \T{\hat{\beta}_1} \\
    ///                     \vdots \\
    ///                     \T{\hat{\beta}_K} \\
    ///      }
    ///      = \T{\bc{A}{X_S}}\,W_S\,Z.
    /// </latex>
    /// Matrix <latex>B</latex> can be analyzed by getting property 
    /// <see cref="RegressionCoefficients"/>.
    /// </para>
    /// <para>
    /// The correlations among the <latex>j</latex>-th <i>active</i> variable 
    /// and the <latex>L</latex> 
    /// standardized principal variables can thus be obtained as follows:
    /// <latex mode="display">
    /// \hat{\rho}_j = \hat{\beta}_j / \sqrt{Cov_{S,\A,j,j}}
    /// </latex>
    /// hence the following <latex>K \times L</latex> matrix of correlations:
    /// <latex mode="display">
    /// \Omega \equiv \mx{
    ///                     \T{\hat{\rho}_1} \\
    ///                     \T{\hat{\rho}_2} \\
    ///                     \vdots \\
    ///                     \T{\hat{\rho}_K} \\
    ///      }
    ///      = 
    ///      \mx{
    ///         Cov_{S,\A,1,1}  &amp; 0              &amp; \cdots &amp; 0\\ 
    ///         0               &amp; Cov_{S,\A,2,2} &amp; \cdots &amp; 0\\
    ///         \vdots          &amp; \vdots         &amp; \ddots &amp; \vdots \\
    ///         0               &amp; 0              &amp; \cdots &amp; Cov_{S,\A,K,K}
    ///     }^{-1/2}\,B,
    /// </latex>
    /// which is returned by property <see cref="Correlations"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="Correspondence"/>
    /// <seealso cref="PrincipalComponents"/>
    /// <seealso cref="MultipleCorrespondence"/>
    /// <seealso cref="Cloud"/>
    /// <seealso href="http://en.wikipedia.org/wiki/Geometric_data_analysis"/>
    public class PrincipalProjections
    {
        #region State

        private const double minimalProjectedVariance = 1e-6;

        /// <summary>
        /// Gets the active cloud of this instance.
        /// </summary>
        /// <value>The active cloud of this instance.</value>
        public Cloud ActiveCloud { get; private set; }

        /// <summary>
        /// Gets the relative contributions of the projected points to the 
        /// variances of the principal variables.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Each column of the principal <see cref="Coordinates"/>
        /// represents a <it>principal</it> variable,
        /// characterized by a specific variance.
        /// </para>
        /// <para>
        /// The point contributions
        /// are returned as a matrix 
        /// in which rows correspond to points, and columns correspond 
        /// to principal variables.
        /// </para>
        /// </remarks>
        /// <value>
        /// The point relative contributions to the variances of the
        /// principal variables.</value>
        public ReadOnlyDoubleMatrix Contributions { get; private set; }

        /// <summary>
        /// Gets the principal coordinates of the projected points.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The coordinates of the projected points on the 
        /// <latex>L</latex> principal directions
        /// are returned as a matrix in which rows correspond to points, 
        /// and columns correspond to directions.
        /// Each row represents the principal coordinates of the 
        /// corresponding point.
        /// </para>
        /// </remarks>
        /// <value>The principal coordinates of the projected points.</value>
        public ReadOnlyDoubleMatrix Coordinates { get; private set; }

        /// <summary>
        /// Gets the correlations among the active variables and 
        /// the standardized principal variables.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Returns a matrix having as many rows as the number of 
        /// active variables,
        /// and as many columns as the number of principal ones. 
        /// A given entry
        /// is the correlation between an active variable, 
        /// that corresponding to the entry row,
        /// and a principal variable, that corresponding to the entry column.
        /// </para>
        /// </remarks>
        /// <value>
        /// The correlations among active and 
        /// standardized principal variables.
        /// </value>
        public ReadOnlyDoubleMatrix Correlations { get; private set; }

        /// <summary>
        /// Gets the point representation qualities on each principal direction.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The quality figures are returned as a matrix
        /// in which rows correspond to points, and columns correspond to directions.
        /// </para>
        /// </remarks>
        /// <value>The qualities of point representations on the principal directions.</value>
        public ReadOnlyDoubleMatrix RepresentationQualities { get; private set; }

        /// <summary>
        /// Gets the variances of the principal variables.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Each column in the <see cref="Coordinates"/> of the 
        /// projected points 
        /// on the principal directions
        /// is referred to as 
        /// a <i>principal variable</i>.
        /// Property <see cref="Variances"/> gets the variances of 
        /// such variables.
        /// </para>
        /// </remarks>
        /// <seealso cref="Coordinates"/>
        /// <value>The variances of the principal variables.</value>
        public ReadOnlyDoubleMatrix Variances { get; private set; }

        // The coordinates of the first L principal directions
        // w.r.t. the active basis.
        private ReadOnlyDoubleMatrix delta_tilde;

        /// <summary>
        /// Gets the coordinates of the principal directions
        /// w.r.t. the basis of the <see cref="ActiveCloud"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Returns a matrix whose columns are the coordinates of the 
        /// principal directions w.r.t. the <see cref="Cloud.Basis"/>
        /// of the <see cref="ActiveCloud"/>.
        /// </para>
        /// </remarks>
        /// <value>The coordinates of the principal directions
        /// w.r.t. the basis of the <see cref="ActiveCloud"/>.</value>
        public ReadOnlyDoubleMatrix Directions
        {
            get
            {
                return this.delta_tilde;
            }

            private set { this.delta_tilde = value; }
        }

        /// <summary>
        /// Gets the number of principal directions.
        /// </summary>
        /// <value>The number of principal directions.</value>
        public int NumberOfDirections
        {
            get
            {
                return this.Coordinates.NumberOfColumns;
            }
        }

        /// <summary>
        /// Gets the coefficients of the regression of each active variable on 
        /// the standardized principal variables.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Returns a matrix having as many rows as the number of active variables,
        /// and as many columns as the number of principal ones. Each row stores
        /// the estimated coefficients of a regression in which the corresponding 
        /// active variable is modeled on a collection of explanatory variables 
        /// represented by the 
        /// standardized principal variables (no intercept included).
        /// </para>
        /// </remarks>
        /// <value>
        /// The regression coefficients of the active variables on the standardized
        /// principal ones.
        /// </value>
        public ReadOnlyDoubleMatrix RegressionCoefficients { get; private set; }

        #endregion

        #region Constructors and factory methods

        private PrincipalProjections()
        { }

        /// <summary>
        /// Initializes instances of 
        /// the <see cref="PrincipalProjections"/> class
        /// representing the principal information 
        /// of row and column profiles of 
        /// the specified data.
        /// </summary>
        /// <param name="data">
        /// The data to analyze.
        /// </param>
        /// <param name="rowPrincipalProjections">
        /// The principal projections regarding 
        /// the cloud row profiles of <paramref name="data"/>.
        /// </param>
        /// <param name="columnPrincipalProjections">
        /// The principal projections regarding 
        /// the cloud of column profiles of <paramref name="data"/>.
        /// </param>
        internal static
            void FromReciprocalAveraging(
                DoubleMatrix data,
                out PrincipalProjections rowPrincipalProjections,
                out PrincipalProjections columnPrincipalProjections)
        {
            #region Principal directions and variances

            var n_r = Stat.Sum(data, DataOperation.OnRows);

            var n_c = Stat.Sum(data, DataOperation.OnColumns);

            if (!(n_r.FindWhile(x => x <= 0.0) is null)
                ||
                !(n_c.FindWhile(x => x <= 0.0) is null))
            {
                throw new ArgumentOutOfRangeException(nameof(data),
                   ImplementationServices.GetResourceString(
                   "STR_EXCEPT_GDA_NON_POSITIVE_MARGINAL_SUMS"));
            }

            var n = Stat.Sum(n_r);
            var sqrt_n = Math.Sqrt(n);

            var f_r = n_r / n;

            n_c.InPlaceTranspose();

            var f_c = n_c / n;

            var inv_n_r = n_r.Apply((x) => 1.0 / x);
            var inv_n_c = n_c.Apply((x) => 1.0 / x);

            var diag_inv_n_r = DoubleMatrix.Diagonal(inv_n_r);
            var diag_inv_n_c = DoubleMatrix.Diagonal(inv_n_c);

            var inv_sqrt_n_r = n_r.Apply((x) => 1.0 / Math.Sqrt(x));
            var inv_sqrt_n_c = n_c.Apply((x) => 1.0 / Math.Sqrt(x));

            var diag_inv_sqrt_n_r = DoubleMatrix.Diagonal(inv_sqrt_n_r);
            var diag_inv_sqrt_n_c = DoubleMatrix.Diagonal(inv_sqrt_n_c);

            var sqrt_f_r = f_r.Apply((x) => Math.Sqrt(x));
            var sqrt_f_c = f_c.Apply((x) => Math.Sqrt(x));

            var diag_sqrt_f_r = DoubleMatrix.Diagonal(sqrt_f_r);
            var diag_sqrt_f_c = DoubleMatrix.Diagonal(sqrt_f_c);

            var pre_g = data - f_r * n_c.Transpose();
            var g = diag_inv_sqrt_n_r * pre_g * diag_inv_sqrt_n_c;

            // SVD of g

            char job_u = 'S';
            char job_v_t = 'S';

            int J = g.NumberOfRows;
            int K = g.NumberOfColumns;
            var p = Math.Min(J, K);

            double[] g_array = g.GetStorage();
            int ld_a = J;

            double[] d_array = new double[p];

            var u = DoubleMatrix.Dense(J, p);
            double[] u_array = u.GetStorage();
            int ld_u = J;

            var v_t = DoubleMatrix.Dense(p, K);
            double[] v_t_array = v_t.GetStorage();
            int ld_v_t = p;

            int info;
            double[] superb = new double[p - 1];
            info = SafeNativeMethods.LAPACK_dgesvd(
                matrix_layout: SafeNativeMethods.LAPACK.ORDER.ColMajor,
                jobu: job_u,
                jobvt: job_v_t,
                m: J,
                n: K,
                a: g_array,
                lda: ld_a,
                s: d_array,
                u: u_array,
                ldu: ld_u,
                vt: v_t_array,
                ldvt: ld_v_t,
                superb);
            if (info != 0)
            {
                throw new InvalidOperationException(
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_SVD_ERRORS"));
            }

            var v = v_t.Transpose();

            var variances = DoubleMatrix.Dense(p, 1, d_array);
            variances.InPlaceApply(x => Math.Pow(x, 2.0));

            var principalIndexes = variances.FindWhile(x => x > minimalProjectedVariance);

            if (principalIndexes is null)
            {
                throw new InvalidOperationException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_GDA_NON_POSITIVE_PRINCIPAL_VARIANCES"));
            }

            variances = variances.Vec(principalIndexes);

            #endregion

            #region Row principal projections

            var a_r = diag_inv_sqrt_n_c * sqrt_n;

            var x_r = diag_inv_n_r * data;

            var w_r = f_r;

            var c_r = new Cloud(
                coordinates: x_r,
                weights: w_r,
                basis: new Basis(a_r));

            rowPrincipalProjections = new PrincipalProjections
            {
                ActiveCloud = c_r
            };

            var delta_tilde_r = diag_sqrt_f_c * v[":", principalIndexes];
            rowPrincipalProjections.delta_tilde =
                (delta_tilde_r).AsReadOnly();

            var rowCoordinates = x_r * c_r.Basis.basisScalarProducts * delta_tilde_r;

            if (data.HasRowNames)
            {
                ImplementationServices.SetMatrixRowNames(
                    matrix: rowCoordinates,
                    indexNamePairs: data.RowNames);
            }

            for (int j = 0; j < rowCoordinates.NumberOfColumns; j++)
            {
                rowCoordinates.SetColumnName(
                    columnIndex: j,
                    columnName: GetPrincipalVariableName(j));
            }

            rowPrincipalProjections.Coordinates =
                rowCoordinates
                    .AsReadOnly();

            rowPrincipalProjections.Variances = variances.AsReadOnly();

            var squaredDistanceComponents =
                rowPrincipalProjections.GetSquaredDistanceComponents();

            rowPrincipalProjections.Contributions =
                rowPrincipalProjections
                    .GetContributions(squaredDistanceComponents)
                        .AsReadOnly();

            rowPrincipalProjections.RepresentationQualities =
                rowPrincipalProjections
                    .GetRepresentationQualities(squaredDistanceComponents)
                        .AsReadOnly();

            var rowRegressionCoefficients = rowPrincipalProjections
                .GetRegressionCoefficients(
                    rowPrincipalProjections.ActiveCloud.Coordinates);

            rowPrincipalProjections.RegressionCoefficients =
                rowRegressionCoefficients.AsReadOnly();

            var rowActiveVariances = 
                c_r.GetVariances(c_r.Coordinates);

            rowPrincipalProjections.Correlations =
                PrincipalProjections.GetCorrelations(
                    rowRegressionCoefficients,
                    rowActiveVariances)
                        .AsReadOnly();

            #endregion

            #region Column principal projections

            var a_c = diag_inv_sqrt_n_r * sqrt_n;

            var x_c = diag_inv_n_c * data.Transpose();

            var w_c = f_c;

            var c_c = new Cloud(
                coordinates: x_c,
                weights: w_c,
                basis: new Basis(a_c));

            columnPrincipalProjections = new PrincipalProjections
            {
                ActiveCloud = c_c
            };

            var delta_tilde_c = diag_sqrt_f_r * u[":", principalIndexes];
            columnPrincipalProjections.delta_tilde =
                (delta_tilde_c).AsReadOnly();

            var columnCoordinates = x_c * c_c.Basis.basisScalarProducts * delta_tilde_c;

            if (data.HasColumnNames)
            {
                ImplementationServices.SetMatrixRowNames(
                    matrix: columnCoordinates,
                    indexNamePairs: data.ColumnNames);
            }

            for (int j = 0; j < columnCoordinates.NumberOfColumns; j++)
            {
                columnCoordinates.SetColumnName(
                    columnIndex: j,
                    columnName: GetPrincipalVariableName(j));
            }

            columnPrincipalProjections.Coordinates =
                columnCoordinates
                    .AsReadOnly();

            columnPrincipalProjections.Variances = variances.AsReadOnly();

            var columnSquaredDistanceComponents =
                columnPrincipalProjections.GetSquaredDistanceComponents();

            columnPrincipalProjections.Contributions =
                columnPrincipalProjections
                    .GetContributions(columnSquaredDistanceComponents)
                        .AsReadOnly();

            columnPrincipalProjections.RepresentationQualities =
                columnPrincipalProjections
                    .GetRepresentationQualities(columnSquaredDistanceComponents)
                        .AsReadOnly();

            var columnRegressionCoefficients = columnPrincipalProjections
                .GetRegressionCoefficients(
                    columnPrincipalProjections.ActiveCloud.Coordinates);

            columnPrincipalProjections.RegressionCoefficients =
                columnRegressionCoefficients.AsReadOnly();

            var columnActiveVariances =
                c_c.GetVariances(c_c.Coordinates);

            columnPrincipalProjections.Correlations =
                PrincipalProjections.GetCorrelations(
                    columnRegressionCoefficients,
                    columnActiveVariances)
                        .AsReadOnly();

            #endregion
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="PrincipalProjections"/> class
        /// that represents the principal information 
        /// of the specified <see cref="Cloud"/>.
        /// </summary>
        /// <param name="cloud">
        /// The cloud whose principal info must be computed.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// The Singular Value Decomposition needed to acquire 
        /// the principal directions cannot be executed or does not converge.<br/>
        /// -or-<br/>
        /// No principal variable has positive variance. 
        /// The principal information cannot be acquired.
        /// </exception>
        internal PrincipalProjections(Cloud cloud)
        {
            this.ActiveCloud = cloud;

            #region Principal directions and variances

            var cov_sa = cloud.Covariance;
            var a_t = cloud.Basis.basisMatrixT;
            var a = a_t.Transpose();
            var p = a * cov_sa * a_t;

            // Diagonalize cov_sa * q_a, where 
            // q_a = at * a

            char jobu = 'O';
            char jobvt = 'N';

            int k = p.NumberOfRows;
            double[] p_array = p.GetStorage();
            int lda = k;
            double[] lambda_array = new double[lda];
            double[] u = null;
            int ldu = lda;
            double[] vt = null;
            int ldvt = lda;
            int lapackInfo;
            double[] superb = new double[k - 1];
            lapackInfo = SafeNativeMethods.LAPACK_dgesvd(
                SafeNativeMethods.LAPACK.ORDER.ColMajor,
                jobu,
                jobvt,
                k,
                k,
                p_array,
                lda,
                lambda_array,
                u,
                ldu,
                vt,
                ldvt,
                superb);
            if (lapackInfo != 0)
            {
                throw new InvalidOperationException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_SVD_ERRORS"));
            }

            // Here p = a * delta

            var variances = DoubleMatrix.Dense(k, 1, lambda_array);

            var principalIndexes = variances.FindWhile(x => x > minimalProjectedVariance);

            if (principalIndexes is null)
            {
                throw new InvalidOperationException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_GDA_NON_POSITIVE_PRINCIPAL_VARIANCES"));
            }

            // Coordinates of principal directions w.r.t. 
            // the active cloud basis, i.e., 
            // this must be delta_tilde = a \ p_tilde.

            var p_tilde = p[":", principalIndexes];
            var delta_tilde =
                (DoubleMatrix.Identity(a.NumberOfColumns) / a)
                * p_tilde;

            this.Directions = delta_tilde.AsReadOnly();
            this.Variances = variances.Vec(principalIndexes).AsReadOnly();

            #endregion

            #region Coordinates

            this.Coordinates =
                this.GetPrincipalCoordinates(cloud.Coordinates).AsReadOnly();

            #endregion

            #region Contributions and representation qualities

            var squaredDistanceComponents =
                this.GetSquaredDistanceComponents();
            this.Contributions =
                this.GetContributions(squaredDistanceComponents)
                    .AsReadOnly();
            this.RepresentationQualities =
                this.GetRepresentationQualities(squaredDistanceComponents)
                    .AsReadOnly();

            #endregion

            #region Regression coefficients and correlations

            var regressionCoefficients =
                this.GetRegressionCoefficients(cloud.Coordinates);

            this.RegressionCoefficients =
                regressionCoefficients.AsReadOnly();

            var activeVariances = this.ActiveCloud.GetVariances(cloud.Coordinates);

            this.Correlations =
                PrincipalComponents.GetCorrelations(
                    regressionCoefficients,
                    activeVariances).AsReadOnly();

            #endregion
        }

        #endregion

        #region Supplementary info

        /// <summary>
        /// Gets the principal coordinates of the 
        /// specified supplementary points given
        /// their active coordinates.
        /// </summary>
        /// <param name="activeCoordinates">
        /// The coordinates of the supplementary points
        /// w.r.t. the basis of the <see cref="ActiveCloud"/>.
        /// </param>
        /// <returns>
        /// The principal coordinates of the supplementary points.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The <paramref name="activeCoordinates"/> of a point
        /// are the values that the <i>active</i> variables
        /// taken on at that point.
        /// </para>
        /// <para>
        /// The matrix passed as <paramref name="activeCoordinates"/>
        /// must have as many rows as the number of supplementary points, while
        /// the number of columns is the <see cref="Basis.Dimension"/> of
        /// the space on which the <see cref="ActiveCloud"/> lies.
        /// </para>        
        /// <para>
        /// The <paramref name="activeCoordinates"/> must be taken
        /// w.r.t. the basis of the <see cref="ActiveCloud"/> of this instance.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="activeCoordinates"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The number of columns 
        /// in <paramref name="activeCoordinates"/> is not equal
        /// to the number of active variables.</exception>
        public DoubleMatrix LocateSupplementaryPoints(
            DoubleMatrix activeCoordinates)
        {
            #region Input validation

            if (activeCoordinates is null)
            {
                throw new ArgumentNullException(
                    nameof(activeCoordinates));
            }

            if (this.ActiveCloud.Coordinates.NumberOfColumns !=
                activeCoordinates.NumberOfColumns)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(activeCoordinates),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_GDA_COLUMNS_NOT_EQUAL_TO_ACTIVE_VARIABLES"));
            }

            #endregion

            return this.GetPrincipalCoordinates(
                activeCoordinates);
        }

        /// <inheritdoc cref="LocateSupplementaryPoints(DoubleMatrix)"/>
        public DoubleMatrix LocateSupplementaryPoints(
            ReadOnlyDoubleMatrix activeCoordinates)
        {
            if (activeCoordinates is null)
                throw new ArgumentNullException(nameof(activeCoordinates));

            return this.LocateSupplementaryPoints(activeCoordinates.matrix);
        }

        /// <summary>
        /// Gets the coefficients of the regression of each specified 
        /// supplementary variable on 
        /// the standardized principal variables.
        /// </summary>
        /// <param name="supplementaryVariables">The supplementary variables.</param>
        /// <remarks>
        /// <para>
        /// The matrix passed as <paramref name="supplementaryVariables"/>
        /// must have as many rows as the number of points in 
        /// the <see cref="ActiveCloud"/>, while
        /// the number of columns is the number of supplementary variables.
        /// </para>        
        /// <para>
        /// Returns a matrix having as many rows as the number of supplementary 
        /// variables,
        /// and as many columns as the number of principal ones. Each row stores
        /// the estimated coefficients of a regression in which the corresponding 
        /// supplementary variable is modeled on a collection of explanatory variables 
        /// represented by the 
        /// standardized principal variables (no intercept included).
        /// </para>
        /// </remarks>
        /// <returns>
        /// The regression coefficients of the specified supplementary variables on 
        /// the standardized principal ones.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="supplementaryVariables"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The number of rows in <paramref name="supplementaryVariables"/> is not equal
        /// to the number of points in the active cloud.</exception>
        public DoubleMatrix RegressSupplementaryVariables(DoubleMatrix supplementaryVariables)
        {
            if (supplementaryVariables is null)
                throw new ArgumentNullException(nameof(supplementaryVariables));

            int numberOfVariables = supplementaryVariables.NumberOfRows;

            if (this.ActiveCloud.Coordinates.NumberOfRows != numberOfVariables)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(supplementaryVariables),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_GDA_ROWS_NOT_EQUAL_TO_ACTIVE_POINTS"));
            }

            return this.GetRegressionCoefficients(supplementaryVariables);
        }

        /// <inheritdoc cref="RegressSupplementaryVariables(DoubleMatrix)"/>
        public DoubleMatrix RegressSupplementaryVariables(ReadOnlyDoubleMatrix supplementaryVariables)
        {
            if (supplementaryVariables is null)
                throw new ArgumentNullException(nameof(supplementaryVariables));

            return this.RegressSupplementaryVariables(supplementaryVariables.matrix);
        }

        /// <summary>
        /// Gets the correlations of each specified 
        /// supplementary variable on 
        /// the standardized principal variables.
        /// </summary>
        /// <param name="supplementaryVariables">The supplementary variables.</param>
        /// <remarks>
        /// <para>
        /// The matrix passed as <paramref name="supplementaryVariables"/>
        /// must have as many rows as the number of points in 
        /// the <see cref="ActiveCloud"/>, while
        /// the number of columns is the number of supplementary variables.
        /// </para>        
        /// <para>
        /// Returns a matrix having as many rows as the number of 
        /// supplementary variables,
        /// and as many columns as the number of principal ones. 
        /// A given entry
        /// is the correlation between a supplementary variable, 
        /// that corresponding to the entry row,
        /// and a principal variable, that corresponding to the entry column.
        /// </para>
        /// </remarks>
        /// <returns>
        /// The correlations of each specified 
        /// supplementary variable on 
        /// the standardized principal ones.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="supplementaryVariables"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The number of rows in <paramref name="supplementaryVariables"/> is not equal
        /// to the number of points in the active cloud.</exception>
        public DoubleMatrix CorrelateSupplementaryVariables(DoubleMatrix supplementaryVariables)
        {
            if (supplementaryVariables is null)
                throw new ArgumentNullException(nameof(supplementaryVariables));

            int numberOfVariables = supplementaryVariables.NumberOfRows;

            if (this.ActiveCloud.Coordinates.NumberOfRows != numberOfVariables)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(supplementaryVariables),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_GDA_ROWS_NOT_EQUAL_TO_ACTIVE_POINTS"));
            }

            var supplementaryRegressionCoefficients = 
                this.GetRegressionCoefficients(supplementaryVariables);

            var supplementaryVariances =
                this.ActiveCloud.GetVariances(supplementaryVariables);

            return PrincipalProjections.GetCorrelations(
                supplementaryRegressionCoefficients,
                supplementaryVariances);
        }

        /// <inheritdoc cref="CorrelateSupplementaryVariables(DoubleMatrix)"/>
        public DoubleMatrix CorrelateSupplementaryVariables(ReadOnlyDoubleMatrix supplementaryVariables)
        {
            if (supplementaryVariables is null)
                throw new ArgumentNullException(nameof(supplementaryVariables));

            return this.CorrelateSupplementaryVariables(supplementaryVariables.matrix);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Gets the name of the principal variable having
        /// the specified index.
        /// </summary>
        /// <param name="index">The index of the principal variable.</param>
        /// <returns>
        /// The name of the principal variable having
        /// the specified index.
        /// </returns>
        private static string GetPrincipalVariableName(int index)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "P_{0}", 
                index + 1);
        }

        /// <summary>
        /// Gets the default name of the active variable having
        /// the specified index.
        /// </summary>
        /// <param name="index">The index of the principal variable.</param>
        /// <returns>
        /// The name of the principal variable having
        /// the specified index.
        /// </returns>
        private static string GetDefaultActiveVariableName(int index)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "A_{0}", 
                index + 1);
        }

        #region Coordinates

        /// <summary>
        /// Gets the principal coordinates of the 
        /// specified points given their 
        /// active coordinates.
        /// </summary>
        /// <param name="activeCoordinates">
        /// The active coordinates of the points under study.
        /// </param>
        /// <returns>
        /// The principal coordinates of the points under study.
        /// </returns>
        private DoubleMatrix GetPrincipalCoordinates(
            DoubleMatrix activeCoordinates)
        {
            var cloud = this.ActiveCloud;
            var x_sa = activeCoordinates;
            var q_a = cloud.Basis.basisScalarProducts;
            var delta_tilde = this.Directions;
            var principalCoordinates = x_sa * q_a * delta_tilde;

            if (activeCoordinates.HasRowNames)
            {
                ImplementationServices.SetMatrixRowNames(
                    matrix: principalCoordinates,
                    indexNamePairs: x_sa.RowNames);
            }

            for (int j = 0; j < principalCoordinates.NumberOfColumns; j++)
            {
                principalCoordinates.SetColumnName(
                    columnIndex: j,
                    columnName: GetPrincipalVariableName(j));
            }

            return principalCoordinates;
        }

        /// <inheritdoc cref="GetPrincipalCoordinates(DoubleMatrix)"/>
        private DoubleMatrix GetPrincipalCoordinates(
            ReadOnlyDoubleMatrix activeCoordinates)
        {
            return this.GetPrincipalCoordinates(
                activeCoordinates.matrix);
        }

        #endregion

        #region Contributions and representation qualities

        /// <summary>
        /// Gets the squared components of the distances among
        /// the principal points and their mean value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Returns an n-by-L matrix whose generic entry is
        /// <latex mode="display">
        /// \Phi_{i,j} = \round{x_{i,\basis{P},j} - m_{S,\basis{P},j} }^2.
        /// </latex>
        /// </para>
        /// </remarks>
        /// <returns>
        /// The squared components of the distances among
        /// the principal points and their mean value.
        /// </returns>
        private DoubleMatrix GetSquaredDistanceComponents()
        {
            var cloud = this.ActiveCloud;

            var x_sp = this.Coordinates;
            int L = this.NumberOfDirections;
            int n = x_sp.NumberOfRows;
            var w_s = cloud.Weights;

            var components = DoubleMatrix.Dense(n, L);

            var m_sp = w_s.Transpose() * x_sp;

            for (int i = 0; i < n; i++)
            {
                components[i, ":"] = x_sp[i, ":"] - m_sp;
            }

            components.InPlaceApply((x) => Math.Pow(x, 2.0));

            return components;
        }

        /// <summary>
        /// Gets the contributions of the 
        /// active points.
        /// </summary>
        /// <param name="squaredDistanceComponents">
        /// As returned by <see cref="GetSquaredDistanceComponents"/>.
        /// </param>
        /// <remarks>
        /// <para>
        /// Let <latex>C_{i,j}</latex> be the generic entry of
        /// <paramref name="squaredDistanceComponents"/>. This
        /// method returns
        /// <latex mode="display">
        /// \frac{w_{S,i}\,C_{i,j}}{\Lambda_{j,j}},
        /// </latex>
        /// where <latex>w_{S,i}</latex> is the <i>i</i>-th 
        /// entry of the weighting scheme in the 
        /// <see cref="ActiveCloud"/>, and 
        /// <latex>\Lambda_{j,j}</latex> is the variance of the <i>j</i>-th
        /// principal variable.
        /// </para>
        /// </remarks>
        /// <returns>
        /// The contributions of the active points.
        /// </returns>
        private DoubleMatrix GetContributions(
            DoubleMatrix squaredDistanceComponents)
        {
            var w_s = this.ActiveCloud.Weights;

            var contributions =
                DoubleMatrix.Diagonal(w_s)
                * squaredDistanceComponents
                * DoubleMatrix.Diagonal(this.Variances.Apply(x => 1.0 / x));

            var x_sp = this.Coordinates;

            if (x_sp.HasRowNames)
            {
                ImplementationServices.SetMatrixRowNames(
                    matrix: contributions,
                    indexNamePairs: x_sp.RowNames);
            }

            ImplementationServices.SetMatrixColumnNames(
                matrix: contributions,
                indexNamePairs: x_sp.ColumnNames);

            return contributions;
        }

        /// <summary>
        /// Gets the representation qualities of the 
        /// active points.
        /// </summary>
        /// <param name="squaredDistanceComponents">
        /// As returned by <see cref="GetSquaredDistanceComponents"/>.
        /// </param>
        /// <remarks>
        /// <para>
        /// Let <latex>C_{i,j}</latex> be the generic entry of
        /// <paramref name="squaredDistanceComponents"/>. This
        /// method returns
        /// <latex mode="display">
        /// \frac{C_{i,j}}{\bdist{A}{x_{i},m_S}^2},
        /// </latex>
        /// where <latex>x_{i}</latex> is the <i>i</i>-th 
        /// point of the weighted 
        /// structure <latex>S</latex> represented the 
        /// <see cref="ActiveCloud"/>, and <latex>m_{S}</latex>
        /// its mean point.
        /// </para>
        /// </remarks>
        /// <returns>
        /// The representation qualities of the active points.
        /// </returns>
        private DoubleMatrix GetRepresentationQualities(
            DoubleMatrix squaredDistanceComponents)
        {
            var cloud = this.ActiveCloud;

            var x_sp = this.Coordinates;
            int n = x_sp.NumberOfRows;
            var w_s = cloud.Weights;

            var invSquaredActiveDistances = DoubleMatrix.Dense(n, 1);

            var x_sa = cloud.Coordinates;
            var m_sa = w_s.Transpose() * x_sa;

            for (int i = 0; i < n; i++)
            {
                invSquaredActiveDistances[i] =
                    1.0
                    /
                    Math.Pow(
                        cloud.Basis.Distance(x_sa[i, ":"], m_sa),
                        2.0);
            }

            var representationQualities =
                DoubleMatrix.Diagonal(invSquaredActiveDistances)
                *
                squaredDistanceComponents;

            if (x_sp.HasRowNames)
            {
                ImplementationServices.SetMatrixRowNames(
                    matrix: representationQualities,
                    indexNamePairs: x_sp.RowNames);
            }

            ImplementationServices.SetMatrixColumnNames(
                matrix: representationQualities,
                indexNamePairs: x_sp.ColumnNames);

            return representationQualities;
        }

        #endregion

        #region Regression coefficients and correlations

        private DoubleMatrix GetRegressionCoefficients(DoubleMatrix variables)
        {
            var x_sp = this.Coordinates;

            int n = x_sp.NumberOfRows;
            var w_s = this.ActiveCloud.Weights;
            var w_s_t = w_s.Transpose();
            var diag_w_s = DoubleMatrix.Diagonal(w_s);
            var centering =
                DoubleMatrix.Identity(n) - DoubleMatrix.Dense(n, 1) * w_s_t;

            var var_sp = this.Variances;
            var invSqrtLambda_tilde = DoubleMatrix.Diagonal(
                var_sp.Apply((x) => 1.0 / Math.Sqrt(x)));

            var z = centering * x_sp * invSqrtLambda_tilde;

            var x_a = variables;
            var m_w_s = diag_w_s - w_s * w_s_t;
            var regressionCoefficients = x_a.Transpose() * (m_w_s * z);

            for (int i = 0; i < regressionCoefficients.NumberOfRows; i++)
            {
                regressionCoefficients.SetRowName(
                    rowIndex: i,
                    rowName: x_a.TryGetColumnName(i, out string columnName) ?
                        columnName : GetDefaultActiveVariableName(i));
            }

            ImplementationServices.SetMatrixColumnNames(
                matrix: regressionCoefficients,
                indexNamePairs: x_sp.ColumnNames);

            return regressionCoefficients;
        }

        /// <inheritdoc cref="GetRegressionCoefficients(DoubleMatrix)"/>
        private DoubleMatrix GetRegressionCoefficients(ReadOnlyDoubleMatrix variables)
        {
            return this.GetRegressionCoefficients(variables.matrix);
        }

        private static DoubleMatrix GetCorrelations(
            DoubleMatrix regressionCoefficients,
            DoubleMatrix variances)
        {
            var diagInvVariances = DoubleMatrix.Diagonal(
                variances.Apply((x) => 1.0 / Math.Sqrt(x)));

            var correlations = diagInvVariances * regressionCoefficients;

            ImplementationServices.SetMatrixRowNames(
                matrix: correlations,
                indexNamePairs: regressionCoefficients.RowNames);

            ImplementationServices.SetMatrixColumnNames(
                matrix: correlations,
                indexNamePairs: regressionCoefficients.ColumnNames);

            return correlations;
        }

        #endregion

        #endregion
    }
}
