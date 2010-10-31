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
    using System.Linq;
    using System.Xml.Serialization;
    using System.IO;
    using System.Web;
    using Utils;

    using Microsoft.CSharp;

    using UI;

    using ZoneFiveSoftware.Common.Data.Fitness;
    using ZoneFiveSoftware.Common.Data.Fitness.CustomData;
    using ZoneFiveSoftware.Common.Data.GPS;
    using ZoneFiveSoftware.Common.Data;

    using ITrailExport;

    internal static class Evaluator
    {
        #region Constants and Fields

        private static List<DataTrackPoint> dataTrackCache11 = null;
        private static DateTime dataTrackCacheStamp11;
        private static List<DataTrackPoint> dataTrackCache10 = null;
        private static DateTime dataTrackCacheStamp10;
        private static List<DataTrackPoint> dataTrackCache01 = null;
        private static DateTime dataTrackCacheStamp01;
        private static List<DataTrackPoint> dataTrackCache00 = null;
        private static DateTime dataTrackCacheStamp00;

        private static uint dataTrackElement = 1000;
        private static bool cacheEnabled = true;
        private static Dictionary<string, object> expressionsCache = new Dictionary<string, object>();

        private static readonly CompilerParameters cp = new CompilerParameters(new[] { "mscorlib.dll", "System.dll", "System.Core.dll", "System.Xml.dll" });
        private static readonly CSharpCodeProvider provider = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });

        private static readonly Regex bracketsPattern = new Regex("{*}*", RegexOptions.Compiled);
        //private static readonly Regex fieldPattern = new Regex("{[a-zA-Z0-9{}(),. _%]*}", RegexOptions.Compiled);
        private static readonly Regex fieldPattern = new Regex("{[^}]*}", RegexOptions.Compiled);

        #endregion

        #region Constructors and Destructors

        static Evaluator()
        {
            cp.GenerateExecutable = false;
            cp.GenerateInMemory = true;
            cp.TreatWarningsAsErrors = false;

            Directory.CreateDirectory(Environment.GetEnvironmentVariable("APPDATA") + "/CalculatedFieldsPlugin/TEMP");

            cp.TempFiles = new TempFileCollection(Environment.GetEnvironmentVariable("APPDATA") + "/CalculatedFieldsPlugin/TEMP/");
            //cp.ReferencedAssemblies.Add("System.dll");
            //cp.ReferencedAssemblies.Add("System.Xml.dll");
            //cp.ReferencedAssemblies.Add("System.Core.dll");

           // cp.ReferencedAssemblies.Add("LinqBridge.dll");
        }

        #endregion

        #region Public Methods

        public static void ClearCalculations(IList<IActivity> activities, List<CalculatedFieldsRow> filter)
        {
            if ((activities != null) && (activities.Count > 0))
            {
                foreach (IActivity activity in activities)
                {
                    ICustomDataFieldObjectType type = CustomDataFieldDefinitions.StandardObjectType(typeof(IActivity));

                    foreach (ICustomDataFieldDefinition definition in
                        CalculatedFields.GetLogBook().CustomDataFieldDefinitions)
                    {
                        if ((filter != null && filter.Exists((o) => (o.CustomField == definition.Name))) || filter == null)
                        {
                            if (definition.ObjectType == type &&
                                GlobalSettings.calculatedFieldsRows.Exists(
                                    (o) => (o.CustomField == definition.Name && o.Active == "Y")))
                            {
                                activity.SetCustomDataValue(definition, null);
                            }
                        }
                    }
                }
            }
        }

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
                                    var allCalculatedFieldsRow = GlobalSettings.calculatedFieldsRows.Where((o) => o.CustomField == definition.Name);

                                    foreach (var calculatedFieldsRow in allCalculatedFieldsRow)
                                    {
                                        if (calculatedFieldsRow.Condition != "")
                                        {
                                            var conditionResult = Evaluate(calculatedFieldsRow.Condition, activity, "", calculatedFieldsRow);

                                            if (conditionResult != null && conditionResult.ToString() == "True")
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
            expression = ParseExpression(expression, activity, condition, calculatedFieldsRow);
            if (expression == "")
            {
                return null;
            }

            if (cacheEnabled)
            {
                if (expressionsCache.ContainsKey(expression))
                {
                    return expressionsCache[expression];
                }
            }

            string tempModuleSource = "namespace ns{" + "using System;" + "using System.Text.RegularExpressions;" +
                                      "class CF{" +
                                      "public static object Evaluate(){return " + expression + ";}}" +
                                      "}";

            CompilerResults cr = provider.CompileAssemblyFromSource(cp, tempModuleSource);
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

                throw new Exception(expression + "\n\n" + errorText);
            }
            else
            {
                object result;
                MethodInfo methodInfo = cr.CompiledAssembly.GetType("ns.CF").GetMethod("Evaluate");

                try
                {
                    result = methodInfo.Invoke(null, null);
                }
                catch (Exception e)
                {
                    throw new Exception(expression + "\n\n" + e.Message + "\n\n" + e.InnerException.Message);
                }

                if (cacheEnabled)
                {
                    expressionsCache.Add(expression, result);
                }

                return result;
            }
        }

        private static string FindField(string field)
        {
            string returnValue = "";

            if (bracketsPattern.IsMatch(field))
            {
                int opening = 0, closing = 0;

                foreach (var c in field)
                {
                    if (c == '{')
                    {
                        opening++;
                    }
                    if (c == '}')
                    {
                        closing++;
                    }

                    if (opening > 0)
                    {
                        returnValue += c;
                    }

                    if (opening > 0 && (opening == closing))
                    {
                        break;
                    }
                }
            }

            return returnValue;
        }

        private static List<DataTrackPoint> GetDataTrack(IActivity activity, bool onlyActive, bool includePauses)
        {
            if (onlyActive && includePauses)
            {
                if (dataTrackCache11 != null && activity.StartTime.Ticks == dataTrackCacheStamp11.Ticks)
                {
                    return dataTrackCache11;
                }
                else
                {
                    dataTrackCacheStamp11 = activity.StartTime;
                    dataTrackCache11 = CalculateDataTrack(activity, onlyActive, includePauses);

                    return dataTrackCache11;
                }
            }
            else if (onlyActive && !includePauses)
            {
                if (dataTrackCache10 != null && activity.StartTime.Ticks == dataTrackCacheStamp10.Ticks)
                {
                    return dataTrackCache10;
                }
                else
                {
                    dataTrackCacheStamp10 = activity.StartTime;
                    dataTrackCache10 = CalculateDataTrack(activity, onlyActive, includePauses);

                    return dataTrackCache10;
                }
            }
            else if (!onlyActive && includePauses)
            {
                if (dataTrackCache01 != null && activity.StartTime.Ticks == dataTrackCacheStamp01.Ticks)
                {
                    return dataTrackCache01;
                }
                else
                {
                    dataTrackCacheStamp01 = activity.StartTime;
                    dataTrackCache01 = CalculateDataTrack(activity, onlyActive, includePauses);

                    return dataTrackCache01;
                }
            }
            else //(!onlyActive && !includePauses)
            {
                if (dataTrackCache00 != null && activity.StartTime.Ticks == dataTrackCacheStamp00.Ticks)
                {
                    return dataTrackCache00;
                }
                else
                {
                    dataTrackCacheStamp00 = activity.StartTime;
                    dataTrackCache00 = CalculateDataTrack(activity, onlyActive, includePauses);

                    return dataTrackCache00;
                }
            }
        }

        private static List<DataTrackPoint> CalculateDataTrack(IActivity activity, bool onlyActive, bool includePauses)
        {
            List<DataTrackPoint> dataTrack = new List<DataTrackPoint>();

            ActivityInfo activityInfoInstance = ActivityInfoCache.Instance.GetInfo(activity);

            uint totalElapsed = 0;
            DateTime startTime = DateTime.MaxValue;

            if (activityInfoInstance.SmoothedSpeedTrack.TotalElapsedSeconds != 0)
            {
                totalElapsed = activityInfoInstance.SmoothedSpeedTrack.TotalElapsedSeconds * 1000;
                startTime = activityInfoInstance.SmoothedSpeedTrack.StartTime;
            }
            else if (activityInfoInstance.ActualDistanceMetersTrack.TotalElapsedSeconds != 0)
            {
                totalElapsed = activityInfoInstance.ActualDistanceMetersTrack.TotalElapsedSeconds * 1000;
                startTime = activityInfoInstance.ActualDistanceMetersTrack.StartTime;
            }
            else if (activityInfoInstance.SmoothedHeartRateTrack.TotalElapsedSeconds != 0)
            {
                totalElapsed = activityInfoInstance.SmoothedHeartRateTrack.TotalElapsedSeconds * 1000;
                startTime = activityInfoInstance.SmoothedHeartRateTrack.StartTime;
            }
            else if (activityInfoInstance.SmoothedPowerTrack.TotalElapsedSeconds != 0)
            {
                totalElapsed = activityInfoInstance.SmoothedPowerTrack.TotalElapsedSeconds * 1000;
                startTime = activityInfoInstance.SmoothedPowerTrack.StartTime;
            }
            else if (activityInfoInstance.SmoothedGradeTrack.TotalElapsedSeconds != 0)
            {
                totalElapsed = activityInfoInstance.SmoothedGradeTrack.TotalElapsedSeconds * 1000;
                startTime = activityInfoInstance.SmoothedGradeTrack.StartTime;
            }
            else if (activityInfoInstance.SmoothedElevationTrack.TotalElapsedSeconds != 0)
            {
                totalElapsed = activityInfoInstance.SmoothedElevationTrack.TotalElapsedSeconds * 1000;
                startTime = activityInfoInstance.SmoothedElevationTrack.StartTime;
            }
            else if (activityInfoInstance.SmoothedCadenceTrack.TotalElapsedSeconds != 0)
            {
                totalElapsed = activityInfoInstance.SmoothedCadenceTrack.TotalElapsedSeconds * 1000;
                startTime = activityInfoInstance.SmoothedCadenceTrack.StartTime;
            }


            //throw new Exception(totalElapsed.ToString());
            if (totalElapsed == 0)
            {
                return dataTrack;
            }

            float pauseShift = 0;
            for (uint i = 0; i <= totalElapsed; i += dataTrackElement)
            {
                bool paused = false;
                bool rest = false;
                DateTime adjustedTime = startTime.AddMilliseconds(i);

                foreach (var timer in activity.TimerPauses)
                {
                    if (adjustedTime >= timer.Lower && adjustedTime <= timer.Upper)
                    {
                        paused = true;
                        if (!includePauses)
                        {
                            pauseShift += dataTrackElement;
                        }
                        break;
                    }
                }

                foreach (var activeLap in activityInfoInstance.LapDetailInfo(ActivityLapsType.RecordedLapsType).Where(o => o.Rest))
                {
                    if (adjustedTime >= activeLap.StartTime && adjustedTime <= activeLap.EndTime)
                    {
                        rest = true;
                        if (onlyActive)
                        {
                            pauseShift += dataTrackElement;
                        }
                        break;
                    }
                }

                if ((!paused || includePauses) && (!rest || !onlyActive))
                {
                    float hr = 0, pace = 0, speed = 0, elevation = 0, grade = 0, cadence = 0, power = 0, distance = 0, climbSpeed = 0;
                    ITimeValueEntry<float> interpolatedValue;

                    interpolatedValue = activityInfoInstance.SmoothedHeartRateTrack.GetInterpolatedValue(adjustedTime);
                    if (interpolatedValue != null)
                    {
                        if (paused)
                        {
                            hr = 0;
                        }
                        else
                        {
                            hr = interpolatedValue.Value;
                        }
                    }
                    interpolatedValue = activityInfoInstance.ActualDistanceMetersTrack.GetInterpolatedValue(adjustedTime);
                    if (interpolatedValue != null)
                    {
                        distance = interpolatedValue.Value;
                    }
                    interpolatedValue = activityInfoInstance.SmoothedSpeedTrack.GetInterpolatedValue(adjustedTime);
                    if (interpolatedValue != null)
                    {
                        if (paused)
                        {
                            pace = 0;
                            speed = 0;
                        }
                        else
                        {
                            pace = 60 / (interpolatedValue.Value * 3.6f);
                            speed = interpolatedValue.Value * 3.6f;
                        }
                    }
                    interpolatedValue = activityInfoInstance.SmoothedElevationTrack.GetInterpolatedValue(adjustedTime);
                    if (interpolatedValue != null)
                    {
                        if (paused)
                        {
                            elevation = 0;
                        }
                        else
                        {
                            elevation = interpolatedValue.Value;
                        }
                    }
                    interpolatedValue = activityInfoInstance.SmoothedGradeTrack.GetInterpolatedValue(adjustedTime);
                    if (interpolatedValue != null)
                    {
                        if (paused)
                        {
                            grade = 0;
                            climbSpeed = 0;
                        }
                        else
                        {
                            grade = interpolatedValue.Value;

                            if (speed != 0)
                            {
                                //throw new Exception(speed + "-" + grade);
                                climbSpeed = speed * 1000f * grade;
                            }
                        }
                    }
                    interpolatedValue = activityInfoInstance.SmoothedCadenceTrack.GetInterpolatedValue(adjustedTime);
                    if (interpolatedValue != null)
                    {
                        if (paused)
                        {
                            cadence = 0;
                        }
                        else
                        {
                            cadence = interpolatedValue.Value;
                        }
                    }
                    interpolatedValue = activityInfoInstance.SmoothedPowerTrack.GetInterpolatedValue(adjustedTime);
                    if (interpolatedValue != null)
                    {
                        if (paused)
                        {
                            power = 0;
                        }
                        else
                        {
                            power = interpolatedValue.Value;
                        }
                    }

                    dataTrack.Add(new DataTrackPoint(distance, hr, pace, speed, elevation, grade, cadence, power, (i - pauseShift) / 1000, climbSpeed, paused));
                }
            }

            //throw new Exception((totalElapsed - pauseShift).ToString());
            // var query = dataTrack.Where((o, index) => o.HR > dataTrack[((index+1) < dataTrack.Count) ? index + 1 : index].HR).Where(o => o.HR )

            return dataTrack;
        }

        private static string ParseExpression(string expression, IActivity activity, string condition, CalculatedFieldsRow calculatedFieldsRow)
        {
            string field;
            string fieldValue = "";
            string nestedExpressionsHistory = "";

            Dictionary<string, List<ITrailResult>> trails = null;

            string history = "";

            ActivityInfo activityInfoInstance = ActivityInfoCache.Instance.GetInfo(activity);

            //while (fieldPattern.IsMatch(expression))
            while (FindField(expression) != "")
            {
                //field = fieldPattern.Match(expression).Value;
                field = FindField(expression);
                //field = bracketsPattern.Replace(field, "");
                field = Regex.Replace(field, "^{", "");
                field = Regex.Replace(field, "}$", "");
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
                    fieldValue = Formulas(activity, activityInfoInstance, field);
                }

                if (fieldValue == "")
                {
                    fieldValue = LastXDays(activity, activityInfoInstance, condition, field, calculatedFieldsRow);
                }

                if (fieldValue == "")
                {
                    fieldValue = DataTrack(activity, activityInfoInstance, field);
                }

                if (fieldValue == "" || fieldValue == "NaN" || fieldValue == "Infinity")
                {
                    expression = "";
                }

                //expression = fieldPattern.Replace(expression, fieldValue, 1);
                if (expression != "")
                {
                    expression = expression.Replace(FindField(expression), fieldValue);
                }

                history += expression + "-";

                fieldValue = "";
            }

            //throw new Exception(history);

            return expression;
        }

        private static string Formulas(IActivity activity, ActivityInfo activityInfo, string field)
        {
            string fieldValue = "";

            if (field.Contains("HALFSPEEDRATIO"))
            {
                bool onlyActive = field.Contains("ACTIVE");

                var dataTrack = GetDataTrack(activity, onlyActive, false);

                float halfDistance = (dataTrack.Last().Distance - dataTrack.First().Distance) / 2f;
                int firstHalfDistanceElapsed = 0;

                for (int i = 0; i < dataTrack.Count; i++)
                {
                    if (dataTrack[i].Distance >= (dataTrack.First().Distance + halfDistance))
                    {
                        firstHalfDistanceElapsed = i;

                        break;
                    }
                }

                int secondHalfDistanceElapsed = dataTrack.Count - firstHalfDistanceElapsed;

                float firstHalfSpeedRatio = halfDistance / (float)firstHalfDistanceElapsed;
                float secondsHalfSpeedRatio = halfDistance / (float)secondHalfDistanceElapsed;

                fieldValue = ((firstHalfSpeedRatio - secondsHalfSpeedRatio) / firstHalfSpeedRatio).ToString(CultureInfo.InvariantCulture.NumberFormat);
            }
            if (field.Contains("DECOUPLINGRATIO"))
            {
                bool onlyActive = field.Contains("ACTIVE");

                var dataTrack = GetDataTrack(activity, onlyActive, false);

                float halfDistance = (dataTrack.Last().Distance - dataTrack.First().Distance) / 2f;
                int firstHalfDistanceElapsed = 0;

                for (int i = 0; i < dataTrack.Count; i++)
                {
                    if (dataTrack[i].Distance >= (dataTrack.First().Distance + halfDistance))
                    {
                        firstHalfDistanceElapsed = i;

                        break;
                    }
                }

                int secondHalfDistanceElapsed = dataTrack.Count - firstHalfDistanceElapsed;

                float firstHalfSpeedHRRatio = (halfDistance / (float)firstHalfDistanceElapsed) / dataTrack.Where((o, index) => index >= 0 && index <= firstHalfDistanceElapsed).Average(o => o.HR);
                float secondsHalfSpeedHRRatio = (halfDistance / (float)secondHalfDistanceElapsed) / dataTrack.Where((o, index) => index > firstHalfDistanceElapsed).Average(o => o.HR);

                fieldValue = ((firstHalfSpeedHRRatio - secondsHalfSpeedHRRatio) / firstHalfSpeedHRRatio).ToString(CultureInfo.InvariantCulture.NumberFormat);
            }
            if (field.Contains("RANGE"))
            {
                field = ParseExpression(field, activity, "", null);

                bool active = field.StartsWith("ACTIVE");

                string returnType = Regex.Match(field, "(?<=RANGE).*(?=\\()").Value;
                string dataField = Regex.Match(field, "(?<=\\()[a-zA-Z]*(?=,)").Value.ToUpper();
                float lowerBound = Single.Parse(Regex.Match(field, "(?<=,)[0-9.]*(?=,)").Value, CultureInfo.InvariantCulture.NumberFormat);
                float upperBound = Single.Parse(Regex.Match(field, "(?<=,)[0-9.]*(?=\\))").Value, CultureInfo.InvariantCulture.NumberFormat);

                if (dataField != "" && returnType != "")
                {
                    var dataTrack = GetDataTrack(activity, active, false);
                    var query = dataTrack.Where(o => (dataField == "CLIMBSPEED" ? o.ClimbSpeed : (dataField == "ELAPSED" ? o.Elapsed : (dataField == "DISTANCE" ? o.Distance : (dataField == "HR" ? o.HR : (dataField == "PACE" ? o.Pace : (dataField == "SPEED" ? o.Speed : (dataField == "ELEVATION" ? o.Elevation : (dataField == "GRADE" ? o.Grade : (dataField == "CADENCE" ? o.Cadence : (dataField == "POWER" ? o.Power : o.Power)))))))))) >= lowerBound && (dataField == "CLIMBSPEED" ? o.ClimbSpeed : (dataField == "ELAPSED" ? o.Elapsed : (dataField == "DISTANCE" ? o.Distance : (dataField == "HR" ? o.HR : (dataField == "PACE" ? o.Pace : (dataField == "SPEED" ? o.Speed : (dataField == "ELEVATION" ? o.Elevation : (dataField == "GRADE" ? o.Grade : (dataField == "CADENCE" ? o.Cadence : (dataField == "POWER" ? o.Power : o.Power)))))))))) <= upperBound);

                    if (query.Count() != 0)
                    {
                        switch (returnType)
                        {
                            case "ELAPSED":
                                fieldValue = query.Count().ToString(CultureInfo.InvariantCulture.NumberFormat);
                                break;
                            case "DISTANCE":
                                fieldValue = dataTrack.Select((o, index) => new { Field = (dataField == "CLIMBSPEED" ? o.ClimbSpeed : (dataField == "ELAPSED" ? o.Elapsed : (dataField == "DISTANCE" ? o.Distance : (dataField == "HR" ? o.HR : (dataField == "PACE" ? o.Pace : (dataField == "SPEED" ? o.Speed : (dataField == "ELEVATION" ? o.Elevation : (dataField == "GRADE" ? o.Grade : (dataField == "CADENCE" ? o.Cadence : (dataField == "POWER" ? o.Power : o.Power)))))))))), Distance = (dataTrack[((index + 1) < dataTrack.Count) ? index + 1 : index].Distance - o.Distance) }).Where(o => o.Field >= lowerBound && o.Field <= upperBound).Sum(o => o.Distance).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                break;
                            case "HR":
                                fieldValue = query.Average(o => o.HR).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                break;
                            case "PACE":
                                fieldValue = query.Average(o => o.Pace).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                break;
                            case "SPEED":
                                fieldValue = query.Average(o => o.Speed).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                break;
                            case "ELEVATION":
                                fieldValue = query.Average(o => o.Elevation).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                break;
                            case "GRADE":
                                fieldValue = query.Average(o => o.Grade).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                break;
                            case "CADENCE":
                                fieldValue = query.Average(o => o.Cadence).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                break;
                            case "POWER":
                                fieldValue = query.Average(o => o.Power).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                break;
                            case "CLIMBSPEED":
                                fieldValue = query.Average(o => o.ClimbSpeed).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                break;
                        }
                    }
                    else
                    {
                        fieldValue = "null";
                    }
                }
            }

            if (field.Contains("RECOVERYHR"))
            {
                field = ParseExpression(field, activity, "", null);

                int interval = 0;

                interval = Int32.Parse(Regex.Match(field, "(?<=\\()[0-9]*(?=\\))").Value);

                var dataTrack = GetDataTrack(activity, false, true);
                fieldValue = dataTrack.Select((o, index) => new { Elapsed = o.Elapsed, HR = (dataTrack[((index + interval) < dataTrack.Count) ? index + interval : index].HR == 0) ? 0 : o.HR - dataTrack[((index + interval) < dataTrack.Count) ? index + interval : index].HR }).OrderBy(o => o.HR).Last().HR.ToString(CultureInfo.InvariantCulture.NumberFormat);
            }
            if (field.Contains("PEAK"))
            {
                field = ParseExpression(field, activity, "", null);

                string operation = Regex.Match(field, "[a-zA-Z]*(?=PEAK)").Value;
                string peakType = Regex.Match(field, "(?<=PEAK).*(?=\\()").Value;
                string dataField = Regex.Match(field, "(?<=\\()[a-zA-Z]*(?=,)").Value.ToUpper();
                string returnType = Regex.Match(field, "(?<=,)[a-zA-Z]*(?=,)").Value.ToUpper();
                int interval = Int32.Parse(Regex.Match(field, "(?<=,)[0-9]*(?=\\))").Value);

                if (dataField != "" && peakType != "" && interval != 0 && (operation == "MAX" || operation == "MIN"))
                {
                    var dataTrack = GetDataTrack(activity, false, true);
                    float peakValue;
                    int peakStart = 0, peakEnd = 0;

                    if (peakType == "TIME")
                    {
                        if (operation == "MAX")
                        {
                            peakValue = float.MinValue;
                        }
                        else
                        {
                            peakValue = float.MaxValue;
                        }

                        for (int i = 0; i < dataTrack.Count; i++)
                        {
                            for (int j = i; j < dataTrack.Count; j++)
                            {
                                if (dataTrack[j].Elapsed - dataTrack[i].Elapsed >= interval)
                                {
                                    float temp;
                                    bool paused = false;

                                    if (operation == "MAX")
                                    {
                                        temp = float.MinValue;
                                    }
                                    else
                                    {
                                        temp = float.MaxValue;
                                    }

                                    switch (dataField)
                                    {
                                        case "ELAPSED":
                                            temp = dataTrack[j].Elapsed - dataTrack[i].Elapsed;
                                            break;
                                        case "DISTANCE":
                                            temp = dataTrack[j].Distance - dataTrack[i].Distance;
                                            break;
                                        case "HR":
                                            temp = dataTrack.Where((o, index) => index >= i && index <= j).Average(o => o.HR);
                                            break;
                                        case "PACE":
                                            temp = dataTrack.Where((o, index) => index >= i && index <= j).Average(o => o.Pace);
                                            break;
                                        case "SPEED":
                                            temp = dataTrack.Where((o, index) => index >= i && index <= j).Average(o => o.Speed);
                                            break;
                                        case "ELEVATION":
                                            temp = dataTrack.Where((o, index) => index >= i && index <= j).Average(o => o.Elevation);
                                            break;
                                        case "GRADE":
                                            temp = dataTrack.Where((o, index) => index >= i && index <= j).Average(o => o.Grade);
                                            break;
                                        case "CADENCE":
                                            temp = dataTrack.Where((o, index) => index >= i && index <= j).Average(o => o.Cadence);
                                            break;
                                        case "POWER":
                                            temp = dataTrack.Where((o, index) => index >= i && index <= j).Average(o => o.Power);
                                            break;
                                        case "CLIMBSPEED":
                                            temp = dataTrack.Where((o, index) => index >= i && index <= j).Average(o => o.ClimbSpeed);
                                            break;
                                    }

                                    for (int index = i; index <= j; index++)
                                    {
                                        if (dataTrack[index].Pause)
                                        {
                                            paused = true;
                                        }
                                    }

                                    if (operation == "MAX")
                                    {
                                        if (temp > peakValue && !paused)
                                        {
                                            peakValue = temp;
                                            peakStart = i;
                                            peakEnd = j;
                                        }
                                    }
                                    else
                                    {
                                        if (temp < peakValue && !paused)
                                        {
                                            peakValue = temp;
                                            peakStart = i;
                                            peakEnd = j;
                                        }
                                    }

                                    break;
                                }
                            }
                        }

                        if (peakValue != float.MinValue && peakValue != float.MaxValue)
                        {
                            if (returnType != "")
                            {
                                switch (returnType)
                                {
                                    case "ELAPSED":
                                        fieldValue = (dataTrack[peakEnd].Elapsed - dataTrack[peakStart].Elapsed).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                        break;
                                    case "DISTANCE":
                                        fieldValue = (dataTrack[peakEnd].Distance - dataTrack[peakStart].Distance).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                        break;
                                    case "HR":
                                        fieldValue = dataTrack.Where((o, index) => index >= peakStart && index <= peakEnd).Average(o => o.HR).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                        break;
                                    case "PACE":
                                        fieldValue = dataTrack.Where((o, index) => index >= peakStart && index <= peakEnd).Average(o => o.Pace).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                        break;
                                    case "SPEED":
                                        fieldValue = dataTrack.Where((o, index) => index >= peakStart && index <= peakEnd).Average(o => o.Speed).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                        break;
                                    case "ELEVATION":
                                        fieldValue = dataTrack.Where((o, index) => index >= peakStart && index <= peakEnd).Average(o => o.Elevation).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                        break;
                                    case "GRADE":
                                        fieldValue = dataTrack.Where((o, index) => index >= peakStart && index <= peakEnd).Average(o => o.Grade).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                        break;
                                    case "CADENCE":
                                        fieldValue = dataTrack.Where((o, index) => index >= peakStart && index <= peakEnd).Average(o => o.Cadence).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                        break;
                                    case "POWER":
                                        fieldValue = dataTrack.Where((o, index) => index >= peakStart && index <= peakEnd).Average(o => o.Power).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                        break;
                                    case "CLIMBSPEED":
                                        fieldValue = dataTrack.Where((o, index) => index >= peakStart && index <= peakEnd).Average(o => o.ClimbSpeed).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                        break;
                                }
                            }
                            else
                            {
                                fieldValue = peakValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
                            }
                        }
                    }

                    if (peakType == "DISTANCE")
                    {
                        if (operation == "MAX")
                        {
                            peakValue = float.MinValue;
                        }
                        else
                        {
                            peakValue = float.MaxValue;
                        }

                        for (int i = 0; i < dataTrack.Count; i++)
                        {
                            for (int j = i; j < dataTrack.Count; j++)
                            {
                                if (dataTrack[j].Distance - dataTrack[i].Distance >= interval)
                                {
                                    float temp;
                                    bool paused = false;

                                    if (operation == "MAX")
                                    {
                                        temp = float.MinValue;
                                    }
                                    else
                                    {
                                        temp = float.MaxValue;
                                    }

                                    switch (dataField)
                                    {
                                        case "ELAPSED":
                                            temp = dataTrack[j].Elapsed - dataTrack[i].Elapsed;
                                            break;
                                        case "DISTANCE":
                                            temp = dataTrack[j].Distance - dataTrack[i].Distance;
                                            break;
                                        case "HR":
                                            temp = dataTrack.Where((o, index) => index >= i && index <= j).Average(o => o.HR);
                                            break;
                                        case "PACE":
                                            temp = dataTrack.Where((o, index) => index >= i && index <= j).Average(o => o.Pace);
                                            break;
                                        case "SPEED":
                                            temp = dataTrack.Where((o, index) => index >= i && index <= j).Average(o => o.Speed);
                                            break;
                                        case "ELEVATION":
                                            temp = dataTrack.Where((o, index) => index >= i && index <= j).Average(o => o.Elevation);
                                            break;
                                        case "GRADE":
                                            temp = dataTrack.Where((o, index) => index >= i && index <= j).Average(o => o.Grade);
                                            break;
                                        case "CADENCE":
                                            temp = dataTrack.Where((o, index) => index >= i && index <= j).Average(o => o.Cadence);
                                            break;
                                        case "POWER":
                                            temp = dataTrack.Where((o, index) => index >= i && index <= j).Average(o => o.Power);
                                            break;
                                        case "CLIMBSPEED":
                                            temp = dataTrack.Where((o, index) => index >= i && index <= j).Average(o => o.ClimbSpeed);
                                            break;
                                    }

                                    for (int index = i; index <= j; index++)
                                    {
                                        if (dataTrack[index].Pause)
                                        {
                                            paused = true;
                                        }
                                    }

                                    if (operation == "MAX" && !paused)
                                    {
                                        if (temp > peakValue)
                                        {
                                            peakValue = temp;
                                            peakStart = i;
                                            peakEnd = j;
                                        }
                                    }
                                    else
                                    {
                                        if (temp < peakValue && !paused)
                                        {
                                            peakValue = temp;
                                            peakStart = i;
                                            peakEnd = j;
                                        }
                                    }

                                    break;
                                }
                            }
                        }

                        if (peakValue != float.MinValue && peakValue != float.MaxValue)
                        {
                            if (returnType != "")
                            {
                                switch (returnType)
                                {
                                    case "ELAPSED":
                                        fieldValue = (dataTrack[peakEnd].Elapsed - dataTrack[peakStart].Elapsed).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                        break;
                                    case "DISTANCE":
                                        fieldValue = (dataTrack[peakEnd].Distance - dataTrack[peakStart].Distance).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                        break;
                                    case "HR":
                                        fieldValue = dataTrack.Where((o, index) => index >= peakStart && index <= peakEnd).Average(o => o.HR).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                        break;
                                    case "PACE":
                                        fieldValue = dataTrack.Where((o, index) => index >= peakStart && index <= peakEnd).Average(o => o.Pace).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                        break;
                                    case "SPEED":
                                        fieldValue = dataTrack.Where((o, index) => index >= peakStart && index <= peakEnd).Average(o => o.Speed).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                        break;
                                    case "ELEVATION":
                                        fieldValue = dataTrack.Where((o, index) => index >= peakStart && index <= peakEnd).Average(o => o.Elevation).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                        break;
                                    case "GRADE":
                                        fieldValue = dataTrack.Where((o, index) => index >= peakStart && index <= peakEnd).Average(o => o.Grade).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                        break;
                                    case "CADENCE":
                                        fieldValue = dataTrack.Where((o, index) => index >= peakStart && index <= peakEnd).Average(o => o.Cadence).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                        break;
                                    case "POWER":
                                        fieldValue = dataTrack.Where((o, index) => index >= peakStart && index <= peakEnd).Average(o => o.Power).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                        break;
                                    case "CLIMBSPEED":
                                        fieldValue = dataTrack.Where((o, index) => index >= peakStart && index <= peakEnd).Average(o => o.ClimbSpeed).ToString(CultureInfo.InvariantCulture.NumberFormat);
                                        break;
                                }
                            }
                            else
                            {
                                fieldValue = peakValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
                            }
                        }
                    }
                }
            }

            return fieldValue;
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
                    field = ParseExpression(field, activity, "", null);

                    days = Int32.Parse(Regex.Match(field, "(?<=,)[ 0-9]*(?=\\))").Value.Trim());

                    if (aggOperation == "GET")
                    {
                        days++;
                    }

                    DateTime actualActivityDate = (activity.StartTime.ToUniversalTime() + activity.TimeZoneUtcOffset).Date;

                    foreach (var pastActivity in CalculatedFields.GetLogBook().Activities)
                    {
                        DateTime pastActivityDate = (pastActivity.StartTime.ToUniversalTime() + activity.TimeZoneUtcOffset).Date;
                        if (pastActivityDate <= actualActivityDate)
                        {
                            if (actualActivityDate.Subtract(pastActivityDate).Days < days)
                            {
                                object conditionResult = "True";

                                if (condition != "")
                                {
                                    conditionResult = Evaluate(condition, pastActivity, "", calculatedFieldsRow);
                                }

                                if (conditionResult.ToString() == "True")
                                {

                                    //if (pastActivity.StartTime.Date.Day == 1)
                                    //{
                                    //    throw new Exception(pastActivity.StartTime.Date.ToString() + "-" + actualActivityDate.ToString() + "-" + activity.StartTime.ToString() + "-" + days.ToString() + "-" + actualActivityDate.Subtract(pastActivity.StartTime.Date).Days.ToString());
                                    //}
                                    count++;

                                    switch (aggOperation)
                                    {
                                        case "GET":
                                            if (actualActivityDate.Subtract(pastActivityDate).Days == (days - 1))
                                            {
                                                result = double.Parse(
                                                    Evaluate("{" + aggField + "}", pastActivity, condition, calculatedFieldsRow).ToString());
                                            }
                                            break;
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
                        case "COUNT":
                            if (count != 0)
                            {
                                result = count;
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
                    if (activity.GetCustomDataValue(definition) == null)
                    {
                        fieldValue = "null";
                    }
                    else
                    {
                        if (definition.DataType.Id == CustomDataFieldDefinitions.StandardDataTypes.NumberDataTypeId)
                        {
                            fieldValue = Double.Parse(activity.GetCustomDataValue(definition).ToString()).ToString(CultureInfo.InvariantCulture.NumberFormat);
                        }
                        else if (definition.DataType.Id == CustomDataFieldDefinitions.StandardDataTypes.TextDataTypeId)
                        {
                            fieldValue = activity.GetCustomDataValue(definition).ToString().Escape();
                        }
                        else if (definition.DataType.Id == CustomDataFieldDefinitions.StandardDataTypes.TimeSpanDataTypeId)
                        {
                            fieldValue = Double.Parse(activity.GetCustomDataValue(definition).ToString()).ToString(CultureInfo.InvariantCulture.NumberFormat);
                        }
                    }
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

        private static string DataTrack(IActivity activity, ActivityInfo activityInfoInstance, string field)
        {
            string fieldValue = "";

            if (field == "DATATRACK")
            {
                fieldValue = "DATATRACK";
            }
            else if (field == "DATATRACKWITHPAUSES")
            {
                fieldValue = "DATATRACKWITHPAUSES";
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
                    fieldValue = "\"" + athleteEntry.DiaryText.Escape() + "\"";
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
                    fieldValue = "\"" + athleteEntry.SickText.Escape() + "\"";
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
                    if (athleteEntry.GetCustomDataValue(definition) != null)
                    {
                        if (athleteEntry.GetCustomDataValue(definition) == null)
                        {
                            fieldValue = "null";
                        }
                        else
                        {
                            if (definition.DataType.Id == CustomDataFieldDefinitions.StandardDataTypes.NumberDataTypeId)
                            {
                                fieldValue = Double.Parse(athleteEntry.GetCustomDataValue(definition).ToString()).ToString(CultureInfo.InvariantCulture.NumberFormat);
                            }
                            else if (definition.DataType.Id == CustomDataFieldDefinitions.StandardDataTypes.TextDataTypeId)
                            {
                                fieldValue = athleteEntry.GetCustomDataValue(definition).ToString().Escape();
                            }
                            else if (definition.DataType.Id == CustomDataFieldDefinitions.StandardDataTypes.TimeSpanDataTypeId)
                            {
                                fieldValue = Double.Parse(athleteEntry.GetCustomDataValue(definition).ToString()).ToString(CultureInfo.InvariantCulture.NumberFormat);
                            }
                        }
                    }
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
                    List<string> categories = new List<string>();
                    categories.Add(activity.Category.Name);
                    
                    var parent = activity.Category.Parent;
                    while (parent != null)
                    {
                        categories.Add(parent.Name);
                        parent = parent.Parent;
                    }

                    categories.Reverse();
                    fieldValue = "\"";
                    for(int i = 0; i < categories.Count; i++)
                    {
                        if (i != 0)
                        {
                            fieldValue += ": ";
                        }

                        fieldValue += categories[i];
                    }

                    fieldValue += "\"";
                    break;
                case "TEMPERATURE":
                    fieldValue = activity.Weather.TemperatureCelsius.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "WEATHERNOTES":
                    fieldValue = "\"" + activity.Weather.ConditionsText.Escape() + "\"";
                    break;
                case "LOCATION":
                    fieldValue = "\"" + activity.Location.Escape() + "\"";
                    break;
                case "NAME":
                    fieldValue = "\"" + activity.Name.Escape() + "\"";
                    break;
                case "DATETIMETICKS":
                    fieldValue = "\"" + (activity.StartTime.ToUniversalTime() + activity.TimeZoneUtcOffset).Ticks.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "DATETIME":
                    fieldValue = "\"" + (activity.StartTime.ToUniversalTime() + activity.TimeZoneUtcOffset).ToString() + "\"";
                    break;
                case "DATETICKS":
                    fieldValue = "\"" + (activity.StartTime.ToUniversalTime() + activity.TimeZoneUtcOffset).Date.Ticks.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "DATE":
                    fieldValue = "\"" + (activity.StartTime.ToUniversalTime() + activity.TimeZoneUtcOffset).ToShortDateString() + "\"";
                    break;
                case "NOTES":
                    fieldValue = "\"" + activity.Notes.Escape() + "\"";
                    break;
                case "INTENSITY":
                    fieldValue = activity.Intensity.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "HASGPSTRACK":
                    fieldValue = (activity.GPSRoute == null) ? "false" : "true";
                    break;
                case "HASHRTRACK":
                    fieldValue = (activity.HeartRatePerMinuteTrack == null) ? "false" : "true";
                    break;
                case "HASELEVATIONTRACK":
                    fieldValue = (activity.ElevationMetersTrack == null) ? "false" : "true";
                    break;
                case "HASCADENCETRACK":
                    fieldValue = (activity.CadencePerMinuteTrack == null) ? "false" : "true";
                    break;
                case "HASPOWERTRACK":
                    fieldValue = (activity.PowerWattsTrack == null) ? "false" : "true";
                    break;



                //totals)
                case "TIME":
                    fieldValue = activityInfoInstance.Time.TotalSeconds.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "HALFTIME":
                    fieldValue = (activityInfoInstance.Time.TotalSeconds / 2f).ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "DISTANCE":
                    fieldValue = activityInfoInstance.DistanceMeters.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "HALFDISTANCE":
                    fieldValue = (activityInfoInstance.DistanceMeters / 2f).ToString(CultureInfo.InvariantCulture.NumberFormat);
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

                // active
                case "ACTIVETIME":
                    fieldValue = activityInfoInstance.ActiveLapsTotalDetail.LapElapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "ACTIVEHALFTIME":
                    fieldValue = (activityInfoInstance.ActiveLapsTotalDetail.LapElapsed.TotalSeconds / 2f).ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "ACTIVEDISTANCE":
                    fieldValue = activityInfoInstance.ActiveLapsTotalDetail.LapDistanceMeters.ToString(CultureInfo.InvariantCulture.NumberFormat);
                    break;
                case "ACTIVEHALFDISTANCE":
                    fieldValue = (activityInfoInstance.ActiveLapsTotalDetail.LapDistanceMeters / 2f).ToString(CultureInfo.InvariantCulture.NumberFormat);
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

                // rest
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
                field = ParseExpression(field, activity, "", null);

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
                            fieldValue = "\"" + split.Notes.Escape() + "\"";
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

        private static string TrailsFields(IActivity activity, ActivityInfo activityInfoInstance, string field, Dictionary<string, List<ITrailResult>> trails)
        {
            string fieldValue = "";

            if (field.StartsWith("TRAIL"))
            {
                if (!Trails.TestIntegration())
                {
                    return "";
                }

                field = ParseExpression(field, activity, "", null);

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
                                fieldValue = trail[trailSplit - 1].AvgPace.ToString(CultureInfo.InvariantCulture.NumberFormat);
                                break;
                            case "DISTANCE":
                                fieldValue = Double.Parse(trail[trailSplit - 1].Distance).ToString(CultureInfo.InvariantCulture.NumberFormat);
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
                                fieldValue = Double.Parse(trail[trailSplit - 1].ElevChg).ToString(CultureInfo.InvariantCulture.NumberFormat);
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