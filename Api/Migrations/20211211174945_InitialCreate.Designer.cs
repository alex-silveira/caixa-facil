// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20211211174945_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("Api.Models.Cashier", b =>
                {
                    b.Property<int>("cashierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("amount")
                        .HasColumnType("int(4)");

                    b.Property<int?>("barCode")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("saleDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("unitValue")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("cashierId");

                    b.ToTable("Cashier");
                });

            modelBuilder.Entity("Api.Models.Inventory", b =>
                {
                    b.Property<int>("inventoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("amount")
                        .HasColumnType("int");

                    b.Property<int?>("barCode")
                        .HasColumnType("int");

                    b.Property<DateTime?>("expirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("registerDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("unitValue")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("inventoryId");

                    b.ToTable("Inventory");
                });
#pragma warning restore 612, 618
        }
    }
}
