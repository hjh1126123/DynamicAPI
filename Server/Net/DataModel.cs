namespace Server
{
    public class DataModel
    {
        string key;
        string condition;
        string @value;

        /// <summary>
        /// 键
        /// </summary>
        public string Key
        {
            get => key;
            set => key = value.Trim().ToLower();
        }

        /// <summary>
        /// 参数化键
        /// </summary>
        public string ParKey
        {
            get => $"p_{Key.Trim().ToLower()}";            
        }

        /// <summary>
        /// 条件符号
        /// </summary>
        public string Condition
        {
            get => condition;
            set => condition = value.Trim().ToLower();
        }

        /// <summary>
        /// 值
        /// </summary>
        public string Value
        {
            get => @value;
            set => this.@value = value;
        }
    }
}
