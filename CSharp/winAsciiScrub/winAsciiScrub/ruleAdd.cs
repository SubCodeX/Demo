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
    public partial class ruleAdd : Form
    {
        toolsASCII t;

        public formFileProcessing parent;

        private List<string> charSelectionInfo;
        private List<string> ruleSelectionInfo;

        private string[] charSelectionOptions;
        private string[] ruleSelectionOptions;

        public ruleAdd()
        {
            InitializeComponent();
            t = new toolsASCII();
            charSelectionInfo = new List<string>();
            ruleSelectionInfo = new List<string>();
        }

        private void ruleAdd_Load(object sender, EventArgs e)
        {

        }

        public void loadLists()
        {
            

            charSelectionInfo.Add("  0 -  31 (0x00 - 0x1F)"); 
            charSelectionInfo.Add("128 - 255 (0x7F - 0xFF)");
            ruleSelectionInfo.Add("Delete");
            ruleSelectionInfo.Add("Space");

            for (int i = 0; i < 256; i++)
            {
                string tStr = t.getEASCIIChar(i);
                charSelectionInfo.Add("0x" + i.ToString("X2") + "     |     " + tStr);
                ruleSelectionInfo.Add("0x" + i.ToString("X2") + "     |     " + tStr);
            }

            charSelectionOptions = charSelectionInfo.ToArray();
            ruleSelectionOptions = ruleSelectionInfo.ToArray();
            charList.DataSource = charSelectionOptions;
            substList.DataSource = ruleSelectionOptions;
        }

        private void addSubstitutionRule()
        {
            if (charList.SelectedIndex == 0)
            {
                for (int i = 0; i <= 31; i++)
                {
                    if (substList.SelectedIndex == 0)
                    {
                        //parent.data.subst[i].substChar.Add(255);
                        parent.data.subst[i].substChar.Clear();
                    }

                    if (substList.SelectedIndex == 1)
                    {
                        //parent.data.subst[i].substChar.Add(255);
                        parent.data.subst[i].substChar.Clear();
                        parent.data.subst[i].substChar.Add(32); // Sets ASCII character to Space
                    }

                    if (substList.SelectedIndex >= 2)
                    {
                        //parent.data.subst[i].substChar.Add(255);
                        parent.data.subst[i].substChar.Clear();
                        parent.data.subst[i].substChar.Add((byte)(substList.SelectedIndex - 2));
                    }
                    Console.WriteLine(i + " >> " + substList.SelectedIndex);
                }
            }

            if (charList.SelectedIndex == 1)
            {
                for (int i = 128; i <= 255; i++)
                {
                    if (substList.SelectedIndex == 0)
                    {
                        //parent.data.subst[i].substChar.Add(255);
                        parent.data.subst[i].substChar.Clear();
                    }

                    if (substList.SelectedIndex == 1)
                    {
                        //parent.data.subst[i].substChar.Add(255);
                        parent.data.subst[i].substChar.Clear();
                        parent.data.subst[i].substChar.Add(32);
                    }

                    if (substList.SelectedIndex >= 2)
                    {
                        //parent.data.subst[i].substChar.Add(255);
                        parent.data.subst[i].substChar.Clear();
                        parent.data.subst[i].substChar.Add((byte)(substList.SelectedIndex - 2));
                    }
                    Console.WriteLine(i + " >> " + substList.SelectedIndex);
                }
            }

            if (charList.SelectedIndex >= 2)
            {
                int selectedIndex = charList.SelectedIndex - 2;
                if (selectedIndex < 256)
                {
                    if (substList.SelectedIndex == 0)
                    {
                        //parent.data.subst[selectedIndex].substChar.Add(255);
                        parent.data.subst[selectedIndex].substChar.Clear();
                    }

                    if (substList.SelectedIndex == 1)
                    {
                        //parent.data.subst[selectedIndex].substChar.Add(255);
                        parent.data.subst[selectedIndex].substChar.Clear();
                        parent.data.subst[selectedIndex].substChar.Add(32);
                    }

                    if (substList.SelectedIndex >= 2)
                    {
                        //parent.data.subst[selectedIndex].substChar.Add(255);
                        parent.data.subst[selectedIndex].substChar.Clear();
                        parent.data.subst[selectedIndex].substChar.Add((byte)(substList.SelectedIndex - 2));
                    }
                }
                Console.WriteLine(charList.SelectedIndex + " >> " + substList.SelectedIndex);
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            addSubstitutionRule();
            this.Close();
        }

        private void Cansel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
