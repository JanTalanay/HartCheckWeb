using HartCheck_Admin.Data;
using HartCheck_Admin.Interfaces;
using HartCheck_Admin.Models;
using HartCheck_Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HartCheck_Admin.Controllers
{
    public class EducationalResourceController : Controller
    {
        private readonly IEducationalResourceRepository _educationalresourceRepository;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        public EducationalResourceController(IEducationalResourceRepository educationalresourceRepository, IHttpContextAccessor httpContextAccessor)
        {
            _educationalresourceRepository = educationalresourceRepository;
            _httpcontextAccessor = httpContextAccessor;

        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            IEnumerable<EducationalResource> resources = await _educationalresourceRepository.GetAll();
            return View(resources);
        }
        [Authorize]
        public IActionResult Create()
        { 
            return View();
        }
        
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(EducationalResource educationalResource)
        {
            if (!ModelState.IsValid)
            {
                return View(educationalResource);
            }
            _educationalresourceRepository.Add(educationalResource); 
            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var educationalresourceDetails = await _educationalresourceRepository.GetByIdAsync(id);
            if (educationalresourceDetails == null)
            {
                ErrorViewModel m = new ErrorViewModel();
                m.RequestId = Guid.NewGuid().ToString();
                return View("Error", m);
            }

            return View(educationalresourceDetails);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteEducationalResource(int id)
        {
            var educationalresourceDetails = await _educationalresourceRepository.GetByIdAsync(id);
            if (educationalresourceDetails == null)
            {
                return View("Error");
            }
            else
            {
                 _educationalresourceRepository.Delete(educationalresourceDetails);
                return RedirectToAction("Index");
            }
   
        }

        public async Task<IActionResult> Edit(int id)
        {
            var educationalresource = await _educationalresourceRepository.GetByIdAsync(id);
            if (educationalresource == null) return View("Error");
            var edVM = new EditEducationalResourceViewModel
            {
                resourceID = educationalresource.resourceID,
                text = educationalresource.text,
                link = educationalresource.link
            };
            return View(edVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditEducationalResourceViewModel edVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit educational resource");
                return View("Edit", edVM);
            }
            var adminEd = await _educationalresourceRepository.GetByIdAsyncNoTracking(id);

            if (adminEd != null)
            {

                var educationalresource = new EducationalResource
                {
                    resourceID = id,
                    text = edVM.text,
                    link = edVM.link
                };

                _educationalresourceRepository.Update(educationalresource);

                return RedirectToAction("Index");
            }
            else
            {
                return View(edVM);
            }
        }

    }
}
