using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Server.DBNet.Components
{
    public class BussCenter
    {
        public List<string> GetAllCenterCode()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("SELECT	[CenterCode] ");
            stringBuilder.AppendLine("FROM	[dbo].[B_BussCenter] ");
            stringBuilder.AppendLine("WHERE	[CenterType] = 'c01' ");
            stringBuilder.AppendLine("AND [IsPublic] = 'T' ");
            stringBuilder.AppendLine("GROUP BY [CenterCode]; ");

            DataTable dataTable = ServerKeeper.Instance.DBNetKeeper.Select(stringBuilder.ToString());

            List<string> strs = new List<string>();


            for (int rowCount = 0; rowCount < dataTable.Rows.Count; rowCount++)
            {
                strs.Add(dataTable.Rows[rowCount]["CenterCode"].ToString());
            }

            return strs;
        }
    }
}
