// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

using Novacta.Analytics.Infrastructure;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents the multiple correspondence of a categorical data set.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A multiple correspondence examines the relationships
    /// existing among the variables observed in a categorical data set.     
    /// Both individuals and categories are represented as weighted points in 
    /// a multidimensional space, having coordinates w.r.t. specific 
    /// bases, forming what are referred to as <i>clouds</i>
    /// (For a definition, see the <see cref="Advanced.Cloud"/> documentation).
    /// The aim of a multiple correspondence analysis is to project those clouds
    /// in a space having a lower dimensionality, in such a way that distances 
    /// in space relate 
    /// to dissimilarities among categories or among individuals.
    /// </para>
    /// <para>
    /// Let <latex>Y</latex> be a <latex>J \times Q</latex> categorical 
    /// data set, where individuals are
    /// assigned to rows and variables to columns. Let <latex>V_q</latex> be, 
    /// for <latex>q= 1,\dots, Q</latex>,
    /// the domain of the <latex>q</latex>-th categorical variable, 
    /// i.e. the set of its observed
    /// categories in <latex>Y</latex>. Given the definition
    /// <latex mode="display">
    /// K = \sum_{ q = 1}^Q \left| V_q \right|,    
    /// </latex>
    /// one can map the overall observed categories to
    /// indexes <latex>1,\dots,K</latex>, so that
    /// a disjunctive form of <latex>Y</latex> can be represented as 
    /// a <latex>J \times K</latex> matrix, say <latex>N</latex>,
    /// such that, for <latex>j=1,\dots,J</latex> and
    /// <latex>k=1,\dots,K</latex>, 
    /// <latex>N\round{j,k}=1</latex> 
    /// if and only if the <latex>k</latex>-th category has been observed 
    /// at the <latex>j</latex>-th individual,
    /// <latex>0</latex> otherwise.
    /// The number of individuals at which the <latex>k</latex>-th category 
    /// has been observed
    /// is thus given as <latex>n_k = \sum_{j=1}^J N_ { j,k }</latex>.
    /// </para>
    /// <para>
    /// Notice that matrix <latex>N</latex> is a contingency table, so that
    /// the cloud of individuals and that of categories can be interpreted, 
    /// respectively, as the clouds of row and column profiles 
    /// taken into account while analyzing a correspondence. 
    /// That is, a multiple correspondence
    /// analysis of <latex>Y</latex> consists precisely in the 
    /// correspondence analysis
    /// of <latex>N</latex>. As a consequence, in what follows is 
    /// exploited the same notation used in the documentation
    /// of class <see cref="Correspondence"/>.
    /// </para>
    /// <para><b>Cloud of individuals</b></para>
    /// <para>
    /// Individuals are <latex>J</latex> points in <latex>\R^{K}</latex> that 
    /// correspond to the row profiles
    /// based on matrix <latex>N</latex>. Their cloud can thus be obtained by 
    /// specializing the cloud 
    /// <latex>\C_{\Rows,\A_\Rows} 
    /// = \round{\A_\Rows,X_{\Rows,\A_\Rows},w_{\Rows}}</latex>, as 
    /// defined in the remarks about class <see cref="Correspondence"/>.
    /// </para>
    /// <para>
    /// More thoroughly, the marginal relative frequency of the <latex>k</latex>-th category
    /// is equal to <latex>n_k / \round{Q\,J}</latex>, hence
    /// <latex mode="display">
    ///     f_\Cols = \round{Q\,J}^{-1} \mx{
    ///         n_1\\
    ///         \vdots\\
    ///         n_K
    ///     },
    /// </latex>
    /// so that basis <latex>\basis{A}_\Rows</latex> is representable
    /// through the matrix
    /// <latex mode="display">
    ///     A_\Rows 
    ///     &amp;= 
    ///     F_\Cols^{-1/2}\\
    ///     &amp;=
    ///     \sqrt{Q\,J}\,
    ///     \mx{
    ///         n_1       &amp; 0       &amp; \cdots &amp; 0\\ 
    ///         0         &amp; n_2     &amp; \cdots &amp; 0\\
    ///         \vdots    &amp; \vdots  &amp; \ddots &amp; \vdots \\
    ///         0         &amp; 0       &amp; \cdots &amp; n_{K}
    ///     }^{-1/2}.
    /// </latex>
    /// Furthermore, since <latex>Q</latex> variables are observed at each individual,
    /// one also has, for <latex>j=1,\dots,J</latex>,
    /// <latex mode="display">
    ///     f_{\Rows,j} = \frac{Q}{Q\,J} = \frac{1}{J}
    /// </latex> 
    /// so that
    /// <latex mode="display">
    ///     w_\Rows 
    ///     =
    ///      f_\Rows
    ///     = 1_J\,/\,J
    /// </latex>
    /// and
    /// <latex mode="display">
    ///     X_{\Rows,\A_\Rows} 
    ///     =
    ///      \Inv{F_\Rows}\,F
    ///     = N\,/\,Q,
    /// </latex>
    /// where <latex>F=N/\round{Q\,J}</latex>.
    /// </para>
    /// <para><b>Cloud of categories</b></para>
    /// <para>
    /// Categories are the <latex>K</latex> column profiles 
    /// in <latex>\R^J</latex> represented 
    /// by the cloud
    /// <latex>\C_{\Cols,\A_\Cols} 
    /// = \round{\A_\Cols,X_{\Cols,\A_\Cols},w_{\Cols}}</latex>,
    /// as discussed in the documentation of <see cref="Correspondence"/>.
    /// </para>
    /// <para>
    /// In the current context, such cloud can be expressed as follows.
    /// </para>
    /// <para>
    /// Basis <latex>\basis{A}_\Cols</latex> has a representation matrix 
    /// evaluating to
    /// <latex mode="display">
    ///     A_\Cols 
    ///     =
    ///      F_\Rows^{-1/2}
    ///     = \sqrt{J}\,I_J
    /// </latex>
    /// while the point coordinates and weights are given by, respectively,  
    /// <latex mode="display">
    ///      X_{\Cols,\A_\Cols} 
    ///     =
    ///      \Inv{F_\Cols}\,\T{F}
    ///     =    
    ///     \Inv{\mx{
    ///         n_1     &amp; 0       &amp; \cdots &amp; 0\\ 
    ///         0       &amp; n_2     &amp; \cdots &amp; 0\\
    ///         \vdots  &amp; \vdots  &amp; \ddots &amp; \vdots \\
    ///         0       &amp; 0       &amp; \cdots &amp; n_{K}
    ///     }}\,\T{N},
    /// </latex>
    /// and, for <latex>k=1,\dots,K</latex>, 
    /// <latex mode="display">
    ///      w_{\Cols,k} 
    ///     =
    ///      f_{\Cols,k}
    ///     = n_k\,/\,\round{Q\,J}.
    /// </latex>
    /// </para>
    /// <para><b>Principal projections</b></para>
    /// <para>
    /// Information about the principal projections of categories and individuals
    /// are exposed through 
    /// properties <see cref="Categories"/> and 
    /// <see cref="Individuals"/>, respectively. 
    /// These properties return objects of class
    /// <see cref="PrincipalProjections"/>. Check its documentation  
    /// for a thorough explanation
    /// of the underlying statistical methods, and for a discussion 
    /// about how to exploit the principal information of the clouds, 
    /// or to locate supplementary points.
    /// </para>
    /// </remarks>
    /// <seealso href="https://en.wikipedia.org/wiki/Multiple_correspondence_analysis"/>
    public class MultipleCorrespondence
    {
        #region State

        private Correspondence correspondence;

        /// <summary>
        /// Gets the principal projections of the cloud of individuals.
        /// </summary>
        /// <value>The principal projections of the cloud of individuals.</value>
        public PrincipalProjections Individuals {
            get { return this.correspondence.RowProfiles; }
        }

        /// <summary>
        /// Gets the principal projections of the cloud of categories.
        /// </summary>
        /// <value>The principal projections of the cloud of categories.</value>
        public PrincipalProjections Categories {
            get { return this.correspondence.ColumnProfiles; } }

        #endregion

        #region Constructors and factory methods

        private MultipleCorrespondence()
        {
        }

        /// <summary>
        /// Analyzes the multiple correspondence of the specified 
        /// categorical data set.
        /// </summary>
        /// <param name="dataSet">The data set to analyze.</param>
        /// <returns>The multiple correspondence of the specified data set.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dataSet"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The disjoint form of parameter <paramref name="dataSet"/> has at least a non 
        /// positive marginal sum. 
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The Singular Value Decomposition needed to acquire 
        /// the correspondence cannot be executed or does not converge.<br/>
        /// -or-<br/>
        /// No principal variable has positive variance. 
        /// The principal information cannot be acquired.
        /// </exception>
        public static MultipleCorrespondence Analyze(
            CategoricalDataSet dataSet)
        {
            if (dataSet is null)
                throw new ArgumentNullException(nameof(dataSet));

            var disjunctiveProtocol = dataSet.Disjoin();

            Correspondence correspondence;

            try
            {
                correspondence = Correspondence.Analyze(
                    disjunctiveProtocol);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException(nameof(dataSet),
                   ImplementationServices.GetResourceString(
                   "STR_EXCEPT_GDA_MCA_NON_POSITIVE_MARGINAL_SUMS"));
            }
            catch (Exception)
            {
                throw;
            }

            var multipleCorrespondence = new MultipleCorrespondence
            {
                correspondence = correspondence
            };

            return multipleCorrespondence;
        }

        #endregion
    }
}
