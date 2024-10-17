using Novacta.Documentation.CodeExamples;
using System;
using System.Collections.Generic;

namespace Novacta.Analytics.CodeExamples
{
    public class CategoricalEntailmentEnsembleClassifierExample0 : ICodeExample
    {
        public void Main()
        {
            // Create the feature variables.
            CategoricalVariable f0 = new("F-0")
            {
                { 0, "A" },
                { 1, "B" },
                { 2, "C" },
                { 3, "D" },
                { 4, "E" }
            };
            f0.SetAsReadOnly();

            CategoricalVariable f1 = new("F-1")
            {
                { 0, "I" },
                { 1, "II" },
                { 2, "III" },
                { 3, "IV" }
            };
            f1.SetAsReadOnly();

            // Create the response variable.
            CategoricalVariable r = new("R")
            {
                0,
                1,
                2
            };
            r.SetAsReadOnly();

            // Create a categorical data set containing
            // observations about such variables.
            List<CategoricalVariable> variables =
                [f0, f1, r];

            DoubleMatrix data = DoubleMatrix.Dense(
                new double[20, 3] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 2, 2 },
                    { 0, 3, 2 },

                    { 1, 0, 0 },
                    { 1, 1, 0 },
                    { 1, 2, 2 },
                    { 1, 3, 2 },

                    { 2, 0, 0 },
                    { 2, 1, 0 },
                    { 2, 2, 2 },
                    { 2, 3, 2 },

                    { 3, 0, 1 },
                    { 3, 1, 1 },
                    { 3, 2, 1 },
                    { 3, 3, 1 },

                    { 4, 0, 1 },
                    { 4, 1, 1 },
                    { 4, 2, 1 },
                    { 4, 3, 1 } });

            CategoricalDataSet dataSet = CategoricalDataSet.FromEncodedData(
                variables,
                data);

            // Train a classifier on the specified data set.
            var classifier = CategoricalEntailmentEnsembleClassifier.Train(
                dataSet: dataSet,
                featureVariableIndexes: IndexCollection.Range(0, 1),
                responseVariableIndex: 2,
                numberOfTrainedCategoricalEntailments: 3,
                allowEntailmentPartialTruthValues: false,
                trainSequentially: true);

            // Show the ensemble of categorical entailments
            // in the trained classifier.
            foreach (var entailment in classifier.Entailments)
            {
                Console.WriteLine(entailment);
            }

            // Classify the items in the data set.
            var predicted = classifier.Classify(dataSet, IndexCollection.Range(0, 1));

            // Evaluate and show the accuracy of the predicted classification 
            // with respect to the actual one.
            var actual = dataSet[":", 2];

            var accuracy = CategoricalEntailmentEnsembleClassifier.EvaluateAccuracy(
                predictedDataSet: predicted,
                predictedResponseVariableIndex: 0,
                actualDataSet: actual,
                actualResponseVariableIndex: 0);
            Console.WriteLine("Accuracy: {0}", accuracy);
        }
    }
}
