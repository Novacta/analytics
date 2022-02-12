// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Novacta.Analytics.Infrastructure;

namespace Novacta.Analytics
{
    /// <summary>
    /// Provides static methods for creating index partition objects and for their
    /// evaluation in terms of linkage among parts.
    /// </summary>
    /// <remarks>
    /// <para id='partitionDefinition'>
    /// An index partition is a data structure that has a specific number of
    /// parts. Each part is a collection of zero-based indexes represented by 
    /// an instance of type <see cref="IndexCollection"/>,
    /// and has an 
    /// identifier whose type is a generic parameter. 
    /// In its general form, an index partition can be constructed on the base
    /// of an <see cref="IndexCollection"/> instance: each index in the collection
    /// is passed to a <see cref="Func{Int32, T}"/> object, which returns a
    /// value of type &lt;T&gt;, so that the indexes for which is returned the same
    /// value are inserted in the same part having such value as its identifier.
    /// This is the way an index partition is built by method
    /// <see cref="Create{T}(IndexCollection,Func{int, T})"/>.
    /// However, an index partition is often initialized by inspecting a collection of 
    /// elements and adding a new part to the partition each time a new element is 
    /// encountered while iterating over the collection. 
    /// Such element is the identifier of the new part, which will store
    /// the positions, in the
    /// collection of elements, where reside values equal to the part identifier.
    /// As a consequence, the parts are mutually exclusive subsets of the 
    /// range of available collection positions.
    /// </para>
    /// <para><b>Instantiation of IndexPartition&lt;T&gt; objects</b></para>
    /// <para id='partitionInstantiation'>
    /// The <see cref="IndexPartition"/> class does not itself represent a partition. 
    /// Instead, it is 
    /// a class that provides the static method 
    /// <see cref="Create{T}(IEnumerable{T})"/>
    /// for creating instances of the 
    /// <see cref="IndexPartition{T}"/> generic type.
    /// It also provides 
    /// method
    /// <see cref="Create(DoubleMatrix)"/>
    /// and its overloaded versions 
    /// that you can 
    /// call to instantiate partition objects 
    /// without having to explicitly specify the type of part identifiers
    /// when the type is <see cref="System.Double"/> or
    /// <see cref="DoubleMatrixRow"/>.
    /// </para>
    /// <para><b>Partition evaluations</b></para>
    /// <para>
    /// In Cluster Analysis, data collecting individual observations 
    /// of multiple variables are partitioned in clusters trying to 
    /// minimize the dissimilarity among clusters, or to maximize the 
    /// similarity of the individuals in each cluster. 
    /// To obtain such optimizations, a linkage between two given parts 
    /// can be defined in terms of the pairwise distances of their observations,
    /// and linkage criterions can be applied to evaluate different partitions
    /// in the search for the optimal one. Some criterions are exposed in
    /// class <see cref="IndexPartition"/> as methods 
    /// <see cref="MinimumCentroidLinkage">MinimumMeanLinkage</see>,
    /// <see cref="DunnIndex">DunnIndex</see>, and
    /// <see cref="DaviesBouldinIndex">DaviesBouldinIndex</see>.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para id='IndexPartitionEx0Intro'>
    /// In the following example, the row indexes of a matrix are partitioned 
    /// by the contents of its first column.
    /// Each part is identified by a value, the part identifier, and contains
    /// the indexes of the rows in which the identifier 
    /// is positioned in the first column.
    ///</para>
    /// <para id='IndexPartitionEx0Code'>
    /// <code title="Partitioning the rows of a matrix by the contents of one of
    /// its columns"
    /// source="..\Novacta.Analytics.CodeExamples\IndexPartitionExample0.cs.txt" 
    /// language="cs" />
    /// </para> 
    /// <para id='IndexPartitionEx1Intro'> 
    /// In the following example, the row indexes of a matrix are partitioned 
    /// by the contents of its rows.
    /// Each part is identified by a distinct row, the part identifier, and contains
    /// the indexes of the rows which are equal to the identifier.
    /// </para>
    /// <para id='IndexPartitionEx1Code'>
    /// <code title="Partitioning the rows of a matrix by their contents"
    /// source="..\Novacta.Analytics.CodeExamples\IndexPartitionExample1.cs.txt" 
    /// language="cs" />
    /// </para>
    /// <para id='IndexPartitionEx2Intro'>
    /// In the following example, the linear indexes of a matrix are partitioned 
    /// by the sign of its entries.
    /// The part corresponding to zero entries is identified by zero, 
    /// the part corresponding to positive entries by 1, and the part of negative
    /// entries by -1.
    /// </para>
    /// <para id='IndexPartitionEx2Code'>
    /// <code title="Partitioning the linear indexes of a matrix by the sign of its entries"
    /// source="..\Novacta.Analytics.CodeExamples\IndexPartitionExample2.cs.txt" 
    /// language="cs" />
    /// </para>
    /// <para id='IndexPartitionEx3Intro'> 
    /// In the following example, the indexes of an array of strings are partitioned 
    /// by their contents.
    /// </para>
    /// <para id='IndexPartitionEx3Code'>
    /// <code title="Partitioning the indexes of an array by its contents"
    /// source="..\Novacta.Analytics.CodeExamples\IndexPartitionExample3.cs.txt" 
    /// language="cs" />
    /// </para>
    /// <para id='IndexPartitionEx5Intro'>
    /// In the following example, the linear indexes of the main diagonal of
    /// a matrix are partitioned 
    /// by checking if their corresponding entries are less than 3 in absolute
    /// value.
    /// Two parts are created, one for diagonal
    /// entries less than 3 in absolute value, the other for 
    /// entries not satisfying that condition.
    /// </para>
    /// <para id='IndexPartitionEx5Code'>
    /// <code title="Partitioning the main diagonal entries of a matrix by 
    /// their absolute value satisfying a certain condition"
    /// source="..\Novacta.Analytics.CodeExamples\IndexPartitionExample5.cs.txt" 
    /// language="cs" />
    /// </para> 
    /// </example>
    /// <seealso cref="IndexPartition{T}"/>
    /// <seealso cref="Distance"/>
    /// <seealso cref="DoubleMatrixRow"/>
    /// <seealso cref="Clusters"/>
    /// <seealso cref="Advanced.PartitionOptimizationContext"/>
    public static class IndexPartition
    {
        #region Factory methods

