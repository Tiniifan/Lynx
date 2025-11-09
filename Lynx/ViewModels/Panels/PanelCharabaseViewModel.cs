using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ImaginationGUI.ViewModels;
using Lynx.Models.InazumaEleven.Games;
using Lynx.Models.InazumaEleven.Logic;
using StudioElevenLib.Level5.Image;
using StudioElevenLib.Level5.Text;
using StudioElevenLib.Tools;

namespace Lynx.ViewModels.Panels
{
    public class PanelCharabaseViewModel : BaseViewModel
    {
        private Game _game;
        private List<ICharabase> _charabases;
        private ICharabase _selectedCharabase;
        private T2bþ _charanames;
        private T2bþ _charaKiznaxHint;
        private Dictionary<string, List<int>> _models;

        #region Properties

        public ObservableCollection<CharabaseListItem> CharabaseItems { get; set; }

        private ICollectionView _charabaseListView;
        public ICollectionView CharabaseListView
        {
            get => _charabaseListView;
            set => SetProperty(ref _charabaseListView, value);
        }

        private CharabaseListItem _selectedCharabaseItem;
        public CharabaseListItem SelectedCharabaseItem
        {
            get => _selectedCharabaseItem;
            set
            {
                if (SetProperty(ref _selectedCharabaseItem, value) && value?.Charabase != null)
                {
                    SelectCharabase(value.Charabase);
                }
            }
        }

        private string _searchText = "Search...";
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    if (value != "Search...")
                    {
                        FilterCharabases();
                    }
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
        public ICommand EditWithNyankoCommand { get; }
        public ICommand InsertCharabaseCommand { get; }
        public ICommand DeleteCharabaseCommand { get; }
        public ICommand ExportAsCfgBinCommand { get; }
        public ICommand ExportAsCsvCommand { get; }

        #endregion

        public PanelCharabaseViewModel()
        {
            CharabaseItems = new ObservableCollection<CharabaseListItem>();
            Years = new ObservableCollection<string>();
            CharaTypes = new ObservableCollection<string> { "Unknown", "Player", "Unused", "NPC", "NPC Other" };
            Skins = new ObservableCollection<string>();
            Bodies = new ObservableCollection<string>();
            Genders = new ObservableCollection<string>();
            AvailableModels = new ObservableCollection<string>();

            EditFullNameCommand = new RelayCommand(ExecuteEditFullName);
            EditNicknameCommand = new RelayCommand(ExecuteEditNickname);
            EditDescriptionCommand = new RelayCommand(ExecuteEditDescription);
            EditWithNyankoCommand = new RelayCommand(ExecuteEditWithNyanko);
            InsertCharabaseCommand = new RelayCommand(ExecuteInsertCharabase);
            DeleteCharabaseCommand = new RelayCommand(ExecuteDeleteCharabase, CanDeleteCharabase);
            ExportAsCfgBinCommand = new RelayCommand(ExecuteExportAsCfgBin);
            ExportAsCsvCommand = new RelayCommand(ExecuteExportAsCsv);
        }

        public void Initialize(Game game)
        {
            _game = game;
            _charabases = game.GetCharabase().ToList();

            // Load text files
            LoadTextFiles();

            // Load models dictionary
            LoadModels();

            BuildListView();
            PopulateDropdowns();
        }

        private void LoadTextFiles()
        {
            // Load character names
            GameSupports.GameFile charaText = _game.Files["chara_text"];
            _charanames = new T2bþ(charaText.File.Directory.GetFileFromFullPath(charaText.Path));

            // Load character descriptions if available
            if (_game.Files.ContainsKey("kiznax_hint_text"))
            {
                GameSupports.GameFile kiznaxHintText = _game.Files["kiznax_hint_text"];
                _charaKiznaxHint = new T2bþ(kiznaxHintText.File.Directory.GetFileFromFullPath(kiznaxHintText.Path));
            }
        }

        private void LoadModels()
        {
            _models = new Dictionary<string, List<int>>
            {
                ["NPC"] = new List<int>(),
                ["NPCOther"] = new List<int>(),
                ["Player"] = new List<int>()
            };

            // Get model info
            GameSupports.GameFile modelRpgPlayer = _game.Files["modelRpgPlayer"];
            GameSupports.GameFile modelWazaPlayer = _game.Files["modelWazaPlayer"];
            GameSupports.GameFile modelRpgNPC = _game.Files["modelRpgNPC"];
            GameSupports.GameFile modelWazaNPC = _game.Files["modelWazaNPC"];

            // Get available models
            FillModel(modelRpgPlayer);
            FillModel(modelWazaPlayer);
            FillModel(modelRpgNPC);
            FillModel(modelWazaNPC);
        }

