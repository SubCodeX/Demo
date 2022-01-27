namespace winAsciiScrub
{
    partial class ruleAdd
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
            this.charList = new System.Windows.Forms.ComboBox();
            this.substList = new System.Windows.Forms.ComboBox();
            this.OK = new System.Windows.Forms.Button();
            this.Cansel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // charList
            // 
            this.charList.FormattingEnabled = true;
            this.charList.Location = new System.Drawing.Point(13, 13);
            this.charList.Name = "charList";
            this.charList.Size = new System.Drawing.Size(164, 21);
            this.charList.TabIndex = 0;
            // 
            // substList
            // 
            this.substList.FormattingEnabled = true;
            this.substList.Location = new System.Drawing.Point(183, 13);
            this.substList.Name = "substList";
            this.substList.Size = new System.Drawing.Size(165, 21);
            this.substList.TabIndex = 1;
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(13, 41);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(164, 23);
            this.OK.TabIndex = 2;
            this.OK.Text = "Add";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // Cansel
            // 
            this.Cansel.Location = new System.Drawing.Point(184, 41);
            this.Cansel.Name = "Cansel";
            this.Cansel.Size = new System.Drawing.Size(164, 23);
            this.Cansel.TabIndex = 3;
            this.Cansel.Text = "Cancel";
            this.Cansel.UseVisualStyleBackColor = true;
            this.Cansel.Click += new System.EventHandler(this.Cansel_Click);
            // 
            // ruleAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 80);
            this.Controls.Add(this.Cansel);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.substList);
            this.Controls.Add(this.charList);
            this.Name = "ruleAdd";
            this.Text = "ruleAdd";
            this.Load += new System.EventHandler(this.ruleAdd_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox charList;
        private System.Windows.Forms.ComboBox substList;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cansel;
    }
}