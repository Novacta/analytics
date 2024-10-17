// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Advanced;
using Novacta.Analytics.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using static System.Math;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents a nonmetric multidimensional scaling analysis.
    /// </summary>
    /// <remarks>
    /// <para><b>Instantiation</b></para>
    /// <para>
    /// New instances of class <see cref="NonMetricMultidimensionalScaling"/> can be 
    /// initialized by calling method 
    /// <see cref="Analyze(DoubleMatrix, int?, double, int, double)"/>, 
    /// which implements the Kruskal's nonmetric multidimensional scaling algorithm
    /// (Kruskal, 1964<cite>kruskal-b-1964</cite>).
    /// </para>
    /// <para><b>Results</b></para>
    /// <para>
    /// The optimal configuration reached via a nonmetric multidimensional scaling analysis 
    /// can be inspected through property <see cref="Configuration"/>.
    /// </para>
    /// <para>
    /// The stress at the optimal configuration is returned by property <see cref="Stress"/>.
    /// </para>
    /// <para>
    /// A boolean value indicating whether the optimization process has converged is 
    /// given via property <see cref="HasConverged"/>.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// In the following example, a nonmetric multidimensional scaling analysis is performed on 
    /// a dataset.
    /// </para>
    /// <para>
    /// <code source="..\Novacta.Analytics.CodeExamples\NonMetricMultidimensionalScalingExample0.cs.txt" 
    /// language="cs" />
    /// </para>
    /// </example>
    /// <seealso href="https://en.wikipedia.org/wiki/Multidimensional_scaling#Non-metric_multidimensional_scaling_(NMDS)"/>
    public class NonMetricMultidimensionalScaling
    {
        #region State

        /// <summary>
        /// Gets the optimal configuration of points of this instance.
        /// </summary>
        /// <value>
        /// The optimal configuration of points.
        /// </value>
        public DoubleMatrix Configuration { get; init; }

        /// <summary>
        /// Gets the stress at the <see cref="Configuration"/> of this instance.
        /// </summary>
        /// <value>
        /// The stress at the <see cref="Configuration"/>.
        /// </value>
        public double Stress { get; init; }

        /// <summary>
        /// Gets a value indicating whether the optimization process has converged.
        /// </summary>
        /// <value>
        /// <c>true</c> if the optimization process has converged; otherwise, <c>false</c>.
        /// </value>
        public bool HasConverged { get; init; }

        #endregion

        #region Constructors and factory methods

        internal NonMetricMultidimensionalScaling(
            DoubleMatrix configuration,
            double stress,
            bool hasConverged)
        {
            this.Configuration = configuration;
            this.Stress = stress;
            this.HasConverged = hasConverged;
        }

        /// <summary>
        /// Executes a nonmetric multidimensional scaling analysis.
        /// </summary>
        /// <param name="dissimilarities">
        /// The matrix of dissimilarities.
        /// </param>
        /// <param name="configurationDimension">
        /// The dimension of the configuration of points in the target space.
        /// Defaults to <b>null</b>, meaning that the dimension of the configuration
        /// will be selected automatically.
        /// </param>
        /// <param name="minkowskiDistanceOrder">
        /// The order of the Minkowski metric. It must be greater than or equal to 1.
        /// Defaults to 2.
        /// </param>
        /// <param name="maximumNumberOfIterations">
        /// The maximum number of iterations. Defaults to 200.
        /// </param>
        /// <param name="terminationTolerance">
        /// The termination tolerance. Defaults to 1e-4.
        /// </param>
        /// <remarks>
        /// <para>
        /// The search for the optimal configuration of points in the target space starts 
        /// with a classical scaling of the dissimilarities. If the initial configuration 
        /// can't be computed, an exception is thrown.
        /// </para>
        /// </remarks>
        /// <returns>
        /// The result of the nonmetric multidimensional scaling analysis.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dissimilarities"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="dissimilarities"/> is not symmetric<br/>
        /// -or-<br/>
        /// <paramref name="dissimilarities"/> can't be scaled to an initial configuration
        /// via a <see cref="ClassicalMultidimensionalScaling"/> analysis.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="configurationDimension"/> is less than 1<br/>
        /// -or-<br/>
        /// <paramref name="configurationDimension"/> is greater than the 
        /// number of rows in <paramref name="dissimilarities"/><br/>
        /// -or-<br/>
        /// <paramref name="configurationDimension"/> is greater than the number of 
        /// positive eigenvalues of the initial scaling matrix.<br/>
        /// -or-<br/>
        /// <paramref name="minkowskiDistanceOrder"/> is less than 1.<br/>
        /// -or-<br/>        
        /// <paramref name="maximumNumberOfIterations"/> is not positive.<br/>
        /// -or-<br/>
        /// <paramref name="terminationTolerance"/> is not positive.
        /// </exception>        
        /// <seealso href="https://en.wikipedia.org/wiki/Multidimensional_scaling#Non-metric_multidimensional_scaling_(NMDS)"/>
        public static NonMetricMultidimensionalScaling Analyze(
            DoubleMatrix dissimilarities,
            int? configurationDimension = null,
            double minkowskiDistanceOrder = 2.0,
            int maximumNumberOfIterations = 200,
            double terminationTolerance = 1e-4)
        {
            #region Input validation

            ArgumentNullException.ThrowIfNull(dissimilarities);

            if (!dissimilarities.IsSymmetric)
            {
                throw new ArgumentException(
                    message: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_MUST_BE_SYMMETRIC"),
                    paramName: nameof(dissimilarities));
            }

            if (configurationDimension < 1)
            {
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(configurationDimension),
                    message: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            if (configurationDimension > dissimilarities.NumberOfRows)
            {
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(configurationDimension),
                    message:
                        string.Format(
                            CultureInfo.InvariantCulture,
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_MUST_BE_NOT_GREATER_THAN_OTHER_ROWS"),
                            nameof(configurationDimension),
                            nameof(dissimilarities)));
            }

            if (minkowskiDistanceOrder < 1.0)
            {
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(minkowskiDistanceOrder),
                    message:
                        string.Format(
                            CultureInfo.InvariantCulture,
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_MUST_BE_NOT_LESS_THAN_VALUE"),
                            1.0));
            }

            if (maximumNumberOfIterations < 1)
            {
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(maximumNumberOfIterations),
                    message: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            if (terminationTolerance <= 0.0)
            {
                throw new ArgumentOutOfRangeException(
                    paramName: nameof(terminationTolerance),
                    message: ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            #endregion

            #region Initial Stress function argument

            // Get the initial transposed configuration of points in the target space 
            // by performing a metric scaling on the dissimilarities.

            /*
               A Stress argument represents a configuration of points, say X, as follows.

               Let <latex>m</latex> be the number of points,
               and let <latex>x_i</latex>, for <latex>i = 0,\ldots,m-1</latex>, the 
               <latex>i</latex>-th configuration row, having length <latex>n</latex>. 
               Then the argument is expected to be a row matrix 
               given by the concatenation of the rows of the configuration matrix, say
               obtaining <latex>\mx{x_0,\ldots,x_{m-1}}</latex>.

               Notice that the argument is thus a RowMajor representation of X,
               whose size is m x n.
               
               Furthermore, let us define such matrix as the triple(n, m, row(X)).
               We know that, given the matrix representations (m, n, row(X)) and
               (n, m, col(X')), then - as vectors -, row(X) is equivalent to col(X').
               Thus an argument can be interpreted as a transposed configuration of 
               points in the target space.

               For example, let us consider matrix(3, 2, row(1, 2, 3, 4, 5, 6)), i.e.

               X = [ 1 2
                     3 4
                     5 6 ]
            
               Then X' is [ 1 3 5
                            2 4 6 ]

               which is ColMajor represented as (2, 3, col(1, 2, 3, 4, 5, 6)).
            */

            int configurationCount = dissimilarities.NumberOfRows;

            var d2 = dissimilarities.Apply((v) => Pow(v, 2));

            var c =
                DoubleMatrix.Identity(configurationCount)
                -
                DoubleMatrix.Dense(configurationCount, configurationCount, 1.0 / configurationCount);

            var b = -0.5 * c * d2 * c;

            var eigenvalues = SpectralDecomposition.Decompose(
                matrix: b,
                lowerTriangularPart: true,
                out DoubleMatrix eigenvectors);

            int effectiveConfigurationDimension;
            int maximalConfigurationDimension = 0;
            for (int i = configurationCount - 1; i > -1; i--)
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
                        paramName: nameof(dissimilarities),
                        message: ImplementationServices.GetResourceString(
                            "STR_EXCEPT_NMMDS_NO_INITIAL_CONFIGURATION"));
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
                        message: ImplementationServices.GetResourceString(
                            "STR_EXCEPT_NMMDS_UNALLOWED_CONFIGURATION_DIMENSION"));
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
                            firstIndex: configurationCount - 1,
                            increment: -1,
                            indexBound: configurationCount - effectiveConfigurationDimension)];

            for (int i = 0; i < effectiveConfigurationDimension; i++)
            {
                int j = configurationCount - i - 1;
                relevantValues[i, i] = Sqrt(eigenvalues[j, j]);
            }

            relevantVectors.InPlaceTranspose();

            var transposedInitialConfiguration = relevantValues * relevantVectors;

            var initialArgument = DoubleMatrix.Dense(
                numberOfRows: 1,
                numberOfColumns: transposedInitialConfiguration.Count,
                data: transposedInitialConfiguration.implementor.Storage,
                copyData: false);

            #endregion

            #region Stress function optimization

            #region Dissimilarity info

            int dissimilarityCount = (configurationCount * (configurationCount - 1)) / 2;

            double[] dissimilarityArray = new double[dissimilarityCount];
            (int, int)[] dissimilarityIndexes = new (int, int)[dissimilarityCount];

            int index = 0;
            for (int i = 0; i < configurationCount; i++)
            {
                for (int j = i + 1; j < configurationCount; j++, index++)
                {
                    dissimilarityIndexes[index] = (i, j);
                    dissimilarityArray[index] = dissimilarities[i, j];
                }
            }

            SortHelper.Sort(
                data: dissimilarityArray,
                sortDirection: SortDirection.Ascending,
                out int[] dissimilarityIndexTable);

            (int RowIndex, int ColumnIndex)[] dissimilarityPositions =
                new (int, int)[dissimilarityCount];

            for (int l = 0; l < dissimilarityCount; l++)
            {
                dissimilarityPositions[l] =
                    dissimilarityIndexes[dissimilarityIndexTable[l]];
            }

            IndexPartition<double> dissimilarityPartitions = IndexPartition.Create(
                elements: dissimilarityArray);

            #endregion

            DoubleMatrix optimalConfiguration =
                NonMetricMultidimensionalScaling.OptimizeStress(
                    initialArgument,
                    ref configurationCount,
                    ref effectiveConfigurationDimension,
                    dissimilarityPositions,
                    dissimilarityPartitions,
                    ref minkowskiDistanceOrder,
                    maximumNumberOfIterations,
                    terminationTolerance,
                    out double stress,
                    out bool hasConverged);

            #endregion

            #region Results

            var optimalConfigurationStorage = optimalConfiguration.implementor.Storage;

            optimalConfiguration = DoubleMatrix.Dense(
                numberOfRows: configurationCount,
                numberOfColumns: effectiveConfigurationDimension,
                data: optimalConfigurationStorage,
                storageOrder: StorageOrder.RowMajor);

            if (dissimilarities.HasRowNames)
            {
                for (int i = 0; i < dissimilarities.NumberOfRows; i++)
                {
                    if (dissimilarities.TryGetRowName(i, out string rowName))
                    {
                        optimalConfiguration.SetRowName(i, rowName);
                    }
                }
            }

            var analysisResults =
                new NonMetricMultidimensionalScaling(
                    configuration: optimalConfiguration,
                    stress: stress,
                    hasConverged: hasConverged);

            return analysisResults;

            #endregion
        }

        #endregion

        #region Minkowski metrics

        /// <summary>
        /// Computes the Minkowski metric of order <paramref name="order"/>.
        /// </summary>
        /// <param name="left">
        /// The left operand of the metric.
        /// </param>
        /// <param name="right">
        /// The right operand of the metric.
        /// </param>
        /// <param name="order">
        /// The order of the metric. It must be greater than or equal to 1
        /// and less than infinity.
        /// </param>
        /// <remarks>
        /// <para>
        /// It is expected that <paramref name="left"/> and <paramref name="right"/>
        /// have the same length and that <paramref name="order"/> is greater than or 
        /// equal to 1.
        /// </para>
        /// </remarks>        
        /// <returns>
        /// The Minkowski metric of order <paramref name="order"/> between
        /// <paramref name="left"/> and <paramref name="right"/>.
        /// </returns>
        static double FiniteOrderMinkowskiMetric(
            ReadOnlyMemory<double> left,
            ReadOnlyMemory<double> right,
            ref double order)
        {
            var leftSpan = left.Span;
            var rightSpan = right.Span;

            double sum = 0.0;

            for (int i = 0; i < left.Length; i++)
            {

                sum += Pow(Abs(leftSpan[i] - rightSpan[i]), order);
            }

            return Pow(sum, 1.0 / order);
        }

        #endregion

        #region Stress computations

        static double ComputeStress(
            DoubleMatrix argument,
            ref int configurationDimension,
            (int RowIndex, int ColumnIndex)[] dissimilarityPositions,
            IndexPartition<double> dissimilarityPartitions,
            ref double minkowskiDistanceOrder,
            out double sStar,
            out double tStar,
            out DoubleMatrix distances,
            out List<double> disparities)
        {
            #region Distances and dissimilarity blocks

            distances = NonMetricMultidimensionalScaling.GetDistances(
                argument: argument,
                configurationDimension: ref configurationDimension,
                dissimilarityPositions: dissimilarityPositions,
                minkowskiDistanceOrder: ref minkowskiDistanceOrder);

            var blockList = NonMetricMultidimensionalScaling.GetDissimilarityBlocks(
                dissimilarityPartitions,
                distances);

            #endregion

            #region Stress computation

            int dissimilarityCount = dissimilarityPositions.Length;

            disparities = new List<double>(dissimilarityCount);

            sStar = 0.0;
            tStar = 0.0;

            int stressIndex = 0;

            var distanceArray = distances.implementor.Storage;

            foreach (var block in blockList)
            {
                double sumOfProximities = block.SumOfProximities;
                double count = block.Count;

                double currentDisparity = sumOfProximities / count;

                for (int l = 0; l < count; l++, stressIndex++)
                {
                    disparities.Add(currentDisparity);
                    var currentDistance = distanceArray[stressIndex];
                    sStar += Math.Pow(currentDistance - currentDisparity, 2);
                    tStar += Math.Pow(currentDistance, 2);
                }
            }

            double stress = Math.Sqrt(sStar / tStar);

            return stress;

            #endregion
        }

        static double[] ComputeStressGradient(
            DoubleMatrix argument,
            ref int configurationDimension,
            (int RowIndex, int ColumnIndex)[] dissimilarityPositions,
            ref double minkowskiDistanceOrder,
            ref double stress,
            ref double sStar,
            ref double tStar,
            DoubleMatrix distances,
            List<double> disparities)
        {
            double[] gradientArray = new double[argument.Count];

            var argumentStorage = argument.implementor.Storage;

            for (int l = 0; l < dissimilarityPositions.Length; l++)
            {
                var (RowIndex, ColumnIndex) = dissimilarityPositions[l];

                if (RowIndex != ColumnIndex)
                {
                    var distance = distances[l];
                    var disparity = disparities[l];

                    var globalTerm =
                        (stress * (((distance - disparity) / sStar) - (distance / tStar)))
                        /
                        Math.Pow(distance, minkowskiDistanceOrder - 1);

                    int rowOffset = RowIndex * configurationDimension;
                    int columnOffset = ColumnIndex * configurationDimension;

                    for (int j = 0; j < configurationDimension; j++)
                    {
                        var rowCoordinateIndex = rowOffset + j;
                        var columnCoordinateIndex = columnOffset + j;

                        var rowCoordinate = argumentStorage[rowCoordinateIndex];
                        var columnCoordinate = argumentStorage[columnCoordinateIndex];
                        var rowColumnCoordinateDifference = rowCoordinate - columnCoordinate;

                        var localTerm =
                            Math.Pow(
                                Math.Abs(rowColumnCoordinateDifference),
                                minkowskiDistanceOrder - 1.0)
                            *
                            Math.Sign(rowColumnCoordinateDifference);

                        var term = globalTerm * localTerm;

                        gradientArray[rowCoordinateIndex] += term;

                        gradientArray[columnCoordinateIndex] -= term;
                    }
                }
            }

            return gradientArray;
        }

        #endregion

        #region Stress optimization

        static DoubleMatrix OptimizeStress(
            DoubleMatrix initialArgument,
            ref int configurationCount,
            ref int configurationDimension,
            (int RowIndex, int ColumnIndex)[] dissimilarityPositions,
            IndexPartition<double> dissimilarityPartitions,
            ref double minkowskiDistanceOrder,
            int maximumNumberOfIterations,
            double terminationTolerance,
            out double stress,
            out bool hasConverged)
        {
            hasConverged = true;
            int numberOfIterations = 0;

            double stressGradientMagnitude = double.PositiveInfinity;

            double[] previousStressGradientArray = null;
            double previousStressGradientSumOfSquaredCoordinates = 0;
            double previousAlpha = 0.2;
            double alpha = previousAlpha;
            double angleFactor;
            double relaxationFactor;
            double goodLuckFactor;
            double doubleConfigurationCount = configurationCount;

            LinkedList<double> stressHistory = new();

            var argument = initialArgument;

            while (true)
            {
                numberOfIterations++;

                NonMetricMultidimensionalScaling.Normalize(
                    argument,
                    configurationCount,
                    configurationDimension);

                double argumentStress =
                    NonMetricMultidimensionalScaling.ComputeStress(
                        argument,
                        ref configurationDimension,
                        dissimilarityPositions,
                        dissimilarityPartitions,
                        ref minkowskiDistanceOrder,
                        out double sStar,
                        out double tStar,
                        out DoubleMatrix distances,
                        out List<double> disparities);

                stressHistory.AddLast(argumentStress);

                if (argumentStress < terminationTolerance)
                {
                    break;
                }

                if (stressGradientMagnitude <= terminationTolerance)
                {
                    break;
                }

                if (numberOfIterations == maximumNumberOfIterations)
                {
                    hasConverged = false;
                    break;
                }

                if (stressHistory.Count > 5)
                {
                    stressHistory.RemoveFirst();
                }

                var stressGradientArray =
                    NonMetricMultidimensionalScaling.ComputeStressGradient(
                    argument,
                    ref configurationDimension,
                    dissimilarityPositions,
                    ref minkowskiDistanceOrder,
                    ref argumentStress,
                    ref sStar,
                    ref tStar,
                    distances,
                    disparities);

                double stressGradientSumOfSquaredCoordinates = 0.0;
                double cosineThetaNumerator = 0.0;

                for (int i = 0; i < stressGradientArray.Length; i++)
                {
                    var term = stressGradientArray[i];
                    stressGradientSumOfSquaredCoordinates += Pow(term, 2.0);

                    if (numberOfIterations > 1)
                    {
                        cosineThetaNumerator +=
                            previousStressGradientArray[i] * term;
                    }
                }

                stressGradientMagnitude =
                    Sqrt(
                        stressGradientSumOfSquaredCoordinates
                        /
                        doubleConfigurationCount);

                if (numberOfIterations > 1)
                {
                    double cosTheta =
                        cosineThetaNumerator
                        /
                        (Sqrt(stressGradientSumOfSquaredCoordinates)
                         *
                         Sqrt(previousStressGradientSumOfSquaredCoordinates));

                    angleFactor = Pow(4.0, Pow(cosTheta, 3.0));

                    double fiveStepRatio =
                        Min(
                            1.0,
                            argumentStress
                            /
                            stressHistory.First.ValueRef);

                    relaxationFactor = 1.3 / (1.0 + Pow(fiveStepRatio, 5.0));

                    goodLuckFactor =
                        Min(
                            1.0,
                            argumentStress
                            /
                            stressHistory.Last.Previous.ValueRef);

                    alpha = previousAlpha * angleFactor * relaxationFactor * goodLuckFactor;
                }

                var argumentArray = argument.implementor.Storage;
                for (int i = 0; i < argumentArray.Length; i++)
                {
                    argumentArray[i] -= stressGradientArray[i] * alpha / stressGradientMagnitude;
                }

                previousStressGradientSumOfSquaredCoordinates =
                    stressGradientSumOfSquaredCoordinates;
                previousStressGradientArray = stressGradientArray;
                previousAlpha = alpha;
            }

            stress = stressHistory.Last.Value;

            return argument;
        }

        #endregion

        #region Configuration normalization

        /// <summary>
        /// Normalizes the specified argument of the Stress function.
        /// </summary>
        /// <param name="argument">
        /// The configuration to normalize.
        /// </param>
        /// <param name="configurationCount">
        /// The number of points in the configuration.
        /// </param>
        /// <param name="configurationDimension">
        /// The dimension of the configuration.
        /// </param>
        /// <remarks>
        /// <para>
        /// The <paramref name="argument"/> represents the configuration of points
        /// as follows. Let <latex>m</latex> be the number of points,
        /// and let <latex>x_i</latex>, for <latex>i = 0,\ldots,m-1</latex>, the 
        /// <latex>i</latex>-th configuration row. 
        /// Then the <paramref name="argument"/> is a row matrix 
        /// given by the concatenation of the rows of the configuration matrix, say
        /// <latex>\mx{x_0,\ldots,x_{m-1}}</latex>.
        /// </para>
        /// <para>
        /// The normalization is performed following Kruskal, 
        /// (1964)<cite>kruskal-b-1964</cite>: 
        /// the configuration is centered and scaled so that
        /// the root mean square of the coordinates is equal to 1.
        /// </para>
        /// </remarks>
        /// <seealso href="https://doi.org/10.1007/BF02289694"/>
        private static void Normalize(
            DoubleMatrix argument,
            int configurationCount,
            int configurationDimension)
        {
            var storage = argument.implementor.Storage;

            var transposedConfiguration = DoubleMatrix.Dense(
                numberOfRows: configurationDimension,
                numberOfColumns: configurationCount,
                data: storage,
                copyData: false);

            var means = Stat.Mean(transposedConfiguration, DataOperation.OnRows);

            for (int j = 0; j < transposedConfiguration.NumberOfRows; j++)
            {
                transposedConfiguration[j, ":"] -= means[j];
            }

            double sumOfSquaredCenteredCoordinates = 0.0;

            for (int i = 0; i < storage.Length; i++)
            {
                sumOfSquaredCenteredCoordinates += Math.Pow(storage[i], 2);
            }

            var scaleFactor =
                1.0 / Math.Sqrt(sumOfSquaredCenteredCoordinates / configurationCount);

            for (int i = 0; i < storage.Length; i++)
            {
                storage[i] *= scaleFactor;
            }
        }

        #endregion

        #region Configuration distances

        // Make the following method private
        static DoubleMatrix GetDistances(
            DoubleMatrix argument,
            ref int configurationDimension,
            (int RowIndex, int ColumnIndex)[] dissimilarityPositions,
            ref double minkowskiDistanceOrder)
        {
            int dissimilarityCount = dissimilarityPositions.Length;

            double[] distanceArray = new double[dissimilarityCount];

            var argumentStorage = argument.implementor.Storage;

            for (int l = 0; l < dissimilarityCount; l++)
            {
                var (RowIndex, ColumnIndex) = dissimilarityPositions[l];

                var left = new ReadOnlyMemory<double>(
                    array: argumentStorage,
                    start: RowIndex * configurationDimension,
                    length: configurationDimension);

                var right = new ReadOnlyMemory<double>(
                    array: argumentStorage,
                    start: ColumnIndex * configurationDimension,
                    length: configurationDimension);

                distanceArray[l] =
                    NonMetricMultidimensionalScaling.FiniteOrderMinkowskiMetric(
                        left,
                        right,
                        ref minkowskiDistanceOrder);
            }

            var distances = DoubleMatrix.Dense(
                numberOfRows: dissimilarityCount,
                numberOfColumns: 1,
                data: distanceArray,
                copyData: false);

            return distances;
        }

        #endregion

        #region Configuration dissimilarity blocks

        static LinkedList<DissimilarityBlock> GetDissimilarityBlocks(
            IndexPartition<double> dissimilarityPartitions,
            DoubleMatrix distances)
        {
            var blockList = new LinkedList<DissimilarityBlock>();

            #region Initial dissimilarity blocks

            foreach (var dissimilarity in dissimilarityPartitions.Identifiers)
            {
                var part = dissimilarityPartitions[dissimilarity];

                int partCount = part.Count;

                double sumOfProximities =
                    partCount == 1
                    ?
                    distances[part[0]]
                    :
                    Stat.Sum(distances.Vec(part));

                var block = new DissimilarityBlock
                {
                    Count = partCount,
                    SumOfProximities = sumOfProximities
                };

                blockList.AddLast(block);
            }

            #endregion

            #region Dissimilarity blocks' fitting (Disparities computation)

            var activeBlockNode = blockList.First;
            activeBlockNode.Value.IsUpActive = true;

            while (activeBlockNode != null)
            {
                var activeBlock = activeBlockNode.Value;
                activeBlock.IsUpActive = true;

                while (!activeBlock.IsSatisfied)
                {
                    if (activeBlock.IsUpActive)
                    {
                        if (!activeBlockNode.IsUpSatisfied())
                        {
                            activeBlockNode.MergeNextNode();
                        }

                        activeBlock.IsDownActive = true;
                    }

                    if (activeBlock.IsDownActive)
                    {
                        if (!activeBlockNode.IsDownSatisfied())
                        {
                            activeBlockNode.MergePreviousNode();
                        }

                        activeBlock.IsUpActive = true;
                    }
                }

                activeBlockNode = activeBlockNode.Next;
            }

            return blockList;

            #endregion
        }

        #endregion
    }

    #region Disparities fitting info

    /// <summary>
    /// Represents a block of dissimilarities.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class is used to store the number of dissimilarities and the sum of 
    /// corresponding proximities. Its definition is based on Section 8 of 
    /// Kruskal, (1964)<cite>kruskal-b-1964</cite>.
    /// </para>
    /// </remarks>
    class DissimilarityBlock
    {
        /// <summary>
        /// Gets or sets the number of dissimilarities in this instance.
        /// </summary>
        public double Count { get; set; }

        /// <summary>
        /// Gets or sets the sum of proximities in this instance.
        /// </summary>
        public double SumOfProximities { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the block is down active.
        /// </summary>
        public bool IsDownActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the block is up active.
        /// </summary>
        public bool IsUpActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the block 
        /// is down satisfied.
        /// </summary>
        public bool IsDownSatisfied { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the block 
        /// is up satisfied.
        /// </summary>
        public bool IsUpSatisfied { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the block 
        /// is up satisfied.
        /// </summary>
        public bool IsSatisfied => IsDownSatisfied && IsUpSatisfied;
    }

    /// <summary>
    /// Provides extension methods for class <see cref="LinkedListNode{T}"/>
    /// with type parameter <see cref="DissimilarityBlock"/>.
    /// </summary>
    static class DissimilarityBlockLinkedListNodeExtensions
    {
        #region Block satisfaction

        /// <summary>
        /// Checks if the dissimilarity block corresponding to the specified node 
        /// is down satisfied.
        /// </summary>
        /// <param name="blockNode">
        /// The node corresponding to the dissimilarity block to check.
        /// </param>
        /// <returns>
        /// <c>true</c> if the dissimilarity block corresponding to the specified 
        ///  is down satisfied; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsDownSatisfied(
            this LinkedListNode<DissimilarityBlock> blockNode)
        {
            var block = blockNode.Value;

            if (blockNode.Previous is null)
            {
                block.IsDownSatisfied = true;
            }
            else
            {
                var previousBlock = blockNode.Previous.Value;
                double previousBlockCount = previousBlock.Count;

                double previousBlockDisparity =
                    previousBlockCount == 1
                    ?
                    previousBlock.SumOfProximities
                    :
                    previousBlock.SumOfProximities / previousBlockCount;

                double blockCount = block.Count;

                double blockDisparity =
                    blockCount == 1
                    ?
                    block.SumOfProximities
                    :
                    block.SumOfProximities / blockCount;

                block.IsDownSatisfied = previousBlockDisparity < blockDisparity;
            }

            return block.IsDownSatisfied;
        }

        /// <summary>
        /// Checks if the dissimilarity block corresponding to the specified node 
        /// is up satisfied.
        /// </summary>
        /// <param name="blockNode">
        /// The node corresponding to the dissimilarity block to check.
        /// </param>
        /// <returns>
        /// <c>true</c> if the dissimilarity block corresponding to the specified 
        /// node is up satisfied; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsUpSatisfied(
            this LinkedListNode<DissimilarityBlock> blockNode)
        {
            var block = blockNode.Value;

            if (blockNode.Next is null)
            {
                block.IsUpSatisfied = true;
            }
            else
            {
                var nextBlock = blockNode.Next.Value;
                double nextBlockCount = nextBlock.Count;

                double nextBlockDisparity =
                    nextBlockCount == 1
                    ?
                    nextBlock.SumOfProximities
                    :
                    nextBlock.SumOfProximities / nextBlockCount;

                double blockCount = block.Count;

                double blockDisparity =
                    blockCount == 1
                    ?
                    block.SumOfProximities
                    :
                    block.SumOfProximities / blockCount;

                block.IsUpSatisfied = nextBlockDisparity > blockDisparity;
            }

            return block.IsUpSatisfied;
        }

        #endregion

        #region Merging blocks

        /// <summary>
        /// Merges the dissimilarity block corresponding to the specified node 
        /// with its <see cref="LinkedListNode{T}.Previous"/> one.
        /// </summary>
        /// <param name="blockNode">
        /// The node corresponding to the dissimilarity block to merge.
        /// </param>
        public static void MergePreviousNode(
            this LinkedListNode<DissimilarityBlock> blockNode)
        {
            var previousBlockNode = blockNode.Previous;
            var previousBlock = previousBlockNode.Value;

            var block = blockNode.Value;

            block.Count += previousBlock.Count;
            block.SumOfProximities += previousBlock.SumOfProximities;

            blockNode.List.Remove(previousBlockNode);
        }

        /// <summary>
        /// Merges the dissimilarity block corresponding to the specified node 
        /// with its <see cref="LinkedListNode{T}.Next"/> one.
        /// </summary>
        /// <param name="blockNode">
        /// The node corresponding to the dissimilarity block to merge.
        /// </param>
        public static void MergeNextNode(
            this LinkedListNode<DissimilarityBlock> blockNode)
        {
            var nextBlockNode = blockNode.Next;
            var nextBlock = nextBlockNode.Value;

            var block = blockNode.Value;

            block.Count += nextBlock.Count;
            block.SumOfProximities += nextBlock.SumOfProximities;

            blockNode.List.Remove(nextBlockNode);
        }

        #endregion
    }

    #endregion
}