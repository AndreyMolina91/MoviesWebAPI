﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Movies.DataAccess.Context;
using NetTopologySuite.Geometries;

namespace Movies.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211127185413_UbicationTheaterWithNetTopologySuit")]
    partial class UbicationTheaterWithNetTopologySuit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Movies.Domain.Models.ActorModels", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ActorModels");
                });

            modelBuilder.Entity("Movies.Domain.Models.GenreModels", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GenresModels");
                });

            modelBuilder.Entity("Movies.Domain.Models.MovieModels", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("MovieRelease")
                        .HasColumnType("datetime2");

                    b.Property<bool>("OnCinema")
                        .HasColumnType("bit");

                    b.Property<string>("Poster")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("MovieModels");
                });

            modelBuilder.Entity("Movies.Domain.Models.MovieTheatreModels", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<Point>("Ubication")
                        .HasColumnType("geography");

                    b.HasKey("Id");

                    b.ToTable("MovieTheatreModels");
                });

            modelBuilder.Entity("Movies.Domain.Models.MoviesAndActorsModels", b =>
                {
                    b.Property<int>("ActorModelsId")
                        .HasColumnType("int");

                    b.Property<int>("MovieModelsId")
                        .HasColumnType("int");

                    b.Property<string>("MovieCharacter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.HasKey("ActorModelsId", "MovieModelsId");

                    b.HasIndex("MovieModelsId");

                    b.ToTable("MoviesAndActorsModels");
                });

            modelBuilder.Entity("Movies.Domain.Models.MoviesAndGenresModels", b =>
                {
                    b.Property<int>("GenreModelsId")
                        .HasColumnType("int");

                    b.Property<int>("MovieModelsId")
                        .HasColumnType("int");

                    b.HasKey("GenreModelsId", "MovieModelsId");

                    b.HasIndex("MovieModelsId");

                    b.ToTable("MoviesAndGenresModels");
                });

            modelBuilder.Entity("Movies.Domain.Models.MoviesAndMovieTheatresModels", b =>
                {
                    b.Property<int>("MovieModelsId")
                        .HasColumnType("int");

                    b.Property<int>("MovieTheatreModelsId")
                        .HasColumnType("int");

                    b.HasKey("MovieModelsId", "MovieTheatreModelsId");

                    b.HasIndex("MovieTheatreModelsId");

                    b.ToTable("MoviesAndMovieTheatresModels");
                });

            modelBuilder.Entity("Movies.Domain.Models.MoviesAndActorsModels", b =>
                {
                    b.HasOne("Movies.Domain.Models.ActorModels", "ActorModels")
                        .WithMany("MoviesAndActorsModels")
                        .HasForeignKey("ActorModelsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Movies.Domain.Models.MovieModels", "MovieModels")
                        .WithMany("MoviesAndActorsModels")
                        .HasForeignKey("MovieModelsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActorModels");

                    b.Navigation("MovieModels");
                });

            modelBuilder.Entity("Movies.Domain.Models.MoviesAndGenresModels", b =>
                {
                    b.HasOne("Movies.Domain.Models.GenreModels", "GenreModels")
                        .WithMany("MoviesAndGenresModels")
                        .HasForeignKey("GenreModelsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Movies.Domain.Models.MovieModels", "MovieModels")
                        .WithMany("MoviesAndGenresModels")
                        .HasForeignKey("MovieModelsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GenreModels");

                    b.Navigation("MovieModels");
                });

            modelBuilder.Entity("Movies.Domain.Models.MoviesAndMovieTheatresModels", b =>
                {
                    b.HasOne("Movies.Domain.Models.MovieModels", "Movies")
                        .WithMany("MoviesAndMovieTheatresModels")
                        .HasForeignKey("MovieModelsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Movies.Domain.Models.MovieTheatreModels", "MovieTheatre")
                        .WithMany("MoviesAndMovieTheatresModels")
                        .HasForeignKey("MovieTheatreModelsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movies");

                    b.Navigation("MovieTheatre");
                });

            modelBuilder.Entity("Movies.Domain.Models.ActorModels", b =>
                {
                    b.Navigation("MoviesAndActorsModels");
                });

            modelBuilder.Entity("Movies.Domain.Models.GenreModels", b =>
                {
                    b.Navigation("MoviesAndGenresModels");
                });

            modelBuilder.Entity("Movies.Domain.Models.MovieModels", b =>
                {
                    b.Navigation("MoviesAndActorsModels");

                    b.Navigation("MoviesAndGenresModels");

                    b.Navigation("MoviesAndMovieTheatresModels");
                });

            modelBuilder.Entity("Movies.Domain.Models.MovieTheatreModels", b =>
                {
                    b.Navigation("MoviesAndMovieTheatresModels");
                });
#pragma warning restore 612, 618
        }
    }
}