// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.ComponentModel;

namespace Novacta.Analytics.Tests
{
    internal class PropertyChangedSubscriber
    {
        public PropertyChangedSubscriber()
        {
            this.PropertyNames = new List<string>
            {
                ""
            };
        }

        public List<string> PropertyNames { get; set; }

        public void PropertyChangedEventHandler(object sender,
            PropertyChangedEventArgs e)
        {
            this.PropertyNames.Add(e.PropertyName);
        }

    }
}