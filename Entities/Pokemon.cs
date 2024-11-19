using System;
using System.Collections.Generic;

namespace PokemonAPINet.Entities;

public partial class Pokemon
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int Health { get; set; }

    public int Attack { get; set; }

    public int Defense { get; set; }
}
