using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LargeFileSync_GD
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string[] lines = { txtMyContentFileLocation.Text };
            File.WriteAllLines(@"ContentFolderLocation.txt", lines);
            this.Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            txtMyContentFileLocation.Text = File.ReadAllLines(@"ContentFolderLocation.txt")[0];
        }
    }
}
