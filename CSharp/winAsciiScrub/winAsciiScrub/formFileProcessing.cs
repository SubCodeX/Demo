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

namespace winAsciiScrub
{
   

    public partial class formFileProcessing : Form
    {
        toolsASCII t;

        public fileData data;
        private string[] listData;
        private string[] listRules;
        private List<string> listRulesDef;
        public formFileProcessing()
        {
            t = new toolsASCII();
            InitializeComponent();
            Console.WriteLine("File Processing Form instantiated");
            listData = new string[256];
            listRulesDef = new List<string>();             
        }
        ~formFileProcessing()
        {
            data.Data = null;
            data = null;
            listData = null;
            listRules = null;
            listRulesDef.Clear();
        }

        private void formFileProcessing_Load(object sender, EventArgs e){}
        public void setFileForProcessing(string filePath)
        {
            Console.WriteLine("Loading file for processing");
            data = new fileData(filePath);
            fileInfoBox.AppendText("File Name : " + data.filePath + Environment.NewLine);
            fileInfoBox.AppendText("File Size : " + data.fileSize + " Bytes" + Environment.NewLine);
        }
        public void listCharsFound()
        {
            for (int i = 0; i < 256; i++)
            {
                string HEX = "0x" + i.ToString("X2");
                string DEC = i.ToString("D3");
                long cCount = data.subst[i].count;
                string CNT = "";
                if (cCount == 0)
                {
                    CNT = "__________";
                }
                else
                {
                    CNT = data.subst[i].count.ToString().PadLeft(10, '_');
                }
                string CHR = t.getEASCIICharAbbr(i);
                string STR = DEC + " | " + HEX + " | " + CNT + " | " + CHR;
                listData[i] = STR;                
            }
            oCharSetList.DataSource = listData;
        }
        public void listSubstRules()
        {
            listRulesDef.Clear();
            for (int i = 0; i < 256; i++)
            {
                if (data.subst[i].substChar.Count() == 0)
                {
                    listRulesDef.Add(t.getEASCIIChar(i) + " >> " + "Delete");                    
                }

                if (data.subst[i].substChar.Count() > 0)
                {                    
                    if (data.subst[i].substChar[0] != i)
                    {
                        string o = t.getEASCIIChar(i);
                        string s = "";
                        foreach (byte c in data.subst[i].substChar)
                        {
                            s += t.getEASCIIChar(c);
                        }
                        listRulesDef.Add(o + " >> " + s);
                    }
                }
            }
            listRules = listRulesDef.ToArray();
            sCharSetList.DataSource = listRules;
        }
        private void checkRules(object sender, FormClosedEventArgs e)
        {
            Console.WriteLine("Rules changed - update rules list...");
            listSubstRules();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ruleAdd r = new ruleAdd();
            r.parent = this;
            r.FormClosed += new FormClosedEventHandler(checkRules);
            r.loadLists();
            r.Show();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            
        }
        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Rule Set|*.rst";
            sfd.Title = "Save rule set to file";
            sfd.ShowDialog();

