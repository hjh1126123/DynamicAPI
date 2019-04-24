using System.Data;

namespace Server.Strategy
{
    public interface IStrategy
    {
        DataTable Operator(DataTable dataTable);
    }
}