        /// <summary>
        /// Creates a partition of the elements in 
        /// an <see cref="IndexCollection"/> instance by 
        /// aggregating those elements corresponding to a same part.
        /// </summary>
        /// <typeparam name="T">The type of the 
        /// parts in the partition.
        /// </typeparam>
        /// <param name="elements">The collection of indexes 
        /// to be partitioned.</param>
        /// <param name="partitioner">A method which returns the part identifier
        /// corresponding to a given element in <paramref name="elements"/>.</param>
        /// <example>
        /// <inheritdoc cref="IndexPartition" 
        /// path="para[@id='IndexPartitionEx5Intro']"/>
        /// <inheritdoc cref="IndexPartition" 
        /// path="para[@id='IndexPartitionEx5Code']"/>
        /// </example>
        /// <returns>The partition of the indexes in the 
        /// specified collection induced by the partitioner.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="elements"/>  is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="partitioner"/> is <b>null</b>.
        /// </exception>
        public static IndexPartition<T> Create<T>(
            IndexCollection elements,
            Func<int, T> partitioner) where T : IComparable<T>
        {
            if (elements is null) {
                throw new ArgumentNullException(nameof(elements));
            }

            if (partitioner is null) {
                throw new ArgumentNullException(nameof(partitioner));
            }

            var distinctPartIdentifiers = new SortedSet<T>();
            var indexLists = new Dictionary<T, List<int>>();

            bool isNotAlreadyPartIdentifier;
            foreach (var element in elements) {

                T part = partitioner(element);
                isNotAlreadyPartIdentifier = distinctPartIdentifiers.Add(part);
                if (isNotAlreadyPartIdentifier) {
                    indexLists.Add(part, new List<int>());
                }

                indexLists[part].Add(element);
            }

            IndexPartition<T> partition = new();

            foreach (var identifer in distinctPartIdentifiers) {
                partition[identifer] = new IndexCollection(
                    indexLists[identifer].ToArray(), false);
                partition.partIndetifiers.Add(identifer);
            }

            return partition;
        }

