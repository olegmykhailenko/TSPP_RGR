using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace LandscapeEditor
{
    class BucketTool : AbstractTool
    {
        Color currentColor;
        public BucketTool(Image image)
        {
            this.Image = new Bitmap(image);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Width = Cursor.Size.Width;
            this.Height = Cursor.Size.Height;
            this.Location = Cursor.Position;
            chooseColor();
        }

        private void chooseColor()
        {
            ColorDialog MyDialog = new ColorDialog();
            MyDialog.AllowFullOpen = false;
            MyDialog.ShowHelp = true;
            MyDialog.Color = Color.White;
            
            if (MyDialog.ShowDialog() == DialogResult.OK)
                currentColor = MyDialog.Color;
        }
        public override Control FactoryMethod()
        {
            PictureBox newObject = new PictureBox();
            newObject.BackColor = currentColor;
            return newObject;
        }
    }
}
