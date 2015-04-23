namespace Apostiller_Project
{
    partial class Preference
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
            this.label1 = new System.Windows.Forms.Label();
            this.preferanceCapacity = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.preferanceOffice = new System.Windows.Forms.TextBox();
            this.preferanceDoc = new System.Windows.Forms.TextBox();
            this.okPreference = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cancelPreference = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Capacity";
            // 
            // preferanceCapacity
            // 
            this.preferanceCapacity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preferanceCapacity.Location = new System.Drawing.Point(134, 52);
            this.preferanceCapacity.Name = "preferanceCapacity";
            this.preferanceCapacity.Size = new System.Drawing.Size(276, 22);
            this.preferanceCapacity.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Kontor";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Dokument";
            // 
            // preferanceOffice
            // 
            this.preferanceOffice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preferanceOffice.Location = new System.Drawing.Point(134, 105);
            this.preferanceOffice.Name = "preferanceOffice";
            this.preferanceOffice.Size = new System.Drawing.Size(276, 22);
            this.preferanceOffice.TabIndex = 4;
            // 
            // preferanceDoc
            // 
            this.preferanceDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preferanceDoc.Location = new System.Drawing.Point(134, 162);
            this.preferanceDoc.Name = "preferanceDoc";
            this.preferanceDoc.Size = new System.Drawing.Size(276, 22);
            this.preferanceDoc.TabIndex = 5;
            // 
            // okPreference
            // 
            this.okPreference.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okPreference.Location = new System.Drawing.Point(27, 255);
            this.okPreference.Name = "okPreference";
            this.okPreference.Size = new System.Drawing.Size(102, 60);
            this.okPreference.TabIndex = 6;
            this.okPreference.Text = "Endre";
            this.okPreference.UseVisualStyleBackColor = true;
            this.okPreference.Click += new System.EventHandler(this.okPreference_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cancelPreference);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.okPreference);
            this.groupBox1.Controls.Add(this.preferanceCapacity);
            this.groupBox1.Controls.Add(this.preferanceDoc);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.preferanceOffice);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(23, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(459, 332);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preferanser";
            // 
            // cancelPreference
            // 
            this.cancelPreference.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelPreference.Location = new System.Drawing.Point(200, 255);
            this.cancelPreference.Name = "cancelPreference";
            this.cancelPreference.Size = new System.Drawing.Size(101, 60);
            this.cancelPreference.TabIndex = 7;
            this.cancelPreference.Text = "Avbryt";
            this.cancelPreference.UseVisualStyleBackColor = true;
            this.cancelPreference.Click += new System.EventHandler(this.cancelPreference_Click);
            // 
            // Preference
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 403);
            this.Controls.Add(this.groupBox1);
            this.Name = "Preference";
            this.Text = "Preferanser";
            this.Load += new System.EventHandler(this.Preference_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button okPreference;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button cancelPreference;
        public System.Windows.Forms.TextBox preferanceCapacity;
        public System.Windows.Forms.TextBox preferanceOffice;
        public System.Windows.Forms.TextBox preferanceDoc;
    }
}