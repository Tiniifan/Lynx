using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ImaginationGUI.ViewModels;
using Lynx.Models.InazumaEleven.Games;
using Lynx.Models.InazumaEleven.Logic;

namespace Lynx.ViewModels.Panels
{
    public class PanelCharabaseViewModel : BaseViewModel
    {
        private Game _game;
        private List<ICharabase> _charabases;
        private ICharabase _selectedCharabase;

        #region Properties

        public ObservableCollection<CharabaseTreeNode> CharabaseTree { get; set; }

        private string _searchText = "";
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    FilterCharabases();
                }
            }
        }

        private bool _isCharacterSelected;
        public bool IsCharacterSelected
        {
            get => _isCharacterSelected;
            set => SetProperty(ref _isCharacterSelected, value);
        }

        private string _selectedCharacterHash;
        public string SelectedCharacterHash
        {
            get => _selectedCharacterHash;
            set => SetProperty(ref _selectedCharacterHash, value);
        }

        private string _selectedCharacterFullName;
        public string SelectedCharacterFullName
        {
            get => _selectedCharacterFullName;
            set => SetProperty(ref _selectedCharacterFullName, value);
        }

        private string _selectedCharacterNickname;
        public string SelectedCharacterNickname
        {
            get => _selectedCharacterNickname;
            set => SetProperty(ref _selectedCharacterNickname, value);
        }

        private string _selectedCharacterDescription;
        public string SelectedCharacterDescription
        {
            get => _selectedCharacterDescription;
            set => SetProperty(ref _selectedCharacterDescription, value);
        }

        private BitmapImage _characterFaceImage;
        public BitmapImage CharacterFaceImage
        {
            get => _characterFaceImage;
            set => SetProperty(ref _characterFaceImage, value);
        }

        public ObservableCollection<string> Years { get; set; }
        public ObservableCollection<string> CharaTypes { get; set; }
        public ObservableCollection<string> Skins { get; set; }
        public ObservableCollection<string> Bodies { get; set; }
        public ObservableCollection<string> Genders { get; set; }
        public ObservableCollection<string> AvailableModels { get; set; }

        private int _selectedYear;
        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                if (SetProperty(ref _selectedYear, value) && _selectedCharabase != null)
                {
                    _selectedCharabase.Year = value;
                }
            }
        }

        private int _selectedCharaType;
        public int SelectedCharaType
        {
            get => _selectedCharaType;
            set
            {
                if (SetProperty(ref _selectedCharaType, value) && _selectedCharabase != null)
                {
                    _selectedCharabase.CharaBaseType = value;
                    UpdateAvailableModels();
                }
            }
        }

        private int _selectedSkin;
        public int SelectedSkin
        {
            get => _selectedSkin;
            set
            {
                if (SetProperty(ref _selectedSkin, value) && _selectedCharabase != null)
                {
                    _selectedCharabase.Skin = value;
                    UpdateSkinColor();
                }
            }
        }

        private SolidColorBrush _skinColor;
        public SolidColorBrush SkinColor
        {
            get => _skinColor;
            set => SetProperty(ref _skinColor, value);
        }

        private int _selectedBody;
        public int SelectedBody
        {
            get => _selectedBody;
            set
            {
                if (SetProperty(ref _selectedBody, value) && _selectedCharabase != null)
                {
                    _selectedCharabase.Body = value;
                }
            }
        }

        private int _selectedGender;
        public int SelectedGender
        {
            get => _selectedGender;
            set
            {
                if (SetProperty(ref _selectedGender, value) && _selectedCharabase != null)
                {
                    _selectedCharabase.Gender = value;
                }
            }
        }

        private string _selectedModel;
        public string SelectedModel
        {
            get => _selectedModel;
            set
            {
                if (SetProperty(ref _selectedModel, value) && _selectedCharabase != null && !string.IsNullOrEmpty(value))
                {
                    if (int.TryParse(value, out int modelNumber))
                    {
                        _selectedCharabase.ModelNumber = modelNumber;
                        UpdateCharacterFace();
                    }
                }
            }
        }

        #endregion

        #region Commands

        public ICommand EditFullNameCommand { get; }
        public ICommand EditNicknameCommand { get; }
        public ICommand EditDescriptionCommand { get; }
        public ICommand InsertCharabaseCommand { get; }
        public ICommand DeleteCharabaseCommand { get; }
        public ICommand ExportAsCfgBinCommand { get; }
        public ICommand ExportAsCsvCommand { get; }

        #endregion

        public PanelCharabaseViewModel()
        {
            CharabaseTree = new ObservableCollection<CharabaseTreeNode>();
            Years = new ObservableCollection<string>();
            CharaTypes = new ObservableCollection<string> { "Unknown", "Player", "Unused", "NPC", "NPC Other" };
            Skins = new ObservableCollection<string>();
            Bodies = new ObservableCollection<string>();
            Genders = new ObservableCollection<string>();
            AvailableModels = new ObservableCollection<string>();

            EditFullNameCommand = new RelayCommand(ExecuteEditFullName);
            EditNicknameCommand = new RelayCommand(ExecuteEditNickname);
            EditDescriptionCommand = new RelayCommand(ExecuteEditDescription);
            InsertCharabaseCommand = new RelayCommand(ExecuteInsertCharabase);
            DeleteCharabaseCommand = new RelayCommand(ExecuteDeleteCharabase, CanDeleteCharabase);
            ExportAsCfgBinCommand = new RelayCommand(ExecuteExportAsCfgBin);
            ExportAsCsvCommand = new RelayCommand(ExecuteExportAsCsv);
        }

        public void Initialize(Game game)
        {
            _game = game;
            _charabases = game.GetCharabase().ToList();

            BuildTreeView();
            PopulateDropdowns();
        }

        private void BuildTreeView()
        {
            CharabaseTree.Clear();

            var rootNode = new CharabaseTreeNode { Name = "Charabase" };

            // Group by CharaBaseType
            var unknownNode = new CharabaseTreeNode { Name = "Unknown" };
            var playerNode = new CharabaseTreeNode { Name = "Player" };
            var unusedNode = new CharabaseTreeNode { Name = "Unused" };
            var npcNode = new CharabaseTreeNode { Name = "NPC" };
            var npcOtherNode = new CharabaseTreeNode { Name = "NPC Other" };

            foreach (var charabase in _charabases)
            {
                var node = new CharabaseTreeNode
                {
                    Name = GetCharacterName(charabase),
                    Charabase = charabase
                };

                switch (charabase.CharaBaseType)
                {
                    case 0:
                        unknownNode.Children.Add(node);
                        break;
                    case 1:
                        playerNode.Children.Add(node);
                        break;
                    case 2:
                        unusedNode.Children.Add(node);
                        break;
                    case 3:
                        npcNode.Children.Add(node);
                        break;
                    case 4:
                        npcOtherNode.Children.Add(node);
                        break;
                }
            }

            rootNode.Children.Add(unknownNode);
            rootNode.Children.Add(playerNode);
            rootNode.Children.Add(unusedNode);
            rootNode.Children.Add(npcNode);
            rootNode.Children.Add(npcOtherNode);

            CharabaseTree.Add(rootNode);
        }

        private void FilterCharabases()
        {
            // TODO: Implement filtering logic
            BuildTreeView();
        }

        private string GetCharacterName(ICharabase charabase)
        {
            // TODO: Get name from T2bþ file
            return $"Character {charabase.BaseHash:X8}";
        }

        public void SelectCharabase(ICharabase charabase)
        {
            _selectedCharabase = charabase;
            IsCharacterSelected = true;

            SelectedCharacterHash = charabase.BaseHash.ToString("X8");
            SelectedCharacterFullName = GetCharacterName(charabase);
            SelectedCharacterNickname = GetCharacterNickname(charabase);
            SelectedCharacterDescription = GetCharacterDescription(charabase);

            SelectedYear = charabase.Year;
            SelectedCharaType = charabase.CharaBaseType;
            SelectedSkin = charabase.Skin;
            SelectedBody = charabase.Body;
            SelectedGender = charabase.Gender;

            UpdateAvailableModels();
            SelectedModel = charabase.ModelNumber.ToString();
            UpdateCharacterFace();
            UpdateSkinColor();
        }

        private string GetCharacterNickname(ICharabase charabase)
        {
            // TODO: Get nickname from T2bþ file
            return "";
        }

        private string GetCharacterDescription(ICharabase charabase)
        {
            // TODO: Get description from T2bþ file
            return "";
        }

        private void UpdateAvailableModels()
        {
            AvailableModels.Clear();
            // TODO: Load models based on CharaBaseType
            for (int i = 0; i < 100; i++)
            {
                AvailableModels.Add(i.ToString());
            }
        }

        private void UpdateCharacterFace()
        {
            // TODO: Load character face image
            CharacterFaceImage = null;
        }

        private void UpdateSkinColor()
        {
            // TODO: Get color based on skin index
            //var colors = new[] { Colors.White, Colors.Beige, Colors.Tan, Colors.Brown, Colors.DarkBrown };
            //if (SelectedSkin < colors.Length)
            //{
            //    SkinColor = new SolidColorBrush(colors[SelectedSkin]);
            //}
        }

        private void PopulateDropdowns()
        {
            // TODO: Populate from enums
            Years.Add("Unknown");
            Skins.Add("Unused");
            Bodies.Add("Tall");
            Genders.Add("Unknown");
        }

        private void ExecuteEditFullName(object parameter)
        {
            // TODO: Open text editor
        }

        private void ExecuteEditNickname(object parameter)
        {
            // TODO: Open text editor
        }

        private void ExecuteEditDescription(object parameter)
        {
            // TODO: Open text editor
        }

        private void ExecuteInsertCharabase(object parameter)
        {
            // TODO: Insert new charabase
        }

        private bool CanDeleteCharabase(object parameter)
        {
            return _selectedCharabase != null;
        }

        private void ExecuteDeleteCharabase(object parameter)
        {
            // TODO: Delete selected charabase
        }

        private void ExecuteExportAsCfgBin(object parameter)
        {
            // TODO: Export as cfg.bin
        }

        private void ExecuteExportAsCsv(object parameter)
        {
            // TODO: Export as CSV
        }
    }

    public class CharabaseTreeNode : BaseViewModel
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public ICharabase Charabase { get; set; }

        public ObservableCollection<CharabaseTreeNode> Children { get; set; }

        public CharabaseTreeNode()
        {
            Children = new ObservableCollection<CharabaseTreeNode>();
        }
    }
}