namespace CalculatedFields
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class CalculatedFieldsRow: IComparable<CalculatedFieldsRow>
    {
        public string ID { get; set; }

        public string CustomField { get; set; }

        public string CalculatedExpression { get; set; }

        public string Condition { get; set; }

        public string Active { get; set; }

        public int CompilationTime { get; set; }

        public int ParsingTime { get; set; }

        public CalculatedFieldsRow(string id, string customField, string calculatedExpression, string condition, string active)
        {
            ID = id;
            CustomField = customField;
            CalculatedExpression = calculatedExpression;
            Condition = condition;

            CompilationTime = 0;
            ParsingTime = 0;

            if (active.ToUpper() == "TRUE" || active == "Y")
            {
                Active = "Y";
            }
            else
            {
                Active = "N";
            }
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

        public static bool runAfterImport;

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
            string id, active, customField, expression, condition, nestedName, nestedExpression;

            if (!File.Exists(path))
            {
                return;
            }

            calculatedFieldsRows.Clear();
            nestedFieldsRows.Clear();

            var document = new XmlDocument();
            XmlReader reader = new XmlTextReader(path);

            try
            {
                document.Load(reader);
                runAfterImport = Boolean.Parse(document.ChildNodes[0].Attributes["RunAfterImport"].Value);

                XmlNodeList rowsNode = document.ChildNodes[0].FirstChild.ChildNodes;

                foreach (XmlNode node in rowsNode)
                {
                    id = (node.Attributes["ID"] != null) ? node.Attributes["ID"].Value : Guid.NewGuid().ToString(); 
                    customField = (node.Attributes["CustomField"] != null) ? node.Attributes["CustomField"].Value : "";
                    expression = (node.Attributes["Expression"] != null) ? node.Attributes["Expression"].Value : "";
                    condition = (node.Attributes["Condition"] != null) ? node.Attributes["Condition"].Value : "";
                    active = (node.Attributes["Active"] != null) ? node.Attributes["Active"].Value : "Y";

                    calculatedFieldsRows.Add(new CalculatedFieldsRow(id, customField, expression, condition, active));
                }

                XmlNodeList nestedRowsNode = document.ChildNodes[0].LastChild.ChildNodes;


                foreach (XmlNode node in nestedRowsNode)
                {
                    id = (node.Attributes["ID"] != null) ? node.Attributes["ID"].Value : Guid.NewGuid().ToString();
                    nestedName = (node.Attributes["NestedExpressionName"] != null) ? node.Attributes["NestedExpressionName"].Value : "";
                    nestedExpression = (node.Attributes["Expression"] != null) ? node.Attributes["Expression"].Value : "";

                    nestedFieldsRows.Add(new NestedFieldsRow(id, nestedName, nestedExpression));
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