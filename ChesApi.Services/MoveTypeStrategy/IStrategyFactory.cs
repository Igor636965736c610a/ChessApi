using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.MoveTypeStrategy
{
    public interface IStrategyFactory<T>
    {
        T GetStrategy(string strategyName);
    }
}
