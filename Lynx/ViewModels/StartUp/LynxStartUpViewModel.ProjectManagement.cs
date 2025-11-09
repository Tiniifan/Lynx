using System;
using System.Linq;
using System.Collections.ObjectModel;
using Lynx.Models;
using Lynx.Services;

namespace Lynx.ViewModels.StartUp
{
    public partial class LynxStartUpViewModel
    {
        #region Project Management Methods

        /// <summary>
        /// Loads all projects from the service.
        /// </summary>
        private void LoadProjects()
        {
            var projects = ProjectService.LoadProjects();
            Projects = new ObservableCollection<ProjectData>(projects);
        }

        /// <summary>
        /// Checks if a project can be created.
        /// </summary>
        private bool CanCreateProject(object parameter)
        {
            return !string.IsNullOrWhiteSpace(NewProjectName) &&
                   !string.IsNullOrWhiteSpace(NewProjectPath) &&
                   !string.IsNullOrWhiteSpace(NewProjectLanguage);
        }

        /// <summary>
        /// Creates a new project using the current parameters.
        /// </summary>
        private void CreateProject(object parameter)
        {
            try
            {
                var project = ProjectService.CreateProject(
                    NewProjectName,
                    NewProjectLanguage,
                    NewProjectPath,
                    NewProjectImagePath
                );

                project.Game = NewProjectGame;
                project.IsExtractedFaFiles = IsExtractedFaFiles;

                Projects.Add(project);
                ProjectService.SaveProjects(Projects.ToList());

                // Reset fields
                ResetProjectFields();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating project: {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a project.
        /// </summary>
        private void DeleteProject(object parameter)
        {
            var project = parameter as ProjectData;
            if (project == null) return;

            try
            {
                var projectsList = Projects.ToList();
                ProjectService.DeleteProject(project, projectsList);
                Projects.Remove(project);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting project: {ex.Message}");
            }
        }

        /// <summary>
        /// Opens an existing project.
        /// </summary>
        private void OpenProject(object parameter)
        {
            var project = parameter as ProjectData;
            if (project == null || !project.Exists) return;

            ProjectOpened?.Invoke(this, project);
        }

        /// <summary>
        /// Resets all fields in the project creation form.
        /// </summary>
        private void ResetProjectFields()
        {
            NewProjectName = string.Empty;
            NewProjectPath = string.Empty;
            NewProjectImagePath = string.Empty;
            NewProjectLanguage = AvailableLanguages.FirstOrDefault() ?? "EN";
            IsExtractedFaFiles = false;
            NewProjectGame = GameType.IEGO;
        }

        #endregion
    }
}