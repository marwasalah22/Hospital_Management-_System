namespace Hospital_Management__System.Controllers
{
    [Authorize]
    public class MedicalRecordsController : Controller
    {
        private readonly IMedicalRecordService _medicalRecordService;
        private readonly IDoctorServices _doctorServices;
        private readonly UserManager<IdentityUser> _userManager;

        public MedicalRecordsController(IMedicalRecordService medicalRecordService,
            IDoctorServices doctorServices,
            UserManager<IdentityUser> userManager)
        {
            _medicalRecordService = medicalRecordService;
            _doctorServices = doctorServices;
            _userManager = userManager;
        }

        //User
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User); // Get the current logged-in user
            if (user == null)
            {
                return Unauthorized(); // Ensure the user is logged in
            }

            var userEmail = user.Email;

            // Get medical records for the current user's email
            var medicalRecords = _medicalRecordService.GetAll()
                                       .Where(m => m.PatiantEmailnew == userEmail)
                                       .ToList();

            return View("Index", medicalRecords);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User); // Get the current user
            ViewBag.UserEmail = user?.Email;
            ViewBag.doctors = _medicalRecordService.SelectedItems();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MedicalRecordViewModel medicalRecordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var medicalRecord = new MedicalRecord
                {
                    DoctorId = medicalRecordViewModel.DoctorId,
                    PatiantEmailnew = user.Email,
                    Appointment = medicalRecordViewModel.Appointment,
                    Department = medicalRecordViewModel.Department,
                    PatiantName = medicalRecordViewModel.PatiantName,
                    Description = medicalRecordViewModel.Description,
                    IsFinished = medicalRecordViewModel.IsFinished,
                    PatiantAddress = medicalRecordViewModel.PatiantAddress,
                    PatiantAge = medicalRecordViewModel.PatiantAge,
                    PatiantPhone = medicalRecordViewModel.PatiantPhone
                };

                

                if (_medicalRecordService.Create(medicalRecord))
                {
                    // Change user role from User to Patient
                    var currentRoles = await _userManager.GetRolesAsync(user); // Get current roles of the user
                    if (currentRoles.Contains(MyRoles.UserRole)) // Check if the user is in the User role
                    {
                        await _userManager.RemoveFromRoleAsync(user, MyRoles.UserRole); // Remove the User role
                        await _userManager.AddToRoleAsync(user, MyRoles.PatientRole); // Add the Patient role
                    }
                    return RedirectToAction(nameof(Index));
                }
                    

                ViewBag.doctors = _medicalRecordService.SelectedItems();
                return View(medicalRecordViewModel);
            }

            ViewBag.doctors = _medicalRecordService.SelectedItems();
            return View(medicalRecordViewModel);
        }


        //Doctor
        public IActionResult DIndex()
        {
            return View(_medicalRecordService.GetAll());
        }
        [HttpGet]
        public async Task<IActionResult> DCreate()
        {
            var user = await _userManager.GetUserAsync(User); // Get the current user
            ViewBag.UserEmail = user?.Email;
            ViewBag.doctors = _medicalRecordService.SelectedItems();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DCreate(MedicalRecordViewModel medicalRecordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var medicalRecord = new MedicalRecord
                {
                    DoctorId = medicalRecordViewModel.DoctorId,
                    PatiantEmailnew = user.Email,
                    Appointment = medicalRecordViewModel.Appointment,
                    Department = medicalRecordViewModel.Department,
                    PatiantName = medicalRecordViewModel.PatiantName,
                    Description = medicalRecordViewModel.Description,
                    IsFinished = medicalRecordViewModel.IsFinished,
                    PatiantAddress = medicalRecordViewModel.PatiantAddress,
                    PatiantAge = medicalRecordViewModel.PatiantAge,
                    PatiantPhone = medicalRecordViewModel.PatiantPhone
                };



                if (_medicalRecordService.DCreate(medicalRecord))
                {
                    // Change user role from User to Patient
                    var currentRoles = await _userManager.GetRolesAsync(user); // Get current roles of the user
                    if (currentRoles.Contains(MyRoles.UserRole)) // Check if the user is in the User role
                    {
                        await _userManager.RemoveFromRoleAsync(user, MyRoles.UserRole); // Remove the User role
                        await _userManager.AddToRoleAsync(user, MyRoles.PatientRole); // Add the Patient role
                    }
                    return RedirectToAction(nameof(DIndex));
                }


                ViewBag.doctors = _medicalRecordService.SelectedItems();
                return View(medicalRecordViewModel);
            }

            ViewBag.doctors = _medicalRecordService.SelectedItems();
            return View(medicalRecordViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DUpdate(int id)
        {
            var medicalRecord = _medicalRecordService.GetOne(id);

            var user = await _userManager.GetUserAsync(User);
            ViewBag.UserEmail = medicalRecord.PatiantEmailnew ?? user?.Email;
            ViewBag.doctors = _medicalRecordService.SelectedItems();

            var viewModel = new MedicalRecordViewModel
            {
                PatiantName = medicalRecord.PatiantName,
                PatiantAge = medicalRecord.PatiantAge,
                PatiantPhone = medicalRecord.PatiantPhone,
                PatiantAddress = medicalRecord.PatiantAddress,
                Department = medicalRecord.Department,
                DoctorId = medicalRecord.DoctorId,
                Appointment = medicalRecord.Appointment,
                Description = medicalRecord.Description,
                IsFinished = medicalRecord.IsFinished,
                Email = medicalRecord.PatiantEmailnew // Pass the email to the view
            };

            return View(viewModel);
        }

        [HttpPost]      
        public async Task<IActionResult> DUpdate(int id, MedicalRecordViewModel medicalRecordViewModel)
        {
            if (ModelState.IsValid)
            {

                // Update the medical record fields
                var medicalRecord = new MedicalRecord
                {
                    PatiantName = medicalRecordViewModel.PatiantName,
                    PatiantAge = medicalRecordViewModel.PatiantAge,
                    PatiantPhone = medicalRecordViewModel.PatiantPhone,
                    PatiantAddress = medicalRecordViewModel.PatiantAddress,
                    Department = medicalRecordViewModel.Department,
                    DoctorId = medicalRecordViewModel.DoctorId,
                    Appointment = medicalRecordViewModel.Appointment,
                    Description = medicalRecordViewModel.Description,
                    IsFinished = medicalRecordViewModel.IsFinished,
                    PatiantEmailnew = medicalRecordViewModel.Email, // Update the email
                };


                if (_medicalRecordService.DUpdate(id, medicalRecord))
                    return RedirectToAction(nameof(DIndex));
                ModelState.AddModelError("", "Unable to update the Medical Record. Please try again.");
            }
            else
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            ViewBag.doctors = _doctorServices.GetDoctorsByDepartment(medicalRecordViewModel.Department);
            return View(medicalRecordViewModel);
        }



        //Admin

        public IActionResult AIndex()
        {
            return View(_medicalRecordService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> ACreate()
        {
            
            ViewBag.doctors = _medicalRecordService.SelectedItems();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ACreate(MedicalRecordViewModel medicalRecordViewModel)
        {
            if (ModelState.IsValid)
            {
                
                var medicalRecord = new MedicalRecord
                {
                    DoctorId = medicalRecordViewModel.DoctorId,
                    PatiantEmailnew = medicalRecordViewModel.Email,                
                    Appointment = medicalRecordViewModel.Appointment,
                    Department = medicalRecordViewModel.Department,
                    PatiantName = medicalRecordViewModel.PatiantName,
                    Description = medicalRecordViewModel.Description,
                    IsFinished = medicalRecordViewModel.IsFinished,
                    PatiantAddress = medicalRecordViewModel.PatiantAddress,
                    PatiantAge = medicalRecordViewModel.PatiantAge,
                    PatiantPhone = medicalRecordViewModel.PatiantPhone
                };
   
                if (_medicalRecordService.ACreate(medicalRecord))
                {
                    // Change user role from User to Patient
                    var user = await _userManager.GetUserAsync(User);
                    var currentRoles = await _userManager.GetRolesAsync(user); // Get current roles of the user
                    if (currentRoles.Contains(MyRoles.UserRole)) // Check if the user is in the User role
                    {
                        await _userManager.RemoveFromRoleAsync(user, MyRoles.UserRole); // Remove the User role
                        await _userManager.AddToRoleAsync(user, MyRoles.PatientRole); // Add the Patient role
                    }
                    return RedirectToAction(nameof(AIndex));
                }


                ViewBag.doctors = _medicalRecordService.SelectedItems();
                return View(medicalRecordViewModel);
            }

            ViewBag.doctors = _medicalRecordService.SelectedItems();
            return View(medicalRecordViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var medicalRecord = _medicalRecordService.GetOne(id);

            if (medicalRecord == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            ViewBag.UserEmail = medicalRecord.PatiantEmailnew ?? user?.Email;
            ViewBag.doctors = _medicalRecordService.SelectedItems();

            var viewModel = new MedicalRecordViewModel
            {
                PatiantName = medicalRecord.PatiantName,
                PatiantAge = medicalRecord.PatiantAge,
                PatiantPhone = medicalRecord.PatiantPhone,
                PatiantAddress = medicalRecord.PatiantAddress,
                Department = medicalRecord.Department,
                DoctorId = medicalRecord.DoctorId,
                Appointment = medicalRecord.Appointment,
                Description = medicalRecord.Description,
                IsFinished = medicalRecord.IsFinished,
                Email = medicalRecord.PatiantEmailnew // Pass the email to the view
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, MedicalRecordViewModel medicalRecordViewModel)
        {
            if (ModelState.IsValid)
            {

                // Update the medical record fields
                var medicalRecord = new MedicalRecord
                {
                    PatiantName = medicalRecordViewModel.PatiantName,
                    PatiantAge = medicalRecordViewModel.PatiantAge,
                    PatiantPhone = medicalRecordViewModel.PatiantPhone,
                    PatiantAddress = medicalRecordViewModel.PatiantAddress,
                    Department = medicalRecordViewModel.Department,
                    DoctorId = medicalRecordViewModel.DoctorId,
                    Appointment = medicalRecordViewModel.Appointment,
                    Description = medicalRecordViewModel.Description,
                    IsFinished = medicalRecordViewModel.IsFinished,
                    PatiantEmailnew = medicalRecordViewModel.Email, // Update the email
                };


                if (_medicalRecordService.Update(id, medicalRecord))
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", "Unable to update the Medical Record. Please try again.");
            }
            else
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            ViewBag.doctors = _doctorServices.GetDoctorsByDepartment(medicalRecordViewModel.Department);
            return View(medicalRecordViewModel);
        }
        public IActionResult Delete(int id)
        {
            var medicalRecord = _medicalRecordService.GetOne(id);
            if (medicalRecord == null)
            {
                return NotFound(); // Handle case where record is not found
            }

            return View(medicalRecord);
        }

        [HttpPost]
        public IActionResult Delete(int id, string msg)
        {
            if (_medicalRecordService.Delete(id))
                return RedirectToAction(nameof(Index));

            return View(_medicalRecordService.GetOne(id));
        }
        public IActionResult Search(string searchString)
        {
            // Pass the search string to ViewData for keeping the search term in the input field
            ViewData["CurrentFilter"] = searchString;

            // Fetch all records from the service
            var medicalRecords = _medicalRecordService.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                medicalRecords = medicalRecords
                                    .Where(m => m.PatiantName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                                    .ToList();
            }
            return View(medicalRecords);
        }

        [HttpGet]
        public JsonResult GetDoctorsByDepartment(string department)
        {
            // Assuming _doctorServices.GetDoctorsByDepartment returns a list of Doctor objects
            var doctors = _doctorServices.GetDoctorsByDepartment(department);

            var doctorList = doctors.Select(d => new
            {
                DoctorId = d.Value, // Make sure these properties exist in the Doctor model
                DoctorName = d.Text
            }).ToList();

            return Json(doctorList);
        }

    }
}

