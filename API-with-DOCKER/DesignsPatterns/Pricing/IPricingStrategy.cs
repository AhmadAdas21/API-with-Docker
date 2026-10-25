namespace API_with_DOCKER.DesignsPatterns.Pricing
{
    public interface IPricingStrategy
    {
        public void pay(decimal amount);
    }
}
