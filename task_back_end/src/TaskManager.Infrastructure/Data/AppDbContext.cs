using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Models;

namespace TaskManager.Infrastructure.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Availability> Availabilities { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Intervention> Interventions { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SubSystemType> SubSystemTypes { get; set; }

    public virtual DbSet<SubSystemTypeExpertise> SubSystemTypeExpertises { get; set; }

    public virtual DbSet<SystemType> SystemTypes { get; set; }

    public virtual DbSet<Technician> Technicians { get; set; }

    public virtual DbSet<UrgencyLevel> UrgencyLevels { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Availability>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("availability_pkey");

            entity.ToTable("availability");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clients_pkey");

            entity.ToTable("clients");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Company).WithMany(p => p.Clients)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("clients_company_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Clients)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("clients_user_id_fkey");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("companies_pkey");

            entity.ToTable("companies");

            entity.HasIndex(e => e.Cui, "companies_cui_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Cui)
                .HasMaxLength(20)
                .HasColumnName("cui");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("contracts_pkey");

            entity.ToTable("contracts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientCompanyId).HasColumnName("client_company_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.TechnicianCompanyId).HasColumnName("technician_company_id");

            entity.HasOne(d => d.ClientCompany).WithMany(p => p.ContractClientCompanies)
                .HasForeignKey(d => d.ClientCompanyId)
                .HasConstraintName("contracts_client_company_id_fkey");

            entity.HasOne(d => d.TechnicianCompany).WithMany(p => p.ContractTechnicianCompanies)
                .HasForeignKey(d => d.TechnicianCompanyId)
                .HasConstraintName("contracts_technician_company_id_fkey");
        });

        modelBuilder.Entity<Intervention>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("interventions_pkey");

            entity.ToTable("interventions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.ClientSignature).HasColumnName("client_signature");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.LevelId).HasColumnName("level_id");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.TechnicianId).HasColumnName("technician_id");
            entity.Property(e => e.TechnicianSignature).HasColumnName("technician_signature");

            entity.HasOne(d => d.Client).WithMany(p => p.Interventions)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("interventions_client_id_fkey");

            entity.HasOne(d => d.Level).WithMany(p => p.Interventions)
                .HasForeignKey(d => d.LevelId)
                .HasConstraintName("interventions_level_id_fkey");

            entity.HasOne(d => d.Technician).WithMany(p => p.Interventions)
                .HasForeignKey(d => d.TechnicianId)
                .HasConstraintName("interventions_technician_id_fkey");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("invoices_pkey");

            entity.ToTable("invoices");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ContractId).HasColumnName("contract_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EmmitingDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("emmiting_date");
            entity.Property(e => e.InterventionId).HasColumnName("intervention_id");

            entity.HasOne(d => d.Contract).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.ContractId)
                .HasConstraintName("invoices_contract_id_fkey");

            entity.HasOne(d => d.Intervention).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.InterventionId)
                .HasConstraintName("invoices_intervention_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<SubSystemType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sub_system_types_pkey");

            entity.ToTable("sub_system_types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.SystemTypeId).HasColumnName("system_type_id");

            entity.HasOne(d => d.SystemType).WithMany(p => p.SubSystemTypes)
                .HasForeignKey(d => d.SystemTypeId)
                .HasConstraintName("sub_system_types_system_type_id_fkey");
        });

        modelBuilder.Entity<SubSystemTypeExpertise>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sub_system_type_expertise_pkey");

            entity.ToTable("sub_system_type_expertise");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SubSystemTypeId).HasColumnName("sub_system_type_id");
            entity.Property(e => e.TechnicianId).HasColumnName("technician_id");

            entity.HasOne(d => d.SubSystemType).WithMany(p => p.SubSystemTypeExpertises)
                .HasForeignKey(d => d.SubSystemTypeId)
                .HasConstraintName("sub_system_type_expertise_sub_system_type_id_fkey");

            entity.HasOne(d => d.Technician).WithMany(p => p.SubSystemTypeExpertises)
                .HasForeignKey(d => d.TechnicianId)
                .HasConstraintName("sub_system_type_expertise_technician_id_fkey");
        });

        modelBuilder.Entity<SystemType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("system_types_pkey");

            entity.ToTable("system_types");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Technician>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("technicians_pkey");

            entity.ToTable("technicians");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AvailabilityId).HasColumnName("availability_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .HasColumnName("phone");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Availability).WithMany(p => p.Technicians)
                .HasForeignKey(d => d.AvailabilityId)
                .HasConstraintName("technicians_availability_id_fkey");

            entity.HasOne(d => d.Company).WithMany(p => p.Technicians)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("technicians_company_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Technicians)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("technicians_user_id_fkey");
        });

        modelBuilder.Entity<UrgencyLevel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("urgency_levels_pkey");

            entity.ToTable("urgency_levels");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Cnp, "users_cnp_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cnp)
                .HasMaxLength(20)
                .HasColumnName("cnp");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.RefreshToken).HasColumnName("refresh_token");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("users_role_id_fkey");
        });

    }
}
