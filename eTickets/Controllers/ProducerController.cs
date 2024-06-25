using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProducerController : Controller
    {
        private readonly IProducersService _service;
        public ProducerController(IProducersService Service)
        {
            _service = Service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var ProducerData = await _service.GetAllAsync();
            return View(ProducerData);
        }

        //GET Producer/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var ProducerData = await _service.GetByIdAsync(id);
            if (ProducerData == null) return View("Not Found");
            return View(ProducerData);
        }

        //GET producer/create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await _service.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }

        //producer/edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var ProducerData = await _service.GetByIdAsync(id);
            if (ProducerData == null) return View("NotFound");
            return View(ProducerData);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Producer producer)
        {
            producer.Id = id;
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            await _service.UpdateAsync(id, producer);
            return RedirectToAction(nameof(Index));
        }

        //producer/delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var ProducerData = await _service.GetByIdAsync(id);
            if (ProducerData == null) return View("NotFound");
            return View(ProducerData);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ProducerData = await _service.GetByIdAsync(id);
            if (ProducerData == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
