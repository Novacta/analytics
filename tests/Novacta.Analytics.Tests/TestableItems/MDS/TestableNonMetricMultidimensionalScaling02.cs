﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.MDS
{
    class TestableNonMetricMultidimensionalScaling02
        : TestableNonMetricMultidimensionalScaling<TestableDoubleMatrix>
    {
        static readonly TestableDoubleMatrix dissimilarities;
        static readonly int? configurationDimension;
        static readonly int minkowskiDistanceOrder;
        static readonly int maximumNumberOfIterations;
        static readonly double terminationTolerance;
        static readonly DoubleMatrix configuration;
        static readonly double stress;
        static readonly bool hasConverged;

        static TestableNonMetricMultidimensionalScaling02()
        {
            #region Dissimilarities

            dissimilarities = new TestableDoubleMatrix(
                asColumnMajorDenseArray: [
                    0         ,
                    122.278371,
                    322.632918,
                    288.622591,
                    347.361483,
                    204.066166,
                    295.920597,
                    322.605022,
                    303.953944,
                    342.040933,
                    141.124059,
                    288.260299,
                    263.600076,
                    214.480768,
                    293.199591,
                    206.973428,
                    248.128999,
                    293.238811,
                    107.312627,
                    334.991045,
                    289.903432,
                    340.98827 ,
                    269.968517,
                    122.278371,
                    0         ,
                    306.926701,
                    335.223806,
                    320.231167,
                    180.729079,
                    316.78226 ,
                    306.884343,
                    317.118274,
                    274.24077 ,
                    186.445703,
                    288.645111,
                    264.992453,
                    203.656574,
                    303.037951,
                    234.187959,
                    245.613518,
                    349.389468,
                    134.773885,
                    264.259342,
                    336.948067,
                    304.960653,
                    295.930735,
                    322.632918,
                    306.926701,
                    0         ,
                    166.117428,
                    36.5239647,
                    131.129707,
                    96.1613228,
                    1.41421356,
                    75.2994024,
                    143.833237,
                    197.372744,
                    93.0161276,
                    113.512114,
                    141.675686,
                    66.8430999,
                    141.71803 ,
                    78.8923317,
                    210.188011,
                    226.83915 ,
                    150.023332,
                    165.813148,
                    55.9553393,
                    108.687626,
                    288.622591,
                    335.223806,
                    166.117428,
                    0         ,
                    201.223756,
                    195.94387 ,
                    70.9224929,
                    166.051197,
                    91.820477 ,
                    297.329783,
                    164.605589,
                    144.176975,
                    159.003145,
                    197.42087 ,
                    102.532922,
                    124.751753,
                    132.449991,
                    81.4370923,
                    221.343624,
                    299.803269,
                    10.1488916,
                    220.997738,
                    64.3583716,
                    347.361483,
                    320.231167,
                    36.5239647,
                    201.223756,
                    0         ,
                    148.852276,
                    130.946554,
                    36.4965752,
                    110.127199,
                    120.971071,
                    226.984581,
                    116.961532,
                    136.436799,
                    160.405736,
                    102.54755 ,
                    173.210854,
                    108.78419 ,
                    243.690377,
                    250.84258 ,
                    129.007752,
                    200.972635,
                    28.7576077,
                    144.710055,
                    204.066166,
                    180.729079,
                    131.129707,
                    195.94387 ,
                    148.852276,
                    0         ,
                    153.153518,
                    131.061054,
                    147.929037,
                    152.731791,
                    105.123737,
                    128.798292,
                    108.452755,
                    51.8362807,
                    130.579478,
                    91.2304774,
                    79.3662397,
                    226.900859,
                    106.990654,
                    150.133274,
                    195.742177,
                    139.212068,
                    138.592929,
                    295.920597,
                    316.78226 ,
                    96.1613228,
                    70.9224929,
                    130.946554,
                    153.153518,
                    0         ,
                    96.0572746,
                    23.3023604,
                    231.237973,
                    162.009259,
                    95.1367437,
                    115.282262,
                    156.284996,
                    34.278273 ,
                    104.522725,
                    80.7155499,
                    126.301227,
                    210.829315,
                    235.044677,
                    70.2210795,
                    151.294415,
                    27.7488739,
                    322.605022,
                    306.884343,
                    1.41421356,
                    166.051197,
                    36.4965752,
                    131.061054,
                    96.0572746,
                    0         ,
                    75.2728371,
                    143.791516,
                    197.367677,
                    92.9300812,
                    113.468057,
                    141.661569,
                    66.7682559,
                    141.661569,
                    78.7527777,
                    210.140429,
                    226.830333,
                    149.976665,
                    165.752828,
                    55.9732079,
                    108.59558 ,
                    303.953944,
                    317.118274,
                    75.2994024,
                    91.820477 ,
                    110.127199,
                    147.929037,
                    23.3023604,
                    75.2728371,
                    0         ,
                    213.892496,
                    170.146995,
                    88.2609767,
                    111.242977,
                    152.492623,
                    20.8326667,
                    111.382225,
                    75.059976 ,
                    143.641916,
                    215.517981,
                    218.304833,
                    91.2688337,
                    131.030531,
                    43.5086198,
                    342.040933,
                    274.24077 ,
                    143.833237,
                    297.329783,
                    120.971071,
                    152.731791,
                    231.237973,
                    143.791516,
                    213.892496,
                    0         ,
                    257.157539,
                    190.373317,
                    190.244579,
                    172.481883,
                    200.28979 ,
                    225.729927,
                    170.710281,
                    333.327167,
                    253.239018,
                    14.3178211,
                    297.516386,
                    93.2469839,
                    234.646543,
                    141.124059,
                    186.445703,
                    197.372744,
                    164.605589,
                    226.984581,
                    105.123737,
                    162.009259,
                    197.367677,
                    170.146995,
                    257.157539,
                    0         ,
                    166.096358,
                    141.439033,
                    103.04368 ,
                    158.234004,
                    66.9477408,
                    126.174482,
                    183.256651,
                    58.3266663,
                    254.847013,
                    163.707055,
                    227.052857,
                    136.157996,
                    288.260299,
                    288.645111,
                    93.0161276,
                    144.176975,
                    116.961532,
                    128.798292,
                    95.1367437,
                    92.9300812,
                    88.2609767,
                    190.373317,
                    166.096358,
                    0         ,
                    46.3141447,
                    136.308474,
                    80.8702665,
                    118.101651,
                    83.3546639,
                    151.145625,
                    199.469296,
                    193.470928,
                    143.840189,
                    127.165247,
                    96.3379468,
                    263.600076,
                    264.992453,
                    113.512114,
                    159.003145,
                    136.436799,
                    108.452755,
                    115.282262,
                    113.468057,
                    111.242977,
                    190.244579,
                    141.439033,
                    46.3141447,
                    0         ,
                    103.32957 ,
                    97.3704267,
                    96.7315874,
                    90.5814551,
                    163.211519,
                    169.4255  ,
                    193.095831,
                    156.764154,
                    140.392307,
                    108.590976,
                    214.480768,
                    203.656574,
                    141.675686,
                    197.42087 ,
                    160.405736,
                    51.8362807,
                    156.284996,
                    141.661569,
                    152.492623,
                    172.481883,
                    103.04368 ,
                    136.308474,
                    103.32957 ,
                    0         ,
                    132.785541,
                    79.0695896,
                    101.616928,
                    226.863395,
                    107.777549,
                    173.086683,
                    194.59702 ,
                    152.888849,
                    141.587429,
                    293.199591,
                    303.037951,
                    66.8430999,
                    102.532922,
                    102.54755 ,
                    130.579478,
                    34.278273 ,
                    66.7682559,
                    20.8326667,
                    200.28979 ,
                    158.234004,
                    80.8702665,
                    97.3704267,
                    132.785541,
                    0         ,
                    97.2213968,
                    62.2093241,
                    151.706954,
                    201.087046,
                    204.731532,
                    101.054441,
                    120.718681,
                    44.5084262,
                    206.973428,
                    234.187959,
                    141.71803 ,
                    124.751753,
                    173.210854,
                    91.2304774,
                    104.522725,
                    141.661569,
                    111.382225,
                    225.729927,
                    66.9477408,
                    118.101651,
                    96.7315874,
                    79.0695896,
                    97.2213968,
                    0         ,
                    81.4739222,
                    156.444878,
                    112.605506,
                    226.589938,
                    121.9016  ,
                    178.087057,
                    81.774079 ,
                    248.128999,
                    245.613518,
                    78.8923317,
                    132.449991,
                    108.78419 ,
                    79.3662397,
                    80.7155499,
                    78.7527777,
                    75.059976 ,
                    170.710281,
                    126.174482,
                    83.3546639,
                    90.5814551,
                    101.616928,
                    62.2093241,
                    81.4739222,
                    0         ,
                    173.951143,
                    158.499211,
                    171.251277,
                    133.626345,
                    114.703967,
                    72.4361788,
                    293.238811,
                    349.389468,
                    210.188011,
                    81.4370923,
                    243.690377,
                    226.900859,
                    126.301227,
                    210.140429,
                    143.641916,
                    333.327167,
                    183.256651,
                    151.145625,
                    163.211519,
                    226.863395,
                    151.706954,
                    156.444878,
                    173.951143,
                    0         ,
                    237.2446  ,
                    335.240212,
                    82.0670458,
                    261.759432,
                    118.12705 ,
                    107.312627,
                    134.773885,
                    226.83915 ,
                    221.343624,
                    250.84258 ,
                    106.990654,
                    210.829315,
                    226.830333,
                    215.517981,
                    253.239018,
                    58.3266663,
                    199.469296,
                    169.4255  ,
                    107.777549,
                    201.087046,
                    112.605506,
                    158.499211,
                    237.2446  ,
                    0         ,
                    248.89556 ,
                    220.676233,
                    244.440177,
                    186.646725,
                    334.991045,
                    264.259342,
                    150.023332,
                    299.803269,
                    129.007752,
                    150.133274,
                    235.044677,
                    149.976665,
                    218.304833,
                    14.3178211,
                    254.847013,
                    193.470928,
                    193.095831,
                    173.086683,
                    204.731532,
                    226.589938,
                    171.251277,
                    335.240212,
                    248.89556 ,
                    0         ,
                    300.318165,
                    101.587401,
                    237.394187,
                    289.903432,
                    336.948067,
                    165.813148,
                    10.1488916,
                    200.972635,
                    195.742177,
                    70.2210795,
                    165.752828,
                    91.2688337,
                    297.516386,
                    163.707055,
                    143.840189,
                    156.764154,
                    194.59702 ,
                    101.054441,
                    121.9016  ,
                    133.626345,
                    82.0670458,
                    220.676233,
                    300.318165,
                    0         ,
                    220.77364 ,
                    63.6631762,
                    340.98827 ,
                    304.960653,
                    55.9553393,
                    220.997738,
                    28.7576077,
                    139.212068,
                    151.294415,
                    55.9732079,
                    131.030531,
                    93.2469839,
                    227.052857,
                    127.165247,
                    140.392307,
                    152.888849,
                    120.718681,
                    178.087057,
                    114.703967,
                    261.759432,
                    244.440177,
                    101.587401,
                    220.77364 ,
                    0         ,
                    161.356748,
                    269.968517,
                    295.930735,
                    108.687626,
                    64.3583716,
                    144.710055,
                    138.592929,
                    27.7488739,
                    108.59558 ,
                    43.5086198,
                    234.646543,
                    136.157996,
                    96.3379468,
                    108.590976,
                    141.587429,
                    44.5084262,
                    81.774079 ,
                    72.4361788,
                    118.12705 ,
                    186.646725,
                    237.394187,
                    63.6631762,
                    161.356748,
                    0],
                numberOfRows: 23,
                numberOfColumns: 23,
                isUpperHessenberg: false,
                isLowerHessenberg: false,
                isUpperTriangular: false,
                isLowerTriangular: false,
                isSymmetric: true,
                isSkewSymmetric: false,
                upperBandwidth: 22,
                lowerBandwidth: 22);

            #endregion

            configurationDimension = 1;

            minkowskiDistanceOrder = 2;

            maximumNumberOfIterations = 5;

            terminationTolerance = 1e-5;

            configuration = DoubleMatrix.Dense(23, 1, [
                -2.24511057000,
                -2.42037065000,
                 0.78906047200,
                 0.87651852500,
                 1.23508880000,
                -0.66575723800,
                 0.53384792400,
                 0.78906227200,
                 0.50876941600,
                -0.55978873000,
                -0.88256980900,
                 0.15723925900,
                 0.00453114387,
                -0.68968305000,
                 0.40741160500,
                -0.18469603400,
                 0.05479583310,
                 1.40241992000,
                -1.02723046000,
                -0.60046048400,
                 0.87920549200,
                 1.19492382000,
                 0.44279255200]);

            stress = 0.34580118788601727;

            hasConverged = false;
        }

        TestableNonMetricMultidimensionalScaling02()
            : base(
                  dissimilarities,
                  configurationDimension,
                  minkowskiDistanceOrder,
                  maximumNumberOfIterations,
                  terminationTolerance,
                  configuration,
                  stress,
                  hasConverged)
        {
        }

        /// <summary>
        /// Gets an instance of the 
        /// <see cref="TestableNonMetricMultidimensionalScaling02"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TestableNonMetricMultidimensionalScaling02"/> class.</returns>
        public static TestableNonMetricMultidimensionalScaling02 Get()
        {
            return new TestableNonMetricMultidimensionalScaling02();
        }
    }
}