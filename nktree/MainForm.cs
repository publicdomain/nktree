
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Alphaleonis.Win32.Filesystem;
using System.Reflection;
using System.Windows.Forms;
using PublicDomain;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="T:nktree.MainForm"/> class.
        /// </summary>
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

            // Focus directory text box
            this.directoryTextBox.Focus();
        }

        void CollectButtonClick(object sender, EventArgs e)
        {
            // Check for empty parent
            if (this.directoryTextBox.Text.Length == 0)
            {
                MessageBox.Show("Please set source directory.", "Directory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                this.directoryTextBox.Focus();

                return;
            }

            // Check directory
            if (!Directory.Exists(this.directoryTextBox.Text))
            {
                MessageBox.Show("Please use an existing directory.", "Directory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                this.directoryTextBox.Focus();

                return;
            }

            // Match all if no pattern
            if (this.patternTextBox.Text.Length == 0)
            {
                this.patternTextBox.Text = "*";
            }

            // Begin update
            this.collectedCheckedListBox.BeginUpdate();

            // Clear list box
            this.collectedCheckedListBox.Items.Clear();

            // Collect directories
            foreach (var item in Directory.GetDirectories(this.directoryTextBox.Text, this.patternTextBox.Text, System.IO.SearchOption.AllDirectories))
            {
                this.collectedCheckedListBox.Items.Add(item, true);
            }

            // Endupdate
            this.collectedCheckedListBox.EndUpdate();

            // update status
            this.collectedCountToolStripStatusLabel.Text = this.collectedCheckedListBox.Items.Count.ToString();
            this.deletedCountToolStripStatusLabel.Text = "0";
        }

        void DeleteButtonClick(object sender, EventArgs e)
        {
            int count = 0;

            // Delete checked
            for (int i = 0; i < this.collectedCheckedListBox.Items.Count; i++)
            {
                if (this.collectedCheckedListBox.GetItemChecked(i))
                {
                    try
                    {
                        Directory.Delete(this.collectedCheckedListBox.Items[i].ToString(), true);

                        count++;
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show($"Could not delete directory:{Environment.NewLine}{this.collectedCheckedListBox.Items[i].ToString()}{Environment.NewLine}{Environment.NewLine}Message:{Environment.NewLine}{exception.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            this.deletedCountToolStripStatusLabel.Text = count.ToString();
        }

        void BrowseButtonClick(object sender, EventArgs e)
        {
            this.folderBrowserDialog.SelectedPath = string.Empty;

            if (this.folderBrowserDialog.ShowDialog() == DialogResult.OK && this.folderBrowserDialog.SelectedPath.Length > 0)
            {
                this.directoryTextBox.Text = this.folderBrowserDialog.SelectedPath;
            }
        }

        void NewToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Clear & reset
            this.directoryTextBox.Clear();
            this.patternTextBox.Clear();
            this.collectedCheckedListBox.Items.Clear();
            this.collectedCountToolStripStatusLabel.Text = "0";
            this.deletedCountToolStripStatusLabel.Text = "0";

            // Focus directory text box
            this.directoryTextBox.Focus();
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
            // Set license text
            var licenseText = $"CC0 1.0 Universal (CC0 1.0) - Public Domain Dedication{Environment.NewLine}" +
                $"https://creativecommons.org/publicdomain/zero/1.0/legalcode{Environment.NewLine}{Environment.NewLine}" +
                $"Libraries and icons have separate licenses.{Environment.NewLine}{Environment.NewLine}" +
                $"AlphaFS library by alphaleonis - MIT License{Environment.NewLine}" +
                $"https://github.com/alphaleonis/AlphaFS/{Environment.NewLine}{Environment.NewLine}" +
                $"Directory Icon by OpenIcons - Pixabay License{Environment.NewLine}" +
                $"https://pixabay.com/vectors/search-search-results-directory-98464/{Environment.NewLine}{Environment.NewLine}" +
                $"Patreon icon used according to published brand guidelines{Environment.NewLine}" +
                $"https://www.patreon.com/brand{Environment.NewLine}{Environment.NewLine}" +
                $"GitHub mark icon used according to published logos and usage guidelines{Environment.NewLine}" +
                $"https://github.com/logos{Environment.NewLine}{Environment.NewLine}" +
                $"DonationCoder icon used with permission{Environment.NewLine}" +
                $"https://www.donationcoder.com/forum/index.php?topic=48718{Environment.NewLine}{Environment.NewLine}" +
                $"PublicDomain icon is based on the following source images:{Environment.NewLine}{Environment.NewLine}" +
                $"Bitcoin by GDJ - Pixabay License{Environment.NewLine}" +
                $"https://pixabay.com/vectors/bitcoin-digital-currency-4130319/{Environment.NewLine}{Environment.NewLine}" +
                $"Letter P by ArtsyBee - Pixabay License{Environment.NewLine}" +
                $"https://pixabay.com/illustrations/p-glamour-gold-lights-2790632/{Environment.NewLine}{Environment.NewLine}" +
                $"Letter D by ArtsyBee - Pixabay License{Environment.NewLine}" +
                $"https://pixabay.com/illustrations/d-glamour-gold-lights-2790573/{Environment.NewLine}{Environment.NewLine}";

            // Prepend sponsors
            licenseText = $"RELEASE SPONSORS:{Environment.NewLine}{Environment.NewLine}* Jesse Reichler{Environment.NewLine}* Max P{Environment.NewLine}{Environment.NewLine}=========={Environment.NewLine}{Environment.NewLine}" + licenseText;

            // Set title
            string programTitle = typeof(MainForm).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title;

            // Set version for generating semantic version 
            Version version = typeof(MainForm).GetTypeInfo().Assembly.GetName().Version;

            // Set about form
            var aboutForm = new AboutForm(
                $"About {programTitle}",
                $"{programTitle} {version.Major}.{version.Minor}.{version.Build}",
                $"Made for: nkormanik{Environment.NewLine}DonationCoder.com{Environment.NewLine}Day #334, Week #48 @ November 30, 2021",
                licenseText,
                this.Icon.ToBitmap())
            {
                // Set about form icon
                Icon = this.associatedIcon,

                // Set always on top
                TopMost = this.TopMost
            };

            // Show about form
            aboutForm.ShowDialog();
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
