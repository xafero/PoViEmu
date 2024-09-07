using System;
using System.IO;
using PoViEmu.Core.Decoding;

namespace PoViEmu.Tests
{
    public static class CodeCheck
    {
        public static void DoShouldRead(string dir, string fileName)
        {
            var file = Path.Combine(dir, $"{fileName}.tml");
            var state = IniStateTool.ReadFile(file);






            foreach (var (seg,i) in state.ToInstructions())
            {
                throw new InvalidOperationException(seg + " / " + i);
            }
            

            
            

            

            



            var debug = state.ToCodeString(); 
            
            // JsonHelper.ToJson(state, false);

            throw new InvalidOperationException(debug);
        }
    }
}