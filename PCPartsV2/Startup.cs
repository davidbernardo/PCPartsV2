using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using PCPartsV2.Models;

[assembly: OwinStartupAttribute(typeof(PCPartsV2.Startup))]
namespace PCPartsV2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //IniciaAplicacao();
        }
        /*
        /// <summary>
        /// cria, caso não existam, as Roles de suporte à aplicação: Veterinario, Funcionario, Dono
        /// cria, nesse caso, também, um utilizador...
        /// </summary>
        private void IniciaAplicacao()
        {

            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // Criar a role 'User'
            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);

                // criar um utilizador 'User'
                var user = new ApplicationUser();
                user.UserName = "user@mail.pt"; //LOGIN
                user.Email = "user@mail.pt";
                string userPWD = "123Qwe!"; //PASSWORD
                var chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Funcionario-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "User");
                }
            }

            // Criar a role 'Admin'
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                // criar um utilizador 'Dono'
                var user = new ApplicationUser();
                user.UserName = "admin@mail.pt"; //LOGIN
                user.Email = "admin@mail.pt";
                string userPWD = "123Qwe!"; //PASSWORD
                var chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Dono-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Admin");
                }
            }
        }
        */
    }
}
