using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shadow_Text_Editor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            WebClient webClient = new WebClient();

            try
            {
                if (!webClient.DownloadString("https://pastebin.com/raw/qwhKxdyX").Contains("1.0"))
                {
                    if (MessageBox.Show("There is an update available! Do you want to download it?", "Shadow Text Editor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) using(var client = new WebClient())
                    {
                        Process.Start("UpdaterSTE");
                        this.Close();
                    }
                }
            }
            catch
            {

            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void fileToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if(e.ClickedItem.Name == "openToolStripMenuItem")
            {
                BeginInvoke(new Action(() =>
                {
                    if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        lblFileName.Text = ofd.FileName;
                        txbEditor.Text = File.ReadAllText(ofd.FileName);

                        if (chkDebug.Checked)
                        {
                            MessageBox.Show(ofd.FileName, "Shadow Text Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }));
            }

            if (e.ClickedItem.Name == "saveAsToolStripMenuItem")
            {
                sfd.FileName = ofd.FileName;

                BeginInvoke(new Action(() =>
                {
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        StreamWriter write = new StreamWriter(File.Create(sfd.FileName));

                        write.Write(txbEditor.Text);
                        write.Dispose();
                    }
                }));
            }
        }

        private void chkProtected_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProtected.Checked)
            {
                txbEditor.Multiline = false;
                txbEditor.UseSystemPasswordChar = true;
                this.Size = new System.Drawing.Size(818, 153);
            }
            else
            {
                txbEditor.Multiline = true;
                txbEditor.UseSystemPasswordChar = false;
                this.Size = new System.Drawing.Size(818, 469);
            }
        }
    }
}
