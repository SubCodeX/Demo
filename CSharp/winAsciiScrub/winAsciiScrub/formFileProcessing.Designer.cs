namespace winAsciiScrub
{
    partial class formFileProcessing
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fileInfoBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bttnSetRule = new System.Windows.Forms.Button();
            this.sCharSetList = new System.Windows.Forms.ListBox();
            this.oCharSetList = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fileInfoBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(634, 141);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File Information";
            // 
            // fileInfoBox
            // 
            this.fileInfoBox.Location = new System.Drawing.Point(7, 20);
            this.fileInfoBox.Multiline = true;
            this.fileInfoBox.Name = "fileInfoBox";
            this.fileInfoBox.Size = new System.Drawing.Size(621, 115);
            this.fileInfoBox.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.bttnSetRule);
            this.groupBox2.Controls.Add(this.sCharSetList);
            this.groupBox2.Controls.Add(this.oCharSetList);
            this.groupBox2.Location = new System.Drawing.Point(12, 159);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(634, 384);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Scrubbing Options";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(7, 341);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(348, 37);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = "Note that values above 127 (0x7F) depends on character table/page.  Characters in" +
    " list above might not be the originaly intended character.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(361, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Rule Options";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(361, 313);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(109, 23);
            this.button6.TabIndex = 8;
            this.button6.Text = "Cancel";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(361, 284);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(109, 23);
            this.button5.TabIndex = 7;
            this.button5.Text = "Process File";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(361, 104);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(109, 23);
            this.button4.TabIndex = 6;
            this.button4.Text = "Save Rules";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(361, 75);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(109, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Load Rules";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(473, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Character Substitute Ruleset";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(257, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Dec | Hex | Count | Explanation --- Original Characters";
            // 
            // bttnSetRule
            // 
            this.bttnSetRule.Location = new System.Drawing.Point(361, 46);
            this.bttnSetRule.Name = "bttnSetRule";
            this.bttnSetRule.Size = new System.Drawing.Size(109, 23);
            this.bttnSetRule.TabIndex = 1;
            this.bttnSetRule.Text = "Add / Change";
            this.bttnSetRule.UseVisualStyleBackColor = true;
            this.bttnSetRule.Click += new System.EventHandler(this.button1_Click);
            // 
            // sCharSetList
            // 
            this.sCharSetList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sCharSetList.FormattingEnabled = true;
            this.sCharSetList.Location = new System.Drawing.Point(476, 46);
            this.sCharSetList.Name = "sCharSetList";
            this.sCharSetList.Size = new System.Drawing.Size(152, 290);
            this.sCharSetList.TabIndex = 1;
            // 
            // oCharSetList
            // 
            this.oCharSetList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oCharSetList.FormattingEnabled = true;
            this.oCharSetList.Location = new System.Drawing.Point(7, 46);
            this.oCharSetList.Name = "oCharSetList";
            this.oCharSetList.Size = new System.Drawing.Size(348, 290);
            this.oCharSetList.TabIndex = 0;
            // 
            // formFileProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 555);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "formFileProcessing";
            this.Text = "File Processing Options";
            this.Load += new System.EventHandler(this.formFileProcessing_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox fileInfoBox;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bttnSetRule;
        private System.Windows.Forms.ListBox sCharSetList;
        private System.Windows.Forms.ListBox oCharSetList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
    }
}