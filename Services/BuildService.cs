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
            var hostname = ConfigurationManager.AppSettings["teamcity-hostname"];
            var username = ConfigurationManager.AppSettings["teamcity-username"];
            var password = ConfigurationManager.AppSettings["teamcity-password"];
            var ssl = bool.Parse(ConfigurationManager.AppSettings["teamcity-usessl"]);
            var client = new TeamCityClient(hostname, ssl);
            if(!string.IsNullOrEmpty(username))
                client.Connect(username,password);
            else
                client.ConnectAsGuest();

		    var buildIdsSource = ConfigurationManager.AppSettings["buildids-source"];
            
            var buildIds = string.IsNullOrEmpty(buildIdsSource)
                ? client.BuildConfigs.All().Select(s => s.Id)
                : File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + buildIdsSource).ToList();

            foreach (var buildId in buildIds)
			{
                if(buildId.StartsWith("#")) continue;
                if (buildId.Trim() == "") continue;
                
			    Build build;
			    try
			    {
			        build = client.Builds.LastBuildByBuildConfigId(buildId);
                    if(build == null)
                        continue;
			        build.BuildConfig = client.BuildConfigs.ByConfigurationId(buildId);
			    }
			    catch (Exception)
			    {
                    
			        build = new Build
			        {
                        Status = "ERROR",
			            BuildConfig = new BuildConfig
			            {
			                Project = new Project { Name = "Error" },
			                Name = $"Could not load build with id {buildId}"
			            }
			        };
			    }

			    yield return build;

			}
		}
	}
}