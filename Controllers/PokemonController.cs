using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonAPINet.Entities;

namespace PokemonAPINet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PokemonController : ControllerBase
{
    private readonly PokemonbddContext DBContext;

    public PokemonController( PokemonbddContext DBContext)
    {
        this.DBContext = DBContext;
    }
    
    [HttpGet("GetPokemon")]
    public async Task<ActionResult<List<Pokemon>>> Get()
    {
        var List = await DBContext.Pokemons.Select(
            s => new Pokemon
            {
                Id = s.Id,
                Name = s.Name,
                Type = s.Type,
                Health = s.Health,
                Attack = s.Attack,
                Defense = s.Defense
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
    
    [HttpGet("GetPokemonById/{Id}")]
    public async Task < ActionResult < Pokemon >> GetPokemonById(int Id) {
        Pokemon? pokemon = await DBContext.Pokemons.Select(s => new Pokemon() {
            Id = s.Id,
            Name = s.Name,
            Type = s.Type,
            Health = s.Health,
            Attack = s.Attack,
            Defense = s.Defense,
        }).FirstOrDefaultAsync(s => s.Id == Id);
        if (pokemon == null) {
            return NotFound();
        } else {
            return pokemon;
        }
    }
    
    [HttpPost("InsertPokemon")]
    public async Task < HttpStatusCode > InsertPokemon(Pokemon pokemon) {
        var entity = new Pokemon() {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Type = pokemon.Type,
            Health = pokemon.Health,
            Attack = pokemon.Attack,
            Defense = pokemon.Defense,
        };
        DBContext.Pokemons.Add(entity);
        await DBContext.SaveChangesAsync();
        return HttpStatusCode.Created;
    }
    
    [HttpPut("UpdatePokemon")]
    public async Task < HttpStatusCode > UpdatePokemon(Pokemon pokemon) {
        var entity = await DBContext.Pokemons.FirstOrDefaultAsync(s => s.Id == pokemon.Id);
        entity.Id = pokemon.Id;
        entity.Name = pokemon.Name;
        entity.Type = pokemon.Type;
        entity.Health = pokemon.Health;
        entity.Attack = pokemon.Attack;
        entity.Defense = pokemon.Defense;
        await DBContext.SaveChangesAsync();
        return HttpStatusCode.OK;
    }
    
    [HttpDelete("DeletePokemon/{Id}")]
    public async Task < HttpStatusCode > DeletePokemon(int Id) {
        var entity = new Pokemon() {
            Id = Id
        };
        DBContext.Pokemons.Attach(entity);
        DBContext.Pokemons.Remove(entity);
        await DBContext.SaveChangesAsync();
        return HttpStatusCode.OK;
    }
}