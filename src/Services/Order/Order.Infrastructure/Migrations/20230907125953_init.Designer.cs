﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Order.Infrastructure.Persistence;

#nullable disable

namespace Order.Infrastructure.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    [Migration("20230907125953_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Shared.Entities.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<decimal>("Total")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Shared.Entities.OrderItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Shared.Entities.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "T-Shirt",
                            Price = 1000m,
                            Quantity = 30
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Suit",
                            Price = 1000m,
                            Quantity = 30
                        },
                        new
                        {
                            Id = 3L,
                            Name = "Cap",
                            Price = 1000m,
                            Quantity = 30
                        },
                        new
                        {
                            Id = 4L,
                            Name = "Belt",
                            Price = 1000m,
                            Quantity = 30
                        },
                        new
                        {
                            Id = 5L,
                            Name = "Gloves",
                            Price = 1000m,
                            Quantity = 30
                        },
                        new
                        {
                            Id = 6L,
                            Name = "Sweater",
                            Price = 1000m,
                            Quantity = 30
                        },
                        new
                        {
                            Id = 7L,
                            Name = "Bra",
                            Price = 1000m,
                            Quantity = 30
                        },
                        new
                        {
                            Id = 8L,
                            Name = "Boots",
                            Price = 1000m,
                            Quantity = 30
                        },
                        new
                        {
                            Id = 9L,
                            Name = "Jumper",
                            Price = 1000m,
                            Quantity = 30
                        },
                        new
                        {
                            Id = 10L,
                            Name = "Sockets",
                            Price = 1000m,
                            Quantity = 30
                        },
                        new
                        {
                            Id = 11L,
                            Name = "Jeans",
                            Price = 1000m,
                            Quantity = 30
                        },
                        new
                        {
                            Id = 12L,
                            Name = "Dress",
                            Price = 1000m,
                            Quantity = 30
                        },
                        new
                        {
                            Id = 13L,
                            Name = "Towel",
                            Price = 1000m,
                            Quantity = 30
                        },
                        new
                        {
                            Id = 14L,
                            Name = "Tie",
                            Price = 1000m,
                            Quantity = 30
                        },
                        new
                        {
                            Id = 15L,
                            Name = "Shoes",
                            Price = 1000m,
                            Quantity = 30
                        },
                        new
                        {
                            Id = 16L,
                            Name = "Sandal",
                            Price = 1000m,
                            Quantity = 30
                        },
                        new
                        {
                            Id = 17L,
                            Name = "Oxford shoe",
                            Price = 1000m,
                            Quantity = 30
                        },
                        new
                        {
                            Id = 18L,
                            Name = "Brogue shoe",
                            Price = 1000m,
                            Quantity = 30
                        },
                        new
                        {
                            Id = 19L,
                            Name = "Platform shoe",
                            Price = 1000m,
                            Quantity = 30
                        },
                        new
                        {
                            Id = 20L,
                            Name = "Clog",
                            Price = 1000m,
                            Quantity = 30
                        });
                });

            modelBuilder.Entity("Shared.Entities.Order", b =>
                {
                    b.OwnsOne("Shared.Entities.Address", "Address", b1 =>
                        {
                            b1.Property<long>("OrderId")
                                .HasColumnType("bigint");

                            b1.Property<string>("District")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Line")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("PhoneNo")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Province")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Shared.Entities.OrderItem", b =>
                {
                    b.HasOne("Shared.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Shared.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
