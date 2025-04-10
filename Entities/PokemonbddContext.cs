﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PokemonAPINet.Entities;

public partial class PokemonbddContext : DbContext
{
    public PokemonbddContext()
    {
    }

    public PokemonbddContext(DbContextOptions<PokemonbddContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pokemon> Pokemons { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=;database=pokemonbdd");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pokemon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Pokemon");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Attack).HasColumnName("attack");
            entity.Property(e => e.Defense).HasColumnName("defense");
            entity.Property(e => e.Health).HasColumnName("health");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Money).HasColumnName("money");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Sexe)
                .HasMaxLength(10)
                .HasColumnName("sexe");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
