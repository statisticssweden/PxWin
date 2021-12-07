using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PCAxis.Desktop
{
    public class LanguageHelper
    {
        /// <summary>
        /// Get available languages (languages that have language file in the configured language directory)
        /// </summary>
        /// <returns>List of available languages</returns>
        public static List<string> GetLanguages()
        {
            List<string> langs = new List<string>();
            string fileNameWithoutExtension;
            string[] splitFileName;
            string language;

            string path = PCAxis.Paxiom.Localization.PxResourceReader.LanguagePath;
            DirectoryInfo dir;

            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory), path);
            }

            if (Directory.Exists(path))
            {
                dir = new DirectoryInfo(path);

                foreach (FileInfo file in dir.GetFiles())
                {
                    fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FullName);
                    splitFileName = fileNameWithoutExtension.Split('.');

                    if (splitFileName.Length > 1)
                    {
                        language = splitFileName[1];
                    }
                    else
                    {
                        language = "en";
                    }

                    langs.Add(language);
                }
            }
            else
            {
                throw new System.Exception("Could not load languages from directory '" + path + "'");
                //MessageBox.Show("Could not load languages from directory '" + path + "'", "Error");
            }

            return langs;
        }

        /// <summary>
        /// Checks if a language is the default language
        /// </summary>
        /// <param name="lang">Language string</param>
        /// <returns>True if it is the default language, else false</returns>
        public static bool IsDefaultLanguage(string lang)
        {
            string defaultLanguage = string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings.Get("defaultLanguage")) ? "en" : System.Configuration.ConfigurationManager.AppSettings.Get("defaultLanguage");
            return lang.Equals(defaultLanguage);
        }
        
        /// <summary>
        /// Get which language to use for the database
        /// </summary>
        /// <returns>Selected language (two letter code)</returns>
        public static string GetTableLanguage(DatabaseInfo dbInfo)
        {
            string selLang = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName; // Selected language in PxWin

            if (dbInfo.Id == "FileSystem")
            {
                return selLang;
            }
            else
            {
                if (dbInfo.Languages.Contains(selLang))
                {
                    return selLang;
                }
                else
                {
                    return dbInfo.DefaultLanguage;
                }
            }
        }
    }
}
