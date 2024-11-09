using System.IO;
using Xunit;
using static PoViEmu.Tests.CompatCheck;

namespace PoViEmu.Tests
{
    public class CompatTest
    {
        [Theory]
        [InlineData("Check1")]
        [InlineData("Check2")]
        [InlineData("Check3")]
        [InlineData("Check4")]
        [InlineData("Op_adc")]
        [InlineData("Op_add")]
        [InlineData("Op_and")]
        [InlineData("Op_call")]
        [InlineData("Op_cmp")]
        [InlineData("Op_dec")]
        [InlineData("Op_div")]
        [InlineData("Op_idiv")]
        [InlineData("Op_imul")]
        [InlineData("Op_inc")]
        [InlineData("Op_in")]
        [InlineData("Op_lahf")]
        [InlineData("Op_lea")]
        [InlineData("Op_loop")]
        [InlineData("Op_mul")]
        [InlineData("Op_neg")]
        [InlineData("Op_not")]
        [InlineData("Op_or")]
        [InlineData("Op_popf")]
        [InlineData("Op_pop")]
        [InlineData("Op_pusha")]
        [InlineData("Op_pushf")]
        [InlineData("Op_rcl")]
        [InlineData("Op_rcr")]
        [InlineData("Op_rol")]
        [InlineData("Op_ror")]
        [InlineData("Op_sal")]
        [InlineData("Op_sar")]
        [InlineData("Op_sbb1")]
        [InlineData("Op_sbb2")]
        [InlineData("Op_shl")]
        [InlineData("Op_shr")]
        [InlineData("Op_sti")]
        [InlineData("Op_stosb")]
        [InlineData("Op_stosw")]
        [InlineData("Op_sub")]
        [InlineData("Op_test")]
        [InlineData("Op_xchg")]
        [InlineData("Op_xlat")]
        [InlineData("Op_xor")]
        public void ShouldRead(string fileName)
        {
            var dir = Path.Combine("Resources", "Compat");
            DoShouldRead(dir, fileName);
        }
    }
}