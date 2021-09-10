using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoonriseV2Mod
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class MoonriseInfoAttribute : Attribute
    {
        public MoonriseInfoAttribute(string version)
        {
            this.m_version = version;
        }

        private string m_version;
        public string Version
        {
            get => m_version;
        }
    }
}
