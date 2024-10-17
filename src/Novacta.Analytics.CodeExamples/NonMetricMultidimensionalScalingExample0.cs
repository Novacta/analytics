using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class NonMetricMultidimensionalScalingExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix representing a data set about breakfast cereals
            // from Kellogg's manufacturer (adapted
            // from https://lib.stat.cmu.edu/datasets/1993.expo/).
            // It contains the following variables: number of calories,
            // protein, fat, sodium, fiber, carbo, sugars, shelf,
            // potassium, and vitamins of 23 food items.
            var data = DoubleMatrix.Dense(
                numberOfRows: 23,
                numberOfColumns: 10,
                data: [
                     70, 4, 1, 260,  9,  7,   5,  3,   320,  25,
                     50, 4, 0, 140, 14,  8,   0,  3,   330,  25,
                    110, 2, 0, 125,  1, 11,  14,  2,    30,  25,
                    100, 2, 0, 290,  1, 21,   2,  1,    35,  25,
                    110, 1, 0,  90,  1, 13,  12,  2,    20,  25,
                    110, 3, 3, 140,  4, 10,   7,  3,   160,  25,
                    110, 2, 0, 220,  1, 21,   3,  3,    30,  25,
                    110, 2, 1, 125,  1, 11,  13,  2,    30,  25,
                    110, 1, 0, 200,  1, 14,  11,  1,    25,  25,
                    100, 3, 0,   0,  3, 14,   7,  2,   100,  25,
                    120, 3, 0, 240,  5, 14,  12,  3,   190,  25,
                    110, 2, 1, 170,  1, 17,   6,  3,    60, 100,
                    140, 3, 1, 170,  2, 20,   9,  3,    95, 100,
                    160, 3, 2, 150,  3, 17,  13,  3,   160,  25,
                    120, 2, 1, 190,  0, 15,   9,  2,    40,  25,
                    140, 3, 2, 220,  3, 21,   7,  3,   130,  25,
                    90 , 3, 0, 170,  3, 18,   2,  3,    90,  25,
                    100, 3, 0, 320,  1, 20,   3,  3,    45, 100,
                    120, 3, 1, 210,  5, 14,  12,  2,   240,  25,
                     90, 2, 0,   0,  2, 15,   6,  3,   110,  25,
                    110, 2, 0, 290,  0, 22,   3,  1,    35,  25,
                    110, 2, 1,  70,  1,  9,  15,  2,    40,  25,
                    110, 6, 0, 230,  1, 16,   3,  1,    55,  25],
                storageOrder: StorageOrder.RowMajor);

            // Set variable names.
            string[] variables = [
                "Calories",
                "Protein",
                "Fat",
                "Sodium",
                "Fiber",
                "Carbo",
                "Sugars",
                "Shelf",
                "Potassium",
                "Vitamins"];

            for (int j = 0; j < 10; j++)
            {
                data.SetColumnName(j, variables[j]);
            }

            // Create a matrix of dissimilarities among data items.
            var dissimilarities = Distance.Euclidean(data);

            // Define the dimension of the configuration of points in
            // the target space.
            // Passing null, the dimension of the configuration is
            // automatically selected.
            int? configurationDimension = 2;

            // Define the Minkowski metric order.
            double minkowskiMetricOrder = 2.0;

            // Execute the nonmetric MDS analysis.
            var results =
                NonMetricMultidimensionalScaling.Analyze(
                    dissimilarities,
                    configurationDimension,
                    minkowskiMetricOrder,
                    maximumNumberOfIterations: 1000,
                    terminationTolerance: 1e-5);

            // Display the optimal configuration.
            Console.WriteLine("Optimal configuration:");
            Console.WriteLine(results.Configuration);            
            
            // Display the stress at the optimal configuration.
            Console.WriteLine("Optimal Stress:");
            Console.WriteLine(results.Stress);
            Console.WriteLine();

            // Display a value indicating if the optimization algorithm
            // has converged.
            Console.WriteLine("Optimization convergence:");
            Console.WriteLine(results.HasConverged);
        }
    }
}
