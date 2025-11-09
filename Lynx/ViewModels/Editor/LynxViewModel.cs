using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using ImaginationGUI.ViewModels;
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

        public LynxViewModel()
        {
            // Initialisation
            ImaginationContainer = new ImaginationContainerViewModel("Lynx", "1.0.0");

            // Initialiser les options
            PopulateEditorOptions();

            //// Créer un nouveau UserControl
            //var encounterControl = new EncounterUserControl();
            //// L'ajouter au conteneur
            //_imaginationContainer.AddPartCommand.Execute(encounterControl);
            //// Créer un nouveau UserControl
            //var mapControl = new MapEditorUserControl();
            //// L'ajouter au conteneur
            //_imaginationContainer.AddPartCommand.Execute(mapControl);
            //// Créer un nouveau UserControl
            //var scriptControl = new ScriptEditorUserControl();
            //// L'ajouter au conteneur
            //_imaginationContainer.AddPartCommand.Execute(scriptControl);
            //// Créer un nouveau UserControl
            //var previewControl = new PreviewUserControl();
            //// L'ajouter au conteneur
            //_imaginationContainer.AddPartCommand.Execute(previewControl);
            //// Créer un nouveau UserControl
            //var propertiesControl = new PropertiesUserControl();
            //// L'ajouter au conteneur
            //_imaginationContainer.AddPartCommand.Execute(propertiesControl);
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