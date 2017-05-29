using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using TeamCitySharp;
using TeamCitySharp.DomainEntities;
using File = System.IO.File;

namespace Buildscreen.Selfhost.Services
{
	public class BuildService
	{
        public IEnumerable<Build> GetBuilds()
		{
            var hostname = ConfigurationManager.AppSettings["teamcity-address"];
            var username = ConfigurationManager.AppSettings["teamcity-username"];
            var password = ConfigurationManager.AppSettings["teamcity-password"];
            var ssl = bool.Parse(ConfigurationManager.AppSettings["teamcity-usessl"]);
            var client = new TeamCityClient(hostname, ssl);
            if(!string.IsNullOrEmpty(username))
                client.Connect(username,password);
            else
                client.ConnectAsGuest();
            
            var configList = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "buildids.txt").ToList();
            foreach (var config in configList)
			{
                if(config.StartsWith("#")) continue;
                if (config.Trim() == "") continue;

                var build = client.Builds.LastBuildByBuildConfigId(config);
				build.BuildConfig = client.BuildConfigs.ByConfigurationId(config);
				yield return build;
			}
		}
	}
}