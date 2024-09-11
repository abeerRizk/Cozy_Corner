using AutoMapper;
using ITI_Project.BLL.ModelVM;
using ITI_Project.BLL.Services.Interface;
using ITI_Project.DAL.Entites;
using Microsoft.AspNetCore.Mvc;

namespace ITI_Project.Controllers
{
    public class RatingController : Controller
    {
        private readonly IRatingService ratingService;
        private readonly IMapper mapper;

        public RatingController(IRatingService ratingService, IMapper mapper)
        {
            this.ratingService = ratingService;
            this.mapper = mapper;
        }

        public IActionResult Read()
        {
            var result = ratingService.GetAll();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var rating = new CreateRatingVM();
            return View(rating);
        }

        [HttpPost]
        public IActionResult Create(CreateRatingVM rating)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var x = ratingService.Create(rating);
                    return RedirectToAction("Read", "Rating");
                }
            }
            catch (Exception)
            {
                return View(rating);
            }

            return View(rating);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = ratingService.GetByRatingId(id);
            return View(data);
        }


        [HttpPost]
        public IActionResult Delete(GetRatingVM rating)
        {
            try
            {
                ratingService.Delete(rating.Id);
                return RedirectToAction("Read", "Rating");
            }
            catch (Exception)
            {
                return View(rating);
            }

        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            var data = ratingService.GetByRatingId(id);
            UpdateRatingVM new_data = mapper.Map<UpdateRatingVM>(data);
            return View(new_data);
        }


        [HttpPost]
        public IActionResult Update(UpdateRatingVM rating)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ratingService.Update(rating);
                    return RedirectToAction("Read", "Rating");
                }
            }
            catch (Exception)
            {
                return View(rating);
            }

            return View(rating);

        }
    }
}
