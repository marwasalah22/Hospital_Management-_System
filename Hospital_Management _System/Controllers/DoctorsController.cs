namespace Hospital_Management__System.Controllers
{
    [Authorize(Roles = MyRoles.AdminRole)]
    public class DoctorsController : Controller
    {
        private readonly IDoctorServices _doctorServices;
        private readonly INurseServices _nurseServices;
        private readonly UserManager<IdentityUser> _userManager;

        public DoctorsController(IDoctorServices doctorServices,
            INurseServices nurseServices,
            UserManager<IdentityUser> userManager)
        {
            _doctorServices = doctorServices;
            _nurseServices = nurseServices;
            _userManager = userManager;
        }

        public IActionResult Index()
        {           
            return View(_doctorServices.GetAll());
        }

        [HttpGet]
        public JsonResult GetNursesByDepartment(string department)
        {
            var nurses = _nurseServices.GetNursesByDepartment(department);
            var nurseList = nurses.Select(n => new
            {
                nurseId = n.Value, 
                name = n.Text
            }).ToList();

            return Json(nurseList);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Nurses = new SelectList(Enumerable.Empty<Nurse>(), "NurseId", "NurseName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorViewModel doctorViewModel)
        {
            if (ModelState.IsValid)
            {               
                var doctor = new Doctor
                {
                    DoctorName = doctorViewModel.DoctorName,
                    DoctorEmail = doctorViewModel.DoctorEmail,
                    Department = doctorViewModel.Department,
                    Schedule = doctorViewModel.Schedule,
                    NurseId = doctorViewModel.NurseId
                };

                if (_doctorServices.Create(doctor)) 
                {
                    var user = await _userManager.GetUserAsync(User);
                    var currentRoles = await _userManager.GetRolesAsync(user); // Get current roles of the user
                    if (currentRoles.Contains(MyRoles.UserRole)) // Check if the user is in the User role
                    {
                        await _userManager.RemoveFromRoleAsync(user, MyRoles.UserRole); // Remove the User role
                        await _userManager.AddToRoleAsync(user, MyRoles.DoctorRule); // Add the Patient role
                    }
                    return RedirectToAction(nameof(Index));
                }
                    

                ModelState.AddModelError("", "Unable to create doctor. Please try again.");
            }

            ViewBag.Nurses = new SelectList(_nurseServices.GetNursesByDepartment(doctorViewModel.Department), "Value", "Text");
            return View(doctorViewModel);
        }
        public async Task<IActionResult> Update(int id)
        {
            var doctor = _doctorServices.GetOne(id);
            
            var user = await _userManager.GetUserAsync(User);
            ViewBag.UserEmail = doctor.DoctorEmail ?? user?.Email;
            var doctorViewModel = new DoctorViewModel
            {
                DoctorName = doctor.DoctorName,
                DoctorEmail = doctor.DoctorEmail,
                Department = doctor.Department,
                Schedule = doctor.Schedule,
                NurseId = doctor.NurseId
            };

            ViewBag.Nurses = new SelectList(_nurseServices.GetNursesByDepartment(doctorViewModel.Department), "Value", "Text");
            return View(doctorViewModel);
        }

        [HttpPost]
        public IActionResult Update(int id, DoctorViewModel doctorViewModel)
        {
            if (ModelState.IsValid)
            {
                var doctor = new Doctor
                {
                    DoctorName = doctorViewModel.DoctorName,
                    DoctorEmail = doctorViewModel.DoctorEmail,
                    Department = doctorViewModel.Department,
                    Schedule = doctorViewModel.Schedule,
                    NurseId = doctorViewModel.NurseId
                };

                if (_doctorServices.Update(id, doctor))
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Unable to update doctor. Please try again.");
            }

            ViewBag.Nurses = new SelectList(_nurseServices.GetNursesByDepartment(doctorViewModel.Department), "Value", "Text");
            return View(doctorViewModel);
        }     
        public IActionResult Delete(int id)
        {
            var doctor = _doctorServices.GetOne(id);
            if (doctor == null)
            {
                return NotFound("Doctor not found.");
            }
            return View(doctor);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_doctorServices.Delete(id))
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ErrorMessage = "Error deleting the doctor.";
            return View(_doctorServices.GetOne(id));
        }
    }
}
