// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using System;
using System.Diagnostics;
using System.Globalization;

namespace Novacta.Analytics
{
    /// <summary>
    /// Provides methods to summarize distances within and between 
    /// sets of multidimensional points.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Sets of multidimensional points are referred to as <i>clusters</i>.
    /// </para>
    /// <para><b>Distances within a cluster</b></para>
    /// <para>
    /// The Euclidean distances among points in a cluster are returned by 
    /// method <see cref="Euclidean(DoubleMatrix)"/> as a distance matrix.
    /// The complete diameter of a cluster, i.e. the maximal Euclidean distance 
    /// among two cluster points, is instead computed
    /// by method <see cref="CompleteDiameter(DoubleMatrix)"/>.
    /// </para>
    /// <para><b>Distances between clusters</b></para>
    /// <para>
    /// Distances between points belonging to different clusters can be
    /// summarized in terms of <i>linkages</i>.
    /// Method <see cref="SingleLinkage(DoubleMatrix, DoubleMatrix)">SingleLinkage</see> computes
    /// the shortest distance between pairs of
    /// individuals belonging to different clusters, while 
    /// <see cref="CompleteLinkage(DoubleMatrix, DoubleMatrix)">CompleteLinkage</see> 
    /// and <see cref="AverageLinkage(DoubleMatrix, DoubleMatrix)">AverageLinkage</see> 
    /// compute the maximum and the average of those distances, respectively. 
    /// Method <see cref="CentroidLinkage(DoubleMatrix, DoubleMatrix)">CentroidLinkage</see> 
    /// the distance between the mean points (centroids) of the specified clusters.
    /// </para>
    /// </remarks>
    public static class Distance
    {
        /// <summary>
        /// Computes the Euclidean distance matrix of the specified cluster.
        /// </summary>
        /// <param name="cluster">The cluster.</param>
        /// <returns>The Euclidean distance matrix of the specified cluster.</returns>
        /// <remarks>
        /// <para>
        /// A cluster is defined as a set of multidimensional points. 
        /// The rows of <paramref name="cluster"/> are
        /// interpreted as the multidimensional points in the cluster.
        /// </para>
        /// <para>
        /// This method returns the matrix of Euclidean distances
        /// among the points in the specified <paramref name="cluster"/>.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="cluster"/> is <b>null</b>.
        /// </exception>
        public static DoubleMatrix Euclidean(DoubleMatrix cluster)
        {
            if (cluster is null)
                throw new ArgumentNullException(nameof(cluster));

            DoubleMatrix currentDiff, currentSquaredDiff;
            DoubleMatrix distances = DoubleMatrix.Dense(
                cluster.NumberOfRows, cluster.NumberOfRows);
            for (int i = 0; i < cluster.NumberOfRows; i++) {
                for (int j = i + 1; j < cluster.NumberOfRows; j++) {
                    currentDiff = cluster[i, ":"] - cluster[j, ":"];
                    currentSquaredDiff = DoubleMatrix.ElementWiseMultiply(
                        currentDiff, currentDiff);
                    distances[i, j] = Math.Sqrt(Stat.Sum(currentSquaredDiff));
                    distances[j, i] = distances[i, j];
                }
            }

            return distances;
        }

        /// <summary>
        /// Computes the Euclidean distance between the specified multidimensional
        /// points.
        /// </summary>
        /// <param name="left">The left point.</param>
        /// <param name="right">The right point.</param>
        /// <returns>The Euclidean distance between the specified points.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.<br/>
        /// -or- <br/>
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        internal static double Euclidean(DoubleMatrix left, DoubleMatrix right)
        {
            Debug.Assert(left is not null);

            Debug.Assert(right is not null);

            int numberOfItems = left.Count;

            Debug.Assert(numberOfItems == right.Count);

            double distance = 0.0, difference;

            for (int i = 0; i < numberOfItems; i++) {
                difference = left[i] - right[i];
                distance += difference * difference;
            }
            distance = Math.Sqrt(distance);

            return distance;
        }

        /// <summary>
        /// Computes the complete diameter of the specified cluster.
        /// </summary>
        /// <param name="cluster">The cluster whose complete diameter must be computed.</param>
        /// <remarks>
        /// <para>
        /// A cluster is defined as a set of multidimensional points. 
        /// The rows of <paramref name="cluster"/> 
        /// are interpreted as the multidimensional points in the cluster.
        /// </para>
        /// <para>
        /// This method computes the largest Euclidean distance between any two 
        /// cluster points.
        /// </para>
        /// </remarks>
        /// <returns>The complete diameter of the specified cluster.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="cluster"/> is <b>null</b>.
        /// </exception>
        public static double CompleteDiameter(DoubleMatrix cluster)
        {
            if (cluster is null)
                throw new ArgumentNullException(nameof(cluster));

            double diameter = double.NegativeInfinity;

            double currentDistance;
            for (int i = 0; i < cluster.NumberOfRows; i++) {
                for (int j = i + 1; j < cluster.NumberOfRows; j++) {
                    currentDistance = Distance.Euclidean(cluster[i, ":"], cluster[j, ":"]);
                    if (diameter < currentDistance) {
                        diameter = currentDistance;
                    }
                }
            }

            return diameter;
        }


