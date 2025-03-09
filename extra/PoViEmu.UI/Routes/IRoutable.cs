namespace PoViEmu.UI.Routes
{
    public interface IRoutable
    {
    }

    public interface INavigable : IRoutable
    {
        void OnBack();
    }
}