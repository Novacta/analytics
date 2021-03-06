﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Infrastructure
{
    internal delegate MatrixImplementor<TReturn>
    MatrixBinaryOperator<TReturn, TLeft, TRight>(MatrixImplementor<TLeft> lm, MatrixImplementor<TRight> rm);
}