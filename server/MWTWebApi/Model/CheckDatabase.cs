
using Microsoft.EntityFrameworkCore;
using MWTCore.Repository;
using MWTCore.Services;
using MWTDbContext;
using MWTDbContext.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MWTWebApi.Model
{

    public class CheckDatabase : ICheckDatabase
    {
        private readonly StoreAppDbCon _context;

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public CheckDatabase(StoreAppDbCon context)
        {
            _context = context;
            if (checkRoles())
            {
                if (createRoles())
                {
                    

                }

            }

            if (checkUsers())
            {
                if (createUsers())
                {
                    

                }

            }

            if (checkCategory())
            {
                createCategory();
            }
            var AvatarListInDB = getAvatarList();
            cleanUpAvatar(AvatarListInDB);
            var ProductImageInDB = getProductList();
            cleanUpProduct(ProductImageInDB);
        }

        private bool checkRoles()
        {
            int count = 0;
            var roles = _context.userRoles.ToList();
            foreach (var role in roles)
            {
                if (role.rolename.Equals("Admin"))
                {
                    count++;
                }
                else if (role.rolename.Equals("Company"))
                {
                    count++;
                }
                else if (role.rolename.Equals("User"))
                {
                    count++;
                }
            }

            return count != 3 ? true : false;
        }

        private bool createRoles()
        {
            var roles = new List<UserRoles>() {
                new UserRoles(){ rolename = "Admin"},
                new UserRoles(){ rolename = "Company"},
                new UserRoles(){ rolename = "User"},
            };

            foreach (var role in roles)
            {
                _context.userRoles.Add(role);
            }

            return _context.SaveChanges() == 3 ? true : false;
        }

        private bool checkUsers()
        {
            int count = 0;
            var users = _context.users.ToList();
            foreach (var user in users)
            {
                if (user.Role == 1 && user.Username.Equals("Admin"))
                {
                    count++;
                }
                else if (user.Role == 2 && user.Username.Equals("Company"))
                {
                    count++;
                }
                else if (user.Role == 3 && user.Username.Equals("User"))
                {
                    count++;
                }
            }

            return count != 3 ? true : false;
        }

        private bool createUsers()
        {
            var users = new List<User>() {
                new User()
                {
                    Fullname = "Admin",
                    isActive = true,
                    Password = ComputeSha256Hash("Admin"),
                    Role = 1,
                    Username = "Admin",

                    createdAt = DateTime.Now,
                    updatedAt= DateTime.Now

                },
                new User()
                {
                    Fullname = "Company",
                    isActive = true,
                    Password = ComputeSha256Hash("Company"),
                    Role = 2,
                    Username = "Company",

                    createdAt = DateTime.Now,
                    updatedAt= DateTime.Now

                },
                new User()
                {
                    Fullname = "User",
                    isActive = true,
                    Password = ComputeSha256Hash("User"),
                    Role = 3,
                    Username = "User",

                    createdAt = DateTime.Now,
                    updatedAt= DateTime.Now

                }
            };

            foreach (var user in users)
            {
                _context.users.Add(user);
            }

            return _context.SaveChanges() == 3 ? true : false;
        }

        private bool checkCategory()
        {
            var categoryCount = 0;

            var categories = _context.categoryMasters.ToList();

            foreach (var category in categories)
            {
                switch (category.categoryName)
                {
                    case "Mobile":
                        categoryCount++;
                        break;
                    case "Laptop":
                        categoryCount++;
                        break;
                    case "Smartwatch":
                        categoryCount++;
                        break;
                    case "Earphones":
                        categoryCount++;
                        break;
                }
            }

            return categoryCount != 4 ? true : false;
        }

        private bool createCategory()
        {
            var categories = new List<String>() { "Mobile", "Laptop", "Smartwatch", "Earphones" };

            foreach (var category in categories)
            {
                var cat = new CategoryMaster() { categoryName = category };
                _context.categoryMasters.Add(cat);
            }

            return _context.SaveChanges() == 4 ? true : false;
        }

        private List<String> getAvatarList()
        {
            return _context.users.Select(us => us.Avatar).ToList();
        }

        private bool cleanUpAvatar(List<String> avatarList)
        {
            try
            {
                string currDir = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Images", "Avatar"));
                var fileList = Directory.GetFiles(currDir);

                var files = new List<string>();

                foreach (var file in fileList)
                {
                    string filename = file.Split('\\')[file.Split('\\').Length - 1];
                    if (!avatarList.Contains(filename) && !filename.Equals("DefaultProfile.jpeg"))
                    {
                        File.Delete(file);
                    }
                }

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }


        }

        private List<String> getProductList()
        {
            return _context.productMasters.Select(pm => pm.ProdImage).ToList();
        }

        private bool cleanUpProduct(List<String> productImageList)
        {
            try
            {
                string currDir = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine("Images", "Product"));
                var fileList = Directory.GetFiles(currDir);

                var files = new List<string>();

                foreach (var file in fileList)
                {
                    string filename = file.Split('\\')[file.Split('\\').Length - 1];
                    if (!productImageList.Contains(filename) && !filename.Equals("DefaultProduct.png"))
                    {
                        File.Delete(file);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
