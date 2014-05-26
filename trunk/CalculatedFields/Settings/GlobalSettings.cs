namespace CalculatedFields
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    using ZoneFiveSoftware.Common.Data.Fitness;

    [Serializable]
    public class DataTrackPoint
    {
        int lapNumber;
        string lapNote;
        bool lapActive;
        float hr;
        float pace;
        float speed;
        float elevation;
        float grade;
        float cadence;
        float power;
        float elapsed;
        float distance;
        float climbSpeed;
        float verticalOscillation;
        float groundContact;
        bool pause;

        public float Distance { get { return distance; } set { distance = value; } }
        public float HR { get { return hr; } set { hr = value; } }
        public float Pace { get { return pace; } set { pace = value; } }
        public float Speed { get { return speed; } set { speed = value; } }
        public float Elevation { get { return elevation; } set { elevation = value; } }
        public float Grade { get { return grade; } set { grade = value; } }
        public float Cadence { get { return cadence; } set { cadence = value; } }
        public float Power { get { return power; } set { power = value; } }
        public float Elapsed { get { return elapsed; } set { elapsed = value; } }
        public float ClimbSpeed { get { return climbSpeed; } set { climbSpeed = value; } }
        public float VerticalOscillation { get { return verticalOscillation; } set { verticalOscillation = value; } }
        public float GroundContact { get { return groundContact; } set { groundContact = value; } }
        public bool Pause { get { return pause; } set { pause = value; } }
        public int LapNumber { get { return lapNumber; } set { lapNumber = value; } }
        public string LapNote { get { return lapNote; } set { lapNote = value; } }
        public bool LapActive { get { return lapActive; } set { lapActive = value; } }

        public DataTrackPoint()
        {
        }

        public DataTrackPoint(
            int lapNumber,
            string lapNote,
            bool lapActive,
            float distance,
            float hr,
            float pace,
            float speed,
            float elevation,
            float grade,
            float cadence,
            float power,
            float elapsed,
            float climbSpeed,
            float verticalOscillation,
            float groundContact,
            bool pause)
        {
            LapNumber = lapNumber;
            LapNote = lapNote;
            LapActive = lapActive;
            Distance = distance;
            HR = hr;
            Pace = pace;
            Speed = speed;
            Elevation = elevation;
            Grade = grade;
            Cadence = cadence;
            Power = power;
            Elapsed = elapsed;
            ClimbSpeed = climbSpeed;
            VerticalOscillation = verticalOscillation;
            GroundContact = groundContact;
            Pause = pause;
        }
    }

    public class CalculatedFieldsRow: IComparable<CalculatedFieldsRow>
    {
        public string ID { get; set; }

        public string CustomField { get; set; }

        public string CalculatedExpression { get; set; }

        public string Condition { get; set; }

        public string Active { get; set; }

        public int SmoothingPace { get; set; }
        public int SmoothingElevation { get; set; }
        public int SmoothingHR { get; set; }
        public int SmoothingCadence { get; set; }
        public int SmoothingPower { get; set; }

        public CalculatedFieldsRow(string id, string customField, string calculatedExpression, string condition, string active, int smoothingPace, int smoothingElevation, int smoothingHR, int smoothingCadence, int smoothingPower)
        {
            ID = id;
            CustomField = customField;
            CalculatedExpression = calculatedExpression;
            Condition = condition;

            if (active.ToUpper() == "TRUE" || active == "Y")
            {
                Active = "Y";
            }
            else
            {
                Active = "N";
            }

            SmoothingPace = smoothingPace;
            SmoothingElevation = smoothingElevation;
            SmoothingHR = smoothingHR;
            SmoothingCadence = smoothingCadence;
            SmoothingPower = smoothingPower;
        }

        #region Implementation of IComparable<CalculatedFieldsRow>

        public int CompareTo(CalculatedFieldsRow other)
        {
            return this.CustomField.CompareTo(other.CustomField);
        }

        #endregion
    }

    public class NestedFieldsRow: IComparable<NestedFieldsRow>
    {
        public string ID { get; set; }

        public string NestedExpression { get; set; }

        public string Expression { get; set; }

        public NestedFieldsRow(string id, string nestedExpressionName, string expression)
        {
            ID = id;
            NestedExpression = nestedExpressionName;
            Expression = expression;
        }

        #region Implementation of IComparable<NestedFieldsRow>

        public int CompareTo(NestedFieldsRow other)
        {
            return this.NestedExpression.CompareTo(other.NestedExpression);
        }

        #endregion
    }

    public static class GlobalSettings
    {
        #region Constants and Fields

        public static List<CalculatedFieldsRow> calculatedFieldsRows = new List<CalculatedFieldsRow>();
        public static List<NestedFieldsRow> nestedFieldsRows = new List<NestedFieldsRow>();
        public static List<CalculatedFieldsRow> virtualFieldsRows = new List<CalculatedFieldsRow>();

        public static bool runAfterImport;
        public static bool calculateFutureAfterImport;
        public static int dataTrackResolution = 1000;

        private static readonly string path;

        #endregion

        #region Constructors and Destructors

        static GlobalSettings()
        {
            path = Environment.GetEnvironmentVariable("APPDATA") + "/CalculatedFieldsPlugin/preferences.xml";

            Directory.CreateDirectory(Environment.GetEnvironmentVariable("APPDATA") + "/CalculatedFieldsPlugin/");
        }

        #endregion

        #region Public Methods

        public static void LoadSettings()
        {
            string id, active, customField, expression, condition, nestedName, nestedExpression, virtualField;
            int smoothingPace, smoothingElevation, smoothingHR, smoothingCadence, smoothingPower;

            if (!File.Exists(path))
            {
                return;
            }

            calculatedFieldsRows.Clear();
            nestedFieldsRows.Clear();
            virtualFieldsRows.Clear();

            var document = new XmlDocument();
            XmlReader reader = new XmlTextReader(path);

            try
            {
                document.Load(reader);
                runAfterImport = (document.ChildNodes[0].Attributes["RunAfterImport"] != null) ? Boolean.Parse(document.ChildNodes[0].Attributes["RunAfterImport"].Value) : false;
                calculateFutureAfterImport = (document.ChildNodes[0].Attributes["CalculateFutureAfterImport"] != null) ? Boolean.Parse(document.ChildNodes[0].Attributes["CalculateFutureAfterImport"].Value) : false;
                dataTrackResolution = (document.ChildNodes[0].Attributes["DataTrackResolution"] != null) ? int.Parse(document.ChildNodes[0].Attributes["DataTrackResolution"].Value) : 1000;

                XmlNodeList rowsNode = null;
                XmlNodeList nestedRowsNode = null;
                XmlNodeList virtualRowsNode = null;
                
                foreach (XmlNode node in document.ChildNodes[0].ChildNodes)
                {
                    if (node.Name == "Rows")
                    {
                        rowsNode = node.ChildNodes;
                    }
                    if (node.Name == "NestedRows")
                    {
                        nestedRowsNode = node.ChildNodes;
                    }
                    if (node.Name == "VirtualRows")
                    {
                        virtualRowsNode = node.ChildNodes;
                    }
                }

                if (rowsNode != null)
                {
                    foreach (XmlNode node in rowsNode)
                    {
                        id = (node.Attributes["ID"] != null) ? node.Attributes["ID"].Value : Guid.NewGuid().ToString();
                        customField = (node.Attributes["CustomField"] != null) ? node.Attributes["CustomField"].Value : "";
                        expression = (node.Attributes["Expression"] != null) ? node.Attributes["Expression"].Value : "";
                        condition = (node.Attributes["Condition"] != null) ? node.Attributes["Condition"].Value : "";
                        active = (node.Attributes["Active"] != null) ? node.Attributes["Active"].Value : "Y";
                        smoothingPace = (node.Attributes["SmoothingPace"] != null) ? int.Parse(node.Attributes["SmoothingPace"].Value) : 0;
                        smoothingElevation = (node.Attributes["SmoothingElevation"] != null) ? int.Parse(node.Attributes["SmoothingElevation"].Value) : 0;
                        smoothingHR = (node.Attributes["SmoothingHR"] != null) ? int.Parse(node.Attributes["SmoothingHR"].Value) : 0;
                        smoothingCadence = (node.Attributes["SmoothingCadence"] != null) ? int.Parse(node.Attributes["SmoothingCadence"].Value) : 0;
                        smoothingPower = (node.Attributes["SmoothingPower"] != null) ? int.Parse(node.Attributes["SmoothingPower"].Value) : 0;

                        calculatedFieldsRows.Add(new CalculatedFieldsRow(id, customField, expression, condition, active, smoothingPace, smoothingElevation, smoothingHR, smoothingCadence, smoothingPower));
                    }
                }

                if (nestedRowsNode != null)
                {
                    foreach (XmlNode node in nestedRowsNode)
                    {
                        id = (node.Attributes["ID"] != null) ? node.Attributes["ID"].Value : Guid.NewGuid().ToString();
                        nestedName = (node.Attributes["NestedExpressionName"] != null) ? node.Attributes["NestedExpressionName"].Value : "";
                        nestedExpression = (node.Attributes["Expression"] != null) ? node.Attributes["Expression"].Value : "";

                        nestedFieldsRows.Add(new NestedFieldsRow(id, nestedName, nestedExpression));
                    }
                }

                if (virtualRowsNode != null)
                {
                    foreach (XmlNode node in virtualRowsNode)
                    {
                        id = (node.Attributes["ID"] != null) ? node.Attributes["ID"].Value : Guid.NewGuid().ToString();
                        virtualField = (node.Attributes["CustomField"] != null) ? node.Attributes["CustomField"].Value : "";
                        expression = (node.Attributes["Expression"] != null) ? node.Attributes["Expression"].Value : "";
                        condition = (node.Attributes["Condition"] != null) ? node.Attributes["Condition"].Value : "";
                        active = (node.Attributes["Active"] != null) ? node.Attributes["Active"].Value : "Y";
                        smoothingPace = (node.Attributes["SmoothingPace"] != null) ? int.Parse(node.Attributes["SmoothingPace"].Value) : 0;
                        smoothingElevation = (node.Attributes["SmoothingElevation"] != null) ? int.Parse(node.Attributes["SmoothingElevation"].Value) : 0;
                        smoothingHR = (node.Attributes["SmoothingHR"] != null) ? int.Parse(node.Attributes["SmoothingHR"].Value) : 0;
                        smoothingCadence = (node.Attributes["SmoothingCadence"] != null) ? int.Parse(node.Attributes["SmoothingCadence"].Value) : 0;
                        smoothingPower = (node.Attributes["SmoothingPower"] != null) ? int.Parse(node.Attributes["SmoothingPower"].Value) : 0;

                        virtualFieldsRows.Add(new CalculatedFieldsRow(id, virtualField, expression, condition, active, smoothingPace, smoothingElevation, smoothingHR, smoothingCadence, smoothingPower));
                    }
                }
            }
            catch (Exception)
            {
                reader.Close();
            }
            reader.Close();
        }

        public static void SaveSettings()
        {
            var document = new XmlDocument();
            XmlElement calculatedFieldsElement = document.CreateElement("CalculatedFields");
            calculatedFieldsElement.SetAttribute("RunAfterImport", runAfterImport.ToString());
            calculatedFieldsElement.SetAttribute("CalculateFutureAfterImport", calculateFutureAfterImport.ToString());
            calculatedFieldsElement.SetAttribute("DataTrackResolution", dataTrackResolution.ToString());

            document.AppendChild(calculatedFieldsElement);

            XmlElement rowsElement = document.CreateElement("Rows");
            calculatedFieldsElement.AppendChild(rowsElement);

            foreach (var row in calculatedFieldsRows)
            {
                XmlElement rowElement = document.CreateElement("Row");
                rowsElement.AppendChild(rowElement);
                rowElement.SetAttribute("ID", row.ID);
                rowElement.SetAttribute("CustomField", row.CustomField);
                rowElement.SetAttribute("Expression", row.CalculatedExpression);
                rowElement.SetAttribute("Condition", row.Condition);
                rowElement.SetAttribute("Active", row.Active);

                rowElement.SetAttribute("SmoothingPace", row.SmoothingPace.ToString());
                rowElement.SetAttribute("SmoothingElevation", row.SmoothingElevation.ToString());
                rowElement.SetAttribute("SmoothingHR", row.SmoothingHR.ToString());
                rowElement.SetAttribute("SmoothingCadence", row.SmoothingCadence.ToString());
                rowElement.SetAttribute("SmoothingPower", row.SmoothingPower.ToString());
            }

            XmlElement nestedRowsElement = document.CreateElement("NestedRows");
            calculatedFieldsElement.AppendChild(nestedRowsElement);

            foreach (var row in nestedFieldsRows)
            {
                XmlElement rowElement = document.CreateElement("Row");
                nestedRowsElement.AppendChild(rowElement);
                rowElement.SetAttribute("ID", row.ID);
                rowElement.SetAttribute("NestedExpressionName", row.NestedExpression);
                rowElement.SetAttribute("Expression", row.Expression);
            }

            XmlElement virtualRowsElement = document.CreateElement("VirtualRows");
            calculatedFieldsElement.AppendChild(virtualRowsElement);

            foreach (var row in virtualFieldsRows)
            {
                XmlElement rowElement = document.CreateElement("Row");
                virtualRowsElement.AppendChild(rowElement);
                rowElement.SetAttribute("ID", row.ID);
                rowElement.SetAttribute("CustomField", row.CustomField);
                rowElement.SetAttribute("Expression", row.CalculatedExpression);
                rowElement.SetAttribute("Condition", row.Condition);
                rowElement.SetAttribute("Active", row.Active);

                rowElement.SetAttribute("SmoothingPace", row.SmoothingPace.ToString());
                rowElement.SetAttribute("SmoothingElevation", row.SmoothingElevation.ToString());
                rowElement.SetAttribute("SmoothingHR", row.SmoothingHR.ToString());
                rowElement.SetAttribute("SmoothingCadence", row.SmoothingCadence.ToString());
                rowElement.SetAttribute("SmoothingPower", row.SmoothingPower.ToString());
            }

            var w = new XmlTextWriter(path, Encoding.UTF8);
            w.Formatting = Formatting.Indented;
            w.Indentation = 3;
            w.IndentChar = ' ';
            document.WriteContentTo(w);
            w.Close();
        }

        #endregion
    }
}