namespace PoViEmu.UI.Routes
{
    public interface IRouter
    {
        void Push<T>(T model) where T : IRoutable;

        void GoBack();

        bool CanGoBack { get; }
    }
}