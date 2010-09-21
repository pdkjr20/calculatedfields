namespace CalculatedFields.Settings
{
    partial class CalculatedFieldsSettingsPageControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBoxAfterImport = new System.Windows.Forms.CheckBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageStandardExpressions = new System.Windows.Forms.TabPage();
            this.buttonTestSelected = new System.Windows.Forms.Button();
            this.buttonCalculateSelected = new System.Windows.Forms.Button();
            this.labelCondition = new System.Windows.Forms.Label();
            this.textBoxCondition = new System.Windows.Forms.TextBox();
            this.checkBoxActive = new System.Windows.Forms.CheckBox();
            this.treeListCalculatedFields = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.labelExpression = new System.Windows.Forms.Label();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.labelCustomField = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.textBoxExpression = new System.Windows.Forms.TextBox();
            this.comboBoxCustomField = new System.Windows.Forms.ComboBox();
            this.tabPageNestedExpressions = new System.Windows.Forms.TabPage();
            this.treeListNestedExpressions = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.buttonUpdateNested = new System.Windows.Forms.Button();
            this.textBoxNestedExpressionName = new System.Windows.Forms.TextBox();
            this.labelNestedExpression = new System.Windows.Forms.Label();
            this.buttonRemoveNested = new System.Windows.Forms.Button();
            this.labelNestedExpressionName = new System.Windows.Forms.Label();
            this.buttonAddNested = new System.Windows.Forms.Button();
            this.textBoxNestedExpression = new System.Windows.Forms.TextBox();
            this.labelTrailsIntegration = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPageStandardExpressions.SuspendLayout();
            this.tabPageNestedExpressions.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxAfterImport
            // 
            this.checkBoxAfterImport.AutoSize = true;
            this.checkBoxAfterImport.Location = new System.Drawing.Point(3, 3);
            this.checkBoxAfterImport.Name = "checkBoxAfterImport";
            this.checkBoxAfterImport.Size = new System.Drawing.Size(203, 17);
            this.checkBoxAfterImport.TabIndex = 2;
            this.checkBoxAfterImport.Text = "Run Calculated Fields plugin at import";
            this.checkBoxAfterImport.UseVisualStyleBackColor = true;
            this.checkBoxAfterImport.CheckedChanged += new System.EventHandler(this.checkBoxAfterImport_CheckedChanged);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageStandardExpressions);
            this.tabControl.Controls.Add(this.tabPageNestedExpressions);
            this.tabControl.Location = new System.Drawing.Point(0, 26);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(803, 671);
            this.tabControl.TabIndex = 9;
            // 
            // tabPageStandardExpressions
            // 
            this.tabPageStandardExpressions.Controls.Add(this.buttonTestSelected);
            this.tabPageStandardExpressions.Controls.Add(this.buttonCalculateSelected);
            this.tabPageStandardExpressions.Controls.Add(this.labelCondition);
            this.tabPageStandardExpressions.Controls.Add(this.textBoxCondition);
            this.tabPageStandardExpressions.Controls.Add(this.checkBoxActive);
            this.tabPageStandardExpressions.Controls.Add(this.treeListCalculatedFields);
            this.tabPageStandardExpressions.Controls.Add(this.buttonUpdate);
            this.tabPageStandardExpressions.Controls.Add(this.labelExpression);
            this.tabPageStandardExpressions.Controls.Add(this.buttonRemove);
            this.tabPageStandardExpressions.Controls.Add(this.labelCustomField);
            this.tabPageStandardExpressions.Controls.Add(this.buttonAdd);
            this.tabPageStandardExpressions.Controls.Add(this.textBoxExpression);
            this.tabPageStandardExpressions.Controls.Add(this.comboBoxCustomField);
            this.tabPageStandardExpressions.Location = new System.Drawing.Point(4, 22);
            this.tabPageStandardExpressions.Name = "tabPageStandardExpressions";
            this.tabPageStandardExpressions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStandardExpressions.Size = new System.Drawing.Size(795, 645);
            this.tabPageStandardExpressions.TabIndex = 0;
            this.tabPageStandardExpressions.Text = "Standard Expressions";
            this.tabPageStandardExpressions.UseVisualStyleBackColor = true;
            // 
            // buttonTestSelected
            // 
            this.buttonTestSelected.Location = new System.Drawing.Point(581, 11);
            this.buttonTestSelected.Name = "buttonTestSelected";
            this.buttonTestSelected.Size = new System.Drawing.Size(211, 23);
            this.buttonTestSelected.TabIndex = 15;
            this.buttonTestSelected.Text = "Test selected rows";
            this.buttonTestSelected.UseVisualStyleBackColor = true;
            this.buttonTestSelected.Click += new System.EventHandler(this.buttonTestSelected_Click);
            // 
            // buttonCalculateSelected
            // 
            this.buttonCalculateSelected.Location = new System.Drawing.Point(581, 40);
            this.buttonCalculateSelected.Name = "buttonCalculateSelected";
            this.buttonCalculateSelected.Size = new System.Drawing.Size(211, 23);
            this.buttonCalculateSelected.TabIndex = 14;
            this.buttonCalculateSelected.Text = "Calculate selected rows for all activities";
            this.buttonCalculateSelected.UseVisualStyleBackColor = true;
            this.buttonCalculateSelected.Click += new System.EventHandler(this.buttonCalculateSelected_Click);
            // 
            // labelCondition
            // 
            this.labelCondition.AutoSize = true;
            this.labelCondition.Location = new System.Drawing.Point(6, 99);
            this.labelCondition.Name = "labelCondition";
            this.labelCondition.Size = new System.Drawing.Size(51, 13);
            this.labelCondition.TabIndex = 13;
            this.labelCondition.Text = "Condition";
            // 
            // textBoxCondition
            // 
            this.textBoxCondition.Location = new System.Drawing.Point(70, 96);
            this.textBoxCondition.Name = "textBoxCondition";
            this.textBoxCondition.Size = new System.Drawing.Size(722, 20);
            this.textBoxCondition.TabIndex = 12;
            // 
            // checkBoxActive
            // 
            this.checkBoxActive.AutoSize = true;
            this.checkBoxActive.Checked = true;
            this.checkBoxActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxActive.Location = new System.Drawing.Point(9, 17);
            this.checkBoxActive.Name = "checkBoxActive";
            this.checkBoxActive.Size = new System.Drawing.Size(110, 17);
            this.checkBoxActive.TabIndex = 11;
            this.checkBoxActive.Text = "Calculation active";
            this.checkBoxActive.UseVisualStyleBackColor = true;
            // 
            // treeListCalculatedFields
            // 
            this.treeListCalculatedFields.BackColor = System.Drawing.Color.Transparent;
            this.treeListCalculatedFields.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.treeListCalculatedFields.CheckBoxes = false;
            this.treeListCalculatedFields.DefaultIndent = 15;
            this.treeListCalculatedFields.DefaultRowHeight = -1;
            this.treeListCalculatedFields.HeaderRowHeight = 21;
            this.treeListCalculatedFields.Location = new System.Drawing.Point(6, 122);
            this.treeListCalculatedFields.MultiSelect = true;
            this.treeListCalculatedFields.Name = "treeListCalculatedFields";
            this.treeListCalculatedFields.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.treeListCalculatedFields.NumLockedColumns = 0;
            this.treeListCalculatedFields.RowAlternatingColors = true;
            this.treeListCalculatedFields.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.treeListCalculatedFields.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.treeListCalculatedFields.RowHotlightMouse = true;
            this.treeListCalculatedFields.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.treeListCalculatedFields.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.treeListCalculatedFields.RowSeparatorLines = true;
            this.treeListCalculatedFields.ShowLines = false;
            this.treeListCalculatedFields.ShowPlusMinus = false;
            this.treeListCalculatedFields.Size = new System.Drawing.Size(786, 517);
            this.treeListCalculatedFields.TabIndex = 10;
            this.treeListCalculatedFields.SelectedItemsChanged += new System.EventHandler(this.treeListCalculatedFields_SelectedItemsChanged);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonUpdate.Location = new System.Drawing.Point(426, 40);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdate.TabIndex = 9;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // labelExpression
            // 
            this.labelExpression.AutoSize = true;
            this.labelExpression.Location = new System.Drawing.Point(6, 72);
            this.labelExpression.Name = "labelExpression";
            this.labelExpression.Size = new System.Drawing.Size(58, 13);
            this.labelExpression.TabIndex = 8;
            this.labelExpression.Text = "Expression";
            // 
            // buttonRemove
            // 
            this.buttonRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRemove.Location = new System.Drawing.Point(426, 11);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 1;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // labelCustomField
            // 
            this.labelCustomField.AutoSize = true;
            this.labelCustomField.Location = new System.Drawing.Point(6, 45);
            this.labelCustomField.Name = "labelCustomField";
            this.labelCustomField.Size = new System.Drawing.Size(120, 13);
            this.labelCustomField.TabIndex = 7;
            this.labelCustomField.Text = "Calculated Custom Field";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Enabled = false;
            this.buttonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAdd.Location = new System.Drawing.Point(345, 40);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 4;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // textBoxExpression
            // 
            this.textBoxExpression.Location = new System.Drawing.Point(70, 69);
            this.textBoxExpression.Name = "textBoxExpression";
            this.textBoxExpression.Size = new System.Drawing.Size(722, 20);
            this.textBoxExpression.TabIndex = 6;
            this.textBoxExpression.TextChanged += new System.EventHandler(this.textBoxExpression_TextChanged);
            // 
            // comboBoxCustomField
            // 
            this.comboBoxCustomField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCustomField.FormattingEnabled = true;
            this.comboBoxCustomField.Location = new System.Drawing.Point(132, 42);
            this.comboBoxCustomField.Name = "comboBoxCustomField";
            this.comboBoxCustomField.Size = new System.Drawing.Size(207, 21);
            this.comboBoxCustomField.TabIndex = 5;
            this.comboBoxCustomField.SelectedValueChanged += new System.EventHandler(this.comboBoxCustomField_SelectedValueChanged);
            this.comboBoxCustomField.Click += new System.EventHandler(this.comboBoxCustomField_Click);
            // 
            // tabPageNestedExpressions
            // 
            this.tabPageNestedExpressions.Controls.Add(this.treeListNestedExpressions);
            this.tabPageNestedExpressions.Controls.Add(this.buttonUpdateNested);
            this.tabPageNestedExpressions.Controls.Add(this.textBoxNestedExpressionName);
            this.tabPageNestedExpressions.Controls.Add(this.labelNestedExpression);
            this.tabPageNestedExpressions.Controls.Add(this.buttonRemoveNested);
            this.tabPageNestedExpressions.Controls.Add(this.labelNestedExpressionName);
            this.tabPageNestedExpressions.Controls.Add(this.buttonAddNested);
            this.tabPageNestedExpressions.Controls.Add(this.textBoxNestedExpression);
            this.tabPageNestedExpressions.Location = new System.Drawing.Point(4, 22);
            this.tabPageNestedExpressions.Name = "tabPageNestedExpressions";
            this.tabPageNestedExpressions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNestedExpressions.Size = new System.Drawing.Size(795, 645);
            this.tabPageNestedExpressions.TabIndex = 1;
            this.tabPageNestedExpressions.Text = "Nested Expressions";
            this.tabPageNestedExpressions.UseVisualStyleBackColor = true;
            // 
            // treeListNestedExpressions
            // 
            this.treeListNestedExpressions.BackColor = System.Drawing.Color.Transparent;
            this.treeListNestedExpressions.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.treeListNestedExpressions.CheckBoxes = false;
            this.treeListNestedExpressions.DefaultIndent = 15;
            this.treeListNestedExpressions.DefaultRowHeight = -1;
            this.treeListNestedExpressions.HeaderRowHeight = 21;
            this.treeListNestedExpressions.Location = new System.Drawing.Point(6, 95);
            this.treeListNestedExpressions.MultiSelect = true;
            this.treeListNestedExpressions.Name = "treeListNestedExpressions";
            this.treeListNestedExpressions.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.treeListNestedExpressions.NumLockedColumns = 0;
            this.treeListNestedExpressions.RowAlternatingColors = true;
            this.treeListNestedExpressions.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.treeListNestedExpressions.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.treeListNestedExpressions.RowHotlightMouse = true;
            this.treeListNestedExpressions.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.treeListNestedExpressions.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.treeListNestedExpressions.RowSeparatorLines = true;
            this.treeListNestedExpressions.ShowLines = false;
            this.treeListNestedExpressions.ShowPlusMinus = false;
            this.treeListNestedExpressions.Size = new System.Drawing.Size(786, 544);
            this.treeListNestedExpressions.TabIndex = 10;
            this.treeListNestedExpressions.SelectedItemsChanged += new System.EventHandler(this.treeListNestedExpressions_SelectedItemsChanged);
            // 
            // buttonUpdateNested
            // 
            this.buttonUpdateNested.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonUpdateNested.Location = new System.Drawing.Point(426, 40);
            this.buttonUpdateNested.Name = "buttonUpdateNested";
            this.buttonUpdateNested.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdateNested.TabIndex = 16;
            this.buttonUpdateNested.Text = "Update";
            this.buttonUpdateNested.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonUpdateNested.UseVisualStyleBackColor = true;
            this.buttonUpdateNested.Click += new System.EventHandler(this.buttonUpdateNested_Click);
            // 
            // textBoxNestedExpressionName
            // 
            this.textBoxNestedExpressionName.Location = new System.Drawing.Point(138, 42);
            this.textBoxNestedExpressionName.Name = "textBoxNestedExpressionName";
            this.textBoxNestedExpressionName.Size = new System.Drawing.Size(201, 20);
            this.textBoxNestedExpressionName.TabIndex = 15;
            this.textBoxNestedExpressionName.TextChanged += new System.EventHandler(this.textBoxNestedExpressionName_TextChanged);
            // 
            // labelNestedExpression
            // 
            this.labelNestedExpression.AutoSize = true;
            this.labelNestedExpression.Location = new System.Drawing.Point(6, 72);
            this.labelNestedExpression.Name = "labelNestedExpression";
            this.labelNestedExpression.Size = new System.Drawing.Size(58, 13);
            this.labelNestedExpression.TabIndex = 14;
            this.labelNestedExpression.Text = "Expression";
            // 
            // buttonRemoveNested
            // 
            this.buttonRemoveNested.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRemoveNested.Location = new System.Drawing.Point(426, 11);
            this.buttonRemoveNested.Name = "buttonRemoveNested";
            this.buttonRemoveNested.Size = new System.Drawing.Size(75, 23);
            this.buttonRemoveNested.TabIndex = 9;
            this.buttonRemoveNested.Text = "Remove";
            this.buttonRemoveNested.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonRemoveNested.UseVisualStyleBackColor = true;
            this.buttonRemoveNested.Click += new System.EventHandler(this.buttonRemoveNested_Click);
            // 
            // labelNestedExpressionName
            // 
            this.labelNestedExpressionName.AutoSize = true;
            this.labelNestedExpressionName.Location = new System.Drawing.Point(6, 46);
            this.labelNestedExpressionName.Name = "labelNestedExpressionName";
            this.labelNestedExpressionName.Size = new System.Drawing.Size(126, 13);
            this.labelNestedExpressionName.TabIndex = 13;
            this.labelNestedExpressionName.Text = "Nested Expression Name";
            // 
            // buttonAddNested
            // 
            this.buttonAddNested.Enabled = false;
            this.buttonAddNested.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddNested.Location = new System.Drawing.Point(345, 40);
            this.buttonAddNested.Name = "buttonAddNested";
            this.buttonAddNested.Size = new System.Drawing.Size(75, 23);
            this.buttonAddNested.TabIndex = 10;
            this.buttonAddNested.Text = "Add";
            this.buttonAddNested.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddNested.UseVisualStyleBackColor = true;
            this.buttonAddNested.Click += new System.EventHandler(this.buttonAddNested_Click);
            // 
            // textBoxNestedExpression
            // 
            this.textBoxNestedExpression.Location = new System.Drawing.Point(70, 69);
            this.textBoxNestedExpression.Name = "textBoxNestedExpression";
            this.textBoxNestedExpression.Size = new System.Drawing.Size(722, 20);
            this.textBoxNestedExpression.TabIndex = 12;
            this.textBoxNestedExpression.TextChanged += new System.EventHandler(this.textBoxNestedExpression_TextChanged);
            // 
            // labelTrailsIntegration
            // 
            this.labelTrailsIntegration.AutoSize = true;
            this.labelTrailsIntegration.ForeColor = System.Drawing.Color.Red;
            this.labelTrailsIntegration.Location = new System.Drawing.Point(606, 4);
            this.labelTrailsIntegration.Name = "labelTrailsIntegration";
            this.labelTrailsIntegration.Size = new System.Drawing.Size(190, 13);
            this.labelTrailsIntegration.TabIndex = 10;
            this.labelTrailsIntegration.Text = "Integration with Trails plugin is disabled";
            this.labelTrailsIntegration.Visible = false;
            // 
            // CalculatedFieldsSettingsPageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelTrailsIntegration);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.checkBoxAfterImport);
            this.Name = "CalculatedFieldsSettingsPageControl";
            this.Size = new System.Drawing.Size(803, 700);
            this.tabControl.ResumeLayout(false);
            this.tabPageStandardExpressions.ResumeLayout(false);
            this.tabPageStandardExpressions.PerformLayout();
            this.tabPageNestedExpressions.ResumeLayout(false);
            this.tabPageNestedExpressions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxAfterImport;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageNestedExpressions;
        private System.Windows.Forms.Label labelNestedExpression;
        private System.Windows.Forms.Button buttonRemoveNested;
        private System.Windows.Forms.Label labelNestedExpressionName;
        private System.Windows.Forms.Button buttonAddNested;
        private System.Windows.Forms.TextBox textBoxNestedExpression;
        private System.Windows.Forms.TextBox textBoxNestedExpressionName;
        private System.Windows.Forms.Button buttonUpdateNested;
        private System.Windows.Forms.TabPage tabPageStandardExpressions;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Label labelExpression;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Label labelCustomField;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox textBoxExpression;
        private System.Windows.Forms.ComboBox comboBoxCustomField;
        private ZoneFiveSoftware.Common.Visuals.TreeList treeListCalculatedFields;
        private ZoneFiveSoftware.Common.Visuals.TreeList treeListNestedExpressions;
        private System.Windows.Forms.Label labelTrailsIntegration;
        private System.Windows.Forms.CheckBox checkBoxActive;
        private System.Windows.Forms.Label labelCondition;
        private System.Windows.Forms.TextBox textBoxCondition;
        private System.Windows.Forms.Button buttonCalculateSelected;
        private System.Windows.Forms.Button buttonTestSelected;
    }
}
