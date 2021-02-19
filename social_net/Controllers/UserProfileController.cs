using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using social_net.Models;

namespace social_net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private IPasswordHasher<ApplicationUser> _passwordHasher;


        private AuthContext _context;
        public UserProfileController(UserManager<ApplicationUser> userManager, AuthContext context, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            _userManager = userManager;
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        [Authorize]
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            return new
            {
                user.FullName,
                user.Email,
                user.UserName,
            };
        }


        [HttpPost, DisableRequestSizeLimit]
        [Authorize]
        [Route("ProfilePicture")]
        public async Task<IActionResult> Upload()
        {
            var formCollection = await Request.ReadFormAsync();
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var file = formCollection.Files.First();
            Image image = new Image();
            image.ImageTitle = file.FileName;
            image.Id = userId;
            MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);
            image.ImageData = Convert.ToBase64String(ms.ToArray(), 0, Convert.ToInt32(ms.Length));
            image.Image_URL = "data:image/jpeg;base64," + image.ImageData;
            ms.Close();
            ms.Dispose();
            var imageAdd = _context.Add(image);
            var result = _context.SaveChanges();
            return Ok( result );
        }

        [HttpGet]
        [Route("GetImage")]
        public async Task<Object> GetImage()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);
            var data = _context.images.Where(i => i.Id == user.Id).ToList();
            return data;
        }

        //TODO: В ПОСТМАНЕ 
        [HttpPost]
        [Route("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(string userName, string email, string password, string fullName)
        {
            try
            {
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    if (!string.IsNullOrEmpty(email))
                    {
                        user.Email = email;
                    }

                    if (!string.IsNullOrEmpty(userName))
                    {
                        user.UserName = userName;
                    }

                    if (!string.IsNullOrEmpty(password))
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, password);
                    }

                    if (!string.IsNullOrEmpty(fullName))
                    {
                        user.FullName = fullName;
                    }

                    return Ok(user);
                }
                else
                {
                    return BadRequest();
                }
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        //[HttpPost, DisableRequestSizeLimit]
        //[Route("ProfilePicture")]
        //public async Task<IActionResult> Upload()
        //{
        //    try
        //    {
        //        var formCollection = await Request.ReadFormAsync();
        //        var file = formCollection.Files.First();

        //        var folderName = Path.Combine("Resources", "Images");
        //        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        //        if (file.Length > 0)
        //        {
        //            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //            var fullPath = Path.Combine(pathToSave, fileName);
        //            var dbPath = Path.Combine(folderName, fileName);

        //            using (var stream = new FileStream(fullPath, FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }

        //            return Ok(new { dbPath });
        //        }
        //        else
        //        {
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}