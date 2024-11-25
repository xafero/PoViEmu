using System.IO;
using Xunit;
using static PoViEmu.Tests.CompatCheck;

namespace PoViEmu.Tests
{
    public class CompatTest
    {
        [Theory]
        /* fail */
        // [InlineData("C_hex")]
        // [InlineData("C_while")]
        // [InlineData("Check2")]
        // [InlineData("Check4")]
        // [InlineData("Op_call")]
        // [InlineData("Op_cmc")]
        // [InlineData("Op_das")]
        // [InlineData("Op_in")]
        // [InlineData("Op_ja")]
        // [InlineData("Op_jbe")]
        // [InlineData("Op_je")]
        // [InlineData("Op_jle")]
        // [InlineData("Op_jna")]
        // [InlineData("Op_jnbe")]
        // [InlineData("Op_jne")]
        // [InlineData("Op_jng")]
        // [InlineData("Op_jnle")]
        // [InlineData("Op_jnp")]
        // [InlineData("Op_jp")]
        // [InlineData("Op_jpe")]
        // [InlineData("Op_jpo")]
        // [InlineData("Op_loope")]
        // [InlineData("Op_loopz")]
        // [InlineData("Op_lopne")]
        // [InlineData("Op_lopnz")]
        // [InlineData("Op_movsb")]
        // [InlineData("Op_movsw")]
        // [InlineData("Op_popf")]
        // [InlineData("Op_pusha")]
        // [InlineData("Op_pushf")]
        // [InlineData("Op_repne")]
        // [InlineData("Op_sbb2")]
        // [InlineData("Op_scasb")]
        // [InlineData("Op_scasw")]
        // [InlineData("Op_stc")]
        // [InlineData("Op_std")]
        
        /* good */
        [InlineData("C_add1")]
        [InlineData("C_add2")]
        [InlineData("C_bitand")]
        [InlineData("C_bitor")]
        [InlineData("C_bitxor")]
        [InlineData("C_cond")]
        [InlineData("C_dec")]
        [InlineData("C_div")]
        [InlineData("C_dowhil")]
        [InlineData("C_equal")]
        [InlineData("C_for")]
        [InlineData("C_greate")]
        [InlineData("C_greatt")]
        [InlineData("C_ifelse")]
        [InlineData("C_inc")]
        [InlineData("C_leftsh")]
        [InlineData("C_less")]
        [InlineData("C_lessth")]
        [InlineData("C_logand")]
        [InlineData("C_lognot")]
        [InlineData("C_logor")]
        [InlineData("C_mod")]
        [InlineData("C_mul")]
        [InlineData("C_notequ")]
        [InlineData("C_rights")]
        [InlineData("C_sizeof")]
        [InlineData("C_sub")]
        [InlineData("C_switch")]
        [InlineData("Check1")]
        [InlineData("Check3")]
        [InlineData("Op_aaa")]
        [InlineData("Op_aad")]
        [InlineData("Op_aam")]
        [InlineData("Op_aas")]
        [InlineData("Op_adc")]
        [InlineData("Op_add")]
        [InlineData("Op_and")]
        [InlineData("Op_cbw")]
        [InlineData("Op_cmp")]
        [InlineData("Op_daa")]
        [InlineData("Op_dec")]
        [InlineData("Op_div")]
        [InlineData("Op_enter")]
        [InlineData("Op_idiv")]
        [InlineData("Op_imul")]
        [InlineData("Op_inc")]
        [InlineData("Op_into")]
        [InlineData("Op_jae")]
        [InlineData("Op_jb")]
        [InlineData("Op_jc")]
        [InlineData("Op_jcxz")]
        [InlineData("Op_jge")]
        [InlineData("Op_jl")]
        [InlineData("Op_jnae")]
        [InlineData("Op_jnb")]
        [InlineData("Op_jnc")]
        [InlineData("Op_jnge")]
        [InlineData("Op_jnl")]
        [InlineData("Op_jno")]
        [InlineData("Op_jns")]
        [InlineData("Op_jo")]
        [InlineData("Op_js")]
        [InlineData("Op_lahf")]
        [InlineData("Op_lea")]
        [InlineData("Op_loop")]
        [InlineData("Op_mul")]
        [InlineData("Op_neg")]
        [InlineData("Op_not")]
        [InlineData("Op_or")]
        [InlineData("Op_outsb")]
        [InlineData("Op_outsw")]
        [InlineData("Op_pop")]
        [InlineData("Op_rcl")]
        [InlineData("Op_rcr")]
        [InlineData("Op_rep")]
        [InlineData("Op_repe")]
        [InlineData("Op_rol")]
        [InlineData("Op_ror")]
        [InlineData("Op_sal")]
        [InlineData("Op_sar")]
        [InlineData("Op_sbb1")]
        [InlineData("Op_shl")]
        [InlineData("Op_shr")]
        [InlineData("Op_sti")]
        [InlineData("Op_stosb")]
        [InlineData("Op_stosw")]
        [InlineData("Op_sub")]
        [InlineData("Op_test")]
        [InlineData("Op_wait")]
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