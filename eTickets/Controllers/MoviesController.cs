using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;
        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var MoviesData = await _service.GetAllAsync(n => n.Cinema);
            return View(MoviesData);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var MoviesData = await _service.GetAllAsync(n => n.Cinema);
            if(!string.IsNullOrEmpty(searchString))
            {
                var filteredData = MoviesData.Where(n=>n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();
                return View("Index",filteredData);
            }
            return View("Index", MoviesData);
        }
        //GET Movie/Detaisl/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var MovieData = await _service.GetMovieByIdAsync(id);
            if (MovieData == null) return View("Not Found");
            return View(MovieData);
        }

        //Get Movie/Create
        public async Task<IActionResult> Create()
        {
            var movieDropDownData = await _service.NewMovieDropDownValues();
            ViewBag.Producers = new SelectList(movieDropDownData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropDownData.Actors, "Id", "FullName");
            ViewBag.Cinemas = new SelectList(movieDropDownData.Cinemas, "Id", "Name");

            return View();
        }

        //Post Movie/create
        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropDownData = await _service.NewMovieDropDownValues();
                ViewBag.Producers = new SelectList(movieDropDownData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropDownData.Actors, "Id", "FullName");
                ViewBag.Cinemas = new SelectList(movieDropDownData.Cinemas, "Id", "Name");
                return View(movie);
            }
            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        //Get Movie/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            if(movieDetails == null) return View("Not Found");

            var response = new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageURL = movieDetails.ImageURL,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorsIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList(),
            };

            var movieDropDownData = await _service.NewMovieDropDownValues();
            ViewBag.Producers = new SelectList(movieDropDownData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropDownData.Actors, "Id", "FullName");
            ViewBag.Cinemas = new SelectList(movieDropDownData.Cinemas, "Id", "Name");

            return View(response);
        }

        //Post Movie/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id,NewMovieVM movie)
        {
            if (id != movie.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var movieDropDownData = await _service.NewMovieDropDownValues();
                ViewBag.Producers = new SelectList(movieDropDownData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropDownData.Actors, "Id", "FullName");
                ViewBag.Cinemas = new SelectList(movieDropDownData.Cinemas, "Id", "Name");
                return View(movie);
            }
            await _service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
    }
}