            if (sfd.FileName != "")
            {
                List<rulePair> ruleList = new List<rulePair>();
                for(int i = 0; i < 256; i++)
                {
                    rulePair rp = new rulePair();
                    rp.original = (byte)i;
                    foreach(byte b in data.subst[i].substChar)
                    {
                        rp.substitute.Add(b);
                    }
                    ruleList.Add(rp);
                }

                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                System.IO.FileStream rs = (System.IO.FileStream)sfd.OpenFile();
                bf.Serialize(rs, ruleList);
                rs.Close();                
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Rule Set|*.rst";
            ofd.Title = "Load rule set from file";
            ofd.ShowDialog();
            if (ofd.FileName != "")
            {
                List<rulePair> ruleList = new List<rulePair>();
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                System.IO.FileStream rs = (System.IO.FileStream)ofd.OpenFile();
                object obj = bf.Deserialize(rs);
                rs.Close();
                ruleList = (obj as IEnumerable<rulePair>).ToList();

                int charCount = 0;
                foreach(rulePair p in ruleList)
                {
                   data.subst[charCount].substChar.Clear();
                   foreach(byte b in p.substitute)
                   {
                       data.subst[charCount].substChar.Add(b);
                   }
                   charCount += 1;
                }
            }
            listSubstRules();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Filter = "Rule Set|*.rst";
            sfd.Title = "Save processed file";
            sfd.FileName = data.filePath + ".new";
            sfd.ShowDialog();

            if(sfd.FileName != "")
            {
                List<byte> tempData = new List<byte>();
                foreach(byte character in data.Data)
                {
                    foreach(byte substitute in data.subst[character].substChar)
                    {
                        tempData.Add(substitute);
                    }                    
                }
                byte[] streamData = tempData.ToArray();

                //clearing file for overwrite
                FileStream fs = new FileStream(sfd.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                BinaryWriter bw = new BinaryWriter(fs);
                long numBytes = new FileInfo(sfd.FileName).Length;
                bw.Write(streamData);
                bw.Flush();
                fs.SetLength(streamData.Length);
                bw.Close();
                tempData.Clear();
            }
            this.Close();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class toolsASCII
    {
        public string getEASCIIChar(int i)
        {
            string CHR = "";
            switch (i)
            {
                case 0: CHR = "NUL"; break;
                case 1: CHR = "SOH"; break;
                case 2: CHR = "STX"; break;
                case 3: CHR = "ETX"; break;
                case 4: CHR = "EOT"; break;
                case 5: CHR = "EOQ"; break;
                case 6: CHR = "ACK"; break;
                case 7: CHR = "BEL"; break;
                case 8: CHR = " BS"; break;
                case 9: CHR = " HT"; break;
                case 10: CHR = " LF"; break;
                case 11: CHR = " VT"; break;
                case 12: CHR = " FF"; break;
                case 13: CHR = " CR"; break;
                case 14: CHR = " SO"; break;
                case 15: CHR = " SI"; break;
                case 16: CHR = "DLE"; break;
                case 17: CHR = "DC1"; break;
                case 18: CHR = "DC2"; break;
                case 19: CHR = "DC3"; break;
                case 20: CHR = "DC4"; break;
                case 21: CHR = "NAK"; break;
                case 22: CHR = "SYN"; break;
                case 23: CHR = "ETB"; break;
                case 24: CHR = "CAN"; break;
                case 25: CHR = " EM"; break;
                case 26: CHR = "SUB"; break;
                case 27: CHR = "ESC"; break;
                case 28: CHR = " FS"; break;
                case 29: CHR = " GS"; break;
                case 30: CHR = " RS"; break;
                case 31: CHR = " US"; break;
                //Extended ASCII used with Latin-1 (ISO/IEC 8859-1)
                case 128: CHR = "€"; break;
                case 129: CHR = "[?]"; break;
                case 130: CHR = "‚"; break;
                case 131: CHR = "ƒ"; break;
                case 132: CHR = "„"; break;
                case 133: CHR = "…"; break;
                case 134: CHR = "†"; break;
                case 135: CHR = "‡"; break;
                case 136: CHR = "ˆ"; break;
                case 137: CHR = "‰"; break;
                case 138: CHR = "Š"; break;
                case 139: CHR = "‹"; break;
                case 140: CHR = "Œ"; break;
                case 141: CHR = "[?]"; break;
                case 142: CHR = "Ž"; break;
                case 143: CHR = "[?]"; break;
                case 144: CHR = "[?]"; break;
                case 145: CHR = "‘"; break;
                case 146: CHR = "’"; break;
                case 147: CHR = "“"; break;
                case 148: CHR = "”"; break;
                case 149: CHR = "•"; break;
                case 150: CHR = "–"; break;
                case 151: CHR = "—"; break;
                case 152: CHR = "˜"; break;
                case 153: CHR = "™"; break;
                case 154: CHR = "š"; break;
                case 155: CHR = "›"; break;
                case 156: CHR = "œ"; break;
                case 157: CHR = "[?]"; break;
                case 158: CHR = "ž"; break;
                case 159: CHR = "Ÿ"; break;
                case 160: CHR = " "; break;
                case 161: CHR = "¡"; break;
                case 162: CHR = "¢"; break;
                case 163: CHR = "£"; break;
                case 164: CHR = "¤"; break;
                case 165: CHR = "¥"; break;
                case 166: CHR = "¦"; break;
                case 167: CHR = "§"; break;
                case 168: CHR = "¨"; break;
                case 169: CHR = "©"; break;
                case 170: CHR = "ª"; break;
                case 171: CHR = "«"; break;
                case 172: CHR = "¬"; break;
                case 173: CHR = "­[?]"; break;
                case 174: CHR = "®"; break;
                case 175: CHR = "¯"; break;
                case 176: CHR = "°"; break;
                case 177: CHR = "±"; break;
                case 178: CHR = "²"; break;
                case 179: CHR = "³"; break;
                case 180: CHR = "´"; break;
                case 181: CHR = "µ"; break;
                case 182: CHR = "¶"; break;
                case 183: CHR = "·"; break;
                case 184: CHR = "¸"; break;
                case 185: CHR = "¹"; break;
                case 186: CHR = "º"; break;
                case 187: CHR = "»"; break;
                case 188: CHR = "¼"; break;
                case 189: CHR = "½"; break;
                case 190: CHR = "¾"; break;
                case 191: CHR = "¿"; break;
                case 192: CHR = "À"; break;
                case 193: CHR = "Á"; break;
                case 194: CHR = "Â"; break;
                case 195: CHR = "Ã"; break;
                case 196: CHR = "Ä"; break;
                case 197: CHR = "Å"; break;
                case 198: CHR = "Æ"; break;
                case 199: CHR = "Ç"; break;
                case 200: CHR = "È"; break;
                case 201: CHR = "É"; break;
                case 202: CHR = "Ê"; break;
                case 203: CHR = "Ë"; break;
                case 204: CHR = "Ì"; break;
                case 205: CHR = "Í"; break;
                case 206: CHR = "Î"; break;
                case 207: CHR = "Ï"; break;
                case 208: CHR = "Ð"; break;
                case 209: CHR = "Ñ"; break;
                case 210: CHR = "Ò"; break;
                case 211: CHR = "Ó"; break;
                case 212: CHR = "Ô"; break;
                case 213: CHR = "Õ"; break;
                case 214: CHR = "Ö"; break;
                case 215: CHR = "×"; break;
                case 216: CHR = "Ø"; break;
                case 217: CHR = "Ù"; break;
                case 218: CHR = "Ú"; break;
                case 219: CHR = "Û"; break;
                case 220: CHR = "Ü"; break;
                case 221: CHR = "Ý"; break;
                case 222: CHR = "Þ"; break;
                case 223: CHR = "ß"; break;
                case 224: CHR = "à"; break;
                case 225: CHR = "á"; break;
                case 226: CHR = "â"; break;
                case 227: CHR = "ã"; break;
                case 228: CHR = "ä"; break;
                case 229: CHR = "å"; break;
                case 230: CHR = "æ"; break;
                case 231: CHR = "ç"; break;
                case 232: CHR = "è"; break;
                case 233: CHR = "é"; break;
                case 234: CHR = "ê"; break;
                case 235: CHR = "ë"; break;
                case 236: CHR = "ì"; break;
                case 237: CHR = "í"; break;
                case 238: CHR = "î"; break;
                case 239: CHR = "ï"; break;
                case 240: CHR = "ð"; break;
                case 241: CHR = "ñ"; break;
                case 242: CHR = "ò"; break;
                case 243: CHR = "ó"; break;
                case 244: CHR = "ô"; break;
                case 245: CHR = "õ"; break;
                case 246: CHR = "ö"; break;
                case 247: CHR = "÷"; break;
                case 248: CHR = "ø"; break;
                case 249: CHR = "ù"; break;
                case 250: CHR = "ú"; break;
                case 251: CHR = "û"; break;
                case 252: CHR = "ü"; break;
                case 253: CHR = "ý"; break;
                case 254: CHR = "þ"; break;
                case 255: CHR = "ÿ"; break;

                default: CHR += (char)i; break;
            }
            return CHR;
        }
        public string getEASCIICharAbbr(int i)
        {
            string CHR = "";
            switch (i)
            {
                case 0: CHR = "NULL"; break;
                case 1: CHR = "SOH - Start of heading"; break;
                case 2: CHR = "STX - Start of text"; break;
                case 3: CHR = "ETX - End of text"; break;
                case 4: CHR = "EOT - End of transmission"; break;
                case 5: CHR = "EOQ - End of query"; break;
                case 6: CHR = "ACK - Acknowledge"; break;
                case 7: CHR = "BEL - Bell, audible signal"; break;
                case 8: CHR = " BS - Backspace"; break;
                case 9: CHR = " HT - Horizontal tab"; break;
                case 10: CHR = " LF - Line feed"; break;
                case 11: CHR = " VT - Vertical tab"; break;
                case 12: CHR = " FF - Form feed"; break;
                case 13: CHR = " CR - Carriage return"; break;
                case 14: CHR = " SO - Shift out"; break;
                case 15: CHR = " SI - Shift in"; break;
                case 16: CHR = "DLE - Data line escape"; break;
                case 17: CHR = "DC1 - Device control 1"; break;
                case 18: CHR = "DC2 - Device control 2"; break;
                case 19: CHR = "DC3 - Device control 3"; break;
                case 20: CHR = "DC4 - Device control 4"; break;
                case 21: CHR = "NAK - Negative acknowledge"; break;
                case 22: CHR = "SYN - Synchronize"; break;
                case 23: CHR = "ETB - End of transmission block"; break;
                case 24: CHR = "CAN - Cancel"; break;
                case 25: CHR = " EM - End of medium"; break;
                case 26: CHR = "SUB - Substitute"; break;
                case 27: CHR = "ESC - Escape"; break;
                case 28: CHR = " FS - File separator (right arrow)"; break;
                case 29: CHR = " GS - Group separator (left arrow)"; break;
                case 30: CHR = " RS - Record separator (up arrow)"; break;
                case 31: CHR = " US - Unit separator (down arrow)"; break;
                //Extended ASCII used with Latin-1 (ISO/IEC 8859-1)
                case 128: CHR = "€"; break;
                case 129: CHR = "[?] - Not printable"; break;
                case 130: CHR = "‚"; break;
                case 131: CHR = "ƒ"; break;
                case 132: CHR = "„"; break;
                case 133: CHR = "…"; break;
                case 134: CHR = "†"; break;
                case 135: CHR = "‡"; break;
                case 136: CHR = "ˆ"; break;
                case 137: CHR = "‰"; break;
                case 138: CHR = "Š"; break;
                case 139: CHR = "‹"; break;
                case 140: CHR = "Œ"; break;
                case 141: CHR = "[?] - Not printable"; break;
                case 142: CHR = "Ž"; break;
                case 143: CHR = "[?] - Not printable"; break;
                case 144: CHR = "[?] - Not printable"; break;
                case 145: CHR = "‘"; break;
                case 146: CHR = "’"; break;
                case 147: CHR = "“"; break;
                case 148: CHR = "”"; break;
                case 149: CHR = "•"; break;
                case 150: CHR = "–"; break;
                case 151: CHR = "—"; break;
                case 152: CHR = "˜"; break;
                case 153: CHR = "™"; break;
                case 154: CHR = "š"; break;
                case 155: CHR = "›"; break;
                case 156: CHR = "œ"; break;
                case 157: CHR = "[?] - Not printable"; break;
                case 158: CHR = "ž"; break;
                case 159: CHR = "Ÿ"; break;
                case 160: CHR = " "; break;
                case 161: CHR = "¡"; break;
                case 162: CHR = "¢"; break;
                case 163: CHR = "£"; break;
                case 164: CHR = "¤"; break;
                case 165: CHR = "¥"; break;
                case 166: CHR = "¦"; break;
                case 167: CHR = "§"; break;
                case 168: CHR = "¨"; break;
                case 169: CHR = "©"; break;
                case 170: CHR = "ª"; break;
                case 171: CHR = "«"; break;
                case 172: CHR = "¬"; break;
                case 173: CHR = "­[?] - Not printable"; break;
                case 174: CHR = "®"; break;
                case 175: CHR = "¯"; break;
                case 176: CHR = "°"; break;
                case 177: CHR = "±"; break;
                case 178: CHR = "²"; break;
                case 179: CHR = "³"; break;
                case 180: CHR = "´"; break;
                case 181: CHR = "µ"; break;
                case 182: CHR = "¶"; break;
                case 183: CHR = "·"; break;
                case 184: CHR = "¸"; break;
                case 185: CHR = "¹"; break;
                case 186: CHR = "º"; break;
                case 187: CHR = "»"; break;
                case 188: CHR = "¼"; break;
                case 189: CHR = "½"; break;
                case 190: CHR = "¾"; break;
                case 191: CHR = "¿"; break;
                case 192: CHR = "À"; break;
                case 193: CHR = "Á"; break;
                case 194: CHR = "Â"; break;
                case 195: CHR = "Ã"; break;
                case 196: CHR = "Ä"; break;
                case 197: CHR = "Å"; break;
                case 198: CHR = "Æ"; break;
                case 199: CHR = "Ç"; break;
                case 200: CHR = "È"; break;
                case 201: CHR = "É"; break;
                case 202: CHR = "Ê"; break;
                case 203: CHR = "Ë"; break;
                case 204: CHR = "Ì"; break;
                case 205: CHR = "Í"; break;
                case 206: CHR = "Î"; break;
                case 207: CHR = "Ï"; break;
                case 208: CHR = "Ð"; break;
                case 209: CHR = "Ñ"; break;
                case 210: CHR = "Ò"; break;
                case 211: CHR = "Ó"; break;
                case 212: CHR = "Ô"; break;
                case 213: CHR = "Õ"; break;
                case 214: CHR = "Ö"; break;
                case 215: CHR = "×"; break;
                case 216: CHR = "Ø"; break;
                case 217: CHR = "Ù"; break;
                case 218: CHR = "Ú"; break;
                case 219: CHR = "Û"; break;
                case 220: CHR = "Ü"; break;
                case 221: CHR = "Ý"; break;
                case 222: CHR = "Þ"; break;
                case 223: CHR = "ß"; break;
                case 224: CHR = "à"; break;
                case 225: CHR = "á"; break;
                case 226: CHR = "â"; break;
                case 227: CHR = "ã"; break;
                case 228: CHR = "ä"; break;
                case 229: CHR = "å"; break;
                case 230: CHR = "æ"; break;
                case 231: CHR = "ç"; break;
                case 232: CHR = "è"; break;
                case 233: CHR = "é"; break;
                case 234: CHR = "ê"; break;
                case 235: CHR = "ë"; break;
                case 236: CHR = "ì"; break;
                case 237: CHR = "í"; break;
                case 238: CHR = "î"; break;
                case 239: CHR = "ï"; break;
                case 240: CHR = "ð"; break;
                case 241: CHR = "ñ"; break;
                case 242: CHR = "ò"; break;
                case 243: CHR = "ó"; break;
                case 244: CHR = "ô"; break;
                case 245: CHR = "õ"; break;
                case 246: CHR = "ö"; break;
                case 247: CHR = "÷"; break;
                case 248: CHR = "ø"; break;
                case 249: CHR = "ù"; break;
                case 250: CHR = "ú"; break;
                case 251: CHR = "û"; break;
                case 252: CHR = "ü"; break;
                case 253: CHR = "ý"; break;
                case 254: CHR = "þ"; break;
                case 255: CHR = "ÿ"; break;

                default: CHR += (char)i; break;
            }
            return CHR;
        }
    }
}
