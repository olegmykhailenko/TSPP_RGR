using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace LandscapeEditor
{
    class Map : PictureBox
    {
        private static readonly Map instance = new Map();

        public static Map Instance
        {
            get { return instance; }
        }
        
        protected Map() { }
    }
}

