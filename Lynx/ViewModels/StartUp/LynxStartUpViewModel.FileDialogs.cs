using Lynx.Models;
using System.Windows.Forms;

namespace Lynx.ViewModels.StartUp
{
    public partial class LynxStartUpViewModel
    {
        #region File Dialog Methods

        /// <summary>
        /// Opens a dialog to select the project path (.fa file or folder).
        /// </summary>
        private void BrowsePath(object parameter)
        {
            // If IEGO and not extracted .fa files, allow .fa file selection
            if (NewProjectGame == GameType.IEGO && !IsExtractedFaFiles)
            {
                BrowseFaFile();
            }
            else
            {
                BrowseFolder();
            }
        }

        /// <summary>
        /// Opens a dialog to select a .fa file.
        /// </summary>
        private void BrowseFaFile()
        {
            var fileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "FA Files|*.fa|All Files|*.*",
                Title = "Select .fa File"
            };

            if (fileDialog.ShowDialog() == true)
            {
                NewProjectPath = fileDialog.FileName;
            }
        }

        /// <summary>
        /// Opens a dialog to select a folder.
        /// </summary>
        private void BrowseFolder()
        {
            var folderDialog = new FolderBrowserDialog
            {
                Description = "Select Project Folder"
            };

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                NewProjectPath = folderDialog.SelectedPath;
            }
        }

        /// <summary>
        /// Opens a dialog to select a project image.
        /// </summary>
        private void BrowseImage(object parameter)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp|All Files|*.*",
                Title = "Select Project Image"
            };

            if (dialog.ShowDialog() == true)
            {
                NewProjectImagePath = dialog.FileName;
            }
        }

        #endregion
    }
}
