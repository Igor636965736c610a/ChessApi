using ChesApi.Infrastructure.MoveTypeStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChesApi.Infrastructure.Repo
{
    public class StrategyFactory<T> : IStrategyFactory<T>
    {
        private readonly IEnumerable<T> candidates;

        public StrategyFactory(IEnumerable<T> candidates)
            => this.candidates = candidates;

        public T GetStrategy(string strategyName)
            => candidates.First(c => c.GetType().Name.StartsWith(strategyName));

    }
}
