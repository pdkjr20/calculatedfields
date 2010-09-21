using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CalculatedFields.UI
{
    public partial class ProgressBarForm : Form
    {
        public bool Cancelled { get; set; }

        public float ProgressBarValue
        {
            get
            {
                return progressBar.Value;
            }
            set
            {
                this.progressBar.Value = (int)value;
                this.Refresh();
            }
        }

        public string ProgressBarLabelText
        {
            set
            {
                this.labelProgressBar.Text = value;
            }
        }
 
        public ProgressBarForm()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Cancelled = true;
        }
    }
}
