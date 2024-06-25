using eTickets.Data.Base;
using eTickets.Data.Enums;
using eTickets.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class NewMovieDropDownVM
{
    public NewMovieDropDownVM()
    {
        Producers = new List<Producer>();
        Actors = new List<Actor>();
        Cinemas = new List<Cinema>();
    }

    public List<Producer> Producers { get; set; }
    public List<Actor> Actors { get; set; }
    public List<Cinema> Cinemas { get; set; }


}
