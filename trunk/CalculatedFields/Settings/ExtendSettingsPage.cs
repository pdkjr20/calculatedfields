namespace CalculatedFields.Settings
{
    using System.Collections.Generic;

    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Fitness;

    internal class ExtendSettingsPage : IExtendSettingsPages
    {
        #region Properties

        public IList<ISettingsPage> SettingsPages
        {
            get
            {
                return new ISettingsPage[] { new CalculatedFieldsSettingsPage() };
            }
        }

        #endregion
    }
}