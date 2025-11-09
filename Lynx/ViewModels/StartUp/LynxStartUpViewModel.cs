using System;
using System.Linq;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ImaginationGUI.ViewModels;
using Lynx.Models;
using Lynx.Services;

namespace Lynx.ViewModels.StartUp
{
    public partial class LynxStartUpViewModel : BaseViewModel
    {
        #region Private Fields
        private ObservableCollection<ProjectData> _projects;
        private ProjectData _selectedProject;
        private string _newProjectName;
        private string _newProjectLanguage = "EN";
        private string _newProjectPath;
        private string _newProjectImagePath;
        private string _searchText;
        private GameType _newProjectGame = GameType.IEGO;
        private bool _isExtractedFaFiles = false;
        private ObservableCollection<string> _availableLanguages;
        #endregion

        #region Public Properties
        public ObservableCollection<ProjectData> Projects
        {
            get => _projects;
            set
            {
                _projects = value;
                OnPropertyChanged(nameof(Projects));
            }
        }

        public ProjectData SelectedProject
        {
            get => _selectedProject;
            set
            {
                _selectedProject = value;
                OnPropertyChanged(nameof(SelectedProject));
            }
        }

        public string NewProjectName
        {
            get => _newProjectName;
            set
            {
                _newProjectName = value;
                OnPropertyChanged(nameof(NewProjectName));
            }
        }

        public string NewProjectLanguage
        {
            get => _newProjectLanguage;
            set
            {
                _newProjectLanguage = value;
                OnPropertyChanged(nameof(NewProjectLanguage));
            }
        }

        public string NewProjectPath
        {
            get => _newProjectPath;
            set
            {
                _newProjectPath = value;
                OnPropertyChanged(nameof(NewProjectPath));
            }
        }

        public string NewProjectImagePath
        {
            get => _newProjectImagePath;
            set
            {
                _newProjectImagePath = value;
                OnPropertyChanged(nameof(NewProjectImagePath));
            }
        }

        public GameType NewProjectGame
        {
            get => _newProjectGame;
            set
            {
                _newProjectGame = value;
                OnPropertyChanged(nameof(NewProjectGame));
                UpdateAvailableLanguages();
            }
        }

        public bool IsExtractedFaFiles
        {
            get => _isExtractedFaFiles;
            set
            {
                _isExtractedFaFiles = value;
                OnPropertyChanged(nameof(IsExtractedFaFiles));
            }
        }

        public ObservableCollection<string> AvailableLanguages
        {
            get => _availableLanguages;
            set
            {
                _availableLanguages = value;
                OnPropertyChanged(nameof(AvailableLanguages));
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                // TODO: Implement filtering
            }
        }
        #endregion

        #region Commands
        public ICommand CreateProjectCommand { get; }
        public ICommand DeleteProjectCommand { get; }
        public ICommand OpenProjectCommand { get; }
        public ICommand BrowsePathCommand { get; }
        public ICommand BrowseImageCommand { get; }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<ProjectData> ProjectOpened;
        #endregion

        #region Constructor
        public LynxStartUpViewModel()
        {
            LoadProjects();
            UpdateAvailableLanguages();

            CreateProjectCommand = new RelayCommand(CreateProject, CanCreateProject);
            DeleteProjectCommand = new RelayCommand(DeleteProject);
            OpenProjectCommand = new RelayCommand(OpenProject);
            BrowsePathCommand = new RelayCommand(BrowsePath);
            BrowseImageCommand = new RelayCommand(BrowseImage);
        }
        #endregion
    }
}