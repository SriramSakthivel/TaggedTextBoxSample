using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TaggedTextBoxSample
{
    [TemplatePart(Name = "PART_CreateTagButton", Type = typeof(Button))]
    public class TagControl : ListBox
    {
        public event EventHandler<TagEventArgs> TagClick;
        public event EventHandler<TagEventArgs> TagAdded;
        public event EventHandler<TagEventArgs> TagRemoved;

        static TagControl()
        {
            // lookless control, get default style from generic.xaml
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TagControl), new FrameworkPropertyMetadata(typeof(TagControl)));
        }

        public TagControl()
        {
            //// some dummy data, this needs to be provided by user
            //this.ItemsSource = new List<EvernoteTagItem>() { new EvernoteTagItem("receipt"), new EvernoteTagItem("restaurant") };
            //this.AllTags = new List<string>() { "recipe", "red" };
        }

        // AllTags
        public List<string> AllTags { get { return (List<string>)GetValue(AllTagsProperty); } set { SetValue(AllTagsProperty, value); } }
        public static readonly DependencyProperty AllTagsProperty = DependencyProperty.Register("AllTags", typeof(List<string>), typeof(TagControl), new PropertyMetadata(new List<string>()));


        // IsEditing, readonly
        public bool IsEditing { get { return (bool)GetValue(IsEditingProperty); } internal set { SetValue(IsEditingPropertyKey, value); } }
        private static readonly DependencyPropertyKey IsEditingPropertyKey = DependencyProperty.RegisterReadOnly("IsEditing", typeof(bool), typeof(TagControl), new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty IsEditingProperty = IsEditingPropertyKey.DependencyProperty;

        public override void OnApplyTemplate()
        {
            Button createBtn = this.GetTemplateChild("PART_CreateTagButton") as Button;
            if (createBtn != null)
                createBtn.Click += createBtn_Click;

            base.OnApplyTemplate();
        }

        /// <summary>
        /// Executed when create new tag button is clicked.
        /// Adds an EvernoteTagItem to the collection and puts it in edit mode.
        /// </summary>
        void createBtn_Click(object sender, RoutedEventArgs e)
        {
            var newItem = new TagItem() { IsEditing = true };
            AddTag(newItem);
            this.SelectedItem = newItem;
            this.IsEditing = true;

        }

        /// <summary>
        /// Adds a tag to the collection
        /// </summary>
        internal void AddTag(TagItem tag)
        {
            if (this.ItemsSource == null)
                this.ItemsSource = new List<TagItem>();

            ((IList)this.ItemsSource).Add(tag); // assume IList for convenience
            this.Items.Refresh();

            if (TagAdded != null)
                TagAdded(this, new TagEventArgs(tag));
        }

        /// <summary>
        /// Removes a tag from the collection
        /// </summary>
        internal void RemoveTag(TagItem tag, bool cancelEvent = false)
        {
            if (this.ItemsSource != null)
            {
                ((IList)this.ItemsSource).Remove(tag); // assume IList for convenience
                this.Items.Refresh();

                if (TagRemoved != null && !cancelEvent)
                    TagRemoved(this, new TagEventArgs(tag));
            }
        }

        /// <summary>
        /// Raises the TagClick event
        /// </summary>
        internal void RaiseTagClick(TagItem tag)
        {
            if (this.TagClick != null)
                TagClick(this, new TagEventArgs(tag));
        }

        internal void FinishAdding(TagItem tag)
        {
            var comparer = new TagItemComparer();
            if (!AllTags.Contains(tag.Text, StringComparer.Ordinal) ||
                ItemsSource.Cast<TagItem>().Count(x => comparer.Equals(x, tag)) > 1)
            {
                RemoveTag(tag, true);
            }
        }
    }
}