# Novacta.Analytics

The **Novacta.Analytics** library
provides functionality for data analysis.

The project targets
[.NET 8](https://github.com/dotnet/core/blob/main/release-notes/8.0/README.md),
and supports the
[x86-64 architecture](https://en.wikipedia.org/wiki/X86-64)
on Windows, Linux, and macOS platforms.

Installation can be performed via [NuGet](https://www.nuget.org/packages/Novacta.Analytics).

## Features

### Matrix algebra operations

* Matrices of
  [Double](https://docs.microsoft.com/dotnet/api/system.double) 
  or
  [Complex](https://docs.microsoft.com/dotnet/api/system.numerics.complex)
  numbers, represented through types
  [DoubleMatrix](https://novacta.github.io/analytics/html/1DED9EB1.htm) and
  [ComplexMatrix](https://novacta.github.io/analytics/html/AFDA21E4.htm),
  respectively.
* Support for both dense
  and 
  [Compressed sparse row](https://en.wikipedia.org/wiki/Sparse_matrix#Compressed_sparse_row_(CSR,_CRS_or_Yale_format))
  storage 
  [schemes](https://novacta.github.io/analytics/html/6274B7FA.htm).
* Matrices exposed as read-only objects, see types
  [ReadOnlyDoubleMatrix](https://novacta.github.io/analytics/html/9FFC4131.htm)
  and 
  [ReadOnlyComplexMatrix](https://novacta.github.io/analytics/html/E2E3D527.htm).
* Overloaded operators
  supporting sums, subtractions, multiplications, and divisions whose operands
  are (writable or read-only) matrices or scalars, eventually mixing Complex or
  Double based operands.
* Matrix slicing where rows or columns are referred to
  using integers, strings, or
  [collections of indexes](https://novacta.github.io/analytics/html/9B3BDFD7.htm).
* Out- or In- place application
  of operations to matrix entries (for examples, see methods
  [Apply](https://novacta.github.io/analytics/html/4A64A4C1.htm)
  or
  [InPlaceApply](https://novacta.github.io/analytics/html/7887E503.htm)).
* Matrix
  [Singular Value Decompositions](https://novacta.github.io/analytics/html/D334409A.htm)
  and
  [Spectral Decompositions](https://novacta.github.io/analytics/html/17359C6B.htm)
  of symmetric/Hermitian matrices.
* Create matrices by
  [encoding](https://novacta.github.io/analytics/html/B1082BD8.htm)
  numerical or categorical data.

### Matrix data presentation and interaction in application UI

* Matrices can be exploited as
  [binding sources](https://docs.microsoft.com/en-us/dotnet/desktop-wpf/data/data-binding-overview#basic-data-binding-concepts)
  by interpreting them as
  [collections](https://novacta.github.io/analytics/html/CF178F79.htm)
  of
  [rows](https://novacta.github.io/analytics/html/8C5BBFE.htm).
  See the
  [BindingToRowCollection](https://github.com/novacta/analytics/blob/master/samples/BindingToRowCollection)
  sample for further details.

### Summary statistics

* [Descriptive statistical functions](https://novacta.github.io/analytics/html/ADDA5F5.htm),
  operating on rows, columns, or all matrix entries.

### Multivariate data analysis

* Represent multi-dimensional, weighted points by taking
  their coordinates with respect to whatever
  [basis](https://novacta.github.io/analytics/html/59623234.htm)
  using
  [cloud](https://novacta.github.io/analytics/html/781E2F6F.htm)
  instances.
* Project clouds along their
  [principal](https://novacta.github.io/analytics/html/3ADFD77D.htm)
  directions by identifying new, uncorrelated variables
  whose variances enhance our comprehension of the overall cloud
  variability, possibly approximating the cloud in a lower dimensional space.
* Multidimensional scaling
  * [Classical](https://novacta.github.io/analytics/html/F58681BC.htm)
    scaling of a proximity matrix.
  * [Nonmetric](https://novacta.github.io/analytics/html/4FE4ECE4.htm)
    scaling of a dissimilarity matrix.
* Compute the
  [principal components](https://novacta.github.io/analytics/html/3ADC7B56.htm)
  of a matrix, an application of principal projections
  to the classical context in which matrix rows are
  interpreted as point coordinates taken with respect to
  bases depending on specific coefficients assigned to the
  observed variables.
* [Correspondence](https://novacta.github.io/analytics/html/8945AB1F.htm)
  analysis of a contingency table.
* Cluster Analysis
  * [Discover](https://novacta.github.io/analytics/html/32B0ECC0.htm)
    optimal clusters in a data set by minimizing the sum of
    intra-cluster squared deviations.
  * [Explain](https://novacta.github.io/analytics/html/76A18A84.htm)
    existing clusters selecting features by minimizing the
    [Davies-Bouldin index](https://novacta.github.io/analytics/html/40CF1518.htm).

### Categorical data sets

* Create
  [categorical data sets](https://novacta.github.io/analytics/html/B39F799B.htm)
  by
  [encoding](https://novacta.github.io/analytics/html/77DAF585.htm)
  categorical or numerical data from a stream.
* Categorize continuous data
  [by entropy minimization](https://novacta.github.io/analytics/html/5D736549.htm).
* [Multiple Correspondence](https://novacta.github.io/analytics/html/C792CEE0.htm)
  analysis of a categorical data set.
* Classify items from a feature space via
  [ensembles](https://novacta.github.io/analytics/html/401D2F27.htm)
  of
  [categorical entailments](https://novacta.github.io/analytics/html/39D67B46.htm).

### Randomization

* Use Mersenne Twister
  [random number generators](https://novacta.github.io/analytics/html/E076C0AC.htm)
  to draw samples from basic statistical distributions.
* Represent additional
  [probability distributions](https://novacta.github.io/analytics/html/1984D730.htm)
  to compute and sample from specific cumulative or probability density
  functions.
* Select
  [random samples](https://novacta.github.io/analytics/html/FCF45A04.htm)
  from a finite population, with
  [equal](https://novacta.github.io/analytics/html/37F088DE.htm)
  or
  [unequal](https://novacta.github.io/analytics/html/EC35632C.htm)
  probabilities of being inserted in a sample.
* [Permute randomly](https://novacta.github.io/analytics/html/C0371263.htm)
  a given collection of integers.

### Optimization

* [Optimize continuous functions](https://novacta.github.io/analytics/html/3D985383.htm)
  having multiple arguments ranging over the real line.
* Apply the Cross-Entropy method to
  represent continuous or combinatorial optimization problems via 
  [Cross-Entropy contexts for optimization](https://novacta.github.io/analytics/html/1C32368C.htm),
  and solve them using a
  [Cross-Entropy optimizer](https://novacta.github.io/analytics/html/E252E84.htm).
* Take advantage of specialized Cross-Entropy contexts to
  optimize functions having as arguments
  [continuous variables](https://novacta.github.io/analytics/html/A8203153.htm),
  [combinations](https://novacta.github.io/analytics/html/236BBBCF.htm),
  [partitions](https://novacta.github.io/analytics/html/240B10D.htm),
  or
  [ensembles of categorical entailments](https://novacta.github.io/analytics/html/EB7894F3.htm).

### Rare event simulation

* Express the problem of estimating the probability of
  rare events via
  [Cross-Entropy contexts for rare event simulation](https://novacta.github.io/analytics/html/F877CA6A.htm),
  solvable using a
  [Cross-Entropy estimator](https://novacta.github.io/analytics/html/463F9A03.htm).

## Documentation

The current documentation includes the following topics.

* [Novacta.Analytics release notes](https://novacta.github.io/analytics/html/e6a1e4b5-02ef-4f97-9bd4-3bf049441535.htm).
* [Build and test](https://novacta.github.io/analytics/html/07a926cb-9c3c-432d-998b-0af7eea037f6.htm)
  instructs about the setup required
  to build and test the assembly.
* Namespaces [Novacta.Analytics](https://novacta.github.io/analytics/html/2406EB43.htm)
  and [Novacta.Analytics.Advanced](https://novacta.github.io/analytics/html/F249DE1E.htm)
  contain reference information about assembly types.

## Versioning

We use [SemVer](http://semver.org/) for versioning.
For available versions, see the
[tags on this repository](https://github.com/novacta/analytics/tags).

## Copyrights and Licenses

All source code is Copyright © 2018 Giovanni Lafratta.

**Novacta.Analytics** is licensed under the
[MIT License](https://github.com/novacta/analytics/blob/master/LICENSE.md).

This project relies on native dynamic-link libraries obtained
via the Intel® oneAPI Math Kernel Library customDLL builder.
[oneAPI MKL](https://www.intel.com/content/www/us/en/developer/tools/oneapi/onemkl.html) is
Copyright © 2021 Intel® Corporation and
licensed under the
[ISSL](https://software.intel.com/en-us/license/intel-simplified-software-license)
terms.
