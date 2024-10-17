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
    /// Provides methods to analyze clusters 
    /// in a data set.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The <see cref="Clusters"/> class supplies method
    /// <see cref="Discover(DoubleMatrix, int)"/>
    /// for identifying clusters in a data set, and 
    /// method 
    /// <see cref="Explain(DoubleMatrix, IndexPartition{double}, int)"/>
    /// to select features from a data set in order to interpret
    /// an existing partition of the available items. 
    /// </para>
    /// <para>
    /// Such methods assume that the data set can be represented as 
    /// a <see cref="DoubleMatrix"/> instance having as many rows as
    /// the number of items, 
    /// while the number of columns is the number of available features. 
    /// The <i>i</i>-th row thus contains the
    /// observations at item <i>i</i> of the features under study.
    /// </para>
    /// <para>
    /// Clusters of items are described as an <see cref="IndexPartition{Double}"/> 
    /// instance, whose parts represents the clusters as collections
    /// of indexes valid for the rows in the data set.
    /// </para>
    /// <para><b>Advanced scenarios</b></para>
    /// Class <see cref="Clusters"/> exploits the Cross-Entropy method to
    /// solve its optimization problems. For more information about 
    /// specialized algorithms, or different
    /// criteria for cluster discovering or feature selection, see 
    /// the documentation of methods <see cref="Discover(DoubleMatrix, int)"/>
    /// or <see cref="Explain(DoubleMatrix, IndexPartition{double}, int)"/>,
    /// respectively.
    /// </remarks>
    public static class Clusters
    {
        #region Explain

        /// <summary>
        /// Explains existing clusters by selecting
        /// a number of features from the specified corresponding data set.
        /// </summary>
        /// <param name="data">
        /// The matrix whose columns contain the features observed at the
        /// items under study.
        /// </param>
        /// <param name="partition">
        /// A partition of the row indexes valid for <paramref name="data"/>.
        /// </param>
        /// <param name="numberOfExplanatoryFeatures">
        /// The number of features to be selected.
        /// </param>
        /// <remarks>
        /// <para>
        /// Method <see cref="Explain(
        /// DoubleMatrix, IndexPartition{double}, int)"/> 
        /// selects the specified <paramref name="numberOfExplanatoryFeatures"/> 
        /// from the given 
        /// <paramref name="data"/>, by minimizing the Davies-Bouldin 
        /// Index corresponding to
        /// the <paramref name="partition"/> of the items under study.
        /// </para>
        /// <para>
        /// This method uses a default Cross-Entropy context of 
        /// type <see cref="CombinationOptimizationContext"/> to identify the 
        /// optimal features.
        /// If different selection criteria need to be applied, 
        /// or extra control on the 
        /// parameters of the underlying algorithm is required, 
        /// a specialized <see cref="CombinationOptimizationContext"/> can be
        /// can be instantiated and hence exploited executing
        /// method <see cref="SystemPerformanceOptimizer.Optimize(
        /// SystemPerformanceOptimizationContext, double, int)">Optimize</see>
        /// on a <see cref="SystemPerformanceOptimizer"/> object.
        /// See the documentation about <see cref="CombinationOptimizationContext"/>
        /// for additional examples.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, an existing partition of 12 items is explained
        /// by selecting 2 features out of the seven ones available in 
        /// an artificial data set regarding the items under study.
        /// </para>
        /// <para>
        /// <code title="Selecting features from a data set to explain a given partition."
        /// source="..\Novacta.Analytics.CodeExamples\ClustersExplainExample0.cs.txt" 
        /// language="cs" />
        /// </para> 
        /// </example>
        /// <returns>
        /// The collection of column indexes, valid for <paramref name="data"/>, that
        /// correspond to the features selected to explain the 
        /// given <paramref name="partition"/> of row indexes.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="partition"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="numberOfExplanatoryFeatures"/> is not positive.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="numberOfExplanatoryFeatures"/> is not less than 
        /// the number of columns in <paramref name="data"/>.<br/>
        /// -or-<br/>
        /// A part in <paramref name="partition"/> contains a position 
        /// which is not valid as a row index of <paramref name="data"/>.
        /// </exception>
        /// <seealso cref="IndexPartition.DaviesBouldinIndex(
        /// DoubleMatrix, IndexPartition{double})"/>
        /// <seealso cref="CombinationOptimizationContext"/>
        /// <seealso cref="SystemPerformanceOptimizer"/>
        public static IndexCollection Explain(
            DoubleMatrix data,
            IndexPartition<double> partition,
            int numberOfExplanatoryFeatures)
        {
            #region Input validation

            ArgumentNullException.ThrowIfNull(data);

            if (numberOfExplanatoryFeatures < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(numberOfExplanatoryFeatures),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_POSITIVE"));
            }

            int stateDimension = data.NumberOfColumns;

            if (stateDimension <= numberOfExplanatoryFeatures)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER_COLUMNS"),
                        nameof(numberOfExplanatoryFeatures),
                        nameof(data)),
                    nameof(numberOfExplanatoryFeatures)
                    );
            }

            ArgumentNullException.ThrowIfNull(partition);

            #endregion

            double objectiveFunction(DoubleMatrix x)
            {
                IndexCollection selected = x.FindNonzero();

                double performance =
                    IndexPartition.DaviesBouldinIndex(
                        data: data[":", selected],
                        partition: partition);

                return performance;
            }

            var optimizer =
                new SystemPerformanceOptimizer();

            var context = new CombinationOptimizationContext(
                objectiveFunction: objectiveFunction,
                stateDimension: stateDimension,
                combinationDimension: numberOfExplanatoryFeatures,
                probabilitySmoothingCoefficient: .8,
                optimizationGoal: OptimizationGoal.Minimization,
                minimumNumberOfIterations: 3,
                maximumNumberOfIterations: 1000);

            double rarity = .01;

            int sampleSize = 1000 * stateDimension;

            var results = optimizer.Optimize(
                context,
                rarity,
                sampleSize);

            var optimalState = results.OptimalState;

            return optimalState.FindNonzero();
        }

        #endregion

        #region Discover

        /// <summary>
        /// Discovers optimal clusters 
        /// in a data set.
        /// </summary>
        /// <param name="maximumNumberOfParts">
        /// The maximum number of parts allowed in the optimal 
        /// partition.
        /// </param>
        /// <param name="data">
        /// The matrix whose rows contain the observations for the
        /// items under study.
        /// </param>
        /// <remarks>
        /// <para>
        /// Method <see cref="Discover(DoubleMatrix, int)"/> partitions 
        /// a collection of items in no more than the specified 
        /// <paramref name="maximumNumberOfParts"/>, 
        /// given the specified <paramref name="data"/>, by minimizing the sum of
        /// intra-cluster squared deviations.
        /// </para>
        /// <para>
        /// This method uses a default Cross-Entropy context of 
        /// type <see cref="PartitionOptimizationContext"/> to identify the 
        /// optimal partition.
        /// If different partitioning criteria need to be applied, 
        /// or extra control on the 
        /// parameters of the underlying algorithm is required, 
        /// a specialized <see cref="PartitionOptimizationContext"/> can be
        /// can be instantiated and hence exploited executing
        /// method <see cref="SystemPerformanceOptimizer.Optimize(
        /// SystemPerformanceOptimizationContext, double, int)">Optimize</see>
        /// on a <see cref="SystemPerformanceOptimizer"/> object.
        /// See the documentation about <see cref="PartitionOptimizationContext"/>
        /// for additional examples.
        /// </para>
        /// </remarks>
        /// <example>
        /// <para>
        /// In the following example, a partition that optimally split 12 items 
        /// is discovered
        /// given
        /// an artificial data set regarding the items under study.
        /// </para>
        /// <para>
        /// <code title="Optimal partitioning of a data set."
        /// source="..\Novacta.Analytics.CodeExamples\ClustersDiscoverExample0.cs.txt" 
        /// language="cs" />
        /// </para> 
        /// </example>
        /// <returns>
        /// A partition of the row indexes valid for <paramref name="data"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maximumNumberOfParts"/> is not greater than one.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="maximumNumberOfParts"/> is not less than 
        /// the number of rows in <paramref name="data"/>.
        /// </exception>
        /// <seealso cref="PartitionOptimizationContext"/>
        /// <seealso cref="SystemPerformanceOptimizer"/>
        public static IndexPartition<double> Discover(
            DoubleMatrix data,
            int maximumNumberOfParts)
        {
            #region Input validation

            ArgumentNullException.ThrowIfNull(data);

            if (maximumNumberOfParts < 2)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(maximumNumberOfParts),
                string.Format(
                    CultureInfo.InvariantCulture,
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_GREATER_THAN_VALUE"),
                    "1"));
            }

            int stateDimension = data.NumberOfRows;

            if (stateDimension <= maximumNumberOfParts)
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_BE_LESS_THAN_OTHER_ROWS"),
                        nameof(maximumNumberOfParts),
                        nameof(data)),
                    nameof(maximumNumberOfParts)
                    );
            }

            #endregion

            double objectiveFunction(DoubleMatrix x)
            {
                double performance = 0.0;
                var partition = IndexPartition.Create(x);

                foreach (double category in partition.Identifiers)
                {
                    performance += Stat.Sum(
                        Stat.SumOfSquaredDeviations(
                            data[partition[category], ":"],
                            DataOperation.OnColumns));
                }

                return performance;
            }

            var optimizer =
                new SystemPerformanceOptimizer();

            var context = new PartitionOptimizationContext(
                objectiveFunction: objectiveFunction,
                stateDimension: stateDimension,
                partitionDimension: maximumNumberOfParts,
                probabilitySmoothingCoefficient: .8,
                optimizationGoal: OptimizationGoal.Minimization,
                minimumNumberOfIterations: 3,
                maximumNumberOfIterations: 1000);

            double rarity = .01;

            int sampleSize = 500 * maximumNumberOfParts;

            var results = optimizer.Optimize(
                context,
                rarity,
                sampleSize);

            return IndexPartition.Create(results.OptimalState);
        }

        #endregion
    }
}
