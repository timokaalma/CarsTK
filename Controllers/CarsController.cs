using Microsoft.AspNetCore.Mvc;
using CarsTK.Core.ServiceInterface;
using CarsTK.Models.Cars;
using CarsTK.Core.Dto;
using CarsTK.Data;

namespace CarsTK.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarsTKContext _context;
        private readonly ICarsServices _Carservices;

        public CarsController(CarsTKContext context, ICarsServices CarsServices)
        {
            _context = context;
            _Carservices = CarsServices;
        }

        public IActionResult Index()
        {
            var result = _context.Cars
                .Select(x => new CarIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            CarCreateUpdateViewModel result = new CarCreateUpdateViewModel();

            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarCreateUpdateViewModel vm)
        {
            var dto = new CarDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Price = vm.Price,
                EnginePower = vm.EnginePower,
                FuelConsumption = vm.FuelConsumption,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
            };

            var result = await _Carservices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var car = await _Carservices.DetailsAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var vm = new CarCreateUpdateViewModel()
            {
                Id = car.Id,
                Name = car.Name,
                Price = car.Price,
                EnginePower = car.EnginePower,
                FuelConsumption = car.FuelConsumption,
                CreatedAt = car.CreatedAt,
                ModifiedAt = car.ModifiedAt
            };

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CarCreateUpdateViewModel vm)
        {
            var dto = new CarDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Price = vm.Price,
                EnginePower = vm.EnginePower,
                FuelConsumption = vm.FuelConsumption,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt
            };

            var result = await _Carservices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var car = await _Carservices.DetailsAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var vm = new CarDetailsViewModel()
            {
                Id = car.Id,
                Name = car.Name,
                Price = car.Price,
                EnginePower = car.EnginePower,
                FuelConsumption = car.FuelConsumption,
                CreatedAt = car.CreatedAt,
                ModifiedAt = car.ModifiedAt
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _Carservices.DetailsAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            var vm = new CarDeleteViewModel()
            {
                Id = car.Id,
                Name = car.Name,
                Price = car.Price,
                EnginePower = car.EnginePower,
                FuelConsumption = car.FuelConsumption,
                CreatedAt = car.CreatedAt,
                ModifiedAt = car.ModifiedAt
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var carId = await _Carservices.Delete(id);

            if (carId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
