using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace winAsciiScrub
{
    public class charData
    {
        public charData()
        {
            count = 0;
            substChar = new List<byte>();
        }
        public long count;
        public List<byte> substChar;
    }

    public class fileData
    {
        public string filePath;
        public long fileSize;
        public charData [] subst;
        public byte [] Data;

        public fileData(string file)
        {
            this.filePath = file;
            this.fileSize = new FileInfo(this.filePath).Length;
            Console.WriteLine("File Name : " + this.filePath);
            Console.WriteLine("File Size : " + this.fileSize + " Bytes");
            
            Console.WriteLine("Preparing data structure...");
            this.subst = new charData[256];
            for (int i = 0; i < 256; i++)
            {
                this.subst[i] = new charData();
                this.subst[i].count = 0;
                this.subst[i].substChar.Clear();
                this.subst[i].substChar.Add((byte)i);
            }

            Console.WriteLine("Loading data into memory...");
            
            Data = null;
            FileStream fs = new FileStream(this.filePath,
                                           FileMode.Open,
                                           FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(this.filePath).Length;
            Data = br.ReadBytes((int)numBytes);

            Console.WriteLine("Parsing data into data structure...");
            for (int i = 0; i < numBytes; i++)
            {
                this.subst[Data[i]].count += 1;
            }
        }

        ~fileData()
        {
            this.Data = null;
            foreach (charData c in this.subst)
            {
                c.substChar.Clear();
            }
            this.subst = null;
        }

    }
}
