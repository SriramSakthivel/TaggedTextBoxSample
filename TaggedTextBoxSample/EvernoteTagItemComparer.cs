using System;
using System.Collections.Generic;

namespace TaggedTextBoxSample
{
    public class EvernoteTagItemComparer : IEqualityComparer<EvernoteTagItem>
    {
        public bool Equals(EvernoteTagItem x, EvernoteTagItem y)
        {
            return StringComparer.Ordinal.Equals(x.Text, y.Text);
        }

        public int GetHashCode(EvernoteTagItem obj)
        {
            return StringComparer.Ordinal.GetHashCode(obj.Text);
        }
    }
}