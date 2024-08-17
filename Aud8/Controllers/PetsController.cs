using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using domain.Identity;
using domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetAdoptionCenter.Domain.enums;
using PetAdoptionCenter.Domain.Models;
using PetAdoptionCenter.Service.Interface;
using repository;

namespace PetAdoptionCenter.Web.Controllers
{
    public class PetsController : Controller
    {
        private readonly IPetService _petService;
        private readonly IAdoptionApplicationService _adoptionApplicationService;
        private readonly UserManager<PetAdoptionCenterUser> _userManager;


        public PetsController(IPetService petService, IAdoptionApplicationService adoptionApplicationService, UserManager<PetAdoptionCenterUser> userManager)
        {
            _petService = petService;
            _adoptionApplicationService = adoptionApplicationService;
            _userManager = userManager;
        }

        // GET: Pets
        [Authorize(Roles = "Shelter,Adopter")]
        public IActionResult Index()
        {
            
            return View(_petService.GetPets());
        }

        // GET: Pets/Details/5
        [Authorize(Roles = "Shelter,Adopter")]
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = _petService.GetPetById(id);
            if (pet == null)
            {
                return NotFound();
            }

            ViewData["AppForms"] = _adoptionApplicationService.GetAdoptionApplicationsByPetId(pet.Id);

            /*var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = _userManager.FindByIdAsync(userId);
            
            ViewData["LoggedInUser"] = user;*/

            return View(pet);
        }


        [Authorize(Roles = "Shelter")]
        public IActionResult MyPets(string? id)
        {
            return View(_petService.GetPetsByShelterId(id));
        }

        // GET: Pets/Create
        [Authorize(Roles = "Shelter")]
        public IActionResult Create()
        {
            //ViewData["LoggedShelterId"] = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

            ViewData["PetType"] = new SelectList(
                                        Enum.GetValues(typeof(PetType))
                                        .Cast<PetType>()
                                        .Select(e => new { Id = (int)e, Name = e.ToString() }),
                                        "Id", "Name");

            return View();
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Age,PetType,Breed,Sex,Description,IsHouseTrained,FavouriteThings,HomeRequirements,PhotoUrl,Id")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
                _petService.CreateNewPet(loggedInUser, pet);
                return RedirectToAction("MyPets", new { id = pet.ShelterId });
            }
            
            
            //ViewData["ShelterId"] = new SelectList(_context.Users, "Id", "Id", pet.ShelterId);
            return View(pet);
        }

        // GET: Pets/Edit/5
        [Authorize(Roles = "Shelter")]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = _petService.GetPetById(id);
            if (pet == null)
            {
                return NotFound();
            }
            //ViewData["ShelterId"] = new SelectList(_context.Users, "Id", "Id", pet.ShelterId);
            return View(pet);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Age,PetType,Breed,Sex,Description,IsHouseTrained,FavouriteThings,HomeRequirements,PhotoUrl,PetStatus,ShelterId,Id")] Pet pet)
        {
            if (id != pet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _petService.UpdatePet(pet);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction("MyPets", new { id = pet.ShelterId });
            }
            //ViewData["ShelterId"] = new SelectList(_context.Users, "Id", "Id", pet.ShelterId);
            return View(pet);
        }

        // GET: Pets/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = _petService.GetPetById(id);
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var pet = _petService.GetPetById(id);
            if (pet != null)
            {
                _petService.DeletePet(pet.Id);
            }

            return RedirectToAction("MyPets", new { id = pet.ShelterId });
        }

        /*private bool PetExists(Guid id)
        {
            return _context.Pet.Any(e => e.Id == id);
        }*/
    }
}
