﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolBytes.Core.Domain;

namespace CoolBytes.Core.Collections
{
    public class MetaTagCollection : UpdatableCollection<MetaTag>
    {
        public override void Update(IEnumerable<MetaTag> items)
        {
            var itemsRemoved = Items.Except(items, MetaTag.NameComparer).ToArray();

            RemoveRange(itemsRemoved);
            AddRange(items);
        }

        public override bool Exists(MetaTag item) 
            => Items.Contains(item, MetaTag.NameComparer);
    }
}