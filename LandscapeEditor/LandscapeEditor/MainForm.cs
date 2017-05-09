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

        Tool currentTool;

        public MainForm()
        {
            cellSize = 100;
            lineWidth = 5;
            currentTool = null;
            InitializeComponent();
        }

        public bool isContainMap()
        {
            if (map.Visible)
                return true;
            else
                return false;
        }

        public void createNewMap(string name, int width, int height)
        {
            currentTool = null;
            map.Controls.Clear();
            //map.Location = new Point(0, 0);
            map.Image = new Bitmap(@"..\..\Images\texture1.jpg");
            map.SizeMode = PictureBoxSizeMode.StretchImage;
            map.Size = new Size(width * cellSize, height * cellSize);
            map.Visible = true;
            PictureBox[] lines = new PictureBox[width + height - 2];
            
            for(int i = 0; i < width - 1; i++)
            {
                lines[i] = new PictureBox();
                lines[i].Location = new Point(map.Location.X + cellSize * (i + 1), map.Location.Y);
                lines[i].Height = height * cellSize;
                lines[i].Width = lineWidth;
                lines[i].Image = new Bitmap(@"..\..\Images\line1.jpg");
                lines[i].SizeMode = PictureBoxSizeMode.StretchImage;
                lines[i].Visible = true;
                map.Controls.Add(lines[i]);
            }
            for (int i = width - 1; i < width + height - 2; i++)
            {
                lines[i] = new PictureBox();
                lines[i].Location = new Point(map.Location.X, map.Location.Y + cellSize * (i - width + 2));
                lines[i].Height = lineWidth;
                lines[i].Width = width * cellSize;
                lines[i].Image = new Bitmap(@"..\..\Images\line1.jpg");
                lines[i].SizeMode = PictureBoxSizeMode.StretchImage;
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
                map.Controls.Add(newObject);
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
                this.createNewMapObject(currentTool.createObject(), this.PointToClient(Cursor.Position));
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (currentTool == null)
            {
                currentTool = new Tool(pictureBox1.Image);
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