        private void FillModel(GameSupports.GameFile gameFile)
        {
            foreach (string fileName in gameFile.File.Directory.GetFolderFromFullPath(gameFile.Path).Files.Keys)
            {
                if (fileName.EndsWith(".xi") || fileName.EndsWith(".xc"))
                {
                    int fileNumber = Convert.ToInt32(fileName
                                    .Replace("ca", "")
                                    .Replace("cn", "")
                                    .Replace("cp", "")
                                    .Replace("a", "")
                                    .Replace("m", "")
                                    .Replace(".xi", "")
                                    .Replace(".xc", "")
                                );

                    if (fileName.StartsWith("ca"))
                    {
                        if (_models["NPCOther"].IndexOf(fileNumber) == -1)
                        {
                            _models["NPCOther"].Add(fileNumber);
                        }
                    }
                    else if (fileName.StartsWith("cn"))
                    {
                        if (_models["NPC"].IndexOf(fileNumber) == -1)
                        {
                            _models["NPC"].Add(fileNumber);
                        }
                    }
                    else if (fileName.StartsWith("cp"))
                    {
                        if (_models["Player"].IndexOf(fileNumber) == -1)
                        {
                            _models["Player"].Add(fileNumber);
                        }
                    }
                }
            }
        }

        #region ListView Building

        private void BuildListView()
        {
            CharabaseItems.Clear();

            var groups = new[]
            {
                new { Type = 0, Name = "Unknown" },
                new { Type = 1, Name = "Player" },
                new { Type = 2, Name = "Unused" },
                new { Type = 3, Name = "NPC" },
                new { Type = 4, Name = "NPC Other" }
            };

            foreach (var group in groups)
            {
                var chars = _charabases.Where(c => c.CharaBaseType == group.Type).ToList();

                foreach (var charabase in chars)
                {
                    CharabaseItems.Add(new CharabaseListItem
                    {
                        Name = GetCharacterName(charabase),
                        Charabase = charabase,
                        CategoryName = group.Name
                    });
                }
            }

            CharabaseListView = CollectionViewSource.GetDefaultView(CharabaseItems);
            CharabaseListView.GroupDescriptions.Add(new PropertyGroupDescription("CategoryName"));
        }

        #endregion

        private void FilterCharabases()
        {
            if (CharabaseListView == null) return;

            if (string.IsNullOrWhiteSpace(SearchText) || SearchText == "Search...")
            {
                CharabaseListView.Filter = null;
            }
            else
            {
                CharabaseListView.Filter = obj =>
                {
                    if (obj is CharabaseListItem item)
                    {
                        return item.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0;
                    }
                    return false;
                };
            }
        }

        private string GetCharacterName(ICharabase charabase)
        {
            if (_charanames != null && _charanames.Nouns.TryGetValue(charabase.NameHash, out var noun) && noun.Strings.Count > 0)
            {
                return noun.Strings[0].Text;
            }
            return $"Character {charabase.BaseHash:X8}";
        }

        private string FindCharacterHash(ICharabase charabase)
        {
            for (int i = 0; i < 10000; i++)
            {
                string namePlayer = "cp" + i.ToString().PadLeft(4, '0');
                string nameNPC = "cn" + i.ToString().PadLeft(4, '0');
                string nameNPCOther = "ca" + i.ToString().PadLeft(4, '0');

                if (SameCharacterHash(charabase, namePlayer))
                {
                    return namePlayer;
                }
                else if (SameCharacterHash(charabase, nameNPC))
                {
                    return nameNPC;
                }
                else if (SameCharacterHash(charabase, nameNPCOther))
                {
                    return nameNPCOther;
                }
            }

            return charabase.BaseHash.ToString("X8");
        }

        private bool SameCharacterHash(ICharabase charabase, string name)
        {
            int crc32 = unchecked((int)Crc32.Compute(Encoding.UTF8.GetBytes(name)));
            return charabase.BaseHash == crc32;
        }

        public void SelectCharabase(ICharabase charabase)
        {
            _selectedCharabase = charabase;
            IsCharacterSelected = true;

            SelectedCharacterHash = FindCharacterHash(charabase);
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
            if (_charanames != null && _charanames.Nouns.ContainsKey(charabase.NicknameHash))
            {
                return _charanames.Nouns[charabase.NicknameHash].Strings[0].Text;
            }
            return string.Empty;
        }