        /// <summary>
        /// Creates a partition of positions in a collection of elements by 
        /// aggregating those positions occupied by a same element.
        /// </summary>
        /// <typeparam name="T">The type of the 
        /// elements whose positions are to be partitioned.
        /// </typeparam>
        /// <param name="elements">The collection of elements whose
        /// positions are to be partitioned.</param>
        /// <example>
        /// <inheritdoc cref="IndexPartition" 
        /// path="para[@id='IndexPartitionEx3Intro']"/>
        /// <inheritdoc cref="IndexPartition" 
        /// path="para[@id='IndexPartitionEx3Code']"/>
        /// </example>
        /// <returns>The partition of element positions in the 
        /// specified collection.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="elements"/> is <b>null</b>.
        /// </exception>
        public static IndexPartition<T> Create<T>(
            IEnumerable<T> elements) where T : IComparable<T>
        {
            if (elements is null) {
                throw new ArgumentNullException(nameof(elements));
            }

            var distinctElements = new SortedSet<T>();
            var indexLists = new Dictionary<T, List<int>>();

            bool isNotAlreadyInElementSet;
            int i = 0;
            foreach (var element in elements) {

                isNotAlreadyInElementSet = distinctElements.Add(element);
                if (isNotAlreadyInElementSet) {
                    indexLists.Add(element, new List<int>());
                }

                indexLists[element].Add(i++);
            }

            IndexPartition<T> partition = new();

            foreach (var element in distinctElements) {
                partition[element] = new IndexCollection(
                    indexLists[element].ToArray(), false);
                partition.partIndetifiers.Add(element);
            }

            return partition;
        }

        /// <summary>
        /// Creates a partition of positions in a collection of 
        /// <see cref="System.Double"/> elements by 
        /// aggregating those positions occupied by a same element.
        /// </summary>
        /// <param name="elements">The collection of elements whose
        /// positions are to be partitioned.</param>
        /// <returns>The partition of element positions in the 
        /// specified collection.</returns>
        /// <example>
        /// <inheritdoc cref="IndexPartition" 
        /// path="para[@id='IndexPartitionEx0Intro']"/>
        /// <inheritdoc cref="IndexPartition" 
        /// path="para[@id='IndexPartitionEx0Code']"/>
        /// <inheritdoc cref="IndexPartition" 
        /// path="para[@id='IndexPartitionEx2Intro']"/>
        /// <inheritdoc cref="IndexPartition" 
        /// path="para[@id='IndexPartitionEx2Code']"/>
        /// </example>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="elements"/> is <b>null</b>.
        /// </exception>
        public static IndexPartition<double> Create(DoubleMatrix elements)
        {
            if (elements is null) {
                throw new ArgumentNullException(nameof(elements));
            }

            var distinctElements = new SortedSet<double>();
            var indexLists = new Dictionary<double, List<int>>();

            bool isNotAlreadyInElementSet;
            double currentElement;
            for (int i = 0; i < elements.Count; i++) {
                currentElement = elements[i];
                isNotAlreadyInElementSet = distinctElements.Add(currentElement);
                if (isNotAlreadyInElementSet) {
                    indexLists.Add(currentElement, new List<int>());
                }

                indexLists[currentElement].Add(i);
            }

            IndexPartition<double> partition = new();

            foreach (var element in distinctElements) {
                partition[element] = new IndexCollection(
                    indexLists[element].ToArray(), false);
                partition.partIndetifiers.Add(element);
            }

            return partition;
        }

