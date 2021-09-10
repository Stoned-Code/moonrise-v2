using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoonriseV2Mod
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class MoonriseAddonAttribute : Attribute
    {
        public MoonriseAddonAttribute(string addonName, string version, string author)
        {
            this.m_version = version;
            this.m_author = author;
        }

        private string m_addonName;
        private string m_version;
        private string m_author;

        private string AddonName
        {
            get => m_addonName;
        }
        public string Version
        {
            get => m_version;
        }

        public string Author
        {
            get => m_author;
        }
    }
}
