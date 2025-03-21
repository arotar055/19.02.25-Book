using Microsoft.EntityFrameworkCore;

namespace AuthorsEF
{
    public class Model : IModel
    {
        public List<Author> Authors { get; set; } = new List<Author>();
        public List<Book> Books { get; set; } = new List<Book>();

        public Model()
        {
            LoadDataFromDb();
        }

        private void LoadDataFromDb()
        {
            using (var db = new MyAuthorsDbContext())
            {
                Authors = db.Authors
                             .Include(a => a.Books)
                             .ToList();

                Books = db.Books
                          .Include(b => b.Author)
                          .ToList();
            }
        }

        public void AddAuthor(Author author)
        {
            using (var db = new MyAuthorsDbContext())
            {
                db.Authors.Add(author);
                db.SaveChanges();
            }
            LoadDataFromDb();
        }

        public void UpdateAuthor(Author author)
        {
            using (var db = new MyAuthorsDbContext())
            {
                db.Authors.Update(author);
                db.SaveChanges();
            }
            LoadDataFromDb();
        }

        public void DeleteAuthor(Author author)
        {
            using (var db = new MyAuthorsDbContext())
            {
                var booksToRemove = db.Books.Where(b => b.AuthorId == author.Id).ToList();
                db.Books.RemoveRange(booksToRemove);
                db.Authors.Remove(author);
                db.SaveChanges();
            }
            LoadDataFromDb();
        }


        public void AddBook(Book book)
        {
            using (var db = new MyAuthorsDbContext())
            {
                db.Books.Add(book);
                db.SaveChanges();
            }
            LoadDataFromDb();
        }

        public void UpdateBook(Book book)
        {
            using (var db = new MyAuthorsDbContext())
            {
                db.Books.Update(book);
                db.SaveChanges();
            }
            LoadDataFromDb();
        }

        public void DeleteBook(Book book)
        {
            using (var db = new MyAuthorsDbContext())
            {
                db.Books.Remove(book);
                db.SaveChanges();
            }
            LoadDataFromDb();
        }

        public void LoadDataFromFile(string fileName) {}
        public void SaveDataToFile(string fileName) {}
    }
}