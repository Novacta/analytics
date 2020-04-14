// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Represents the state of a <see cref="DoubleMatrix"/>.
    /// </summary>
    class DoubleMatrixState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleMatrixState" /> class.
        /// </summary>
        /// <param name="asColumnMajorDenseArray">
        /// A column major dense representation of 
        /// the matrix data.</param>
        /// <param name="numberOfRows">The matrix number of rows.</param>
        /// <param name="numberOfColumns">The matrix number of columns.</param>
        /// <param name="name">The name of the matrix.</param>
        /// <param name="rowNames">The matrix row names.</param>
        /// <param name="columnNames">The matrix column names.</param>
        public DoubleMatrixState(
            double[] asColumnMajorDenseArray,
            int numberOfRows,
            int numberOfColumns,
            string name = null,
            Dictionary<int, string> rowNames = null,
            Dictionary<int, string> columnNames = null)
        {
            this.AsColumnMajorDenseArray = asColumnMajorDenseArray;
            this.NumberOfRows = numberOfRows;
            this.NumberOfColumns = numberOfColumns;
            this.Name = name;
            this.RowNames = rowNames;
            this.ColumnNames = columnNames;
        }

        #region State

        /// <summary>
        /// Gets a column major dense representation of 
        /// the matrix data.
        /// </summary>
        /// <value>The column major dense array representing the matrix data.</value>
        public double[] AsColumnMajorDenseArray { get; private set; }

        /// <summary>
        /// Gets the number of rows of the matrix.
        /// </summary>
        public int NumberOfRows { get; private set; }

        /// <summary>
        /// Gets the number of columns of the matrix.
        /// </summary>
        public int NumberOfColumns { get; private set; }

        #region Names

        /// <summary>
        /// Gets the name of this instance.
        /// </summary>
        /// <value>The matrix name.</value>
        public string Name
        { get; private set; }

        /// <summary>
        /// Gets the row names of this instance.
        /// </summary>
        /// <value>The matrix row names.</value>
        public Dictionary<int, string> RowNames
        { get; private set; }

        /// <summary>
        /// Gets the column names of this instance.
        /// </summary>
        /// <value>The matrix column names.</value>
        public Dictionary<int, string> ColumnNames
        { get; private set; }

        /// <summary>
        /// Sets the row names of the specified matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="rowNames">The row names.</param>
        void SetRowNames(DoubleMatrix matrix, Dictionary<int, string> rowNames)
        {
            if (rowNames != null)
            {
                foreach (var pair in rowNames)
                {
                    matrix.SetRowName(pair.Key, pair.Value);
                }
            }
        }

        /// <summary>
        /// Sets the column names of the specified matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="columnNames">The column names.</param>
        void SetColumnNames(DoubleMatrix matrix, Dictionary<int, string> columnNames)
        {
            if (columnNames != null)
            {
                foreach (var pair in columnNames)
                {
                    matrix.SetColumnName(pair.Key, pair.Value);
                }
            }
        }

        #endregion

        #endregion

        #region DoubleMatrix

        /// <summary>
        /// Gets a dense implemented <see cref="DoubleMatrix"/> having
        /// the state specified by this instance.
        /// </summary>
        /// <returns>A dense implemented <see cref="DoubleMatrix"/> having
        /// the state specified by this instance.</returns>
        public virtual DoubleMatrix GetDenseDoubleMatrix()
        {
            var matrix = DoubleMatrix.Dense(
                this.NumberOfRows,
                this.NumberOfColumns,
                this.AsColumnMajorDenseArray);

            matrix.Name = this.Name;
            this.SetRowNames(matrix, this.RowNames);
            this.SetColumnNames(matrix, this.ColumnNames);

            return matrix;
        }

        /// <summary>
        /// Gets a view implemented <see cref="DoubleMatrix"/> having
        /// the state specified by this instance.
        /// </summary>
        /// <returns>A view implemented <see cref="DoubleMatrix"/> having
        /// the state specified by this instance.</returns>
        public virtual DoubleMatrix GetViewDoubleMatrix()
        {
            var matrix = DoubleMatrix.Dense(
                this.NumberOfRows,
                this.NumberOfColumns,
                this.AsColumnMajorDenseArray);

            var subMatrix = matrix[":", ":", avoidDenseAllocations: true];

            subMatrix.Name = this.Name;
            this.SetRowNames(subMatrix, this.RowNames);
            this.SetColumnNames(subMatrix, this.ColumnNames);

            return subMatrix;
        }

        /// <summary>
        /// Gets a sparse implemented <see cref="DoubleMatrix"/> having
        /// the state specified by this instance.
        /// </summary>
        /// <returns>A sparse implemented <see cref="DoubleMatrix"/> having
        /// the state specified by this instance.</returns>
        public virtual DoubleMatrix GetSparseDoubleMatrix()
        {
            var matrix = DoubleMatrix.Sparse(
                this.NumberOfRows,
                this.NumberOfColumns,
                capacity: 0);

            var data = this.AsColumnMajorDenseArray;

            for (int i = 0; i < data.Length; i++)
            {
                matrix[i] = data[i];
            }

            matrix.Name = this.Name;
            this.SetRowNames(matrix, this.RowNames);
            this.SetColumnNames(matrix, this.ColumnNames);

            return matrix;
        }

        #endregion
    }
}
