using PropertyChanged;
using System.Collections.Generic;

namespace ReportApp.ViewModel
{
    public class Home
    {
        public string Version { get; }

        public string VersionFeature { get; }

        public string UpdateVersion { get; }

        public Home(string version, string versionFeature, string updateVersion)
        {
            Version = version;
            VersionFeature = versionFeature;

            UpdateVersion = updateVersion;
        }
    }

    [ImplementPropertyChanged]
    public class HomeViewModel
    {
        public List<Home> HomeModels { get; set; }

        public HomeViewModel(List<Home> homeModels)
        {
            HomeModels = homeModels;
        }
    }
}
