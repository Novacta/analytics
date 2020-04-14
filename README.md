# Novacta.Analytics

The **Novacta.Analytics** library
provides functionality for data analysis. 

The project targets all platforms implementing
the [.NET Standard](https://github.com/dotnet/standard), 
version 2.0, and supports the
[x86 architecture](https://en.wikipedia.org/wiki/X86):
64 bit is supported on all platforms, 32 bit on Windows only.

Installation can be performed via [NuGet](https://www.nuget.org/packages/Novacta.Analytics).

# Features

## Matrix algebra operations

* [Read only](http://novacta.github.io/analytics/html/T_Novacta_Analytics_ReadOnlyDoubleMatrix.htm)
  or
  [writable](http://novacta.github.io/analytics/html/T_Novacta_Analytics_DoubleMatrix.htm)
  matrices, supporting
  [Sparse](http://novacta.github.io/analytics/html/M_Novacta_Analytics_DoubleMatrix_Sparse.htm)
  and
  [Dense](http://novacta.github.io/analytics/html/M_Novacta_Analytics_DoubleMatrix_Dense_1.htm)
  storage schemes.
* [Overloaded operators](http://novacta.github.io/analytics/html/Operators_T_Novacta_Analytics_DoubleMatrix.htm)
  supporting sums, subtractions, multiplications, and divisions whose operands 
  are matrices or scalars.
* Matrix slicing where rows or columns are referred to
  using integers, strings, or
  [collections of indexes](http://novacta.github.io/analytics/html/T_Novacta_Analytics_IndexCollection.htm),
  possibly avoiding dense allocations via specialized
  [indexers](http://novacta.github.io/analytics/html/P_Novacta_Analytics_DoubleMatrix_Item_1.htm).
* [Out-](http://novacta.github.io/analytics/html/M_Novacta_Analytics_DoubleMatrix_Apply.htm)
  or
  [In-](http://novacta.github.io/analytics/html/M_Novacta_Analytics_DoubleMatrix_InPlaceApply.htm)
  place application of operations to matrix entries.
   
## Matrix data presentation and interaction in application UI

* Matrices can be exploited as
  [binding sources](https://docs.microsoft.com/en-us/dotnet/desktop-wpf/data/data-binding-overview#basic-data-binding-concepts)
  by interpreting them as
  [collections](http://novacta.github.io/analytics/html/T_Novacta_Analytics_DoubleMatrixRowCollection.htm)
  of
  [rows](http://novacta.github.io/analytics/html/T_Novacta_Analytics_DoubleMatrixRow.htm).
  See the
  [BindingToRowCollection](https://github.com/novacta/analytics/blob/master/samples/BindingToRowCollection)
  sample for further details.

## Summary statistics

* [Descriptive statistical functions](http://novacta.github.io/analytics/html/T_Novacta_Analytics_Stat.htm),
  operating on rows, columns, or all matrix entries.

## Multivariate data analysis

* Represent multi-dimensional, weighted points by taking
  their coordinates with respect to whatever
  [basis](http://novacta.github.io/analytics/html/T_Novacta_Analytics_Advanced_Basis.htm)
  using
  [cloud](http://novacta.github.io/analytics/html/T_Novacta_Analytics_Advanced_Cloud.htm)
  instances.
* Project clouds along their
  [principal](http://novacta.github.io/analytics/html/T_Novacta_Analytics_PrincipalProjections.htm)
  directions by identifying new, uncorrelated variables
  whose variances enhance our comprehension of the overall cloud
  variability, possibly approximating the cloud in a lower dimensional space.
* Compute the
  [principal components](http://novacta.github.io/analytics/html/T_Novacta_Analytics_PrincipalComponents.htm)
  of a matrix, an application of principal projections
  to the classical context in which matrix rows are
  interpreted as point coordinates taken with respect to
  bases depending on specific coefficients assigned to the
  observed variables.
* [Correspondence](http://novacta.github.io/analytics/html/T_Novacta_Analytics_Correspondence.htm)
  analysis of a contingency table.
* Cluster Analysis
  * [Discover](http://novacta.github.io/analytics/html/M_Novacta_Analytics_Clusters_Discover.htm)
    optimal clusters in a data set by minimizing the sum of
    intra-cluster squared deviations.
  * [Explain](http://novacta.github.io/analytics/html/M_Novacta_Analytics_Clusters_Explain.htm)
    existing clusters selecting features by minimizing the
    [Davies-Bouldin index](http://novacta.github.io/analytics/html/M_Novacta_Analytics_IndexPartition_DaviesBouldinIndex.htm).

## Categorical data sets

* Create
  [categorical data sets](http://novacta.github.io/analytics/html/T_Novacta_Analytics_CategoricalDataSet.htm)
  by
  [encoding](http://novacta.github.io/analytics/html/M_Novacta_Analytics_CategoricalDataSet_Encode_1.htm)
  categorical or numerical data from a stream.
* Categorize continuous data
  [by entropy minimization](http://novacta.github.io/analytics/html/M_Novacta_Analytics_CategoricalDataSet_CategorizeByEntropyMinimization.htm).
* [Multiple Correspondence](http://novacta.github.io/analytics/html/T_Novacta_Analytics_MultipleCorrespondence.htm)
  analysis of a categorical data set.
* Classify items from a feature space via
  [ensembles](http://novacta.github.io/analytics/html/T_Novacta_Analytics_CategoricalEntailmentEnsembleClassifier.htm)
  of
  [categorical entailments](http://novacta.github.io/analytics/html/T_Novacta_Analytics_CategoricalEntailment.htm).

## Randomization

* Use Mersenne Twister
  [random number generators](http://novacta.github.io/analytics/html/T_Novacta_Analytics_RandomNumberGenerator.htm)
  to draw samples from basic statistical distributions.
* Represent additional
  [probability distributions](http://novacta.github.io/analytics/html/T_Novacta_Analytics_ProbabilityDistribution.htm)
  to compute and sample from specific cumulative or probability density
  functions.
* Select
  [random samples](http://novacta.github.io/analytics/html/T_Novacta_Analytics_RandomSampling.htm)
  from a finite population, with
  [equal](http://novacta.github.io/analytics/html/T_Novacta_Analytics_SimpleRandomSampling.htm)
  or
  [unequal](http://novacta.github.io/analytics/html/T_Novacta_Analytics_UnequalProbabilityRandomSampling.htm)
  probabilities of being inserted in a sample.
* [Permute randomly](http://novacta.github.io/analytics/html/T_Novacta_Analytics_RandomIndexPermutation.htm)
  a given collection of integers.

## Optimization

* [Optimize continuous functions](http://novacta.github.io/analytics/html/T_Novacta_Analytics_ContinuousOptimization.htm)
  having multiple arguments ranging over the real line.
* Apply the Cross-Entropy method to
  represent continuous or combinatorial optimization problems via 
  [Cross-Entropy contexts for optimization](http://novacta.github.io/analytics/html/T_Novacta_Analytics_Advanced_SystemPerformanceOptimizationContext.htm),
  and solve them using a
  [Cross-Entropy optimizer](http://novacta.github.io/analytics/html/T_Novacta_Analytics_Advanced_SystemPerformanceOptimizer.htm).
* Take advantage of specialized Cross-Entropy contexts to
  optimize functions having as arguments
  [continuous variables](http://novacta.github.io/analytics/html/T_Novacta_Analytics_Advanced_ContinuousOptimizationContext.htm),
  [combinations](http://novacta.github.io/analytics/html/T_Novacta_Analytics_Advanced_CombinationOptimizationContext.htm),
  [partitions](http://novacta.github.io/analytics/html/T_Novacta_Analytics_Advanced_PartitionOptimizationContext.htm),
  or
  [ensembles of categorical entailments](http://novacta.github.io/analytics/html/T_Novacta_Analytics_Advanced_CategoricalEntailmentEnsembleOptimizationContext.htm).

## Rare event simulation

* Express the problem of estimating the probability of
  rare events via 
  [Cross-Entropy contexts for rare event simulation](http://novacta.github.io/analytics/html/T_Novacta_Analytics_Advanced_RareEventProbabilityEstimationContext.htm),
  solvable using a
  [Cross-Entropy estimator](http://novacta.github.io/analytics/html/T_Novacta_Analytics_Advanced_RareEventProbabilityEstimator.htm).

## Documentation

The current documentation includes the following topics.

* [Novacta.Analytics release notes](http://novacta.github.io/analytics/html/41221eed-891e-4925-a654-ecf8b2d38c65.htm).
* [Testing environment](http://novacta.github.io/analytics/html/07a926cb-9c3c-432d-998b-0af7eea037f6.htm)
  instructs about the setup required
  to test the assembly.
* [Novacta.Analytics namespaces](http://novacta.github.io/analytics/html/G_Novacta_Analytics.htm)
  contains reference information about library
  types by namespace.

## Versioning

We use [SemVer](http://semver.org/) for versioning. 
For available versions, see the 
[tags on this repository](https://github.com/novacta/analytics/tags). 

## Copyrights and Licenses

All source code is Copyright (c) 2020 Giovanni Lafratta.

**Novacta.Analytics** is licensed under the 
[MIT License](https://github.com/novacta/analytics/blob/master/LICENSE.md). 

This project relies on native dynamic-link libraries obtained via the Intel® MKL custom DLL builder.
[MKL](https://software.intel.com/en-us/mkl) is Copyright (c) 2018 Intel Corporation and
licensed under the
[ISSL](https://software.intel.com/en-us/license/intel-simplified-software-license)
terms.