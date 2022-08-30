namespace WindowsFormsApp1
{
    partial class frmCar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCar));
            this.Arduino = new System.IO.Ports.SerialPort(this.components);
            this.btnAlaram = new System.Windows.Forms.Button();
            this.btnNormal = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbxBlackBox = new System.Windows.Forms.ListBox();
            this.lblInsideTemp = new System.Windows.Forms.Label();
            this.lblHeadlights = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBlackBox = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Arduino
            // 
            this.Arduino.PortName = "COM4";
            // 
            // btnAlaram
            // 
            this.btnAlaram.BackColor = System.Drawing.Color.Red;
            this.btnAlaram.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlaram.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAlaram.Location = new System.Drawing.Point(12, 12);
            this.btnAlaram.Name = "btnAlaram";
            this.btnAlaram.Size = new System.Drawing.Size(289, 198);
            this.btnAlaram.TabIndex = 0;
            this.btnAlaram.Text = "ALARM";
            this.btnAlaram.UseVisualStyleBackColor = false;
            this.btnAlaram.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnNormal
            // 
            this.btnNormal.BackColor = System.Drawing.Color.LawnGreen;
            this.btnNormal.Location = new System.Drawing.Point(12, 216);
            this.btnNormal.Name = "btnNormal";
            this.btnNormal.Size = new System.Drawing.Size(289, 57);
            this.btnNormal.TabIndex = 1;
            this.btnNormal.Text = "Disabled alarm\r\n(back to normal mode)";
            this.btnNormal.UseVisualStyleBackColor = false;
            this.btnNormal.Click += new System.EventHandler(this.btnNormal_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbxBlackBox
            // 
            this.lbxBlackBox.FormattingEnabled = true;
            this.lbxBlackBox.ItemHeight = 16;
            this.lbxBlackBox.Location = new System.Drawing.Point(421, 44);
            this.lbxBlackBox.Name = "lbxBlackBox";
            this.lbxBlackBox.Size = new System.Drawing.Size(201, 356);
            this.lbxBlackBox.TabIndex = 2;
            // 
            // lblInsideTemp
            // 
            this.lblInsideTemp.AutoSize = true;
            this.lblInsideTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsideTemp.Location = new System.Drawing.Point(7, 299);
            this.lblInsideTemp.Name = "lblInsideTemp";
            this.lblInsideTemp.Size = new System.Drawing.Size(306, 24);
            this.lblInsideTemp.TabIndex = 3;
            this.lblInsideTemp.Text = "The temperatrure inside the car is...";
            this.lblInsideTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHeadlights
            // 
            this.lblHeadlights.AutoSize = true;
            this.lblHeadlights.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeadlights.Location = new System.Drawing.Point(7, 326);
            this.lblHeadlights.Name = "lblHeadlights";
            this.lblHeadlights.Size = new System.Drawing.Size(200, 24);
            this.lblHeadlights.TabIndex = 4;
            this.lblHeadlights.Text = "Headlights status: OFF";
            this.lblHeadlights.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(418, 403);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 46);
            this.label1.TabIndex = 5;
            this.label1.Text = "The headlights status is updated every 5 seconds.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblBlackBox
            // 
            this.lblBlackBox.AutoSize = true;
            this.lblBlackBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlackBox.Location = new System.Drawing.Point(470, 17);
            this.lblBlackBox.Name = "lblBlackBox";
            this.lblBlackBox.Size = new System.Drawing.Size(92, 24);
            this.lblBlackBox.TabIndex = 6;
            this.lblBlackBox.Text = "Black box";
            this.lblBlackBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBlackBox.Click += new System.EventHandler(this.label2_Click);
            // 
            // frmCar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(647, 450);
            this.Controls.Add(this.lblBlackBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblHeadlights);
            this.Controls.Add(this.lblInsideTemp);
            this.Controls.Add(this.lbxBlackBox);
            this.Controls.Add(this.btnNormal);
            this.Controls.Add(this.btnAlaram);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCar";
            this.Text = "BMW Bordcomputer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort Arduino;
        private System.Windows.Forms.Button btnAlaram;
        private System.Windows.Forms.Button btnNormal;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListBox lbxBlackBox;
        private System.Windows.Forms.Label lblInsideTemp;
        private System.Windows.Forms.Label lblHeadlights;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBlackBox;
    }
}

