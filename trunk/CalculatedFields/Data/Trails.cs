namespace CalculatedFields.Data
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Reflection;
    using System.Linq;

    using ZoneFiveSoftware.Common.Data;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Data.GPS;
    using ZoneFiveSoftware.Common.Data.Measurement;

    using TrailsPlugin.Integration;

    public static class Trails
    {
        #region Public Methods

        public static Dictionary<string, List<ITrailResult>> GetTrailsResultsForActivity(IActivity activity)
        {
            object[] parameters = new object[1];
            parameters[0] = activity;

            Dictionary<string, List<ITrailResult>> list = null;

            try
            {
                Version version;
                Type type = GetType("TrailsPlugin.Data.Integration", "TrailsPlugin", out version);
                if ((type != null)) //&& (version.CompareTo(this.minVersion) >= 0))
                {
                    list = (Dictionary<string, List<ITrailResult>>)type.GetMethod("GetTrailsResultsForActivity").Invoke(null, parameters);
                    //throw new Exception(list.Count.ToString() + "fero");
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                if (e.InnerException != null)
                {
                    error += "\n\n" + e.InnerException.Message;
                }
                throw new Exception(error);
            }

            return list;
        }

        public static bool TestIntegration()
        {
            try
            {
                Version version;
                Type type = GetType("TrailsPlugin.Data.Integration", "TrailsPlugin", out version);
                if ((type != null)) //&& (version.CompareTo(this.minVersion) >= 0))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        public static Type GetType(string clrType, string PluginName, out Version version)
        {
            version = null;
            try
            {
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    var name = new AssemblyName(assembly.FullName);
                    if (name.Name.Equals(PluginName) || name.Name.Equals(PluginName + ".dll"))
                    {
                        version = name.Version;
                        return assembly.GetType(clrType);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return null;
        }

        #endregion
    }
}