namespace CalculatedFields.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;

    using Data;

    using TrailsPlugin.Data;

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

            /*if (Trails.TestIntegration())
            {
                labelTrailsIntegration.Text = "Integration with Trails plugin is enabled";
                labelTrailsIntegration.ForeColor = Color.Black;
            }*/
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

        #endregion

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
    }
}