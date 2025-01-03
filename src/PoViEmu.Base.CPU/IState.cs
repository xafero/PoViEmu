using System.ComponentModel;

namespace PoViEmu.Base.CPU
{
    public interface IState : INotifyPropertyChanged, INotifyPropertyChanging
    {
        object this[string name] { get; }
    }
}