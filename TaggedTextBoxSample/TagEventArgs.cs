using System;

namespace TaggedTextBoxSample
{
    public class TagEventArgs : EventArgs
    {
        public TagEventArgs(TagItem item)
        {
            this.Item = item;
        }

        public TagItem Item { get; private set; }
    }
}