using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Lynx.ViewModels.Editor;

namespace Lynx.Views.Editor
{
    public partial class LynxMainContent : UserControl
    {
        private Point _dragStartPoint;
        private bool _isDragging;

        public LynxViewModel ViewModel => DataContext as LynxViewModel;

        public LynxMainContent()
        {
            InitializeComponent();
        }

        private void EditorOptionsTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (DataContext is LynxViewModel viewModel && e.NewValue is EditorOptionItem selectedOption)
            {
                viewModel.SelectedOption = selectedOption;
            }
        }

        private void EditorOptionsTreeView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is LynxViewModel viewModel && viewModel.SelectedOption != null)
            {
                viewModel.OnOptionDoubleClick(viewModel.SelectedOption.Option);
            }
        }

        private void EditorOptionsTreeView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragStartPoint = e.GetPosition(null);
            _isDragging = false;
        }

        private void EditorOptionsTreeView_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && !_isDragging)
            {
                Point position = e.GetPosition(null);
                Vector diff = _dragStartPoint - position;

                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    TreeView treeView = sender as TreeView;
                    EditorOptionItem selectedItem = treeView.SelectedItem as EditorOptionItem;

                    if (selectedItem != null)
                    {
                        _isDragging = true;
                        DataObject dragData = new DataObject("EditorOption", selectedItem);
                        DragDrop.DoDragDrop(treeView, dragData, DragDropEffects.Copy);
                        _isDragging = false;
                    }
                }
            }
        }

        private void Container_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("EditorOption"))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void Container_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("EditorOption"))
            {
                EditorOptionItem droppedItem = e.Data.GetData("EditorOption") as EditorOptionItem;
                if (droppedItem != null && DataContext is LynxViewModel viewModel)
                {
                    viewModel.OnOptionDoubleClick(droppedItem.Option);
                }
            }
        }
    }
}