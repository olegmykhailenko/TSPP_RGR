using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace LandscapeEditor
{
    //[Serializable]
    class Map : PictureBox
    {
        private static Map instance;

        public int numberOfText{get; set;}
        public static Map Instance
        {
            get { return instance ?? (instance = new Map()); }
        }
        protected Map() { }
    }
}

