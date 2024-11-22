namespace Hospital_Management__System.Controllers
{
    [Authorize(Roles = MyRoles.DoctorRule + "," + MyRoles.PatientRole + "," + MyRoles.UserRole + "," + MyRoles.NurseRole + "," + MyRoles.AdminRole)]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDoctorServices _doctorServices;


        public HomeController(ILogger<HomeController> logger,
            IDoctorServices doctorServices)
        {
            _logger = logger;
            _doctorServices = doctorServices;
        }  
            
        // show Home
        public IActionResult Index()
        {
            return View();
        }


        // show Doctors
        public IActionResult Doctor()
        {
            return View(_doctorServices.GetAll());
        }

        // show Department
        public IActionResult Department()
        {
            return View();
        }


        public IActionResult AIndex()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}