        /// <summary>
        /// Creates a partition of positions in a collection of 
        /// <see cref="DoubleMatrixRow"/> 
        /// elements by 
        /// aggregating those positions occupied by a same element.
        /// </summary>
        /// <param name="elements">The collection of rows whose
        /// positions are to be partitioned.</param>
        /// <example>
        /// <inheritdoc cref="IndexPartition" 
        /// path="para[@id='IndexPartitionEx1Intro']"/>
        /// <inheritdoc cref="IndexPartition" 
        /// path="para[@id='IndexPartitionEx1Code']"/>
        /// </example>
        /// <returns>The partition of row indexes in the 
        /// specified collection.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="elements"/> is <b>null</b>.
        /// </exception>
        public static IndexPartition<DoubleMatrixRow> Create(
            DoubleMatrixRowCollection elements)
        {
            if (elements is null) {
                throw new ArgumentNullException(nameof(elements));
            }

            SortedSet<DoubleMatrixRow> distinctElements =
                new();
            Dictionary<DoubleMatrixRow, List<int>> indexLists =
                new();

            bool isNotAlreadyInElementSet;
            int i = 0;
            foreach (var row in elements) {

                isNotAlreadyInElementSet = distinctElements.Add(row);
                if (isNotAlreadyInElementSet) {
                    indexLists.Add(row, new List<int>());
                }

                indexLists[row].Add(i);
                i++;
            }

            IndexPartition<DoubleMatrixRow> partition =
                new();

            foreach (var element in distinctElements) {
                partition[element] = new IndexCollection(
                    indexLists[element].ToArray(), false);
                partition.partIndetifiers.Add(element);
            }

            return partition;
        }

        #endregion

        #region Partition evaluations

        /// <summary>
        /// Computes the minimum centroid linkage among parts
        /// in the given partition of the specified data.
        /// </summary>
        /// <param name="data">The data whose rows represent the available
        /// observations.</param>
        /// <param name="partition">The data partition to evaluate.</param>
        /// <returns>Returns the minimum value of
        /// <see cref="Distance.CentroidLinkage">CentroidLinkage</see> 
        /// over the pairs of clusters corresponding to parts 
        /// in <paramref name="partition" />.</returns>
        /// <remarks>
        /// <para id='parameterDescription'>
        /// Each column of <paramref name="data"/> is associated to one of 
        /// the variables under study, while
        /// its rows are associated to the individuals. The 
        /// <paramref name="partition"/> is intended to define parts which
        /// contains row indexes valid for <paramref name="data"/>.
        /// </para>
        /// <para>
        /// This method applies Euclidean distances.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/>.<br/>
        /// -or-<br/>
        /// <paramref name="partition"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// A part in <paramref name="partition"/> contains a position 
        /// which is not valid as a row index of <paramref name="data"/>.
        /// </exception>
        public static double MinimumCentroidLinkage(DoubleMatrix data,
            IndexPartition<double> partition)
        {
            if (data is null) {
                throw new ArgumentNullException(nameof(data));
            }

            if (partition is null) {
                throw new ArgumentNullException(nameof(partition));
            }

            double minimumMeanLinkage, currentMeanLinkage;

            DoubleMatrix left, right;

            minimumMeanLinkage = double.PositiveInfinity;
            double[] categories = partition.Identifiers.ToArray();

            int numberOfClusters = categories.Length;
            int numberOfObservations = data.NumberOfRows;

            for (int i = 0; i < numberOfClusters; i++) {
                var currentLeftPart = partition[categories[i]];
                if (i == 0) {
                    if (currentLeftPart.Max >= numberOfObservations) {
                        throw new ArgumentException(
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_INP_PART_CONTAINS_INVALID_INDEX"),
                            nameof(partition));
                    }
                }
                left = data[currentLeftPart, ":"];
                for (int j = i + 1; j < numberOfClusters; j++) {
                    var currentRightPart = partition[categories[j]];
                    if (i == 0) {
                        if (currentRightPart.Max >= numberOfObservations) {
                            throw new ArgumentException(
                                ImplementationServices.GetResourceString(
                                    "STR_EXCEPT_INP_PART_CONTAINS_INVALID_INDEX"),
                                nameof(partition));
                        }
                    }
                    right = data[currentRightPart, ":"];
                    currentMeanLinkage = Distance.CentroidLinkage(left, right);
                    if (currentMeanLinkage < minimumMeanLinkage)
                        minimumMeanLinkage = currentMeanLinkage;
                }
            }

            return minimumMeanLinkage;
        }

