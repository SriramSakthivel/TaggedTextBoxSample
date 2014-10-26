using System;
using System.Collections.Generic;

namespace TaggedTextBoxSample
{
    public class TagItemComparer : IEqualityComparer<TagItem>
    {
        public bool Equals(TagItem x, TagItem y)
        {
            return StringComparer.Ordinal.Equals(x.Text, y.Text);
        }

        public int GetHashCode(TagItem obj)
        {
            return StringComparer.Ordinal.GetHashCode(obj.Text);
        }
    }
}