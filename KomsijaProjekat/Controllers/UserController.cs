using KomsijaProjekat.Authorize;
using KomsijaProjekat.Data;
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
    [AuthorizeRole(UserRole.ADMIN)]
    public ActionResult Create(User user)
    {
        if (ModelState.IsValid)
        {
            _userRepository.Add(user);
            return RedirectToAction("Index");
        }
        return View(user);
    }

    [AuthorizeRole(UserRole.ADMIN)]
    public ActionResult Edit(int id)
    {
        var user = _userRepository.GetById(id);
        if (user == null) return HttpNotFound();
        return View(user);
    }

    [HttpPost]
    [AuthorizeRole(UserRole.ADMIN)]

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
    [AuthorizeRole(UserRole.ADMIN)]

    public ActionResult Delete(int id)
    {
        var user = _userRepository.GetById(id);
        if (user == null) return HttpNotFound();
        return View(user);
    }

    // POST Delete action (this is the Confirm Delete action)
    [HttpPost]
    [ActionName("Delete")]
    [AuthorizeRole(UserRole.ADMIN)] // Pomoću ActionName možete definisati isto ime akcije za GET i POST
    public ActionResult DeleteConfirmed(int id)
    {
        var user = _userRepository.GetById(id);
        if (user == null) return HttpNotFound();

        _userRepository.Delete(id);
        return RedirectToAction("Index");
    }

    public ActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Login(string email, string password)
    {
        using (var context = new AppDbContext())
        {
            // Pronađi korisnika prema email-u i lozinci
            var user = context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                // Postavljanje korisničkih podataka u sesiju
                Session["UserId"] = user.Id;
                Session["UserName"] = user.Name;
                Session["UserRole"] = user.Role.ToString(); // Postavljanje uloge korisnika u sesiju kao string (npr. "USER", "ADMIN")

                // Preusmeravanje korisnika na odgovarajući URL na osnovu uloge
                if (user.Role == UserRole.ADMIN)
                {
                    return RedirectToAction("Index", "Index"); // Admin panel ili stranica
                }
                else if (user.Role == UserRole.USER)
                {
                    return RedirectToAction("Index", "Index"); // Proizvodi za korisnika
                }
                else
                {
                    return RedirectToAction("Index", "Index"); // Defaultna stranica za guest
                }
            }
            else
            {
                // Ako nije pronađen korisnik, postavljanje uloge na GUEST
                Session["UserId"] = null;
                Session["UserName"] = "Guest";
                Session["UserRole"] = UserRole.GUEST.ToString();

                // Preusmeravanje na početnu stranicu
                return RedirectToAction("Index", "Index");
            }
        }
    }



    public ActionResult Logout()
    {
        // Očisti sesiju
        Session.Clear();
        Session["UserId"] = null;
        Session["UserName"] = "Guest";
        Session["UserRole"] = "GUEST"; // Postavljanje uloge na "GUEST"

        // Preusmeravanje na početnu stranicu
        return RedirectToAction("Index", "Index");
    }





}
