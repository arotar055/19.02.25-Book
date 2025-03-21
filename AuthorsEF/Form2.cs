using System;
using System.Windows.Forms;

namespace AuthorsEF
{
    public partial class Form2 : Form, IAuthorView
    {
        public Form2()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            button1.Click += (s, e) => SaveAuthor?.Invoke(this, EventArgs.Empty);
            button2.Click += (s, e) => Cancel?.Invoke(this, EventArgs.Empty);
        }

        public string AuthorName
        {
            get => textBox1.Text.Trim();
            set => textBox1.Text = value;
        }

        public event EventHandler SaveAuthor;
        public event EventHandler Cancel;

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void SetTitle(string title)
        {
            this.Text = title;
        }

        public void CloseView()
        {
            this.Close();
        }

        public void SetOperationSuccess(bool success)
        {
            this.DialogResult = success ? DialogResult.OK : DialogResult.Cancel;
        }
    }
}