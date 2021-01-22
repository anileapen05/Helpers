using System;
using System.Windows.Forms;
using System.Drawing;

namespace File_Archiver_1_0
{

	/// <summary>
	/// Summary for Message_Box
	///
	/// WARNING: If you change the name of this class, you will need to change the
	///          'Resource File Name' property for the managed resource compiler tool
	///          associated with all .resx files this class depends on.  Otherwise,
	///          the designers will not be able to interact properly with localized
	///          resources associated with this form.
	/// </summary>
	public class Message_Box : System.Windows.Forms.Form
	{
		public Message_Box()
		{
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
		}

        public void show_Message_Box_Dialog(string message)
        {
            this.textBox1.Text = message;

            this.ShowDialog();
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~Message_Box()
		{
			if (components != null)
			{
				components = null;
			}
		}
	public System.Windows.Forms.TextBox textBox1;

	private System.Windows.Forms.Button close_but;

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
			this.textBox1 = (new System.Windows.Forms.TextBox());
			this.close_but = (new System.Windows.Forms.Button());
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.Color.Black;
			this.textBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.textBox1.ForeColor = System.Drawing.Color.Yellow;
			this.textBox1.Location = new System.Drawing.Point(12, 12);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(268, 239);
			this.textBox1.TabIndex = 0;
			// 
			// close_but
			// 
            this.close_but.Location = new System.Drawing.Point(109, 257);
			this.close_but.Name = "close_but";
            this.close_but.Size = new System.Drawing.Size(75, 23);
			this.close_but.TabIndex = 1;
			this.close_but.Text = "Close";
			this.close_but.UseVisualStyleBackColor = true;
			this.close_but.Click += new System.EventHandler(close_but_Click);
			// 
			// Message_Box
			// 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6, 13);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(292, 284);
			this.ControlBox = false;
			this.Controls.Add(this.close_but);
			this.Controls.Add(this.textBox1);
			this.Name = "Message_Box";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Message_Box";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
#endregion
	private void close_but_Click(object sender, System.EventArgs e)
	{

				 this.Close();
	}
	}
}
