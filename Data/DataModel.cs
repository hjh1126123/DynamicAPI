namespace Data
{
    public class DataModel
    {
        string key;
        string parKey;
        string condition;
        string @value;

        public string Key
        {
            get => key;
            set => key = value.Trim().ToLower();
        }

        public string ParKey
        {
            get => $"p_{Key.Trim().ToLower()}";            
        }

        public string Condition
        {
            get => condition;
            set => condition = value.Trim().ToLower();
        }

        public string Value
        {
            get => @value;
            set => this.@value = value;
        }
    }
}
