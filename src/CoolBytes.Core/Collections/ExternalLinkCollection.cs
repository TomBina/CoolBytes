﻿using System;
using System.Collections.Generic;
using System.Linq;
using CoolBytes.Core.Domain;

namespace CoolBytes.Core.Collections
{
    public class ExternalLinkCollection : UpdatableCollection<ExternalLink>
    {
        public override void Update(IEnumerable<ExternalLink> items)
        {
            var itemsRemoved = Items.Except(items, ExternalLink.NameUrlComparer).ToArray();

            RemoveRange(itemsRemoved);
            AddRange(items);
        }

        public override bool Exists(ExternalLink item)
            => Items.Any(externalLink => externalLink.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase));
    }
}
