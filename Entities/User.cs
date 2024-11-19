using System;
using System.Collections.Generic;

namespace PokemonAPINet.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Sexe { get; set; } = null!;

    public int Money { get; set; }
}
