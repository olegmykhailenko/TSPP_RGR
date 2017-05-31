using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LandscapeEditor
{
    public partial class MainForm : Form
    {
        private int cellSize;
        private int lineWidth;

        AbstractTool currentTool;
        Map map;

        public MainForm()
        {
            cellSize = 100;
            lineWidth = 5;
            currentTool = null;
            map = Map.Instance;
            map.Location = new Point(0, 0);
            map.BackColor = Color.FromArgb(81, 168, 46);
            map.Visible = false;
            map.MouseClick += map_MouseClick;
            InitializeComponent();
            panel2.Controls.Add(map);
        }

        public bool isContainMap()
        {
            if (map.Visible)
                return true;
            else
                return false;
        }

        public int numberOfLines()
        {
            if (isContainMap())
            {
                return ((map.Width + map.Height) / cellSize) - 2;
            }
            else
                return 0;
        }

        public void createNewMap(string name, int width, int height)
        {
            currentTool = null;
            map.Controls.Clear();
            map.numberOfText = 0;
            map.Size = new Size(width * cellSize, height * cellSize);
            map.Visible = true;
            PictureBox[] lines = new PictureBox[width + height - 2];
            
            for(int i = 0; i < width - 1; i++)
            {
                lines[i] = new PictureBox();
                map.Controls.Add(lines[i]);
                lines[i].Location = new Point(cellSize * (i + 1), 0);
                lines[i].Height = height * cellSize;
                lines[i].Width = lineWidth;
                lines[i].BackColor = Color.FromArgb(54, 54, 54);
                lines[i].Visible = true;
            }
            for (int i = width - 1; i < width + height - 2; i++)
            {
                lines[i] = new PictureBox();
                map.Controls.Add(lines[i]);
                lines[i].Location = new Point(0, cellSize * (i - width + 2));
                lines[i].Height = lineWidth;
                lines[i].Width = width * cellSize;
                lines[i].BackColor = Color.FromArgb(54, 54, 54);
                lines[i].Visible = true;
                map.Controls.Add(lines[i]);
            }
        }

        public void createNewMapObject(Control newObject, Point newPoint)
        {
            if (this.isContainMap())
            {
                newPoint = new Point(Math.Abs(map.Location.X) + newPoint.X, 
                    Math.Abs(map.Location.Y) + newPoint.Y - this.menuStrip1.Height);
                newObject.Location = new Point(newPoint.X - (newPoint.X % cellSize), 
                    newPoint.Y - (newPoint.Y % cellSize));
                newObject.Height = cellSize;
                newObject.Width = cellSize;
                newObject.MouseClick += map_MouseClick;
                map.Controls.Add(newObject);
                foreach (Control c in map.Controls)
                {
                    if (map.Controls.GetChildIndex(c) >= numberOfLines() + map.numberOfText       //lines always on the top
                        && c.Location == newObject.Location                     //objects have same location
                        && map.Controls.GetChildIndex(c) != map.Controls.GetChildIndex(newObject))  //objects are not the same
                    {
                        if(newObject.BackColor == Color.Transparent)    //if new object has no backcolor
                            newObject.BackColor = c.BackColor;          //copy it from previous object
                        c.Dispose();                                    //and delete previous object
                        
                    }
                }
            }
        }

        public void createNewTextObject(Control newObject, Point newPoint)
        {
            if (this.isContainMap())
            {
                newObject.Location = new Point(Math.Abs(map.Location.X) + newPoint.X,
                    Math.Abs(map.Location.Y) + newPoint.Y - this.menuStrip1.Height);
                
                map.Controls.Add(newObject);
                newObject.TextChanged += textBox_TextChanged;
                map.Controls.SetChildIndex(newObject, 0);
                map.numberOfText++;
                
            }
        }

        public void deleteMapObject(Point newPoint)
        {
            if (this.isContainMap())
            {
                newPoint = new Point(Math.Abs(map.Location.X) + newPoint.X,
                    Math.Abs(map.Location.Y) + newPoint.Y - this.menuStrip1.Height);
                newPoint = new Point(newPoint.X - (newPoint.X % cellSize),
                    newPoint.Y - (newPoint.Y % cellSize));
                foreach (Control c in map.Controls)
                {
                    if (map.Controls.GetChildIndex(c) >= numberOfLines() + map.numberOfText     
                        && c.Location == newPoint)                     
                    {

                        if (((PictureBox)c).Image == null)    
                            c.Dispose();          
                        else
                            ((PictureBox)c).Image = null;                                    
                    }
                }
            }
        }

        public void setUser(string name)
        {
            label1.Text = name;
            label1.Location = new Point(Convert.ToInt32(Math.Ceiling(this.Width - 0.04 * this.Width - label1.Width)), label1.Location.Y);
        }

        public void setCursor(Cursor newCursor)
        {
            if (this.isContainMap())
            {
                map.Cursor = newCursor;
                foreach(Control control in map.Controls)
                {
                    control.Cursor = newCursor;
                }
            }
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTool = null;
            newFileForm aForm = new newFileForm(this);
            aForm.StartPosition = FormStartPosition.CenterParent;
            aForm.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)               //sign in
        {
            currentTool = null;
            SignInForm aForm = new SignInForm(this);
            aForm.StartPosition = FormStartPosition.CenterParent;
            aForm.ShowDialog();

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTool = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Landscape Designer Files (*.ld)|*.ld|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;
                System.IO.FileStream fl = new System.IO.FileStream(fileName, System.IO.FileMode.Open);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                try
                {
                    map.Controls.Clear();
                    ((SerializableMap)formatter.Deserialize(fl)).restore(map);
                    foreach(Control current in map.Controls)
                    {
                        current.MouseClick += map_MouseClick;
                    }
                    map.Visible = true;
                }
                catch (System.Runtime.Serialization.SerializationException ex)
                {
                    MessageBox.Show("Failed to serialize. Reason: " + ex.Message);
                    //throw;
                }
                finally
                {
                    fl.Close();
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentTool = null;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Landscape Designer Files (*.ld)|*.ld|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                System.IO.FileStream fl = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                try
                {
                    formatter.Serialize(fl, new SerializableMap(map));
                }
                catch (System.Runtime.Serialization.SerializationException ex)
                {
                    MessageBox.Show("Failed to serialize. Reason: " + ex.Message);
                    //throw;
                }
                finally
                {
                    fl.Close();
                }
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            //TODO
        }

        private void map_MouseClick(object sender, MouseEventArgs e)
        {
            if (currentTool !=  null)
            {
                if((string)currentTool.Tag == "DeleteTool")
                {
                    deleteMapObject(this.PointToClient(Cursor.Position));
                }
                else if ((string)currentTool.Tag == "TextTool")
                {
                    this.createNewTextObject(currentTool.FactoryMethod(), this.PointToClient(Cursor.Position));
                }
                else
                    this.createNewMapObject(currentTool.FactoryMethod(), this.PointToClient(Cursor.Position));
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (currentTool == null || !currentTool.Tag.Equals(pictureBox1.Tag))
            {
                currentTool = new ObjectTool(pictureBox1.Image);
                currentTool.Tag = pictureBox1.Tag;
                this.Controls.Add(currentTool);
                this.setCursor(new Cursor( @"..\..\Images\tree1.cur"));
            }
            else
            {
                currentTool.Dispose();
                currentTool = null;
                this.setCursor(Cursors.Default);
            }
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (currentTool == null || !currentTool.Tag.Equals(pictureBox2.Tag))
            {
                currentTool = new BucketTool(pictureBox2.Image);
                currentTool.Tag = pictureBox2.Tag;
                this.Controls.Add(currentTool);
                this.setCursor(new Cursor(@"..\..\Images\paint_bucket.cur"));
            }
            else
            {
                currentTool.Dispose();
                currentTool = null;
                this.setCursor(Cursors.Default);
            }

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (currentTool == null || !currentTool.Tag.Equals(pictureBox4.Tag))
            {
                currentTool = new DeleteTool();
                currentTool.Tag = pictureBox4.Tag;
                this.Controls.Add(currentTool);
                this.setCursor(new Cursor(@"..\..\Images\delete.cur"));
            }
            else
            {
                currentTool.Dispose();
                currentTool = null;
                this.setCursor(Cursors.Default);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (currentTool == null || !currentTool.Tag.Equals(pictureBox3.Tag))
            {
                currentTool = new TextTool();
                currentTool.Tag = pictureBox3.Tag;
                this.Controls.Add(currentTool);
                this.setCursor(new Cursor(@"..\..\Images\text.cur"));
            }
            else
            {
                currentTool.Dispose();
                currentTool = null;
                this.setCursor(Cursors.Default);
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            Size sz = new Size(((TextBox)sender).ClientSize.Width, int.MaxValue);
            //TextFormatFlags flags = TextFormatFlags.WordBreak;
            //int padding = 3;
            int borders = ((TextBox)sender).Width - ((TextBox)sender).ClientSize.Width;
            sz = TextRenderer.MeasureText(((TextBox)sender).Text, ((TextBox)sender).Font, sz);
            //int h = ((TextBox)sender).TextLength * sz.Width + borders + padding;
            if (sz.Width > 100)
            {
                ((TextBox)sender).Width = sz.Width;
            }
        }
    }
}
