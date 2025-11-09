using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lynx.Models;
using Newtonsoft.Json;

namespace Lynx.Services
{
    public static class ProjectService
    {
        private const string ProjectsDirectory = "projects";
        private const string ProjectsFile = "projects/projects.json";

        private class ProjectsContainer
        {
            public List<ProjectData> Projects { get; set; } = new List<ProjectData>();
        }

        static ProjectService()
        {
            if (!Directory.Exists(ProjectsDirectory))
            {
                Directory.CreateDirectory(ProjectsDirectory);
            }
        }

        public static List<ProjectData> LoadProjects()
        {
            try
            {
                if (!File.Exists(ProjectsFile))
                {
                    return new List<ProjectData>();
                }

                string json = File.ReadAllText(ProjectsFile);
                var container = JsonConvert.DeserializeObject<ProjectsContainer>(json);

                if (container == null || container.Projects == null)
                {
                    return new List<ProjectData>();
                }

                // Check the existence of each project
                foreach (var project in container.Projects)
                {
                    // If it is IEGO and extracted .fa files is not enabled, check the .fa file.
                    if (project.Game == GameType.IEGO && !project.IsExtractedFaFiles)
                    {
                        project.Exists = File.Exists(project.Path);
                    }
                    else
                    {
                        // For all other cases, check the file
                        project.Exists = Directory.Exists(project.Path);
                    }
                }

                return container.Projects;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProjectManager] Error loading projects: {ex.Message}");
                return new List<ProjectData>();
            }
        }

        public static void SaveProjects(List<ProjectData> projects)
        {
            try
            {
                var container = new ProjectsContainer
                {
                    Projects = projects
                };

                string json = JsonConvert.SerializeObject(container, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(ProjectsFile, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ProjectManager] Error saving projects: {ex.Message}");
            }
        }

        public static ProjectData CreateProject(string name, string language, string path, string imagePath = null)
        {
            var project = new ProjectData
            {
                Name = name,
                Language = language,
                //Version = version,
                Path = path,
                Exists = true
            };

            // Copier l'image si fournie
            if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
            {
                try
                {
                    string imageFileName = project.Id + ".png";
                    string destinationPath = System.IO.Path.Combine(ProjectsDirectory, imageFileName);

                    // Copier et convertir en PNG si nécessaire
                    File.Copy(imagePath, destinationPath, true);
                    project.ImagePath = imageFileName;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ProjectManager] Error copying image: {ex.Message}");
                }
            }

            // Créer le dossier du projet s'il n'existe pas
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ProjectManager] Error creating project directory: {ex.Message}");
                }
            }

            return project;
        }

        public static void DeleteProject(ProjectData project, List<ProjectData> projects)
        {
            // Supprimer l'image si elle existe
            if (!string.IsNullOrEmpty(project.ImagePath))
            {
                try
                {
                    string imagePath = System.IO.Path.Combine(ProjectsDirectory, project.ImagePath);
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ProjectManager] Error deleting image: {ex.Message}");
                }
            }

            // Retirer le projet de la liste
            projects.Remove(project);
            SaveProjects(projects);
        }

        public static string GetDefaultImagePath()
        {
            // Retourne une image par défaut si disponible
            return null;
        }
    }
}
