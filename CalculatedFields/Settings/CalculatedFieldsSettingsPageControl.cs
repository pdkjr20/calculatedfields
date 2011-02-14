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
            buttonAddVirtual.Image = CommonResources.Images.DocumentAdd16;
            buttonRemove.Image = CommonResources.Images.Delete16;
            buttonRemoveNested.Image = CommonResources.Images.Delete16;
            buttonRemoveVirtual.Image = CommonResources.Images.Delete16;
            buttonUpdate.Image = CommonResources.Images.Edit16;
            buttonUpdateNested.Image = CommonResources.Images.Edit16;
            buttonUpdateVirtual.Image = CommonResources.Images.Edit16;

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
            this.comboBoxDataTrackResolution.SelectedItem = GlobalSettings.dataTrackResolution.ToString();

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


            var virtualField = new TreeList.Column("CustomField", "Virtual Field", 150, StringAlignment.Near);
            var virtualExpression = new TreeList.Column("CalculatedExpression", "Expression", 300, StringAlignment.Near);

            treeListVirtualExpressions.Columns.Add(virtualField);
            treeListVirtualExpressions.Columns.Add(virtualExpression);
            treeListVirtualExpressions.Columns.Add(condition);
            treeListVirtualExpressions.Columns.Add(active);

            treeListVirtualExpressions.RowData = GlobalSettings.virtualFieldsRows;
            treeListVirtualExpressions.ThemeChanged(visualTheme);


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
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("HALFTIME"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("TIMESTOPPED"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("DISTANCE"));
            stripActivity.DropDownItems.Add(new ToolStripMenuItem("HALFDISTANCE"));
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
            stripActive.DropDownItems.Add(new ToolStripMenuItem("ACTIVEHALFTIME"));
            stripActive.DropDownItems.Add(new ToolStripMenuItem("ACTIVEDISTANCE"));
            stripActive.DropDownItems.Add(new ToolStripMenuItem("ACTIVEHALFDISTANCE"));
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
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETEHEIGHT"));
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETESEX"));
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETEBIRTHDATE"));
            stripAthlete.DropDownItems.Add(new ToolStripMenuItem("ATHLETEAGE"));

            foreach (ToolStripMenuItem item in stripAthlete.DropDownItems)
            {
                item.Click += fieldItem_Click;
            }

            stripZones.DropDownItems.Add(new ToolStripMenuItem("HR Zone Standard Zone 3 Low bound"));
            stripZones.DropDownItems.Add(new ToolStripMenuItem("HR Zone Standard Zone 4 High bound"));
            stripZones.DropDownItems.Add(new ToolStripMenuItem("Cadence Zone Standard Zone 2 Low bound"));
            stripZones.DropDownItems.Add(new ToolStripMenuItem("Power Zone Standard Strength training High bound"));
            stripZones.DropDownItems.Add(new ToolStripMenuItem("Speed Zone Running Jogging Low bound"));
            stripZones.DropDownItems.Add(new ToolStripMenuItem("Climb Zone Standard Flat High bound"));

            foreach (ToolStripMenuItem item in stripZones.DropDownItems)
            {
                item.Click += fieldItem_Click;
            }

            stripAggregate.DropDownItems.Add(new ToolStripMenuItem("Sum"));
            stripAggregate.DropDownItems.Add(new ToolStripMenuItem("Avg"));
            stripAggregate.DropDownItems.Add(new ToolStripMenuItem("Min"));
            stripAggregate.DropDownItems.Add(new ToolStripMenuItem("Max"));
            stripAggregate.DropDownItems.Add(new ToolStripMenuItem("Count"));

            foreach (ToolStripMenuItem item in stripAggregate.DropDownItems)
            {
                item.Click += fieldItem_Click;
            }

            stripTracks.DropDownItems.Add(new ToolStripMenuItem("HASGPSTRACK"));
            stripTracks.DropDownItems.Add(new ToolStripMenuItem("HASHRTRACK"));
            stripTracks.DropDownItems.Add(new ToolStripMenuItem("HASELEVATIONTRACK"));
            stripTracks.DropDownItems.Add(new ToolStripMenuItem("HASCADENCETRACK"));
            stripTracks.DropDownItems.Add(new ToolStripMenuItem("HASPOWERTRACK"));

            foreach (ToolStripMenuItem item in stripTracks.DropDownItems)
            {
                item.Click += fieldItem_Click;
            }

            stripRange.DropDownItems.Add(new ToolStripMenuItem("Time in HR between 150-180"));
            stripRange.DropDownItems.Add(new ToolStripMenuItem("HR in Pace between 5:00-5:30"));
            stripRange.DropDownItems.Add(new ToolStripMenuItem("Time of first half of distance"));
            stripRange.DropDownItems.Add(new ToolStripMenuItem("Average HR of second half of distance"));
            stripRange.DropDownItems.Add(new ToolStripMenuItem("Time of first half of distance (only active parts)"));
            stripRange.DropDownItems.Add(new ToolStripMenuItem("Average HR of second half of distance (only active parts)"));

            foreach (ToolStripMenuItem item in stripRange.DropDownItems)
            {
                item.Click += fieldItem_Click;
            }

            stripPeak.DropDownItems.Add(new ToolStripMenuItem("Fastest 1000 meters"));
            stripPeak.DropDownItems.Add(new ToolStripMenuItem("Return average HR of your fastest 1000 meters"));
            stripPeak.DropDownItems.Add(new ToolStripMenuItem("Fastest 300 seconds"));
            stripPeak.DropDownItems.Add(new ToolStripMenuItem("HR peak for 30 seconds"));
            stripPeak.DropDownItems.Add(new ToolStripMenuItem("Power peak for 20 seconds"));
            stripPeak.DropDownItems.Add(new ToolStripMenuItem("Power peak for 1000 meters"));
            stripPeak.DropDownItems.Add(new ToolStripMenuItem("Return average cadence for power peak for 20 seconds"));
            stripPeak.DropDownItems.Add(new ToolStripMenuItem("Return average HR for power peak for 20 seconds"));

            foreach (ToolStripMenuItem item in stripPeak.DropDownItems)
            {
                item.Click += fieldItem_Click;
            }

            stripDataTrack.DropDownItems.Add(new ToolStripMenuItem("HR in Pace between 5:00-5:30 (any resolution)"));
            stripDataTrack.DropDownItems.Add(new ToolStripMenuItem("Time in HR between 150-180 (1000 ms resolution)"));
            stripDataTrack.DropDownItems.Add(new ToolStripMenuItem("Time in HR between 150-180 (100 ms resolution, only for active parts)"));
            stripDataTrack.DropDownItems.Add(new ToolStripMenuItem("Distance within first 300 seconds (any resolution)"));
            stripDataTrack.DropDownItems.Add(new ToolStripMenuItem("Average HR of split with notes First segment"));
            

            foreach (ToolStripMenuItem item in stripDataTrack.DropDownItems)
            {
                item.Click += fieldItem_Click;
            }

            stripFormulas.DropDownItems.Add(new ToolStripMenuItem("RECOVERYHR"));
            stripFormulas.DropDownItems.Add(new ToolStripMenuItem("Decoupling ratio of first half and second half of your activity (Ruskie)"));
            stripFormulas.DropDownItems.Add(new ToolStripMenuItem("Decoupling ratio of first half and second half of your activity (active parts)"));
            stripFormulas.DropDownItems.Add(new ToolStripMenuItem("Speed ratio between first half and second half of your activity (Decoupling ratio without HR) (Kuki)"));
            stripFormulas.DropDownItems.Add(new ToolStripMenuItem("Speed ratio between first half and second half of your activity (active parts)"));

            foreach (ToolStripMenuItem item in stripFormulas.DropDownItems)
            {
                item.Click += fieldItem_Click;
            }

            stripFormulasPool.DropDownItems.Add(new ToolStripMenuItem("Bike score to show climby bike index (Stumpjumper68)"));
            stripFormulasPool.DropDownItems.Add(new ToolStripMenuItem("Stride length in mm, you need footpod for this formula (dave a walker)"));
            stripFormulasPool.DropDownItems.Add(new ToolStripMenuItem("Elevation change (GaryS)"));
            stripFormulasPool.DropDownItems.Add(new ToolStripMenuItem("Estimated distance taking ascent into account (vax)"));
            stripFormulasPool.DropDownItems.Add(new ToolStripMenuItem("First half distance Pace (GaryS)"));
            stripFormulasPool.DropDownItems.Add(new ToolStripMenuItem("Second half distance Pace (GaryS)"));
            stripFormulasPool.DropDownItems.Add(new ToolStripMenuItem("% VO2max (Scorpion79)"));
            stripFormulasPool.DropDownItems.Add(new ToolStripMenuItem("Vdot (Scorpion79)"));
            stripFormulasPool.DropDownItems.Add(new ToolStripMenuItem("VO2max (Scorpion79)"));

            foreach (ToolStripMenuItem item in stripFormulasPool.DropDownItems)
            {
                item.Click += fieldItem_Click;
            }

            stripExamples.DropDownItems.Add(new ToolStripMenuItem("Avg. HR/min"));
            stripExamples.DropDownItems.Add(new ToolStripMenuItem("Sum of last 7 days distance"));
            stripExamples.DropDownItems.Add(new ToolStripMenuItem("Sum of last 21 days distance"));
            stripExamples.DropDownItems.Add(new ToolStripMenuItem("Sum of last 7 days distance only for category Running"));
            stripExamples.DropDownItems.Add(new ToolStripMenuItem("Distance to Km"));
            stripExamples.DropDownItems.Add(new ToolStripMenuItem("Distance to Miles"));
            stripExamples.DropDownItems.Add(new ToolStripMenuItem("Speed to Speed in Miles"));
            stripExamples.DropDownItems.Add(new ToolStripMenuItem("Adjust Distance by 3%"));
            stripExamples.DropDownItems.Add(new ToolStripMenuItem("Condition for Category is Race OR Category is Trail"));
            stripExamples.DropDownItems.Add(new ToolStripMenuItem("Condition for Category is Race AND Distance is greater than 7000m"));
            stripExamples.DropDownItems.Add(new ToolStripMenuItem("Avg pace for Trail with name Pernek"));

            foreach (ToolStripMenuItem item in stripExamples.DropDownItems)
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


            if (item.OwnerItem == stripActivity || item.OwnerItem == stripAthlete || item.OwnerItem == stripCustom || item.OwnerItem == stripNested || item.OwnerItem == stripVirtual || item.OwnerItem == stripActive || item.OwnerItem == stripRest || item.OwnerItem == stripTracks)
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

            if (item.OwnerItem == stripDataTrack || item.OwnerItem == stripFormulas || item.OwnerItem == stripFormulasPool || item.OwnerItem == stripRange || item.OwnerItem == stripPeak || item.OwnerItem == stripZones)
            {
                switch (item.Text)
                {
                    case "HR in Pace between 5:00-5:30 (any resolution)":
                        result = "{DATATRACK}.Where(o => o.Pace >= 5 && o.Pace <= 5.5).Average(o => o.HR)";
                        break;
                    case "Time in HR between 150-180 (1000 ms resolution)":
                        result = "{DATATRACK}.Where(o => o.HR >= 150 && o.HR <= 180).Count()";
                        break;
                    case "Time in HR between 150-180 (100 ms resolution, only for active parts)":
                        result = "{DATATRACKACTIVE}.Where(o => o.HR >= 150 && o.HR <= 180).Count() / 10f";
                        break;
                    case "Distance within first 300 seconds (any resolution)":
                        result = "{DATATRACK}.Select((o, index) => new { Field = o.Elapsed, Distance = ({DATATRACK}[((index + 1) < {DATATRACK}.Count) ? index + 1 : index].Distance - o.Distance) }).Where(o => o.Field >= 0 && o.Field <= 300).Sum(o => o.Distance)";
                        break;
                    case "Average HR of split with notes First segment":
                        result = "{DATATRACK}.Where(o => o.LapNote == \"First segment\").Average(o => o.HR)";
                        break;


                    case "Fastest 1000 meters":
                        result = "{MINPEAKDISTANCE(Elapsed,1000)}";
                        break;
                    case "Return average HR of your fastest 1000 meters":
                        result = "{MINPEAKDISTANCE(Elapsed,HR,1000)}";
                        break;
                    case "Fastest 300 seconds":
                        result = "{MAXPEAKTIME(Distance,300)}";
                        break;
                    case "HR peak for 30 seconds":
                        result = "{MAXPEAKTIME(HR,30)}";
                        break;
                    case "Power peak for 20 seconds":
                        result = "{MAXPEAKTIME(Power,20)}";
                        break;
                    case "Power peak for 1000 meters":
                        result = "{MAXPEAKDISTANCE(Power,1000)}";
                        break;
                    case "Return average cadence for power peak for 20 seconds":
                        result = "{MAXPEAKTIME(Power,Cadence,20)}";
                        break;
                    case "Return average HR for power peak for 20 seconds":
                        result = "{MAXPEAKTIME(Power,HR,20)}";
                        break;


                    case "Bike score to show climby bike index (Stumpjumper68)":
                        result = "({ASCENDING} / {DISTANCE} * 100) * 4 + {ASCENDING} * {ASCENDING} / {DISTANCE} + {DISTANCE} / 1000";
                        break;
                    case "Stride length in mm, you need footpod for this formula (dave a walker)":
                        result = "({DISTANCE} / ({TIME} / 60)) / {AVGCADENCE} * 1000 / 2";
                        textBoxCondition.Text = "{AVGCADENCE} != 0";
                        break;
                    case "Elevation change (GaryS)":
                        result = "{MAXPEAKDISTANCE(Elevation,10)} - {MINPEAKDISTANCE(Elevation,10)}";
                        break;
                    case "Estimated distance taking ascent into account (vax)":
                        result = "({DISTANCE} + {ASCENDING} * 10)";
                        break;
                    case "First half distance Pace (GaryS)":
                        result = "{RANGEELAPSED(Distance,0,{HALFDISTANCE})} / {HALFDISTANCE} * 1000";
                        break;
                    case "Second half distance Pace (GaryS)":
                        result = "{RANGEELAPSED(Distance,{HALFDISTANCE},{Distance})} / {HALFDISTANCE} * 1000";
                        break;
                    case "% VO2max (Scorpion79)":
                        result = "(0.8 + 0.1894393 * Math.Exp(-0.012778 * ({DISTANCE}/{TIME}*60)) + 0.2989558 * Math.Exp(-0.1932605 * ({DISTANCE}/{TIME}*60)))";
                        break;
                    case "Vdot (Scorpion79)":
                        result = "(-4.60 + 0.182258 * ({DISTANCE}/{TIME}*60) + 0.000104 * Math.Pow({DISTANCE}/{TIME}*60, 2)) / (0.8 + 0.1894393 * Math.Exp(-0.012778 * ({DISTANCE}/{TIME}*60)) + 0.2989558 * Math.Exp(-0.1932605 * ({DISTANCE}/{TIME}*60)))";
                        break;
                    case "VO2max (Scorpion79)":
                        result = "{ATHLETEWEIGHT} * ((-4.60 + 0.182258 * ({DISTANCE}/{TIME}*60) + 0.000104 * Math.Pow({DISTANCE}/{TIME}*60, 2)) / (0.8 + 0.1894393 * Math.Exp(-0.012778 * ({DISTANCE}/{TIME}*60)) + 0.2989558 * Math.Exp(-0.1932605 * ({DISTANCE}/{TIME}*60)))) / 1000";
                        break;

                    
                    case "Time in HR between 150-180":
                        result = "{RANGEELAPSED(HR,150,180)}";
                        break;
                    case "HR in Pace between 5:00-5:30":
                        result = "{RANGEHR(Pace,5,5.5)}";
                        break;
                    case "Time of first half of distance":
                        result = "{RANGEELAPSED(Distance,0,{HALFDISTANCE})}";
                        break;
                    case "Average HR of second half of distance":
                        result = "{RANGEHR(Distance,{HALFDISTANCE},{DISTANCE})}";
                        break;
                    case "Time of first half of distance (only active parts)":
                        result = "{ACTIVERANGEELAPSED(Distance,0,{HALFDISTANCE})}";
                        break;
                    case "Average HR of second half of distance (only active parts)":
                        result = "{ACTIVERANGEHR(Distance,{HALFDISTANCE},{DISTANCE})}";
                        break;


                    case "RECOVERYHR":
                        result = "{RECOVERYHR(60)}";
                        break;
                    case "Decoupling ratio of first half and second half of your activity (Ruskie)":
                        result = "{DECOUPLINGRATIO}";
                        break;
                    case "Decoupling ratio of first half and second half of your activity (active parts)":
                        result = "{DECOUPLINGRATIOACTIVE}";
                        break;
                    case "Speed ratio between first half and second half of your activity (Decoupling ratio without HR) (Kuki)":
                        result = "{HALFSPEEDRATIO}";
                        break;
                    case "Speed ratio between first half and second half of your activity (active parts)":
                        result = "{HALFSPEEDRATIOACTIVE}";
                        break;



                    case "HR Zone Standard Zone 3 Low bound":
                        result = "{ZONEHR(Standard,Zone 3,LOW)}";
                        break;
                    case "HR Zone Standard Zone 4 High bound":
                        result = "{ZONEHR(Standard,Zone 4,HIGH)}";
                        break;
                    case "Cadence Zone Standard Zone 2 Low bound":
                        result = "{ZONECADENCE(Standard,Zone 2,LOW)}";
                        break;
                    case "Power Zone Standard Strength training High bound":
                        result = "{ZONEPOWER(Standard,Strength training,HIGH)}";
                        break;
                    case "Speed Zone Running Jogging Low bound":
                        result = "{ZONESPEED(Running,Jogging,LOW)}";
                        break;
                    case "Climb Zone Standard Flat High bound":
                        result = "{ZONECLIMB(Standard,Flat,HIGH)}";
                        break;
                }
            }

            if (item.OwnerItem == stripExamples && textBoxExpression.Focused)
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
            if (textBoxVirtualExpression.Focused)
            {
                textBoxVirtualExpression.SelectedText = result;
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
            GlobalSettings.calculatedFieldsRows.Add(new CalculatedFieldsRow(Guid.NewGuid().ToString(), this.comboBoxCustomField.SelectedItem.ToString(), textBoxExpression.Text, textBoxCondition.Text, checkBoxActive.Checked.ToString(), (int)(numericUpDownPace.Value), (int)numericUpDownElevation.Value, (int)numericUpDownHR.Value, (int)numericUpDownCadence.Value, (int)numericUpDownPower.Value));
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

        private void buttonAddVirtual_Click(object sender, EventArgs e)
        {
            GlobalSettings.virtualFieldsRows.Add(new CalculatedFieldsRow(Guid.NewGuid().ToString(), this.textBoxVirtualField.Text, textBoxVirtualExpression.Text, textBoxVirtualCondition.Text, checkBoxVirtualActive.Checked.ToString(), (int)numericUpDownPaceVirtual.Value, (int)numericUpDownElevationVirtual.Value, (int)numericUpDownHRVirtual.Value, (int)numericUpDownCadenceVirtual.Value, (int)numericUpDownPowerVirtual.Value));
            GlobalSettings.virtualFieldsRows.Sort();
            treeListVirtualExpressions.RowData = GlobalSettings.virtualFieldsRows;

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
                    
                    updateRow.SmoothingPace = (int)numericUpDownPace.Value;
                    updateRow.SmoothingElevation = (int)numericUpDownElevation.Value;
                    updateRow.SmoothingHR = (int)numericUpDownHR.Value;
                    updateRow.SmoothingCadence = (int)numericUpDownCadence.Value;
                    updateRow.SmoothingPower = (int)numericUpDownPower.Value;

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

        private void buttonUpdateVirtual_Click(object sender, EventArgs e)
        {
            if (treeListVirtualExpressions.SelectedItems.Count == 1)
            {
                foreach (CalculatedFieldsRow row in treeListVirtualExpressions.SelectedItems)
                {
                    var updateRow = GlobalSettings.virtualFieldsRows.Find((r) => r.ID == row.ID);
                    updateRow.CustomField = this.textBoxVirtualField.Text;
                    updateRow.CalculatedExpression = textBoxVirtualExpression.Text;
                    updateRow.Condition = textBoxVirtualCondition.Text;
                    updateRow.Active = (checkBoxVirtualActive.Checked) ? "Y" : "N";

                    updateRow.SmoothingPace = (int)numericUpDownPaceVirtual.Value;
                    updateRow.SmoothingElevation = (int)numericUpDownElevationVirtual.Value;
                    updateRow.SmoothingHR = (int)numericUpDownHRVirtual.Value;
                    updateRow.SmoothingCadence = (int)numericUpDownCadenceVirtual.Value;
                    updateRow.SmoothingPower = (int)numericUpDownPowerVirtual.Value;

                    GlobalSettings.virtualFieldsRows.Sort();
                    treeListVirtualExpressions.RowData = GlobalSettings.virtualFieldsRows;
                }
            }
            else
            {
                foreach (CalculatedFieldsRow row in treeListVirtualExpressions.SelectedItems)
                {
                    GlobalSettings.virtualFieldsRows.Find((r) => r.ID == row.ID).Active = (checkBoxVirtualActive.Checked) ? "Y" : "N";
                    GlobalSettings.virtualFieldsRows.Sort();
                    treeListVirtualExpressions.RowData = GlobalSettings.virtualFieldsRows;
                }
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

        private void buttonRemoveVirtual_Click(object sender, EventArgs e)
        {
            foreach (CalculatedFieldsRow row in treeListVirtualExpressions.SelectedItems)
            {
                GlobalSettings.virtualFieldsRows.Remove(row);
                treeListVirtualExpressions.RowData = GlobalSettings.virtualFieldsRows;
            }

            GlobalSettings.SaveSettings();
        }

        private void textBoxVirtualField_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxVirtualExpression.Text == "" || textBoxVirtualField.Text == "")
            {
                this.buttonAddNested.Enabled = false;
            }
            else
            {
                this.buttonAddVirtual.Enabled = true;
            }
        }

        private void textBoxVirtualExpression_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxVirtualExpression.Text == "" || textBoxVirtualField.Text == "")
            {
                this.buttonAddNested.Enabled = false;
            }
            else
            {
                this.buttonAddVirtual.Enabled = true;
            }
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

                this.numericUpDownPace.Value = row.SmoothingPace;
                this.numericUpDownElevation.Value = row.SmoothingElevation;
                this.numericUpDownHR.Value = row.SmoothingHR;
                this.numericUpDownCadence.Value = row.SmoothingCadence;
                this.numericUpDownPower.Value = row.SmoothingPower;
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

        private void treeListVirtualExpressions_SelectedItemsChanged(object sender, EventArgs e)
        {
            if (treeListVirtualExpressions.SelectedItems.Count == 0)
            {
                buttonRemoveVirtual.Enabled = false;
                buttonUpdateVirtual.Enabled = false;
            }
            else
            {
                buttonRemoveVirtual.Enabled = true;
                buttonUpdateVirtual.Enabled = true;
            }

            foreach (CalculatedFieldsRow row in treeListVirtualExpressions.SelectedItems)
            {
                this.textBoxVirtualField.Text = row.CustomField;
                this.textBoxVirtualExpression.Text = row.CalculatedExpression;
                this.textBoxVirtualCondition.Text = row.Condition;

                if (row.Active == "Y")
                {
                    this.checkBoxVirtualActive.Checked = true;
                }
                else
                {
                    this.checkBoxVirtualActive.Checked = false;
                }

                this.numericUpDownPaceVirtual.Value = row.SmoothingPace;
                this.numericUpDownElevationVirtual.Value = row.SmoothingElevation;
                this.numericUpDownHRVirtual.Value = row.SmoothingHR;
                this.numericUpDownCadenceVirtual.Value = row.SmoothingCadence;
                this.numericUpDownPowerVirtual.Value = row.SmoothingPower;
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
            stripVirtual.DropDownItems.Clear();

            ICustomDataFieldObjectType typeActivity = CustomDataFieldDefinitions.StandardObjectType(typeof(IActivity));
            ICustomDataFieldObjectType typeAthlete = CustomDataFieldDefinitions.StandardObjectType(typeof(IAthleteInfoEntry));

            foreach (ICustomDataFieldDefinition definition in CalculatedFields.GetLogBook().CustomDataFieldDefinitions)
            {
                if (definition.ObjectType == typeActivity || definition.ObjectType == typeAthlete)
                {
                    stripCustom.DropDownItems.Add(new ToolStripMenuItem(definition.Name));
                }
            }

            foreach (var row in GlobalSettings.nestedFieldsRows)
            {
                stripNested.DropDownItems.Add(new ToolStripMenuItem(row.NestedExpression));
            }

            foreach (var row in GlobalSettings.virtualFieldsRows)
            {
                stripVirtual.DropDownItems.Add(new ToolStripMenuItem(row.CustomField));
            }

            foreach (ToolStripMenuItem item in stripCustom.DropDownItems)
            {
                item.Click += fieldItem_Click;
            }

            foreach (ToolStripMenuItem item in stripNested.DropDownItems)
            {
                item.Click += fieldItem_Click;
            }

            foreach (ToolStripMenuItem item in stripVirtual.DropDownItems)
            {
                item.Click += fieldItem_Click;
            }
        }

        private void labelDonationImage_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=5Y5242N7FPZGY&lc=SK&item_name=Camel%2fCalculated%20Fields%20plugin&item_number=CalculatedFields&currency_code=EUR&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted");
        }

        private void comboBoxDataTrackResolution_SelectedValueChanged(object sender, EventArgs e)
        {
            GlobalSettings.dataTrackResolution = int.Parse(this.comboBoxDataTrackResolution.SelectedItem.ToString());

            GlobalSettings.SaveSettings();
        }
    }
}