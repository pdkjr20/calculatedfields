namespace CalculatedFields.View
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Resources;

    internal class PluginView
    {
        #region Constants and Fields

        private static readonly ResourceManager m_ResourceManager =
            new ResourceManager("CalculatedFields.Resources.StringResources", Assembly.GetExecutingAssembly());

        #endregion

        #region Properties

        public static ResourceManager ResourceManager
        {
            get
            {
                return m_ResourceManager;
            }
        }

        #endregion

        #region Public Methods

        public static string GetLocalizedString(string name)
        {
            try
            {
                return ResourceManager.GetString(name);
            }
            catch
            {
                Debug.Assert(false, "Unable to find string resource named " + name);

                return String.Empty;
            }
        }

        #endregion
    }
}