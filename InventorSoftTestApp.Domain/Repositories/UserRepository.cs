using InventorSoftTestApp.Domain.Contexts;
using InventorSoftTestApp.Domain.Models.DbEntities;
using InventorSoftTestApp.Domain.Repositories.Abstractions;

namespace InventorSoftTestApp.Domain.Repositories;

public class UserRepository(InventorSoftDbContext context)
    : GenericRepository<User>(context.Users), IUserRepository;