        /// <summary>
        /// Computes the Dunn index to assess the quality of a given partition
        /// of the specified data.
        /// </summary>
        /// <param name="data">The data whose rows represent the available
        /// observations.</param>
        /// <param name="partition">The data partition to evaluate.</param>
        /// <remarks>
        /// <inheritdoc cref="MinimumCentroidLinkage" 
        /// path="para[@id='parameterDescription']"/>
        /// <para> 
        /// The Dunn index aims to identify dense and well-separated clusters. 
        /// It is defined as the ratio between the minimal inter-cluster distance to 
        /// maximal intra-cluster distance. 
        /// Since this criterion seeks clusters with high intra-cluster similarity 
        /// and low inter-cluster similarity, clusters with 
        /// high Dunn index are more desirable.
        /// </para>
        /// <para>
        /// This method applies Euclidean distances. The intra-cluster distance is
        /// implemented as the maximal distance between any pair of elements 
        /// in the cluster. The inter-cluster distance is implemented as the 
        /// single linkage, i.e. the shortest distance between pairs of
        /// individuals belonging to different clusters.
        /// </para>
        ///</remarks>
        /// <returns>The Dunn Index for the given data partition.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/>.<br/>
        /// -or-<br/>
        /// <paramref name="partition"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// A part in <paramref name="partition"/> contains a position 
        /// which is not valid as a row index of <paramref name="data"/>.
        /// </exception>
        /// <seealso href="http://en.wikipedia.org/wiki/Dunn_index"/>
        public static double DunnIndex(DoubleMatrix data, IndexPartition<double> partition)
        {
            if (data is null) {
                throw new ArgumentNullException(nameof(data));
            }

            if (partition is null) {
                throw new ArgumentNullException(nameof(partition));
            }

            int numberOfClusters = partition.Count;
            double[] categories = partition.Identifiers.ToArray();

            DoubleMatrix[] clusters = new DoubleMatrix[numberOfClusters];
            double diameter, maxDiameter;

            int numberOfObservations = data.NumberOfRows;

            // Compute clusters, cluster diameters and their maximum
            maxDiameter = Double.NegativeInfinity;
            for (int i = 0; i < numberOfClusters; i++) {
                var currentPart = partition[categories[i]];
                if (currentPart.Max >= numberOfObservations) {
                    throw new ArgumentException(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_INP_PART_CONTAINS_INVALID_INDEX"),
                        nameof(partition));
                }
                clusters[i] = data[currentPart, ":"];
                diameter = Distance.CompleteDiameter(clusters[i]);
                if (maxDiameter < diameter) {
                    maxDiameter = diameter;
                }
            }

            double min = Double.PositiveInfinity;
            double current;

            for (int i = 0; i < numberOfClusters; i++) {

                for (int j = i + 1; j < numberOfClusters; j++) {
                    current = Distance.SingleLinkage(clusters[i], clusters[j]);
                    if (current < min) {
                        min = current;
                    }
                }
            }

            // DI = min { min     { D(i,j) / max { D(k) } } }
            //       i     j, j≠i             k

            double dunnIndex = min / maxDiameter;
            return dunnIndex;
        }

