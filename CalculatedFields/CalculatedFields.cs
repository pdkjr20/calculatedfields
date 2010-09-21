namespace CalculatedFields
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Xml;

    using Resources;

    using ZoneFiveSoftware.Common.Data;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Visuals.Fitness;

    internal class CalculatedFields : IPlugin
    {
        #region Constants and Fields

        private static IApplication m_App;

        private static ILogbook m_CurrentLogbook;

        #endregion

        #region Delegates

        public delegate void ActivityCategoryChangedEventHandler(object sender, IActivityCategory category);

        public delegate void LogbookChangedEventHandler(object sender, ILogbook oldLogbook, ILogbook newLogbook);

        public delegate void ZoneCategoryChangedEventHandler(object sender, IZoneCategory category);

        #endregion

        #region Events

        public static event ActivityCategoryChangedEventHandler ActivityCategoryListChanged;

        public static event LogbookChangedEventHandler LogbookChanged;

        public static event ZoneCategoryChangedEventHandler ZoneCategoryListChanged;

        #endregion

        #region Properties

        public IApplication Application
        {
            set
            {
                if (m_App != null)
                {
                    m_App.PropertyChanged -= this.AppPropertyChanged;
                }

                m_App = value;

                if (m_App != null)
                {
                    m_App.PropertyChanged += this.AppPropertyChanged;
                }
            }
        }

        public Guid Id
        {
            get
            {
                return GUIDs.CalculatedFields;
            }
        }

        public string Name
        {
            get
            {
                return StringResources.CalculatedFieldsPluginText;
            }
        }

        public string Version
        {
            get
            {
                return this.GetType().Assembly.GetName().Version.ToString(4);
            }
        }

        #endregion

        #region Public Methods

        public static IApplication GetApplication()
        {
            return m_App;
        }

        public static ILogbook GetLogBook()
        {
            return m_CurrentLogbook;
        }

        #endregion

        #region Implemented Interfaces

        #region IPlugin

        public void ReadOptions(XmlDocument xmlDoc, XmlNamespaceManager nsmgr, XmlElement pluginNode)
        {
        }

        public void WriteOptions(XmlDocument xmlDoc, XmlElement pluginNode)
        {
        }

        #endregion

        #endregion

        #region Methods

        private void AppPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e != null && e.PropertyName == "Logbook")
            {
                if (m_CurrentLogbook != null)
                {
                    this.UnregisterCategoryCallback(m_CurrentLogbook);

                    m_CurrentLogbook.CadenceZones.CollectionChanged -= this.OnZoneCategoriesCollectionChanged;
                    m_CurrentLogbook.ClimbZones.CollectionChanged -= this.OnZoneCategoriesCollectionChanged;
                    m_CurrentLogbook.HeartRateZones.CollectionChanged -= this.OnZoneCategoriesCollectionChanged;
                    m_CurrentLogbook.PowerZones.CollectionChanged -= this.OnZoneCategoriesCollectionChanged;
                    m_CurrentLogbook.SpeedZones.CollectionChanged -= this.OnZoneCategoriesCollectionChanged;
                }

                if (LogbookChanged != null)
                {
                    LogbookChanged(this, m_CurrentLogbook, m_App.Logbook);
                }

                m_CurrentLogbook = m_App.Logbook;

                if (m_CurrentLogbook != null)
                {
                    this.RegisterCategoryCallback(m_CurrentLogbook);

                    m_CurrentLogbook.CadenceZones.CollectionChanged += this.OnZoneCategoriesCollectionChanged;
                    m_CurrentLogbook.ClimbZones.CollectionChanged += this.OnZoneCategoriesCollectionChanged;
                    m_CurrentLogbook.HeartRateZones.CollectionChanged += this.OnZoneCategoriesCollectionChanged;
                    m_CurrentLogbook.PowerZones.CollectionChanged += this.OnZoneCategoriesCollectionChanged;
                    m_CurrentLogbook.SpeedZones.CollectionChanged += this.OnZoneCategoriesCollectionChanged;
                }
            }
        }

        private void OnActivityCategoriesCollectionChanged(
            object sender, NotifyCollectionChangedEventArgs<IActivityCategory> e)
        {
            var referenceList = new List<IActivityCategory>();

            if (e.Action != NotifyCollectionChangedAction.Move)
            {
                referenceList.AddRange(e.NewItems);
                referenceList.AddRange(e.OldItems);

                if (referenceList != null)
                {
                    foreach (IActivityCategory currentItem in referenceList)
                    {
                        if (ActivityCategoryListChanged != null)
                        {
                            ActivityCategoryListChanged(this, currentItem);
                        }
                    }
                }
            }

            // Refresh our callbacks so that it registers the new categories
            this.UnregisterCategoryCallback(m_CurrentLogbook);
            this.RegisterCategoryCallback(m_CurrentLogbook);
        }

        private void OnZoneCategoriesCollectionChanged(object sender, NotifyCollectionChangedEventArgs<IZoneCategory> e)
        {
            var referenceList = new List<IZoneCategory>();

            if (e.Action != NotifyCollectionChangedAction.Move)
            {
                referenceList.AddRange(e.NewItems);
                referenceList.AddRange(e.OldItems);

                if (referenceList != null)
                {
                    foreach (IZoneCategory currentItem in referenceList)
                    {
                        if (ZoneCategoryListChanged != null)
                        {
                            ZoneCategoryListChanged(this, currentItem);
                        }
                    }
                }
            }
        }

        private void RegisterCategoryCallback(ILogbook logbook)
        {
            if (logbook != null)
            {
                foreach (IActivityCategory category in logbook.ActivityCategories)
                {
                    RegisterCategoryCallback(category);
                }
            }
        }

        private void RegisterCategoryCallback(IActivityCategory category)
        {
            category.SubCategories.CollectionChanged += this.OnActivityCategoriesCollectionChanged;

            foreach (IActivityCategory subCategory in category.SubCategories)
            {
                RegisterCategoryCallback(subCategory);
            }
        }

        private void UnregisterCategoryCallback(ILogbook logbook)
        {
            if (logbook != null)
            {
                foreach (IActivityCategory category in logbook.ActivityCategories)
                {
                    UnregisterCategoryCallback(category);
                }
            }
        }

        private void UnregisterCategoryCallback(IActivityCategory category)
        {
            category.SubCategories.CollectionChanged -= this.OnActivityCategoriesCollectionChanged;

            foreach (IActivityCategory subCategory in category.SubCategories)
            {
                UnregisterCategoryCallback(subCategory);
            }
        }

        #endregion
    }
}