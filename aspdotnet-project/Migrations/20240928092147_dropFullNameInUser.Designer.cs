﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using aspdotnet_project.Context;

#nullable disable

namespace aspdotnet_project.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240928092147_dropFullNameInUser")]
    partial class dropFullNameInUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("FormatMovie", b =>
                {
                    b.Property<long>("FormatsId")
                        .HasColumnType("bigint");

                    b.Property<string>("MoviesId")
                        .HasColumnType("varchar(50)");

                    b.HasKey("FormatsId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("movie_format", (string)null);
                });

            modelBuilder.Entity("GenreMovie", b =>
                {
                    b.Property<long>("GenresId")
                        .HasColumnType("bigint");

                    b.Property<string>("MoviesId")
                        .HasColumnType("varchar(50)");

                    b.HasKey("GenresId", "MoviesId");

                    b.HasIndex("MoviesId");

                    b.ToTable("movie_genre", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "a6779ff5-736e-4f96-8f42-9be57507ea5b",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "35f9d6b7-172c-4c77-b554-3c2e0f6a1d1b",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("aspdotnet_project.App.Bill.entities.Bill", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("id");

                    b.Property<DateTime>("ExpireAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("expire_at");

                    b.Property<string>("FailureReason")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("failure_reason");

                    b.Property<DateTime>("PaymentAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("payment_at");

                    b.Property<string>("PaymentUrl")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("payment_url");

                    b.Property<long>("Total")
                        .HasColumnType("bigint")
                        .HasColumnName("total");

                    b.Property<long>("status_id")
                        .HasColumnType("bigint");

                    b.Property<string>("user_id")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("status_id");

                    b.HasIndex("user_id");

                    b.ToTable("bills");
                });

            modelBuilder.Entity("aspdotnet_project.App.Bill.entities.BillStatus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("bill_status");
                });

            modelBuilder.Entity("aspdotnet_project.App.Cinema.Entities.Cinema", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("address");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Hotline")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("hotline");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<long>("status_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("status_id");

                    b.ToTable("cinemas");
                });

            modelBuilder.Entity("aspdotnet_project.App.Cinema.Entities.CinemaStatus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("cinema_status");
                });

            modelBuilder.Entity("aspdotnet_project.App.Cinema.Entities.Hall", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.Property<int>("NumberOfRows")
                        .HasColumnType("int(11)")
                        .HasColumnName("number_of_rows");

                    b.Property<int>("SeatsPerRow")
                        .HasColumnType("int(11)")
                        .HasColumnName("seats_per_row");

                    b.Property<string>("cinema_id")
                        .HasColumnType("varchar(50)");

                    b.Property<long>("status_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("cinema_id");

                    b.HasIndex("status_id");

                    b.ToTable("halls");
                });

            modelBuilder.Entity("aspdotnet_project.App.Cinema.Entities.HallStatus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("hall_status");
                });

            modelBuilder.Entity("aspdotnet_project.App.Cinema.Entities.Seat", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("Order")
                        .HasColumnType("int(11)")
                        .HasColumnName("order");

                    b.Property<int>("RowIndex")
                        .HasColumnType("int(11)")
                        .HasColumnName("row_index");

                    b.Property<string>("RowName")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("varchar(1)")
                        .HasColumnName("row_name");

                    b.Property<ulong>("Status")
                        .HasColumnType("bit(1)")
                        .HasColumnName("status");

                    b.Property<long>("hall_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("hall_id");

                    b.ToTable("seats");
                });

            modelBuilder.Entity("aspdotnet_project.App.Movie.Entities.Format", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Caption")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("caption");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("slug");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("version");

                    b.HasKey("Id");

                    b.ToTable("formats");
                });

            modelBuilder.Entity("aspdotnet_project.App.Movie.Entities.Genre", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("genres");
                });

            modelBuilder.Entity("aspdotnet_project.App.Movie.Entities.Image", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("url");

                    b.Property<string>("movie_id")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("movie_id");

                    b.ToTable("images");
                });

            modelBuilder.Entity("aspdotnet_project.App.Movie.Entities.Movie", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("id");

                    b.Property<int>("AgeRestriction")
                        .HasColumnType("int(11)")
                        .HasColumnName("age_restriction");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("director");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date")
                        .HasColumnName("end_date");

                    b.Property<string>("HorizontalPoster")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("horizontal_poster");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("language");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.Property<int>("NumberOfRatings")
                        .HasColumnType("int(11)")
                        .HasColumnName("number_of_ratings");

                    b.Property<string>("Performers")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("performers");

                    b.Property<string>("Poster")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("poster");

                    b.Property<string>("Producer")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("producer");

                    b.Property<DateOnly>("ReleaseDate")
                        .HasColumnType("date")
                        .HasColumnName("release_date");

                    b.Property<int>("RunningTime")
                        .HasColumnType("int(11)")
                        .HasColumnName("running_time");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("slug");

                    b.Property<string>("SubName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("sub_name");

                    b.Property<int>("SumOfRatings")
                        .HasColumnType("int(11)")
                        .HasColumnName("sum_of_ratings");

                    b.Property<string>("Trailer")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("trailer");

                    b.Property<long>("status_id")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("status_id");

                    b.ToTable("movies");
                });

            modelBuilder.Entity("aspdotnet_project.App.Movie.Entities.MovieStatus", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("description");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("slug");

                    b.HasKey("Id");

                    b.ToTable("movie_status");
                });

            modelBuilder.Entity("aspdotnet_project.App.Show.Entities.Show", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("id");

                    b.Property<int>("RunningTime")
                        .HasColumnType("int(11)")
                        .HasColumnName("running_time");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date")
                        .HasColumnName("start_date");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time(6)")
                        .HasColumnName("start_time");

                    b.Property<ulong>("Status")
                        .HasColumnType("bit(1)")
                        .HasColumnName("status");

                    b.Property<long>("format_id")
                        .HasColumnType("bigint");

                    b.Property<long>("hall_id")
                        .HasColumnType("bigint");

                    b.Property<string>("movie_id")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("format_id");

                    b.HasIndex("hall_id");

                    b.HasIndex("movie_id");

                    b.ToTable("shows");
                });

            modelBuilder.Entity("aspdotnet_project.App.Show.Entities.Ticket", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("id");

                    b.Property<string>("bill_id")
                        .HasColumnType("varchar(50)");

                    b.Property<long>("seat_id")
                        .HasColumnType("bigint");

                    b.Property<string>("show_id")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("bill_id");

                    b.HasIndex("seat_id");

                    b.HasIndex("show_id");

                    b.ToTable("tickets");
                });

            modelBuilder.Entity("aspdotnet_project.App.User.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("avatar");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreateDate")
                        .HasMaxLength(6)
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_date");

                    b.Property<DateOnly?>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("enum('Female','Male','Unknown')")
                        .HasColumnName("gender");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("FormatMovie", b =>
                {
                    b.HasOne("aspdotnet_project.App.Movie.Entities.Format", null)
                        .WithMany()
                        .HasForeignKey("FormatsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("aspdotnet_project.App.Movie.Entities.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GenreMovie", b =>
                {
                    b.HasOne("aspdotnet_project.App.Movie.Entities.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("aspdotnet_project.App.Movie.Entities.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("aspdotnet_project.App.User.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("aspdotnet_project.App.User.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("aspdotnet_project.App.User.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("aspdotnet_project.App.User.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("aspdotnet_project.App.Bill.entities.Bill", b =>
                {
                    b.HasOne("aspdotnet_project.App.Bill.entities.BillStatus", "Status")
                        .WithMany("Bills")
                        .HasForeignKey("status_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("aspdotnet_project.App.User.Entities.User", "User")
                        .WithMany("Bills")
                        .HasForeignKey("user_id");

                    b.Navigation("Status");

                    b.Navigation("User");
                });

            modelBuilder.Entity("aspdotnet_project.App.Cinema.Entities.Cinema", b =>
                {
                    b.HasOne("aspdotnet_project.App.Cinema.Entities.CinemaStatus", "Status")
                        .WithMany("Cinemas")
                        .HasForeignKey("status_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("aspdotnet_project.App.Cinema.Entities.Hall", b =>
                {
                    b.HasOne("aspdotnet_project.App.Cinema.Entities.Cinema", "Cinema")
                        .WithMany("Halls")
                        .HasForeignKey("cinema_id");

                    b.HasOne("aspdotnet_project.App.Cinema.Entities.HallStatus", "Status")
                        .WithMany("Halls")
                        .HasForeignKey("status_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cinema");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("aspdotnet_project.App.Cinema.Entities.Seat", b =>
                {
                    b.HasOne("aspdotnet_project.App.Cinema.Entities.Hall", "Hall")
                        .WithMany("Seats")
                        .HasForeignKey("hall_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hall");
                });

            modelBuilder.Entity("aspdotnet_project.App.Movie.Entities.Image", b =>
                {
                    b.HasOne("aspdotnet_project.App.Movie.Entities.Movie", "Movie")
                        .WithMany("Images")
                        .HasForeignKey("movie_id");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("aspdotnet_project.App.Movie.Entities.Movie", b =>
                {
                    b.HasOne("aspdotnet_project.App.Movie.Entities.MovieStatus", "Status")
                        .WithMany("Movies")
                        .HasForeignKey("status_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");
                });

            modelBuilder.Entity("aspdotnet_project.App.Show.Entities.Show", b =>
                {
                    b.HasOne("aspdotnet_project.App.Movie.Entities.Format", "Format")
                        .WithMany("Shows")
                        .HasForeignKey("format_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("aspdotnet_project.App.Cinema.Entities.Hall", "Hall")
                        .WithMany("Shows")
                        .HasForeignKey("hall_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("aspdotnet_project.App.Movie.Entities.Movie", "Movie")
                        .WithMany("Shows")
                        .HasForeignKey("movie_id");

                    b.Navigation("Format");

                    b.Navigation("Hall");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("aspdotnet_project.App.Show.Entities.Ticket", b =>
                {
                    b.HasOne("aspdotnet_project.App.Bill.entities.Bill", "Bill")
                        .WithMany("Tickets")
                        .HasForeignKey("bill_id");

                    b.HasOne("aspdotnet_project.App.Cinema.Entities.Seat", "Seat")
                        .WithMany("Tickets")
                        .HasForeignKey("seat_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("aspdotnet_project.App.Show.Entities.Show", "Show")
                        .WithMany("Tickets")
                        .HasForeignKey("show_id");

                    b.Navigation("Bill");

                    b.Navigation("Seat");

                    b.Navigation("Show");
                });

            modelBuilder.Entity("aspdotnet_project.App.Bill.entities.Bill", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("aspdotnet_project.App.Bill.entities.BillStatus", b =>
                {
                    b.Navigation("Bills");
                });

            modelBuilder.Entity("aspdotnet_project.App.Cinema.Entities.Cinema", b =>
                {
                    b.Navigation("Halls");
                });

            modelBuilder.Entity("aspdotnet_project.App.Cinema.Entities.CinemaStatus", b =>
                {
                    b.Navigation("Cinemas");
                });

            modelBuilder.Entity("aspdotnet_project.App.Cinema.Entities.Hall", b =>
                {
                    b.Navigation("Seats");

                    b.Navigation("Shows");
                });

            modelBuilder.Entity("aspdotnet_project.App.Cinema.Entities.HallStatus", b =>
                {
                    b.Navigation("Halls");
                });

            modelBuilder.Entity("aspdotnet_project.App.Cinema.Entities.Seat", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("aspdotnet_project.App.Movie.Entities.Format", b =>
                {
                    b.Navigation("Shows");
                });

            modelBuilder.Entity("aspdotnet_project.App.Movie.Entities.Movie", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Shows");
                });

            modelBuilder.Entity("aspdotnet_project.App.Movie.Entities.MovieStatus", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("aspdotnet_project.App.Show.Entities.Show", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("aspdotnet_project.App.User.Entities.User", b =>
                {
                    b.Navigation("Bills");
                });
#pragma warning restore 612, 618
        }
    }
}