        private string GetCharacterDescription(ICharabase charabase)
        {
            if (_charaKiznaxHint != null && _charaKiznaxHint.Texts.ContainsKey(charabase.DescriptionHash))
            {
                return _charaKiznaxHint.Texts[charabase.DescriptionHash].Strings[0].Text;
            }
            return string.Empty;
        }

        private void UpdateAvailableModels()
        {
            AvailableModels.Clear();

            if (_selectedCharabase == null || _models == null) return;

            List<int> models = null;

            switch (_selectedCharabase.CharaBaseType)
            {
                case 1: // Player
                    models = _models["Player"];
                    break;
                case 3: // NPC
                    models = _models["NPC"];
                    break;
                case 4: // NPC Other
                    models = _models["NPCOther"];
                    break;
            }

            if (models != null)
            {
                foreach (var model in models)
                {
                    AvailableModels.Add(model.ToString());
                }
            }
        }

        private void UpdateCharacterFace()
        {
            if (_selectedCharabase == null || _game == null) return;

            try
            {
                string fileCode = GetFileCodeForCharaType(_selectedCharabase.CharaBaseType);
                if (string.IsNullOrEmpty(fileCode))
                {
                    CharacterFaceImage = null;
                    return;
                }

                GameSupports.GameFile faceInfo = _game.Files["face"];
                VirtualDirectory faceFolder = faceInfo.File.Directory.GetFolderFromFullPath(faceInfo.Path);

                string faceFileName = fileCode + _selectedCharabase.ModelNumber.ToString().PadLeft(4, '0') + "a.xi";

                if (faceFolder.Files.ContainsKey(faceFileName))
                {
                    byte[] imageData = faceInfo.File.Directory.GetFileFromFullPath(faceInfo.Path + "/" + faceFileName);
                    var bitmap = IMGC.ToBitmap(imageData);

                    // Convert System.Drawing.Bitmap to BitmapImage
                    using (MemoryStream memory = new MemoryStream())
                    {
                        bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                        memory.Position = 0;

                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = memory;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();
                        bitmapImage.Freeze();

                        CharacterFaceImage = bitmapImage;
                    }
                }
                else
                {
                    CharacterFaceImage = null;
                }
            }
            catch
            {
                CharacterFaceImage = null;
            }
        }

        private string GetFileCodeForCharaType(int charaType)
        {
            switch (charaType)
            {
                case 1: return "cp"; // Player
                case 3: return "cn"; // NPC
                case 4: return "ca"; // NPC Other
                default: return null;
            }
        }

        private void UpdateSkinColor()
        {
            if (_selectedCharabase == null) return;

            // Get color from EnumHelper - cette méthode doit être implémentée dans votre EnumHelper
            // Color color = EnumHelper.GetColorById<Skins>(_selectedCharabase.Skin);
            // SkinColor = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));

            // Placeholder implementation
            SkinColor = new SolidColorBrush(Colors.Beige);
        }

        private void PopulateDropdowns()
        {
            Years.Add("Unknown");
            Skins.Add("Unused");
            Bodies.Add("Tall");
            Genders.Add("Unknown");
        }

        #region Commands Implementation

        private void ExecuteEditFullName(object parameter)
        {
            // TODO: Ouvrir Nyanko pour éditer le nom complet
        }

        private void ExecuteEditNickname(object parameter)
        {
            // TODO: Ouvrir Nyanko pour éditer le surnom
        }

        private void ExecuteEditDescription(object parameter)
        {
            // TODO: Ouvrir Nyanko pour éditer la description
        }

        private void ExecuteEditWithNyanko(object parameter)
        {
            // TODO: Ouvrir Nyanko editor
            // Pour le moment le bouton ne fait rien comme demandé
        }

        private void ExecuteInsertCharabase(object parameter)
        {
            // TODO: Implémenter l'insertion
        }

        private bool CanDeleteCharabase(object parameter)
        {
            return _selectedCharabase != null;
        }

        private void ExecuteDeleteCharabase(object parameter)
        {
            // TODO: Implémenter la suppression
        }

        private void ExecuteExportAsCfgBin(object parameter)
        {
            // TODO: Implémenter l'export
        }

        private void ExecuteExportAsCsv(object parameter)
        {
            // TODO: Implémenter l'export CSV
        }

        #endregion
    }

    public class CharabaseListItem : BaseViewModel
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public ICharabase Charabase { get; set; }

        public string CategoryName { get; set; }
    }
}