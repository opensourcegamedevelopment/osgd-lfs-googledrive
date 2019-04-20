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
        Form1 frm1;
        public SettingsForm(Form1 parent)
        {
            InitializeComponent();
            frm1 = parent;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string[] lines = { txtMyContentFileLocation.Text };
            File.WriteAllLines(@"ContentFolderLocation.txt", lines);
            

            string[] lines2 = { txtProjectName.Text };
            File.WriteAllLines(@"ProjectName.txt", lines2);
            

            frm1.updateTxtContentFolderLocation(lines[0]);
            frm1.updateTxtProjectName(lines2[0]);

            this.Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            txtMyContentFileLocation.Text = File.ReadAllLines(@"ContentFolderLocation.txt")[0];
            txtProjectName.Text = File.ReadAllLines(@"ProjectName.txt")[0];
        }
    }
}
