using AspnetBBS.DataContext;
using AspnetBBS.Models;
using AspnetBBS.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspnetBBS.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Customer Login
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            //Id and PWD necessary 
            if(ModelState.IsValid)
            {
                //DB 검증
                using (var db = new AppDbContext())
                {
                    //Linq query
                    var user = db.Users.FirstOrDefault
                        (u => u.UserId.Equals(model.UserId) && 
                        u.UserPassword.Equals(model.UserPassword));
                    if(user!=null)
                    {
                        //login success
                        HttpContext.Session.SetInt32("USER_LOGIN_KEY", user.UserNo);
                        TempData["UserName"] = user.UserName;
                        return RedirectToAction("LoginSuccess", "Home");
                    }
                }
                //login fail, 그리고 using문 안에서 데이터 처리하는 케이스가 아니기에, using 밖으로 빼도 OK
                //using 문에서 한 줄이라도 코드 즉 session이 줄면, using이 빠르게 처리되기에
                ModelState.AddModelError(string.Empty, "You entered wrong User Id  or pasword");
            }
            return View(model);
            //model값 넘겨주는 이유? 올바른 값 입력하지 않았을때
            //뭐가 잘못 됐는지 넘겨줘야, view에서 확인
            //null값이나 잘못된 값이 넘어왔을 때,
            //asp-validation-for 확인 가능(제대로 된 값이 넘어왔을 때는 작동 안함)
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("USER_LOGIN_KEY");
            //HttpContext.Session.Clear()의 경우 모든 존재하는 세션을 삭제. 관리자들이 사용하는 명령어
            return RedirectToAction("Index", "Home");

        }
        /// <summary>
        /// Customer Register
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// 회원가입 전송
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(User model)
        {
            //Validation Check
            if(ModelState.IsValid)
            {
                //database입출력할때, 데이타 받을때는 open connection, 끝나면 close.
                //메모리 누수를 방지
                //open, close 대신 using문을 쓸 수 있음
                //Java try(SqlSession){} catch(){}
                using (var db = new AppDbContext())
                {
                    db.Users.Add(model);
                    //이것까지 메모리에 올라감
                    db.SaveChanges();
                    //이것까지 해야 실제 sql로 저장               
                }
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
