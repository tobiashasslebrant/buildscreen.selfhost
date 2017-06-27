# Buildscreen.Selfhost
Selfhosted Buildscreen for showing builds from Teamcity

## buildids.txt
Used for identify which builds in Teamcity to display. Seperate build ids with linebreaks.

## Configuration:
* teamcity-hostname = http address to Teamcity
* teamcity-username = username for authenticate to Teamcity, if left empty uses guest
* teamcity-password = password for authenticate to Teamcity
* teamcity-usessl = True if teamcity uses https
* buildscreen-url = Url for displaying buildscreen
* buildids-source = filename for file containing used buildids from teamcity, if setting is left empty will take all 

## As Self:
Just run buildscreen.selfhost.exe

## As Windowsservice:
### Installing 
* buildscreen.selfhost.exe install -servicename "buildscreen.selfhost" -displayname "Buildscreen" -description "Service that show buildstatus on http. See config file for address."
* buildscreen.selfhost.exe start -servicename "buildscreen.selfhost"

### Uninstall
* buildscreen.selfhost.exe uninstall -servicename "buildscreen.selfhost"

See topshelf documentation for more switches:
http://docs.topshelf-project.com/en/latest/overview/commandline.html
