using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace LandscapeEditor
{
    [Serializable]
    abstract class CustomControl
    {
        abstract public Control restore();
    }
}
