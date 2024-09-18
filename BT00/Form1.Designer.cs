
namespace BT00
{
    partial class Form1
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
            this.btnAccess2003 = new System.Windows.Forms.Button();
            this.btnAccess2019 = new System.Windows.Forms.Button();
            this.btnSQLW = new System.Windows.Forms.Button();
            this.btnSQLsa = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAccess2003
            // 
            this.btnAccess2003.Location = new System.Drawing.Point(34, 39);
            this.btnAccess2003.Name = "btnAccess2003";
            this.btnAccess2003.Size = new System.Drawing.Size(140, 91);
            this.btnAccess2003.TabIndex = 0;
            this.btnAccess2003.Text = "Access2003";
            this.btnAccess2003.UseVisualStyleBackColor = true;
            this.btnAccess2003.Click += new System.EventHandler(this.btnAccess2003_Click);
            // 
            // btnAccess2019
            // 
            this.btnAccess2019.Location = new System.Drawing.Point(202, 39);
            this.btnAccess2019.Name = "btnAccess2019";
            this.btnAccess2019.Size = new System.Drawing.Size(140, 91);
            this.btnAccess2019.TabIndex = 0;
            this.btnAccess2019.Text = "Access2019";
            this.btnAccess2019.UseVisualStyleBackColor = true;
            this.btnAccess2019.Click += new System.EventHandler(this.btnAccess2019_Click);
            // 
            // btnSQLW
            // 
            this.btnSQLW.Location = new System.Drawing.Point(370, 39);
            this.btnSQLW.Name = "btnSQLW";
            this.btnSQLW.Size = new System.Drawing.Size(140, 91);
            this.btnSQLW.TabIndex = 0;
            this.btnSQLW.Text = "SQL Windows";
            this.btnSQLW.UseVisualStyleBackColor = true;
            // 
            // btnSQLsa
            // 
            this.btnSQLsa.Location = new System.Drawing.Point(538, 39);
            this.btnSQLsa.Name = "btnSQLsa";
            this.btnSQLsa.Size = new System.Drawing.Size(140, 91);
            this.btnSQLsa.TabIndex = 0;
            this.btnSQLsa.Text = "SQL sa";
            this.btnSQLsa.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(706, 175);
            this.Controls.Add(this.btnSQLsa);
            this.Controls.Add(this.btnSQLW);
            this.Controls.Add(this.btnAccess2019);
            this.Controls.Add(this.btnAccess2003);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAccess2003;
        private System.Windows.Forms.Button btnAccess2019;
        private System.Windows.Forms.Button btnSQLW;
        private System.Windows.Forms.Button btnSQLsa;
    }
}

