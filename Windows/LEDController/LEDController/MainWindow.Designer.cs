namespace LEDController
{
	partial class MainWindow
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
			this.Btn_Red = new System.Windows.Forms.Button();
			this.Btn_Blue = new System.Windows.Forms.Button();
			this.Btn_Green = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// Btn_Red
			// 
			this.Btn_Red.Location = new System.Drawing.Point(32, 41);
			this.Btn_Red.Name = "Btn_Red";
			this.Btn_Red.Size = new System.Drawing.Size(75, 23);
			this.Btn_Red.TabIndex = 0;
			this.Btn_Red.Text = "RED";
			this.Btn_Red.UseVisualStyleBackColor = true;
			this.Btn_Red.Click += new System.EventHandler(this.Btn_Red_Click);
			// 
			// Btn_Blue
			// 
			this.Btn_Blue.Location = new System.Drawing.Point(32, 70);
			this.Btn_Blue.Name = "Btn_Blue";
			this.Btn_Blue.Size = new System.Drawing.Size(75, 23);
			this.Btn_Blue.TabIndex = 1;
			this.Btn_Blue.Text = "BLUE";
			this.Btn_Blue.UseVisualStyleBackColor = true;
			this.Btn_Blue.Click += new System.EventHandler(this.Btn_Blue_Click);
			// 
			// Btn_Green
			// 
			this.Btn_Green.Location = new System.Drawing.Point(32, 99);
			this.Btn_Green.Name = "Btn_Green";
			this.Btn_Green.Size = new System.Drawing.Size(75, 23);
			this.Btn_Green.TabIndex = 2;
			this.Btn_Green.Text = "GREEN";
			this.Btn_Green.UseVisualStyleBackColor = true;
			this.Btn_Green.Click += new System.EventHandler(this.Btn_Green_Click);
			// 
			// MainWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.Btn_Green);
			this.Controls.Add(this.Btn_Blue);
			this.Controls.Add(this.Btn_Red);
			this.Name = "MainWindow";
			this.Text = "LED Controller Host";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button Btn_Blue;
		private System.Windows.Forms.Button Btn_Green;
	}
}

