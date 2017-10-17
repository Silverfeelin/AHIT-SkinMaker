namespace AHITSkinMaker
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.GbxGameInformation = new System.Windows.Forms.GroupBox();
            this.TbxEditorExecutablePath = new System.Windows.Forms.TextBox();
            this.LblEditorExecutable = new System.Windows.Forms.Label();
            this.TbxGameFolder = new System.Windows.Forms.TextBox();
            this.LblGameFolder = new System.Windows.Forms.Label();
            this.BtnUpdateGameFolder = new System.Windows.Forms.Button();
            this.BtnCreateMod = new System.Windows.Forms.Button();
            this.BtnCook = new System.Windows.Forms.Button();
            this.BtnCompile = new System.Windows.Forms.Button();
            this.TbxInfo = new System.Windows.Forms.RichTextBox();
            this.PbxPreview = new System.Windows.Forms.PictureBox();
            this.GbxSkinModInformation = new System.Windows.Forms.GroupBox();
            this.LblSkins = new System.Windows.Forms.Label();
            this.PbxIcon = new System.Windows.Forms.PictureBox();
            this.BtnSelectIcon = new System.Windows.Forms.Button();
            this.LblModIcon = new System.Windows.Forms.Label();
            this.LytButtons = new System.Windows.Forms.TableLayoutPanel();
            this.BtnRemoveSkin = new System.Windows.Forms.Button();
            this.BtnAddSkin = new System.Windows.Forms.Button();
            this.LbxSkins = new System.Windows.Forms.ListBox();
            this.TbxModName = new System.Windows.Forms.TextBox();
            this.LblModName = new System.Windows.Forms.Label();
            this.TTip = new System.Windows.Forms.ToolTip(this.components);
            this.BtnChangeSkin = new System.Windows.Forms.Button();
            this.GbxGameInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbxPreview)).BeginInit();
            this.GbxSkinModInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbxIcon)).BeginInit();
            this.LytButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // GbxGameInformation
            // 
            this.GbxGameInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GbxGameInformation.Controls.Add(this.TbxEditorExecutablePath);
            this.GbxGameInformation.Controls.Add(this.LblEditorExecutable);
            this.GbxGameInformation.Controls.Add(this.TbxGameFolder);
            this.GbxGameInformation.Controls.Add(this.LblGameFolder);
            this.GbxGameInformation.Controls.Add(this.BtnUpdateGameFolder);
            this.GbxGameInformation.Location = new System.Drawing.Point(12, 12);
            this.GbxGameInformation.Name = "GbxGameInformation";
            this.GbxGameInformation.Size = new System.Drawing.Size(928, 80);
            this.GbxGameInformation.TabIndex = 0;
            this.GbxGameInformation.TabStop = false;
            this.GbxGameInformation.Text = "Game Information";
            // 
            // TbxEditorExecutablePath
            // 
            this.TbxEditorExecutablePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TbxEditorExecutablePath.Location = new System.Drawing.Point(105, 47);
            this.TbxEditorExecutablePath.Name = "TbxEditorExecutablePath";
            this.TbxEditorExecutablePath.ReadOnly = true;
            this.TbxEditorExecutablePath.Size = new System.Drawing.Size(817, 20);
            this.TbxEditorExecutablePath.TabIndex = 8;
            // 
            // LblEditorExecutable
            // 
            this.LblEditorExecutable.AutoSize = true;
            this.LblEditorExecutable.Location = new System.Drawing.Point(6, 50);
            this.LblEditorExecutable.Name = "LblEditorExecutable";
            this.LblEditorExecutable.Size = new System.Drawing.Size(93, 13);
            this.LblEditorExecutable.TabIndex = 7;
            this.LblEditorExecutable.Text = "Editor Executable:";
            // 
            // TbxGameFolder
            // 
            this.TbxGameFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TbxGameFolder.Location = new System.Drawing.Point(82, 21);
            this.TbxGameFolder.Name = "TbxGameFolder";
            this.TbxGameFolder.Size = new System.Drawing.Size(717, 20);
            this.TbxGameFolder.TabIndex = 0;
            // 
            // LblGameFolder
            // 
            this.LblGameFolder.AutoSize = true;
            this.LblGameFolder.Location = new System.Drawing.Point(6, 24);
            this.LblGameFolder.Name = "LblGameFolder";
            this.LblGameFolder.Size = new System.Drawing.Size(70, 13);
            this.LblGameFolder.TabIndex = 5;
            this.LblGameFolder.Text = "Game Folder:";
            // 
            // BtnUpdateGameFolder
            // 
            this.BtnUpdateGameFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnUpdateGameFolder.Location = new System.Drawing.Point(805, 19);
            this.BtnUpdateGameFolder.Name = "BtnUpdateGameFolder";
            this.BtnUpdateGameFolder.Size = new System.Drawing.Size(117, 23);
            this.BtnUpdateGameFolder.TabIndex = 1;
            this.BtnUpdateGameFolder.Text = "Update";
            this.BtnUpdateGameFolder.UseVisualStyleBackColor = true;
            this.BtnUpdateGameFolder.Click += new System.EventHandler(this.BtnUpdateGameFolder_Click);
            // 
            // BtnCreateMod
            // 
            this.BtnCreateMod.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCreateMod.Enabled = false;
            this.BtnCreateMod.Location = new System.Drawing.Point(3, 37);
            this.BtnCreateMod.Name = "BtnCreateMod";
            this.BtnCreateMod.Size = new System.Drawing.Size(203, 28);
            this.BtnCreateMod.TabIndex = 2;
            this.BtnCreateMod.Text = "Create Mod";
            this.BtnCreateMod.UseVisualStyleBackColor = true;
            this.BtnCreateMod.Click += new System.EventHandler(this.BtnCreateMod_Click);
            // 
            // BtnCook
            // 
            this.BtnCook.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCook.Enabled = false;
            this.BtnCook.Location = new System.Drawing.Point(422, 37);
            this.BtnCook.Name = "BtnCook";
            this.BtnCook.Size = new System.Drawing.Size(205, 28);
            this.BtnCook.TabIndex = 4;
            this.BtnCook.Text = "Cook";
            this.BtnCook.UseVisualStyleBackColor = true;
            this.BtnCook.Click += new System.EventHandler(this.BtnCook_Click);
            // 
            // BtnCompile
            // 
            this.BtnCompile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCompile.Enabled = false;
            this.BtnCompile.Location = new System.Drawing.Point(212, 37);
            this.BtnCompile.Name = "BtnCompile";
            this.BtnCompile.Size = new System.Drawing.Size(204, 28);
            this.BtnCompile.TabIndex = 3;
            this.BtnCompile.Text = "Compile";
            this.BtnCompile.UseVisualStyleBackColor = true;
            this.BtnCompile.Click += new System.EventHandler(this.BtnCompile_Click);
            // 
            // TbxInfo
            // 
            this.TbxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TbxInfo.Location = new System.Drawing.Point(12, 428);
            this.TbxInfo.Name = "TbxInfo";
            this.TbxInfo.ReadOnly = true;
            this.TbxInfo.Size = new System.Drawing.Size(928, 113);
            this.TbxInfo.TabIndex = 3;
            this.TbxInfo.Text = "";
            this.TbxInfo.WordWrap = false;
            // 
            // PbxPreview
            // 
            this.PbxPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PbxPreview.Image = global::AHITSkinMaker.Properties.Resources.Template;
            this.PbxPreview.Location = new System.Drawing.Point(660, 98);
            this.PbxPreview.Name = "PbxPreview";
            this.PbxPreview.Size = new System.Drawing.Size(280, 324);
            this.PbxPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.PbxPreview.TabIndex = 4;
            this.PbxPreview.TabStop = false;
            this.TTip.SetToolTip(this.PbxPreview, "This template is a modified version of Piggybank12\'s Hat Kit animation.\r\nYou can " +
        "view the original on DeviantArt at: https://goo.gl/DyyPMY");
            // 
            // GbxSkinModInformation
            // 
            this.GbxSkinModInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GbxSkinModInformation.Controls.Add(this.LblSkins);
            this.GbxSkinModInformation.Controls.Add(this.PbxIcon);
            this.GbxSkinModInformation.Controls.Add(this.BtnSelectIcon);
            this.GbxSkinModInformation.Controls.Add(this.LblModIcon);
            this.GbxSkinModInformation.Controls.Add(this.LytButtons);
            this.GbxSkinModInformation.Controls.Add(this.LbxSkins);
            this.GbxSkinModInformation.Controls.Add(this.TbxModName);
            this.GbxSkinModInformation.Controls.Add(this.LblModName);
            this.GbxSkinModInformation.Location = new System.Drawing.Point(12, 98);
            this.GbxSkinModInformation.Name = "GbxSkinModInformation";
            this.GbxSkinModInformation.Size = new System.Drawing.Size(642, 324);
            this.GbxSkinModInformation.TabIndex = 1;
            this.GbxSkinModInformation.TabStop = false;
            this.GbxSkinModInformation.Text = "Skin Mod Information";
            // 
            // LblSkins
            // 
            this.LblSkins.AutoSize = true;
            this.LblSkins.Location = new System.Drawing.Point(174, 50);
            this.LblSkins.Name = "LblSkins";
            this.LblSkins.Size = new System.Drawing.Size(36, 13);
            this.LblSkins.TabIndex = 25;
            this.LblSkins.Text = "Skins:";
            // 
            // PbxIcon
            // 
            this.PbxIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PbxIcon.Location = new System.Drawing.Point(9, 74);
            this.PbxIcon.Name = "PbxIcon";
            this.PbxIcon.Size = new System.Drawing.Size(159, 159);
            this.PbxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PbxIcon.TabIndex = 24;
            this.PbxIcon.TabStop = false;
            // 
            // BtnSelectIcon
            // 
            this.BtnSelectIcon.Location = new System.Drawing.Point(67, 45);
            this.BtnSelectIcon.Name = "BtnSelectIcon";
            this.BtnSelectIcon.Size = new System.Drawing.Size(101, 23);
            this.BtnSelectIcon.TabIndex = 1;
            this.BtnSelectIcon.Text = "Select Icon...";
            this.BtnSelectIcon.UseVisualStyleBackColor = true;
            this.BtnSelectIcon.Click += new System.EventHandler(this.BtnSelectIcon_Click);
            // 
            // LblModIcon
            // 
            this.LblModIcon.AutoSize = true;
            this.LblModIcon.Location = new System.Drawing.Point(6, 51);
            this.LblModIcon.Name = "LblModIcon";
            this.LblModIcon.Size = new System.Drawing.Size(55, 13);
            this.LblModIcon.TabIndex = 22;
            this.LblModIcon.Text = "Mod Icon:";
            // 
            // LytButtons
            // 
            this.LytButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LytButtons.ColumnCount = 3;
            this.LytButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.LytButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.LytButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.LytButtons.Controls.Add(this.BtnChangeSkin, 1, 0);
            this.LytButtons.Controls.Add(this.BtnCook, 2, 1);
            this.LytButtons.Controls.Add(this.BtnRemoveSkin, 2, 0);
            this.LytButtons.Controls.Add(this.BtnCompile, 1, 1);
            this.LytButtons.Controls.Add(this.BtnCreateMod, 0, 1);
            this.LytButtons.Controls.Add(this.BtnAddSkin, 0, 0);
            this.LytButtons.Location = new System.Drawing.Point(6, 250);
            this.LytButtons.Name = "LytButtons";
            this.LytButtons.RowCount = 2;
            this.LytButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.LytButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.LytButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.LytButtons.Size = new System.Drawing.Size(630, 68);
            this.LytButtons.TabIndex = 3;
            // 
            // BtnRemoveSkin
            // 
            this.BtnRemoveSkin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnRemoveSkin.Location = new System.Drawing.Point(422, 3);
            this.BtnRemoveSkin.Name = "BtnRemoveSkin";
            this.BtnRemoveSkin.Size = new System.Drawing.Size(205, 28);
            this.BtnRemoveSkin.TabIndex = 1;
            this.BtnRemoveSkin.Text = "Remove Skin";
            this.BtnRemoveSkin.UseVisualStyleBackColor = true;
            this.BtnRemoveSkin.Click += new System.EventHandler(this.BtnRemoveSkin_Click);
            // 
            // BtnAddSkin
            // 
            this.BtnAddSkin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAddSkin.Location = new System.Drawing.Point(3, 3);
            this.BtnAddSkin.Name = "BtnAddSkin";
            this.BtnAddSkin.Size = new System.Drawing.Size(203, 28);
            this.BtnAddSkin.TabIndex = 0;
            this.BtnAddSkin.Text = "Add Skin";
            this.BtnAddSkin.UseVisualStyleBackColor = true;
            this.BtnAddSkin.Click += new System.EventHandler(this.BtnAddSkin_Click);
            // 
            // LbxSkins
            // 
            this.LbxSkins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LbxSkins.FormattingEnabled = true;
            this.LbxSkins.Location = new System.Drawing.Point(174, 68);
            this.LbxSkins.Name = "LbxSkins";
            this.LbxSkins.Size = new System.Drawing.Size(462, 173);
            this.LbxSkins.TabIndex = 2;
            this.LbxSkins.SelectedIndexChanged += new System.EventHandler(this.LbxSkins_SelectedIndexChanged);
            // 
            // TbxModName
            // 
            this.TbxModName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TbxModName.Location = new System.Drawing.Point(74, 19);
            this.TbxModName.Name = "TbxModName";
            this.TbxModName.Size = new System.Drawing.Size(562, 20);
            this.TbxModName.TabIndex = 0;
            // 
            // LblModName
            // 
            this.LblModName.AutoSize = true;
            this.LblModName.Location = new System.Drawing.Point(6, 22);
            this.LblModName.Name = "LblModName";
            this.LblModName.Size = new System.Drawing.Size(62, 13);
            this.LblModName.TabIndex = 9;
            this.LblModName.Text = "Mod Name:";
            // 
            // BtnChangeSkin
            // 
            this.BtnChangeSkin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnChangeSkin.Location = new System.Drawing.Point(212, 3);
            this.BtnChangeSkin.Name = "BtnChangeSkin";
            this.BtnChangeSkin.Size = new System.Drawing.Size(204, 28);
            this.BtnChangeSkin.TabIndex = 5;
            this.BtnChangeSkin.Text = "Change Skin";
            this.BtnChangeSkin.UseVisualStyleBackColor = true;
            this.BtnChangeSkin.Click += new System.EventHandler(this.BtnChangeSkin_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 553);
            this.Controls.Add(this.GbxSkinModInformation);
            this.Controls.Add(this.PbxPreview);
            this.Controls.Add(this.TbxInfo);
            this.Controls.Add(this.GbxGameInformation);
            this.MinimumSize = new System.Drawing.Size(594, 470);
            this.Name = "MainForm";
            this.Text = "A Hat in Time Skin Maker";
            this.GbxGameInformation.ResumeLayout(false);
            this.GbxGameInformation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbxPreview)).EndInit();
            this.GbxSkinModInformation.ResumeLayout(false);
            this.GbxSkinModInformation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbxIcon)).EndInit();
            this.LytButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox GbxGameInformation;
        private System.Windows.Forms.TextBox TbxGameFolder;
        private System.Windows.Forms.Label LblGameFolder;
        private System.Windows.Forms.Button BtnUpdateGameFolder;
        private System.Windows.Forms.TextBox TbxEditorExecutablePath;
        private System.Windows.Forms.Label LblEditorExecutable;
        private System.Windows.Forms.Button BtnCreateMod;
        private System.Windows.Forms.RichTextBox TbxInfo;
        private System.Windows.Forms.Button BtnCompile;
        private System.Windows.Forms.Button BtnCook;
        private System.Windows.Forms.PictureBox PbxPreview;
        private System.Windows.Forms.GroupBox GbxSkinModInformation;
        private System.Windows.Forms.TextBox TbxModName;
        private System.Windows.Forms.Label LblModName;
        private System.Windows.Forms.ListBox LbxSkins;
        private System.Windows.Forms.TableLayoutPanel LytButtons;
        private System.Windows.Forms.Button BtnRemoveSkin;
        private System.Windows.Forms.Button BtnAddSkin;
        private System.Windows.Forms.Button BtnSelectIcon;
        private System.Windows.Forms.Label LblModIcon;
        private System.Windows.Forms.Label LblSkins;
        private System.Windows.Forms.PictureBox PbxIcon;
        private System.Windows.Forms.ToolTip TTip;
        private System.Windows.Forms.Button BtnChangeSkin;
    }
}

