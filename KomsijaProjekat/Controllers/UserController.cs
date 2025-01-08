using KomsijaProjekat.Models;
using KomsijaProjekat.Repository;
using System.Linq;
using System.Web.Mvc;

public class UserController : Controller
{
    private readonly IUserRepository _userRepository;

    public UserController()
    {
        _userRepository = new UserRepository();
    }

    public ActionResult Index()
    {
        var users = _userRepository.GetAll();
        return View(users);
    }

   

    // Dodavanje korisnika
    [HttpPost]
    public ActionResult Add(User user)
    {
        if (ModelState.IsValid)
        {
            _userRepository.Add(user);
            return RedirectToAction("Index");
        }
        return View(user);
    }


    public ActionResult GetUserByEmail(string email)
    {
        var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == email); // Traži korisnika sa datim email-om
        if (user != null)
        {
            // Ako korisnik postoji, možemo ga proslediti na view
            return View(user); // Ovde se može proslediti korisnik u view, ako je potrebno
        }

        // Ako korisnik nije pronađen
        ViewBag.ErrorMessage = "Korisnik sa tim email-om ne postoji.";
        return View();
    }



    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(User user)
    {
        if (ModelState.IsValid)
        {
            _userRepository.Add(user);
            return RedirectToAction("Index");
        }
        return View(user);
    }

    public ActionResult Edit(int id)
    {
        var user = _userRepository.GetById(id);
        if (user == null) return HttpNotFound();
        return View(user);
    }

    [HttpPost]
    public ActionResult Edit(User user)
    {
        if (ModelState.IsValid)
        {
            _userRepository.Update(user);
            return RedirectToAction("Index");
        }
        return View(user);
    }

    [HttpGet]
    public ActionResult Delete(int id)
    {
        var user = _userRepository.GetById(id);
        if (user == null) return HttpNotFound();
        return View(user);
    }

    // POST Delete action (this is the Confirm Delete action)
    [HttpPost]
    [ActionName("Delete")]  // Pomoću ActionName možete definisati isto ime akcije za GET i POST
    public ActionResult DeleteConfirmed(int id)
    {
        var user = _userRepository.GetById(id);
        if (user == null) return HttpNotFound();

        _userRepository.Delete(id);
        return RedirectToAction("Index");
    }

    // GET: Login page
    public ActionResult Login()
{
    return View();
}

    

    [HttpPost]
    public ActionResult Login(string email)
    {
        var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == email);
        if (user != null)
        {
            // Sačuvaj email korisnika u sesiju
            Session["LoggedInUser"] = user.Email;
            ViewBag.UserEmail = user.Email; // Dodaj email u ViewBag
            return RedirectToAction("Index", "Index"); // Preusmeri na početnu stranicu
        }

        // Ako email nije pronađen, prikaži grešku
        ViewBag.ErrorMessage = "Uneti email ne postoji u bazi.";
        return View();
    }




    public ActionResult Logout()
    {
        // Očisti sesiju
        Session["LoggedInUser"] = null;
        return RedirectToAction("Login");
    }




}
