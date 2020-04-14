// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics
{
    internal class ImplementorChangedEventArgs : EventArgs
    {
        private readonly object newImplementor;

        public ImplementorChangedEventArgs(object newImplementor)
        {
            this.newImplementor = newImplementor;
        }

        public object NewImplementor
        {
            get
            {
                return this.newImplementor;
            }
        }
    }
}
