using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AuthorsEF;

namespace AuthorsEF
{
    public partial class Form1 : Form, IMainView
    {
        private MenuStrip StripMenu;
        private ToolStripMenuItem fileMenu, optionsMenu;
        private ToolStripMenuItem addBookMenuItem, deleteBookMenuItem, editBookMenuItem;
        private ToolStripMenuItem openFileMenuItem, saveFileMenuItem, exitMenuItem;
        private ToolStripMenuItem addAuthorMenuItem, deleteAuthorMenuItem, editAuthorMenuItem;

        public Form1()
        { 
            InitializeMenu();
            Text = "Автор и книги";
            checkBox1.CheckedChanged += (s, e) => FilterByAuthorChanged?.Invoke(this, EventArgs.Empty);
            comboBox1.SelectedIndexChanged += (s, e) => SelectedAuthorChanged?.Invoke(this, EventArgs.Empty);
            this.KeyDown += (s, e) => FormKeyDown?.Invoke(this, e); 
            this.KeyUp += (s, e) => FormKeyUp?.Invoke(this, e); 
        }   

        public List<Author> Authors
        {
            get => comboBox1.Items.Cast<Author>().ToList();
            set
            {
                comboBox1.Items.Clear();
                comboBox1.Items.AddRange(value.ToArray());
            }
        }

        public List<Book> Books
        {
            get => listBox1.Items.Cast<Book>().ToList();
            set
            {
                listBox1.Items.Clear();
                listBox1.Items.AddRange(value.ToArray());
                listBox1.DisplayMember = "Title";
            }
        }

        public Author SelectedAuthor
        {
            get => comboBox1.SelectedItem as Author;
            set => comboBox1.SelectedItem = value;
        }

        public Book SelectedBook
        {
            get => listBox1.SelectedItem as Book;
            set => listBox1.SelectedItem = value;
        }

        public bool FilterByAuthor => checkBox1.Checked;
        public event EventHandler AddAuthor;
        public event EventHandler EditAuthor;
        public event EventHandler DeleteAuthor;
        public event EventHandler AddBook;
        public event EventHandler EditBook;
        public event EventHandler DeleteBook;
        public event EventHandler OpenFile;
        public event EventHandler SaveFile;
        public event EventHandler ExitApplication;
        public event EventHandler SelectedAuthorChanged;
        public event EventHandler FilterByAuthorChanged;
        public event KeyEventHandler FormKeyDown;
        public event KeyEventHandler FormKeyUp;

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public bool ConfirmAction(string message)
        {
            return MessageBox.Show(message, "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes;
        }

        public void RefreshView()
        {
            Refresh();
        }

        public bool ShowAuthorDialog(Author author, bool isNew)
        {
            using (var authorView = new Form2())
            {
                var authorPresenter = new AuthorPresenter(authorView, author, isNew);
                return authorView.ShowDialog() == DialogResult.OK;
            }
        }

        public bool ShowBookDialog(Book book, bool isNew)
        {
            using (var bookView = new Form3())
            {
                var bookPresenter = new BookPresenter(bookView, book, Authors, isNew);
                return bookView.ShowDialog() == DialogResult.OK;
            }
        }

        public string ShowOpenFileDialog()
        {
            using (var openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            })
            {
                return openFileDialog.ShowDialog() == DialogResult.OK ? openFileDialog.FileName : null;
            }
        }

        public string ShowSaveFileDialog()
        {
            using (var saveFileDialog = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            })
            {
                return saveFileDialog.ShowDialog() == DialogResult.OK ? saveFileDialog.FileName : null;
            }
        }

        public void CloseApplication()
        {
            Application.Exit();
        }


        private void InitializeMenu()
        {
            StripMenu = new MenuStrip();
            fileMenu = new ToolStripMenuItem("&File");
            openFileMenuItem = new ToolStripMenuItem("Open");
            openFileMenuItem.Click += (s, e) => OpenFile?.Invoke(this, EventArgs.Empty);
            fileMenu.DropDownItems.Add(openFileMenuItem);
            saveFileMenuItem = new ToolStripMenuItem("Save");
            saveFileMenuItem.Click += (s, e) => SaveFile?.Invoke(this, EventArgs.Empty);
            fileMenu.DropDownItems.Add(saveFileMenuItem);
            exitMenuItem = new ToolStripMenuItem("Exit");
            exitMenuItem.Click += (s, e) => ExitApplication?.Invoke(this, EventArgs.Empty);
            fileMenu.DropDownItems.Add(exitMenuItem);
            StripMenu.Items.Add(fileMenu);

            optionsMenu = new ToolStripMenuItem("&Options");
            addAuthorMenuItem = new ToolStripMenuItem("Add Author");
            addAuthorMenuItem.Click += (s, e) => AddAuthor?.Invoke(this, EventArgs.Empty);
            optionsMenu.DropDownItems.Add(addAuthorMenuItem);

            deleteAuthorMenuItem = new ToolStripMenuItem("Delete Author");
            deleteAuthorMenuItem.Click += (s, e) => DeleteAuthor?.Invoke(this, EventArgs.Empty);
            optionsMenu.DropDownItems.Add(deleteAuthorMenuItem);

            editAuthorMenuItem = new ToolStripMenuItem("Edit Author");
            editAuthorMenuItem.Click += (s, e) => EditAuthor?.Invoke(this, EventArgs.Empty);
            optionsMenu.DropDownItems.Add(editAuthorMenuItem);

            addBookMenuItem = new ToolStripMenuItem("Add Book");
            addBookMenuItem.Click += (s, e) => AddBook?.Invoke(this, EventArgs.Empty);
            optionsMenu.DropDownItems.Add(addBookMenuItem);

            deleteBookMenuItem = new ToolStripMenuItem("Delete Book");
            deleteBookMenuItem.Click += (s, e) => DeleteBook?.Invoke(this, EventArgs.Empty);
            optionsMenu.DropDownItems.Add(deleteBookMenuItem);

            editBookMenuItem = new ToolStripMenuItem("Edit Book");
            editBookMenuItem.Click += (s, e) => EditBook?.Invoke(this, EventArgs.Empty);
            optionsMenu.DropDownItems.Add(editBookMenuItem);

            StripMenu.Items.Add(optionsMenu);
            Controls.Add(StripMenu);
            MainMenuStrip = StripMenu;
        }
    }
}