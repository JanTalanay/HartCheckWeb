using HartCheck_Admin.Data;
using HartCheck_Admin.Interfaces;
using HartCheck_Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HartCheck_Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IHCProfessionalRepository _hcprofessionalRepository;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        private readonly ApplicationDbContext _context;
        public UserController(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor, IHCProfessionalRepository hcprofessionalRepository)
        {
            _userRepository = userRepository;
            _hcprofessionalRepository = hcprofessionalRepository;
            _httpcontextAccessor = httpContextAccessor;

        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            IEnumerable<User> users = await _userRepository.GetAll();
            return View(users);
        }
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userdetails = await _userRepository.GetByIdAsync(id);
            if (userdetails == null)
            {
                ErrorViewModel m = new ErrorViewModel();
                m.RequestId = Guid.NewGuid().ToString();
                return View("Error", m);
            }

            return View(userdetails);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userdetails = await _userRepository.GetByIdAsync(id);
            if (userdetails == null)
            {
                return View("Error");
            }
            else
            {
                _userRepository.Delete(userdetails);
                return RedirectToAction("Index");
            }

        }

        public async Task<IActionResult> ViewPending()
        {
            IEnumerable<User> users = await _userRepository.GetAll();
            IEnumerable<User> approved = users.Where(u => u.role == 1);
            return View(approved);
        }

        public async Task<IActionResult> ViewApproved()
        {
            IEnumerable<User> users = await _userRepository.GetAll();
            IEnumerable<User> pending = users.Where(u => u.role == 1);
            var approved = await DisplayApprovedHCProfessional();
            return View(approved);
            
        }

        public async Task<IActionResult> ViewDeclined()
        {
            IEnumerable<User> users = await _userRepository.GetAll();
            IEnumerable<User> declined = users.Where(u => u.role == 1);
            return View (declined);
        }

        public async Task<IActionResult> Approve(int id)
        {
            var userdetails = await _userRepository.GetByIdAsync(id);
            if (userdetails == null)
            {
                ErrorViewModel m = new ErrorViewModel();
                m.RequestId = Guid.NewGuid().ToString();
                return View("Error", m);
            }

            return View(userdetails);
        }

        [HttpPost, ActionName("Approve")]
        public async Task<IActionResult> ApproveDoctor(int id)
        {
            var userdetails = await _userRepository.GetByIdAsync(id);
            var doctorStatus = await _hcprofessionalRepository.GetByIdAsync(id);

            if (userdetails != null && doctorStatus != null)
            {
                // Update the user's status in the UserStatus table
                doctorStatus.verification = 1;
                 _hcprofessionalRepository.Update(doctorStatus);
            }
            else
            {
                return View("Error");
            }
            return RedirectToAction("ViewPending");
        }

        public async Task<IActionResult> DisplayApprovedHCProfessional()
        {
            // Query the HealthcareProfessional table for records with the specific verification value.
            var healthcareProfessionals = await _hcprofessionalRepository.GetHealthcareProfessionalsWithVerification();

            // Extract the User IDs from the matching healthcare professional records.
            var userIds = healthcareProfessionals.Select(h => int.Parse(h.userID)).ToList();

            // Query the User table to get the users associated with the extracted User IDs.
            var usersWithVerification = await _userRepository.GetUsersWithIds(userIds);

            return View(usersWithVerification);
        }

        
    }
} 
