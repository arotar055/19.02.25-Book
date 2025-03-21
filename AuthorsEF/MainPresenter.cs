using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace AuthorsEF
{
    public class MainPresenter
    {
        private readonly IMainView view;
        private readonly IModel modl;
        private bool press = false;

        public MainPresenter(IMainView view, IModel model)
        {
            this.view = view;
            modl = model;
            this.view.AddAuthor += OnAddAuthor;
            this.view.EditAuthor += OnEditAuthor;
            this.view.DeleteAuthor += OnDeleteAuthor;
            this.view.AddBook += OnAddBook;
            this.view.EditBook += OnEditBook;
            this.view.DeleteBook += OnDeleteBook;
            this.view.ExitApplication += OnExitApplication;
            this.view.SelectedAuthorChanged += OnSelectedAuthorChanged;
            this.view.FilterByAuthorChanged += OnFilterByAuthorChanged;
            this.view.FormKeyDown += OnFormKeyDown;
            this.view.FormKeyUp += OnFormKeyUp;
            UpdateAuthorList();
            UpdateBookList();
        }

        private void OnAddAuthor(object sender, EventArgs e)
        {
            var author = new Author();
            if (view.ShowAuthorDialog(author, true))
            {
                modl.AddAuthor(author);
                UpdateAuthorList();
                view.SelectedAuthor = author;
            }
        }

        private void OnEditAuthor(object sender, EventArgs e)
        {
            if (view.SelectedAuthor == null)
            {
                view.ShowMessage("Please select an author to edit.");
                return;
            }

            var editedAuthor = view.SelectedAuthor;
            if (view.ShowAuthorDialog(editedAuthor, false))
            {
                modl.UpdateAuthor(editedAuthor);
                UpdateAuthorList();
                view.SelectedAuthor = editedAuthor;
            }
        }

        private void OnDeleteAuthor(object sender, EventArgs e)
        {
            if (view.SelectedAuthor == null)
                return;

            if (view.ConfirmAction("Delete author and all their books?"))
            {
                modl.DeleteAuthor(view.SelectedAuthor);
                UpdateAuthorList();
                UpdateBookList();
            }
        }

        private void OnAddBook(object sender, EventArgs e)
        {
            if (view.SelectedAuthor == null)
            {
                view.ShowMessage("Please select an author before adding a book.");
                return;
            }
            var book = new Book
            {
                AuthorId = view.SelectedAuthor.Id
            };
            if (view.ShowBookDialog(book, true))
            {
                modl.AddBook(book);
                UpdateBookList();
            }
        }

        private void OnEditBook(object sender, EventArgs e)
        {
            if (view.SelectedBook == null)
            {
                view.ShowMessage("Please select a book to edit.");
                return;
            }
            var editedBook = view.SelectedBook;
            if (view.ShowBookDialog(editedBook, false))
            {
                modl.UpdateBook(editedBook);
                UpdateBookList();
            }
        }

        private void OnDeleteBook(object sender, EventArgs e)
        {
            if (view.SelectedBook == null)
            {
                view.ShowMessage("Please select a book to delete.");
                return;
            }

            if (view.ConfirmAction("Delete this book?"))
            {
                modl.DeleteBook(view.SelectedBook);
                UpdateBookList();
            }
        }


        private void OnOpenFile(object sender, EventArgs e)
        {
            string fileName = view.ShowOpenFileDialog();
            if (!string.IsNullOrEmpty(fileName))
            {
                modl.LoadDataFromFile(fileName);
                UpdateAuthorList();
                UpdateBookList();
            }
        }

        private void OnSaveFile(object sender, EventArgs e)
        {
            string fileName = view.ShowSaveFileDialog();
            if (!string.IsNullOrEmpty(fileName))
            {
                modl.SaveDataToFile(fileName);
            }
        }

        private void OnExitApplication(object sender, EventArgs e)
        {
            view.CloseApplication();
        }


        private void OnSelectedAuthorChanged(object sender, EventArgs e)
        {
            if (view.FilterByAuthor)
                UpdateBookList();
        }

        private void OnFilterByAuthorChanged(object sender, EventArgs e)
        {
            UpdateBookList();
        }

        private void OnFormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Menu && !press)
            {
                press = true;
                view.RefreshView();
            }
        }

        private void OnFormKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Menu && press)
            {
                press = false;
                view.RefreshView();
            }
        }

        private void UpdateAuthorList()
        {
            view.Authors = modl.Authors;
            if (modl.Authors.Any())
                view.SelectedAuthor ??= modl.Authors.First();
        }

        private void UpdateBookList()
        {
            if (view.FilterByAuthor && view.SelectedAuthor != null)
                view.Books = modl.Books.Where(b => b.AuthorId == view.SelectedAuthor.Id).ToList();
            else
                view.Books = modl.Books;
        }
    }
}