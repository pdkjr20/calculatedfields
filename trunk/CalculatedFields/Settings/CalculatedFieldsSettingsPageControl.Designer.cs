﻿namespace CalculatedFields.Settings
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
            this.components = new System.ComponentModel.Container();
            this.checkBoxAfterImport = new System.Windows.Forms.CheckBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageStandardExpressions = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.labelCustomField = new System.Windows.Forms.Label();
            this.labelForAllActivities = new System.Windows.Forms.Label();
            this.comboBoxCustomField = new System.Windows.Forms.ComboBox();
            this.buttonClearSelected = new System.Windows.Forms.Button();
            this.textBoxExpression = new System.Windows.Forms.TextBox();
            this.contextMenuStripFields = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.stripActivity = new System.Windows.Forms.ToolStripMenuItem();
            this.stripActive = new System.Windows.Forms.ToolStripMenuItem();
            this.stripRest = new System.Windows.Forms.ToolStripMenuItem();
            this.stripSplits = new System.Windows.Forms.ToolStripMenuItem();
            this.stripTrails = new System.Windows.Forms.ToolStripMenuItem();
            this.stripAthlete = new System.Windows.Forms.ToolStripMenuItem();
            this.stripRange = new System.Windows.Forms.ToolStripMenuItem();
            this.stripPeak = new System.Windows.Forms.ToolStripMenuItem();
            this.stripAggregate = new System.Windows.Forms.ToolStripMenuItem();
            this.stripCustom = new System.Windows.Forms.ToolStripMenuItem();
            this.stripNested = new System.Windows.Forms.ToolStripMenuItem();
            this.stripTracks = new System.Windows.Forms.ToolStripMenuItem();
            this.stripExamples = new System.Windows.Forms.ToolStripMenuItem();
            this.stripFormulas = new System.Windows.Forms.ToolStripMenuItem();
            this.stripFormulasPool = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonTestSelected = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonCalculateSelected = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.labelCondition = new System.Windows.Forms.Label();
            this.labelExpression = new System.Windows.Forms.Label();
            this.textBoxCondition = new System.Windows.Forms.TextBox();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.checkBoxActive = new System.Windows.Forms.CheckBox();
            this.treeListCalculatedFields = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.tabPageNestedExpressions = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.labelNestedExpressionName = new System.Windows.Forms.Label();
            this.buttonUpdateNested = new System.Windows.Forms.Button();
            this.textBoxNestedExpression = new System.Windows.Forms.TextBox();
            this.textBoxNestedExpressionName = new System.Windows.Forms.TextBox();
            this.buttonAddNested = new System.Windows.Forms.Button();
            this.labelNestedExpression = new System.Windows.Forms.Label();
            this.buttonRemoveNested = new System.Windows.Forms.Button();
            this.treeListNestedExpressions = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.labelTrailsIntegration = new System.Windows.Forms.Label();
            this.checkBoxAfterImportFuture = new System.Windows.Forms.CheckBox();
            this.labelTrailsIntegration2 = new System.Windows.Forms.Label();
            this.labelDonationImage = new System.Windows.Forms.Label();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.labelCopyright2 = new System.Windows.Forms.Label();
            this.labelCopyright3 = new System.Windows.Forms.Label();
            this.labelCopyright4 = new System.Windows.Forms.Label();
            this.labelDonationText = new System.Windows.Forms.Label();
            this.labelDonationText2 = new System.Windows.Forms.Label();
            this.labelDonationsText3 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl.SuspendLayout();
            this.tabPageStandardExpressions.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.contextMenuStripFields.SuspendLayout();
            this.tabPageNestedExpressions.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
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
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(803, 544);
            this.tabControl.TabIndex = 9;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.treeListCalculatedFields_SelectedItemsChanged);
            // 
            // tabPageStandardExpressions
            // 
            this.tabPageStandardExpressions.Controls.Add(this.splitContainer3);
            this.tabPageStandardExpressions.Location = new System.Drawing.Point(4, 22);
            this.tabPageStandardExpressions.Name = "tabPageStandardExpressions";
            this.tabPageStandardExpressions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStandardExpressions.Size = new System.Drawing.Size(795, 518);
            this.tabPageStandardExpressions.TabIndex = 0;
            this.tabPageStandardExpressions.Text = "Standard Expressions";
            this.tabPageStandardExpressions.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.labelCustomField);
            this.splitContainer3.Panel1.Controls.Add(this.labelForAllActivities);
            this.splitContainer3.Panel1.Controls.Add(this.comboBoxCustomField);
            this.splitContainer3.Panel1.Controls.Add(this.buttonClearSelected);
            this.splitContainer3.Panel1.Controls.Add(this.textBoxExpression);
            this.splitContainer3.Panel1.Controls.Add(this.buttonTestSelected);
            this.splitContainer3.Panel1.Controls.Add(this.buttonAdd);
            this.splitContainer3.Panel1.Controls.Add(this.buttonCalculateSelected);
            this.splitContainer3.Panel1.Controls.Add(this.buttonRemove);
            this.splitContainer3.Panel1.Controls.Add(this.labelCondition);
            this.splitContainer3.Panel1.Controls.Add(this.labelExpression);
            this.splitContainer3.Panel1.Controls.Add(this.textBoxCondition);
            this.splitContainer3.Panel1.Controls.Add(this.buttonUpdate);
            this.splitContainer3.Panel1.Controls.Add(this.checkBoxActive);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.treeListCalculatedFields);
            this.splitContainer3.Size = new System.Drawing.Size(789, 512);
            this.splitContainer3.SplitterDistance = 116;
            this.splitContainer3.TabIndex = 18;
            // 
            // labelCustomField
            // 
            this.labelCustomField.AutoSize = true;
            this.labelCustomField.Location = new System.Drawing.Point(3, 38);
            this.labelCustomField.Name = "labelCustomField";
            this.labelCustomField.Size = new System.Drawing.Size(120, 13);
            this.labelCustomField.TabIndex = 7;
            this.labelCustomField.Text = "Calculated Custom Field";
            // 
            // labelForAllActivities
            // 
            this.labelForAllActivities.AutoSize = true;
            this.labelForAllActivities.Location = new System.Drawing.Point(538, 9);
            this.labelForAllActivities.Name = "labelForAllActivities";
            this.labelForAllActivities.Size = new System.Drawing.Size(97, 13);
            this.labelForAllActivities.TabIndex = 17;
            this.labelForAllActivities.Text = "For all activities do:";
            // 
            // comboBoxCustomField
            // 
            this.comboBoxCustomField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCustomField.FormattingEnabled = true;
            this.comboBoxCustomField.Location = new System.Drawing.Point(129, 35);
            this.comboBoxCustomField.Name = "comboBoxCustomField";
            this.comboBoxCustomField.Size = new System.Drawing.Size(207, 21);
            this.comboBoxCustomField.TabIndex = 5;
            this.comboBoxCustomField.SelectedValueChanged += new System.EventHandler(this.comboBoxCustomField_SelectedValueChanged);
            this.comboBoxCustomField.Click += new System.EventHandler(this.comboBoxCustomField_Click);
            // 
            // buttonClearSelected
            // 
            this.buttonClearSelected.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonClearSelected.Location = new System.Drawing.Point(641, 4);
            this.buttonClearSelected.Name = "buttonClearSelected";
            this.buttonClearSelected.Size = new System.Drawing.Size(148, 23);
            this.buttonClearSelected.TabIndex = 16;
            this.buttonClearSelected.Text = "Clear calculated values";
            this.buttonClearSelected.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonClearSelected.UseVisualStyleBackColor = true;
            this.buttonClearSelected.Click += new System.EventHandler(this.buttonClearSelected_Click);
            // 
            // textBoxExpression
            // 
            this.textBoxExpression.ContextMenuStrip = this.contextMenuStripFields;
            this.textBoxExpression.Location = new System.Drawing.Point(67, 62);
            this.textBoxExpression.Name = "textBoxExpression";
            this.textBoxExpression.Size = new System.Drawing.Size(722, 20);
            this.textBoxExpression.TabIndex = 6;
            this.textBoxExpression.TextChanged += new System.EventHandler(this.textBoxExpression_TextChanged);
            // 
            // contextMenuStripFields
            // 
            this.contextMenuStripFields.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripActivity,
            this.stripActive,
            this.stripRest,
            this.stripSplits,
            this.stripTrails,
            this.stripAthlete,
            this.stripRange,
            this.stripPeak,
            this.stripAggregate,
            this.stripCustom,
            this.stripNested,
            this.stripTracks,
            this.stripExamples,
            this.stripFormulas,
            this.stripFormulasPool});
            this.contextMenuStripFields.Name = "contextMenuStripFields";
            this.contextMenuStripFields.Size = new System.Drawing.Size(175, 334);
            this.contextMenuStripFields.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStripFields_Opening);
            // 
            // stripActivity
            // 
            this.stripActivity.Name = "stripActivity";
            this.stripActivity.Size = new System.Drawing.Size(174, 22);
            this.stripActivity.Text = "Activity";
            // 
            // stripActive
            // 
            this.stripActive.Name = "stripActive";
            this.stripActive.Size = new System.Drawing.Size(174, 22);
            this.stripActive.Text = "Active";
            // 
            // stripRest
            // 
            this.stripRest.Name = "stripRest";
            this.stripRest.Size = new System.Drawing.Size(174, 22);
            this.stripRest.Text = "Rest";
            // 
            // stripSplits
            // 
            this.stripSplits.Name = "stripSplits";
            this.stripSplits.Size = new System.Drawing.Size(174, 22);
            this.stripSplits.Text = "Splits";
            // 
            // stripTrails
            // 
            this.stripTrails.Name = "stripTrails";
            this.stripTrails.Size = new System.Drawing.Size(174, 22);
            this.stripTrails.Text = "Trails";
            // 
            // stripAthlete
            // 
            this.stripAthlete.Name = "stripAthlete";
            this.stripAthlete.Size = new System.Drawing.Size(174, 22);
            this.stripAthlete.Text = "Athlete";
            // 
            // stripRange
            // 
            this.stripRange.Name = "stripRange";
            this.stripRange.Size = new System.Drawing.Size(174, 22);
            this.stripRange.Text = "Range";
            // 
            // stripPeak
            // 
            this.stripPeak.Name = "stripPeak";
            this.stripPeak.Size = new System.Drawing.Size(174, 22);
            this.stripPeak.Text = "Peak values";
            // 
            // stripAggregate
            // 
            this.stripAggregate.Name = "stripAggregate";
            this.stripAggregate.Size = new System.Drawing.Size(174, 22);
            this.stripAggregate.Text = "Aggregate";
            // 
            // stripCustom
            // 
            this.stripCustom.Name = "stripCustom";
            this.stripCustom.Size = new System.Drawing.Size(174, 22);
            this.stripCustom.Text = "Custom Fields";
            // 
            // stripNested
            // 
            this.stripNested.Name = "stripNested";
            this.stripNested.Size = new System.Drawing.Size(174, 22);
            this.stripNested.Text = "Nested Expressions";
            // 
            // stripTracks
            // 
            this.stripTracks.Name = "stripTracks";
            this.stripTracks.Size = new System.Drawing.Size(174, 22);
            this.stripTracks.Text = "Data Tracks";
            // 
            // stripExamples
            // 
            this.stripExamples.Name = "stripExamples";
            this.stripExamples.Size = new System.Drawing.Size(174, 22);
            this.stripExamples.Text = "Examples";
            // 
            // stripFormulas
            // 
            this.stripFormulas.Name = "stripFormulas";
            this.stripFormulas.Size = new System.Drawing.Size(174, 22);
            this.stripFormulas.Text = "Formulas";
            // 
            // stripFormulasPool
            // 
            this.stripFormulasPool.Name = "stripFormulasPool";
            this.stripFormulasPool.Size = new System.Drawing.Size(174, 22);
            this.stripFormulasPool.Text = "Formulas pool";
            // 
            // buttonTestSelected
            // 
            this.buttonTestSelected.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTestSelected.Location = new System.Drawing.Point(517, 33);
            this.buttonTestSelected.Name = "buttonTestSelected";
            this.buttonTestSelected.Size = new System.Drawing.Size(118, 23);
            this.buttonTestSelected.TabIndex = 15;
            this.buttonTestSelected.Text = "Test selected rows";
            this.buttonTestSelected.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonTestSelected.UseVisualStyleBackColor = true;
            this.buttonTestSelected.Click += new System.EventHandler(this.buttonTestSelected_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Enabled = false;
            this.buttonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAdd.Location = new System.Drawing.Point(342, 33);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 4;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonCalculateSelected
            // 
            this.buttonCalculateSelected.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCalculateSelected.Location = new System.Drawing.Point(641, 33);
            this.buttonCalculateSelected.Name = "buttonCalculateSelected";
            this.buttonCalculateSelected.Size = new System.Drawing.Size(148, 23);
            this.buttonCalculateSelected.TabIndex = 14;
            this.buttonCalculateSelected.Text = "Calculate selected rows";
            this.buttonCalculateSelected.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCalculateSelected.UseVisualStyleBackColor = true;
            this.buttonCalculateSelected.Click += new System.EventHandler(this.buttonCalculateSelected_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRemove.Location = new System.Drawing.Point(423, 4);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 1;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // labelCondition
            // 
            this.labelCondition.AutoSize = true;
            this.labelCondition.Location = new System.Drawing.Point(3, 92);
            this.labelCondition.Name = "labelCondition";
            this.labelCondition.Size = new System.Drawing.Size(51, 13);
            this.labelCondition.TabIndex = 13;
            this.labelCondition.Text = "Condition";
            // 
            // labelExpression
            // 
            this.labelExpression.AutoSize = true;
            this.labelExpression.Location = new System.Drawing.Point(3, 65);
            this.labelExpression.Name = "labelExpression";
            this.labelExpression.Size = new System.Drawing.Size(58, 13);
            this.labelExpression.TabIndex = 8;
            this.labelExpression.Text = "Expression";
            // 
            // textBoxCondition
            // 
            this.textBoxCondition.ContextMenuStrip = this.contextMenuStripFields;
            this.textBoxCondition.Location = new System.Drawing.Point(67, 89);
            this.textBoxCondition.Name = "textBoxCondition";
            this.textBoxCondition.Size = new System.Drawing.Size(722, 20);
            this.textBoxCondition.TabIndex = 12;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonUpdate.Location = new System.Drawing.Point(423, 33);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdate.TabIndex = 9;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // checkBoxActive
            // 
            this.checkBoxActive.AutoSize = true;
            this.checkBoxActive.Checked = true;
            this.checkBoxActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxActive.Location = new System.Drawing.Point(6, 10);
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
            this.treeListCalculatedFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListCalculatedFields.HeaderRowHeight = 21;
            this.treeListCalculatedFields.Location = new System.Drawing.Point(0, 0);
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
            this.treeListCalculatedFields.Size = new System.Drawing.Size(789, 392);
            this.treeListCalculatedFields.TabIndex = 11;
            this.treeListCalculatedFields.SelectedItemsChanged += new System.EventHandler(this.treeListCalculatedFields_SelectedItemsChanged);
            // 
            // tabPageNestedExpressions
            // 
            this.tabPageNestedExpressions.Controls.Add(this.splitContainer4);
            this.tabPageNestedExpressions.Location = new System.Drawing.Point(4, 22);
            this.tabPageNestedExpressions.Name = "tabPageNestedExpressions";
            this.tabPageNestedExpressions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNestedExpressions.Size = new System.Drawing.Size(795, 518);
            this.tabPageNestedExpressions.TabIndex = 1;
            this.tabPageNestedExpressions.Text = "Nested Expressions";
            this.tabPageNestedExpressions.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(3, 3);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.labelNestedExpressionName);
            this.splitContainer4.Panel1.Controls.Add(this.buttonUpdateNested);
            this.splitContainer4.Panel1.Controls.Add(this.textBoxNestedExpression);
            this.splitContainer4.Panel1.Controls.Add(this.textBoxNestedExpressionName);
            this.splitContainer4.Panel1.Controls.Add(this.buttonAddNested);
            this.splitContainer4.Panel1.Controls.Add(this.labelNestedExpression);
            this.splitContainer4.Panel1.Controls.Add(this.buttonRemoveNested);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.treeListNestedExpressions);
            this.splitContainer4.Size = new System.Drawing.Size(789, 512);
            this.splitContainer4.SplitterDistance = 87;
            this.splitContainer4.TabIndex = 17;
            // 
            // labelNestedExpressionName
            // 
            this.labelNestedExpressionName.AutoSize = true;
            this.labelNestedExpressionName.Location = new System.Drawing.Point(3, 39);
            this.labelNestedExpressionName.Name = "labelNestedExpressionName";
            this.labelNestedExpressionName.Size = new System.Drawing.Size(126, 13);
            this.labelNestedExpressionName.TabIndex = 13;
            this.labelNestedExpressionName.Text = "Nested Expression Name";
            // 
            // buttonUpdateNested
            // 
            this.buttonUpdateNested.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonUpdateNested.Location = new System.Drawing.Point(423, 33);
            this.buttonUpdateNested.Name = "buttonUpdateNested";
            this.buttonUpdateNested.Size = new System.Drawing.Size(75, 23);
            this.buttonUpdateNested.TabIndex = 16;
            this.buttonUpdateNested.Text = "Update";
            this.buttonUpdateNested.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonUpdateNested.UseVisualStyleBackColor = true;
            this.buttonUpdateNested.Click += new System.EventHandler(this.buttonUpdateNested_Click);
            // 
            // textBoxNestedExpression
            // 
            this.textBoxNestedExpression.ContextMenuStrip = this.contextMenuStripFields;
            this.textBoxNestedExpression.Location = new System.Drawing.Point(67, 62);
            this.textBoxNestedExpression.Name = "textBoxNestedExpression";
            this.textBoxNestedExpression.Size = new System.Drawing.Size(722, 20);
            this.textBoxNestedExpression.TabIndex = 12;
            this.textBoxNestedExpression.TextChanged += new System.EventHandler(this.textBoxNestedExpression_TextChanged);
            // 
            // textBoxNestedExpressionName
            // 
            this.textBoxNestedExpressionName.Location = new System.Drawing.Point(135, 35);
            this.textBoxNestedExpressionName.Name = "textBoxNestedExpressionName";
            this.textBoxNestedExpressionName.Size = new System.Drawing.Size(201, 20);
            this.textBoxNestedExpressionName.TabIndex = 15;
            this.textBoxNestedExpressionName.TextChanged += new System.EventHandler(this.textBoxNestedExpressionName_TextChanged);
            // 
            // buttonAddNested
            // 
            this.buttonAddNested.Enabled = false;
            this.buttonAddNested.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAddNested.Location = new System.Drawing.Point(342, 33);
            this.buttonAddNested.Name = "buttonAddNested";
            this.buttonAddNested.Size = new System.Drawing.Size(75, 23);
            this.buttonAddNested.TabIndex = 10;
            this.buttonAddNested.Text = "Add";
            this.buttonAddNested.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonAddNested.UseVisualStyleBackColor = true;
            this.buttonAddNested.Click += new System.EventHandler(this.buttonAddNested_Click);
            // 
            // labelNestedExpression
            // 
            this.labelNestedExpression.AutoSize = true;
            this.labelNestedExpression.Location = new System.Drawing.Point(3, 65);
            this.labelNestedExpression.Name = "labelNestedExpression";
            this.labelNestedExpression.Size = new System.Drawing.Size(58, 13);
            this.labelNestedExpression.TabIndex = 14;
            this.labelNestedExpression.Text = "Expression";
            // 
            // buttonRemoveNested
            // 
            this.buttonRemoveNested.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRemoveNested.Location = new System.Drawing.Point(423, 4);
            this.buttonRemoveNested.Name = "buttonRemoveNested";
            this.buttonRemoveNested.Size = new System.Drawing.Size(75, 23);
            this.buttonRemoveNested.TabIndex = 9;
            this.buttonRemoveNested.Text = "Remove";
            this.buttonRemoveNested.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonRemoveNested.UseVisualStyleBackColor = true;
            this.buttonRemoveNested.Click += new System.EventHandler(this.buttonRemoveNested_Click);
            // 
            // treeListNestedExpressions
            // 
            this.treeListNestedExpressions.BackColor = System.Drawing.Color.Transparent;
            this.treeListNestedExpressions.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.treeListNestedExpressions.CheckBoxes = false;
            this.treeListNestedExpressions.DefaultIndent = 15;
            this.treeListNestedExpressions.DefaultRowHeight = -1;
            this.treeListNestedExpressions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListNestedExpressions.HeaderRowHeight = 21;
            this.treeListNestedExpressions.Location = new System.Drawing.Point(0, 0);
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
            this.treeListNestedExpressions.Size = new System.Drawing.Size(789, 421);
            this.treeListNestedExpressions.TabIndex = 18;
            this.treeListNestedExpressions.SelectedItemsChanged += new System.EventHandler(this.treeListNestedExpressions_SelectedItemsChanged);
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
            // 
            // checkBoxAfterImportFuture
            // 
            this.checkBoxAfterImportFuture.AutoSize = true;
            this.checkBoxAfterImportFuture.Location = new System.Drawing.Point(3, 24);
            this.checkBoxAfterImportFuture.Name = "checkBoxAfterImportFuture";
            this.checkBoxAfterImportFuture.Size = new System.Drawing.Size(256, 17);
            this.checkBoxAfterImportFuture.TabIndex = 11;
            this.checkBoxAfterImportFuture.Text = "Recalculate future planned activities upon import";
            this.checkBoxAfterImportFuture.UseVisualStyleBackColor = true;
            this.checkBoxAfterImportFuture.CheckedChanged += new System.EventHandler(this.checkBoxAfterImportFuture_CheckedChanged);
            // 
            // labelTrailsIntegration2
            // 
            this.labelTrailsIntegration2.AutoSize = true;
            this.labelTrailsIntegration2.Location = new System.Drawing.Point(596, 25);
            this.labelTrailsIntegration2.Name = "labelTrailsIntegration2";
            this.labelTrailsIntegration2.Size = new System.Drawing.Size(200, 13);
            this.labelTrailsIntegration2.TabIndex = 18;
            this.labelTrailsIntegration2.Text = "Thanks to Brandon Doherty and Gerhard";
            // 
            // labelDonationImage
            // 
            this.labelDonationImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelDonationImage.Image = global::CalculatedFields.Properties.Resources.btn_donateCC_LG;
            this.labelDonationImage.Location = new System.Drawing.Point(414, 7);
            this.labelDonationImage.Name = "labelDonationImage";
            this.labelDonationImage.Size = new System.Drawing.Size(91, 47);
            this.labelDonationImage.TabIndex = 19;
            this.labelDonationImage.Click += new System.EventHandler(this.labelDonationImage_Click);
            // 
            // labelCopyright
            // 
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.Location = new System.Drawing.Point(3, 7);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(141, 13);
            this.labelCopyright.TabIndex = 20;
            this.labelCopyright.Text = "Copyright Peter Furucz 2010";
            // 
            // labelCopyright2
            // 
            this.labelCopyright2.AutoSize = true;
            this.labelCopyright2.Location = new System.Drawing.Point(3, 20);
            this.labelCopyright2.Name = "labelCopyright2";
            this.labelCopyright2.Size = new System.Drawing.Size(401, 13);
            this.labelCopyright2.TabIndex = 21;
            this.labelCopyright2.Text = "Calculated Fields Plugin is distributed under the GNU Lesser General Public Licen" +
                "se";
            // 
            // labelCopyright3
            // 
            this.labelCopyright3.AutoSize = true;
            this.labelCopyright3.Location = new System.Drawing.Point(3, 33);
            this.labelCopyright3.Name = "labelCopyright3";
            this.labelCopyright3.Size = new System.Drawing.Size(306, 13);
            this.labelCopyright3.TabIndex = 22;
            this.labelCopyright3.Text = "The license is included in the plugin installation directory and at:";
            // 
            // labelCopyright4
            // 
            this.labelCopyright4.AutoSize = true;
            this.labelCopyright4.Location = new System.Drawing.Point(3, 46);
            this.labelCopyright4.Name = "labelCopyright4";
            this.labelCopyright4.Size = new System.Drawing.Size(187, 13);
            this.labelCopyright4.TabIndex = 23;
            this.labelCopyright4.Text = "http://www.gnu.org/licenses/lgpl.html";
            // 
            // labelDonationText
            // 
            this.labelDonationText.AutoSize = true;
            this.labelDonationText.Location = new System.Drawing.Point(521, 7);
            this.labelDonationText.Name = "labelDonationText";
            this.labelDonationText.Size = new System.Drawing.Size(229, 13);
            this.labelDonationText.TabIndex = 24;
            this.labelDonationText.Text = "If you like this plugin, please consider donating.";
            // 
            // labelDonationText2
            // 
            this.labelDonationText2.AutoSize = true;
            this.labelDonationText2.Location = new System.Drawing.Point(521, 20);
            this.labelDonationText2.Name = "labelDonationText2";
            this.labelDonationText2.Size = new System.Drawing.Size(264, 13);
            this.labelDonationText2.TabIndex = 25;
            this.labelDonationText2.Text = "You will get special care for your requests and support.";
            // 
            // labelDonationsText3
            // 
            this.labelDonationsText3.AutoSize = true;
            this.labelDonationsText3.Location = new System.Drawing.Point(828, 19);
            this.labelDonationsText3.Name = "labelDonationsText3";
            this.labelDonationsText3.Size = new System.Drawing.Size(0, 13);
            this.labelDonationsText3.TabIndex = 26;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxAfterImport);
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxAfterImportFuture);
            this.splitContainer1.Panel1.Controls.Add(this.labelTrailsIntegration);
            this.splitContainer1.Panel1.Controls.Add(this.labelTrailsIntegration2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(803, 700);
            this.splitContainer1.SplitterDistance = 68;
            this.splitContainer1.TabIndex = 27;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.labelDonationText2);
            this.splitContainer2.Panel2.Controls.Add(this.labelCopyright3);
            this.splitContainer2.Panel2.Controls.Add(this.labelDonationsText3);
            this.splitContainer2.Panel2.Controls.Add(this.labelCopyright);
            this.splitContainer2.Panel2.Controls.Add(this.labelCopyright2);
            this.splitContainer2.Panel2.Controls.Add(this.labelDonationImage);
            this.splitContainer2.Panel2.Controls.Add(this.labelCopyright4);
            this.splitContainer2.Panel2.Controls.Add(this.labelDonationText);
            this.splitContainer2.Size = new System.Drawing.Size(803, 628);
            this.splitContainer2.SplitterDistance = 544;
            this.splitContainer2.TabIndex = 0;
            // 
            // CalculatedFieldsSettingsPageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.splitContainer1);
            this.Name = "CalculatedFieldsSettingsPageControl";
            this.Size = new System.Drawing.Size(803, 700);
            this.tabControl.ResumeLayout(false);
            this.tabPageStandardExpressions.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.contextMenuStripFields.ResumeLayout(false);
            this.tabPageNestedExpressions.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Label labelTrailsIntegration;
        private System.Windows.Forms.CheckBox checkBoxActive;
        private System.Windows.Forms.Label labelCondition;
        private System.Windows.Forms.TextBox textBoxCondition;
        private System.Windows.Forms.Button buttonCalculateSelected;
        private System.Windows.Forms.Button buttonTestSelected;
        private System.Windows.Forms.Label labelForAllActivities;
        private System.Windows.Forms.Button buttonClearSelected;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFields;
        private System.Windows.Forms.ToolStripMenuItem stripActivity;
        private System.Windows.Forms.ToolStripMenuItem stripSplits;
        private System.Windows.Forms.ToolStripMenuItem stripTrails;
        private System.Windows.Forms.ToolStripMenuItem stripCustom;
        private System.Windows.Forms.ToolStripMenuItem stripAthlete;
        private System.Windows.Forms.ToolStripMenuItem stripNested;
        private System.Windows.Forms.ToolStripMenuItem stripActive;
        private System.Windows.Forms.ToolStripMenuItem stripRest;
        private System.Windows.Forms.ToolStripMenuItem stripExamples;
        private System.Windows.Forms.ToolStripMenuItem stripAggregate;
        private System.Windows.Forms.CheckBox checkBoxAfterImportFuture;
        private System.Windows.Forms.ToolStripMenuItem stripTracks;
        private System.Windows.Forms.ToolStripMenuItem stripFormulas;
        private System.Windows.Forms.Label labelTrailsIntegration2;
        private System.Windows.Forms.ToolStripMenuItem stripRange;
        private System.Windows.Forms.ToolStripMenuItem stripPeak;
        private System.Windows.Forms.Label labelDonationImage;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label labelCopyright2;
        private System.Windows.Forms.Label labelCopyright3;
        private System.Windows.Forms.Label labelCopyright4;
        private System.Windows.Forms.Label labelDonationText;
        private System.Windows.Forms.Label labelDonationText2;
        private System.Windows.Forms.Label labelDonationsText3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripMenuItem stripFormulasPool;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private ZoneFiveSoftware.Common.Visuals.TreeList treeListCalculatedFields;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private ZoneFiveSoftware.Common.Visuals.TreeList treeListNestedExpressions;
    }
}
