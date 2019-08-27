using System.Linq;
using AspnetBBS.DataContext;
using AspnetBBS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspnetBBS.Controllers
{
    public class NoteController : Controller
    {
        /// <summary>
        /// Note list
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if(HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //No login
                return RedirectToAction("Login", "Account");
            }           
            //instead of var list = new List<Note>();
            using (var db = new AppDbContext())
            {
                // 1) get all data in note table
                var list = db.Notes.ToList();
                list.Reverse();
                return View(list);


                // 2) get all notes joining with other table (not working)
                //var list = db.Notes.Join(db.Users,
                //    n => n.UserNo,
                //    u => u.UserNo,
                //    (n, u) => new { n.NoteNo, n.NoteTitle, n.NoteContents, n.UserNo, u.UserName });
                //return View(list);


            }           
        }
        /// <summary>
        /// Note details
        /// </summary>
        /// <param name="noteNo"></param>
        /// <returns></returns>
        public IActionResult Detail(int noteNo)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //No login
                return RedirectToAction("Login", "Account");
            }
            using (var db = new AppDbContext())
            {
                var note = db.Notes.FirstOrDefault(n => n.NoteNo.Equals(noteNo));
                return View(note);
            }
        }
        /// <summary>
        /// Return add page
        /// </summary>
        /// <returns>Add page</returns>
        public IActionResult Add()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                // without login
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        /// <summary>
        /// Add note to DB
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(Note model)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                // without login
                return RedirectToAction("Login", "Account");
            }
            //null의 가능성 때문에 에러: 명시적 변환이 있을 수 있다고 하면서 에러
            //위에 null 검사했는데 또?? 나중에 identity를 통해 중복된 코드 고칠 예정
            //model.UserNo = HttpContext.Session.GetInt32("USER_LOGIN_KEY");
            model.UserNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());
            if (ModelState.IsValid)
            {
                using (var db = new AppDbContext())
                {
                    db.Notes.Add(model);
                    // instead of db.SaveChanges(); 
                    if (db.SaveChanges() > 0)
                    {
                        return Redirect("Index");
                    }                          
                }
                ModelState.AddModelError(string.Empty, "Sorry, we cannot save the note");
            }
            return View(model);
        }
        /// <summary>
        /// Edit
        [HttpGet]
        public IActionResult Edit(int noteNo)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //No login
                return RedirectToAction("Login", "Account");
            }
            using(var db = new AppDbContext())
            {
                var note = db.Notes.FirstOrDefault(n => n.NoteNo.Equals(noteNo));
                return View(note);
            }          
        }

        /// <summary>
        /// Edit 코드 추가 2019 01 25
        /// </summary>
        /// <param name="noteNo"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(int noteNo, Note note)
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //No login
                return RedirectToAction("Login", "Account");
            }
            note.UserNo = int.Parse(HttpContext.Session.GetInt32("USER_LOGIN_KEY").ToString());
            if (ModelState.IsValid)
            {
                var db = new AppDbContext();
                db.Notes.Update(note);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Delete(int noteNo)
        {
            //컨펌 메시지에서 확인을 누르면 이 메소드가 트리거 됨
            //파라미터로 글 번호와 컨펌 메시지의 결과가 들어와야함
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                //No login
                return RedirectToAction("Login", "Account");
            }

            using (var db = new AppDbContext())
            {
                var note = db.Notes.FirstOrDefault(n => n.NoteNo.Equals(noteNo));
                db.Notes.Remove(note);

                if (db.SaveChanges() > 0)
                {
                    return Redirect("Index");
                }
                ModelState.AddModelError(string.Empty, "Sorry, we cannot delete the note");
            }
            return View();
        }
    }
}
