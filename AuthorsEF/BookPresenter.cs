using AuthorsEF;

public class BookPresenter
{
    private readonly IBookView view;
    private readonly Book bk;
    private readonly List<Author> authors;
    private readonly bool CheckNew;

    public BookPresenter(IBookView view, Book book, List<Author> authors, bool CheckisNew)
    {
        this.view = view;
        bk = book;
        this.authors = authors;
        CheckNew = CheckisNew;

        this.view.BookTitle = bk.Title;
        this.view.SetTitle(CheckNew ? "Add Book" : "Edit Book");

        this.view.SaveBook += OnSaveBook;
        this.view.Cancel += OnCancel;
    }

    private void OnSaveBook(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(view.BookTitle))
        {
            view.ShowMessage("Book title cannot be empty.");
            return;
        }

        bk.Title = view.BookTitle;
        view.SetOperationSuccess(true);
        view.CloseView();
    }

    private void OnCancel(object sender, EventArgs e)
    {
        view.SetOperationSuccess(false);
        view.CloseView();
    }
}