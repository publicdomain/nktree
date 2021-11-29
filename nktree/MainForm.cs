
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace nktree
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Gets or sets the associated icon.
        /// </summary>
        /// <value>The associated icon.</value>
        private Icon associatedIcon = null;


        public MainForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            /* Set icons */

            // Set associated icon from exe file
            this.associatedIcon = Icon.ExtractAssociatedIcon(typeof(MainForm).GetTypeInfo().Assembly.Location);

            // Set PublicDomain.is tool strip menu item image
            this.freeReleasesPublicDomainIsToolStripMenuItem.Image = this.associatedIcon.ToBitmap();
        }

        void DeleteButtonClick(object sender, EventArgs e)
        {

        }

        void BrowseButtonClick(object sender, EventArgs e)
        {

        }

        void NewToolStripMenuItemClick(object sender, EventArgs e)
        {

        }

        void FreeReleasesPublicDomainIsToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Open our website
            Process.Start("https://publicdomain.is");
        }

        void OriginalThreadDonationCodercomToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Open original thread
            Process.Start("https://www.donationcoder.com/forum/index.php?topic=51944.0");
        }

        void SourceCodeGithubcomToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Open GitHub repository
            Process.Start("https://github.com/publicdomain/nktree/");
        }

        void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {

        }

        void CollectButtonClick(object sender, EventArgs e)
        {

        }

        void DirectoryTextBoxDragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var possibleDirectory = (string[])e.Data.GetData(DataFormats.FileDrop);

                if (Directory.Exists(possibleDirectory[0]))
                {
                    this.directoryTextBox.Text = possibleDirectory[0];
                }
            }
        }

        void DirectoryTextBoxDragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        void OptionsToolStripMenuItemDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Set tool strip menu item
            ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)e.ClickedItem;

            // Toggle checked
            toolStripMenuItem.Checked = !toolStripMenuItem.Checked;

            // Set topmost by check box
            this.TopMost = this.alwaysOnTopToolStripMenuItem.Checked;
        }

        void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Close program        
            this.Close();
        }
    }
}
