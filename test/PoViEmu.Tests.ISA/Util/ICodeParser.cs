namespace PoViEmu.Tests.ISA.Util
{
    internal interface ICodeParser
    {
        string Parse(byte[] bytes);
    }
}