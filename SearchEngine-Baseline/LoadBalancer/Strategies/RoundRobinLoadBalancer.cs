using LoadBalancer.Interfaces;

namespace LoadBalancer.Strategies
{
    public class RoundRobinLoadBalancer : ILoadBalancer
    {

        private readonly List<string> _services = new List<string>();
        private int _currentIndex = 0;
        private readonly object _lock;

        public List<string> GetAllServices()
        {
            return _services;
        }

        public int AddService(string url)
        {
            _services.Add(url);
            return _services.Count - 1;
        }

        public int RemoveService(int id)
        {
            if (id >= 0 && id < _services.Count)
            {
                _services.RemoveAt(id);
                return id;
            }

            return -1;
        }

        public ILoadBalancerStrategy GetActiveStrategy()
        {
            return new RoundRobinStrategy();
        }

        public void SetActiveStrategy(ILoadBalancerStrategy strategy)
        {
            // Does nothing for now
        }

        public string NextService()
        {
            if (_services.Count == 0)
                throw new Exception("No services available.");

            lock (_lock)
            {
                _currentIndex++;
                if (_currentIndex >= _services.Count)
                    _currentIndex = 0;

                var nextService = _services[_currentIndex % _services.Count];
                return nextService;
            }
        }
    }
}
