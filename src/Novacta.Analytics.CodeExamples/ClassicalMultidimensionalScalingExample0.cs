﻿using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class ClassicalMultidimensionalScalingExample0 : ICodeExample
    {
        public void Main()
        {
            // Create a matrix of proximities among cities.
            int numberOfCities = 10;

            string[] cities = [
                "Atlanta",
                "Chicago",
                "Denver",
                "Houston",
                "Los Angeles",
                "Miami",
                "New York",
                "San Francisco",
                "Seattle",
                "Washington DC"];

            var proximities = DoubleMatrix.Dense(
                numberOfRows: numberOfCities,
                numberOfColumns: numberOfCities,
                data: [
                      0,  587, 1212,  701, 1936,  604,  748, 2139, 2182,  543,
                    587,    0,  920,  940, 1745, 1188,  713, 1858, 1737,  597,
                   1212,  920,    0,  879,  831, 1726, 1631,  949, 1021, 1494,
                    701,  940,  879,    0, 1374,  968, 1420, 1645, 1891, 1220,
                   1936, 1745,  831, 1374,    0, 2339, 2451,  347,  959, 2300,
                    604, 1188, 1726,  968, 2339,    0, 1092, 2594, 2734,  923,
                    748,  713, 1631, 1420, 2451, 1092,    0, 2571, 2408,  205,
                   2139, 1858,  949, 1645,  347, 2594, 2571,    0,  678, 2442,
                   2182, 1737, 1021, 1891,  959, 2734, 2408,  678,    0, 2329,
                    543,  597, 1494, 1220, 2300,  923,  205, 2442, 2329,    0],
                storageOrder: StorageOrder.RowMajor);

            // Add city names to the matrix of proximities.
            for (int i = 0; i < numberOfCities; i++)
            {
                var city = cities[i];
                proximities.SetRowName(i, city);
                proximities.SetColumnName(i, city);
            }

            // Define the dimension of the configuration of points in the target space.
            // Passing null, the dimension of the configuration is automatically selected.
            int? configurationDimension = null;

            // Execute a classical MDS analysis.
            var metricMds =
                ClassicalMultidimensionalScaling.Analyze(
                    proximities,
                    configurationDimension);

            // Display the configuration.
            Console.WriteLine("City coordinates:");
            Console.WriteLine(metricMds.Configuration);

            // Display the goodness of fit.
            Console.WriteLine("Goodness of fit:");
            Console.WriteLine(metricMds.GoodnessOfFit);
        }
    }
}
