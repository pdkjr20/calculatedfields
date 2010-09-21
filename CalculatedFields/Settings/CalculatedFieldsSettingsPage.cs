namespace CalculatedFields.Settings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Windows.Forms;

    using Resources;

    using ZoneFiveSoftware.Common.Visuals;

    internal class CalculatedFieldsSettingsPage : ISettingsPage
    {
        #region Constants and Fields

        private CalculatedFieldsSettingsPageControl control;

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        public Guid Id
        {
            get
            {
                return GUIDs.SettingsPage;
            }
        }

        public string PageName
        {
            get
            {
                return this.Title;
            }
        }

        public IPageStatus Status
        {
            get
            {
                return null;
            }
        }

        public IList<ISettingsPage> SubPages
        {
            get
            {
                return null;
            }
        }

        public string Title
        {
            get
            {
                return StringResources.CalculatedFieldsPageTitle;
            }
        }

        #endregion

        #region Implemented Interfaces

        #region IDialogPage

        public Control CreatePageControl()
        {
            if (this.control == null)
            {
                this.control = new CalculatedFieldsSettingsPageControl();
            }

            return this.control;
        }

        public bool HidePage()
        {
            return true;
        }

        public void ShowPage(string bookmark)
        {
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            if (this.control != null)
            {
                this.control.ThemeChanged(visualTheme);
            }
        }

        public void UICultureChanged(CultureInfo culture)
        {
            if (this.control != null)
            {
                //this.control.UICultureChanged(culture);
            }
        }

        #endregion

        #endregion
    }
}