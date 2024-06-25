using eTickets.Data.Base;
using eTickets.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class NewMovieVM
{
    public int Id { get; set; }
    [Display(Name = "Movie Name")]
    [Required(ErrorMessage = "Movie Name is required")]
    public string Name { get; set; }
    [Display(Name = "Movie Description")]
    [Required(ErrorMessage = "Movie Description is required")]
    public string Description { get; set; }
    [Display(Name = "Movie Price")]
    [Required(ErrorMessage = "Movie Price is required")]
    public double Price { get; set; }
    [Display(Name = "Movie Poster URL")]
    [Required(ErrorMessage = "Movie Poster URL is required")]
    public string ImageURL { get; set; }
    [Display(Name = "Movie StartDate")]
    [Required(ErrorMessage = "Start Date is required")]
    public DateTime StartDate { get; set; }
    [Display(Name = "Movie EndDate")]
    [Required(ErrorMessage = "End Date is required")]
    public DateTime EndDate { get; set; }
    [Display(Name = "Select a Category")]
    [Required(ErrorMessage = "Category is required")]
    public MovieCategory MovieCategory { get; set; }
    [Display(Name = "Select Actor(s)")]
    [Required(ErrorMessage = "Movie Actor(s) is required")]
    public List<int> ActorsIds { get; set; }
    [Display(Name = "Select a Producer")]
    [Required(ErrorMessage = "Movie Producer is required")]
    public int ProducerId { get; set; }
    [Display(Name = "Select a Cinema")]
    [Required(ErrorMessage = "Movie Cinema is required")]
    public int CinemaId { get; set; }

}
