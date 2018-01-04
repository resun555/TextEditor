using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            // Show the Open File dialog. If the user clicks OK, read all text in the
            // file that the user chose. Retry if the file name is invalid.
            while (true)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        textBox1.Text = File.ReadAllText(openFileDialog1.FileName);
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exception: " + ex.Message);
                        continue;
                    }
                }
            }
        }

        private void closeSaveButton_Click(object sender, EventArgs e)
        {
            // If the user has chosen a file, 
            // change the Save File Dialog's
            // FileName property to the Open File Dialog's
            // FileName property.
            if (openFileDialog1.FileName != "")
            {
                saveFileDialog1.FileName = openFileDialog1.FileName;
            }
            // If the user has not chosen a file, 
            // show the Save File dialog. If the user clicks 
            // OK or the user has chosen a file, writeout all text to the
            // file that the user chose. Retry if the file name is invalid.
            while (true)
            {
                if (openFileDialog1.FileName != "" || saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        File.WriteAllText(saveFileDialog1.FileName, textBox1.Text);

                        // Clear the text.
                        textBox1.Clear();

                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exception: " + ex.Message);
                        continue;
                    }
                }
            }
        }

        private void closeDiscardButton_Click(object sender, EventArgs e)
        {
            // Clear the text.
            textBox1.Clear();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            // If the user has chosen a file, 
            // compare the content of the file with
            // the content of the TextBox. If the contents are
            // different, show a MessageBox to ask the user
            // whether to save changes or to select cancel
            if (openFileDialog1.FileName != "")
            {
                if (textBox1.Text != File.ReadAllText(openFileDialog1.FileName))
                {
                    DialogResult result = MessageBox.Show("Do you want to save changes to " + openFileDialog1.FileName + "?", 
                                                                                                                   this.Text, 
                                                                                               MessageBoxButtons.YesNoCancel, 
                                                                                                     MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        this.closeSaveButton_Click(sender, e);
                    }
                    else if (result == DialogResult.No)
                    {
                        this.closeDiscardButton_Click(sender, e);
                    }

                    if (result != DialogResult.Cancel)
                    {
                        // Close the form.
                        this.Close();
                    }
                }
                else
                {
                    // Close the form.
                    this.Close();
                }
            }
            // If the user has not chosen a file, 
            // check if the content of the TextBox is 
            // empty. If the content is not
            // empty, show a MessageBox to ask the user
            // whether to save changes or to select cancel
            else
            {
                if (textBox1.Text != "")
                {
                    const string untitled = "Untitled";
                    DialogResult result = MessageBox.Show("Do you want to save changes to " + untitled + "?", 
                                                                                                   this.Text, 
                                                                               MessageBoxButtons.YesNoCancel, 
                                                                                     MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        this.closeSaveButton_Click(sender, e);
                    }
                    else if (result == DialogResult.No)
                    {
                        this.closeDiscardButton_Click(sender, e);
                    }

                    if (result != DialogResult.Cancel)
                    {
                        // Close the form.
                        this.Close();
                    }
                }
                else
                {
                    // Close the form.
                    this.Close();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // If the user selects the Word Wrap check box, 
            // change the TextBox's
            // WordWrap property to true. If the user clears
            // the check box, change it to false.
            if (checkBox1.Checked)
            {
                textBox1.WordWrap = true;
            }
            else
            {
                textBox1.WordWrap = false;
            }
        }
    }
}
