namespace CountPV
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnSelect = new Button();
            txtFolderPath = new TextBox();
            btnActive = new Button();
            pictureBox2 = new PictureBox();
            txtLog = new RichTextBox();
            checkBox1 = new CheckBox();
            explainBox = new RichTextBox();
            share = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // btnSelect
            // 
            btnSelect.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point);
            btnSelect.Location = new Point(29, 41);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(120, 39);
            btnSelect.TabIndex = 0;
            btnSelect.Text = "选择文件";
            btnSelect.UseVisualStyleBackColor = true;
            btnSelect.Click += btnSelect_Click;
            // 
            // txtFolderPath
            // 
            txtFolderPath.Location = new Point(166, 46);
            txtFolderPath.Name = "txtFolderPath";
            txtFolderPath.ReadOnly = true;
            txtFolderPath.ScrollBars = ScrollBars.Horizontal;
            txtFolderPath.Size = new Size(677, 30);
            txtFolderPath.TabIndex = 1;
            txtFolderPath.TextChanged += txtFolderPath_TextChanged;
            // 
            // btnActive
            // 
            btnActive.BackColor = SystemColors.ActiveBorder;
            btnActive.BackgroundImageLayout = ImageLayout.Center;
            btnActive.Cursor = Cursors.Hand;
            btnActive.FlatStyle = FlatStyle.System;
            btnActive.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Bold, GraphicsUnit.Point);
            btnActive.ForeColor = SystemColors.ButtonHighlight;
            btnActive.Location = new Point(957, 41);
            btnActive.Name = "btnActive";
            btnActive.Size = new Size(194, 39);
            btnActive.TabIndex = 2;
            btnActive.Text = "开始执行";
            btnActive.UseVisualStyleBackColor = false;
            btnActive.Click += btnActive_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(869, 476);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(282, 272);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 6;
            pictureBox2.TabStop = false;
            // 
            // txtLog
            // 
            txtLog.BorderStyle = BorderStyle.None;
            txtLog.Location = new Point(29, 98);
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.Size = new Size(814, 728);
            txtLog.TabIndex = 8;
            txtLog.Text = "";
            txtLog.WordWrap = false;
            txtLog.TextChanged += txtLog_TextChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(861, 48);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(90, 28);
            checkBox1.TabIndex = 9;
            checkBox1.Text = "重命名";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // explainBox
            // 
            explainBox.BackColor = Color.WhiteSmoke;
            explainBox.BorderStyle = BorderStyle.None;
            explainBox.Location = new Point(869, 134);
            explainBox.Name = "explainBox";
            explainBox.ReadOnly = true;
            explainBox.Size = new Size(282, 295);
            explainBox.TabIndex = 10;
            explainBox.Text = "";
            explainBox.TextChanged += explainBox_TextChanged;
            // 
            // share
            // 
            share.BackColor = Color.WhiteSmoke;
            share.BorderStyle = BorderStyle.None;
            share.ForeColor = Color.Black;
            share.Location = new Point(29, 842);
            share.Multiline = true;
            share.Name = "share";
            share.Size = new Size(1128, 31);
            share.TabIndex = 11;
            share.Text = "交流群：156115036   编程小白   请大佬指教   Github地址：https://github.com/Muytec/CountPV";
            share.TextChanged += share_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1189, 885);
            Controls.Add(share);
            Controls.Add(checkBox1);
            Controls.Add(txtLog);
            Controls.Add(pictureBox2);
            Controls.Add(btnActive);
            Controls.Add(txtFolderPath);
            Controls.Add(btnSelect);
            Controls.Add(explainBox);
            Name = "Form1";
            Text = "PVT Counter v0.1.0";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSelect;
        private TextBox txtFolderPath;
        private Button btnActive;
        private PictureBox pictureBox2;
        private RichTextBox txtLog;
        private CheckBox checkBox1;
        private RichTextBox explainBox;
        private TextBox share;
    }
}