        /// <summary>
        /// Computes the single linkage between the specified clusters.
        /// </summary>
        /// <param name="left">The left cluster.</param>
        /// <param name="right">The right cluster.</param>
        /// <returns>
        /// The single linkage between the specified clusters.
        /// </returns>
        /// <remarks>
        /// <para>
        /// A cluster is defined as a set of multidimensional points. 
        /// The rows of <paramref name="left"/> or <paramref name="right"/> 
        /// are interpreted as the multidimensional points in the cluster.
        /// </para>
        /// <para>
        /// This method returns the shortest Euclidean distance between pairs of
        /// individuals in which one belongs to cluster <paramref name="left"/>,
        /// the other to cluster <paramref name="right"/>.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="right"/> has not the same <see cref="DoubleMatrix.NumberOfColumns"/> of 
        /// <paramref name="left"/>.
        /// </exception>
        public static double SingleLinkage(DoubleMatrix left, DoubleMatrix right)
        {
            Distance.ValidateLinkageInput(left, right);

            double minimumDistance = double.PositiveInfinity;

            double currentDistance;
            for (int i = 0; i < left.NumberOfRows; i++) {
                for (int j = 0; j < right.NumberOfRows; j++) {
                    currentDistance = Distance.Euclidean(left[i, ":"], right[j, ":"]);
                    if (minimumDistance > currentDistance) {
                        minimumDistance = currentDistance;
                    }
                }
            }

            return minimumDistance;
        }


        /// <summary>
        /// Computes the complete linkage between the specified clusters.
        /// </summary>
        /// <param name="left">The left cluster.</param>
        /// <param name="right">The right cluster.</param>
        /// <returns>The complete linkage between the specified clusters.
        /// </returns>
        /// <remarks>
        /// <para>
        /// A cluster is defined as a set of multidimensional points. 
        /// The rows of <paramref name="left"/> or <paramref name="right"/> 
        /// are interpreted as the multidimensional points in the cluster.
        /// </para>
        /// <para>
        /// This method returns the maximum Euclidean distance between pairs of
        /// individuals in which one belongs to cluster <paramref name="left"/>,
        /// the other to cluster <paramref name="right"/>.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="right"/> has not the same <see cref="DoubleMatrix.NumberOfColumns"/> of 
        /// <paramref name="left"/>.
        /// </exception>
        public static double CompleteLinkage(DoubleMatrix left, DoubleMatrix right)
        {
            Distance.ValidateLinkageInput(left, right);

            double maximumDistance = double.NegativeInfinity;

            double currentDistance;
            for (int i = 0; i < left.NumberOfRows; i++) {
                for (int j = 0; j < right.NumberOfRows; j++) {
                    currentDistance = Distance.Euclidean(left[i, ":"], right[j, ":"]);
                    if (maximumDistance < currentDistance) {
                        maximumDistance = currentDistance;
                    }
                }
            }

            return maximumDistance;
        }


        /// <summary>
        /// Computes the average linkage between the specified clusters.
        /// </summary>
        /// <param name="left">The left cluster.</param>
        /// <param name="right">The right cluster.</param>
        /// <returns>The average linkage between the specified clusters.
        /// </returns>
        /// <remarks>
        /// <para>
        /// A cluster is defined as a set of multidimensional points. 
        /// The rows of <paramref name="left"/> or <paramref name="right"/> 
        /// are interpreted as the multidimensional points in the cluster.
        /// </para>
        /// <para>
        /// This method returns the average Euclidean distance between pairs of
        /// individuals in which one belongs to cluster <paramref name="left"/>,
        /// the other to cluster <paramref name="right"/>.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="right"/> has not the same <see cref="DoubleMatrix.NumberOfColumns"/> of 
        /// <paramref name="left"/>.
        /// </exception>
        public static double AverageLinkage(DoubleMatrix left, DoubleMatrix right)
        {
            Distance.ValidateLinkageInput(left, right);

            int leftNumberOfRows = left.NumberOfRows;
            int rightNumberOfRows = right.NumberOfRows;

            double averageDistance = 0.0;
            for (int i = 0; i < leftNumberOfRows; i++) {
                for (int j = 0; j < rightNumberOfRows; j++) {
                    averageDistance += Distance.Euclidean(left[i, ":"], right[j, ":"]);
                }
            }

            averageDistance /= (leftNumberOfRows * rightNumberOfRows);

            return averageDistance;
        }

        /// <summary>
        /// Computes the centroid linkage between the specified clusters.
        /// </summary>
        /// <param name="left">The left cluster.</param>
        /// <param name="right">The right cluster.</param>
        /// <returns>The centroid linkage between the specified clusters.
        /// </returns>
        /// <remarks>
        /// <para>
        /// A cluster is defined as a set of multidimensional points. 
        /// The rows of <paramref name="left"/> or <paramref name="right"/> 
        /// are interpreted as the multidimensional points in the cluster.
        /// </para>
        /// <para>
        /// This method returns the Euclidean distance between the average points
        /// (centroids) of the two specified clusters.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="left"/> is <b>null</b>.<br/>
        /// -or-<br/>
        /// <paramref name="right"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="right"/> has not the same <see cref="DoubleMatrix.NumberOfColumns"/> of 
        /// <paramref name="left"/>.
        /// </exception>
        public static double CentroidLinkage(DoubleMatrix left, DoubleMatrix right)
        {
            Distance.ValidateLinkageInput(left, right);

            DoubleMatrix leftMean = Stat.Mean(left, DataOperation.OnColumns);
            DoubleMatrix rightMean = Stat.Mean(right, DataOperation.OnColumns);

            return Distance.Euclidean(leftMean, rightMean);
        }

        private static void ValidateLinkageInput(DoubleMatrix left, DoubleMatrix right)
        {
            if (left is null)
                throw new ArgumentNullException(nameof(left));

            if (right is null)
                throw new ArgumentNullException(nameof(right));

            if (left.NumberOfColumns != right.NumberOfColumns) {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ImplementationServices.GetResourceString(
                            "STR_EXCEPT_PAR_MUST_HAVE_SAME_NUM_OF_COLUMNS"), 
                        nameof(left)),
                    nameof(right));
            }
        }
    }
}
