namespace CalculatedFields.Edit
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;

    using Data;

    using Resources;

    using ZoneFiveSoftware.Common.Data;
    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Visuals;
    using ZoneFiveSoftware.Common.Visuals.Fitness;
    using ZoneFiveSoftware.Common.Visuals.Util;

    internal class CalculateFieldsAction : IAction
    {
        #region Constants and Fields

        private readonly IDailyActivityView dailyView;

        private readonly IActivityReportsView reportView;

        private IList<IActivity> activities;

        #endregion

        #region Constructors and Destructors

        public CalculateFieldsAction(IActivityReportsView view)
        {
            this.reportView = view;

            GlobalSettings.LoadSettings();
        }

        public CalculateFieldsAction(IDailyActivityView view)
        {
            this.dailyView = view;

            GlobalSettings.LoadSettings();
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        public bool Enabled
        {
            get
            {
                return (Activities != null);
            }
        }

        public bool HasMenuArrow
        {
            get
            {
                return false;
            }
        }

        public Image Image
        {
            get
            {
                return CommonResources.Images.Calculator16;
            }
        }

        public IList<string> MenuPath
        {
            get
            {
                return new string[] { StringResources.Edit_CalculatedFieldsMenu };
            }
        }

        public string Title
        {
            get
            {
                return StringResources.Edit_CalculatedFieldsAction_Text;
            }
        }

        public bool Visible
        {
            get
            {
                if (Activities != null)
                {
                    return (Activities.Count > 0);
                }

                return false;
            }
        }

        private IList<IActivity> Activities
        {
            get
            {
                if (this.activities != null)
                {
                    return this.activities;
                }
                if (this.dailyView != null)
                {
                    return this.GetAllContainedItems<IActivity>(this.dailyView.SelectionProvider);
                }
                if (this.reportView != null)
                {
                    return this.GetAllContainedItems<IActivity>(this.reportView.SelectionProvider);
                }
                return new List<IActivity>();
            }

            set
            {
                this.activities = value;
            }
        }

        #endregion

        #region Implemented Interfaces

        #region IAction

        public void Refresh()
        {
        }

        public void Run(Rectangle rectButton)
        {
            Evaluator.Calculate(this.Activities, null, false);
        }

        #endregion

        #endregion

        #region Methods

        private void AddGroupItems<ItemType>(IList<IGroupedItem<ItemType>> groups, IList<ItemType> allItems)
        {
            foreach (var item in groups)
            {
                foreach (ItemType local in item.Items)
                {
                    if (!allItems.Contains(local))
                    {
                        allItems.Add(local);
                    }
                }
                this.AddGroupItems(item.SubGroups, allItems);
            }
        }

        private IList<ItemType> GetAllContainedItems<ItemType>(ISelectionProvider selectionProvider)
        {
            var allItems = new List<ItemType>();

            foreach (ItemType local in CollectionUtils.GetItemsOfType<ItemType>(selectionProvider.SelectedItems))
            {
                if (!allItems.Contains(local))
                {
                    allItems.Add(local);
                }
            }

            this.AddGroupItems(
                CollectionUtils.GetItemsOfType<IGroupedItem<ItemType>>(selectionProvider.SelectedItems), allItems);
            return allItems;
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}