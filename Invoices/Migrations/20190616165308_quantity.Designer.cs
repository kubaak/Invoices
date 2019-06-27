﻿// <auto-generated />
using System;
using Invoices.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Invoices.Migrations
{
    [DbContext(typeof(InvoiceDbContext))]
    [Migration("20190616165308_quantity")]
    partial class quantity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Invoices.Models.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfIssue");

                    b.Property<DateTime>("DueDate");

                    b.Property<int>("InvoicePayingStatus");

                    b.Property<int?>("SubscriberId");

                    b.Property<int?>("SupplierId");

                    b.Property<decimal>("Total");

                    b.HasKey("Id");

                    b.HasIndex("SubscriberId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Invoices");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfIssue = new DateTime(2019, 6, 16, 18, 53, 7, 512, DateTimeKind.Local).AddTicks(5308),
                            DueDate = new DateTime(2019, 6, 30, 18, 53, 7, 514, DateTimeKind.Local).AddTicks(2205),
                            InvoicePayingStatus = 0,
                            SubscriberId = 1,
                            SupplierId = 1,
                            Total = 99m
                        });
                });

            modelBuilder.Entity("Invoices.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int?>("InvoiceId");

                    b.Property<decimal>("Quantity");

                    b.Property<decimal>("UnitPrice");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Some goods",
                            InvoiceId = 1,
                            Quantity = 1m,
                            UnitPrice = 99m
                        });
                });

            modelBuilder.Entity("Invoices.Models.Subscriber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Dic");

                    b.Property<string>("Email");

                    b.Property<string>("Ic");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.ToTable("Subscribers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Some Address",
                            Name = "Test subscriber"
                        });
                });

            modelBuilder.Entity("Invoices.Models.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("BankAccount");

                    b.Property<string>("Dic");

                    b.Property<string>("Email");

                    b.Property<string>("Ic");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Some Address",
                            Name = "Test supplier"
                        });
                });

            modelBuilder.Entity("Invoices.Models.Invoice", b =>
                {
                    b.HasOne("Invoices.Models.Subscriber", "Subscriber")
                        .WithMany()
                        .HasForeignKey("SubscriberId");

                    b.HasOne("Invoices.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId");
                });

            modelBuilder.Entity("Invoices.Models.Item", b =>
                {
                    b.HasOne("Invoices.Models.Invoice", "Invoice")
                        .WithMany("Items")
                        .HasForeignKey("InvoiceId");
                });
#pragma warning restore 612, 618
        }
    }
}
