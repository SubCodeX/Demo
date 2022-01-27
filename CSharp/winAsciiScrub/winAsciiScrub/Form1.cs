using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winAsciiScrub
{
    public partial class mainWindow : Form
    {
        formFileProcessing fileForProcessing;
        public mainWindow()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(mainWindow_DragEnter);
            this.DragDrop += new DragEventHandler(mainWindow_DragDrop);
        }

        void mainWindow_DragEnter(object sender, DragEventArgs e) {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        void mainWindow_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files) Console.WriteLine(file);
            Console.WriteLine("Only " + files[0] + "will be processed");

            fileForProcessing = new formFileProcessing();
            fileForProcessing.FormClosed += new FormClosedEventHandler(formFileProcessing_Closed);
            fileForProcessing.setFileForProcessing(files[0]);
            fileForProcessing.listCharsFound();
            fileForProcessing.listSubstRules();
            fileForProcessing.Show();
        }

        private void formFileProcessing_Closed(object sender, EventArgs e)
        {
            fileForProcessing.Dispose();
        }
    

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Load file for processing";
            ofd.ShowDialog();
            if (ofd.FileName != "")
            {
                fileForProcessing = new formFileProcessing();
                fileForProcessing.FormClosed += new FormClosedEventHandler(formFileProcessing_Closed);
                fileForProcessing.setFileForProcessing(ofd.FileName);
                fileForProcessing.listCharsFound();
                fileForProcessing.listSubstRules();
                fileForProcessing.Show();
                fileForProcessing.Dispose();
            }
        }
    }
}
