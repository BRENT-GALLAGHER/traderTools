﻿namespace traderTools
{
    partial class Swap
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
            this.FMEDcheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // FMEDcheckBox
            // 
            this.FMEDcheckBox.AutoSize = true;
            this.FMEDcheckBox.Location = new System.Drawing.Point(12, 12);
            this.FMEDcheckBox.Name = "FMEDcheckBox";
            this.FMEDcheckBox.Size = new System.Drawing.Size(122, 21);
            this.FMEDcheckBox.TabIndex = 0;
            this.FMEDcheckBox.Text = "FMED Failures";
            this.FMEDcheckBox.UseVisualStyleBackColor = true;
            // 
            // Swap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 317);
            this.Controls.Add(this.FMEDcheckBox);
            this.Name = "Swap";
            this.Text = "Swap";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox FMEDcheckBox;
    }
}