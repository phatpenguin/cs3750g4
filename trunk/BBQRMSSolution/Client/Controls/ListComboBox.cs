using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Controls
{
    public class ListComboBox : ListBox
    {
        private object newItemInstance = null;
        private object newItem = null;

        public static readonly DependencyProperty SelectableItemsSourceProperty = DependencyProperty.Register(
            "SelectableItemsSource", typeof(IList), typeof(ListComboBox));

        public static readonly DependencyProperty InsertAtEndProperty = DependencyProperty.Register(
           "InsertAtEnd", typeof(bool), typeof(ListComboBox), new UIPropertyMetadata(false));


        public IEnumerable SelectableItemsSource
        {
            get { return (IEnumerable)GetValue(SelectableItemsSourceProperty); }
            set { SetValue(SelectableItemsSourceProperty, value); }
        }

        public bool InsertAtEnd
        {
            get { return (bool)GetValue(InsertAtEndProperty); }
            set { SetValue(InsertAtEndProperty, value); }
        }

        public ListComboBox()
        {
            SelectionChanged += ListComboBox_SelectionChanged;
            ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;
            SelectedIndex = 0;
        }

        private void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
        {
            if (this.ItemContainerGenerator.Status == System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
            {
                if (newItem != null)
                {
                    ListBoxItem container = (ListBoxItem)this.ItemContainerGenerator.ContainerFromItem(newItem);
                    this.ScrollIntoView(newItem);
                    if (container != null) container.IsSelected = true;
                    newItem = null;
                }
            }
        }

        private void ListComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionChanged -= ListComboBox_SelectionChanged;
            if (e.OriginalSource is ComboBox)
            {
                ComboBox cbox = (ComboBox)e.OriginalSource;
                ListComboBox box = (ListComboBox)e.Source;
                object clone = cbox.SelectedValue;
                int index = box.SelectedIndex;
                if (index > -1 && clone != null && !box.SelectedItem.Equals(clone))
                {
                    IList list = ((IList)box.ItemsSource);
                    list.RemoveAt(index);
                    list.Insert(index, clone);
                }
            }
            else if (e.OriginalSource is ListComboBox)
            {
                ListComboBox box = (ListComboBox)e.OriginalSource;
                IList list = ((IList)box.SelectableItemsSource);
                if (list != null)
                {
                    foreach (var selectedItem in box.ItemsSource)
                    {
                        foreach (var item in list)
                        {
                            if (item.Equals(selectedItem))
                            {
                                list.Remove(item);
                                break;
                            }
                        }
                    }
                    bool containsNewItem = false;
                    if (e.AddedItems.Count > 0)
                    {
                        foreach (var list1 in list)
                        {
                            if (list1.Equals(e.AddedItems[0]))
                            {
                                containsNewItem = true;
                                break;
                            }
                        }
                        if (!containsNewItem)
                        {
                            list.Insert(0, e.AddedItems[0]);
                        }
                    }
                }
            }

            SelectionChanged += ListComboBox_SelectionChanged;
        }

        public DelegateCommand AddItem { 
            get {
                return new DelegateCommand(DoAddItem);
            }
        }

        private void DoAddItem(object obj)
        {
            IList list = ((IList)SelectableItemsSource);
            IList itemList = ((IList)ItemsSource);

            if (list != null && list.Count > 0)
            {
                if (newItemInstance == null)
                {
                    newItemInstance = Activator.CreateInstance((list)[0].GetType());
                }
                newItem = newItemInstance;
                for (int i = 0; i < itemList.Count; i++)
                {
                    if (itemList[i].Equals(newItem))
                    {
                        SelectedIndex = i;
                        return;
                    }
                }

                bool insertAtEnd = InsertAtEnd;
                if (!insertAtEnd)
                {
                    itemList.Insert(0, newItem);
                }
                else
                {
                    itemList.Add(newItem);
                }
            }
        }

        public ICommand RemoveItem
        {
            get
            {
                return new DelegateCommand(arg =>
                {
                    ((IList)SelectableItemsSource).Add(arg);
                    ((IList)ItemsSource).Remove(arg);
                });
            }
        }
    }
}
