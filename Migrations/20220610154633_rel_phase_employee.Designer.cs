﻿// <auto-generated />
using System;
using Enquiry.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Enquiry.Web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220610154633_rel_phase_employee")]
    partial class rel_phase_employee
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Enquiry.Web.Models.Clients", b =>
                {
                    b.Property<int>("EnquiryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("AppoinmentDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("BDA")
                        .HasColumnType("int");

                    b.Property<string>("ClientName")
                        .HasColumnType("longtext");

                    b.Property<string>("Contact")
                        .HasColumnType("longtext");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Domain")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("EnquiryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("EnquiryRef")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsTechAppoinment")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PaymentRemarks")
                        .HasColumnType("longtext");

                    b.Property<bool>("Registered")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("TechExpert")
                        .HasColumnType("int");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("EnquiryId");

                    b.HasIndex("BDA");

                    b.HasIndex("TechExpert");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Enquiry.Web.Models.Department", b =>
                {
                    b.Property<int>("DeptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DeptName")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("DeptId");

                    b.ToTable("Department");

                    b.HasData(
                        new
                        {
                            DeptId = 1,
                            DeptName = "BDA",
                            IsActive = true
                        },
                        new
                        {
                            DeptId = 2,
                            DeptName = "Tech",
                            IsActive = true
                        },
                        new
                        {
                            DeptId = 3,
                            DeptName = "Programming",
                            IsActive = true
                        },
                        new
                        {
                            DeptId = 4,
                            DeptName = "Publication",
                            IsActive = true
                        });
                });

            modelBuilder.Entity("Enquiry.Web.Models.Employees", b =>
                {
                    b.Property<int>("EmpId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("ContactNo")
                        .HasColumnType("longtext");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DOJ")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("DeptId")
                        .HasColumnType("int");

                    b.Property<string>("EmailId")
                        .HasColumnType("longtext");

                    b.Property<string>("EmployeeName")
                        .HasColumnType("longtext");

                    b.Property<string>("IdentityNo")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("EmpId");

                    b.HasIndex("DeptId");

                    b.HasIndex("RoleId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Enquiry.Web.Models.History", b =>
                {
                    b.Property<int>("HistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Comments")
                        .HasColumnType("longtext");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EmpId")
                        .HasColumnType("int");

                    b.Property<int>("EnquiryId")
                        .HasColumnType("int");

                    b.Property<bool>("IsComment")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("HistoryId");

                    b.HasIndex("EmpId");

                    b.HasIndex("EnquiryId");

                    b.ToTable("History");
                });

            modelBuilder.Entity("Enquiry.Web.Models.PaymentMode", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PaymentType")
                        .HasColumnType("longtext");

                    b.HasKey("PaymentId");

                    b.ToTable("PaymentMode");

                    b.HasData(
                        new
                        {
                            PaymentId = 1,
                            IsActive = true,
                            PaymentType = "Cash"
                        },
                        new
                        {
                            PaymentId = 2,
                            IsActive = true,
                            PaymentType = "Other"
                        },
                        new
                        {
                            PaymentId = 3,
                            IsActive = true,
                            PaymentType = "Bank"
                        });
                });

            modelBuilder.Entity("Enquiry.Web.Models.Payments", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("PartPayment")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PaymentType")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("PaymentId");

                    b.HasIndex("PaymentType");

                    b.HasIndex("ProjectId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Enquiry.Web.Models.Phase", b =>
                {
                    b.Property<int>("PhaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeadLine")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PhaseName")
                        .HasColumnType("longtext");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("TechExpert")
                        .HasColumnType("int");

                    b.HasKey("PhaseId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("Status");

                    b.HasIndex("TechExpert");

                    b.ToTable("Phase");
                });

            modelBuilder.Entity("Enquiry.Web.Models.ProjectHistory", b =>
                {
                    b.Property<int>("HistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Comments")
                        .HasColumnType("longtext");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EmpId")
                        .HasColumnType("int");

                    b.Property<bool>("IsComment")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("HistoryId");

                    b.HasIndex("EmpId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectHistory");
                });

            modelBuilder.Entity("Enquiry.Web.Models.Projects", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EnquiryId")
                        .HasColumnType("int");

                    b.Property<string>("FeeConfirmation")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ProjectName")
                        .HasColumnType("longtext");

                    b.Property<string>("ProjectRef")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("TotalPayment")
                        .HasColumnType("int");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ProjectId");

                    b.HasIndex("EnquiryId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Enquiry.Web.Models.Roles", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("RoleName")
                        .HasColumnType("longtext");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            IsActive = true,
                            RoleName = "Manager"
                        },
                        new
                        {
                            RoleId = 2,
                            IsActive = true,
                            RoleName = "Team Lead"
                        },
                        new
                        {
                            RoleId = 3,
                            IsActive = true,
                            RoleName = "Executive"
                        });
                });

            modelBuilder.Entity("Enquiry.Web.Models.WorkStatus", b =>
                {
                    b.Property<int>("WorkStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("WorkStatusName")
                        .HasColumnType("longtext");

                    b.HasKey("WorkStatusId");

                    b.ToTable("WorkStatus");

                    b.HasData(
                        new
                        {
                            WorkStatusId = 1,
                            IsActive = true,
                            WorkStatusName = "Assigned"
                        },
                        new
                        {
                            WorkStatusId = 2,
                            IsActive = true,
                            WorkStatusName = "Progress"
                        },
                        new
                        {
                            WorkStatusId = 3,
                            IsActive = true,
                            WorkStatusName = "Hold"
                        },
                        new
                        {
                            WorkStatusId = 4,
                            IsActive = true,
                            WorkStatusName = "Completed"
                        });
                });

            modelBuilder.Entity("Enquiry.Web.Models.Clients", b =>
                {
                    b.HasOne("Enquiry.Web.Models.Employees", "BDAEmployee")
                        .WithMany()
                        .HasForeignKey("BDA");

                    b.HasOne("Enquiry.Web.Models.Employees", "TechEmployee")
                        .WithMany()
                        .HasForeignKey("TechExpert");

                    b.Navigation("BDAEmployee");

                    b.Navigation("TechEmployee");
                });

            modelBuilder.Entity("Enquiry.Web.Models.Employees", b =>
                {
                    b.HasOne("Enquiry.Web.Models.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DeptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Enquiry.Web.Models.Roles", "Roles")
                        .WithMany("Employees")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Enquiry.Web.Models.History", b =>
                {
                    b.HasOne("Enquiry.Web.Models.Employees", "Employees")
                        .WithMany()
                        .HasForeignKey("EmpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Enquiry.Web.Models.Clients", "Clients")
                        .WithMany("History")
                        .HasForeignKey("EnquiryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clients");

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Enquiry.Web.Models.Payments", b =>
                {
                    b.HasOne("Enquiry.Web.Models.PaymentMode", "PaymentMode")
                        .WithMany()
                        .HasForeignKey("PaymentType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Enquiry.Web.Models.Projects", "Projects")
                        .WithMany("Payments")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentMode");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("Enquiry.Web.Models.Phase", b =>
                {
                    b.HasOne("Enquiry.Web.Models.Projects", "Projects")
                        .WithMany("Phase")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Enquiry.Web.Models.WorkStatus", "WorkStatus")
                        .WithMany()
                        .HasForeignKey("Status")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Enquiry.Web.Models.Employees", "Employees")
                        .WithMany()
                        .HasForeignKey("TechExpert");

                    b.Navigation("Employees");

                    b.Navigation("Projects");

                    b.Navigation("WorkStatus");
                });

            modelBuilder.Entity("Enquiry.Web.Models.ProjectHistory", b =>
                {
                    b.HasOne("Enquiry.Web.Models.Employees", "Employees")
                        .WithMany()
                        .HasForeignKey("EmpId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Enquiry.Web.Models.Projects", "Projects")
                        .WithMany("ProjectHistory")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employees");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("Enquiry.Web.Models.Projects", b =>
                {
                    b.HasOne("Enquiry.Web.Models.Clients", "Clients")
                        .WithMany("Projects")
                        .HasForeignKey("EnquiryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clients");
                });

            modelBuilder.Entity("Enquiry.Web.Models.Clients", b =>
                {
                    b.Navigation("History");

                    b.Navigation("Projects");
                });

            modelBuilder.Entity("Enquiry.Web.Models.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("Enquiry.Web.Models.Projects", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("Phase");

                    b.Navigation("ProjectHistory");
                });

            modelBuilder.Entity("Enquiry.Web.Models.Roles", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
