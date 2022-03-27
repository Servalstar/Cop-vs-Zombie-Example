namespace Services
{
    public class Locator
    {
        public TService RegisterSingle<TService>(TService implementation) =>
            Implementation<TService>.ServiceInstance = implementation;

        public TService GetSingle<TService>() =>
            Implementation<TService>.ServiceInstance;

        private class Implementation<TService>
        {
            public static TService ServiceInstance;
        }
    }
}