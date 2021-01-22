using System;
using System.Windows.Forms;
using System.Drawing;

namespace Endecrypter_1_0
{

	/// <summary>
	/// Summary for About
	///
	/// WARNING: If you change the name of this class, you will need to change the
	///          'Resource File Name' property for the managed resource compiler tool
	///          associated with all .resx files this class depends on.  Otherwise,
	///          the designers will not be able to interact properly with localized
	///          resources associated with this form.
	/// </summary>
	public class About : System.Windows.Forms.Form
	{
		public About()
		{
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~About()
		{
			if (components != null)
			{
				components = null;
			}
		}
	private System.Windows.Forms.Button button1;
	private System.Windows.Forms.Label key_enc_lbl;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components;

#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.button1 = new System.Windows.Forms.Button();
            this.key_enc_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(97, 143);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // key_enc_lbl
            // 
            this.key_enc_lbl.AutoSize = true;
            this.key_enc_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.key_enc_lbl.Location = new System.Drawing.Point(71, 22);
            this.key_enc_lbl.Name = "key_enc_lbl";
            this.key_enc_lbl.Size = new System.Drawing.Size(118, 104);
            this.key_enc_lbl.TabIndex = 2;
            this.key_enc_lbl.Text = "Demo Purpose Only\r\n\r\nFile_Encrypter_1.0\r\n\r\n      \n      progneilae\r\n\r\n     (c) 20" +
    "12-13\r\n";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 173);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.key_enc_lbl);
            this.MaximumSize = new System.Drawing.Size(274, 211);
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
#endregion
	private void button1_Click(object sender, System.EventArgs e)
	{
				 this.Close();
	}


	}
}
