using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonAPINet.Entities;

namespace PokemonAPINet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly PokemonbddContext DBContext;

    public UserController( PokemonbddContext DBContext)
    {
        this.DBContext = DBContext;
    }
    
    [HttpGet("GetUsers")]
    public async Task<ActionResult<List<User>>> Get()
    {
        var List = await DBContext.Users.Select(
            s => new User
            {
                Id = s.Id,
                Name = s.Name,
                Sexe = s.Sexe,
                Money = s.Money,
            }
        ).ToListAsync();

        if (List.Count < 0)
        {
            return NotFound();
        }
        else
        {
            return List;
        }
    }
    
    [HttpGet("GetUserById")]
    public async Task < ActionResult < User >> GetUserById(int Id) {
        User? user = await DBContext.Users.Select(s => new User() {
            Id = s.Id,
            Name = s.Name,
            Sexe = s.Sexe,
            Money = s.Money,
        }).FirstOrDefaultAsync(s => s.Id == Id);
        if (user == null) {
            return NotFound();
        } else {
            return user;
        }
    }
    
    [HttpPost("InsertUser")]
    public async Task < HttpStatusCode > InsertUser(User user) {
        var entity = new User() {
            Id = user.Id,
            Name = user.Name,
            Sexe = user.Sexe,
            Money = user.Money,
        };
        DBContext.Users.Add(entity);
        await DBContext.SaveChangesAsync();
        return HttpStatusCode.Created;
    }
    
    [HttpPut("UpdateUser")]
    public async Task < HttpStatusCode > UpdateUser(User user) {
        var entity = await DBContext.Users.FirstOrDefaultAsync(s => s.Id == user.Id);
        entity.Id = user.Id;
        entity.Name = user.Name;
        entity.Sexe = user.Sexe;
        entity.Money = user.Money;
        await DBContext.SaveChangesAsync();
        return HttpStatusCode.OK;
    }
    
    [HttpDelete("DeleteUser/{Id}")]
    public async Task < HttpStatusCode > DeleteUser(int Id) {
        var entity = new User() {
            Id = Id
        };
        DBContext.Users.Attach(entity);
        DBContext.Users.Remove(entity);
        await DBContext.SaveChangesAsync();
        return HttpStatusCode.OK;
    }
}