using System.Collections.Generic;
using System.Text;

namespace Data
{
    public abstract class IData
    {
        protected StringBuilder sqlBuilder;

        public Dictionary<string, List<DataModel>> dictParams;

        public IData(Dictionary<string, List<DataModel>> @params)
        {
            sqlBuilder = new StringBuilder();
            dictParams = @params;
        }

        public IData(List<DataModel> dms)
        {
            sqlBuilder = new StringBuilder();
            dictParams = new Dictionary<string, List<DataModel>>();
            dictParams.Add("default", dms);
        }

        public string SQL
        {
            get
            {
                Build(sqlBuilder);
                return sqlBuilder.ToString();
            }
        }

        /// <summary>
        /// 添加查询条件
        /// </summary>
        /// <param name="key">条件键值</param>
        protected void AddWhere(string index, string key)
        {
            foreach (var m in dictParams[key])
            {
                if (!string.IsNullOrWhiteSpace(m.Value))
                {
                    string tmpK = string.IsNullOrWhiteSpace(index) ? m.Key : $"{index}.{m.Key}";
                    if (m.Condition.Equals("in"))
                    {
                        int maxCount = m.Value.Split(',').Length;
                        sqlBuilder.Append($" and {tmpK} {m.Condition} (");
                        for (int vCount = 0; vCount < maxCount; vCount++)
                        {
                            sqlBuilder.Append($"@{m.ParKey}{vCount}");
                            if (vCount < maxCount - 1)
                            {
                                sqlBuilder.Append(",");
                            }
                        }
                        sqlBuilder.Append(") ");
                    }
                    else
                    {
                        sqlBuilder.Append($" and {tmpK} {m.Condition} @{m.ParKey} ");
                    }
                }
            }
        }

        /// <summary>
        /// 添加查询条件（默认key）
        /// </summary>
        protected void AddWhere(string index)
        {
            AddWhere(index, "default");
        }

        protected abstract void Build(StringBuilder sql);
    }
}
