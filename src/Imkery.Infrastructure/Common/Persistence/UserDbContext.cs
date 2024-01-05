using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Imkery.Infrastructure.Common.Persistence;
internal class UserDbContext(DbContextOptions<UserDbContext> options)
    : IdentityDbContext<IdentityUser>(options)
{
}
