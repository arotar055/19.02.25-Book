namespace AuthorsEF
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();

        public override string ToString()
        {
            return Name;
        }
    }
}