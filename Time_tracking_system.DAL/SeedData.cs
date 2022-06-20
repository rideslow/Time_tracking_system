using Microsoft.AspNetCore.Identity;
using System.Linq;
using Time_tracking_system.Domain.Entity;

namespace Time_tracking_system.DAL
{
    public class SeedData
    {
        public static void Seed(UserManager<Employee> userManager,
            RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
            SeedTypeReques(db);
            SeedStatusReques(db);
            SeedTypeVocation(db);
            SeedAttributes(db);
            SeedAttributes_TypeRequest(db);
            db.SaveChangesAsync();
        }
        #region SeedUsers
        private static void SeedUsers(UserManager<Employee> userManager)
        {
            if (userManager.FindByNameAsync("admin@mail.ru").Result == null)
            {
                var user = new Employee
                {
                    UserName = "admin@mail.ru",
                    Email = "admin@mail.ru"
                };
                var result = userManager.CreateAsync(user, "Pass.123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Администратор").Wait();
                }
            }
        }
        #endregion

        #region SeedRoles
        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Администратор").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Администратор"
                };
                var result = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Руководитель").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Руководитель"
                };
                var result = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Директор").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Директор"
                };
                var result = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Сотрудник").Result)
            {
                var role = new IdentityRole
                {
                    Name = "Сотрудник"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
        }
        #endregion

        #region SeedTypeReques
        private static void SeedTypeReques(ApplicationDbContext db)
        {
            if (!db.TypesRequests.Any())
            {
                db.AddRangeAsync(
                    new TypeRequest
                    {
                        Name = "Заявка на отпуск"
                    },
                    new TypeRequest
                    {
                        Name = "Заявка на выходной в счет отпуска"
                    },
                    new TypeRequest
                    {
                        Name = "Заявка на выходной в счет отработки"
                    },
                    new TypeRequest
                    {
                        Name = "Заявка на отгул"
                    },
                    new TypeRequest
                    {
                        Name = "Заявка на удаленную работу"
                    });
                db.SaveChangesAsync().Wait();
            }
        }
        #endregion

        #region SeedStatusReques
        private static void SeedStatusReques(ApplicationDbContext db)
        {
            if (!db.StatusRequests.Any())
            {
                db.AddRangeAsync(
                    new StatusRequest
                    {
                        Name = "Новая"
                    },
                    new StatusRequest
                    {
                        Name = "Отправлена на согласование"
                    },
                    new StatusRequest
                    {
                        Name = "Согласована"
                    },
                    new StatusRequest
                    {
                        Name = "Не согласована"
                    },
                    new StatusRequest
                    {
                        Name = "Утверждена"
                    },
                    new StatusRequest
                    {
                        Name = "Не утверждена"
                    },
                    new StatusRequest
                    {
                        Name = "Отозвана"
                    },
                    new StatusRequest
                    {
                        Name = "Отменена"
                    },
                    new StatusRequest
                    {
                        Name = "Ожидает отмены руководителем"
                    },
                    new StatusRequest
                    {
                        Name = "Ожидает отмены директором"
                    });
                db.SaveChangesAsync().Wait();
            }
        }
        #endregion

        #region SeedTypeVocation
        private static void SeedTypeVocation(ApplicationDbContext db)
        {
            if (!db.TypesVocations.Any())
            {
                db.AddRangeAsync(
                    new TypeVocation
                    {
                        Name = "Основной оплачиваемый отпуск"
                    },
                    new TypeVocation
                    {
                        Name = "Без сохранения ЗП"
                    },
                    new TypeVocation
                    {
                        Name = "Декретный отпуск"
                    },
                    new TypeVocation
                    {
                        Name = "По уходу за ребенком"
                    });
                db.SaveChangesAsync().Wait();
            }
        }
        #endregion

        #region SeedAttributes
        private static void SeedAttributes(ApplicationDbContext db)
        {
            if (!db.Attributes.Any())
            {
                db.AddRangeAsync(
                    new Attributes
                    {
                        NameAttributes = "Тип отпуска",
                        TypeData = "TypeVocation"
                    },
                    new Attributes
                    {
                        NameAttributes = "Заменяющий сотрудник",
                        TypeData = "Employee"
                    },
                    new Attributes
                    {
                        NameAttributes = "Возможность передвинуть отпуск",
                        TypeData = "Bool"
                    },
                    new Attributes
                    {
                        NameAttributes = "Дата отработки",
                        TypeData = "DateTime"
                    },
                    new Attributes
                    {
                        NameAttributes = "Рабочий план",
                        TypeData = "Text"
                    });
                db.SaveChangesAsync().Wait();
            }
        }
        #endregion

        #region SeedAttributes_TypeRequest
        private static void SeedAttributes_TypeRequest(ApplicationDbContext db)
        {
            if (!db.Attributes_TypesRequests.Any())
            {
                db.AddRangeAsync(
                    new Attributes_TypeRequest
                    {
                        TypeRequest_id = db.TypesRequests.FirstOrDefault(
                            x => x.Name == "Заявка на отпуск").Id,
                        Attr_id = db.Attributes.FirstOrDefault(
                            x => x.NameAttributes == "Тип отпуска").Id
                    },
                    new Attributes_TypeRequest
                    {
                        TypeRequest_id = db.TypesRequests.FirstOrDefault(
                            x => x.Name == "Заявка на отпуск").Id,
                        Attr_id = db.Attributes.FirstOrDefault(
                            x => x.NameAttributes == "Заменяющий сотрудник").Id
                    },
                    new Attributes_TypeRequest
                    {
                        TypeRequest_id = db.TypesRequests.FirstOrDefault(
                            x => x.Name == "Заявка на отпуск").Id,
                        Attr_id = db.Attributes.FirstOrDefault(
                            x => x.NameAttributes == "Возможность передвинуть отпуск").Id
                    },
                    new Attributes_TypeRequest
                    {
                        TypeRequest_id = db.TypesRequests.FirstOrDefault(
                            x => x.Name == "Заявка на выходной в счет отпуска").Id,
                        Attr_id = db.Attributes.FirstOrDefault(
                            x => x.NameAttributes == "Заменяющий сотрудник").Id
                    },
                    new Attributes_TypeRequest
                    {
                        TypeRequest_id = db.TypesRequests.FirstOrDefault(
                            x => x.Name == "Заявка на выходной в счет отработки").Id,
                        Attr_id = db.Attributes.FirstOrDefault(
                            x => x.NameAttributes == "Заменяющий сотрудник").Id
                    },
                    new Attributes_TypeRequest
                    {
                        TypeRequest_id = db.TypesRequests.FirstOrDefault(
                            x => x.Name == "Заявка на выходной в счет отработки").Id,
                        Attr_id = db.Attributes.FirstOrDefault(
                            x => x.NameAttributes == "Дата отработки").Id
                    },
                    new Attributes_TypeRequest
                    {
                        TypeRequest_id = db.TypesRequests.FirstOrDefault(
                            x => x.Name == "Заявка на отгул").Id,
                        Attr_id = db.Attributes.FirstOrDefault(
                            x => x.NameAttributes == "Заменяющий сотрудник").Id
                    },
                    new Attributes_TypeRequest
                    {
                        TypeRequest_id = db.TypesRequests.FirstOrDefault(
                            x => x.Name == "Заявка на удаленную работу").Id,
                        Attr_id = db.Attributes.FirstOrDefault(
                            x => x.NameAttributes == "Рабочий план").Id
                    });
                db.SaveChangesAsync().Wait();
            }
        }
        #endregion

    }
}
