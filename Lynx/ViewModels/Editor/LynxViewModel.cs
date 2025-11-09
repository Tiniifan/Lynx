using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using ImaginationGUI.ViewModels;
using Lynx.Models;
using Lynx.Models.InazumaEleven.Games;
using Lynx.Models.InazumaEleven.Games.GO;
using Lynx.Views.Editor;

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

        public LynxViewModel()
        {
            // Initialization
            string appVersion = System.Reflection.Assembly
                .GetExecutingAssembly()
                .GetName()
                .Version
                .ToString();

            ImaginationContainer = new ImaginationContainerViewModel("Lynx", appVersion);

            // Initialize options
            PopulateEditorOptions();
        }

        public LynxViewModel(ProjectData project) : this()
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));

            // Create the game according to the type
            _game = CreateGame(project);
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
            // Récupérer toutes les valeurs de l'enum, les trier alphabétiquement
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
            // Convertir le nom de l'enum en format lisible
            // Ex: "ChallengeRoute" -> "Challenge Route"
            string name = option.ToString();
            return Regex.Replace(name, "([a-z])([A-Z])", "$1 $2");
        }

        private void OnOptionSelected(EditorOption? option)
        {
            if (!option.HasValue)
                return;

            // Logique pour charger le contenu selon l'option sélectionnée
            switch (option.Value)
            {
                case EditorOption.Charabase:
                    // Charger l'éditeur Charabase
                    // var charabaseControl = new CharabaseUserControl();
                    // ImaginationContainer.AddPartCommand.Execute(charabaseControl);
                    break;
                case EditorOption.Charaparam:
                    // Charger l'éditeur Charaparam
                    break;
                case EditorOption.Shops:
                    // Charger l'éditeur Shops
                    break;
                case EditorOption.Skills:
                    // Charger l'éditeur Skills
                    break;
                case EditorOption.FightingSpirits:
                    // Charger l'éditeur Fighting Spirits
                    break;
                case EditorOption.Scripts:
                    // Charger l'éditeur Scripts
                    break;
                case EditorOption.MapEditor:
                    // Charger l'éditeur Map
                    break;
                case EditorOption.SaveEditor:
                    // Charger l'éditeur Save
                    break;
                case EditorOption.ChallengeRoute:
                    // Charger l'éditeur Challenge Route
                    break;
                case EditorOption.Teams:
                    // Charger l'éditeur Teams
                    break;
                case EditorOption.Coaches:
                    // Charger l'éditeur Coaches
                    break;
                case EditorOption.TranslationHelper:
                    // Charger l'éditeur Translation Helper
                    break;
            }
        }
    }
}