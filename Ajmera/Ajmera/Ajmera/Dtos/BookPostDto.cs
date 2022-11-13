using System.ComponentModel.DataAnnotations;

namespace Ajmera.Dtos
{
    public class BookPostDto
    {
        public BookPostDto(string name, string authorName)
        {
            Name = name;
            AuthorName = authorName;
        }

        public BookPostDto()
        {
        }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string AuthorName { get; set; }
    }
}