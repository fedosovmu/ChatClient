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
    public partial class ChatForm : Form
    {
        
        private ClientNet Chat; 
        private string nickname;
        public ChatForm(string nickname, string hostIp, int port)
        {
            Chat = new ClientNet(nickname, hostIp, port);
            this.Size = new Size(600,500);

            RichTextBox ChatLogBox = new RichTextBox();
            ChatLogBox.Location = new Point(0, 60);
            ChatLogBox.Size = new Size(585, 250);
            ChatLogBox.ReadOnly = true;
            Controls.Add(ChatLogBox);

            Label ChatHead = new Label();
            ChatHead.Location = new Point(0, ChatLogBox.Top-20);
            ChatHead.Text = "Лог:";
            Controls.Add(ChatHead);

            Label NameHead = new Label();
            NameHead.Location = new Point(0, 10);
            NameHead.Text = String.Format("{0}: {1}", "Вы вошли как", nickname);
            Controls.Add(NameHead);

            RichTextBox MsgBox = new RichTextBox();
            MsgBox.Location = new Point(0,Bottom-180);
            MsgBox.Size = new Size(585, 100);
            Controls.Add(MsgBox);

            Button SendButton = new Button();
            SendButton.Location = new Point(0, MsgBox.Bottom);
            SendButton.Size = new Size(160,40);
            SendButton.Text = "Отправить";
            Controls.Add(SendButton);

            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            SendButton.Click += (sender, args) =>
            {
                Chat.SendMessage(MsgBox.Text);
                MsgBox.Text = "";
            };

            Chat.MsgRecievedEvent += (data) =>
            {
                BeginInvoke(new Action(() => ChatLogBox.AppendText(data + System.Environment.NewLine)));
            };

            this.FormClosed += (sender, args) =>
            {
                Environment.Exit(0);
            };
        }
        
    }
}
