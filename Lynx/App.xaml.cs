using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ImaginationGUI.ViewModels;
using System.Windows.Controls;
using ImaginationGUI.Services;
using Lynx.Models;
using Lynx.ViewModels.Editor;
using Lynx.ViewModels.StartUp;

namespace Lynx
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainViewModel _viewModel;
        private ProjectData _currentProject;
        private Button _saveButton;
        private Button _closeProjectButton;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _viewModel = new MainViewModel
            {
                IsLoading = false,
                Title = "Leon - Startup",
                MainContent = null
            };

            var workingArea = SystemParameters.WorkArea;
            var mainWindow = new ImaginationGUI.Views.MainWindow
            {
                DataContext = _viewModel,
                IsMaximized = true,
                Left = workingArea.Left,
                Top = workingArea.Top,
                Width = workingArea.Width,
                Height = workingArea.Height,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            ShowStartupScreen();

            mainWindow.Show();
        }

        private void ShowStartupScreen()
        {
            var startupView = new Views.StartUp.LynxStartUp();
            var startupViewModel = startupView.DataContext as LynxStartUpViewModel;

            if (startupViewModel != null)
            {
                startupViewModel.ProjectOpened += OnProjectOpened;
            }

            _viewModel.Title = "Project Selection";
            _viewModel.MainContent = startupView;
            _viewModel.CustomTitleBarButtons = null;
        }

        private void OnProjectOpened(object sender, ProjectData project)
        {
            if (project == null || !project.Exists)
            {
                MessageBox.Show("Cannot open project: Project directory not found.",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _currentProject = project;
            ShowEditorScreen();
        }

        private void ShowEditorScreen()
        {
            var menuButtonStyle = Application.Current.FindResource("MenuButtonStyle") as Style;

            var openButton = new Button
            {
                Content = "Open",
                Style = menuButtonStyle,
                Margin = new Thickness(0, 0, 5, 0)
            };
            openButton.Click += OpenButton_Click;

            _saveButton = new Button
            {
                Content = "Save",
                Style = menuButtonStyle,
                IsEnabled = true,
                Margin = new Thickness(0, 0, 5, 0)
            };
            _saveButton.Click += SaveButton_Click;

            _closeProjectButton = new Button
            {
                Content = "Close Project",
                Style = menuButtonStyle
            };
            _closeProjectButton.Click += CloseProjectButton_Click;

            var buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Children =
                {
                    openButton,
                    _saveButton,
                    _closeProjectButton
                }
            };

            _viewModel.Title = $"{_currentProject.Name} ({_currentProject.Game})";
            _viewModel.MainContent = new Views.Editor.LynxMainContent();
            _viewModel.CustomTitleBarButtons = buttonPanel;

            // TODO: open the project folder
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentProject == null)
            {
                MessageBox.Show("No project is currently opened.",
                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // TODO: Implémenter la sauvegarde du projet

                MessageBox.Show($"Project '{_currentProject.Name}' saved successfully!",
                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving project: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Do you want to save the current project before opening another?",
                "Save Current Project",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Cancel)
                return;

            if (result == MessageBoxResult.Yes)
            {
                SaveButton_Click(sender, e);
            }

            _currentProject = null;
            ShowStartupScreen();
        }

        private void CloseProjectButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Do you want to save the current project before closing?",
                "Close Project",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Cancel)
                return;

            if (result == MessageBoxResult.Yes)
            {
                SaveButton_Click(sender, e);
            }

            // TODO: Nettoyer les ressources

            _currentProject = null;
            ShowStartupScreen();
        }
    }
}
