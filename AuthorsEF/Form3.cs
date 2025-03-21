using System;
using System.Windows.Forms;

namespace AuthorsEF
{
    public partial class Form3 : Form, IBookView
    {
        public Form3()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            button1.Click += (s, e) => SaveBook?.Invoke(this, EventArgs.Empty);
            button2.Click += (s, e) => Cancel?.Invoke(this, EventArgs.Empty);
        }

        public string BookTitle
        {
            get => textBox2.Text.Trim();
            set => textBox2.Text = value;
        }

        public event EventHandler SaveBook;
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

        private void textBox2_TextChanged(object sender, EventArgs e) { }
    }
}