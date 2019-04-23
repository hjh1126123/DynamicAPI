using HDBChartModel.ServiceModel;
using System.Data;
using WEB.Plugin.CommonHelper;

namespace HDBChartService
{
    public class H_HDBUsersSer
    {
        public DataTable GetUser(string userCode)
        {
            if (HelperMediator.GetInstance().stringHelper.CheckSpecialCharacters(userCode))
            {
                return null;
            }


            H_HDBUsers h_HDBUsers = new H_HDBUsers()
            {
                UserCodeHDB = userCode
            };

            return ServiceConfig.GetInstance().GetOperation().SimpleQuery(h_HDBUsers);
        }

        public bool CheckRoleCode(string UserCode, string roleCode)
        {
            if (HelperMediator.GetInstance().stringHelper.CheckSpecialCharacters(UserCode, roleCode))
            {
                return false;
            }


            H_HDBUser_Role r_HDBUser_Role = new H_HDBUser_Role()
            {
                UserCodeHDB = UserCode
            };

            DataTable dataTable = ServiceConfig.GetInstance().GetOperation().SimpleQuery(r_HDBUser_Role);
            DataRow[] dataRows = dataTable.Select(string.Format("RoleCode = '{0}'", roleCode));

            if (dataRows.Length > 0)
            {
                return true;
            }

            return false;
        }
    }
}