        /// <summary>
        /// Computes the Davies-Bouldin index to assess the quality of a given partition
        /// of the specified data.
        /// </summary>
        /// <remarks>
        /// <inheritdoc cref="MinimumCentroidLinkage" 
        /// path="para[@id='parameterDescription']"/>
        /// <para> 
        /// The Davies-Bouldin index takes on small values for partitions 
        /// with high similarity among observations in each part and 
        /// low similarities among parts.
        /// As a consequence, the best partition is considered the one with 
        /// the smallest Davies–Bouldin index.
        /// </para>
        /// <para>
        /// This method applies Euclidean distances. The intra-cluster distance is
        /// implemented as the centroid diameter, or the average distance 
        /// between the elements 
        /// in the cluster and the cluster centroid. 
        /// The inter-cluster distance is implemented as the 
        /// centroid linkage, i.e. the distance between cluster 
        /// centroids.
        /// </para>
        /// </remarks>
        /// <param name="data">The data whose rows represent the available
        /// observations.</param>
        /// <param name="partition">The data partition to evaluate.</param>
        /// <returns>The Davies Bouldin Index for the given data partition.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="data"/>.<br/>
        /// -or-<br/>
        /// <paramref name="partition"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// A part in <paramref name="partition"/> contains a position 
        /// which is not valid as a row index of <paramref name="data"/>.
        /// </exception>
        /// <seealso href="http://en.wikipedia.org/wiki/Davies%E2%80%93Bouldin_index"/>
        public static double DaviesBouldinIndex(DoubleMatrix data,
            IndexPartition<double> partition)
        {
            if (data is null) {
                throw new ArgumentNullException(nameof(data));
            }

            if (partition is null) {
                throw new ArgumentNullException(nameof(partition));
            }

            int numberOfClusters = partition.Count;
            double[] categories = partition.Identifiers.ToArray();
            double distance;
            int size;

            // Compute cluster centroids, their distance matrix, and, 
            // in each cluster, the average distance to 
            // the corresponding centroid.
            DoubleMatrix centroids = DoubleMatrix.Dense(
                numberOfClusters, data.NumberOfColumns);
            DoubleMatrix averageDistanceFromCentroid =
                DoubleMatrix.Dense(numberOfClusters, 1);

            int numberOfObservations = data.NumberOfRows;

            for (int i = 0; i < numberOfClusters; i++) {
                var currentPart = partition[categories[i]];
                if (currentPart.Max >= numberOfObservations) {
                    throw new ArgumentException(
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_INP_PART_CONTAINS_INVALID_INDEX"),
                        nameof(partition));
                }
                DoubleMatrix cluster = data[currentPart, ":"];
                centroids[i, ":"] = Stat.Mean(cluster, DataOperation.OnColumns);
                size = cluster.NumberOfRows;
                distance = 0.0;

                for (int r = 0; r < size; r++) {
                    distance += Distance.Euclidean(cluster[r, ":"], centroids[i, ":"]);
                }
                averageDistanceFromCentroid[i] = distance / size;
            }

            DoubleMatrix centroidDistances = Distance.Euclidean(centroids);

            // DBI = (1/numberOfIndividuals) Σ  max{ (S(i) + S(j) ) / d(C(i), C(j)) }
            //             i  j≠i

            double daviesBouldinIndex = 0.0;

            double[] max = new double[numberOfClusters];
            double current;

            for (int i = 0; i < numberOfClusters; i++) {
                max[i] = Double.NegativeInfinity;
                for (int j = 0; j < numberOfClusters; j++) {
                    if (j != i) {
                        current = (averageDistanceFromCentroid[i] +
                            averageDistanceFromCentroid[j])
                            / centroidDistances[i, j];
                        if (max[i] < current) {
                            max[i] = current;
                        }
                    }
                }
                daviesBouldinIndex += max[i];
            }

            daviesBouldinIndex /= numberOfClusters;

            return daviesBouldinIndex;
        }

        #endregion
    }

