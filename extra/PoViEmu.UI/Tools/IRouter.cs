namespace PoViEmu.UI.Tools
{
    public interface IRouter
    {
        void Push<T>(T model) where T : IRoutable;
    }
}