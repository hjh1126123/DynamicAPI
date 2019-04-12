using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Model
{
    public class MHome
    {
        public string Version { get; }

        public string VersionFeature { get; }

        public string UpdateVersion { get; }

        public MHome(string version,string versionFeature,string updateVersion)
        {
            Version = version;
            VersionFeature = versionFeature;

            UpdateVersion = updateVersion;
        }
    }
}
