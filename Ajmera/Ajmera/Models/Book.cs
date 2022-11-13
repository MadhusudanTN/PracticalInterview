namespace Ajmera.Models;

public partial class Book
{
    public Guid BookId { get; set; }

    public string BookName { get; set; }

    public string AuthorName { get; set; }

    public DateTime CreateTs { get; set; }

    public DateTime UpdateTs { get; set; }
}