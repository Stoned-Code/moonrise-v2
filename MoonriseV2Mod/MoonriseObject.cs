using MoonriseV2Mod.API;
using RubyButtonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoonriseV2Mod
{
    public class MoonriseObject
    {
        public bool isInitialized = false;

        public virtual void LoadMenu(QMNestedButton functions, QMNestedButton socialInterractions, MRUser user) { }
    }
}
