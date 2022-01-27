using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winAsciiScrub
{
    [Serializable]
    class rulePair
    {
        public rulePair()
        {
            substitute = new List<byte>();
        }
        public byte original;
        public List<byte> substitute;
    }
}
