namespace PublisherWin
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
            btnZhihu = new Button();
            tbxMdFile = new TextBox();
            btnSelect = new Button();
            btnCnblogs = new Button();
            button1 = new Button();
            button2 = new Button();
            txtLog = new TextBox();
            SuspendLayout();
            // 
            // btnZhihu
            // 
            btnZhihu.Location = new Point(46, 127);
            btnZhihu.Name = "btnZhihu";
            btnZhihu.Size = new Size(219, 131);
            btnZhihu.TabIndex = 0;
            btnZhihu.Text = "ZHIHU";
            btnZhihu.UseVisualStyleBackColor = true;
            btnZhihu.Click += button1_Click;
            // 
            // tbxMdFile
            // 
            tbxMdFile.Location = new Point(46, 42);
            tbxMdFile.Name = "tbxMdFile";
            tbxMdFile.Size = new Size(1195, 30);
            tbxMdFile.TabIndex = 1;
            // 
            // btnSelect
            // 
            btnSelect.Location = new Point(1247, 42);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(143, 30);
            btnSelect.TabIndex = 2;
            btnSelect.Text = "SELECT";
            btnSelect.UseVisualStyleBackColor = true;
            btnSelect.Click += button2_Click;
            // 
            // btnCnblogs
            // 
            btnCnblogs.Location = new Point(318, 127);
            btnCnblogs.Name = "btnCnblogs";
            btnCnblogs.Size = new Size(219, 131);
            btnCnblogs.TabIndex = 3;
            btnCnblogs.Text = "Cnblogs";
            btnCnblogs.UseVisualStyleBackColor = true;
            btnCnblogs.Click += button1_Click_1;
            // 
            // button1
            // 
            button1.Location = new Point(1171, 127);
            button1.Name = "button1";
            button1.Size = new Size(219, 131);
            button1.TabIndex = 4;
            button1.Text = "MdNice";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_2;
            // 
            // button2
            // 
            button2.Location = new Point(592, 127);
            button2.Name = "button2";
            button2.Size = new Size(219, 131);
            button2.TabIndex = 5;
            button2.Text = "Wechat";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click_1;
            // 
            // txtLog
            // 
            txtLog.Location = new Point(46, 335);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.Size = new Size(1344, 571);
            txtLog.TabIndex = 6;
            txtLog.Text = " playwright codegen https://mp.weixin.qq.com/";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1413, 930);
            Controls.Add(txtLog);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(btnCnblogs);
            Controls.Add(btnSelect);
            Controls.Add(tbxMdFile);
            Controls.Add(btnZhihu);
            Name = "Form1";
            Text = "Publisher";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnZhihu;
        private TextBox tbxMdFile;
        private Button btnSelect;
        private Button btnCnblogs;
        private Button button1;
        private Button button2;
        private TextBox txtLog;
    }
}
