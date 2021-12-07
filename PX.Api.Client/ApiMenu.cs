using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCAxis.Menu;
using PCAxis.Menu.Exceptions;
using System.Net.Http;
using Newtonsoft.Json;

namespace PX.Api.Client
{
    public class ApiMenu : PCAxis.Menu.PxMenuBase
    {

        private string _baseUrl;

        public ApiMenu(string baseUrl, string selection, string language)
        {
            //Corrects the baseUrl
            if (baseUrl.EndsWith("/"))
            {
                _baseUrl = baseUrl.Substring(0, baseUrl.Length - 1);
            }
            else
            {
                _baseUrl = baseUrl;
            }
            _baseUrl = string.Format(_baseUrl, language);

            string menu;
            if (selection.Contains("/"))
            {
                int index = selection.LastIndexOf("/");
                menu = selection.Substring(0, index);
                selection = selection.Substring(index + 1);
            }
            else
            {
                menu = selection;
                selection = "";
            }

            RootItem = new PxMenuItem(this, "", "", "", menu, selection, "");
            SetCurrentItemBySelection(RootItem.ID.Menu, RootItem.ID.Selection);

        }

        public override bool SetCurrentItemBySelection(string menu, string selection)
        {
            if (base.SetCurrentItemBySelection(menu, selection))
            {
                Item item = RootItem.FindSelection(menu, selection);

                if (item is PxMenuItem)
                {
                    PxMenuItem menuItem = (PxMenuItem)item;
                    if (!menuItem.HasBeenLoaded())
                        Load(menuItem);
                }
            }
            else
                return false;

            return true;
        }

        protected void Load(PxMenuItem menuItem)
        {
            if (menuItem.HasBeenLoaded())
                throw new ItemHasBeenLoadedException("DatamodelMenu does not allow subsequent loads on its MenuItems.");

            menuItem.HasBeenLoaded(true);

            Level[] levels = GetLevels(menuItem.ID.Menu, menuItem.ID.Selection);

            if (levels != null)
            {
                foreach (var level in levels)
                {
                    if (level.Type == "l")
                    {
                        menuItem.AddSubItem(
                            new PxMenuItem(
                                this,
                                level.Text,
                                level.Text,
                                "TODO",
                                menuItem.ID.Menu + (menuItem.ID.Menu == "/" ? "" : "/") + menuItem.ID.Selection,
                                level.Id,
                                null));
                    }
                    else if (level.Type == "t")
                    {
                        menuItem.AddSubItem(
                            new TableLink(
                                level.Text,
                                level.Text,
                                "TODO SORT",
                                menuItem.ID.Menu + "/" + menuItem.ID.Selection,
                                level.Id,
                                null,
                                LinkType.Table,
                                TableStatus.AccessibleToAll,
                                level.Updated,
                                null,
                                "",
                                "",
                                "",
                                PresCategory.Official));
                    }
                }
            }

        }

        private Level[] GetLevels(string menu, string selection)
        {
            var http = new HttpClient();

            try
            {
                var response = http.GetAsync(_baseUrl + menu + (menu == "/" ? "" : "/") + selection).Result;

                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<Level[]>(data).OrderBy(l => l.Text).ToArray();
                }

            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }


    }


    public class Level
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public DateTime? Updated { get; set; }
    }

    /// <summary>
    /// Extensionmethods for DatamodelMenu
    /// </summary>
    public static class ApiMenuExtensions
    {
        //TODO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool HasBeenLoaded(this PxMenuItem i)
        {
            return i.HasAttribute("HasBeenLoaded") && i.GetAttribute<bool>("HasBeenLoaded");
        }

        //TODO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="value"></param>
        public static void HasBeenLoaded(this PxMenuItem i, bool value)
        {
            i.SetAttribute("HasBeenLoaded", value);
        }
    }
}
