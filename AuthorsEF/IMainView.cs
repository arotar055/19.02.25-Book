using AuthorsEF;

public interface IMainView
{
    List<Author> Authors { get; set; }
    List<Book> Books { get; set; }
    Author SelectedAuthor { get; set; }
    Book SelectedBook { get; set; }
    bool FilterByAuthor { get; }

   
    event EventHandler DeleteBook;
    event EventHandler OpenFile;
    event EventHandler SaveFile;
    event EventHandler ExitApplication;
    event EventHandler AddAuthor;
    event EventHandler EditAuthor;
    event EventHandler DeleteAuthor;
    event EventHandler AddBook;
    event EventHandler EditBook;
    event EventHandler SelectedAuthorChanged;
    event EventHandler FilterByAuthorChanged;
    event KeyEventHandler FormKeyDown;
    event KeyEventHandler FormKeyUp;

    void ShowMessage(string message);
    bool ConfirmAction(string message);
    void RefreshView();
    bool ShowAuthorDialog(Author author, bool isNew);
    bool ShowBookDialog(Book book, bool isNew);
    string ShowOpenFileDialog();
    string ShowSaveFileDialog();
    void CloseApplication();
}