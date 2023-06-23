using System.ComponentModel;

namespace ASCII_Graphics
{
    partial class OutForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.OutImageBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.OutImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // OutImageBox
            // 
            this.OutImageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OutImageBox.Location = new System.Drawing.Point(0, 0);
            this.OutImageBox.Name = "OutImageBox";
            this.OutImageBox.Size = new System.Drawing.Size(800, 450);
            this.OutImageBox.TabIndex = 0;
            this.OutImageBox.TabStop = false;
            // 
            // OutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.OutImageBox);
            this.Name = "OutForm";
            this.Text = "OutForm";
            ((System.ComponentModel.ISupportInitialize)(this.OutImageBox)).EndInit();
            this.ResumeLayout(false);
        }

        public System.Windows.Forms.PictureBox OutImageBox;

        #endregion
    }
}