using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lynx.Forms.Maps
{
    public partial class MapPreview : Form
    {
        private bool IsRightClicking = false;

        public MapPreview()
        {
            InitializeComponent();
        }

        public void UpdateNpcMapPictureBox(Image newImage)
        {
            if (selectedNpcMapPictureBox != null)
            {
                selectedNpcMapPictureBox.Image = newImage;
            }
        }

        private void SelectedNpcMapPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                IsRightClicking = true;
            }
        }

        private void SelectedNpcMapPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                IsRightClicking = false;
            }

            ((MapEditor)Application.OpenForms["MapEditor"])?.BeginInvoke((Action)(() =>
            {
                ((MapEditor)Application.OpenForms["MapEditor"])?.EndReceiveCursorPosition();
            }));
        }

        private void SelectedNpcMapPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsRightClicking)
            {
                // Check if the cursor is within the boundaries of selectedNpcMapPictureBox
                if (e.X >= 0 && e.Y >= 0 && e.X <= selectedNpcMapPictureBox.Width && e.Y <= selectedNpcMapPictureBox.Height)
                {
                    // Retrieve the dimensions of the image in the PictureBox
                    Image image = selectedNpcMapPictureBox.Image;
                    if (image != null)
                    {
                        // Adjust the coordinates based on the SizeMode
                        float imageX = 0, imageY = 0, scaleX = 1, scaleY = 1;

                        if (selectedNpcMapPictureBox.SizeMode == PictureBoxSizeMode.StretchImage)
                        {
                            scaleX = (float)image.Width / selectedNpcMapPictureBox.Width;
                            scaleY = (float)image.Height / selectedNpcMapPictureBox.Height;
                        }
                        else if (selectedNpcMapPictureBox.SizeMode == PictureBoxSizeMode.Zoom)
                        {
                            float ratioX = (float)selectedNpcMapPictureBox.Width / image.Width;
                            float ratioY = (float)selectedNpcMapPictureBox.Height / image.Height;
                            float ratio = Math.Min(ratioX, ratioY);

                            scaleX = scaleY = 1 / ratio;

                            imageX = (selectedNpcMapPictureBox.Width - image.Width * ratio) / 2;
                            imageY = (selectedNpcMapPictureBox.Height - image.Height * ratio) / 2;
                        }

                        // Convert the mouse coordinates relative to the image
                        float imageCursorX = (e.X - imageX) * scaleX;
                        float imageCursorY = (e.Y - imageY) * scaleY;

                        if (imageCursorX >= 0 && imageCursorY >= 0 && imageCursorX <= image.Width && imageCursorY <= image.Height)
                        {
                            Point cursorPosition = new Point((int)imageCursorX, (int)imageCursorY);

                            // Send the information to the main window
                            SendCursorPositionToMainForm(cursorPosition);
                        }
                    }
                }
            }
        }

        private void SendCursorPositionToMainForm(Point position)
        {
            ((MapEditor)Application.OpenForms["MapEditor"])?.ReceiveCursorPosition(position);
        }
    }
}
