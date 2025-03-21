public interface IAuthorView
{
    string AuthorName { get; set; }
    event EventHandler SaveAuthor;
    event EventHandler Cancel;
    void ShowMessage(string message);
    void SetTitle(string title);
    void CloseView();
    void SetOperationSuccess(bool success);
}