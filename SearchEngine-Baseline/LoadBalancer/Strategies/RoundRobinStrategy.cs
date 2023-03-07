using LoadBalancer.Interfaces;

namespace LoadBalancer.Strategies
{
    public class RoundRobinStrategy : ILoadBalancerStrategy
    {
        private int _currentIndex = 0;

        public string NextService(List<string> services)
        {
            if (services.Count == 0)
            {
                throw new InvalidOperationException("No services available");
            }

            var nextService = services[_currentIndex];
            _currentIndex = (_currentIndex + 1) % services.Count;

            return nextService;
        }
    }
}
