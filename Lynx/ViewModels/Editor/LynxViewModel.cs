using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using ImaginationGUI.ViewModels;
using Lynx.Models;
using Lynx.Models.InazumaEleven.Games;
using Lynx.Models.InazumaEleven.Games.GO;
using Lynx.Models.InazumaEleven.Logic;
using Lynx.Views.Panels;
using Lynx.ViewModels.Panels;

namespace Lynx.ViewModels.Editor
{
    public class LynxViewModel : BaseViewModel
    {
        private ImaginationContainerViewModel _imaginationContainer;
        public ImaginationContainerViewModel ImaginationContainer
        {
            get => _imaginationContainer;
            set => SetProperty(ref _imaginationContainer, value);
        }

        private ObservableCollection<EditorOptionItem> _editorOptions;
        public ObservableCollection<EditorOptionItem> EditorOptions
        {
            get => _editorOptions;
            set => SetProperty(ref _editorOptions, value);
        }

        private EditorOptionItem _selectedOption;
        public EditorOptionItem SelectedOption
        {
            get => _selectedOption;
            set
            {
                if (SetProperty(ref _selectedOption, value))
                {
                    OnOptionSelected(value?.Option);
                }
            }
        }

        private Game _game;

        // Shared data for synchronization
        private List<ICharabase> _sharedCharabases;
        public List<ICharabase> SharedCharabases
        {
            get => _sharedCharabases;
            set
            {
                _sharedCharabases = value;
                NotifyAllCharabasePanels();
            }
        }

        // Keep track of all open panels for synchronization
        private Dictionary<Type, List<UserControl>> _openPanels;

        public LynxViewModel()
        {
            string appVersion = System.Reflection.Assembly
                .GetExecutingAssembly()
                .GetName()
                .Version
                .ToString();

            ImaginationContainer = new ImaginationContainerViewModel("Lynx", appVersion);
            _openPanels = new Dictionary<Type, List<UserControl>>();

            PopulateEditorOptions();
        }

        public LynxViewModel(ProjectData project) : this()
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));

            _game = CreateGame(project);

            // Initialize shared data
            _sharedCharabases = _game.GetCharabase().ToList();
        }

        private Game CreateGame(ProjectData project)
        {
            switch (project.Game)
            {
                case GameType.IEGO:
                    return new GO(
                        project.Path,
                        project.IsExtractedFaFiles,
                        project.Language
                    );

                case GameType.IEGOCS:
                case GameType.IEGOGalaxy:
                    throw new NotSupportedException(
                        $"Game type '{project.Game}' is not yet supported."
                    );

                default:
                    throw new ArgumentException(
                        $"Unknown game type: {project.Game}",
                        nameof(project)
                    );
            }
        }

        private void PopulateEditorOptions()
        {
            var options = Enum.GetValues(typeof(EditorOption))
                              .Cast<EditorOption>()
                              .Select(o => new EditorOptionItem
                              {
                                  DisplayName = FormatOptionName(o),
                                  Option = o
                              })
                              .OrderBy(o => o.DisplayName)
                              .ToList();

            EditorOptions = new ObservableCollection<EditorOptionItem>(options);
        }

        private string FormatOptionName(EditorOption option)
        {
            string name = option.ToString();
            return Regex.Replace(name, "([a-z])([A-Z])", "$1 $2");
        }

        public void OnOptionDoubleClick(EditorOption? option)
        {
            if (!option.HasValue)
                return;

            OpenEditorPanel(option.Value);
        }

        private void OnOptionSelected(EditorOption? option)
        {
            // Selection only, no action
        }

        private void OpenEditorPanel(EditorOption option)
        {
            UserControl panelControl = null;

            switch (option)
            {
                case EditorOption.Charabase:
                    var charabasePanel = new PanelCharabase();
                    var charabaseViewModel = new PanelCharabaseViewModel();
                    charabaseViewModel.Initialize(_game);
                    charabasePanel.DataContext = charabaseViewModel;
                    panelControl = charabasePanel;

                    // Track this panel
                    TrackPanel(typeof(PanelCharabase), panelControl);
                    break;

                case EditorOption.Charaparam:
                    // TODO: Implement Charaparam panel
                    break;

                case EditorOption.Shops:
                    // TODO: Implement Shops panel
                    break;

                case EditorOption.Skills:
                    // TODO: Implement Skills panel
                    break;

                case EditorOption.FightingSpirits:
                    // TODO: Implement Fighting Spirits panel
                    break;

                case EditorOption.Scripts:
                    // TODO: Implement Scripts panel
                    break;

                case EditorOption.MapEditor:
                    // TODO: Implement Map Editor panel
                    break;

                case EditorOption.SaveEditor:
                    // TODO: Implement Save Editor panel
                    break;

                case EditorOption.ChallengeRoute:
                    // TODO: Implement Challenge Route panel
                    break;

                case EditorOption.Teams:
                    // TODO: Implement Teams panel
                    break;

                case EditorOption.Coaches:
                    // TODO: Implement Coaches panel
                    break;

                case EditorOption.TranslationHelper:
                    // TODO: Implement Translation Helper panel
                    break;
            }

            if (panelControl != null)
            {
                ImaginationContainer.AddPartCommand.Execute(panelControl);
            }
        }

        private void TrackPanel(Type panelType, UserControl panel)
        {
            if (!_openPanels.ContainsKey(panelType))
            {
                _openPanels[panelType] = new List<UserControl>();
            }

            _openPanels[panelType].Add(panel);

            // Subscribe to panel closing event to remove from tracking
            panel.Unloaded += (s, e) => UntrackPanel(panelType, panel);
        }

        private void UntrackPanel(Type panelType, UserControl panel)
        {
            if (_openPanels.ContainsKey(panelType))
            {
                _openPanels[panelType].Remove(panel);

                if (_openPanels[panelType].Count == 0)
                {
                    _openPanels.Remove(panelType);
                }
            }
        }

        private void NotifyAllCharabasePanels()
        {
            if (_openPanels.ContainsKey(typeof(PanelCharabase)))
            {
                foreach (var panel in _openPanels[typeof(PanelCharabase)])
                {
                    if (panel.DataContext is PanelCharabaseViewModel viewModel)
                    {
                        // Reinitialize with updated data
                        viewModel.Initialize(_game);
                    }
                }
            }
        }

        public void SaveAllChanges()
        {
            if (_game != null && _sharedCharabases != null)
            {
                _game.SaveCharaBase(_sharedCharabases.ToArray());
            }
        }
    }
}