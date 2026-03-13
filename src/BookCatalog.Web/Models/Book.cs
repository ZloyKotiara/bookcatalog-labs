using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Web.Models;

public class Book
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Введите название")]
    [StringLength(150)]
    [Display(Name = "Название")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите автора")]
    [StringLength(120)]
    [Display(Name = "Автор")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите жанр")]
    [StringLength(100)]
    [Display(Name = "Жанр")]
    public string Genre { get; set; } = string.Empty;

    [Range(1, 3000, ErrorMessage = "Введите корректный год")]
    [Display(Name = "Год")]
    public int Year { get; set; }
}
