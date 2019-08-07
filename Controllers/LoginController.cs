using Microsoft.AspNetCore.Authorization;  
using System.IdentityModel.Tokens.Jwt;  
using System.Security.Claims;  
using Microsoft.IdentityModel.Tokens;  
using System.Text;  


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo.Models;
using Demo.dal;
using Microsoft.Extensions.Configuration; 
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;

namespace Demo.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration _config;  
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        public IActionResult Login()
        {
            return View();
            
        }
        public IActionResult CheckUser(User obj)
        {
             ClaimsIdentity identity = null;
            bool isAuthenticated = false;
            if (obj.UserName == "shiv" && obj.Password == "password")
            {
                //Create the identity for the user
                identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, obj.UserName),
                    new Claim(ClaimTypes.Role, "User")
                }, JwtBearerDefaults.AuthenticationScheme);

                isAuthenticated = true;
            }
            
            
             if (isAuthenticated)
            {
                var principal = new ClaimsPrincipal(identity);
               // var login = await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme);

             var login = HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }
        
            
        }
            
        


         private string GenerateJSONWebToken()  
        {  
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));  
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);  
  
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],  
              _config["Jwt:Issuer"],  
              null,  
              expires: DateTime.Now.AddMinutes(120),  
              signingCredentials: credentials);  
  
            return new JwtSecurityTokenHandler().WriteToken(token);  
        }  
     


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

           /* if(obj.UserName=="shiv"&& obj.Password=="123password") // <-- DB calls
            {
                IActionResult response =Unauthorized();
                var user= AuthenticateUser(obj);
                if(user!= null)
                {
                    string str = GenerateJSONWebToken();
                    response = Ok(new { token = str });  
                }
                // generate the token
                
            //      HttpContext.Session.SetString("JWToken", value);
                return RedirectToAction("Index","Home");
                
            }
            else{
                return View("Login");
            }*/
                