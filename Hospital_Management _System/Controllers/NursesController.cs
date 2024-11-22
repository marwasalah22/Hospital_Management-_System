namespace Hospital_Management__System.Controllers
{
    [Authorize(Roles = MyRoles.AdminRole)]
    public class NursesController : Controller
    {
        private readonly INurseServices _nurseServices;
        private readonly UserManager<IdentityUser> _userManager;
        public NursesController(INurseServices nurseServices,
             UserManager<IdentityUser> userManager)
        {
            _nurseServices = nurseServices;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(_nurseServices.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Nurse nurse)
        {
           
            if (ModelState.IsValid)
            {
                var nurses = new Nurse
                {
                    NurseId = nurse.NurseId,
                    NurseEmail =nurse.NurseEmail,
                    NurseDepartment = nurse.NurseDepartment,           
                };
              
                if (_nurseServices.Create(nurse))
                {
                    // Change user role from User to Patient
                    var user = await _userManager.GetUserAsync(User);
                    var currentRoles = await _userManager.GetRolesAsync(user); // Get current roles of the user
                    if (currentRoles.Contains(MyRoles.UserRole)) // Check if the user is in the User role
                    {
                        await _userManager.RemoveFromRoleAsync(user, MyRoles.UserRole); // Remove the User role
                        await _userManager.AddToRoleAsync(user, MyRoles.NurseRole); // Add the Patient role
                    }
                    return RedirectToAction(nameof(Index));
                }
                   
                ModelState.AddModelError("create error", "not created");
                return View(nurse);

            }
            ModelState.AddModelError("create error", "not created");
            return View(nurse);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
           
            return View( _nurseServices.GetOne(id));
        }

        [HttpPost]
        public IActionResult Delete(int id, string msg)
        {
            if (_nurseServices.Delete(id))
                return RedirectToAction(nameof(Index));
            return View(_nurseServices.GetOne(id));
        }

      

    }
}
