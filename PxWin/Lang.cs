using System.Globalization;
using System.Threading;
using PCAxis.Paxiom;
using PCAxis.Paxiom.Localization;

namespace PCAxis.Desktop
{
    public static class Lang
    {
        public static string GetLocalizedString(string key)
        {
            return GetLocalizedString(key, Thread.CurrentThread.CurrentCulture);
        }

        public static string CurrentLanguage()
        {
            return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        }

        public static string Translate(this string key)
        {
            return GetLocalizedString(key, Thread.CurrentThread.CurrentCulture);
        }

        public static string GetLocalizedString(string key, CultureInfo culture)
        {
            return GetResourceManager().GetString(key, culture);
        }

        private static PxResourceManager GetResourceManager()
        {
            return PxResourceManager.GetResourceManager();
        }

        /// <summary>
        /// Sätter ny titel på modellen. Byter ut by, and, PxcMetaTitleBy och PxcMetaTitleAnd, till rätt språk, alltså det som
        /// språk som px-modellen är skapad för, och inte själva applikationen.
        /// </summary>
        public static void SetTitleForModel(PXModel model)
        {
            model.Meta.Title = GetTranslatedTitleForModel(model.Meta.Title, model.Meta.Language);
        }

        /// <summary>
        /// Returnerar en översatt sträng. Byter ut by, and, PxcMetaTitleBy och PxcMetaTitleAnd, till rätt språk, alltså det som
        /// språk som px-modellen är skapad för, och inte själva applikationen.
        /// </summary>
        public static string GetTranslatedTitleForModel(string title, string language)
        {

            if (language == null)
            {
                language = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            }
            
            //Ta bort eventuella språk-detaljer: "en-US" => "en"
            var cultureName = CultureInfo.CreateSpecificCulture(language).Parent;

            if (title == null)
            {
                title = "";
            }

            title = title.Replace("PxcMetaTitleBy", GetLocalizedString("by", cultureName));
            title = title.Replace("PxcMetaTitleAnd", GetLocalizedString("and", cultureName));


            //TODO: Return DESCRIPTION if DESCRIPTIONDEFAULT = YES and DESCRIPTION is not null

            return title;
        }
    }
}