using System.Linq;
using System.Collections.ObjectModel;
using Lynx.Models;

namespace Lynx.ViewModels.StartUp
{
    public partial class LynxStartUpViewModel
    {
        #region Game-Specific Logic

        /// <summary>
        /// Updates the available languages based on the selected game.
        /// </summary>
        private void UpdateAvailableLanguages()
        {
            switch (NewProjectGame)
            {
                case GameType.IEGO:
                case GameType.IEGOCS:
                    AvailableLanguages = new ObservableCollection<string> { "DE", "EN", "ES", "FR", "IT" };
                    break;

                case GameType.IEGOGalaxy:
                    AvailableLanguages = new ObservableCollection<string> { "JP" };
                    NewProjectLanguage = "JP"; // Force JP for Galaxy
                    break;

                default:
                    AvailableLanguages = new ObservableCollection<string> { "EN" };
                    break;
            }

            // If the current language is not in the list, select the first one
            if (!AvailableLanguages.Contains(NewProjectLanguage))
            {
                NewProjectLanguage = AvailableLanguages.FirstOrDefault();
            }
        }

        #endregion
    }
}
