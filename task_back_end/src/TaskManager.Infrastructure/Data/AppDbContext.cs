using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Models;

namespace TaskManager.Infrastructure.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientContact> ClientContacts { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<InterventionDocument> InterventionDocuments { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }

    public virtual DbSet<Offer> Offers { get; set; }

    public virtual DbSet<OfferDevice> OfferDevices { get; set; }

    public virtual DbSet<OfferMaintenanceSystem> OfferMaintenanceSystems { get; set; }

    public virtual DbSet<Technician> Technicians { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Done in Startup.cs
        
        // Add UTC DateTime conversion
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_pkey");

            entity.ToTable("client");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CuiCnp)
                .HasMaxLength(255)
                .HasColumnName("cui_cnp");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
        });

        modelBuilder.Entity<ClientContact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("client_contact_pkey");

            entity.ToTable("client_contact");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.ContactName)
                .HasMaxLength(255)
                .HasColumnName("contact_name");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientContacts)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("client_contact_client_id_fkey");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("contract_pkey");

            entity.ToTable("contract");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.FreeInterventions).HasColumnName("free_interventions");
            entity.Property(e => e.OfferId).HasColumnName("offer_id");
            entity.Property(e => e.Signatures).HasColumnName("signatures");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.Offer).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.OfferId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("contract_offer_id_fkey");
        });

        modelBuilder.Entity<InterventionDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("intervention_document_pkey");

            entity.ToTable("intervention_document");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(255)
                .HasColumnName("contact_person");
            entity.Property(e => e.InterventionDate)
                .HasMaxLength(255)
                .HasColumnName("intervention_date");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.TimeInterval).HasColumnName("time_interval");
            entity.Property(e => e.WorkPointAddress).HasColumnName("work_point_address");
            entity.Property(e => e.TechnicianId).HasColumnName("technician_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasOne(d => d.Technician)
                .WithMany()
                .HasForeignKey(d => d.TechnicianId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.Client)
                .WithMany()
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("invoice_pkey");

            entity.ToTable("invoice");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientCompany)
                .HasMaxLength(255)
                .HasColumnName("client_company");
            entity.Property(e => e.IssueDate).HasColumnName("issue_date");
            entity.Property(e => e.IssuingCompany)
                .HasMaxLength(255)
                .HasColumnName("issuing_company");
            entity.Property(e => e.PaymentDue).HasColumnName("payment_due");
            entity.Property(e => e.Subtotal)
                .HasPrecision(10, 2)
                .HasColumnName("subtotal");
            entity.Property(e => e.Total)
                .HasPrecision(10, 2)
                .HasColumnName("total");
        });

        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("invoice_item_pkey");

            entity.ToTable("invoice_item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TotalPrice)
                .HasPrecision(10, 2)
                .HasColumnName("total_price");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(10, 2)
                .HasColumnName("unit_price");
            entity.Property(e => e.Vat)
                .HasPrecision(10, 2)
                .HasColumnName("vat");

            entity.HasOne(d => d.Invoice).WithMany(p => p.InvoiceItems)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("invoice_item_invoice_id_fkey");
        });

        modelBuilder.Entity<Offer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("offer_pkey");

            entity.ToTable("offer");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientCompany)
                .HasMaxLength(255)
                .HasColumnName("client_company");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.ValidityPeriod).HasColumnName("validity_period");
        });

        modelBuilder.Entity<OfferDevice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("offer_device_pkey");

            entity.ToTable("offer_device");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(255)
                .HasColumnName("category");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.OfferId).HasColumnName("offer_id");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SubCategory)
                .HasMaxLength(255)
                .HasColumnName("sub_category");
            entity.Property(e => e.SystemType)
                .HasMaxLength(255)
                .HasColumnName("system_type");

            entity.HasOne(d => d.Offer).WithMany(p => p.OfferDevices)
                .HasForeignKey(d => d.OfferId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("offer_device_offer_id_fkey");
        });

        modelBuilder.Entity<OfferMaintenanceSystem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("offer_maintenance_system_pkey");

            entity.ToTable("offer_maintenance_system");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.OfferId).HasColumnName("offer_id");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Offer).WithMany(p => p.OfferMaintenanceSystems)
                .HasForeignKey(d => d.OfferId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("offer_maintenance_system_offer_id_fkey");
        });

        modelBuilder.Entity<Technician>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("technician_pkey");

            entity.ToTable("technician");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .HasColumnName("phone_number");
            entity.Property(e => e.Signature).HasColumnName("signature");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
