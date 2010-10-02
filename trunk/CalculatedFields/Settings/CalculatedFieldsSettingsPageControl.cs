namespace CalculatedFields.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using Data;

    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Data.Fitness.CustomData;
    using ZoneFiveSoftware.Common.Visuals;

    public partial class CalculatedFieldsSettingsPageControl : UserControl
    {
        #region Constructors and Destructors

        public CalculatedFieldsSettingsPageControl()
        {
            this.InitializeComponent();

            buttonAdd.Image = CommonResources.Images.DocumentAdd16;
            buttonAddNested.Image = CommonResources.Images.DocumentAdd16;
            buttonRemove.Image = CommonResources.Images.Delete16;
            buttonRemoveNested.Image = CommonResources.Images.Delete16;
            buttonUpdate.Image = CommonResources.Images.Edit16;
            buttonUpdateNested.Image = CommonResources.Images.Edit16;

            buttonTestSelected.Image = CommonResources.Images.Analyze16;
            buttonCalculateSelected.Image = CommonResources.Images.Calculator16;
            buttonClearSelected.Image = CommonResources.Images.Delete16;

            ICustomDataFieldObjectType type = CustomDataFieldDefinitions.StandardObjectType(typeof(IActivity));

            foreach (ICustomDataFieldDefinition definition in CalculatedFields.GetLogBook().CustomDataFieldDefinitions)
            {
                if (definition.ObjectType == type)
                {
                    if (!comboBoxCustomField.Items.Contains(definition.Name))
                    {
                        comboBoxCustomField.Items.Add(definition.Name);
                    }
                }
            }

            GlobalSettings.LoadSettings();
            GlobalSettings.calculatedFieldsRows.Sort();
            GlobalSettings.nestedFieldsRows.Sort();

            this.checkBoxAfterImport.Checked = GlobalSettings.runAfterImport;
            this.checkBoxAfterImportFuture.Checked = GlobalSettings.calculateFutureAfterImport;

            ITheme visualTheme = CalculatedFields.GetApplication().VisualTheme;

            var active = new TreeList.Column("Active", "Active", 50, StringAlignment.Center);
            var customField = new TreeList.Column("CustomField", "Calculated Custom Field", 150, StringAlignment.Near);
            var calculatedExpression = new TreeList.Column("CalculatedExpression", "Expression", 300, StringAlignment.Near);
            var condition = new TreeList.Column("Condition", "Condition", 200, StringAlignment.Near);
            //var parsingTime = new TreeList.Column("ParsingTime", "Parsing Time", 100, StringAlignment.Near);
            //var compilationTime = new TreeList.Column("CompilationTime", "Compilation Time", 100, StringAlignment.Near);

            treeListCalculatedFields.Columns.Add(customField);
            treeListCalculatedFields.Columns.Add(calculatedExpression);
            treeListCalculatedFields.Columns.Add(condition);
            treeListCalculatedFields.Columns.Add(active);
            //treeListCalculatedFields.Columns.Add(parsingTime);
            //treeListCalculatedFields.Columns.Add(compilationTime);

            treeListCalculatedFields.RowData = GlobalSettings.calculatedFieldsRows;
            treeListCalculatedFields.ThemeChanged(visualTheme);



            var nestedExpressionName = new TreeList.Column("NestedExpression", "Nested Expression Name", 150, StringAlignment.Near);
            var nestedExpression = new TreeList.Column("Expression", "Expression", 500, StringAlignment.Near);

            treeListNestedExpressions.Columns.Add(nestedExpressionName);
            treeListNestedExpressions.Columns.Add(nestedExpression);

            treeListNestedExpressions.RowData = GlobalSettings.nestedFieldsRows;
            treeListNestedExpressions.ThemeChanged(visualTheme);

            if (Trails.TestIntegration())
            {
                labelTrailsIntegration.Text = "Integration with Trails plugin is enabled";
                labelTrailsIntegration.ForeColor = Color.Black;
            }

            stripActivity.DropDownItems.Add(new ToolStripMenuItem("EQUIPMENT"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("CATEGORY"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("TEMPERATURE"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("WEATHERNOTES"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("LOCATION"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("NAME"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("DATETIME"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("DATE"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("NOTES"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("INTENSITY"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("TIME"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("DISTANCE"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("AVGPACE"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("AVGCADENCE"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("AVGGRADE"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("AVGHR"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("AVGPOWER"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("AVGSPEED"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("MAXCADENCE"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("MAXGRADE"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("MAXHR"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("MAXPOWER"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("ASCENDING"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("DESCENDING"));

            foreach (ToolStripMenuItem item in stripActivity.DropDownItems)
            {
                item.Click += fieldItem_Click;
            }

            stripActive.DropDownItems.Add(new ToolStripMenuItem("ACTIVETIME"));
            stripActive.DropDownItems.Add(new ToolStripMenuItem("ACTIVEDISTANCE"));
            stripActive.DropDownItems.Add(new ToolStripMenuItem("ACTIVEAVGPACE"));
            stripActive.DropDownItems.Add(new ToolStripMenuItem("ACTIVEAVGSPEED"));
            stripActive.DropDownItems.Add(new ToolStripMenuItem("ACTIVEAVGCADENCE"));
            stripActive.DropDownItems.Add(new ToolStripMenuItem("ACTIVEAVGHR"));
            stripActive.DropDownItems.Add(new ToolStripMenuItem("ACTIVEAVGPOWER"));
            stripActive.DropDownItems.Add(new ToolStripMenuItem("ACTIVEMAXCADENCE"));
            stripActive.DropDownItems.Add(new ToolStripMenuItem("ACTIVEMAXHR"));
            stripActive.DropDownItems.Add(new ToolStripMenuItem("ACTIVEMAXPOWER"));

            foreach (ToolStripMenuItem item in stripActive.DropDownItems)
            {
                item.Click += fieldItem_Click;
            }

            stripRest.DropDownItems.Add(new ToolStripMenuItem("RESTTIME"));
            stripRest.DropDownItems.Add(new ToolStripMenuItem("RESTDISTANCE"));
            stripRest.DropDownItems.Add(new ToolStripMenuItem("RESTAVGPACE"));
            stripRest.DropDownItems.Add(new ToolStripMenuItem("RESTAVGSPEED"));
            stripRest.DropDownItems.Add(new ToolStripMenuItem("RESTAVGCADENCE"));
            stripRest.DropDownItems.Add(new ToolStripMenuItem("RESTAVGHR"));
            stripRest.DropDownItems.Add(new ToolStripMenuItem("RESTAVGPOWER"));
            stripRest.DropDownItems.Add(new ToolStripMenuItem("RESTMAXCADENCE"));
            stripRest.DropDownItems.Add(new ToolStripMenuItem("RESTMAXHR"));
            stripRest.DropDownItems.Add(new ToolStripMenuItem("RESTMAXPOWER"));

            foreach (ToolStripMenuItem item in stripRest.DropDownItems)
            {
                item.Click += fieldItem_Click;
            }

            stripSplits.DropDownItems.Add(new ToolStripMenuItem("NOTES"));
            stripSplits.DropDownItems.Add(new ToolStripMenuItem("AVGPACE"));
            stripSplits.DropDownItems.Add(new ToolStripMenuItem("AVGSPEED"));
            stripSplits.DropDownItems.Add(new ToolStripMenuItem("DISTANCE"));
            stripSplits.DropDownItems.Add(new ToolStripMenuItem("TIME"));
            stripSplits.DropDownItems.Add(new ToolStripMenuItem("AVGCADENCE"));
            stripSplits.DropDownItems.Add(new ToolStripMenuItem("AVGHR"));
            stripSplits.DropDownItems.Add(new ToolStripMenuItem("AVGPOWER"));
            stripSplits.DropDownItems.Add(new ToolStripMenuItem("MAXCADENCE"));
            stripSplits.DropDownItems.Add(new ToolStripMenuItem("MAXHR"));
            stripSplits.DropDownItems.Add(new ToolStripMenuItem("MAXPOWER"));

            foreach (ToolStripMenuItem item in stripSplits.DropDownItems)
            {
                item.Click += fieldItem_Click;
            }

            stripTrails.DropDownItems.Add(new ToolStripMenuItem("AVGPACE"));
            stripTrails.DropDownItems.Add(new ToolStripMenuItem("DISTANCE"));
            stripTrails.DropDownItems.Add(new ToolStripMenuItem("TIME"));
            stripTrails.DropDownItems.Add(new ToolStripMenuItem("AVGCADENCE"));
            stripTrails.DropDownItems.Add(new ToolStripMenuItem("AVGGRADE"));
            stripTrails.DropDownItems.Add(new ToolStripMenuItem("AVGHR"));
            stripTrails.DropDownItems.Add(new ToolStripMenuItem("AVGPOWER"));
            stripTrails.DropDownItems.Add(new ToolStripMenuItem("AVGSPEED"));
            stripTrails.DropDownItems.Add(new ToolStripMenuItem("ELEVATIONCHANGE"));
            stripTrails.DropDownItems.Add(new ToolStripMenuItem("MAXHR"));

            foreach (ToolStripMenuItem item in stripTrails.DropDownItems)
            {
                item.Click += fieldItem_Click;
            }

            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETEBMI"));
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETEBODYFATPERCENTAGE"));
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETECALORIES"));
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETEDATE"));
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETEDIARY"));
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETEDIASTOLICBLOODPRESSURE"));
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETESYSTOLICBLOODPRESSURE"));
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETEINJURED"));
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETEINJUREDTEXT"));
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETEMAXHR"));
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETEMISSEDWORKOUT"));
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETEMISSEDWORKOUTTEXT"));
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETEMOOD"));
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETERESTHR"));
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETESICK"));
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETESICKTEXT"));
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETESKINFOLD"));
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETESLEEPHOURS"));
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETESLEEPQUALITY"));
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETEWEIGHT"));

            foreach (ToolStripMenuItem item in stripAthlete.DropDownItems)
            {
                item.Click += fieldItem_Click;
            }

            stripAggregate.DropDownItems.Add(new ToolStripMenuItem("Sum"));
            stripAggregate.DropDownItems.Add(new ToolStripMenuItem("Avg"));
            stripAggregate.DropDownItems.Add(new ToolStripMenuItem("Min"));
            stripAggregate.DropDownItems.Add(new ToolStripMenuItem("Max"));

            foreach (ToolStripMenuItem item in stripAggregate.DropDownItems)
            {
                item.Click += fieldItem_Click;
            }

            stripFormulas.DropDownItems.Add(new ToolStripMenuItem("Avg. HR/min"));
            stripFormulas.DropDownItems.Add(new ToolStripMenuItem("Sum of last 7 days distance"));
            stripFormulas.DropDownItems.Add(new ToolStripMenuItem("Sum of last 21 days distance"));
            stripFormulas.DropDownItems.Add(new ToolStripMenuItem("Sum of last 7 days distance only for category Running"));
            stripFormulas.DropDownItems.Add(new ToolStripMenuItem("Distance to Km"));
            stripFormulas.DropDownItems.Add(new ToolStripMenuItem("Distance to Miles"));
            stripFormulas.DropDownItems.Add(new ToolStripMenuItem("Speed to Speed in Miles"));
            stripFormulas.DropDownItems.Add(new ToolStripMenuItem("Adjust Distance by 3%"));
            stripFormulas.DropDownItems.Add(new ToolStripMenuItem("Condition for Category is Race OR Category is Trail"));
            stripFormulas.DropDownItems.Add(new ToolStripMenuItem("Condition for Category is Race AND Distance is greater than 7000m"));
            stripFormulas.DropDownItems.Add(new ToolStripMenuItem("Avg pace for Trail with name Pernek"));

            foreach (ToolStripMenuItem item in stripFormulas.DropDownItems)
            {
                item.Click += fieldItem_Click;
            }
        }

        #endregion

        #region Public Methods

        public void ThemeChanged(ITheme visualTheme)
        {
            this.treeListCalculatedFields.ThemeChanged(visualTheme);
            this.treeListNestedExpressions.ThemeChanged(visualTheme);
        }

        #endregion

        #region Methods

        private void checkBoxAfterImport_CheckedChanged(object sender, EventArgs e)
        {
            GlobalSettings.runAfterImport = this.checkBoxAfterImport.Checked;

            GlobalSettings.SaveSettings();
        }

        private void checkBoxAfterImportFuture_CheckedChanged(object sender, EventArgs e)
        {
            GlobalSettings.calculateFutureAfterImport = this.checkBoxAfterImportFuture.Checked;

            GlobalSettings.SaveSettings();
        }

        #endregion

        private void fieldItem_Click(object sender, EventArgs e)
        {
            string result = "";
            ToolStripMenuItem item = sender as ToolStripMenuItem;


            if (item.OwnerItem == stripActivity || item.OwnerItem == stripAthlete || item.OwnerItem == stripCustom || item.OwnerItem == stripNested || item.OwnerItem == stripActive || item.OwnerItem == stripRest)
            {
                result = "{" + item.Text + "}";
            }

            if (item.OwnerItem == stripSplits)
            {
                result = "{SPLIT" + item.Text + "(1)}";
            }

            if (item.OwnerItem == stripTrails)
            {
                result = "{TRAIL" + item.Text + "(,1)}";
            }

            if (item.OwnerItem == stripAggregate)
            {
                result = "{Field(" + item.Text + ",7)}";
            }

            if (item.OwnerItem == stripFormulas && textBoxExpression.Focused)
            {
                switch (item.Text)
                {
                    case "Avg. HR/min":
                        result = "{AVGPACE}/60 * {AVGHR}";
                        textBoxCondition.Text = "{DISTANCE} != 0";
                        break;
                    case "Sum of last 7 days distance":
                        result = "{DISTANCE(SUM,7)}";
                        break;
                    case "Sum of last 21 days distance":
                        result = "{DISTANCE(SUM,21)}";
                        break;
                    case "Sum of last 7 days distance only for category Running":
                        result = "{DISTANCE(SUM,7)}";
                        textBoxCondition.Text = "{CATEGORY}.Contains(\"Running\")";
                        break;
                    case "Distance to Km":
                        result = "{DISTANCE}/1000";
                        break;
                    case "Distance to Miles":
                        result = "{DISTANCE}/1609.344";
                        break;
                    case "Speed to Speed in Miles":
                        result = "{AVGSPEED}/1.609344";
                        break;
                    case "Adjust Distance by 3%":
                        result = "{DISTANCE}*1.03";
                        break;
                    case "Condition for Category is Race OR Category is Trail":
                        textBoxCondition.Text = "{CATEGORY}.Contains(\"Race\") || {CATEGORY}.Contains(\"Trail\")";
                        break;
                    case "Condition for Category is Race AND Distance is greater than 7000m":
                        textBoxCondition.Text = "{CATEGORY}.Contains(\"Race\") && {DISTANCE} > 7000";
                        break;
                    case "Avg pace for Trail with name Pernek":
                        result = "{TRAILAVGPACE(Pernek,1)}";
                        break;
                }
            }

            if (textBoxExpression.Focused)
            {
                textBoxExpression.SelectedText = result;
            }
            if (textBoxCondition.Focused)
            {
                textBoxCondition.SelectedText = result;
            }
            if (textBoxNestedExpression.Focused)
            {
                textBoxNestedExpression.SelectedText = result;
            }
        }

        private void comboBoxCustomField_Click(object sender, EventArgs e)
        {
            ICustomDataFieldObjectType type = CustomDataFieldDefinitions.StandardObjectType(typeof(IActivity));

            foreach (ICustomDataFieldDefinition definition in CalculatedFields.GetLogBook().CustomDataFieldDefinitions)
            {
                if (definition.ObjectType == type)
                {
                    if (!comboBoxCustomField.Items.Contains(definition.Name))
                    {
                        comboBoxCustomField.Items.Add(definition.Name);
                    }
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            GlobalSettings.calculatedFieldsRows.Add(new CalculatedFieldsRow(Guid.NewGuid().ToString(), this.comboBoxCustomField.SelectedItem.ToString(), textBoxExpression.Text, textBoxCondition.Text, checkBoxActive.Checked.ToString()));
            GlobalSettings.calculatedFieldsRows.Sort();
            treeListCalculatedFields.RowData = GlobalSettings.calculatedFieldsRows;

            GlobalSettings.SaveSettings();
        }

        private void buttonAddNested_Click(object sender, EventArgs e)
        {
            GlobalSettings.nestedFieldsRows.Add(new NestedFieldsRow(Guid.NewGuid().ToString(), this.textBoxNestedExpressionName.Text, textBoxNestedExpression.Text));
            GlobalSettings.nestedFieldsRows.Sort();
            treeListNestedExpressions.RowData = GlobalSettings.nestedFieldsRows;

            GlobalSettings.SaveSettings();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (treeListCalculatedFields.SelectedItems.Count == 1)
            {
                foreach (CalculatedFieldsRow row in treeListCalculatedFields.SelectedItems)
                {
                    var updateRow = GlobalSettings.calculatedFieldsRows.Find((r) => r.ID == row.ID);
                    updateRow.CustomField = this.comboBoxCustomField.SelectedItem.ToString();
                    updateRow.CalculatedExpression = textBoxExpression.Text;
                    updateRow.Condition = textBoxCondition.Text;
                    updateRow.Active = (checkBoxActive.Checked) ? "Y" : "N";

                    GlobalSettings.calculatedFieldsRows.Sort();
                    treeListCalculatedFields.RowData = GlobalSettings.calculatedFieldsRows;
                }
            }
            else
            {
                foreach (CalculatedFieldsRow row in treeListCalculatedFields.SelectedItems)
                {
                    GlobalSettings.calculatedFieldsRows.Find((r) => r.ID == row.ID).Active = (checkBoxActive.Checked) ? "Y" : "N";
                    GlobalSettings.calculatedFieldsRows.Sort();
                    treeListCalculatedFields.RowData = GlobalSettings.calculatedFieldsRows;
                }
            }

            GlobalSettings.SaveSettings();
        }

        private void buttonUpdateNested_Click(object sender, EventArgs e)
        {
            foreach (NestedFieldsRow row in treeListNestedExpressions.SelectedItems)
            {
                var updateRow = GlobalSettings.nestedFieldsRows.Find((r) => r.ID == row.ID);
                updateRow.NestedExpression = this.textBoxNestedExpressionName.Text;
                updateRow.Expression = textBoxNestedExpression.Text;

                GlobalSettings.nestedFieldsRows.Sort();
                treeListNestedExpressions.RowData = GlobalSettings.nestedFieldsRows;
            }

            GlobalSettings.SaveSettings();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            foreach (CalculatedFieldsRow row in treeListCalculatedFields.SelectedItems)
            {
                GlobalSettings.calculatedFieldsRows.Remove(row);
                treeListCalculatedFields.RowData = GlobalSettings.calculatedFieldsRows;
            }

            GlobalSettings.SaveSettings();
        }

        private void buttonRemoveNested_Click(object sender, EventArgs e)
        {
            foreach (NestedFieldsRow row in treeListNestedExpressions.SelectedItems)
            {
                GlobalSettings.nestedFieldsRows.Remove(row);
                treeListNestedExpressions.RowData = GlobalSettings.nestedFieldsRows;
            }

            GlobalSettings.SaveSettings();
        }

        private void textBoxExpression_TextChanged(object sender, EventArgs e)
        {
            string customField = "";

            if (this.comboBoxCustomField.SelectedItem != null)
            {
                customField = this.comboBoxCustomField.SelectedItem.ToString();
            }

            if (this.textBoxExpression.Text == "" || customField == "")
            {
                this.buttonAdd.Enabled = false;
            }
            else
            {
                this.buttonAdd.Enabled = true;
            }
        }

        private void comboBoxCustomField_SelectedValueChanged(object sender, EventArgs e)
        {
            string customField = "";

            if (this.comboBoxCustomField.SelectedItem != null)
            {
                customField = this.comboBoxCustomField.SelectedItem.ToString();
            }

            if (this.textBoxExpression.Text == "" || customField == "")
            {
                this.buttonAdd.Enabled = false;
            }
            else
            {
                this.buttonAdd.Enabled = true;
            }
        }

        private void textBoxNestedExpression_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxNestedExpression.Text == "" || textBoxNestedExpressionName.Text == "")
            {
                this.buttonAddNested.Enabled = false;
            }
            else
            {
                this.buttonAddNested.Enabled = true;
            }
        }

        private void textBoxNestedExpressionName_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxNestedExpression.Text == "" || textBoxNestedExpressionName.Text == "")
            {
                this.buttonAddNested.Enabled = false;
            }
            else
            {
                this.buttonAddNested.Enabled = true;
            }

        }

        private void treeListCalculatedFields_SelectedItemsChanged(object sender, EventArgs e)
        {
            if (treeListCalculatedFields.SelectedItems.Count == 0)
            {
                buttonRemove.Enabled = false;
                buttonUpdate.Enabled = false;
            }
            else
            {
                buttonRemove.Enabled = true;
                buttonUpdate.Enabled = true;
            }

            foreach (CalculatedFieldsRow row in treeListCalculatedFields.SelectedItems)
            {
                foreach (var item in comboBoxCustomField.Items)
                {
                    if (item.ToString() == row.CustomField)
                    {
                        this.comboBoxCustomField.SelectedItem = item.ToString();
                    }
                }

                this.textBoxExpression.Text = row.CalculatedExpression;
                this.textBoxCondition.Text = row.Condition;

                if (row.Active == "Y")
                {
                    this.checkBoxActive.Checked = true;
                }
                else
                {
                    this.checkBoxActive.Checked = false;
                }
            }
        }

        private void treeListNestedExpressions_SelectedItemsChanged(object sender, EventArgs e)
        {
            if (treeListNestedExpressions.SelectedItems.Count == 0)
            {
                buttonRemoveNested.Enabled = false;
                buttonUpdateNested.Enabled = false;
            }
            else
            {
                buttonRemoveNested.Enabled = true;
                buttonUpdateNested.Enabled = true;
            }

            foreach (NestedFieldsRow row in treeListNestedExpressions.SelectedItems)
            {
                this.textBoxNestedExpressionName.Text = row.NestedExpression;
                this.textBoxNestedExpression.Text = row.Expression;
            }
        }

        private void buttonCalculateSelected_Click(object sender, EventArgs e)
        {
            if (treeListCalculatedFields.SelectedItems.Count != 0)
            {
                GlobalSettings.LoadSettings();

                List<CalculatedFieldsRow> selected = new List<CalculatedFieldsRow>();
                foreach (var row in treeListCalculatedFields.SelectedItems)
                {
                    selected.Add((CalculatedFieldsRow)row);
                }

                Evaluator.Calculate((IList<IActivity>)CalculatedFields.GetLogBook().Activities, selected, false);
            }
        }

        private void buttonTestSelected_Click(object sender, EventArgs e)
        {
            if (treeListCalculatedFields.SelectedItems.Count != 0)
            {
                GlobalSettings.LoadSettings();

                List<CalculatedFieldsRow> selected = new List<CalculatedFieldsRow>();
                foreach (var row in treeListCalculatedFields.SelectedItems)
                {
                    selected.Add((CalculatedFieldsRow)row);
                }

                Evaluator.Calculate((IList<IActivity>)CalculatedFields.GetLogBook().Activities, selected, true);
            }
        }

        private void buttonClearSelected_Click(object sender, EventArgs e)
        {
            if (treeListCalculatedFields.SelectedItems.Count != 0)
            {
                GlobalSettings.LoadSettings();

                List<CalculatedFieldsRow> selected = new List<CalculatedFieldsRow>();
                foreach (var row in treeListCalculatedFields.SelectedItems)
                {
                    selected.Add((CalculatedFieldsRow)row);
                }

                Evaluator.ClearCalculations((IList<IActivity>)CalculatedFields.GetLogBook().Activities, selected);
            }
        }

        private void contextMenuStripFields_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            stripCustom.DropDownItems.Clear();
            stripNested.DropDownItems.Clear();

            ICustomDataFieldObjectType type = CustomDataFieldDefinitions.StandardObjectType(typeof(IActivity));

            foreach (ICustomDataFieldDefinition definition in CalculatedFields.GetLogBook().CustomDataFieldDefinitions)
            {
                if (definition.ObjectType == type)
                {
                    stripCustom.DropDownItems.Add(new ToolStripMenuItem(definition.Name));
                }
            }

            foreach (var row in GlobalSettings.nestedFieldsRows)
            {
                stripNested.DropDownItems.Add(new ToolStripMenuItem(row.NestedExpression));
            }

            foreach (ToolStripMenuItem item in stripCustom.DropDownItems)
            {
                item.Click += fieldItem_Click;
            }

            foreach (ToolStripMenuItem item in stripNested.DropDownItems)
            {
                item.Click += fieldItem_Click;
            }
        }
    }
}