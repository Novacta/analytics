﻿using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class StandardDeviationExample1 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix.
            var data = new double[6] {
                1,  2,
                2, -3,
                3,  4,
            };
            var matrix = DoubleMatrix.Dense(3, 2, data, StorageOrder.RowMajor);
            Console.WriteLine("The data matrix:");
            Console.WriteLine(matrix);

            // StandardDeviation can be adjusted for bias.
            bool adjustForBias = false; 

            // Compute the data standard deviation.
            var stdDev = Stat.StandardDeviation(matrix, adjustForBias);

            Console.WriteLine();
            Console.WriteLine("Data standard deviation is:");
            Console.WriteLine(stdDev);

            // StandardDeviation is overloaded to accept data as a read-only matrix:
            // compute the standard deviation using a read-only wrapper of the data matrix.
            ReadOnlyDoubleMatrix readOnlyMatrix = matrix.AsReadOnly();
            var stdDevOfReadOnlyData = Stat.StandardDeviation(readOnlyMatrix, adjustForBias);

            Console.WriteLine();
            Console.WriteLine("Using read-only data. The standard deviation is:");
            Console.WriteLine(stdDevOfReadOnlyData);
        }
    }
}