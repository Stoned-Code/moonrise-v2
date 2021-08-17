using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRC;

namespace MoonriseV2Mod.API
{
    internal class MoonriseMultiCast
    {
        internal delegate void SimpleMultiCast();
        internal delegate void OnPlayerMultiCast(Player player, bool isFriend, bool allJN);
        internal delegate void BoolMultiCast(bool state);
    }
}
