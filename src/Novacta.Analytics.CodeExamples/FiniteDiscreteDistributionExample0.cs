using Novacta.Documentation.CodeExamples;
using System;

namespace Novacta.Analytics.CodeExamples
{
    public class FiniteDiscreteDistributionExample0 : ICodeExample
    {
        public void Main()
        {
            // Create the values.
            var values = DoubleMatrix.Dense(
                3, 2, new double[6] { 2, -1, 0, 1, 3, -4 });

            // Create the corresponding probabilities.
            var probabilities = DoubleMatrix.Dense(
                3, 2, new double[6] { 1.0 / 8, 0.0, 2.0 / 8, 1.0 / 8, 3.0 / 8, 1.0 / 8 });

            // Create the finite discrete distribution.
            var distribution = new FiniteDiscreteDistribution(values, probabilities);
            Console.WriteLine("Values:");
            Console.WriteLine(distribution.Values);
            Console.WriteLine();
            Console.WriteLine("Probabilities:");
            Console.WriteLine(distribution.Masses);
            Console.WriteLine();

            // Compute the mean value.
            double mean = distribution.Mean();
            Console.WriteLine("Expected value: {0}", mean);
            Console.WriteLine();

            // Draw a sample from the distribution.
            int sampleSize = 100000;
            var sample = distribution.Sample(sampleSize);

            // Compute the sample mean.
            double sampleMean = Stat.Mean(sample);
            Console.WriteLine("Sample mean: {0}", sampleMean);
            Console.WriteLine();

            // Get a specific value.
            int valueIndex = 3;
            double value = distribution.Values[valueIndex];

            // Get its mass.
            double mass = distribution.Masses[valueIndex];
            Console.WriteLine("Mass of value {0}: {1}", value, mass);
            Console.WriteLine();

            // Compute its sample frequency.
            IndexCollection valuePositions = sample.Find(value);
            double frequency = (valuePositions == null) ? 0.0 
                : (double)valuePositions.Count / sampleSize;
            Console.WriteLine("Sample frequency of value {0}: {1}", value, frequency);
        }
    }
}