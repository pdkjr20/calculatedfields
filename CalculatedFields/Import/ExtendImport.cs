namespace CalculatedFields.Import
{
    using System.Collections;
    using System.Collections.Generic;

    using Data;

    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Visuals.Fitness;

    internal class ExtendImport : IExtendDataImporters
    {
        #region Properties

        public IList<IFileImporter> FileImporters
        {
            get
            {
                return null;
            }
        }

        #endregion

        #region Implemented Interfaces

        #region IExtendDataImporters

        public void AfterImport(IList added, IList updated)
        {
            if (GlobalSettings.runAfterImport)
            {
                var activities = new List<IActivity>();

                foreach (object record in added)
                {
                    if (record is IActivity)
                    {
                        activities.Add(record as IActivity);
                    }
                }

                foreach (object record in updated)
                {
                    if (record is IActivity)
                    {
                        activities.Add(record as IActivity);
                    }
                }

                GlobalSettings.LoadSettings();
                Evaluator.Calculate(activities, null, false);
            }
        }

        public void BeforeImport(IList items)
        {
        }

        #endregion

        #endregion
    }
}