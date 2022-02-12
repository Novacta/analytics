// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

using Novacta.Analytics.Infrastructure;
using Novacta.Analytics.Advanced;
using System.Globalization;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents the principal components of a data matrix.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Rows of the data matrix correspond to individuals, columns to variables.
    /// Each row is interpreted as the coordinates of a multidimensional
    /// point with respect to a given basis, where the dimension is the number 
    /// of possibly correlated variables 
    /// observed for each individual.
    /// The analysis of the principal components aims to project the given 
    /// cloud of points in
    /// a space having a reduced dimension, in which the new coordinates form 
    /// uncorrelated variables which are selected by maximizing their variances, 
    /// so that the projection retains as much of the variability
    /// of the initial variables as possible.
    /// </para>
    /// <note>
    /// Components are added 
    /// until the corresponding projected variance is greater than 1e-6. 
    /// </note>
    /// <para>
    /// Class <see cref="PrincipalComponents"/> inherits from class
    /// <see cref="PrincipalProjections"/>. Check its documentation  
    /// for a thorough explanation
    /// of the statistical methods underlying a Principal Component 
    /// analysis.
    /// </para>
    /// <para><b>Instantiation</b></para>
    /// <para>
    /// New instances of class <see cref="PrincipalComponents"/> can be 
    /// initialized by calling one of the overloaded methods 
    /// <see cref="Analyze(DoubleMatrix)"/>
    /// or one of its overloaded versions.
    /// The simple overload <see cref="Analyze(DoubleMatrix)">
    /// Analyze</see> takes a data matrix as the coordinates of the cloud
    /// with respect to the <see cref="Basis.Standard">
    /// standard</see> basis, and assigns uniform weights to each individual.
    /// Nonuniform weights can be specified through the overload
    /// <see cref="Analyze(DoubleMatrix, DoubleMatrix)">
    /// Analyze</see>, which again refers the coordinates to the standard 
    /// basis. 
    /// </para>
    /// <para>
    /// The more complex overload <see cref="Analyze(
    /// DoubleMatrix, DoubleMatrix, DoubleMatrix)">Analyze</see> takes the 
    /// coordinates matrix, the individual weights, and a sequence of
    /// coefficients assigned to variables. Such variable coefficients
    /// are used to refer the coordinates to a basis other than 
    /// the standard one.
    /// More thoroughly, given the sequence of coefficients
    /// <latex>\nu_1,\dots,\nu_K</latex>, then 
    /// the <see cref="Basis"/> is chosen so that
    /// its matrix is as follows:
    /// <latex mode="display">
    /// A = \mx{
    ///   \sqrt{\nu_{1}}  &amp; 0              &amp; \cdots &amp; 0\\ 
    ///   0               &amp; \sqrt{\nu_{2}} &amp; \cdots &amp; 0\\
    ///   \vdots          &amp; \vdots         &amp; \ddots &amp; \vdots \\
    ///   0               &amp; 0              &amp; \cdots &amp; \sqrt{\nu_{K}}
    /// }.  
    /// </latex>
    /// </para>
    /// </remarks>
    /// <seealso cref="PrincipalProjections"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Principal_component_analysis"/>
    public class PrincipalComponents : PrincipalProjections
    {
        #region Constructors and factory methods

        /// <summary>
        /// Prevents a default instance of the <see cref="PrincipalComponents"/> 
        /// class from being created.
        /// </summary>
        private PrincipalComponents(Cloud cloud) : base(cloud)
        {
        }

        private static PrincipalComponents InternalAnalyze(
            DoubleMatrix data,
            DoubleMatrix individualWeights, 
            DoubleMatrix variableCoefficients)
        {
            var basisMatrix = DoubleMatrix.Diagonal(
                variableCoefficients.Apply((x) => Math.Sqrt(x)));

            var cloud = new Cloud(data, individualWeights, new Basis(basisMatrix));

            var principalComponents = new PrincipalComponents(cloud);

            return principalComponents;
        }

        /// <summary>
        /// Validates the individual weights.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="individualWeights">The individual weights.</param>
        private static void ValidateIndividualWeights(
            DoubleMatrix data,
            DoubleMatrix individualWeights)
        {
            #region Input validation

            if (individualWeights is null)
            {
                throw new ArgumentNullException(nameof(individualWeights));
            }

            if (!individualWeights.IsColumnVector)
            {
                throw new ArgumentOutOfRangeException(nameof(individualWeights),
                   ImplementationServices.GetResourceString(
                   "STR_EXCEPT_PAR_MUST_BE_COLUMN_VECTOR"));
            }

            if (data.NumberOfRows != individualWeights.NumberOfRows)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(individualWeights),
                    string.Format(CultureInfo.InvariantCulture,
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_ROWS"),
                        nameof(data)));
            }

            double sum = 0.0, weight;
            for (int i = 0; i < individualWeights.Count; i++)
            {
                weight = individualWeights[i];
                if (weight < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(individualWeights),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_ENTRIES_MUST_BE_NON_NEGATIVE"));
                }
                sum += weight;
            }

            if (Math.Abs(sum - 1.0) > 1.0e-3)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(individualWeights),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_ENTRIES_MUST_SUM_TO_1"));
            }

            #endregion
        }

        /// <summary>
        /// Validates the variable coefficients.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="variableCoefficients">The variable coefficients.</param>
        private static void ValidateVariableCoefficients(
            DoubleMatrix data,
            DoubleMatrix variableCoefficients)
        {
            #region Input validation

            if (variableCoefficients is null)
            {
                throw new ArgumentNullException(nameof(variableCoefficients));
            }

            if (!variableCoefficients.IsRowVector)
            {
                throw new ArgumentOutOfRangeException(nameof(variableCoefficients),
                   ImplementationServices.GetResourceString(
                   "STR_EXCEPT_PAR_MUST_BE_ROW_VECTOR"));
            }

            if (data.NumberOfColumns != variableCoefficients.NumberOfColumns)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(variableCoefficients),
                    string.Format(CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_COLUMNS"),
                        nameof(data)));
            }

            for (int i = 0; i < variableCoefficients.Count; i++)
            {
                if (variableCoefficients[i] <= 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(variableCoefficients),
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_ENTRIES_MUST_BE_POSITIVE"));
                }
            }

            #endregion
        }


        /// <summary>
        /// Analyzes the principal components of data
        /// in which individuals and variables have been assigned
        /// the specified weights and coefficients, respectively.
        /// </summary>
        /// <remarks>
        /// Rows of the data matrix correspond to individuals, columns to variables. 
        /// Each row is interpreted as the coordinates of a multidimensional point 
        /// with respect to a <see cref="Basis">basis</see> whose vectors are defined as follows:
        /// the square root of the weight of the <i>j</i>-th variable is multiplied 
        /// by the <i>j</i>-th
        /// unit vector of the same dimension, and the result is included as 
        /// a basis vector.
        /// </remarks>
        /// <param name="data">The data to analyze.</param>
        /// <param name="individualWeights">The individual weights.</param>
        /// <param name="variableCoefficients">The variable coefficients.</param>
        /// <returns>
        /// The Principal Components of the specified data.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="individualWeights"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="variableCoefficients"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="individualWeights"/> is not a column vector.<br/>
        /// -or-<br/> 
        /// <paramref name="variableCoefficients"/> is not a row vector.<br/>
        /// -or-<br/>
        /// The <see cref="DoubleMatrix.
        /// Count"/> of <paramref name="individualWeights"/> is not equal 
        /// to the number of rows 
        /// of <paramref name="data"/>.<br/>
        /// -or-<br/>
        /// The <see cref="DoubleMatrix.Count"/> of 
        /// <paramref name="variableCoefficients"/> is not equal 
        /// to the number of columns 
        /// of <paramref name="data"/>.<br/>
        /// -or-<br/> 
        /// <paramref name="individualWeights"/> entries do not sum up to 1.<br/>
        /// -or-<br/> 
        /// Any entry of <paramref name="individualWeights"/> is negative.<br/>
        /// -or-<br/>
        /// Any entry of <paramref name="variableCoefficients"/> is not positive.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The Singular Value Decomposition needed to acquire 
        /// the principal components cannot be executed or does not converge.<br/>
        /// -or-<br/>
        /// No principal component has positive variance. 
        /// The principal information cannot be acquired.
        /// </exception>
        public static PrincipalComponents Analyze(DoubleMatrix data,
            DoubleMatrix individualWeights, DoubleMatrix variableCoefficients)
        {
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            PrincipalComponents.ValidateIndividualWeights(data, individualWeights);
            PrincipalComponents.ValidateVariableCoefficients(data, variableCoefficients);

            return InternalAnalyze(data, individualWeights, variableCoefficients);
        }

        /// <summary>
        /// Analyzes the principal components of data
        /// in which individuals have received the specified weights.
        /// </summary>
        /// <remarks>
        /// Rows of the data matrix correspond to individuals, columns to variables. 
        /// Each row is interpreted as the coordinates of a multidimensional point 
        /// with respect to the <see cref="Basis.Standard">standard</see> basis.
        /// </remarks>
        /// <param name="data">The data to analyze.</param>
        /// <param name="individualWeights">The individual weights.</param>
        /// <returns>The Principal Components of the specified data.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="individualWeights"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="individualWeights"/> is not a column vector.<br/>
        /// -or-<br/>
        /// The <see cref="DoubleMatrix.
        /// Count"/> of <paramref name="individualWeights"/> is not equal 
        /// to the number of rows 
        /// of <paramref name="data"/>.<br/>
        /// -or-<br/> 
        /// <paramref name="individualWeights"/> entries do not sum up to 1.<br/>
        /// -or-<br/> 
        /// Any entry of <paramref name="individualWeights"/> is negative.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The Singular Value Decomposition needed to acquire 
        /// the principal components cannot be executed or does not converge.<br/>
        /// -or-<br/>
        /// No principal component has positive variance. 
        /// The principal information cannot be acquired.
        /// </exception>
        public static PrincipalComponents Analyze(
            DoubleMatrix data,
            DoubleMatrix individualWeights)
        {
            #region Input validation

            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            PrincipalComponents.ValidateIndividualWeights(data, individualWeights);

            #endregion

            DoubleMatrix variableCoefficients =
                DoubleMatrix.Dense(1, data.NumberOfColumns, 1.0);

            return InternalAnalyze(data, individualWeights, variableCoefficients);
        }

        /// <summary>
        /// Analyzes the principal components of the specified data.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Rows of the data matrix correspond to individuals, columns to variables. 
        /// Each row is interpreted as the coordinates of a multidimensional point 
        /// with respect to the <see cref="Basis.Standard">standard</see> basis.
        /// Uniform weights are assigned to each individual. 
        /// </para>
        /// </remarks>
        /// <param name="data">The data to analyze.</param>
        /// <returns>The Principal Components of the specified data.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The Singular Value Decomposition needed to acquire 
        /// the principal components cannot be executed or does not converge.<br/>
        /// -or-<br/>
        /// No principal component has positive variance. 
        /// The principal information cannot be acquired.
        /// </exception>
        public static PrincipalComponents Analyze(DoubleMatrix data)
        {
            if (data is null)
                throw new ArgumentNullException(nameof(data));

            int numberOfIndividuals = data.NumberOfRows;
            int numberOfVariables = data.NumberOfColumns;

            var individualWeights = DoubleMatrix.Dense(numberOfIndividuals, 1,
                1.0 / numberOfIndividuals);
            var variableCoefficients = DoubleMatrix.Dense(numberOfVariables, 1, 1.0);

            return InternalAnalyze(data, individualWeights, variableCoefficients);
        }

        #endregion
    }
}
