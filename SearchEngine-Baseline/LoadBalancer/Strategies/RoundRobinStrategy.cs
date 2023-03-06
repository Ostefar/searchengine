using LoadBalancer.Interfaces;

namespace LoadBalancer.Strategies
{
    public class RoundRobinStrategy : ILoadBalancerStrategy
    {
        private int _currentIndex = 0;

        public string NextService(List<string> services)
        {
            var nextService = services[_currentIndex];

            _currentIndex++;
            if (_currentIndex == services.Count)
            {
                _currentIndex = 0;
            }

            return nextService;
        }
    }
}
