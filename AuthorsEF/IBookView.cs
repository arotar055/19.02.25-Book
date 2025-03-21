public interface IBookView
{
    string BookTitle { get; set; }
    event EventHandler SaveBook;
    event EventHandler Cancel;
    void ShowMessage(string message);
    void SetTitle(string title);
    void CloseView();
    void SetOperationSuccess(bool success);
}