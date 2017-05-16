﻿using System;
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
            //map.Image = new Bitmap(@"..\..\Images\texture1.jpg");
            //map.SizeMode = PictureBoxSizeMode.StretchImage;
            map.Size = new Size(width * cellSize, height * cellSize);
            map.Visible = true;
            PictureBox[] lines = new PictureBox[width + height - 2];
            
            for(int i = 0; i < width - 1; i++)
            {
                lines[i] = new PictureBox();
                lines[i].Location = new Point(map.Location.X + cellSize * (i + 1), map.Location.Y);
                lines[i].Height = height * cellSize;
                lines[i].Width = lineWidth;
                lines[i].BackColor = Color.FromArgb(54, 54, 54);
                //lines[i].Image = new Bitmap(@"..\..\Images\line1.jpg");
                //lines[i].SizeMode = PictureBoxSizeMode.StretchImage;
                lines[i].Visible = true;
                map.Controls.Add(lines[i]);
            }
            for (int i = width - 1; i < width + height - 2; i++)
            {
                lines[i] = new PictureBox();
                lines[i].Location = new Point(map.Location.X, map.Location.Y + cellSize * (i - width + 2));
                lines[i].Height = lineWidth;
                lines[i].Width = width * cellSize;
                lines[i].BackColor = Color.FromArgb(54, 54, 54);
                //lines[i].Image = new Bitmap(@"..\..\Images\line1.jpg");
                //lines[i].SizeMode = PictureBoxSizeMode.StretchImage;
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
                    if (map.Controls.GetChildIndex(c) >= numberOfLines()        //lines always on the top
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

        public void setUser(string name)
        {
            label1.Text = name;
            label1.Location = new Point(Convert.ToInt32(Math.Ceiling(this.Width - 0.04 * this.Width - label1.Width)), label1.Location.Y);
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
                string FileName = openFileDialog.FileName;
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
                string FileName = saveFileDialog.FileName;
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
                this.createNewMapObject(currentTool.FactoryMethod(), this.PointToClient(Cursor.Position));
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (currentTool == null || currentTool.Image != pictureBox1.Image)
            {
                currentTool = new ObjectTool(pictureBox1.Image);
                this.Controls.Add(currentTool);
            }
            else
            {
                currentTool.Dispose();
                currentTool = null;
            }
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            if (currentTool == null || currentTool.Image != pictureBox2.Image)
            {
                currentTool = new BucketTool(pictureBox2.Image);
                this.Controls.Add(currentTool);
            }
            else
            {
                currentTool.Dispose();
                currentTool = null;
            }

        }
    }
}
