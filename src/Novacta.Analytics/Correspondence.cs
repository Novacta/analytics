// Copyright(c) Giovanni Lafratta.All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents the results of a correspondence analysis applied to 
    /// a data table having positive marginal sums.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A <see cref="Correspondence"/> usually examines the relationships 
    /// existing between two categorical variables whose observations 
    /// have been summarized in a contingency table, 
    /// i.e. a table showing the joint frequency distribution of the variables,
    /// in which the rows are assigned to the categories of a first variable,
    /// and the columns to those of another one.
    /// However, the current implementation of <see cref="Correspondence"/> does
    /// not control that the analyzed data strictly represent counts: what 
    /// is checked is only that the marginal data sums, i.e. the sums computed 
    /// separately on each row and on each column, are all positive values.
    /// </para>
    /// <para>
    /// Both rows and columns 
    /// are represented as weighted points in a multidimensional space, 
    /// forming what are
    /// referred to as the clouds of <it>row</it> or 
    /// <it>column</it> profiles (See the <see cref="Advanced.Cloud"/> documentation
    /// for a formal definition of the cloud concept). 
    /// The aim of a correspondence analysis is to
    /// project such clouds in a space having a lower dimensionality, so
    /// that distances in such space relate to dissimilarities among the categories
    /// of the two variables.
    /// Check the <see cref="PrincipalProjections"/> documentation  
    /// for a thorough explanation
    /// of the statistical methods underlying such projections.
    /// </para>
    /// <note>
    /// DIrections along which to project the points are added 
    /// until the corresponding projected variance is greater than 1e-6. 
    /// </note>
    /// <para><b>Contingency tables</b></para>
    /// <para>
    /// Let <latex>N</latex> be a <latex>J \times K</latex> contingency table.
    /// The number of overall observations
    /// is
    /// <latex mode="display">
    /// n = \T{1_J}\,N\,1_K,
    /// </latex>
    /// where <latex>1_J</latex> and <latex>1_K</latex> are column vectors 
    /// having lengths <latex>J</latex> and <latex>K</latex>, respectively,
    /// so that the relative joint frequency distribution can be represented by the
    /// matrix
    /// <latex mode="display">
    ///     F = N\,/\,n.
    /// </latex>
    /// The relative marginal frequency distribution of the row variable is
    /// <latex mode="display">\label{eq:f_R}
    ///     f_\Rows = F\,1_K,
    /// </latex>
    /// and that of the column one is
    /// <latex mode="display">\label{eq:f_C}
    ///     f_\Cols = \T{F}\,1_J.
    /// </latex>
    /// Given vectors <latex>f_\Rows</latex> and <latex>f_\Cols</latex>, 
    /// one can also define the following diagonal matrices:
    /// <latex mode="display">
    ///     F_\Rows = \diag{f_\Rows} \equiv \mx{
    ///         f_{\Rows,1}  &amp; 0              &amp; \cdots &amp; 0\\ 
    ///         0               &amp; f_{\Rows,2} &amp; \cdots &amp; 0\\
    ///         \vdots          &amp; \vdots         &amp; \ddots &amp; \vdots \\
    ///         0               &amp; 0              &amp; \cdots &amp; f_{\Rows,J}
    ///     },
    /// </latex>
    /// and 
    /// <latex mode="display">
    ///     F_\Cols = \diag{f_\Cols}.
    /// </latex>
    /// </para>
    /// <para><b>Cloud of row profiles</b></para>
    /// <para>
    /// Row profiles are <latex>J</latex> points in <latex>\R^K</latex>, 
    /// say <latex>x_{\Rows,1},\dots,x_{\Rows,J}</latex>.
    /// The points, if measured w.r.t. the basis <latex>\basis{A}_\Rows</latex>, with
    /// <latex mode="display">\label{eq:CA_Cloud_of_Rows_Basis}
    ///     A_\Rows = F_\Cols^{-1/2}, 
    /// </latex>
    /// have coordinates
    /// <latex mode="display">\label{eq:CA_Cloud_of_Rows_Coords}
    ///     X_{\Rows,\A_\Rows} 
    ///     \equiv \mx{
    ///     \T{x_{\Rows,1,{\basis{A}_\Rows}}} \\ \vdots \\ \T{x_{\Rows,J,{\basis{A}_\Rows}}}
    ///     }
    ///     = \Inv{F_\Rows}\,F,
    /// </latex>
    /// and weights
    /// <latex mode="display">\label{eq:CA_Cloud_of_Rows_Weights}
    ///     w_\Rows = f_\Rows,
    /// </latex>
    /// forming the cloud 
    /// <latex mode="display">\label{eq:CA_Cloud_of_Rows}
    ///     \C_{\Rows,\A_\Rows} = \round{\A_\Rows,X_{\Rows,\A_\Rows},w_{\Rows}}.
    /// </latex>
    /// Such cloud and its projections can be inspected via 
    /// property <see cref="RowProfiles"/>.
    /// </para>
    /// <para><b>Cloud of column profiles</b></para>
    /// <para>
    /// Column profiles are <latex>K</latex> points in <latex>\R^J</latex>, 
    /// say <latex>x_{\Cols,1},\dots,x_{\Cols,K}</latex>.
    /// Such points are measured w.r.t. the 
    /// basis <latex>\basis{A}_\Cols</latex>, 
    /// with
    /// <latex mode="display">\label{eq:CA_Cloud_of_Cols_Basis}
    ///     A_\Cols = F_\Rows^{-1/2}, 
    /// </latex>
    /// having coordinates
    /// <latex mode="display">\label{eq:CA_Cloud_of_Cols_Coords}
    ///     X_{\Cols,\A_\Cols} 
    ///     \equiv \mx{
    ///     \T{x_{\Cols,1,{\basis{A}_\Cols}}} \\ \vdots \\ \T{x_{\Cols,K,{\basis{A}_\Cols}}}
    ///     }
    ///     = \Inv{F_\Cols}\,\T{F},
    /// </latex>
    /// and weights
    /// <latex mode="display">\label{eq:CA_Cloud_of_Cols_Weights}
    ///     w_\Cols = f_\Cols,
    /// </latex>
    /// forming the cloud 
    /// <latex mode="display">\label{eq:CA_Cloud_of_Cols}
    ///     \C_{\Cols,\A_\Cols} = \round{\A_\Cols,X_{\Cols,\A_\Cols},w_{\Cols}}.
    /// </latex>
    /// Such cloud and its projections can be inspected via 
    /// property <see cref="ColumnProfiles"/>.
    /// </para>
    /// </remarks>
    /// <seealso cref="PrincipalProjections"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Correspondence_analysis"/>
    public class Correspondence
    {
        /// <summary>
        /// Analyzes the correspondence of the specified data.
        /// </summary>
        /// <param name="data">The data to analyze.</param>
        /// <returns>
        /// The correspondence of the specified data.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Parameter <paramref name="data"/> has at least a non 
        /// positive marginal sum. 
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The Singular Value Decomposition needed to acquire 
        /// the correspondence cannot be executed or does not converge.<br/>
        /// -or-<br/>
        /// No principal variable has positive variance. 
        /// The principal information cannot be acquired.
        /// </exception>
        public static Correspondence Analyze(DoubleMatrix data)
        {
            ArgumentNullException.ThrowIfNull(data);

            PrincipalProjections.FromReciprocalAveraging(
                data: data,
                rowPrincipalProjections:
                    out PrincipalProjections rowPrincipalProjections,
                columnPrincipalProjections:
                    out PrincipalProjections columnPrincipalProjections);

            var correspondence = new Correspondence
            {
                RowProfiles = rowPrincipalProjections,
                ColumnProfiles = columnPrincipalProjections
            };

            return correspondence;
        }

        /// <summary>
        /// Gets the principal projections of the row profiles.
        /// </summary>
        /// <value>The principal projections of the row profiles.</value>
        public PrincipalProjections RowProfiles { get; private set; }

        /// <summary>
        /// Gets the principal projections of the column profiles.
        /// </summary>
        /// <value>The principal projections of the column profiles.</value>
        public PrincipalProjections ColumnProfiles { get; private set; }
    }
}
