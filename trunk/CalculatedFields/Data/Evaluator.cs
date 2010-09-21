namespace CalculatedFields.Data
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using System.Xml;

    using Microsoft.CSharp;

    using TrailsPlugin.Data;

    using UI;

    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Data.Fitness.CustomData;
    using ZoneFiveSoftware.Common.Data.GPS;

    internal static class Evaluator
    {
        #region Constants and Fields

        private static bool cacheEnabled = true;
        private static Dictionary<string, object> expressionsCache = new Dictionary<string, object>();

        private static readonly CompilerParameters cp = new CompilerParameters();
        private static readonly CSharpCodeProvider provider = new CSharpCodeProvider();

        private static readonly Regex bracketsPattern = new Regex("{*}*", RegexOptions.Compiled);
        private static readonly Regex fieldPattern = new Regex("{[A-Za-z0-9\\(\\), ]*}", RegexOptions.Compiled);
        //private static readonly Regex fieldPattern = new Regex("{.*}", RegexOptions.Compiled);

        #endregion

        #region Constructors and Destructors

        static Evaluator()
        {
            cp.GenerateExecutable = false;
            cp.GenerateInMemory = true;
            cp.TreatWarningsAsErrors = false;
            cp.ReferencedAssemblies.Add("System.dll");
        }

        #endregion

        #region Public Methods

        public static void Calculate(IList<IActivity> activities, List<CalculatedFieldsRow> filter, bool testRun)
        {
            if ((activities != null) && (activities.Count > 0))
            {
                var progressBarForm = new ProgressBarForm();
                progressBarForm.Text = Resources.StringResources.ProgressFormTitle;

                progressBarForm.Show();

                object result;

                int actualActivity = 0;

                foreach (var row in GlobalSettings.calculatedFieldsRows)
                {
                    row.CompilationTime = 0;
                    row.ParsingTime = 0;
                }

                try
                {
                    foreach (IActivity activity in activities)
                    {
                        if (testRun)
                        {
                            progressBarForm.ProgressBarLabelText = "Testing calculation of fields for activity " + ++actualActivity +
                                                                   "/30";
                            progressBarForm.ProgressBarValue += 1000000 / 30;
                        }
                        else
                        {
                            progressBarForm.ProgressBarLabelText = "Calculating fields for activity " + ++actualActivity +
                                                                   "/" + activities.Count.ToString();
                            progressBarForm.ProgressBarValue += 1000000 / activities.Count;
                        }

                        ICustomDataFieldObjectType type =
                            CustomDataFieldDefinitions.StandardObjectType(typeof(IActivity));

                        foreach (ICustomDataFieldDefinition definition in CalculatedFields.GetLogBook().CustomDataFieldDefinitions)
                        {
                            if (progressBarForm.Cancelled || (testRun && actualActivity >= 30))
                            {
                                progressBarForm.Close();
                                break;
                            }

                            if ((filter != null && filter.Exists((o) => (o.CustomField == definition.Name))) || filter == null)
                            {
                                if (definition.ObjectType == type &&
                                    GlobalSettings.calculatedFieldsRows.Exists(
                                        (o) => (o.CustomField == definition.Name && o.Active == "Y")))
                                {
                                    var calculatedFieldsRow =
                                        GlobalSettings.calculatedFieldsRows.Find(
                                            (o) => o.CustomField == definition.Name);

                                    if (calculatedFieldsRow.Condition != "")
                                    {
                                        if (
                                            Evaluate(calculatedFieldsRow.Condition, activity, "", calculatedFieldsRow).
                                                ToString() == "True")
                                        {
                                            result = Evaluate(
                                                calculatedFieldsRow.CalculatedExpression,
                                                activity,
                                                calculatedFieldsRow.Condition,
                                                calculatedFieldsRow);
                                        }
                                        else
                                        {
                                            result = null;
                                        }
                                    }
                                    else
                                    {
                                        result = Evaluate(
                                            calculatedFieldsRow.CalculatedExpression, activity, "", calculatedFieldsRow);
                                    }

                                    if (result != null && !testRun)
                                    {
                                        if (definition.DataType.Id ==
                                            CustomDataFieldDefinitions.StandardDataTypes.NumberDataTypeId)
                                        {
                                            activity.SetCustomDataValue(definition, Double.Parse(result.ToString()));
                                        }
                                        else if (definition.DataType.Id ==
                                                 CustomDataFieldDefinitions.StandardDataTypes.TextDataTypeId)
                                        {
                                            activity.SetCustomDataValue(definition, result.ToString());
                                        }
                                        else if (definition.DataType.Id ==
                                                 CustomDataFieldDefinitions.StandardDataTypes.TimeSpanDataTypeId)
                                        {
                                            double seconds = double.Parse(result.ToString());
                                            activity.SetCustomDataValue(
                                                definition,
                                                new TimeSpan(
                                                    (int)(seconds / 3600),
                                                    (int)((seconds % 3600) / 60),
                                                    (int)(seconds % 60)));
                                        }
                                    }
                                }
                            }

                            Application.DoEvents();
                        }

                        if (progressBarForm.Cancelled || (testRun && actualActivity >= 30))
                        {
                            progressBarForm.Close();
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                    progressBarForm.Close();

                    throw;
                }

                progressBarForm.Close();
            }
        }

        #endregion

        #region Methods

        private static object Evaluate(string expression, IActivity activity, string condition, CalculatedFieldsRow calculatedFieldsRow)
        {
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();

            expression = ParseExpression(expression, activity, condition, calculatedFieldsRow);
            if (expression == "")
            {
                return null;
            }

            //stopwatch.Stop();
            //calculatedFieldsRow.ParsingTime += stopwatch.Elapsed.Milliseconds;
            
            //stopwatch.Reset();
            //stopwatch.Start();

            if (cacheEnabled)
            {
                if (expressionsCache.ContainsKey(expression))
                {
                    //stopwatch.Stop();
                    //calculatedFieldsRow.CompilationTime += stopwatch.Elapsed.Milliseconds;
                    return expressionsCache[expression];
                }
            }

            string TempModuleSource = "namespace ns{" + "using System;" + "using System.Text.RegularExpressions;" +
                                      "class CF{" + "public static object Evaluate(){return " + expression + ";}}} ";

            CompilerResults cr = provider.CompileAssemblyFromSource(cp, TempModuleSource);
            if (cr.Errors.Count > 0)
            {
                string errorText = "Expression: \"" + expression + "\" cannot be evaluated, please use a valid C# expression\n\n";

                foreach (CompilerError error in cr.Errors)
                {
                    if (!error.IsWarning)
                    {
                        errorText += "\"" + error.ErrorText + "\"" + "\n";
                    }
                }

                throw new ArgumentException(errorText);
            }
            else
            {
                MethodInfo methodInfo = cr.CompiledAssembly.GetType("ns.CF").GetMethod("Evaluate");
                object result = methodInfo.Invoke(null, null);

                if (cacheEnabled)
                {
                    expressionsCache.Add(expression, result);
                }

                //stopwatch.Stop();
                //calculatedFieldsRow.CompilationTime += stopwatch.Elapsed.Milliseconds;
                return result;
            }
        }

        private static string ParseExpression(string expression, IActivity activity, string condition, CalculatedFieldsRow calculatedFieldsRow)
        {
            string field;
            string fieldValue = "";
            string nestedExpressionsHistory = "";

            Dictionary<string, IList<TrailResult>> trails = null;

            string history = "";

            ActivityInfo activityInfoInstance = ActivityInfoCache.Instance.GetInfo(activity);

            while (fieldPattern.IsMatch(expression))
            {
                field = fieldPattern.Match(expression).Value;
                field = bracketsPattern.Replace(field, "");
                field = field.ToUpper();

                if (fieldValue == "")
                {
                    fieldValue = ActivityFields(activity, activityInfoInstance, field);
                }

                if (fieldValue == "")
                {
                    fieldValue = SplitsFields(activity, activityInfoInstance, field);
                }

                if (fieldValue == "")
                {
                    fieldValue = TrailsFields(activity, activityInfoInstance, field, trails);
                }

                if (fieldValue == "")
                {
                    fieldValue = AthleteFields(activity, activityInfoInstance, field);
                }

                if (fieldValue == "")
                {
                    fieldValue = CustomFields(activity, activityInfoInstance, field);
                }

                if (fieldValue == "")
                {
                    fieldValue = NestedExpressions(field, nestedExpressionsHistory);
                }

                if (fieldValue == "")
                {
                    fieldValue = LastXDays(activity, activityInfoInstance, condition, field, calculatedFieldsRow);
                }

                if (fieldValue == "" || fieldValue == "NaN" || fieldValue == "Infinity")
                {
                    expression = "";
                }

                expression = fieldPattern.Replace(expression, fieldValue, 1);

                history += expression + "-";

                fieldValue = "";
            }

            //throw new Exception(history);

            return expression;
        }

        private static string LastXDays(IActivity activity, ActivityInfo activityInfo, string condition, string field, CalculatedFieldsRow calculatedFieldsRow)
        {
            string fieldValue = "";

            if (!field.StartsWith("TRAIL"))
            {
                double result = 0;

                string aggField, aggOperation;
                int count = 0, days = 0;

                aggField = Regex.Match(field, ".*(?=\\()").Value;
                aggOperation = Regex.Match(field, "(?<=\\().*(?=,)").Value;

                if (aggOperation != "")
                {
                    days = Int32.Parse(Regex.Match(field, "(?<=,)[ 0-9]*(?=\\))").Value.Trim());

                    DateTime actualActivityDate = activity.StartTime;

                    foreach (var pastActivity in CalculatedFields.GetLogBook().Activities)
                    {
                        if (pastActivity.StartTime <= actualActivityDate)
                        {
                            if (actualActivityDate.Date.Subtract(pastActivity.StartTime.Date).Days < days && (condition == "" || (condition != "" && Evaluate(condition, pastActivity, "", calculatedFieldsRow).ToString() == "True")))
                            {
                                count++;

                                switch (aggOperation)
                                {
                                    case "SUM":
                                        result +=
                                            double.Parse(
                                                Evaluate("{" + aggField + "}", pastActivity, condition, calculatedFieldsRow).ToString());
                                        break;
                                    case "AVG":
                                        result +=
                                            double.Parse(
                                                Evaluate("{" + aggField + "}", pastActivity, condition, calculatedFieldsRow).ToString());
                                        break;
                                    case "MAX":
                                        if (result == 0)
                                        {
                                            result =
                                                double.Parse(
                                                    Evaluate("{" + aggField + "}", pastActivity, condition, calculatedFieldsRow).ToString());
                                        }

                                        if (
                                            double.Parse(
                                                Evaluate("{" + aggField + "}", pastActivity, condition, calculatedFieldsRow).ToString()) >
                                            result)
                                        {
                                            result =
                                                double.Parse(
                                                    Evaluate("{" + aggField + "}", pastActivity, condition, calculatedFieldsRow).ToString());
                                        }
                                        break;
                                    case "MIN":
                                        if (result == 0)
                                        {
                                            result =
                                                double.Parse(
                                                    Evaluate("{" + aggField + "}", pastActivity, condition, calculatedFieldsRow).ToString());
                                        }

                                        if (
                                            double.Parse(
                                                Evaluate("{" + aggField + "}", pastActivity, condition, calculatedFieldsRow).ToString()) <
                                            result)
                                        {
                                            result =
                                                double.Parse(
                                                    Evaluate("{" + aggField + "}", pastActivity, condition, calculatedFieldsRow).ToString());
                                        }
                                        break;
                                }
                            }
                        }
                    }

                    switch (aggOperation)
                    {
                        case "AVG":
                            if (count != 0)
                            {
                                result /= count;
                            }
                            else
                            {
                                result = 0;
                            }
                            break;
                    }

                    fieldValue = result.ToString(CultureInfo.InvariantCulture.NumberFormat);
                }
            }

            return fieldValue;
        }

        private static string CustomFields(IActivity activity, ActivityInfo activityInfoInstance, string field)
        {
            string fieldValue = "";

            ICustomDataFieldObjectType type = CustomDataFieldDefinitions.StandardObjectType(typeof(IActivity));

            foreach (ICustomDataFieldDefinition definition in
                CalculatedFields.GetLogBook().CustomDataFieldDefinitions)
            {
                if (definition.Name.ToUpper() == field && definition.ObjectType == type)
                {
                    fieldValue = activity.GetCustomDataValue(definition).ToString();
                }
            }

            return fieldValue;
        }

        private static string NestedExpressions(string field, string nestedExpressionsHistory)
        {
            string fieldValue = "";

            foreach (var row in GlobalSettings.nestedFieldsRows)
            {
                if (row.NestedExpression.ToUpper() == field)
                {
                    if (!nestedExpressionsHistory.Contains("{" + field + "}"))
                    {
                        fieldValue = row.Expression;

                        nestedExpressionsHistory += "{" + field + "}";
                    }
                    else
                    {
                        throw new Exception("You can't use recursive formula.");
                    }
                }
            }

            return fieldValue;
        }

        private static string AthleteFields(IActivity activity, ActivityInfo activityInfoInstance, string field)
        {
            string fieldValue = "";

            var athleteEntry = CalculatedFields.GetLogBook().Athlete.InfoEntries.LastEntryAsOfDate(activity.StartTime);

            switch (field)
            {
                case "ATHLETEBMI":
                    fieldValue = athleteEntry.BMI.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "ATHLETEBODYFATPERCENTAGE":
                    fieldValue = athleteEntry.BodyFatPercentage.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "ATHLETECALORIES":
                    fieldValue = athleteEntry.CaloriesConsumed.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "ATHLETEDATE":
                    fieldValue = "\"" + athleteEntry.Date.ToShortDateString() + "\"";
                    break;
                case "ATHLETEDIARY":
                    fieldValue = "\"" + athleteEntry.DiaryText + "\"";
                    break;
                case "ATHLETEDIASTOLICBLOODPRESSURE":
                    fieldValue = athleteEntry.DiastolicBloodPressure.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "ATHLETESYSTOLICBLOODPRESSURE":
                    fieldValue = athleteEntry.SystolicBloodPressure.ToString(CultureInfo.InvariantCulture.NumberFormat);

                    break;
                case "ATHLETEINJURED":
                    fieldValue = athleteEntry.Injured.ToString();
                    break;
                case "ATHLETEINJUREDTEXT":
                    fieldValue = "\"" + athleteEntry.InjuredText + "\"";
                    break;
                case "ATHLETEMAXHR":
                    fieldValue = athleteEntry.MaximumHeartRatePerMinute.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "ATHLETEMISSEDWORKOUT":
                    fieldValue = athleteEntry.MissedWorkout.ToString();
                    break;
                case "ATHLETEMISSEDWORKOUTTEXT":
                    fieldValue = "\"" + athleteEntry.MissedWorkoutText + "\"";
                    break;
                case "ATHLETEMOOD":
                    fieldValue = athleteEntry.Mood.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "ATHLETERESTHR":
                    fieldValue = athleteEntry.RestingHeartRatePerMinute.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "ATHLETESICK":
                    fieldValue = athleteEntry.Sick.ToString();
                    break;
                case "ATHLETESICKTEXT":
                    fieldValue = "\"" + athleteEntry.SickText + "\"";
                    break;
                case "ATHLETESKINFOLD":
                    fieldValue = athleteEntry.Skinfold.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "ATHLETESLEEPHOURS":
                    fieldValue = athleteEntry.SleepHours.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "ATHLETESLEEPQUALITY":
                    fieldValue = "\"" + athleteEntry.SleepQuality.ToString(CultureInfo.InvariantCulture.NumberFormat) + "\"";
                    break;
                case "ATHLETEWEIGHT":
                    fieldValue = athleteEntry.WeightKilograms.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
            }

            ICustomDataFieldObjectType type = CustomDataFieldDefinitions.StandardObjectType(typeof(IAthleteInfoEntry));

            foreach (ICustomDataFieldDefinition definition in
                CalculatedFields.GetLogBook().CustomDataFieldDefinitions)
            {
                if (definition.Name.ToUpper() == field && definition.ObjectType == type)
                {
                    fieldValue = athleteEntry.GetCustomDataValue(definition).ToString();
                }
            }

            return fieldValue;
        }

        private static string ActivityFields(IActivity activity, ActivityInfo activityInfoInstance, string field)
        {
            string fieldValue = "";
            double pace;

            switch (field)
            {
                //special
                case "EQUIPMENT":
                    foreach (IEquipmentItem equipment in activity.EquipmentUsed)
                    {
                        if (fieldValue != "")
                        {
                            fieldValue += ", ";
                        }

                        fieldValue += equipment.Name;
                    }

                    fieldValue = "\"" + fieldValue + "\"";
                    break;
                //others
                case "CATEGORY":
                    fieldValue = "\"" + activity.Category.Name + "\"";
                    break;
                case "TEMPERATURE":
                    fieldValue = activity.Weather.TemperatureCelsius.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "WEATHERNOTES":
                    fieldValue = "\"" + activity.Weather.ConditionsText + "\"";
                    break;
                case "LOCATION":
                    fieldValue = "\"" + activity.Location + "\"";
                    break;
                case "NAME":
                    fieldValue = "\"" + activity.Name + "\"";
                    break;
                case "DATETIME":
                    fieldValue = "\"" + activity.StartTime + "\"";
                    break;
                case "DATE":
                    fieldValue = "\"" + activity.StartTime.ToShortDateString() + "\"";
                    break;
                case "NOTES":
                    fieldValue = "\"" + activity.Notes + "\"";
                    break;
                case "INTENSITY":
                    fieldValue = activity.Intensity.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;

                //totals)
                case "TIME":
                    fieldValue = activityInfoInstance.Time.TotalSeconds.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "DISTANCE":
                    fieldValue = activityInfoInstance.DistanceMeters.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "AVGPACE":
                    pace = activityInfoInstance.Time.TotalSeconds / activityInfoInstance.DistanceMeters * 1000;
                    fieldValue = pace.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "AVGCADENCE":
                    fieldValue = activityInfoInstance.AverageCadence.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "AVGGRADE":
                    fieldValue = activityInfoInstance.AverageGrade.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "AVGHR":
                    fieldValue = activityInfoInstance.AverageHeartRate.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "AVGPOWER":
                    fieldValue = activityInfoInstance.AveragePower.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "AVGSPEED":
                    fieldValue = activityInfoInstance.AverageSpeedMetersPerSecond.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "MAXCADENCE":
                    fieldValue = activityInfoInstance.MaximumCadence.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "MAXGRADE":
                    fieldValue = activityInfoInstance.MaximumGrade.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "MAXHR":
                    fieldValue = activityInfoInstance.MaximumHeartRate.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "MAXPOWER":
                    fieldValue = activityInfoInstance.MaximumPower.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "ASCENDING":
                    fieldValue = activityInfoInstance.TotalAscendingMeters(CalculatedFields.GetLogBook().ClimbZones[0]).ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "DESCENDING":
                    fieldValue = activityInfoInstance.TotalDescendingMeters(CalculatedFields.GetLogBook().ClimbZones[0]).ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;

                //active
                case "ACTIVETIME":
                    fieldValue = activityInfoInstance.ActiveLapsTotalDetail.LapElapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "ACTIVEDISTANCE":
                    fieldValue = activityInfoInstance.ActiveLapsTotalDetail.LapDistanceMeters.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "ACTIVEAVGPACE":
                    pace = activityInfoInstance.ActiveLapsTotalDetail.LapElapsed.TotalSeconds /
                                activityInfoInstance.ActiveLapsTotalDetail.LapDistanceMeters * 1000;
                    fieldValue = pace.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "ACTIVEAVGSPEED":
                    fieldValue = (activityInfoInstance.ActiveLapsTotalDetail.LapDistanceMeters /
                                 activityInfoInstance.ActiveLapsTotalDetail.LapElapsed.TotalSeconds).ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "ACTIVEAVGCADENCE":
                    fieldValue = activityInfoInstance.ActiveLapsTotalDetail.AverageCadencePerMinute.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "ACTIVEAVGHR":
                    fieldValue = activityInfoInstance.ActiveLapsTotalDetail.AverageHeartRatePerMinute.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "ACTIVEAVGPOWER":
                    fieldValue = activityInfoInstance.ActiveLapsTotalDetail.AveragePowerWatts.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "ACTIVEMAXCADENCE":
                    fieldValue = activityInfoInstance.ActiveLapsTotalDetail.MaximumCadencePerMinute.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "ACTIVEMAXHR":
                    fieldValue = activityInfoInstance.ActiveLapsTotalDetail.MaximumHeartRatePerMinute.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "ACTIVEMAXPOWER":
                    fieldValue = activityInfoInstance.ActiveLapsTotalDetail.MaximumPowerWatts.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;

                //rest
                case "RESTTIME":
                    fieldValue = activityInfoInstance.RestLapsTotalDetail.LapElapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "RESTDISTANCE":
                    fieldValue = activityInfoInstance.RestLapsTotalDetail.LapDistanceMeters.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "RESTAVGPACE":
                    pace = activityInfoInstance.RestLapsTotalDetail.LapElapsed.TotalSeconds /
                                     activityInfoInstance.RestLapsTotalDetail.LapDistanceMeters * 1000;
                    fieldValue = pace.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "RESTAVGSPEED":
                    fieldValue = (activityInfoInstance.RestLapsTotalDetail.LapDistanceMeters /
                                 activityInfoInstance.RestLapsTotalDetail.LapElapsed.TotalSeconds).ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "RESTAVGCADENCE":
                    fieldValue = activityInfoInstance.RestLapsTotalDetail.AverageCadencePerMinute.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "RESTAVGHR":
                    fieldValue = activityInfoInstance.RestLapsTotalDetail.AverageHeartRatePerMinute.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "RESTAVGPOWER":
                    fieldValue = activityInfoInstance.RestLapsTotalDetail.AveragePowerWatts.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "RESTMAXCADENCE":
                    fieldValue = activityInfoInstance.RestLapsTotalDetail.MaximumCadencePerMinute.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "RESTMAXHR":
                    fieldValue = activityInfoInstance.RestLapsTotalDetail.MaximumHeartRatePerMinute.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "RESTMAXPOWER":
                    fieldValue = activityInfoInstance.RestLapsTotalDetail.MaximumPowerWatts.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
            }

            return fieldValue;
        }

        private static string SplitsFields(IActivity activity, ActivityInfo activityInfoInstance, string field)
        {
            string fieldValue = "";

            if (field.StartsWith("SPLIT"))
            {
                string splitField;
                int splitNumber = 0;

                splitField = Regex.Match(field, "(?<=SPLIT).*(?=\\()").Value;
                splitNumber = Int32.Parse(Regex.Match(field, "(?<=\\()[0-9]*(?=\\))").Value);

                // ak mi nezadal split default mu vytiahnem prvy
                if (splitNumber == 0)
                {
                    splitNumber = 1;
                }

                if (splitField != "" && activityInfoInstance.RecordedLapDetailInfo.Count >= splitNumber)
                {
                    var split = activityInfoInstance.RecordedLapDetailInfo[splitNumber - 1];

                    switch (splitField)
                    {
                        case "NOTES":
                            fieldValue = "\"" + split.Notes + "\"";
                            break;
                        case "AVGPACE":
                            double pace = split.LapElapsed.TotalSeconds / split.LapDistanceMeters * 1000;
                            fieldValue = pace.ToString(CultureInfo.InvariantCulture.NumberFormat);
                            break;
                        case "AVGSPEED":
                            fieldValue = (split.LapDistanceMeters / split.LapElapsed.Seconds).ToString(CultureInfo.InvariantCulture.NumberFormat);
                            break;
                        case "DISTANCE":
                            fieldValue = split.LapDistanceMeters.ToString(CultureInfo.InvariantCulture.NumberFormat);
                            break;
                        case "TIME":
                            fieldValue = split.LapElapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture.NumberFormat);
                            break;
                        case "AVGCADENCE":
                            fieldValue = split.AverageCadencePerMinute.ToString(CultureInfo.InvariantCulture.NumberFormat);
                            break;
                        case "AVGHR":
                            fieldValue = split.AverageHeartRatePerMinute.ToString(CultureInfo.InvariantCulture.NumberFormat);
                            break;
                        case "AVGPOWER":
                            fieldValue = split.AveragePowerWatts.ToString(CultureInfo.InvariantCulture.NumberFormat);
                            break;
                        case "MAXCADENCE":
                            fieldValue = split.MaximumCadencePerMinute.ToString(CultureInfo.InvariantCulture.NumberFormat);
                            break;
                        case "MAXHR":
                            fieldValue = split.MaximumHeartRatePerMinute.ToString(CultureInfo.InvariantCulture.NumberFormat);
                            break;
                        case "MAXPOWER":
                            fieldValue = split.MaximumPowerWatts.ToString(CultureInfo.InvariantCulture.NumberFormat);
                            break;
                    }
                }
            }

            return fieldValue;
        }

        private static string TrailsFields(IActivity activity, ActivityInfo activityInfoInstance, string field, Dictionary<string, IList<TrailResult>> trails)
        {
            string fieldValue = "";

            if (field.StartsWith("TRAIL"))
            {
                string trailField;
                string trailName;
                int trailSplit = 0;

                trailField = Regex.Match(field, "(?<=TRAIL).*(?=\\()").Value;
                trailName = Regex.Match(field, "(?<=\\().*(?=,)").Value;
                trailSplit = Int32.Parse(Regex.Match(field, "(?<=,)[ 0-9]*(?=\\))").Value.Trim());

                // ak mi nezadal split default mu vytiahnem prvy
                if (trailSplit == 0)
                {
                    trailSplit = 1;
                }

                //if je tam aby sa nevykonaval vypocet trails redundantne
                if (trails == null)
                {
                    trails = Trails.GetTrailsResultsForActivity(activity);
                }

                if (trailField != "" && trailName != "")
                {
                    string trailKey = "";

                    foreach (var trail in trails)
                    {
                        if (trail.Key.ToUpper() == trailName)
                        {
                            trailKey = trail.Key;
                        }
                    }


                    if (trailKey != "" && trails[trailKey].Count >= trailSplit)
                    {
                        var trail = trails[trailKey];

                        switch (trailField)
                        {
                            case "AVGPACE":
                                double pace = trail[trailSplit - 1].AvgPace;
                                fieldValue = pace.ToString(CultureInfo.InvariantCulture.NumberFormat);
                                break;
                            case "DISTANCE":
                                fieldValue = trail[trailSplit - 1].Distance.ToString(CultureInfo.InvariantCulture.NumberFormat);
                                break;
                            case "TIME":
                                fieldValue = trail[trailSplit - 1].Duration.TotalSeconds.ToString(CultureInfo.InvariantCulture.NumberFormat);
                                break;
                            case "AVGCADENCE":
                                fieldValue = trail[trailSplit - 1].AvgCadence.ToString(CultureInfo.InvariantCulture.NumberFormat);
                                break;
                            case "AVGGRADE":
                                fieldValue = trail[trailSplit - 1].AvgGrade.ToString(CultureInfo.InvariantCulture.NumberFormat);
                                break;
                            case "AVGHR":
                                fieldValue = trail[trailSplit - 1].AvgHR.ToString(CultureInfo.InvariantCulture.NumberFormat);
                                break;
                            case "AVGPOWER":
                                fieldValue = trail[trailSplit - 1].AvgPower.ToString(CultureInfo.InvariantCulture.NumberFormat);
                                break;
                            case "AVGSPEED":
                                fieldValue = trail[trailSplit - 1].AvgSpeed.ToString(CultureInfo.InvariantCulture.NumberFormat);
                                break;
                            case "ELEVATIONCHANGE":
                                fieldValue = trail[trailSplit - 1].ElevChg.ToString(CultureInfo.InvariantCulture.NumberFormat);
                                break;
                            case "MAXHR":
                                fieldValue = trail[trailSplit - 1].MaxHR.ToString(CultureInfo.InvariantCulture.NumberFormat);
                                break;
                        }
                    }
                }
            }

            return fieldValue;
        }

        #endregion
    }
}