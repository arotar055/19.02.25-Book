namespace AuthorsEF
{
    public interface IModel
    {
        List<Author> Authors { get; set; }
        List<Book> Books { get; set; }
        void LoadDataFromFile(string fileName);
        void SaveDataToFile(string fileName);
        void AddAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(Author author);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
    }
}