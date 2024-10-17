// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;
using Novacta.Analytics.Infrastructure;
using System;
using System.Globalization;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents a classical multidimensional scaling analysis.
    /// </summary>
    /// <remarks>
    /// <para><b>Instantiation</b></para>
    /// <para>
    /// New instances of class <see cref="ClassicalMultidimensionalScaling"/> can be initialized by 
    /// calling method <see cref="Analyze(DoubleMatrix, int?)"/>, which implements the 
    /// classical multidimensional scaling algorithm.
    /// </para>
    /// <para>
    /// Given a matrix of proximities, say <latex>D</latex>, let
    /// <latex>D^{2}</latex> be the matrix of its squared elements, and 
    /// define the transformed matrix of proximities as 
    /// <latex mode="display">B = - C D^{2} C / 2,</latex>
    /// where <latex>C</latex> is the centering matrix. 
    /// </para>
    /// <para>
    /// Let <latex>m</latex> be the number of positive eigenvalues
    /// of matrix <latex>B</latex>.
    /// If the configuration dimension is not specified to method
    /// <see cref="Analyze(DoubleMatrix, int?)"/>,
    /// it is set equal to <latex>m</latex> if <latex>m \neq 0</latex>;
    /// otherwise, an exception is thrown.
    /// </para>
    /// <para>
    /// If the configuration dimension is specified to method 
    /// <see cref="Analyze(DoubleMatrix, int?)"/>, then it must be less than or equal to
    /// <latex>m</latex>; otherwise, an exception is thrown.
    /// </para>
    /// <para><b>Results</b></para>
    /// <para>
    /// Let <latex>\lambda_{1}, \lambda_{2}, \ldots, \lambda_{n}</latex> be the
    /// eigenvalues of <latex>B</latex> in decreasing order, and let
    /// <latex>v_{1}, v_{2}, \ldots, v_{n}</latex> be the corresponding eigenvectors.
    /// </para>    
    /// <para>
    /// If the dimension is equal to <latex>k</latex>, then
    /// the configuration is given by matrix:
    /// <latex mode="display">
    /// X =\mx{
    ///     v_1 &amp; \cdots &amp; v_k
    ///     }
    ///      \mx{
    ///         \sqrt{\lambda_{1}}  &amp; 0              &amp; \cdots &amp; 0\\ 
    ///         0               &amp; \sqrt{\lambda_{2}} &amp; \cdots &amp; 0\\
    ///         \vdots          &amp; \vdots         &amp; \ddots &amp; \vdots \\
    ///         0               &amp; 0              &amp; \cdots &amp; \sqrt{\lambda_{k}}
    ///     },        
    /// </latex>
    /// which can be inspected through property <see cref="Configuration"/>.
    /// </para>
    /// <para><b>Goodness of fit</b></para>
    /// <para>
    /// The goodness of fit of such configuration is given by the formula
    /// <latex mode="display">
    /// \frac{\sum_{i=1}^{k} \lvert\lambda_{i}\rvert}{\sum_{i=1}^{n} \lvert\lambda_{i}\rvert},
    /// </latex>
    /// and is accessible through property <see cref="GoodnessOfFit"/>.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// In the following example, a classical multidimensional scaling analysis is performed on 
    /// a matrix of proximities.
    /// </para>
    /// <para>
    /// <code source="..\Novacta.Analytics.CodeExamples\ClassicalMultidimensionalScalingExample0.cs.txt" 
    /// language="cs" />
    /// </para>
    /// </example>
    /// <seealso cref="NonMetricMultidimensionalScaling"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Multidimensional_scaling#Classical_multidimensional_scaling"/>
    public class ClassicalMultidimensionalScaling
    {
        #region State

        /// <summary>
        /// Gets the configuration of points in the target space.
        /// </summary>
        /// <value>
        /// The configuration of points in the target space.
        /// </value>
        public DoubleMatrix Configuration { get; init; }

        /// <summary>
        /// Gets the goodness of fit at the <see cref="Configuration"/>.
        /// </summary>
        /// <value>
        /// The goodness of fit at the <see cref="Configuration"/>.
        /// </value>
        public double GoodnessOfFit { get; init; }

        #endregion

        #region Constructors and factory methods

        internal ClassicalMultidimensionalScaling(
            DoubleMatrix configuration,
            double goodnessOfFit)
        {
            this.Configuration = configuration;
            this.GoodnessOfFit = goodnessOfFit;
        }

        /// <summary>
        /// Executes a metric multidimensional scaling analysis.
        /// </summary>
        /// <param name="proximities">
        /// The matrix of proximities.
        /// </param>
        /// <param name="configurationDimension">
        /// The dimension of the configuration of points in the target space.
        /// Defaults to <b>null</b>, meaning that the dimension of the configuration
        /// is automatically selected.
        /// </param>
        /// <returns>
        /// A <see cref="ClassicalMultidimensionalScaling"/> instance representing the results
        /// of the specified analysis.
        /// </returns>
        /// <remarks>
        /// <para>
        /// 
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="proximities"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="proximities"/> is not symmetric<br/>
        /// -or-<br/>
        /// The transformed <paramref name="proximities"/> matrix
        /// has no positive eigenvalues (see <see cref="ClassicalMultidimensionalScaling"/> remarks).
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="configurationDimension"/> is less than 1<br/>
        /// -or-<br/>
        /// <paramref name="configurationDimension"/> is greater than the 
        /// number of rows in <paramref name="proximities"/><br/>
        /// -or-<br/>
        /// <paramref name="configurationDimension"/> is greater than the number of 
        /// positive eigenvalues of the transformed <paramref name="proximities"/> matrix 
        /// (see <see cref="ClassicalMultidimensionalScaling"/> remarks).
        /// </exception>
        /// <seealso cref="ClassicalMultidimensionalScaling"/>
        public static ClassicalMultidimensionalScaling Analyze(
            DoubleMatrix proximities,
            int? configurationDimension = null)
        {
            #region Input validation

            ArgumentNullException.ThrowIfNull(proximities);

            if (!proximities.IsSymmetric)
            {
                throw new ArgumentException(
                    message: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_MUST_BE_SYMMETRIC"),
                    paramName: nameof(proximities));
            }

            if (configurationDimension < 1)
            {
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(configurationDimension),
                    message: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            if (configurationDimension > proximities.NumberOfRows)
            {
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(configurationDimension),
                    message:
                        string.Format(
                            CultureInfo.InvariantCulture,
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_MUST_BE_NOT_GREATER_THAN_OTHER_ROWS"),
                            nameof(configurationDimension),
                            nameof(proximities)));
            }

            #endregion

            int n = proximities.NumberOfRows;
            var d2 = proximities.Apply((v) => Math.Pow(v, 2));

            var c =
                DoubleMatrix.Identity(n)
                -
                DoubleMatrix.Dense(n, n, 1.0 / n);

            var b = -0.5 * c * d2 * c;

            var eigenvalues = SpectralDecomposition.Decompose(
                matrix: b,
                lowerTriangularPart: true,
                out DoubleMatrix eigenvectors);

            int effectiveConfigurationDimension;
            int maximalConfigurationDimension = 0;
            for (int i = n - 1; i > -1; i--)
            {
                if (eigenvalues[i, i] > 0.0)
                {
                    maximalConfigurationDimension++;
                }
                else
                {
                    break;
                }
            }

            if (configurationDimension == null)
            {
                if (maximalConfigurationDimension == 0)
                {
                    throw new ArgumentException(
                        paramName: nameof(proximities),
                        message: ImplementationServices.GetResourceString(
                            "STR_EXCEPT_CMDS_PROXIMITIES_CANNOT_BE_SCALED"));
                }
                effectiveConfigurationDimension = maximalConfigurationDimension;
            }
            else
            {
                int configurationDimensionValue = configurationDimension.Value;

                if (configurationDimensionValue > maximalConfigurationDimension)
                {
                    throw new ArgumentOutOfRangeException(
                        paramName: nameof(configurationDimension),
                        message:
                            string.Format(
                                CultureInfo.InvariantCulture,
                                ImplementationServices.GetResourceString(
                                    "STR_EXCEPT_CMDS_UNALLOWED_CONFIGURATION_DIMENSION"),
                                nameof(configurationDimension),
                                maximalConfigurationDimension));
                }
                effectiveConfigurationDimension = configurationDimensionValue;
            }

            var relevantValues = DoubleMatrix.Sparse(
                numberOfRows: effectiveConfigurationDimension,
                numberOfColumns: effectiveConfigurationDimension,
                capacity: effectiveConfigurationDimension);

            var relevantVectors =
                eigenvectors[
                    rowIndexes: ":",
                    columnIndexes:
                        IndexCollection.Sequence(
                            firstIndex: n - 1,
                            increment: -1,
                            indexBound: n - effectiveConfigurationDimension)];

            for (int i = 0; i < effectiveConfigurationDimension; i++)
            {
                int j = n - i - 1;
                relevantValues[i, i] = Math.Sqrt(eigenvalues[j, j]);
            }

            var configuration = relevantVectors * relevantValues;

            if (proximities.HasRowNames)
            {
                for (int i = 0; i < n; i++)
                {
                    if (proximities.TryGetRowName(i, out string rowName))
                    {
                        configuration.SetRowName(i, rowName);
                    }
                }
            }

            double goodnessOfFitNumerator = 0.0;
            double goodnessOfFitDenominator = 0.0;

            for (int i = n - 1; i > -1; i--)
            {
                double absoluteEigenvalue = Math.Abs(eigenvalues[i, i]);

                goodnessOfFitDenominator += absoluteEigenvalue;

                if (i > n - effectiveConfigurationDimension - 1)
                {
                    goodnessOfFitNumerator += absoluteEigenvalue;
                }
            }

            double goodnessOfFit = goodnessOfFitNumerator / goodnessOfFitDenominator;

            var results = new ClassicalMultidimensionalScaling(
                configuration: configuration,
                goodnessOfFit: goodnessOfFit);

            return results;
        }

        #endregion
    }
}
