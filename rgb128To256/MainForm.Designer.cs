namespace KinnectDataToHexAndXYZ
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
            this.RunSystem = new System.Windows.Forms.Button();
            this.OutPutBox = new System.Windows.Forms.RichTextBox();
            this.PathBox = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // RunSystem
            // 
            this.RunSystem.Location = new System.Drawing.Point(299, 265);
            this.RunSystem.Name = "RunSystem";
            this.RunSystem.Size = new System.Drawing.Size(404, 70);
            this.RunSystem.TabIndex = 0;
            this.RunSystem.Text = "Run";
            this.RunSystem.UseVisualStyleBackColor = true;
            this.RunSystem.Click += new System.EventHandler(this.RunSystem_Click);
            // 
            // OutPutBox
            // 
            this.OutPutBox.Location = new System.Drawing.Point(12, 12);
            this.OutPutBox.Name = "OutPutBox";
            this.OutPutBox.ReadOnly = true;
            this.OutPutBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.OutPutBox.Size = new System.Drawing.Size(271, 317);
            this.OutPutBox.TabIndex = 1;
            this.OutPutBox.Text = "";
            // 
            // PathBox
            // 
            this.PathBox.Location = new System.Drawing.Point(299, 239);
            this.PathBox.Name = "PathBox";
            this.PathBox.Size = new System.Drawing.Size(403, 20);
            this.PathBox.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Interval = 5;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 341);
            this.Controls.Add(this.PathBox);
            this.Controls.Add(this.OutPutBox);
            this.Controls.Add(this.RunSystem);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RunSystem;
        private System.Windows.Forms.RichTextBox OutPutBox;
        private System.Windows.Forms.TextBox PathBox;
        private System.Windows.Forms.Timer timer1;
    }
}