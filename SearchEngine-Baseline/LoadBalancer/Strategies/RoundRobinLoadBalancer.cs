using LoadBalancer.Interfaces;

namespace LoadBalancer.Strategies
{
    public class RoundRobinLoadBalancer : ILoadBalancer
    {

        private List<string> services = new List<string>();
        private ILoadBalancerStrategy activeStrategy;

        public List<string> GetAllServices()
        {
            return services;
        }

        public int AddService(string url)
        {
            services.Add(url);
            return services.Count - 1;
        }

        public int RemoveService(int id)
        {
            services.RemoveAt(id);
            return services.Count;
        }

        public ILoadBalancerStrategy GetActiveStrategy()
        {
            return activeStrategy;
        }

        public void SetActiveStrategy(ILoadBalancerStrategy strategy)
        {
            activeStrategy = strategy;
        }

        public string NextService()
        {
            if (services.Count == 0)
            {
                throw new InvalidOperationException("No services available.");
            }

            if (activeStrategy == null)
            {
                throw new InvalidOperationException("No load balancing strategy set.");
            }

            return activeStrategy.NextService(services);
        }
    }
}
