namespace MainForm
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
            this.display = new System.Windows.Forms.PictureBox();
            this.Start = new System.Windows.Forms.Button();
            this.EngineButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.display)).BeginInit();
            this.SuspendLayout();
            // 
            // display
            // 
            this.display.Location = new System.Drawing.Point(12, 12);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(1000, 640);
            this.display.TabIndex = 0;
            this.display.TabStop = false;
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(1128, 50);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(151, 23);
            this.Start.TabIndex = 1;
            this.Start.Text = "Start RRT";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // EngineButton
            // 
            this.EngineButton.Location = new System.Drawing.Point(1128, 113);
            this.EngineButton.Name = "EngineButton";
            this.EngineButton.Size = new System.Drawing.Size(151, 23);
            this.EngineButton.TabIndex = 2;
            this.EngineButton.Text = "Start engine";
            this.EngineButton.UseVisualStyleBackColor = true;
            this.EngineButton.Click += new System.EventHandler(this.EngineButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 672);
            this.Controls.Add(this.EngineButton);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.display);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.display)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox display;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button EngineButton;
    }
}

