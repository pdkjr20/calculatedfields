namespace CalculatedFields.Edit
{
    using System.Collections.Generic;

    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Fitness;

    internal class ExtendActions : IExtendDailyActivityViewActions, IExtendActivityReportsViewActions
    {
        #region Implemented Interfaces

        #region IExtendActivityReportsViewActions

        public IList<IAction> GetActions(IActivityReportsView view, ExtendViewActions.Location location)
        {
            if (location == ExtendViewActions.Location.EditMenu)
            {
                return new IAction[] { new CalculateFieldsAction(view), new ClearFieldsAction(view) };
            }

            return new IAction[0];
        }

        #endregion

        #region IExtendDailyActivityViewActions

        public IList<IAction> GetActions(IDailyActivityView view, ExtendViewActions.Location location)
        {
            if (location == ExtendViewActions.Location.EditMenu)
            {
                return new IAction[] { new CalculateFieldsAction(view), new ClearFieldsAction(view) };
            }

            return new IAction[0];
        }

        #endregion

        #endregion
    }
}