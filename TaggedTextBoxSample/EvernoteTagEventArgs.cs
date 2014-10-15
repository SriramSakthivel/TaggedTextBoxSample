using System;

namespace TaggedTextBoxSample
{
    public class EvernoteTagEventArgs : EventArgs
    {
        public EvernoteTagItem Item { get; set; }

        public EvernoteTagEventArgs(EvernoteTagItem item)
        {
            this.Item = item;
        }
    }
}