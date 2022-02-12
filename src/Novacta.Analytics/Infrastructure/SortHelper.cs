// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace Novacta.Analytics.Infrastructure
{
    internal static class SortHelper
    {
        #region Sort

        /// <summary>
        /// Sort data in increasing or decreasing order.
        /// </summary>
        /// <param name="data">The array to be sorted into increasing order or into decreasing order, 
        /// depending on <paramref name="sortDirection"/>.</param>
        /// <param name="sortDirection">The sorting mode (increasing/decreasing) to be applied.</param>
        public static void Sort(double[] data, SortDirection sortDirection)
        {
            if (data.Length > partialQuichSortMaxDataLength)
                PartialQuickSort(data, sortDirection);

            InsertionSort(data, sortDirection);
        }

        /// <summary>
        /// Sort data in increasing or decreasing order and calculate the corresponding index table.
        /// </summary>
        /// <param name="data">The array to be sorted into increasing order or into decreasing order, 
        /// depending on <paramref name="sortDirection"/>.</param>
        /// <param name="sortDirection">The sorting mode (increasing/decreasing) to be applied.</param>
        /// <param name="indexTable">The position table corresponding to the applied sorting operation.</param>
        public static void Sort(double[] data, SortDirection sortDirection, out int[] indexTable)
        {
            int n = data.Length;
            indexTable = new int[n];
            for (int i = 0; i < indexTable.Length; i++)
                indexTable[i] = i;

            if (n > partialQuichSortMaxDataLength)
                PartialQuickSort(data, sortDirection, indexTable);

            InsertionSort(data, sortDirection, indexTable);
        }

        /// <summary>
        /// Sort data in increasing or decreasing order and calculate the corresponding index table.
        /// </summary>
        /// <param name="data">The array to be sorted into increasing order or into decreasing order, 
        /// depending on <paramref name="sortDirection"/>.</param>
        /// <param name="sortDirection">The sorting mode (increasing/decreasing) to be applied.</param>
        /// <param name="indexTable">The position table corresponding to the applied sorting operation.</param>
        public static void Sort(int[] data, SortDirection sortDirection, out int[] indexTable)
        {
            int n = data.Length;
            indexTable = new int[n];
            for (int i = 0; i < indexTable.Length; i++)
                indexTable[i] = i;

            if (n > partialQuichSortMaxDataLength)
                PartialQuickSort(data, sortDirection, indexTable);

            InsertionSort(data, sortDirection, indexTable);
        }

        #endregion

        #region QuickSort

        [StructLayout(LayoutKind.Explicit, Size = 8)]
        internal struct QuickSortStackItem
        {
            [FieldOffset(0)]
            public int m_l;
            [FieldOffset(4)]
            public int m_r;

            public QuickSortStackItem(int l, int r)
            {
                this.m_l = l;
                this.m_r = r;
            }

            public override string ToString()
            {
                StringBuilder stringBuilder = new();
                stringBuilder.Append("(");
                stringBuilder.Append(this.m_l);
                stringBuilder.Append(", ");
                stringBuilder.Append(this.m_r);
                stringBuilder.Append(")\t");
                return stringBuilder.ToString();
            }
        }

        const int partialQuichSortMaxDataLength = 10;
        
        /// <summary>
        /// Partially sort data and corresponding indexes in increasing or decreasing order using the QuickSort algorithm.
        /// </summary>
        /// <param name="data">The array to be sorted into increasing order or into decreasing order,
        /// depending on <paramref name="sortDirection"/>.</param>
        /// <param name="sortDirection">The sorting mode (increasing/decreasing) to be applied.</param>
        /// <param name="indexes">The indexes.</param>
        /// <remarks>This method does not execute the final Insertion Sort step, which instead must be called
        /// explicitly.</remarks>
        private static void PartialQuickSort(double[] data, SortDirection sortDirection, int[] indexes)
        {
            /* This implementation follows that described as Algorithm Q in
             * 
             * Knuth, D. The Art of Computer Programming, Vol. 3, Sorting and Searching, p. 115.
             * 
             * However, the search of the pivot element is based on the Median-of-Three approach,
             * as described in 
             * 
             * SedgewicK, u. Implementing QuickSort Programs, Comm. ACM 21, 10 (Oct. 1978), 847-857.
             */

            int r, l, i, j;

            double k;  // This is the pivot element
            double tmp; // swap helper
            int jMinusl, rMinusj;
            int intTmp; // swap helper
            int mid, lPlus1; // median-of-three search helpers
            // Stack initialization
            int n = data.Length;
            int stackCapacity = (int)Math.Floor(Math.Log((n + 1) / (partialQuichSortMaxDataLength + 2), 2.0));
            Stack<QuickSortStackItem> stack = new(stackCapacity);
            QuickSortStackItem item;

            // Q1: Initialize
            l = 0;
            r = n - 1;

            if (sortDirection == SortDirection.Ascending) {
                //unsafe {
                //    fixed (double* keysPointer = &keys[0]) {
                //        fixed (int* indexesPointer = &indexes[0]) {
                Q2: // Begin new stage


                // Median-of-Three search of the pivot element
                mid = (l + r) / 2;
                lPlus1 = l + 1;

                tmp = data[mid];
                data[mid] = data[lPlus1];
                data[lPlus1] = tmp;

                intTmp = indexes[mid];
                indexes[mid] = indexes[lPlus1];
                indexes[lPlus1] = intTmp;

                if (data[lPlus1] > data[r]) {
                    tmp = data[lPlus1];
                    data[lPlus1] = data[r];
                    data[r] = tmp;

                    intTmp = indexes[lPlus1];
                    indexes[lPlus1] = indexes[r];
                    indexes[r] = intTmp;
                }

                if (data[l] > data[r]) {
                    tmp = data[l];
                    data[l] = data[r];
                    data[r] = tmp;

                    intTmp = indexes[l];
                    indexes[l] = indexes[r];
                    indexes[r] = intTmp;
                }

                if (data[lPlus1] > data[l]) {
                    tmp = data[lPlus1];
                    data[lPlus1] = data[l];
                    data[l] = tmp;

                    intTmp = indexes[lPlus1];
                    indexes[lPlus1] = indexes[l];
                    indexes[l] = intTmp;
                }

                i = lPlus1;
                j = r;

                // Knuth initialization
                //i = l;
                //j = r + 1;

                k = data[l];

                Q3: // Compare keys[i] : k
                do {
                    i++;
                } while (data[i] < k && i < n);
                //while (i < n && keys[i] < k); // We need to check for i<n in case k is the largest of the keys.
                // No need for this check if Median-of-3 is exploited

                // Here i<=j

                // Q4: Compare k : keys[j]
                do {
                    j--;
                } while (k < data[j]);

                //Debug.WriteLine("(i, j) = ({0}, {1})", i, j);

                // Here i-1 <= j

                // Q5: Test i : j
                if (i < j) {
                    // Q6: Exchange
                    tmp = data[i];
                    data[i] = data[j];
                    data[j] = tmp;

                    intTmp = indexes[i];
                    indexes[i] = indexes[j];
                    indexes[j] = intTmp;

                    goto Q3;
                }
                else {
                    // Here j <= i

                    // Interchange keys[l] with keys[j]
                    tmp = data[l];
                    data[l] = data[j];
                    data[j] = tmp;

                    intTmp = indexes[l];
                    indexes[l] = indexes[j];
                    indexes[j] = intTmp;

                    // Q7: Put on stack
                    jMinusl = j - l;
                    rMinusj = r - j;
                    if (partialQuichSortMaxDataLength < jMinusl && jMinusl <= rMinusj) {

                        // Here both subfile lengths > M
                        // but the greater is that on the right 
                        stack.Push(new QuickSortStackItem(j + 1, r));
                        r = j - 1;
                        goto Q2;
                    }
                    else {
                        if (partialQuichSortMaxDataLength < rMinusj && rMinusj < jMinusl) {

                            // Here both subfile lengths > M
                            // but the greater is that on the left 
                            stack.Push(new QuickSortStackItem(l, j - 1));
                            l = j + 1;
                            goto Q2;
                        }
                        else {
                            if (jMinusl <= partialQuichSortMaxDataLength && partialQuichSortMaxDataLength < rMinusj) {
                                l = j + 1;
                                goto Q2;
                            }
                            else {
                                if (partialQuichSortMaxDataLength < jMinusl && rMinusj <= partialQuichSortMaxDataLength) {
                                    r = j - 1;
                                    goto Q2;
                                }
                            }
                        }
                    }

                    // Here both subfile lengths <= M

                    // Q8: Take off stack
                    if (stack.Count > 0) {

                        // Here if the stack is nonempty
                        item = stack.Pop();
                        l = item.m_l;
                        r = item.m_r;
                        goto Q2;
                    }
                }

                //        }
                //    }
                //}
            }
            else {
                Q2bis: // Begin new stage

                // Median-of-Three search of the pivot element
                mid = (l + r) / 2;
                lPlus1 = l + 1;

                tmp = data[mid];
                data[mid] = data[lPlus1];
                data[lPlus1] = tmp;

                intTmp = indexes[mid];
                indexes[mid] = indexes[lPlus1];
                indexes[lPlus1] = intTmp;

                if (data[lPlus1] > data[r]) {
                    tmp = data[lPlus1];
                    data[lPlus1] = data[r];
                    data[r] = tmp;

                    intTmp = indexes[lPlus1];
                    indexes[lPlus1] = indexes[r];
                    indexes[r] = intTmp;
                }

                if (data[l] > data[r]) {
                    tmp = data[l];
                    data[l] = data[r];
                    data[r] = tmp;

                    intTmp = indexes[l];
                    indexes[l] = indexes[r];
                    indexes[r] = intTmp;
                }

                if (data[lPlus1] > data[l]) {
                    tmp = data[lPlus1];
                    data[lPlus1] = data[l];
                    data[l] = tmp;

                    intTmp = indexes[lPlus1];
                    indexes[lPlus1] = indexes[l];
                    indexes[l] = intTmp;
                }

                i = lPlus1;
                j = r;

                // Knuth initialization
                //i = l;
                //j = r + 1;

                k = data[l];

                Q3bis: // Compare keys[i] : k
                do {
                    i++;
                } while (data[i] > k && i < n);
                //while (i < n && keys[i] > k); // We need to check for i<n in case k is the largest of the keys.
                // No need for this check if Median-of-3 is exploited

                // Here i<=j

                // Q4: Compare k : keys[j]
                do {
                    j--;
                } while (k > data[j]);

                // Here i-1 <= j

                // Q5: Test i : j
                if (i < j) {
                    // Q6: Exchange
                    tmp = data[i];
                    data[i] = data[j];
                    data[j] = tmp;

                    intTmp = indexes[i];
                    indexes[i] = indexes[j];
                    indexes[j] = intTmp;

                    goto Q3bis;
                }
                else {
                    // Here j <= i

                    // Interchange keys[l] with keys[j]
                    tmp = data[l];
                    data[l] = data[j];
                    data[j] = tmp;

                    intTmp = indexes[l];
                    indexes[l] = indexes[j];
                    indexes[j] = intTmp;

                    // Q7: Put on stack
                    jMinusl = j - l;
                    rMinusj = r - j;
                    if (partialQuichSortMaxDataLength < jMinusl && jMinusl <= rMinusj) {

                        // Here both subfile lengths > M
                        // but the greater is that on the right 
                        stack.Push(new QuickSortStackItem(j + 1, r));
                        r = j - 1;
                        goto Q2bis;
                    }
                    else {
                        if (partialQuichSortMaxDataLength < rMinusj && rMinusj < jMinusl) {

                            // Here both subfile lengths > M
                            // but the greater is that on the left 
                            stack.Push(new QuickSortStackItem(l, j - 1));
                            l = j + 1;
                            goto Q2bis;
                        }
                        else {
                            if (jMinusl <= partialQuichSortMaxDataLength && partialQuichSortMaxDataLength < rMinusj) {
                                l = j + 1;
                                goto Q2bis;
                            }
                            else {
                                if (partialQuichSortMaxDataLength < jMinusl && rMinusj <= partialQuichSortMaxDataLength) {
                                    r = j - 1;
                                    goto Q2bis;
                                }
                            }
                        }
                    }

                    // Here both subfile lengths <= M

                    // Q8: Take off stack
                    if (stack.Count > 0) {

                        // Here if the stack is nonempty
                        item = stack.Pop();
                        l = item.m_l;
                        r = item.m_r;
                        goto Q2bis;
                    }
                }
            }

            // Q9:
            //InsertionSort(keys, sortMode);
        }

        /// <summary>
        /// Partially sort data and corresponding indexes in increasing or decreasing order using the QuickSort algorithm.
        /// </summary>
        /// <param name="data">The array to be sorted into increasing order or into decreasing order,
        /// depending on <paramref name="sortDirection"/>.</param>
        /// <param name="sortDirection">The sorting mode (increasing/decreasing) to be applied.</param>
        /// <param name="indexes">The indexes.</param>
        /// <remarks>This method does not execute the final Insertion Sort step, which instead must be called
        /// explicitly.</remarks>
        private static void PartialQuickSort(int[] data, SortDirection sortDirection, int[] indexes)
        {
            /* This implementation follows that described as Algorithm Q in
             * 
             * Knuth, D. The Art of Computer Programming, Vol. 3, Sorting and Searching, p. 115.
             * 
             * However, the search of the pivot element is based on the Median-of-Three approach,
             * as described in 
             * 
             * SedgewicK, u. Implementing QuickSort Programs, Comm. ACM 21, 10 (Oct. 1978), 847-857.
             */

            int r, l, i, j;

            double k;  // This is the pivot element
            int tmp; // swap helper
            int jMinusl, rMinusj;
            int intTmp; // swap helper
            int mid, lPlus1; // median-of-three search helpers
            // Stack initialization
            int n = data.Length;
            int stackCapacity = (int)Math.Floor(Math.Log((n + 1) / (partialQuichSortMaxDataLength + 2), 2.0));
            Stack<QuickSortStackItem> stack = new(stackCapacity);
            QuickSortStackItem item;

            // Q1: Initialize
            l = 0;
            r = n - 1;

            if (sortDirection == SortDirection.Ascending) {
                //unsafe {
                //    fixed (double* keysPointer = &keys[0]) {
                //        fixed (int* indexesPointer = &indexes[0]) {
                Q2: // Begin new stage

                // Median-of-Three search of the pivot element
                mid = (l + r) / 2;
                lPlus1 = l + 1;

                tmp = data[mid];
                data[mid] = data[lPlus1];
                data[lPlus1] = tmp;

                intTmp = indexes[mid];
                indexes[mid] = indexes[lPlus1];
                indexes[lPlus1] = intTmp;

                if (data[lPlus1] > data[r]) {
                    tmp = data[lPlus1];
                    data[lPlus1] = data[r];
                    data[r] = tmp;

                    intTmp = indexes[lPlus1];
                    indexes[lPlus1] = indexes[r];
                    indexes[r] = intTmp;
                }

                if (data[l] > data[r]) {
                    tmp = data[l];
                    data[l] = data[r];
                    data[r] = tmp;

                    intTmp = indexes[l];
                    indexes[l] = indexes[r];
                    indexes[r] = intTmp;
                }

                if (data[lPlus1] > data[l]) {
                    tmp = data[lPlus1];
                    data[lPlus1] = data[l];
                    data[l] = tmp;

                    intTmp = indexes[lPlus1];
                    indexes[lPlus1] = indexes[l];
                    indexes[l] = intTmp;
                }

                i = lPlus1;
                j = r;

                // Knuth initialization
                //i = l;
                //j = r + 1;

                k = data[l];

                Q3: // Compare keys[i] : k
                do {
                    i++;
                } while (data[i] < k && i < n);
                //while (i < n && keys[i] < k); // We need to check for i<n in case k is the largest of the keys.
                // No need for this check if Median-of-3 is exploited

                // Here i<=j

                // Q4: Compare k : keys[j]
                do {
                    j--;
                } while (k < data[j]);

                //Debug.WriteLine("(i, j) = ({0}, {1})", i, j);

                // Here i-1 <= j

                // Q5: Test i : j
                if (i < j) {
                    // Q6: Exchange
                    tmp = data[i];
                    data[i] = data[j];
                    data[j] = tmp;

                    intTmp = indexes[i];
                    indexes[i] = indexes[j];
                    indexes[j] = intTmp;

                    goto Q3;
                }
                else {
                    // Here j <= i

                    // Interchange keys[l] with keys[j]
                    tmp = data[l];
                    data[l] = data[j];
                    data[j] = tmp;

                    intTmp = indexes[l];
                    indexes[l] = indexes[j];
                    indexes[j] = intTmp;

                    // Q7: Put on stack
                    jMinusl = j - l;
                    rMinusj = r - j;
                    if (partialQuichSortMaxDataLength < jMinusl && jMinusl <= rMinusj) {

                        // Here both subfile lengths > M
                        // but the greater is that on the right 
                        stack.Push(new QuickSortStackItem(j + 1, r));
                        r = j - 1;
                        goto Q2;
                    }
                    else {
                        if (partialQuichSortMaxDataLength < rMinusj && rMinusj < jMinusl) {

                            // Here both subfile lengths > M
                            // but the greater is that on the left 
                            stack.Push(new QuickSortStackItem(l, j - 1));
                            l = j + 1;
                            goto Q2;
                        }
                        else {
                            if (jMinusl <= partialQuichSortMaxDataLength && partialQuichSortMaxDataLength < rMinusj) {
                                l = j + 1;
                                goto Q2;
                            }
                            else {
                                if (partialQuichSortMaxDataLength < jMinusl && rMinusj <= partialQuichSortMaxDataLength) {
                                    r = j - 1;
                                    goto Q2;
                                }
                            }
                        }
                    }

                    // Here both subfile lengths <= M

                    // Q8: Take off stack
                    if (stack.Count > 0) {

                        // Here if the stack is nonempty
                        item = stack.Pop();
                        l = item.m_l;
                        r = item.m_r;
                        goto Q2;
                    }
                }

                //        }
                //    }
                //}
            }
            else {
                Q2bis: // Begin new stage

                // Median-of-Three search of the pivot element
                mid = (l + r) / 2;
                lPlus1 = l + 1;

                tmp = data[mid];
                data[mid] = data[lPlus1];
                data[lPlus1] = tmp;

                intTmp = indexes[mid];
                indexes[mid] = indexes[lPlus1];
                indexes[lPlus1] = intTmp;

                if (data[lPlus1] > data[r]) {
                    tmp = data[lPlus1];
                    data[lPlus1] = data[r];
                    data[r] = tmp;

                    intTmp = indexes[lPlus1];
                    indexes[lPlus1] = indexes[r];
                    indexes[r] = intTmp;
                }

                if (data[l] > data[r]) {
                    tmp = data[l];
                    data[l] = data[r];
                    data[r] = tmp;

                    intTmp = indexes[l];
                    indexes[l] = indexes[r];
                    indexes[r] = intTmp;
                }

                if (data[lPlus1] > data[l]) {
                    tmp = data[lPlus1];
                    data[lPlus1] = data[l];
                    data[l] = tmp;

                    intTmp = indexes[lPlus1];
                    indexes[lPlus1] = indexes[l];
                    indexes[l] = intTmp;
                }

                i = lPlus1;
                j = r;

                // Knuth initialization
                //i = l;
                //j = r + 1;

                k = data[l];

                Q3bis: // Compare keys[i] : k
                do {
                    i++;
                } while (data[i] > k && i < n); // We need to check for i<n in case k is the smallest of the keys

                // Here i<=j

                // Q4: Compare k : keys[j]
                do {
                    j--;
                } while (k > data[j]);

                // Here i-1 <= j

                // Q5: Test i : j
                if (i < j) {
                    // Q6: Exchange
                    tmp = data[i];
                    data[i] = data[j];
                    data[j] = tmp;

                    intTmp = indexes[i];
                    indexes[i] = indexes[j];
                    indexes[j] = intTmp;

                    goto Q3bis;
                }
                else {
                    // Here j <= i

                    // Interchange keys[l] with keys[j]
                    tmp = data[l];
                    data[l] = data[j];
                    data[j] = tmp;

                    intTmp = indexes[l];
                    indexes[l] = indexes[j];
                    indexes[j] = intTmp;

                    // Q7: Put on stack
                    jMinusl = j - l;
                    rMinusj = r - j;
                    if (partialQuichSortMaxDataLength < jMinusl && jMinusl <= rMinusj) {

                        // Here both subfile lengths > M
                        // but the greater is that on the right 
                        stack.Push(new QuickSortStackItem(j + 1, r));
                        r = j - 1;
                        goto Q2bis;
                    }
                    else {
                        if (partialQuichSortMaxDataLength < rMinusj && rMinusj < jMinusl) {

                            // Here both subfile lengths > M
                            // but the greater is that on the left 
                            stack.Push(new QuickSortStackItem(l, j - 1));
                            l = j + 1;
                            goto Q2bis;
                        }
                        else {
                            if (jMinusl <= partialQuichSortMaxDataLength && partialQuichSortMaxDataLength < rMinusj) {
                                l = j + 1;
                                goto Q2bis;
                            }
                            else {
                                if (partialQuichSortMaxDataLength < jMinusl && rMinusj <= partialQuichSortMaxDataLength) {
                                    r = j - 1;
                                    goto Q2bis;
                                }
                            }
                        }
                    }

                    // Here both subfile lengths <= M

                    // Q8: Take off stack
                    if (stack.Count > 0) {

                        // Here if the stack is nonempty
                        item = stack.Pop();
                        l = item.m_l;
                        r = item.m_r;
                        goto Q2bis;
                    }
                }
            }

            // Q9:
            //InsertionSort(keys, sortMode);
        }

        /// <summary>
        /// Partially sort data in increasing or decreasing order using the QuickSort algorithm.
        /// </summary>
        /// <param name="data">The array to be sorted into increasing order or into decreasing order, 
        /// depending on <paramref name="sortDirection"/>.</param>
        /// <param name="sortDirection">The sorting mode (increasing/decreasing) to be applied.</param>
        /// <remarks>This method does not execute the final Insertion Sort step, which instead must be called
        /// explicitly.</remarks>
        private static void PartialQuickSort(double[] data, SortDirection sortDirection)
        {
            /* This implementation follows that described as Algorithm Q in
             * 
             * Knuth, D. The Art of Computer Programming, Vol. 3, Sorting and Searching, p. 115.
             * 
             * However, the search of the pivot element is based on the Median-of-Three approach,
             * as described in 
             * 
             * Sedgewick, u. Implementing QuickSort Programs, Comm. ACM 21, 10 (Oct. 1978), 847-857.
             */

            int r, l, i, j;

            double k;  // This is the pivot element
            double tmp; // swap helper
            int jMinusl, rMinusj;

            // Stack initialization
            int n = data.Length;

            int stackCapacity = (int)Math.Floor(Math.Log((n + 1) / (partialQuichSortMaxDataLength + 2), 2.0));
            Stack<QuickSortStackItem> stack = new(stackCapacity);
            QuickSortStackItem item;

            // Q1: Initialize
            l = 0;
            r = n - 1;

            if (sortDirection == SortDirection.Ascending) {
                Q2: // Begin new stage

                // Median-of-Three search of the pivot element
                int mid = (l + r) / 2;
                int lPlus1 = l + 1;

                tmp = data[mid];
                data[mid] = data[lPlus1];
                data[lPlus1] = tmp;

                if (data[lPlus1] > data[r]) {
                    tmp = data[lPlus1];
                    data[lPlus1] = data[r];
                    data[r] = tmp;
                }

                if (data[l] > data[r]) {
                    tmp = data[l];
                    data[l] = data[r];
                    data[r] = tmp;
                }

                if (data[lPlus1] > data[l]) {
                    tmp = data[lPlus1];
                    data[lPlus1] = data[l];
                    data[l] = tmp;
                }
                i = lPlus1;
                j = r;

                // Knuth's initialization
                //i = l;
                //j = r + 1;

                k = data[l];

                Q3: // Compare keys[i] : k
                do {
                    i++;
                } while (data[i] < k && i < n);
                // We need to check for i<n in case k is the largest of the keys.
                // No need for this check if Median-of-3 is exploited

                // Here i <= j

                // Q4: Compare k : keys[j]
                do {
                    j--;
                } while (k < data[j]);

                //Debug.WriteLine("(i, j) = ({0}, {1})", i, j);

                // Here i - 1 <= j

                // Q5: Test i : j
                if (i < j) {
                    // Q6: Exchange
                    tmp = data[i];
                    data[i] = data[j];
                    data[j] = tmp;
                    goto Q3;
                }
                else {
                    // Here j <= i

                    // Interchange keys[l] with keys[j]
                    tmp = data[l];
                    data[l] = data[j];
                    data[j] = tmp;

                    // Q7: Put on stack
                    jMinusl = j - l;
                    rMinusj = r - j;
                    if (partialQuichSortMaxDataLength < jMinusl && jMinusl <= rMinusj) {

                        // Here both subfile lengths > M
                        // but the greater is that on the right 
                        stack.Push(new QuickSortStackItem(j + 1, r));
                        r = j - 1;
                        goto Q2;
                    }
                    else {
                        if (partialQuichSortMaxDataLength < rMinusj && rMinusj < jMinusl) {

                            // Here both subfile lengths > M
                            // but the greater is that on the left 
                            stack.Push(new QuickSortStackItem(l, j - 1));
                            l = j + 1;
                            goto Q2;
                        }
                        else {
                            if (jMinusl <= partialQuichSortMaxDataLength && partialQuichSortMaxDataLength < rMinusj) {
                                l = j + 1;
                                goto Q2;
                            }
                            else {
                                if (partialQuichSortMaxDataLength < jMinusl && rMinusj <= partialQuichSortMaxDataLength) {
                                    r = j - 1;
                                    goto Q2;
                                }
                            }
                        }
                    }

                    // Here both subfile lengths <= M

                    // Q8: Take off stack
                    if (stack.Count > 0) {

                        // Here if the stack is nonempty
                        item = stack.Pop();
                        l = item.m_l;
                        r = item.m_r;
                        goto Q2;
                    }
                }
            }
            else {
                Q2bis: // Begin new stage

                // Median-of-Three search of the pivot element
             
                i = l;
                j = r + 1;
                k = data[l];

                Q3bis: // Compare keys[i] : k
                do {
                    i++;
                } while (i < n && data[i] > k); 
                // We need to check for i<n in case k is the smallest of the keys

                // Here i<=j

                // Q4: Compare k : keys[j]
                do {
                    j--;
                } while (k > data[j]);

                // Here i-1 <= j

                // Q5: Test i : j
                if (i < j) {
                    // Q6: Exchange
                    tmp = data[i];
                    data[i] = data[j];
                    data[j] = tmp;
                    goto Q3bis;
                }
                else {
                    // Here j <= i

                    // Interchange keys[l] with keys[j]
                    tmp = data[l];
                    data[l] = data[j];
                    data[j] = tmp;

                    // Q7: Put on stack
                    jMinusl = j - l;
                    rMinusj = r - j;
                    if (partialQuichSortMaxDataLength < jMinusl && jMinusl <= rMinusj) {

                        // Here both subfile lengths > M
                        // but the greater is that on the right 
                        stack.Push(new QuickSortStackItem(j + 1, r));
                        r = j - 1;
                        goto Q2bis;
                    }
                    else {
                        if (partialQuichSortMaxDataLength < rMinusj && rMinusj < jMinusl) {

                            // Here both subfile lengths > M
                            // but the greater is that on the left 
                            stack.Push(new QuickSortStackItem(l, j - 1));
                            l = j + 1;
                            goto Q2bis;
                        }
                        else {
                            if (jMinusl <= partialQuichSortMaxDataLength && partialQuichSortMaxDataLength < rMinusj) {
                                l = j + 1;
                                goto Q2bis;
                            }
                            else {
                                if (partialQuichSortMaxDataLength < jMinusl && rMinusj <= partialQuichSortMaxDataLength) {
                                    r = j - 1;
                                    goto Q2bis;
                                }
                            }
                        }
                    }

                    // Here both subfile lengths <= M

                    // Q8: Take off stack
                    if (stack.Count > 0) {

                        // Here if the stack is nonempty
                        item = stack.Pop();
                        l = item.m_l;
                        r = item.m_r;
                        goto Q2bis;
                    }
                }
            }

            // Q9:
            //InsertionSort(keys, sortMode);
        }

        #endregion

        #region InsertionSort

        /// <summary>
        /// Sort data in increasing or decreasing order using the Insertion Sort algorithm.
        /// </summary>
        /// <param name="data">The array to be sorted into increasing order or into decreasing order, 
        /// depending on <paramref name="sortDirection"/>.</param>
        /// <param name="sortDirection">The sorting mode (increasing/decreasing) to be applied.</param>
        public static void InsertionSort(double[] data, SortDirection sortDirection)
        {
            int i, j;
            double k;

            if (SortDirection.Ascending == sortDirection) {

                for (j = 1; j < data.Length; j++) {

                    if (data[j - 1] > data[j]) {
                        k = data[j];
                        i = j - 1;
                        while (i >= 0 && data[i] > k) {
                            data[i + 1] = data[i];

                            i--;
                        }
                        data[i + 1] = k;
                    }
                }
            }
            else {

                for (j = 1; j < data.Length; j++) {

                    if (data[j - 1] < data[j]) {
                        k = data[j];
                        i = j - 1;
                        while (i >= 0 && data[i] < k) {
                            data[i + 1] = data[i];

                            i--;
                        }
                        data[i + 1] = k;
                    }
                }
            }
        }

        internal static void InsertionSort(
            int[] keys, 
            SortDirection sortDirection, 
            int[] indexTable)
        {
            int i, j;

            int k;
            int t;

            if (SortDirection.Ascending == sortDirection) {

                for (j = 1; j < keys.Length; j++) {

                    if (keys[j - 1] > keys[j]) {
                        k = keys[j];
                        t = indexTable[j];
                        i = j - 1;
                        while (i >= 0 && keys[i] > k) {
                            keys[i + 1] = keys[i];
                            indexTable[i + 1] = indexTable[i];
                            i--;
                        }
                        keys[i + 1] = k;
                        indexTable[i + 1] = t;
                    }
                }
            }
            else {

                for (j = 1; j < keys.Length; j++) {

                    if (keys[j - 1] < keys[j]) {
                        k = keys[j];
                        t = indexTable[j];
                        i = j - 1;
                        while (i >= 0 && keys[i] < k) {
                            keys[i + 1] = keys[i];
                            indexTable[i + 1] = indexTable[i];

                            i--;
                        }
                        keys[i + 1] = k;
                        indexTable[i + 1] = t;
                    }
                }

            }
        }

        internal static void InsertionSort(
            double[] keys, 
            SortDirection sortDirection, 
            int[] indexTable)
        {
            int i, j;

            double k;
            int t;

            if (SortDirection.Ascending == sortDirection) {

                for (j = 1; j < keys.Length; j++) {

                    if (keys[j - 1] > keys[j]) {
                        k = keys[j];
                        t = indexTable[j];
                        i = j - 1;
                        while (i >= 0 && keys[i] > k) {
                            keys[i + 1] = keys[i];
                            indexTable[i + 1] = indexTable[i];
                            i--;
                        }
                        keys[i + 1] = k;
                        indexTable[i + 1] = t;
                    }
                }
            }
            else {

                for (j = 1; j < keys.Length; j++) {

                    if (keys[j - 1] < keys[j]) {
                        k = keys[j];
                        t = indexTable[j];
                        i = j - 1;
                        while (i >= 0 && keys[i] < k) {
                            keys[i + 1] = keys[i];
                            indexTable[i + 1] = indexTable[i];

                            i--;
                        }
                        keys[i + 1] = k;
                        indexTable[i + 1] = t;
                    }
                }
            }
        }

        #endregion
    }
}

