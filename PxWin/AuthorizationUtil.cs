using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCAxis.Desktop
{
    public class AuthorizationUtil
    {
        public static bool IsAuthorized(string databaseId, string menu, string selection)
        {
            var dbInfo = DatabaseRepository.Current.GetDatabase(databaseId);

            var authMetod = dbInfo.GetParam("authorizationMethod");

            if (!string.IsNullOrEmpty(authMetod))
            {
                try
                {
                    var typeString = dbInfo.GetParam(DatabaseInfo.AUTHORIZATION_METHOD);
                    var parts = typeString.Split(',');
                    var typeName = parts[0].Trim();
                    var assemblyName = parts[1].Trim();
                    PX.Security.IAuthorization authorizer = (PX.Security.IAuthorization)Activator.CreateInstance(assemblyName, typeName).Unwrap();
                     return authorizer.IsAuthorized(databaseId, menu, selection);
                }
                catch (Exception)
                {
                    //TODO log error
                    return false;
                }
            }

            return true;
        }
    }
}
