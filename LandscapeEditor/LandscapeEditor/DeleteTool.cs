using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace LandscapeEditor
{
    class DeleteTool : AbstractTool
    {

        public override Control FactoryMethod()
        {
            PictureBox newObject = new PictureBox();
            newObject.Image = this.Image;
            newObject.SizeMode = PictureBoxSizeMode.StretchImage;
            newObject.BackColor = Color.Transparent;
            return newObject;
        }
    }
}
