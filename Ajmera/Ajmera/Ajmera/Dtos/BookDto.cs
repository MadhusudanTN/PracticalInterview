using System.ComponentModel.DataAnnotations;

namespace Ajmera.Dtos
{
    public class BookDto : BookPostDto
    {
        public BookDto(string name, string authorName) : base(name, authorName)
        {
            BookId = Guid.NewGuid();
        }

        public BookDto()
        {
        }

        public BookDto(BookPostDto bookPostDto) : base(bookPostDto.Name, bookPostDto.AuthorName)
        {
            BookId = Guid.NewGuid();
        }

        [NotNullGuid]
        public Guid BookId { get; set; }
    }

    public class NotNullGuidAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var guid = (Guid)value;
            if (guid == Guid.Empty)
            {
                return false;
            }
            return true;
        }
    }
}