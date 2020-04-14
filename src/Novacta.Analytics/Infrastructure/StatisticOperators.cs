// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Infrastructure
{
    static class StatisticOperators
    {
        #region Max

        #region By dimension

        public static MatrixImplementor<double> Dense_MaxByCols(
            MatrixImplementor<double> data,
            out IndexCollection indexes)
        {
            // Called if dimension == 1
            int numberOfRows = data.NumberOfRows;
            int numberOfColumns = data.NumberOfColumns;

            MatrixImplementor<double> result = new 
                DenseDoubleMatrixImplementor(1, numberOfColumns);
            double[] resArray = result.Storage;

            int[] indexesArray = new int[numberOfColumns];

            int j;

            double[] dataArray = data.Storage;

            double guess;
            int i, offset;

            for (j = 0; j < numberOfColumns; j++) {
                offset = j * numberOfRows;
                resArray[j] = dataArray[offset];
                indexesArray[j] = 0;

                for (i = 1; i < numberOfRows; i++) {
                    guess = dataArray[offset + i];

                    if (resArray[j] < guess) {
                        resArray[j] = guess;
                        indexesArray[j] = i;
                    }
                }
            }

            indexes = new IndexCollection(indexesArray, false);

            return result;
        }

        public static MatrixImplementor<double> Sparse_MaxByRows(
            MatrixImplementor<double> data,
            out IndexCollection indexes)
        {
            // Called if dimension == 0
            var sparseData = (SparseCsr3DoubleMatrixImplementor)data;
            int numberOfRows = sparseData.numberOfRows;
            int numberOfColumns = sparseData.numberOfColumns;

            MatrixImplementor<double> result = new 
                DenseDoubleMatrixImplementor(numberOfRows, 1);
            double[] resArray = result.Storage;

            // For each row, get the column index corresponding to the maximum
            // row value. 
            int[] indexesArray = new int[numberOfRows];

            // For each row, get the row index corresponding to the first
            // position which is not stored. It's equal to numberOfColumns if and only if
            // all positions on the given row are stored ones.
            int firstNonStoredPosition = -1; 

            // For each column, get a flag signaling that there exist no positions in a given column
            // which are stored. It's equal to true if and only if
            // all positions on the given column are not stored ones.

            for (int i = 0; i < numberOfRows; i++) {

                // It's equal to -1 if and only if
                // all positions on the given row are not stored ones.
                indexesArray[i] = -1;
            }

            var rowIndex = sparseData.rowIndex;
            var columns = sparseData.columns;
            var values = sparseData.values;

            for (int i = 0; i < numberOfRows; i++) {

                double guess;
                int j, fromInclusive = rowIndex[i], toExclusive = rowIndex[i + 1];
                for (int p = fromInclusive; p < toExclusive; p++) {
                    j = columns[p];
                    guess = values[p];
                    if (indexesArray[i] == -1) {
                        resArray[i] = guess;
                        indexesArray[i] = j;
                        //hasNoStoredPositions[j] = false;
                        firstNonStoredPosition = (j > 0) ? 0 : j + 1;
                    }
                    else {
                        if (resArray[i] < guess) {
                            resArray[i] = guess;
                            indexesArray[i] = j;
                        }

                        if (firstNonStoredPosition == j) {
                            firstNonStoredPosition++;
                        }
                    }
                }

                if (indexesArray[i] < 0) {
                    // Here if and only if row i has no stored positions
                    resArray[i] = 0.0;
                    indexesArray[i] = 0;
                }
                else {
                    // If nonStoredPosition is numberOfColumns, all positions in row i
                    // are stored, hence we don't need to compare the
                    // actual maximum with zero
                    if (firstNonStoredPosition != numberOfColumns) {
                        guess = resArray[i];
                        if (resArray[i] < 0.0) {
                            resArray[i] = 0.0;
                            indexesArray[i] = firstNonStoredPosition;
                        }
                        else {
                            if (guess == 0.0) {
                                int guessIndex = indexesArray[i];
                                if (firstNonStoredPosition < guessIndex)
                                    indexesArray[i] = firstNonStoredPosition;
                            }
                        }
                    }
                }
            };

            indexes = new IndexCollection(indexesArray, false);

            return result;
        }

        public static MatrixImplementor<double> Sparse_MaxByCols(
            MatrixImplementor<double> data,
            out IndexCollection indexes)
        {
            // Called if dimension == 1
            var sparseData = (SparseCsr3DoubleMatrixImplementor)data;
            int numberOfRows = sparseData.numberOfRows;
            int numberOfColumns = sparseData.numberOfColumns;

            MatrixImplementor<double> result = new DenseDoubleMatrixImplementor(1, numberOfColumns);
            double[] resArray = result.Storage;

            // For each column, get the row index corresponding to the maximum
            // column value. 
            int[] indexesArray = new int[numberOfColumns];

            // For each column, get the row index corresponding to the first
            // position which is not stored. It's equal to numberOfRows if and only if
            // all positions on the given column are stored ones.
            int[] firstNonStoredPosition = new int[numberOfColumns];

            for (int j = 0; j < numberOfColumns; j++) {

                // It's equal to -1 if and only if
                // all positions on the given column are not stored ones.
                indexesArray[j] = -1;
            }

            var rowIndex = sparseData.rowIndex;
            var columns = sparseData.columns;
            var values = sparseData.values;

            for (int i = 0; i < numberOfRows; i++) {

                double guess;
                int j;
                for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++) {
                    j = columns[p];
                    guess = values[p];
                    if (indexesArray[j] == -1) {
                        resArray[j] = guess;
                        indexesArray[j] = i;
                        firstNonStoredPosition[j] = (i > 0) ? 0 : i + 1;
                    }
                    else {
                        if (resArray[j] < guess) {
                            resArray[j] = guess;
                            indexesArray[j] = i;
                        }

                        if (firstNonStoredPosition[j] == i) {
                            firstNonStoredPosition[j]++;
                        }
                    }
                }
            };

            int nonStoredPosition;
            for (int j = 0; j < numberOfColumns; j++) {
                if (indexesArray[j] < 0) {
                    // Here if and only if column j has no stored positions
                    resArray[j] = 0.0;
                    indexesArray[j] = 0;
                }
                else {
                    nonStoredPosition = firstNonStoredPosition[j];
                    // If nonStoredPosition is numberOfRows, all positions in column j
                    // are stored, hence we don't need to compare the
                    // actual maximum with zero
                    if (nonStoredPosition != numberOfRows) {
                        double guess = resArray[j];
                        if (resArray[j] < 0.0) {
                            resArray[j] = 0.0;
                            indexesArray[j] = nonStoredPosition;
                        }
                        else {
                            if (guess == 0.0) {
                                int guessIndex = indexesArray[j];
                                if (nonStoredPosition < guessIndex)
                                    indexesArray[j] = nonStoredPosition;
                            }
                        }
                    }
                }
            }

            indexes = new IndexCollection(indexesArray, false);

            return result;
        }

        public static MatrixImplementor<double> View_MaxByCols(
            MatrixImplementor<double> data,
            out IndexCollection indexes)
        {
            ViewDoubleMatrixImplementor subImplementor = (ViewDoubleMatrixImplementor)(data);
            IndexCollection[] subIndexes = subImplementor.GetReferredImplementor(out MatrixImplementor<double> parentImplementor);

            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] colSubIndexes = subIndexes[1].indexes;
            int numberOfRows = rowSubIndexes.Length;
            int numberOfColumns = colSubIndexes.Length;

            var result = new DenseDoubleMatrixImplementor(1, numberOfColumns);
            double[] resultArray = result.Storage;

            int[] indexesArray = new int[numberOfColumns];

            int j;

            double guess;
            int leadingDim, refIndex;
            int i;

            switch (parentImplementor.StorageScheme) {
                case StorageScheme.Dense: {
                        double[] refArray = parentImplementor.Storage;

                        leadingDim = parentImplementor.NumberOfRows;

                        for (j = 0; j < numberOfColumns; j++) {
                            refIndex = leadingDim * colSubIndexes[j];
                            resultArray[j] = refArray[refIndex];
                            indexesArray[j] = 0;

                            for (i = 1; i < numberOfRows; i++) {
                                guess = refArray[refIndex + i];

                                if (resultArray[j] < guess) {
                                    resultArray[j] = guess;
                                    indexesArray[j] = i;
                                }
                            }
                        }
                    }
                    break;
            }

            indexes = new IndexCollection(indexesArray, false);

            return result;
        }

        public static MatrixImplementor<double> Dense_MaxByRows(
            MatrixImplementor<double> data,
            out IndexCollection indexes)
        {
            int numberOfRows = data.NumberOfRows;
            int numberOfColumns = data.NumberOfColumns;

            MatrixImplementor<double> result = new DenseDoubleMatrixImplementor(numberOfRows, 1);
            double[] resArray = result.Storage;

            int[] indexesArray = new int[numberOfRows];

            int i;

            double[] dataArray = data.Storage;

            double guess;
            int j, offset;

            for (i = 0; i < numberOfRows; i++) {
                resArray[i] = dataArray[i];
                indexesArray[i] = 0;
            }

            for (j = 1; j < numberOfColumns; j++) {
                offset = j * numberOfRows;

                for (i = 0; i < numberOfRows; i++) {
                    guess = dataArray[offset + i];

                    if (resArray[i] < guess) {
                        resArray[i] = guess;
                        indexesArray[i] = j;
                    }
                }
            }

            indexes = new IndexCollection(indexesArray, false);

            return result;
        }

        public static MatrixImplementor<double> View_MaxByRows(
            MatrixImplementor<double> data,
            out IndexCollection indexes)
        {
            ViewDoubleMatrixImplementor subImplementor = 
                (ViewDoubleMatrixImplementor)(data);
            IndexCollection[] subIndexes =
                subImplementor.GetReferredImplementor(out MatrixImplementor<double> parentImplementor);

            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] colSubIndexes = subIndexes[1].indexes;
            int numberOfRows = rowSubIndexes.Length;
            int numberOfColumns = colSubIndexes.Length;

            var result = new 
                DenseDoubleMatrixImplementor(numberOfRows, 1);
            double[] resultArray = result.Storage;

            int[] indexesArray = new int[numberOfRows];

            int i;

            int leadingDim, refIndex;
            int j;
            double guess;

            switch (parentImplementor.StorageScheme) {
                case StorageScheme.Dense: {
                        double[] refArray = parentImplementor.Storage;

                        leadingDim = parentImplementor.NumberOfRows;

                        for (i = 0; i < numberOfRows; i++) {
                            resultArray[i] = refArray[i];
                            indexesArray[i] = 0;
                        }

                        for (j = 1; j < numberOfColumns; j++) {
                            refIndex = leadingDim * colSubIndexes[j];

                            for (i = 0; i < numberOfRows; i++) {
                                guess = refArray[rowSubIndexes[i] + refIndex];

                                if (resultArray[i] < guess) {
                                    resultArray[i] = guess;
                                    indexesArray[i] = j;
                                }
                            }

                        }
                    }
                    break;
            }

            indexes = new IndexCollection(indexesArray, false);

            return result;
        }

        public static MatrixImplementor<double> Sparse_Max(
            MatrixImplementor<double> data,
            out IndexCollection indexes,
            int dimension)
        {
            MatrixImplementor<double> result;

            if (0 == dimension)
                result = Sparse_MaxByRows(data, out indexes);
            else
                result = Sparse_MaxByCols(data, out indexes);

            return result;
        }

        public static MatrixImplementor<double> Dense_Max(
            MatrixImplementor<double> data,
            out IndexCollection indexes,
            int dimension)
        {
            MatrixImplementor<double> result;

            if (0 == dimension)
                result = Dense_MaxByRows(data, out indexes);
            else
                result = Dense_MaxByCols(data, out indexes);

            return result;
        }


        public static MatrixImplementor<double> View_Max(
            MatrixImplementor<double> data,
            out IndexCollection indexes,
            int dimension)
        {
            MatrixImplementor<double> result;

            if (0 == dimension)
                result = View_MaxByRows(data, out indexes);
            else
                result = View_MaxByCols(data, out indexes);

            return result;
        }

        #endregion

        #region All

        public static double Dense_Max(
            MatrixImplementor<double> data,
            out int index)
        {
            double result;

            double[] dataArray = data.Storage;
            result = dataArray[0];
            index = 0;
            double guess;

            for (int i = 1; i < dataArray.Length; i++) {
                guess = dataArray[i];
                if (result < guess) {
                    result = guess;
                    index = i;
                }
            }

            return result;
        }

        public static double Sparse_Max(
            MatrixImplementor<double> data,
            out int index)
        {
            // Called if dimension == 0
            var sparseData = (SparseCsr3DoubleMatrixImplementor)data;
            int numberOfRows = sparseData.numberOfRows;
            int numberOfColumns = sparseData.numberOfColumns;

            double result;

            var rowIndex = sparseData.rowIndex;
            var columns = sparseData.columns;
            var values = sparseData.values;
            int numberOfStoredPositions = rowIndex[numberOfRows];

            if (numberOfStoredPositions == 0) {
                index = 0;
                return 0.0;
            }

            int j, firstNonStoredPosition = -1;
            if (numberOfStoredPositions < sparseData.Count) {

                // Find the minimum column index for a stored position
                int minimumColumn = columns[0];
                for (int p = 1; p < numberOfStoredPositions; p++) {
                    j = columns[p];
                    if (j < minimumColumn) {
                        minimumColumn = j;
                    }
                }

                if (0 < minimumColumn) {
                    firstNonStoredPosition = 0;
                }
                else {
                    bool firstPositionDetected = false;
                    for (j = 0; j < numberOfColumns; j++) {
                        for (int i = 0; i < numberOfRows; i++) {
                            if (!sparseData.TryGetPosition(i, j, out _)) {
                                firstNonStoredPosition = i + j * numberOfRows;
                                firstPositionDetected = true;
                                break;
                            }
                        }
                        if (firstPositionDetected) {
                            break;
                        }
                    }
                }
            }

            int jMin = -1;
            result = Double.NaN;
            index = -1;
            double guess;
            bool isFirstValue = true;
            for (int i = 0; i < numberOfRows; i++) {
                for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++) {
                    j = columns[p];
                    guess = values[p];
                    if (isFirstValue) {
                        jMin = j;
                        result = guess;
                        index = i + j * numberOfRows;
                        isFirstValue = false;
                    }
                    else {
                        if (result < guess) {
                            jMin = j;
                            result = guess;
                            index = i + j * numberOfRows;
                        }
                        else {
                            if (result == guess) {
                                if (j < jMin) {
                                    jMin = j;
                                    index = i + j * numberOfRows;
                                }
                            }
                        }

                    }
                }
            }

            // If firstNonStoredPosition is -1, all positions in the matrix
            // are stored, hence we don't need to compare the
            // actual maximum with zero
            if (firstNonStoredPosition != -1) {
                if (result < 0.0) {
                    result = 0.0;
                    index = firstNonStoredPosition;
                }
                else {
                    if (result == 0.0) {
                        int guessIndex = index;
                        if (firstNonStoredPosition < guessIndex)
                            index = firstNonStoredPosition;
                    }
                }
            }

            return result;
        }

        public static double View_Max(
            MatrixImplementor<double> data,
            out int index)
        {
            double result;

            double guess;

            ViewDoubleMatrixImplementor subImplementor = (ViewDoubleMatrixImplementor)(data);
            IndexCollection[] subIndexes = subImplementor.GetReferredImplementor(out MatrixImplementor<double> parentImplementor);

            int l = 0;
            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] colSubIndexes = subIndexes[1].indexes;
            int numberOfRows = rowSubIndexes.Length;
            int numberOfColumns = colSubIndexes.Length;

            int leadingDim, refIndex;
            index = 0;
            result = 0.0;
            switch (parentImplementor.StorageScheme) {
                case StorageScheme.Dense: {
                        double[] RefArray = parentImplementor.Storage;
                        result = RefArray[0];
                        leadingDim = parentImplementor.NumberOfRows;

                        for (int j = 0; j < numberOfColumns; j++) {
                            refIndex = leadingDim * colSubIndexes[j];

                            for (int i = 0; i < numberOfRows; i++, l++) {
                                guess = RefArray[rowSubIndexes[i] + refIndex];
                                if (result < guess) {
                                    result = guess;
                                    index = l;
                                }
                            }
                        }
                        break;
                    }
            }

            return result;
        }

        #endregion

        #endregion

        #region Min

        #region By dimension

        public static MatrixImplementor<double> Dense_MinByCols(
            MatrixImplementor<double> data,
            out IndexCollection indexes)
        {
            int numerOfRows = data.NumberOfRows;
            int numberOfColumns = data.NumberOfColumns;

            var result = new 
                DenseDoubleMatrixImplementor(1, numberOfColumns);
            double[] resultArray = result.Storage;

            int[] indexesArray = new int[numberOfColumns];

            int j;

            double[] dataArray = data.Storage;

            double guess;
            int i, refIndex;

            for (j = 0; j < numberOfColumns; j++) {
                refIndex = j * numerOfRows;
                resultArray[j] = dataArray[refIndex];
                indexesArray[j] = 0;

                for (i = 1; i < numerOfRows; i++) {
                    guess = dataArray[refIndex + i];

                    if (resultArray[j] > guess) {
                        resultArray[j] = guess;
                        indexesArray[j] = i;
                    }
                }
            }

            indexes = new IndexCollection(indexesArray, false);

            return result;
        }

        public static MatrixImplementor<double> Sparse_MinByCols(
            MatrixImplementor<double> data,
            out IndexCollection indexes)
        {
            // Called if dimension == 1
            var sparseData = (SparseCsr3DoubleMatrixImplementor)data;
            int numberOfRows = sparseData.numberOfRows;
            int numberOfColumns = sparseData.numberOfColumns;

            MatrixImplementor<double> result = new 
                DenseDoubleMatrixImplementor(1, numberOfColumns);
            double[] resArray = result.Storage;

            // For each column, get the row index corresponding to the maximum
            // column value. 
            int[] indexesArray = new int[numberOfColumns];

            // For each column, get the row index corresponding to the first
            // position which is not stored. It's equal to numberOfRows if and only if
            // all positions on the given column are stored ones.
            int[] firstNonStoredPosition = new int[numberOfColumns];

            for (int j = 0; j < numberOfColumns; j++) {

                // It's equal to -1 if and only if
                // all positions on the given column are not stored ones.
                indexesArray[j] = -1;
            }

            var rowIndex = sparseData.rowIndex;
            var columns = sparseData.columns;
            var values = sparseData.values;

            for (int i = 0; i < numberOfRows; i++) {

                double guess;
                int j;
                for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++) {
                    j = columns[p];
                    guess = values[p];
                    if (indexesArray[j] == -1) {
                        resArray[j] = guess;
                        indexesArray[j] = i;
                        firstNonStoredPosition[j] = (i > 0) ? 0 : i + 1;
                    }
                    else {
                        if (resArray[j] > guess) {
                            resArray[j] = guess;
                            indexesArray[j] = i;
                        }

                        if (firstNonStoredPosition[j] == i) {
                            firstNonStoredPosition[j]++;
                        }
                    }
                }
            };

            int nonStoredPosition;
            for (int j = 0; j < numberOfColumns; j++) {
                if (indexesArray[j] < 0) {
                    // Here if and only if column j has no stored positions
                    resArray[j] = 0.0;
                    indexesArray[j] = 0;
                }
                else {
                    nonStoredPosition = firstNonStoredPosition[j];
                    // If nonStoredPosition is numberOfRows, all positions in column j
                    // are stored, hence we don't need to compare the
                    // actual maximum with zero
                    if (nonStoredPosition != numberOfRows) {
                        double guess = resArray[j];
                        if (resArray[j] > 0.0) {
                            resArray[j] = 0.0;
                            indexesArray[j] = nonStoredPosition;
                        }
                        else {
                            if (guess == 0.0) {
                                int guessIndex = indexesArray[j];
                                if (nonStoredPosition < guessIndex)
                                    indexesArray[j] = nonStoredPosition;
                            }
                        }
                    }
                }
            }

            indexes = new IndexCollection(indexesArray, false);

            return result;
        }

        public static MatrixImplementor<double> View_MinByCols(
            MatrixImplementor<double> data,
            out IndexCollection indexes)
        {
            ViewDoubleMatrixImplementor subImplementor = 
                (ViewDoubleMatrixImplementor)(data);
            IndexCollection[] subIndexes =
                subImplementor.GetReferredImplementor(out MatrixImplementor<double> parentImplementor);

            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] colSubIndexes = subIndexes[1].indexes;
            int numberOfRows = rowSubIndexes.Length;
            int numberOfColumns = colSubIndexes.Length;

            var result = new DenseDoubleMatrixImplementor(1, numberOfColumns);
            double[] resultArray = result.Storage;

            int[] indexesArray = new int[numberOfColumns];

            int j;

            double guess;
            int leadingDim, refIndex;
            int i;

            switch (parentImplementor.StorageScheme) {
                case StorageScheme.Dense: {
                        double[] refArray = parentImplementor.Storage;

                        leadingDim = parentImplementor.NumberOfRows;

                        for (j = 0; j < numberOfColumns; j++) {
                            refIndex = leadingDim * colSubIndexes[j];
                            resultArray[j] = refArray[refIndex];
                            indexesArray[j] = 0;

                            for (i = 1; i < numberOfRows; i++) {
                                guess = refArray[refIndex + i];

                                if (resultArray[j] > guess) {
                                    resultArray[j] = guess;
                                    indexesArray[j] = i;
                                }
                            }
                        }
                    }
                    break;
            }

            indexes = new IndexCollection(indexesArray, false);

            return result;
        }

        public static MatrixImplementor<double> Dense_MinByRows(
            MatrixImplementor<double> data,
            out IndexCollection indexes)
        {
            int numberOfRows = data.NumberOfRows;
            int numberOfColumns = data.NumberOfColumns;

            var result = new 
                DenseDoubleMatrixImplementor(numberOfRows, 1);
            double[] resultArray = result.Storage;

            int[] indexesArray = new int[numberOfRows];

            int i;

            double[] dataArray = data.Storage;

            double guess;
            int j, refIndex;

            for (i = 0; i < numberOfRows; i++) {
                resultArray[i] = dataArray[i];
                indexesArray[i] = 0;
            }

            for (j = 1; j < numberOfColumns; j++) {
                refIndex = j * numberOfRows;

                for (i = 0; i < numberOfRows; i++) {
                    guess = dataArray[refIndex + i];

                    if (resultArray[i] > guess) {
                        resultArray[i] = guess;
                        indexesArray[i] = j;
                    }
                }
            }

            indexes = new IndexCollection(indexesArray, false);

            return result;
        }

        public static MatrixImplementor<double> Sparse_MinByRows(
            MatrixImplementor<double> data,
            out IndexCollection indexes)
        {
            // Called if dimension == 0
            var sparseData = (SparseCsr3DoubleMatrixImplementor)data;
            int numberOfRows = sparseData.numberOfRows;
            int numberOfColumns = sparseData.numberOfColumns;

            MatrixImplementor<double> result = new 
                DenseDoubleMatrixImplementor(numberOfRows, 1);
            double[] resArray = result.Storage;

            // For each row, get the column index corresponding to the maximum
            // row value. 
            int[] indexesArray = new int[numberOfRows];

            // For each row, get the row index corresponding to the first
            // position which is not stored. It's equal to numberOfColumns if and only if
            // all positions on the given row are stored ones.
            int firstNonStoredPosition = -1;

            for (int i = 0; i < numberOfRows; i++) {

                // It's equal to -1 if and only if
                // all positions on the given row are not stored ones.
                indexesArray[i] = -1;
            }

            var rowIndex = sparseData.rowIndex;
            var columns = sparseData.columns;
            var values = sparseData.values;

            for (int i = 0; i < numberOfRows; i++) {

                double guess;
                int j;
                for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++) {
                    j = columns[p];
                    guess = values[p];
                    if (indexesArray[i] == -1) {
                        resArray[i] = guess;
                        indexesArray[i] = j;
                        firstNonStoredPosition = (j > 0) ? 0 : j + 1;
                    }
                    else {
                        if (resArray[i] > guess) {
                            resArray[i] = guess;
                            indexesArray[i] = j;
                        }

                        if (firstNonStoredPosition == j) {
                            firstNonStoredPosition++;
                        }
                    }
                }

                if (indexesArray[i] < 0) {
                    // Here if and only if row i has no stored positions
                    resArray[i] = 0.0;
                    indexesArray[i] = 0;
                }
                else {
                    // If nonStoredPosition is numberOfColumns, all positions in row i
                    // are stored, hence we don't need to compare the
                    // actual maximum with zero
                    if (firstNonStoredPosition != numberOfColumns) {
                        guess = resArray[i];
                        if (resArray[i] > 0.0) {
                            resArray[i] = 0.0;
                            indexesArray[i] = firstNonStoredPosition;
                        }
                        else {
                            if (guess == 0.0) {
                                int guessIndex = indexesArray[i];
                                if (firstNonStoredPosition < guessIndex)
                                    indexesArray[i] = firstNonStoredPosition;
                            }
                        }
                    }
                }
            };

            indexes = new IndexCollection(indexesArray, false);

            return result;
        }

        public static MatrixImplementor<double> View_MinByRows(
            MatrixImplementor<double> data,
            out IndexCollection indexes)
        {
            ViewDoubleMatrixImplementor subImplementor = 
                (ViewDoubleMatrixImplementor)(data);
            IndexCollection[] subIndexes =
                subImplementor.GetReferredImplementor(out MatrixImplementor<double> parentImplementor);

            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] colSubIndexes = subIndexes[1].indexes;
            int numberOfRows = rowSubIndexes.Length;
            int numberOfColumns = colSubIndexes.Length;

            var result = new 
                DenseDoubleMatrixImplementor(numberOfRows, 1);
            double[] resultArray = result.Storage;

            int[] indexesArray = new int[numberOfRows];

            int i;

            int leadingDim, refIndex;
            int j;
            double guess;

            switch (parentImplementor.StorageScheme) {
                case StorageScheme.Dense: {
                        double[] refArray = parentImplementor.Storage;

                        leadingDim = parentImplementor.NumberOfRows;

                        for (i = 0; i < numberOfRows; i++) {
                            resultArray[i] = refArray[i];
                            indexesArray[i] = 0;
                        }

                        for (j = 1; j < numberOfColumns; j++) {
                            refIndex = leadingDim * colSubIndexes[j];

                            for (i = 0; i < numberOfRows; i++) {
                                guess = refArray[rowSubIndexes[i] + refIndex];

                                if (resultArray[i] > guess) {
                                    resultArray[i] = guess;
                                    indexesArray[i] = j;
                                }
                            }

                        }
                        break;
                    }
            }

            indexes = new IndexCollection(indexesArray, false);

            return result;
        }

        public static MatrixImplementor<double> Dense_Min(
            MatrixImplementor<double> data,
            out IndexCollection indexes,
            int dimension)
        {
            MatrixImplementor<double> result;

            if (0 == dimension)
                result = Dense_MinByRows(data, out indexes);
            else
                result = Dense_MinByCols(data, out indexes);

            return result;
        }

        public static MatrixImplementor<double> Sparse_Min(
            MatrixImplementor<double> data,
            out IndexCollection indexes,
            int dimension)
        {
            MatrixImplementor<double> result;

            if (0 == dimension)
                result = Sparse_MinByRows(data, out indexes);
            else
                result = Sparse_MinByCols(data, out indexes);

            return result;
        }

        public static MatrixImplementor<double> Vew_Min(
            MatrixImplementor<double> data,
            out IndexCollection indexes,
            int dimension)
        {
            MatrixImplementor<double> result;

            if (0 == dimension)
                result = View_MinByRows(data, out indexes);
            else
                result = View_MinByCols(data, out indexes);

            return result;
        }

        #endregion

        #region All

        public static double Dense_Min(
            MatrixImplementor<double> data,
            out int index)
        {
            double result;

            double[] dataArray = data.Storage;
            result = dataArray[0];
            index = 0;
            double guess;
            int i;
            int count = dataArray.Length;

            for (i = 1; i < count; i++) {
                guess = dataArray[i];
                if (result > guess) {
                    result = guess;
                    index = i;
                }
            }

            return result;
        }

        public static double Sparse_Min(
            MatrixImplementor<double> data,
            out int index)
        {
            var sparseData = (SparseCsr3DoubleMatrixImplementor)data;
            int numberOfRows = sparseData.numberOfRows;
            int numberOfColumns = sparseData.numberOfColumns;

            double result;

            var rowIndex = sparseData.rowIndex;
            var columns = sparseData.columns;
            var values = sparseData.values;
            int numberOfStoredPositions = rowIndex[numberOfRows];

            if (numberOfStoredPositions == 0) {
                index = 0;
                return 0.0;
            }

            int j, firstNonStoredPosition = -1;
            if (numberOfStoredPositions < sparseData.Count) {

                // Find the minimum column index for a stored position
                int minimumColumn = columns[0];
                for (int p = 1; p < numberOfStoredPositions; p++) {
                    j = columns[p];
                    if (j < minimumColumn) {
                        minimumColumn = j;
                    }
                }

                if (0 < minimumColumn) {
                    firstNonStoredPosition = 0;
                }
                else {
                    bool firstPositionDetected = false;
                    for (j = 0; j < numberOfColumns; j++) {
                        for (int i = 0; i < numberOfRows; i++) {
                            if (!sparseData.TryGetPosition(i, j, out _)) {
                                firstNonStoredPosition = i + j * numberOfRows;
                                firstPositionDetected = true;
                                break;
                            }
                        }
                        if (firstPositionDetected) {
                            break;
                        }
                    }
                }
            }

            int jMin = -1;
            result = Double.NaN;
            index = -1;
            double guess;
            bool isFirstValue = true;
            for (int i = 0; i < numberOfRows; i++) {
                for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++) {
                    j = columns[p];
                    guess = values[p];
                    if (isFirstValue) {
                        jMin = j;
                        result = guess;
                        index = i + j * numberOfRows;
                        isFirstValue = false;
                    }
                    else {
                        if (result > guess) {
                            jMin = j;
                            result = guess;
                            index = i + j * numberOfRows;
                        }
                        else {
                            if (result == guess) {
                                if (j < jMin) {
                                    jMin = j;
                                    index = i + j * numberOfRows;
                                }
                            }
                        }
                    }
                }
            }

            // If firstNonStoredPosition is -1, all positions in the matrix
            // are stored, hence we don't need to compare the
            // actual maximum with zero
            if (firstNonStoredPosition != -1) {
                if (result > 0.0) {
                    result = 0.0;
                    index = firstNonStoredPosition;
                }
                else {
                    if (result == 0.0) {
                        int guessIndex = index;
                        if (firstNonStoredPosition < guessIndex)
                            index = firstNonStoredPosition;
                    }
                }
            }

            return result;
        }

        public static double View_Min(
            MatrixImplementor<double> data,
            out int index)
        {
            double result;

            double guess;

            ViewDoubleMatrixImplementor subImplementor = (ViewDoubleMatrixImplementor)(data);
            IndexCollection[] subIndexes = subImplementor.GetReferredImplementor(out MatrixImplementor<double> parentImplementor);

            int l = 0;
            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] colSubIndexes = subIndexes[1].indexes;
            int numberOfRows = rowSubIndexes.Length;
            int numberOfColumns = colSubIndexes.Length;

            int leadingDim, refIndex;
            index = 0;
            result = 0.0;
            switch (parentImplementor.StorageScheme) {
                case StorageScheme.Dense: {
                        double[] refArray = parentImplementor.Storage;
                        result = refArray[0];


                        leadingDim = parentImplementor.NumberOfRows;

                        for (int j = 0; j < numberOfColumns; j++) {
                            refIndex = leadingDim * colSubIndexes[j];

                            for (int i = 0; i < numberOfRows; i++, l++) {
                                guess = refArray[rowSubIndexes[i] + refIndex];
                                if (result > guess) {
                                    result = guess;
                                    index = l;
                                }
                            }
                        }
                    }
                    break;
            }

            return result;
        }

        #endregion

        #endregion

        #region Sum

        #region Sparse

        public static MatrixImplementor<double> Sparse_SumByCols(
            MatrixImplementor<double> data)
        {
            var sparseData = (SparseCsr3DoubleMatrixImplementor)data;
            int numberOfRows = sparseData.numberOfRows;
            int numberOfColumns = sparseData.numberOfColumns;

            var result = new DenseDoubleMatrixImplementor(1, numberOfColumns);
            double[] resArray = result.Storage;

            var rowIndex = sparseData.rowIndex;
            var columns = sparseData.columns;
            var values = sparseData.values;
            int numberOfStoredPositions = rowIndex[numberOfRows];

            if (numberOfStoredPositions == 0) {
                return result;
            }

            int j;
            for (int i = 0; i < numberOfRows; i++) {
                for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++) {
                    j = columns[p];
                    resArray[j] += values[p];
                }
            }

            return result;
        }

        public static MatrixImplementor<double> Sparse_SumByRows(
            MatrixImplementor<double> data)
        {
            var sparseData = (SparseCsr3DoubleMatrixImplementor)data;
            int numberOfRows = sparseData.numberOfRows;

            MatrixImplementor<double> result = new DenseDoubleMatrixImplementor(numberOfRows, 1);
            double[] resArray = result.Storage;

            var rowIndex = sparseData.rowIndex;
            var values = sparseData.values;
            int numberOfStoredPositions = rowIndex[numberOfRows];

            if (numberOfStoredPositions == 0) {
                return result;
            }

            for (int i = 0; i < numberOfRows; i++) {
                for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++) {
                    resArray[i] += values[p];
                }
            }

            return result;
        }

        public static MatrixImplementor<double> Sparse_Sum(
            MatrixImplementor<double> data,
            int dimension)
        {
            MatrixImplementor<double> result;

            if (0 == dimension)
                result = Sparse_SumByRows(data);
            else
                result = Sparse_SumByCols(data);

            return result;
        }

        public static double Sparse_Sum(
            MatrixImplementor<double> data)
        {
            var sparseData = (SparseCsr3DoubleMatrixImplementor)data;

            double result = 0.0;

            var rowIndex = sparseData.rowIndex;
            var values = sparseData.values;
            int numberOfStoredPositions = rowIndex[sparseData.numberOfRows];

            for (int p = 0; p < numberOfStoredPositions; p++) {
                result += values[p];
            }

            return result;
        }

        #endregion

        #region Dense

        public static MatrixImplementor<double> Dense_SumByCols(
            MatrixImplementor<double> data)
        {
            int numberOfRows = data.NumberOfRows;
            int numberOfColumns = data.NumberOfColumns;

            MatrixImplementor<double> result = new DenseDoubleMatrixImplementor(1, numberOfColumns);
            double[] resArray = result.Storage;

            double[] dataArray = data.Storage;

            int i, j, offset;

            for (j = 0; j < numberOfColumns; j++) {
                offset = j * numberOfRows;
                for (i = 0; i < numberOfRows; i++)
                    resArray[j] += dataArray[offset + i];
            }

            return result;
        }

        public static MatrixImplementor<double> Dense_SumByRows(
            MatrixImplementor<double> data)
        {
            int numberOfRows = data.NumberOfRows;
            int numberOfColumns = data.NumberOfColumns;

            MatrixImplementor<double> result = new DenseDoubleMatrixImplementor(numberOfRows, 1);
            double[] resArray = result.Storage;

            double[] dataArray = data.Storage;

            int i, j, offset;

            for (j = 0; j < numberOfColumns; j++) {
                offset = j * numberOfRows;
                for (i = 0; i < numberOfRows; i++)
                    resArray[i] += dataArray[offset + i];
            }

            return result;
        }

        public static MatrixImplementor<double> Dense_Sum(
            MatrixImplementor<double> data,
            int dimension)
        {
            MatrixImplementor<double> result;

            if (0 == dimension)
                result = Dense_SumByRows(data);
            else
                result = Dense_SumByCols(data);

            return result;
        }

        public static double Dense_Sum(
            MatrixImplementor<double> data)
        {
            double result = 0.0;

            double[] dataArray = data.Storage;

            for (int l = 0; l < dataArray.Length; l++)
                result += dataArray[l];

            return result;
        }

        #endregion

        #region VIEW

        public static MatrixImplementor<double> View_SumByRows(
            MatrixImplementor<double> data)
        {
            ViewDoubleMatrixImplementor subImplementor = (ViewDoubleMatrixImplementor)(data);
            IndexCollection[] subIndexes = subImplementor.GetReferredImplementor(out MatrixImplementor<double> parentImplementor);

            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] colSubIndexes = subIndexes[1].indexes;
            int numberOfRows = rowSubIndexes.Length;
            int numberOfColumns = colSubIndexes.Length;

            var result = new DenseDoubleMatrixImplementor(numberOfRows, 1);
            double[] resultArray = result.Storage;

            int leadingDim, refIndex;

            switch (parentImplementor.StorageScheme) {
                case StorageScheme.Dense: {
                        double[] RefArray = parentImplementor.Storage;

                        leadingDim = parentImplementor.NumberOfRows;

                        for (int j = 0; j < numberOfColumns; j++) {
                            refIndex = leadingDim * colSubIndexes[j];

                            for (int i = 0; i < numberOfRows; i++)
                                resultArray[i] += RefArray[rowSubIndexes[i] + refIndex];
                        }
                    }
                    break;
            }

            return result;
        }

        public static MatrixImplementor<double> View_SumByCols(
            MatrixImplementor<double> data)
        {
            ViewDoubleMatrixImplementor subImplementor = (ViewDoubleMatrixImplementor)(data);
            IndexCollection[] subIndexes = subImplementor.GetReferredImplementor(out MatrixImplementor<double> parentImplementor);

            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] colSubIndexes = subIndexes[1].indexes;
            int numberOfRows = rowSubIndexes.Length;
            int numberOfColumns = colSubIndexes.Length;

            var result = new DenseDoubleMatrixImplementor(1, numberOfColumns);
            double[] resultArray = result.Storage;

            int leadingDim, refIndex;

            switch (parentImplementor.StorageScheme) {
                case StorageScheme.Dense: {
                        double[] refArray = parentImplementor.Storage;

                        leadingDim = parentImplementor.NumberOfRows;

                        for (int j = 0; j < numberOfColumns; j++) {
                            refIndex = leadingDim * colSubIndexes[j];

                            for (int i = 0; i < numberOfRows; i++) //, Index++ )
                                resultArray[j] += refArray[rowSubIndexes[i] + refIndex];
                        }
                    }
                    break;
            }

            return result;
        }

        public static MatrixImplementor<double> View_Sum(
            MatrixImplementor<double> data,
            int dimension)
        {
            MatrixImplementor<double> result;

            if (0 == dimension)
                result = View_SumByRows(data);
            else
                result = View_SumByCols(data);

            return result;
        }

        public static double View_Sum(
            MatrixImplementor<double> data)
        {
            double result = 0.0;

            ViewDoubleMatrixImplementor subImplementor = (ViewDoubleMatrixImplementor)(data);
            IndexCollection[] subIndexes = subImplementor.GetReferredImplementor(out MatrixImplementor<double> parentImplementor);

            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] colSubIndexes = subIndexes[1].indexes;
            int numberOfRows = rowSubIndexes.Length;
            int numberOfColumns = colSubIndexes.Length;

            int leadingDim, refIndex;

            switch (parentImplementor.StorageScheme) {
                case StorageScheme.Dense: {
                        double[] refArray = parentImplementor.Storage;

                        leadingDim = parentImplementor.NumberOfRows;

                        for (int j = 0; j < numberOfColumns; j++) {
                            refIndex = leadingDim * colSubIndexes[j];

                            for (int i = 0; i < numberOfRows; i++)
                                result += refArray[rowSubIndexes[i] + refIndex];
                        }
                    }
                    break;
            }

            return result;
        }

        #endregion

        #endregion

        #region Kurtosis

        #region Sparse

        public static MatrixImplementor<double> Sparse_KurtosisByCols(
            MatrixImplementor<double> data)
        {
            var sparseData = (SparseCsr3DoubleMatrixImplementor)data;

            int numberOfColumns = sparseData.numberOfColumns;

            MatrixImplementor<double> result = new DenseDoubleMatrixImplementor(1, numberOfColumns);
            double[] resArray = result.Storage;

            int numberOfRows = sparseData.numberOfRows;

            int j;

            if (1 == numberOfRows) {
                for (j = 0; j < numberOfColumns; j++)
                    resArray[j] = Double.NaN;

                return result;
            }

            double[] errorArray = new double[numberOfColumns];
            double[] sArray = new double[numberOfColumns];
            double[] m4Array = new double[numberOfColumns];

            MatrixImplementor<double> average = Sparse_Sum(sparseData, 1);
            double[] averageArray = average.Storage;
            for (j = 0; j < numberOfColumns; j++)
                averageArray[j] /= numberOfRows;

            var rowIndex = sparseData.rowIndex;
            var columns = sparseData.columns;
            var values = sparseData.values;

            double d, squaredD;
            int[] numberOfColumnStoredPositions = new int[numberOfColumns];

            for (int i = 0; i < numberOfRows; i++) {
                for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++) {
                    j = columns[p];
                    d = values[p] - averageArray[j];
                    squaredD = d * d;
                    sArray[j] += squaredD;
                    m4Array[j] += squaredD * squaredD;
                    errorArray[j] += d;

                    numberOfColumnStoredPositions[j]++;
                }
            }

            int numberOfColumnUnstoredPositions;

            for (j = 0; j < resArray.Length; j++) {
                numberOfColumnUnstoredPositions = numberOfRows - numberOfColumnStoredPositions[j];
                if (numberOfColumnUnstoredPositions > 0) {
                    d = -averageArray[j];
                    double zeroDeviations = d * numberOfColumnUnstoredPositions;
                    squaredD = d * d;
                    sArray[j] += squaredD * numberOfColumnUnstoredPositions;
                    m4Array[j] += squaredD * d * zeroDeviations;
                    errorArray[j] += zeroDeviations;
                }
                sArray[j] = (sArray[j] + Math.Pow(errorArray[j], 2.0)) / numberOfRows;
                m4Array[j] /= numberOfRows;
                resArray[j] = -3.0 + m4Array[j] / Math.Pow(sArray[j], 2.0);
            }

            return result;
        }

        public static MatrixImplementor<double> Sparse_KurtosisByRows(
            MatrixImplementor<double> data)
        {
            var sparseData = (SparseCsr3DoubleMatrixImplementor)data;

            int numberOfRows = sparseData.numberOfRows;

            MatrixImplementor<double> result = new DenseDoubleMatrixImplementor(numberOfRows, 1);
            double[] resArray = result.Storage;

            int numberOfColumns = sparseData.numberOfColumns;

            if (1 == numberOfColumns) {
                for (int i = 0; i < numberOfRows; i++)
                    resArray[i] = Double.NaN;

                return result;
            }

            double[] errorArray = new double[numberOfRows];
            double[] sArray = new double[numberOfRows];
            double[] m4Array = new double[numberOfRows];

            MatrixImplementor<double> average = Sparse_Sum(sparseData, 0);
            double[] averageArray = average.Storage;

            for (int i = 0; i < numberOfRows; i++)
                averageArray[i] /= numberOfColumns;

            var rowIndex = sparseData.rowIndex;
            var values = sparseData.values;

            double d, squaredD, rowAverage;
            int numberOfRowUnstoredPositions, rowFromInclusive, rowToExclusive;
            for (int i = 0; i < averageArray.Length; i++) {
                rowAverage = averageArray[i];
                rowFromInclusive = rowIndex[i];
                rowToExclusive = rowIndex[i + 1];
                for (int p = rowFromInclusive; p < rowToExclusive; p++) {
                    d = values[p] - rowAverage;
                    squaredD = d * d;
                    sArray[i] += squaredD;
                    m4Array[i] += squaredD * squaredD;
                    errorArray[i] += d;
                }

                numberOfRowUnstoredPositions = numberOfColumns - rowToExclusive + rowFromInclusive;

                if (numberOfRowUnstoredPositions > 0) {
                    d = -rowAverage;
                    squaredD = d * d;
                    sArray[i] += squaredD * numberOfRowUnstoredPositions;
                    m4Array[i] += squaredD * squaredD * numberOfRowUnstoredPositions;
                    errorArray[i] += d * numberOfRowUnstoredPositions;
                }
            }

            for (int i = 0; i < numberOfRows; i++) {
                sArray[i] = (sArray[i] + Math.Pow(errorArray[i], 2.0)) / numberOfColumns;
                m4Array[i] = m4Array[i] / numberOfColumns;
                resArray[i] = -3.0 + m4Array[i] / Math.Pow(sArray[i], 2.0);
            }

            return result;
        }

        public static MatrixImplementor<double> Sparse_Kurtosis(
            MatrixImplementor<double> data,
            int dimension)
        {
            MatrixImplementor<double> result;

            if (0 == dimension)
                result = Sparse_KurtosisByRows(data);
            else
                result = Sparse_KurtosisByCols(data);

            return result;
        }

        public static double Sparse_Kurtosis(
            MatrixImplementor<double> data)
        {
            var sparseData = (SparseCsr3DoubleMatrixImplementor)data;

            int dataLength = sparseData.Count;

            if (1 == dataLength)
                return Double.NaN;

            double average = Sparse_Sum(sparseData) / dataLength;

            double s = 0.0;
            double m4 = 0.0;
            double e = 0.0;
            double d, squaredD;

            var rowIndex = sparseData.rowIndex;
            var values = sparseData.values;
            int numberOfStoredPositions = rowIndex[sparseData.numberOfRows];

            for (int p = 0; p < numberOfStoredPositions; p++) {
                d = values[p] - average;
                squaredD = d * d;
                s += squaredD;
                m4 += squaredD * squaredD;
                e += d;
            }

            int numberOfUnstoredPositions = dataLength - numberOfStoredPositions;

            if (numberOfUnstoredPositions > 0) {
                d = -average;
                double zeroDeviations = d * numberOfUnstoredPositions;
                squaredD = d * d;
                s += squaredD * numberOfUnstoredPositions;
                m4 += squaredD * d * zeroDeviations;
                e += zeroDeviations;
            }

            s += Math.Pow(e, 2.0);
            s /= dataLength;

            double S2 = s * s;

            m4 /= dataLength;

            double result = -3.0 + m4 / S2;
            return result;
        }

        #endregion

        #region Dense

        public static MatrixImplementor<double> Dense_KurtosisByCols(
            MatrixImplementor<double> data)
        {
            int numberOfColumns = data.NumberOfColumns;

            var result = new DenseDoubleMatrixImplementor(1, numberOfColumns);
            double[] resArray = result.Storage;

            int numberOfRows = data.NumberOfRows;
            int j;

            if (1 == numberOfRows) {
                for (j = 0; j < numberOfColumns; j++)
                    resArray[j] = Double.NaN;

                return result;
            }

            double[] dataArray = data.Storage;
            double[] errorArray = new double[numberOfColumns];
            double[] sArray = new double[numberOfColumns];
            double[] m4Array = new double[numberOfColumns];

            MatrixImplementor<double> average = Dense_Sum(data, 1);
            double[] averageArray = average.Storage;
            for (j = 0; j < numberOfColumns; j++)
                averageArray[j] /= numberOfRows;

            int i, offset;
            double d, squaredD;

            for (j = 0; j < numberOfColumns; j++) {
                offset = j * numberOfRows;
                for (i = 0; i < numberOfRows; i++) {
                    d = dataArray[offset + i] - averageArray[j];
                    squaredD = d * d;
                    sArray[j] = sArray[j] + squaredD;
                    m4Array[j] = m4Array[j] + squaredD * squaredD;
                    errorArray[j] = errorArray[j] + d;
                }
                sArray[j] = (sArray[j] + Math.Pow(errorArray[j], 2.0)) / numberOfRows;
                m4Array[j] = m4Array[j] / numberOfRows;
                resArray[j] = -3.0 + m4Array[j] / Math.Pow(sArray[j], 2.0);
            }

            return result;
        }

        public static MatrixImplementor<double> Dense_KurtosisByRows(
            MatrixImplementor<double> data)
        {
            int numberOfRows = data.NumberOfRows;

            var result = new DenseDoubleMatrixImplementor(numberOfRows, 1);
            double[] resArray = result.Storage;

            int numberOfColumns = data.NumberOfColumns;
            int i;

            if (1 == numberOfColumns) {
                for (i = 0; i < numberOfRows; i++)
                    resArray[i] = Double.NaN;

                return result;
            }

            double[] dataArray = data.Storage;
            double[] errorArray = new double[numberOfRows];
            double[] sArray = new double[numberOfRows];
            double[] m4Array = new double[numberOfRows];

            MatrixImplementor<double> average = Dense_Sum(data, 0);
            double[] averageArray = average.Storage;

            for (i = 0; i < numberOfRows; i++)
                averageArray[i] /= numberOfColumns;

            int j, offset;

            double d, squaredD;

            for (j = 0; j < numberOfColumns; j++) {
                offset = j * numberOfRows;
                for (i = 0; i < numberOfRows; i++) {
                    d = dataArray[offset + i] - averageArray[i];
                    squaredD = d * d;
                    sArray[i] = sArray[i] + squaredD;
                    m4Array[i] = m4Array[i] + squaredD * squaredD;
                    errorArray[i] = errorArray[i] + d;
                }
            }

            for (i = 0; i < numberOfRows; i++) {
                sArray[i] = (sArray[i] + Math.Pow(errorArray[i], 2.0)) / numberOfColumns;
                m4Array[i] = m4Array[i] / numberOfColumns;
                resArray[i] = -3.0 + m4Array[i] / Math.Pow(sArray[i], 2.0);
            }

            return result;
        }

        public static MatrixImplementor<double> Dense_Kurtosis(
            MatrixImplementor<double> data,
            int dimension)
        {
            MatrixImplementor<double> result;

            if (0 == dimension)
                result = Dense_KurtosisByRows(data);
            else
                result = Dense_KurtosisByCols(data);

            return result;
        }

        public static double Dense_Kurtosis(
            MatrixImplementor<double> data)
        {
            double[] dataArray = data.Storage;
            int dataLength = dataArray.Length;

            if (1 == dataLength)
                return Double.NaN;

            double average = Dense_Sum(data) / dataLength;

            double s = 0.0;
            double m4 = 0.0;
            double e = 0.0;
            double d, squaredD;

            int j;

            for (j = 0; j < dataLength; j++) {
                d = dataArray[j] - average;
                squaredD = d * d;
                s += squaredD;
                m4 += squaredD * squaredD;
                e += d;
            }

            s += Math.Pow(e, 2.0);
            s /= dataLength;

            double S2 = s * s;

            m4 /= dataLength;

            double result = -3.0 + m4 / S2;
            return result;
        }

        #endregion

        #region View

        public static MatrixImplementor<double> View_KurtosisByCols(
            MatrixImplementor<double> data)
        {
            ViewDoubleMatrixImplementor subImplementor = (ViewDoubleMatrixImplementor)(data);
            IndexCollection[] subIndexes = subImplementor.GetReferredImplementor(out MatrixImplementor<double> parentImplementor);

            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] colSubIndexes = subIndexes[1].indexes;
            int numberOfRows = rowSubIndexes.Length;
            int numberOfColumns = colSubIndexes.Length;

            var result = new DenseDoubleMatrixImplementor(1, numberOfColumns);
            double[] resultArray = result.Storage;

            int j;

            if (1 == numberOfRows) {
                for (j = 0; j < numberOfColumns; j++)
                    resultArray[j] = Double.NaN;

                return result;
            }

            double[] errorArray = new double[numberOfColumns];
            double[] sArray = new double[numberOfColumns];
            double[] m4Array = new double[numberOfColumns];

            MatrixImplementor<double> average = View_Sum(data, 1);
            double[] averageArray = average.Storage;
            for (j = 0; j < numberOfColumns; j++)
                averageArray[j] /= numberOfRows;

            int leadingDim, refIndex;
            int i;

            double d, squaredD;

            switch (parentImplementor.StorageScheme) {
                case StorageScheme.Dense: {
                        double[] refArray = parentImplementor.Storage;

                        leadingDim = parentImplementor.NumberOfRows;

                        for (j = 0; j < numberOfColumns; j++) {
                            refIndex = leadingDim * colSubIndexes[j];
                            for (i = 0; i < numberOfRows; i++) {
                                d = refArray[rowSubIndexes[i] + refIndex] - averageArray[j];
                                squaredD = d * d;
                                sArray[j] = sArray[j] + squaredD;
                                m4Array[j] = m4Array[j] + squaredD * squaredD;
                                errorArray[j] = errorArray[j] + d;
                            }
                            sArray[j] = (sArray[j] + Math.Pow(errorArray[j], 2.0)) / numberOfRows;
                            m4Array[j] = m4Array[j] / numberOfRows;
                            resultArray[j] = -3.0 + m4Array[j] / Math.Pow(sArray[j], 2.0);
                        }
                    }
                    break;
            }

            return result;
        }

        public static MatrixImplementor<double> View_KurtosisByRows(
            MatrixImplementor<double> data)
        {
            ViewDoubleMatrixImplementor subImplementor = (ViewDoubleMatrixImplementor)(data);
            IndexCollection[] subIndexes = subImplementor.GetReferredImplementor(out MatrixImplementor<double> parentImplementor);

            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] colSubIndexes = subIndexes[1].indexes;
            int numberOfRows = rowSubIndexes.Length;
            int numberOfColumns = colSubIndexes.Length;

            var result = new DenseDoubleMatrixImplementor(numberOfRows, 1);
            double[] resultArray = result.Storage;
            int i;

            if (1 == numberOfColumns) {
                for (i = 0; i < numberOfRows; i++)
                    resultArray[i] = Double.NaN;

                return result;
            }

            double[] errorArray = new double[numberOfRows];
            double[] sArray = new double[numberOfRows];
            double[] m4Array = new double[numberOfRows];

            MatrixImplementor<double> average = View_Sum(data, 0);
            double[] averageArray = average.Storage;
            for (i = 0; i < numberOfRows; i++)
                averageArray[i] /= numberOfColumns;

            int leadingDim, refIndex;
            int j;

            double d, squaredD;

            switch (parentImplementor.StorageScheme) {
                case StorageScheme.Dense: {
                        double[] refArray = parentImplementor.Storage;

                        leadingDim = parentImplementor.NumberOfRows;

                        for (j = 0; j < numberOfColumns; j++) {
                            refIndex = leadingDim * colSubIndexes[j];

                            for (i = 0; i < numberOfRows; i++) {
                                d = refArray[rowSubIndexes[i] + refIndex] - averageArray[i];
                                squaredD = d * d;
                                sArray[i] = sArray[i] + squaredD;
                                m4Array[i] = m4Array[i] + squaredD * squaredD;
                                errorArray[i] = errorArray[i] + d;
                            }
                        }

                        for (i = 0; i < numberOfRows; i++) {
                            sArray[i] = (sArray[i] + Math.Pow(errorArray[i], 2.0)) / numberOfColumns;
                            m4Array[i] = m4Array[i] / numberOfColumns;
                            resultArray[i] = -3.0 + m4Array[i] / Math.Pow(sArray[i], 2.0);
                        }
                    }
                    break;
            }

            return result;
        }

        public static MatrixImplementor<double> View_Kurtosis(
            MatrixImplementor<double> data,
            int dimension)
        {
            MatrixImplementor<double> result;

            if (0 == dimension)
                result = View_KurtosisByRows(data);
            else
                result = View_KurtosisByCols(data);

            return result;
        }

        public static double View_Kurtosis(
            MatrixImplementor<double> data)
        {
            int count = data.Count;

            if (1 == count)
                return Double.NaN;

            double average = View_Sum(data) / count;

            double s = 0.0;
            double m4 = 0.0;
            double e = 0.0;
            double d, squaredD;

            ViewDoubleMatrixImplementor subImplementor = (ViewDoubleMatrixImplementor)(data);
            IndexCollection[] subIndexes = subImplementor.GetReferredImplementor(out MatrixImplementor<double> parentImplementor);

            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] colSubIndexes = subIndexes[1].indexes;
            int numberOfRows = rowSubIndexes.Length;
            int numberOfColumns = colSubIndexes.Length;

            int leadingDim, refIndex;
            int j, i;

            switch (parentImplementor.StorageScheme) {
                case StorageScheme.Dense: {
                        double[] refArray = parentImplementor.Storage;

                        leadingDim = parentImplementor.NumberOfRows;

                        for (j = 0; j < numberOfColumns; j++) {
                            refIndex = leadingDim * colSubIndexes[j];

                            for (i = 0; i < numberOfRows; i++) {
                                d = refArray[rowSubIndexes[i] + refIndex] - average;
                                squaredD = d * d;
                                s += squaredD;
                                m4 += squaredD * squaredD;
                                e += d;
                            }
                        }
                    }
                    break;
            }

            s += Math.Pow(e, 2.0);
            s /= count;
            double S2 = s * s;
            m4 /= count;
            double result = -3.0 + m4 / S2;

            return result;
        }

        #endregion

        #endregion

        #region Skewness

        #region Sparse

        public static MatrixImplementor<double> Sparse_SkewnessByCols(
            MatrixImplementor<double> data)
        {
            var sparseData = (SparseCsr3DoubleMatrixImplementor)data;

            int numberOfColumns = sparseData.numberOfColumns;

            var result = new DenseDoubleMatrixImplementor(1, numberOfColumns);
            double[] resultArray = result.Storage;

            int numberOfRows = sparseData.numberOfRows;

            int j;

            if (1 == numberOfRows) {
                for (j = 0; j < numberOfColumns; j++)
                    resultArray[j] = Double.NaN;

                return result;
            }

            double[] errorArray = new double[numberOfColumns];
            double[] sArray = new double[numberOfColumns];
            double[] m3Array = new double[numberOfColumns];

            MatrixImplementor<double> average = Sparse_Sum(sparseData, 1);
            double[] averageArray = average.Storage;
            for (j = 0; j < numberOfColumns; j++)
                averageArray[j] /= numberOfRows;

            var rowIndex = sparseData.rowIndex;
            var columns = sparseData.columns;
            var values = sparseData.values;

            double d, squaredD;
            int[] numberOfColumnStoredPositions = new int[numberOfColumns];

            for (int i = 0; i < numberOfRows; i++) {
                for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++) {
                    j = columns[p];
                    d = values[p] - averageArray[j];
                    squaredD = d * d;
                    sArray[j] += squaredD;
                    m3Array[j] += squaredD * d;
                    errorArray[j] += d;
                    numberOfColumnStoredPositions[j]++;
                }
            }

            int numberOfColumnUnstoredPositions;

            for (j = 0; j < resultArray.Length; j++) {
                numberOfColumnUnstoredPositions = numberOfRows - numberOfColumnStoredPositions[j];
                if (numberOfColumnUnstoredPositions > 0) {
                    d = -averageArray[j];
                    double zeroDeviations = d * numberOfColumnUnstoredPositions;
                    squaredD = d * d;
                    sArray[j] += squaredD * numberOfColumnUnstoredPositions;
                    m3Array[j] += squaredD * zeroDeviations;
                    errorArray[j] += zeroDeviations;
                }
                sArray[j] = (sArray[j] + Math.Pow(errorArray[j], 2.0)) / numberOfRows;
                m3Array[j] /= numberOfRows;
                resultArray[j] = m3Array[j] / Math.Pow(sArray[j], 1.5);
            }

            return result;
        }

        public static MatrixImplementor<double> Sparse_SkewnessByRows(
            MatrixImplementor<double> data)
        {
            var sparseData = (SparseCsr3DoubleMatrixImplementor)data;

            int numberOfRows = sparseData.numberOfRows;

            var result = new DenseDoubleMatrixImplementor(numberOfRows, 1);
            double[] resultArray = result.Storage;

            int numberOfColumns = sparseData.numberOfColumns;

            if (1 == numberOfColumns) {
                for (int i = 0; i < numberOfRows; i++)
                    resultArray[i] = Double.NaN;

                return result;
            }

            double[] errorArray = new double[numberOfRows];
            double[] sArray = new double[numberOfRows];
            double[] m3Array = new double[numberOfRows];

            MatrixImplementor<double> average = Sparse_Sum(sparseData, 0);

            double[] averageArray = average.Storage;
            for (int i = 0; i < numberOfRows; i++)
                averageArray[i] /= numberOfColumns;

            var rowIndex = sparseData.rowIndex;
            var values = sparseData.values;

            double d, squaredD, rowAverage;
            int numberOfRowUnstoredPositions, rowFromInclusive, rowToExclusive;
            for (int i = 0; i < averageArray.Length; i++) {
                rowAverage = averageArray[i];
                rowFromInclusive = rowIndex[i];
                rowToExclusive = rowIndex[i + 1];
                for (int p = rowFromInclusive; p < rowToExclusive; p++) {
                    d = values[p] - rowAverage;
                    squaredD = d * d;
                    sArray[i] += squaredD;
                    m3Array[i] += squaredD * d;
                    errorArray[i] += d;
                }

                numberOfRowUnstoredPositions = numberOfColumns - rowToExclusive + rowFromInclusive;

                if (numberOfRowUnstoredPositions > 0) {
                    d = -rowAverage;
                    squaredD = d * d;
                    double zeroDeviations = d * numberOfRowUnstoredPositions;
                    sArray[i] += squaredD * numberOfRowUnstoredPositions;
                    m3Array[i] += squaredD * zeroDeviations;
                    errorArray[i] += zeroDeviations;
                }
            }

            for (int i = 0; i < numberOfRows; i++) {
                sArray[i] = (sArray[i] + Math.Pow(errorArray[i], 2.0)) / numberOfColumns;
                m3Array[i] /= numberOfColumns;
                resultArray[i] = m3Array[i] / Math.Pow(sArray[i], 1.5);
            }

            return result;
        }

        public static MatrixImplementor<double> Sparse_Skewness(
            MatrixImplementor<double> data,
            int dimension)
        {
            MatrixImplementor<double> result;

            if (0 == dimension)
                result = Sparse_SkewnessByRows(data);
            else
                result = Sparse_SkewnessByCols(data);

            return result;
        }

        public static double Sparse_Skewness(
            MatrixImplementor<double> data)
        {
            var sparseData = (SparseCsr3DoubleMatrixImplementor)data;

            int count = sparseData.Count;

            if (1 == count)
                return Double.NaN;

            double average = Sparse_Sum(sparseData) / count;

            double s = 0.0;
            double m3 = 0.0;
            double e = 0.0;
            double d, squaredD;

            var rowIndex = sparseData.rowIndex;
            var values = sparseData.values;
            int numberOfStoredPositions = rowIndex[sparseData.numberOfRows];

            for (int p = 0; p < numberOfStoredPositions; p++) {
                d = values[p] - average;
                squaredD = d * d;
                s += squaredD;
                m3 += squaredD * d;
                e += d;
            }

            int numberOfUnstoredPositions = count - numberOfStoredPositions;

            if (numberOfUnstoredPositions > 0) {
                d = -average;
                double zeroDeviations = d * numberOfUnstoredPositions;
                squaredD = d * d;
                s += squaredD * numberOfUnstoredPositions;
                m3 += squaredD * zeroDeviations;
                e += zeroDeviations;
            }

            s += Math.Pow(e, 2.0);
            s /= count;

            double s3 = Math.Pow(s, 1.5);

            m3 /= count;

            double result = m3 / s3;
            return result;
        }

        #endregion

        #region Dense

        public static MatrixImplementor<double> Dense_SkewnessByCols(
            MatrixImplementor<double> data)
        {
            int numberOfColumns = data.NumberOfColumns;

            MatrixImplementor<double> result = new DenseDoubleMatrixImplementor(1, numberOfColumns);
            double[] resultArray = result.Storage;

            int numberOfRows = data.NumberOfRows;
            int j;

            if (1 == numberOfRows) {
                for (j = 0; j < numberOfColumns; j++)
                    resultArray[j] = Double.NaN;

                return result;
            }

            double[] dataArray = data.Storage;
            double[] errorArray = new double[numberOfColumns];
            double[] sArray = new double[numberOfColumns];
            double[] m3Array = new double[numberOfColumns];

            MatrixImplementor<double> average = Dense_Sum(data, 1);
            double[] averageArray = average.Storage;
            for (j = 0; j < numberOfColumns; j++)
                averageArray[j] /= numberOfRows;

            int i, offset;

            double d, squaredD;

            for (j = 0; j < numberOfColumns; j++) {
                offset = j * numberOfRows;
                for (i = 0; i < numberOfRows; i++) {
                    d = dataArray[offset + i] - averageArray[j];
                    squaredD = d * d;
                    sArray[j] = sArray[j] + squaredD;
                    m3Array[j] = m3Array[j] + squaredD * d;
                    errorArray[j] = errorArray[j] + d;
                }
                sArray[j] = (sArray[j] + Math.Pow(errorArray[j], 2.0)) / numberOfRows;
                m3Array[j] = m3Array[j] / numberOfRows;
                resultArray[j] = m3Array[j] / Math.Pow(sArray[j], 1.5);
            }

            return result;
        }

        public static MatrixImplementor<double> Dense_SkewnessByRows(
            MatrixImplementor<double> data)
        {
            int numberOfRows = data.NumberOfRows;

            var result = new DenseDoubleMatrixImplementor(numberOfRows, 1);
            double[] resultArray = result.Storage;

            int numberOfColumns = data.NumberOfColumns;
            int i;

            if (1 == numberOfColumns) {
                for (i = 0; i < numberOfRows; i++)
                    resultArray[i] = Double.NaN;

                return result;
            }

            double[] dataArray = data.Storage;
            double[] errorArray = new double[numberOfRows];
            double[] sArray = new double[numberOfRows];
            double[] m3Array = new double[numberOfRows];

            MatrixImplementor<double> average = Dense_Sum(data, 0);

            double[] averageArray = average.Storage;
            for (i = 0; i < numberOfRows; i++)
                averageArray[i] /= numberOfColumns;

            int j, offset;

            double d, squaredD;

            for (j = 0; j < numberOfColumns; j++) {
                offset = j * numberOfRows;
                for (i = 0; i < numberOfRows; i++) {
                    d = dataArray[offset + i] - averageArray[i];
                    squaredD = d * d;
                    sArray[i] = sArray[i] + squaredD;
                    m3Array[i] = m3Array[i] + squaredD * d;
                    errorArray[i] = errorArray[i] + d;
                }
            }

            for (i = 0; i < numberOfRows; i++) {
                sArray[i] = (sArray[i] + Math.Pow(errorArray[i], 2.0)) / numberOfColumns;
                m3Array[i] = m3Array[i] / numberOfColumns;
                resultArray[i] = m3Array[i] / Math.Pow(sArray[i], 1.5);
            }

            return result;
        }

        public static MatrixImplementor<double> Dense_Skewness(
            MatrixImplementor<double> data,
            int dimension)
        {
            MatrixImplementor<double> result;

            if (0 == dimension)
                result = Dense_SkewnessByRows(data);
            else
                result = Dense_SkewnessByCols(data);

            return result;
        }

        public static double Dense_Skewness(
            MatrixImplementor<double> data)
        {
            double[] dataArray = data.Storage;
            int count = dataArray.Length;

            if (1 == count)
                return Double.NaN;

            double average = Dense_Sum(data) / count;

            double s = 0.0;
            double m3 = 0.0;
            double e = 0.0;
            double d, squaredD;

            int j;

            for (j = 0; j < count; j++) {
                d = dataArray[j] - average;
                squaredD = d * d;
                s += squaredD;
                m3 += squaredD * d;
                e += d;
            }

            s += Math.Pow(e, 2.0);
            s /= count;

            double s3 = Math.Pow(s, 1.5);

            m3 /= count;

            double result = m3 / s3;
            return result;
        }

        #endregion

        #region View

        public static MatrixImplementor<double> View_SkewnessByCols(
            MatrixImplementor<double> data)
        {
            ViewDoubleMatrixImplementor subImplementor = (ViewDoubleMatrixImplementor)(data);
            IndexCollection[] subIndexes = subImplementor.GetReferredImplementor(out MatrixImplementor<double> parentImplementor);

            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] colSubIndexes = subIndexes[1].indexes;
            int numberOfRows = rowSubIndexes.Length;
            int numberOfColumns = colSubIndexes.Length;

            var result = new DenseDoubleMatrixImplementor(1, numberOfColumns);
            double[] resultArray = result.Storage;
            int j;

            if (1 == numberOfRows) {
                for (j = 0; j < numberOfColumns; j++)
                    resultArray[j] = Double.NaN;

                return result;
            }

            double[] errorArray = new double[numberOfColumns];
            double[] sArray = new double[numberOfColumns];
            double[] m3Array = new double[numberOfColumns];

            MatrixImplementor<double> average = View_Sum(data, 1);
            double[] averageArray = average.Storage;
            for (j = 0; j < numberOfColumns; j++)
                averageArray[j] /= numberOfRows;

            int leadingDim, refIndex;
            int i;

            double d, squaredD;

            switch (parentImplementor.StorageScheme) {
                case StorageScheme.Dense: {
                        double[] refArray = parentImplementor.Storage;

                        leadingDim = parentImplementor.NumberOfRows;

                        for (j = 0; j < numberOfColumns; j++) {
                            refIndex = leadingDim * colSubIndexes[j];
                            for (i = 0; i < numberOfRows; i++) {
                                d = refArray[rowSubIndexes[i] + refIndex] - averageArray[j];
                                squaredD = d * d;
                                sArray[j] = sArray[j] + squaredD;
                                m3Array[j] = m3Array[j] + squaredD * d;
                                errorArray[j] = errorArray[j] + d;
                            }
                            sArray[j] = (sArray[j] + Math.Pow(errorArray[j], 2.0)) / numberOfRows;
                            m3Array[j] = m3Array[j] / numberOfRows;
                            resultArray[j] = m3Array[j] / Math.Pow(sArray[j], 1.5);
                        }
                    }
                    break;
            }

            return result;
        }

        public static MatrixImplementor<double> View_SkewnessByRows(
            MatrixImplementor<double> data)
        {
            ViewDoubleMatrixImplementor subImplementor = (ViewDoubleMatrixImplementor)(data);
            IndexCollection[] subIndexes = subImplementor.GetReferredImplementor(out MatrixImplementor<double> parentImplementor);

            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] colSubIndexes = subIndexes[1].indexes;
            int numberOfRows = rowSubIndexes.Length;
            int numberOfColumns = colSubIndexes.Length;

            var result = new DenseDoubleMatrixImplementor(numberOfRows, 1);
            double[] resultArray = result.Storage;
            int i;

            if (1 == numberOfColumns) {
                for (i = 0; i < numberOfRows; i++)
                    resultArray[i] = Double.NaN;

                return result;
            }

            double[] errorArray = new double[numberOfRows];
            double[] sArray = new double[numberOfRows];
            double[] m3Array = new double[numberOfRows];

            MatrixImplementor<double> average = View_Sum(data, 0);
            double[] averageArray = average.Storage;
            for (i = 0; i < numberOfRows; i++)
                averageArray[i] /= numberOfColumns;

            int leadingDim, refIndex;
            int j;

            double d, squaredD;

            switch (parentImplementor.StorageScheme) {
                case StorageScheme.Dense: {
                        double[] refArray = parentImplementor.Storage;

                        leadingDim = parentImplementor.NumberOfRows;

                        for (j = 0; j < numberOfColumns; j++) {
                            refIndex = leadingDim * colSubIndexes[j];

                            for (i = 0; i < numberOfRows; i++) {
                                d = refArray[rowSubIndexes[i] + refIndex] - averageArray[i];
                                squaredD = d * d;
                                sArray[i] = sArray[i] + squaredD;
                                m3Array[i] = m3Array[i] + squaredD * d;
                                errorArray[i] = errorArray[i] + d;
                            }
                        }

                        for (i = 0; i < numberOfRows; i++) {
                            sArray[i] = (sArray[i] + Math.Pow(errorArray[i], 2.0)) / numberOfColumns;
                            m3Array[i] = m3Array[i] / numberOfColumns;
                            resultArray[i] = m3Array[i] / Math.Pow(sArray[i], 1.5);
                        }
                    }
                    break;
            }

            return result;
        }

        public static MatrixImplementor<double> View_Skewness(
            MatrixImplementor<double> data,
            int dimension)
        {
            MatrixImplementor<double> result;

            if (0 == dimension)
                result = View_SkewnessByRows(data);
            else
                result = View_SkewnessByCols(data);

            return result;
        }

        public static double View_Skewness(
            MatrixImplementor<double> data)
        {
            int count = data.Count;

            if (1 == count)
                return Double.NaN;

            double average = View_Sum(data) / count;

            double s = 0.0;
            double m3 = 0.0;
            double e = 0.0;
            double d, squaredD;

            ViewDoubleMatrixImplementor subImplementor = (ViewDoubleMatrixImplementor)(data);
            IndexCollection[] subIndexes = subImplementor.GetReferredImplementor(out MatrixImplementor<double> parentImplementor);

            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] colSubIndexes = subIndexes[1].indexes;
            int numberOfRows = rowSubIndexes.Length;
            int numberOfColumns = colSubIndexes.Length;

            int leadingDim, refIndex;

            switch (parentImplementor.StorageScheme) {
                case StorageScheme.Dense: {
                        double[] refArray = parentImplementor.Storage;

                        leadingDim = parentImplementor.NumberOfRows;

                        for (int j = 0; j < numberOfColumns; j++) {
                            refIndex = leadingDim * colSubIndexes[j];

                            for (int i = 0; i < numberOfRows; i++) {
                                d = refArray[rowSubIndexes[i] + refIndex] - average;
                                squaredD = d * d;
                                s += squaredD;
                                m3 += squaredD * d;
                                e += d;
                            }
                        }
                    }
                    break;
            }

            s += Math.Pow(e, 2.0);
            s /= count;
            double S3 = Math.Pow(s, 1.5);
            m3 /= count;
            double Result = m3 / S3;

            return Result;
        }

        #endregion

        #endregion

        #region Sum of squared deviations

        #region Sparse

        public static MatrixImplementor<double> Sparse_SumOfSquaredDeviationsByCols(
            MatrixImplementor<double> data)
        {
            var sparseData = (SparseCsr3DoubleMatrixImplementor)data;

            int numberOfColumns = sparseData.numberOfColumns;

            var result = new DenseDoubleMatrixImplementor(1, numberOfColumns);

            int numberOfRows = sparseData.numberOfRows;

            if (numberOfRows == 1) {
                return result;
            }

            double[] resArray = result.Storage;

            int j;


            double[] errorArray = new double[numberOfColumns];

            MatrixImplementor<double> average = Sparse_Sum(sparseData, 1);

            double[] averageArray = average.Storage;
            for (j = 0; j < averageArray.Length; j++)
                averageArray[j] /= numberOfRows;

            var rowIndex = sparseData.rowIndex;
            var columns = sparseData.columns;
            var values = sparseData.values;

            double d;
            int[] numberOfColumnStoredPositions = new int[numberOfColumns];

            for (int i = 0; i < numberOfRows; i++) {
                for (int p = rowIndex[i]; p < rowIndex[i + 1]; p++) {
                    j = columns[p];
                    d = values[p] - averageArray[j];
                    resArray[j] += d * d;
                    errorArray[j] += d;
                    numberOfColumnStoredPositions[j]++;
                }
            }

            int numberOfColumnUnstoredPositions;

            for (j = 0; j < resArray.Length; j++) {
                numberOfColumnUnstoredPositions = numberOfRows - numberOfColumnStoredPositions[j];
                if (numberOfColumnUnstoredPositions > 0) {
                    d = -averageArray[j];
                    double zeroDeviations = d * numberOfColumnUnstoredPositions;
                    resArray[j] += d * zeroDeviations;
                    errorArray[j] += zeroDeviations;
                }

                resArray[j] += Math.Pow(errorArray[j], 2.0) / numberOfRows;
            }

            return result;
        }

        public static MatrixImplementor<double> Sparse_SumOfSquaredDeviationsByRows(
            MatrixImplementor<double> data)
        {
            var sparseData = (SparseCsr3DoubleMatrixImplementor)data;

            int numberOfRows = sparseData.numberOfRows;

            var result = new DenseDoubleMatrixImplementor(numberOfRows, 1);

            int numberOfColumns = sparseData.numberOfColumns;

            if (numberOfColumns == 1) {
                return result;
            }

            double[] resArray = result.Storage;


            double[] errorArray = new double[numberOfRows];

            MatrixImplementor<double> average = Sparse_Sum(sparseData, 0);
            double[] averageArray = average.Storage;
            for (int i = 0; i < numberOfRows; i++)
                averageArray[i] /= numberOfColumns;

            var rowIndex = sparseData.rowIndex;
            var values = sparseData.values;

            double d, rowAverage;
            int numberOfRowUnstoredPositions, rowFromInclusive, rowToExclusive;
            for (int i = 0; i < averageArray.Length; i++) {
                rowAverage = averageArray[i];
                rowFromInclusive = rowIndex[i];
                rowToExclusive = rowIndex[i + 1];
                for (int p = rowFromInclusive; p < rowToExclusive; p++) {
                    d = values[p] - rowAverage;
                    resArray[i] += d * d;
                    errorArray[i] += d;
                }

                numberOfRowUnstoredPositions = numberOfColumns - rowToExclusive + rowFromInclusive;

                if (numberOfRowUnstoredPositions > 0) {
                    d = -rowAverage;
                    double zeroDeviations = d * numberOfRowUnstoredPositions;
                    resArray[i] += d * zeroDeviations;
                    errorArray[i] += zeroDeviations;
                }
            }

            for (int i = 0; i < resArray.Length; i++)
                resArray[i] += Math.Pow(errorArray[i], 2.0) / numberOfColumns;

            return result;
        }

        public static MatrixImplementor<double> Sparse_SumOfSquaredDeviations(
            MatrixImplementor<double> data,
            int dimension)
        {
            MatrixImplementor<double> result;

            if (0 == dimension)
                result = Sparse_SumOfSquaredDeviationsByRows(data);
            else
                result = Sparse_SumOfSquaredDeviationsByCols(data);

            return result;
        }
        public static double Sparse_SumOfSquaredDeviations(
            MatrixImplementor<double> data)
        {
            var sparseData = (SparseCsr3DoubleMatrixImplementor)data;

            int dataLength = sparseData.Count;

            if (1 == dataLength)
                return 0.0;

            double average = Sparse_Sum(sparseData) / dataLength;

            double s = 0.0;
            double e = 0.0;
            double d;

            var rowIndex = sparseData.rowIndex;
            var values = sparseData.values;
            int numberOfStoredPositions = rowIndex[sparseData.numberOfRows];

            for (int p = 0; p < numberOfStoredPositions; p++) {
                d = values[p] - average;
                s += d * d;
                e += d;
            }

            int numberOfUnstoredPositions = dataLength - numberOfStoredPositions;

            if (numberOfUnstoredPositions > 0) {
                d = -average;
                double zeroDeviations = d * numberOfUnstoredPositions;
                s += d * zeroDeviations;
                e += zeroDeviations;
            }

            double result = s + Math.Pow(e, 2.0) / dataLength;
            return result;
        }

        #endregion

        #region Dense

        public static MatrixImplementor<double> Dense_SumOfSquaredDeviationsByCols(
            MatrixImplementor<double> data)
        {
            int numberOfColumns = data.NumberOfColumns;

            var result = new DenseDoubleMatrixImplementor(1, numberOfColumns);
            
            int numberOfRows = data.NumberOfRows;

            if (numberOfRows == 1) {
                return result;
            }

            double[] resultArray = result.Storage;
            int j;

            double[] dataArray = data.Storage;
            double[] errorArray = new double[numberOfColumns];

            MatrixImplementor<double> average = Dense_Sum(data, 1);

            double[] averageArray = average.Storage;
            for (j = 0; j < numberOfColumns; j++)
                averageArray[j] /= numberOfRows;

            int i, refIndex;
            double d;

            for (j = 0; j < numberOfColumns; j++) {
                refIndex = j * numberOfRows;
                for (i = 0; i < numberOfRows; i++) {
                    d = dataArray[refIndex + i] - averageArray[j];
                    resultArray[j] = resultArray[j] + d * d;
                    errorArray[j] = errorArray[j] + d;
                }
                resultArray[j] += Math.Pow(errorArray[j], 2.0) / numberOfRows;
            }

            return result;
        }

        public static MatrixImplementor<double> Dense_SumOfSquaredDeviationsByRows(
            MatrixImplementor<double> data)
        {
            int numberOfRows = data.NumberOfRows;

            var result = new DenseDoubleMatrixImplementor(numberOfRows, 1);

            int numberOfColumns = data.NumberOfColumns;

            if (numberOfColumns == 1) {
                return result;
            }
            
            double[] resultArray = result.Storage;
            int i;

            double[] dataArray = data.Storage;
            double[] errorArray = new double[numberOfRows];

            MatrixImplementor<double> average = Dense_Sum(data, 0);
            double[] averageArray = average.Storage;
            for (i = 0; i < numberOfRows; i++)
                averageArray[i] /= numberOfColumns;

            int j, refIndex;
            double d;

            for (j = 0; j < numberOfColumns; j++) {
                refIndex = j * numberOfRows;
                for (i = 0; i < numberOfRows; i++) {
                    d = dataArray[refIndex + i] - averageArray[i];
                    resultArray[i] = resultArray[i] + d * d;
                    errorArray[i] = errorArray[i] + d;
                }
            }

            for (i = 0; i < numberOfRows; i++)
                resultArray[i] = resultArray[i] + Math.Pow(errorArray[i], 2.0) / numberOfColumns;

            return result;
        }

        public static MatrixImplementor<double> Dense_SumOfSquaredDeviations(
            MatrixImplementor<double> data,
            int dimension)
        {
            MatrixImplementor<double> result;

            if (0 == dimension)
                result = Dense_SumOfSquaredDeviationsByRows(data);
            else
                result = Dense_SumOfSquaredDeviationsByCols(data);

            return result;
        }

        public static double Dense_SumOfSquaredDeviations(
            MatrixImplementor<double> data)
        {
            double[] dataArray = data.Storage;
            int count = dataArray.Length;

            if (1 == count)
                return 0.0;

            double average = Dense_Sum(data) / count;

            double s = 0.0;
            double e = 0.0;
            double d;

            int j;

            for (j = 0; j < count; j++) {
                d = dataArray[j] - average;
                s += d * d;
                e += d;
            }

            double result = s + Math.Pow(e, 2.0) / count;
            return result;
        }

        #endregion

        #region View

        public static MatrixImplementor<double> View_SumOfSquaredDeviationsByCols(
            MatrixImplementor<double> data)
        {
            ViewDoubleMatrixImplementor subImplementor = (ViewDoubleMatrixImplementor)(data);
            IndexCollection[] subIndexes = subImplementor.GetReferredImplementor(out MatrixImplementor<double> parentImplementor);

            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] colSubIndexes = subIndexes[1].indexes;
            int numberOfRows = rowSubIndexes.Length;
            int numberOfColumns = colSubIndexes.Length;

            var result = new DenseDoubleMatrixImplementor(1, numberOfColumns);

            if (numberOfRows == 1) {
                return result;
            }

            double[] resultArray = result.Storage;

            double[] errorArray = new double[numberOfColumns];

            MatrixImplementor<double> average = View_Sum(data, 1);
            double[] averageArray = average.Storage;
            for (int j = 0; j < numberOfColumns; j++)
                averageArray[j] /= numberOfRows;

            int leadingDim, refIndex;

            double d;

            switch (parentImplementor.StorageScheme) {
                case StorageScheme.Dense: {
                        double[] refArray = parentImplementor.Storage;

                        leadingDim = parentImplementor.NumberOfRows;

                        for (int j = 0; j < numberOfColumns; j++) {
                            refIndex = leadingDim * colSubIndexes[j];
                            for (int i = 0; i < numberOfRows; i++) {
                                d = refArray[rowSubIndexes[i] + refIndex] - averageArray[j];
                                resultArray[j] = resultArray[j] + d * d;
                                errorArray[j] = errorArray[j] + d;
                            }
                            resultArray[j] += Math.Pow(errorArray[j], 2.0) / numberOfRows;
                        }
                    }
                    break;
            }

            return result;
        }

        public static MatrixImplementor<double> View_SumOfSquaredDeviationsByRows(
            MatrixImplementor<double> data)
        {
            ViewDoubleMatrixImplementor subImplementor = (ViewDoubleMatrixImplementor)(data);
            IndexCollection[] subIndexes = subImplementor.GetReferredImplementor(out MatrixImplementor<double> parentImplementor);

            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] colSubIndexes = subIndexes[1].indexes;
            int numberOfRows = rowSubIndexes.Length;
            int numberOfColumns = colSubIndexes.Length;

            var result = new DenseDoubleMatrixImplementor(numberOfRows, 1);

            if (numberOfColumns == 1) {
                return result;
            }

            double[] resultArray = result.Storage;

            double[] errorArray = new double[numberOfRows];

            MatrixImplementor<double> average = View_Sum(data, 0);
            double[] averageArray = average.Storage;
            for (int i = 0; i < numberOfRows; i++)
                averageArray[i] /= numberOfColumns;

            int leadingDim, refIndex;

            double d;

            switch (parentImplementor.StorageScheme) {
                case StorageScheme.Dense: {
                        double[] refArray = parentImplementor.Storage;

                        leadingDim = parentImplementor.NumberOfRows;

                        for (int j = 0; j < numberOfColumns; j++) {
                            refIndex = leadingDim * colSubIndexes[j];

                            for (int i = 0; i < numberOfRows; i++) {
                                d = refArray[rowSubIndexes[i] + refIndex] - averageArray[i];
                                resultArray[i] = resultArray[i] + d * d;
                                errorArray[i] = errorArray[i] + d;
                            }
                        }

                        for (int i = 0; i < numberOfRows; i++)
                            resultArray[i] = resultArray[i] + Math.Pow(errorArray[i], 2.0) / numberOfColumns;
                    }
                    break;
            }

            return result;
        }

        public static MatrixImplementor<double> View_SumOfSquaredDeviations(
            MatrixImplementor<double> data,
            int dimension)
        {
            MatrixImplementor<double> result;

            if (0 == dimension)
                result = View_SumOfSquaredDeviationsByRows(data);
            else
                result = View_SumOfSquaredDeviationsByCols(data);

            return result;
        }

        public static double View_SumOfSquaredDeviations(
            MatrixImplementor<double> data)
        {
            int count = data.Count;

            if (1 == count)
                return 0.0;

            double average = View_Sum(data) / count;

            double s = 0.0;
            double e = 0.0;
            double d;

            ViewDoubleMatrixImplementor subImplementor = (ViewDoubleMatrixImplementor)(data);
            IndexCollection[] subIndexes = subImplementor.GetReferredImplementor(out MatrixImplementor<double> parentImplementor);

            int[] rowSubIndexes = subIndexes[0].indexes;
            int[] colSubIndexes = subIndexes[1].indexes;
            int numberOfRows = rowSubIndexes.Length;
            int numberOfColumns = colSubIndexes.Length;

            int leadingDim, refIndex;

            switch (parentImplementor.StorageScheme) {
                case StorageScheme.Dense: {
                        double[] refArray = parentImplementor.Storage;

                        leadingDim = parentImplementor.NumberOfRows;

                        for (int j = 0; j < numberOfColumns; j++) {
                            refIndex = leadingDim * colSubIndexes[j];

                            for (int i = 0; i < numberOfRows; i++) {
                                d = refArray[rowSubIndexes[i] + refIndex] - average;
                                s += d * d;
                                e += d;
                            }
                        }
                    }
                    break;
            }

            return s + Math.Pow(e, 2.0) / count;
        }

        #endregion

        #endregion

        #region Out-place sort

        public static void Dense_Sort_IndexTable(
            MatrixImplementor<double> data,
            SortDirection sortDirection,
            out IndexCollection indexTable)
        {
            double[] dataArray = data.Storage;
            SortHelper.Sort(dataArray, sortDirection, out int[] indexTableArray);
            indexTable = new IndexCollection(indexTableArray, false);
        }

        public static void Sparse_Sort_IndexTable(
            MatrixImplementor<double> data,
            SortDirection sortDirection,
            out IndexCollection indexTable)
        {
            var sparseData = (SparseCsr3DoubleMatrixImplementor)data;
            int numberOfRows = sparseData.numberOfRows;

            var rowIndex = sparseData.rowIndex;
            var columns = sparseData.columns;
            var values = sparseData.values;
            int numberOfStoredPositions = rowIndex[numberOfRows];
            int dataLength = sparseData.Count;

            if (numberOfStoredPositions == 0) {
                indexTable = IndexCollection.Range(0, dataLength - 1);
                return;
            }

            var denseStorage = sparseData.AsColumnMajorDenseArray();

            Array.Clear(rowIndex, 0, numberOfRows + 1);
            Array.Clear(columns, 0, numberOfStoredPositions);
            Array.Clear(values, 0, numberOfStoredPositions);

            SortHelper.Sort(denseStorage, sortDirection, out int[] indexes);
            indexTable = new IndexCollection(indexes, false);

            double value;
            int firstIndex = 0, lastIndex = -1;
            if (sortDirection == SortDirection.Ascending) {
                for (int l = 0; l < dataLength; l++) {
                    value = denseStorage[l];
                    if (value < 0.0) {
                        sparseData.SetValue(firstIndex++, value);
                    }
                    else {
                        if (value > 0.0) {
                            lastIndex = l;
                            break;
                        }
                    }
                }
            }
            else {
                for (int l = 0; l < dataLength; l++) {
                    value = denseStorage[l];
                    if (value > 0.0) {
                        sparseData.SetValue(firstIndex++, value);
                    }
                    else {
                        if (value < 0.0) {
                            lastIndex = l;
                            break;
                        }
                    }
                }
            }

            if (lastIndex != -1) {
                for (int l = lastIndex; l < dataLength; l++) {
                    value = denseStorage[l];
                    sparseData.SetValue(l, value);
                }
            }
        }

        public static void View_Sort_IndexTable(
            MatrixImplementor<double> data,
            SortDirection sortDirection,
            out IndexCollection indexTable)
        {
            ViewDoubleMatrixImplementor subMatrix = (ViewDoubleMatrixImplementor)(data);

            subMatrix.ParentImplementor.ChangingData -=
               new EventHandler<EventArgs>
                  (subMatrix.ChangingDataHandler);

            subMatrix.ParentImplementor.ImplementorChanged -=
               new EventHandler<ImplementorChangedEventArgs>
                  (subMatrix.ImplementorChangedHandler);

            DenseDoubleMatrixImplementor denseMatrix = subMatrix;

            ImplementorChangedEventArgs args = new ImplementorChangedEventArgs(denseMatrix);

            subMatrix.OnImplementorChanged(args);

            Dense_Sort_IndexTable(denseMatrix, sortDirection, out indexTable);
        }

        public static void Dense_Sort(
            MatrixImplementor<double> data,
            SortDirection sortDirection)
        {
            double[] dataArray = data.Storage;

            SortHelper.Sort(dataArray, sortDirection);
        }

        public static void Sparse_Sort(
            MatrixImplementor<double> data,
            SortDirection sortDirection)
        {
            var sparseData = (SparseCsr3DoubleMatrixImplementor)data;
            int numberOfRows = sparseData.numberOfRows;

            var rowIndex = sparseData.rowIndex;
            var columns = sparseData.columns;
            var values = sparseData.values;
            int numberOfStoredPositions = rowIndex[numberOfRows];

            if (numberOfStoredPositions == 0)
                return;

            Array.Clear(rowIndex, 0, numberOfRows + 1);
            Array.Clear(columns, 0, numberOfStoredPositions);

            var newValues = new double[numberOfStoredPositions];
            Array.Copy(values, newValues, numberOfStoredPositions);
            Array.Clear(values, 0, numberOfStoredPositions);

            SortHelper.Sort(newValues, sortDirection);

            double value;
            int firstIndex = 0, lastIndex = -1;
            if (sortDirection == SortDirection.Ascending) {
                for (int p = 0; p < numberOfStoredPositions; p++) {
                    value = newValues[p];
                    if (value < 0.0) {
                        sparseData.SetValue(firstIndex++, value);
                    }
                    else {
                        if (value > 0.0) {
                            lastIndex = p;
                            break;
                        }
                    }
                }
            }
            else {
                for (int p = 0; p < numberOfStoredPositions; p++) {
                    value = newValues[p];
                    if (value > 0.0) {
                        sparseData.SetValue(firstIndex++, value);
                    }
                    else {
                        if (value < 0.0) {
                            lastIndex = p;
                            break;
                        }
                    }
                }
            }

            if (lastIndex != -1) {
                int numberOfStoredLastValues = newValues.Length - lastIndex;
                int lastValuesFirstPosition = sparseData.Count - numberOfStoredLastValues;
                for (int p = lastIndex; p < numberOfStoredPositions; p++) {
                    value = newValues[p];
                    sparseData.SetValue(lastValuesFirstPosition++, value);
                }
            }
        }

        public static void View_Sort(
            MatrixImplementor<double> data,
            SortDirection sortDirection)
        {
            ViewDoubleMatrixImplementor subMatrix = (ViewDoubleMatrixImplementor)(data);

            subMatrix.ParentImplementor.ChangingData -=
               new EventHandler<EventArgs>
                  (subMatrix.ChangingDataHandler);

            subMatrix.ParentImplementor.ImplementorChanged -=
               new EventHandler<ImplementorChangedEventArgs>
                  (subMatrix.ImplementorChangedHandler);

            DenseDoubleMatrixImplementor denseMatrix = subMatrix;

            ImplementorChangedEventArgs args = new ImplementorChangedEventArgs(denseMatrix);

            subMatrix.OnImplementorChanged(args);

            Dense_Sort(denseMatrix, sortDirection);
        }

        #endregion
    }
}
