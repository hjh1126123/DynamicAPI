namespace Server.Strategy
{
    public class StrategyModel
    {        
        public string Name { get; set; }
        public string Describe { get; set; }
        public IStrategy Strategy { get; set; }
    }
}
