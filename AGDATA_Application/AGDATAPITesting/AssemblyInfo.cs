using System.Reflection;
using log4net.Config;

// This will configure log4net to read its settings from the App.config file.
[assembly: XmlConfigurator(ConfigFile = "App.config", Watch = true)]