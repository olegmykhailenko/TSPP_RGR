using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace LandscapeEditor
{
    class TextTool : AbstractTool
    {
        public TextTool()
        {
        }

        public override Control FactoryMethod()
        {
            TextBox newObject = new TextBox();
            return newObject;
        }
    }
}