    /// <summary>
    /// Represents a collection of non empty, mutually exclusive subsets
    /// of a range of zero-based indexes. 
    /// </summary> 
    /// <typeparam name="T">The type of part identifiers.</typeparam>
    /// <remarks>
    /// <para>
    /// An index partition is a data structure that has a specific number of
    /// parts. Each part is a collection of zero-based indexes represented by 
    /// an instance of type <see cref="IndexCollection"/>,
    /// and has an 
    /// identifier whose type is the generic parameter <typeparamref name="T"/>. 
    /// </para>
    /// <para>
    /// Index partitions are useful when a collection of 
    /// elements is inspected and its range must be partitioned  
    /// by grouping those positions which share the same element. 
    /// The resulting groups, referred to as parts, have no common indexes, 
    /// and their union includes
    /// all the available positions in the collection of elements.
    /// Let us consider an array of doubles: an example of index partition is a data 
    /// structure with two parts, in which the first part is used
    /// to store the array positions which correspond to positive entries,
    /// and a second part stores the non positive ones.
    /// </para>
    /// <para>
    /// Index partition instances can be created by calling methods 
    /// provided by the <see cref="IndexPartition"/>
    /// static class.
    /// </para>
    /// </remarks>
    /// <seealso cref="IndexPartition"/>
    public sealed class IndexPartition<T>
    {
        internal Dictionary<T, IndexCollection> parts =
            new();

        internal List<T> partIndetifiers =
            new();

        internal IndexPartition()
        {
        }

        /// <summary>
        /// Gets the <see cref="IndexCollection" /> part
        /// corresponding to the specified identifier.
        /// </summary>
        /// <param name="partIdentifier">The part identifier.</param>
        /// <value>The indexes in the identified part.</value>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="partIdentifier"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="partIdentifier"/> is not the identifier of any part
        /// in the <see cref="IndexPartition{T}"/>.
        /// </exception>
        public IndexCollection this[T partIdentifier]
        {
            get
            {
                if (partIdentifier == null) {
                    throw new ArgumentNullException(nameof(partIdentifier));
                }

                if (!this.parts.ContainsKey(partIdentifier)) {
                    throw new ArgumentException(
                        ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_IS_NOT_A_PART_IDENTIFIER"), 
                        nameof(partIdentifier));
                }
                return this.parts[partIdentifier];
            }
            internal set
            {
                this.parts[partIdentifier] = value;
            }
        }

        /// <summary>
        /// Tries the get part associated with the specified identifier.
        /// </summary>
        /// <param name="partIdentifier">The identifier of the part to get.</param>
        /// <param name="part">When this method returns, contains the part associated 
        /// with the specified identifier, if the identifier is found; 
        /// otherwise, the default value for the type of the <paramref name="part"/> parameter. 
        /// This parameter is passed uninitialized.</param>
        /// <returns><c>true</c> if the partition contains a part with the 
        /// specified identifier, <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="partIdentifier"/> is <b>null</b>.
        /// </exception>
        public bool TryGetPart(T partIdentifier, out IndexCollection part)
        {
            return this.parts.TryGetValue(partIdentifier, out part);
        }

        /// <summary>
        /// Gets the collection of part identifiers.
        /// </summary>
        /// <value>The part identifiers.</value>
        public IReadOnlyList<T> Identifiers
        {
            get { return this.partIndetifiers; }

        }

        /// <summary>
        /// Gets the count of the parts.
        /// </summary>
        /// <value>The count of the parts in the partition.</value>
        public int Count { get { return this.partIndetifiers.Count; } }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new();

            foreach (KeyValuePair<T, IndexCollection> pair in this.parts) {
                stringBuilder.Append('[');
                stringBuilder.Append("(");
                stringBuilder.Append(pair.Key);
                stringBuilder.Append("),  ");
                stringBuilder.Append(pair.Value);
                stringBuilder.AppendLine("]");
            }

            return stringBuilder.ToString();
        }
    }
}
