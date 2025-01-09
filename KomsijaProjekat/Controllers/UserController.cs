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
        var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == email); 
        if (user != null)
        {
            return View(user); 
        }

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

    [HttpPost]
    [ActionName("Delete")]
    [AuthorizeRole(UserRole.ADMIN)] 
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
           
            var user = context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
             
                Session["UserId"] = user.Id;
                Session["UserName"] = user.Name;
                Session["UserRole"] = user.Role.ToString(); 

                if (user.Role == UserRole.ADMIN)
                {
                    return RedirectToAction("Index", "Index"); 
                }
                else if (user.Role == UserRole.USER)
                {
                    return RedirectToAction("Index", "Index"); 
                }
                else
                {
                    return RedirectToAction("Index", "Index"); 
                }
            }
            else
            {
   
                Session["UserId"] = null;
                Session["UserName"] = "Guest";
                Session["UserRole"] = UserRole.GUEST.ToString();

                return RedirectToAction("Index", "Index");
            }
        }
    }

    public ActionResult Logout()
    {
        
        Session.Clear();
        Session["UserId"] = null;
        Session["UserName"] = "Guest";
        Session["UserRole"] = "GUEST"; 

        return RedirectToAction("Index", "Index");
    }





}
