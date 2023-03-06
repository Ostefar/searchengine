namespace LoadBalancer.Interfaces
{
    public interface ILoadBalancerStrategy
    {
        public string NextService(List<string> services);

    }
}
