using System;
using System.Threading;
using System.Threading.Tasks;
using Database.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;

namespace Database.Api.Db
{
    public abstract class AbstractContext : DbContext
    {

        public const int KeyMaxLength = 15;
        public const int StringMaxLength = 255;
        public IConfiguration Configuration;

        protected AbstractContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Assets_Life_Cycle_Events> Assets_Life_Cycle_Events { get; set; }
        public DbSet<Life_Cycle_Phases> Life_Cycle_Phases { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Ref_Asset_Categories> Ref_Asset_Categories { get; set; }
        public DbSet<Ref_Sizes> Ref_Sizes { get; set; }
        public DbSet<Ref_Asset_Types> Ref_Asset_Types { get; set; }
        public DbSet<Ref_Asset_Supertypes> Ref_Asset_Supertypes { get; set; }
        public DbSet<Ref_Status> Ref_Status { get; set; }
        public DbSet<Responsible_Party> Responsible_Party { get; set; }

        public Task<int> SaveChangesAsync() => SaveChangesAsync(default);

        public virtual async Task RunMigrationsAsync(CancellationToken cancellationToken)
            => await Database.MigrateAsync(cancellationToken);

        public abstract bool IsUniqueConstraintViolationException(DbUpdateException exception);

        public virtual bool SupportsLimitInSubqueries => true;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Location>(BuildLocationEntity);
            builder.Entity<Ref_Sizes>(BuildRefSizeEntity);
            builder.Entity<Ref_Asset_Categories>(BuildRefAssetCategoryEntity);
            builder.Entity<Ref_Asset_Supertypes>(BuildRefAssetSupertypeEntity);
            builder.Entity<Ref_Asset_Types>(BuildRefAssetTypeEntity);
            builder.Entity<Ref_Status>(BuildRefStatusEntity);
            builder.Entity<Responsible_Party>(BuildResponsiblePartyEntity);
            builder.Entity<Life_Cycle_Phases>(BuildLifeCyclePhasesEntity);
            builder.Entity<Asset>(BuildAssetEntity);
            builder.Entity<Assets_Life_Cycle_Events>(BuildAssetLifeCycleEventEntity);

        }


        public void DeleteDb()
        {
            Database.EnsureDeleted();
        }

        private void BuildAssetLifeCycleEventEntity(EntityTypeBuilder<Assets_Life_Cycle_Events> entity)
        {
            entity.HasOne(obj => obj.Asset)
                .WithOne()
                .HasForeignKey<Assets_Life_Cycle_Events>(obj => obj.Asset_ID);

            entity.HasOne(obj => obj.Responsible_Party)
                .WithOne()
                .HasForeignKey<Assets_Life_Cycle_Events>(obj => obj.Party_ID);

            entity.HasOne(obj => obj.Location)
                .WithOne()
                .HasForeignKey<Assets_Life_Cycle_Events>(obj => obj.Location_ID);

            entity.HasOne(obj => obj.Life_Cycle_Phases)
                .WithOne()
                .HasForeignKey<Assets_Life_Cycle_Events>(obj => obj.Life_Cycle_Code);

            entity.HasOne(obj => obj.Ref_Status)
                .WithOne()
                .HasForeignKey<Assets_Life_Cycle_Events>(obj => obj.Status_Code);

            entity.HasKey(obj => obj.Asset_Life_Cycle_Event_ID);
            entity.HasIndex(obj => obj.Asset_Life_Cycle_Event_ID)
                .IsUnique();
            entity.Property(obj => obj.Asset_Life_Cycle_Event_ID)
                .IsRequired();

            entity.Property(obj => obj.Date_From)
                .HasConversion(obj => obj, obj => obj.ToOffset(new TimeSpan(-2, 0, 0)));
            entity.Property(obj => obj.Date_To)
                .HasConversion(obj => obj, obj => obj.ToOffset(new TimeSpan(-2, 0, 0)));


        }

        private void BuildAssetEntity(EntityTypeBuilder<Asset> entity)
        {
            entity.HasOne(obj => obj.Ref_Asset_Types)
                .WithOne()
                .HasForeignKey<Asset>(obj => obj.Asset_Type_Code);

            entity.HasOne(obj => obj.Ref_Sizes)
                .WithOne()
                .HasForeignKey<Asset>(obj => obj.Size_Code);

            entity.HasKey(obj => obj.Asset_ID);
            entity.HasIndex(obj => obj.Asset_ID)
                .IsUnique();
            entity.Property(obj => obj.Asset_ID)
                .IsRequired();

            entity.Property(obj => obj.Asset_Type_Code).
                HasMaxLength(KeyMaxLength);

            entity.Property(obj => obj.Asset_Name)
                .HasMaxLength(StringMaxLength);
            entity.Property(obj => obj.Other_Details)
                .HasMaxLength(StringMaxLength);
        }

        private void BuildResponsiblePartyEntity(EntityTypeBuilder<Responsible_Party> entity)
        {

            entity.HasKey(obj => obj.Party_ID);
            entity.HasIndex(obj => obj.Party_ID)
                .IsUnique();
            entity.Property(obj => obj.Party_ID)
                .IsRequired();

            entity.Property(obj => obj.Party_Details)
                .HasMaxLength(StringMaxLength);
        }
        private void BuildLifeCyclePhasesEntity(EntityTypeBuilder<Life_Cycle_Phases> entity)
        {

            entity.HasKey(obj => obj.Life_Cycle_Code);
            entity.Property(obj => obj.Life_Cycle_Code)
                .IsRequired()
                .HasMaxLength(KeyMaxLength);

            entity.Property(obj => obj.Life_Cycle_Description)
                .HasMaxLength(StringMaxLength);
            entity.Property(obj => obj.Life_Cycle_Name)
                .HasMaxLength(StringMaxLength);
        }
        private void BuildRefStatusEntity(EntityTypeBuilder<Ref_Status> entity)
        {

            entity.HasKey(obj => obj.Status_Code);
            entity.Property(obj => obj.Status_Code)
                .IsRequired()
                .HasMaxLength(KeyMaxLength);

            entity.Property(obj => obj.Status_Description)
                .HasMaxLength(StringMaxLength);
        }

        private void BuildRefAssetTypeEntity(EntityTypeBuilder<Ref_Asset_Types> entity)
        {
            entity.HasOne(obj => obj.Ref_Asset_Supertypes)
                .WithOne()
                .HasForeignKey<Ref_Asset_Types>(obj => obj.Asset_Supertype_Code);

            entity.HasKey(obj => obj.Asset_Type_Code);
            entity.Property(obj => obj.Asset_Type_Description)
                .IsRequired()
                .HasMaxLength(KeyMaxLength);

            entity.Property(obj => obj.Asset_Type_Code)
                .IsRequired()
                .HasMaxLength(KeyMaxLength);

            entity.Property(obj => obj.Asset_Supertype_Code).
                HasMaxLength(KeyMaxLength);

            entity.Property(obj => obj.Asset_Type_Description)
                .HasMaxLength(StringMaxLength);
        }
        private void BuildRefAssetSupertypeEntity(EntityTypeBuilder<Ref_Asset_Supertypes> entity)
        {
            entity.HasOne(obj => obj.Ref_Asset_Categories)
                .WithOne()
                .HasForeignKey<Ref_Asset_Supertypes>(obj => obj.Asset_Category_Code);

            entity.HasKey(obj => obj.Asset_Supertype_Code);
            entity.Property(obj => obj.Asset_Supertype_Code)
                .IsRequired()
                .HasMaxLength(KeyMaxLength);

            entity.Property(obj => obj.Asset_Category_Code).
                HasMaxLength(KeyMaxLength);

            entity.Property(obj => obj.Asset_Supertype_Description)
                .HasMaxLength(StringMaxLength);
        }
        private void BuildRefAssetCategoryEntity(EntityTypeBuilder<Ref_Asset_Categories> entity)
        {

            entity.HasKey(obj => obj.Asset_Category_Code);
            entity.Property(obj => obj.Asset_Category_Code)
                .IsRequired()
                .HasMaxLength(KeyMaxLength);

            entity.Property(obj => obj.Asset_Category_Description)
                .HasMaxLength(StringMaxLength);
        }

        private void BuildRefSizeEntity(EntityTypeBuilder<Ref_Sizes> entity)
        {
            entity.HasKey(obj => obj.Size_Code);
            entity.Property(obj => obj.Size_Code)
                .IsRequired()
                .HasMaxLength(KeyMaxLength);

            entity.Property(obj => obj.Size_Description)
                .HasMaxLength(StringMaxLength);
        }

        private void BuildLocationEntity(EntityTypeBuilder<Location> location)
        {
            location.HasKey(obj => obj.Location_ID);
            location.HasIndex(obj => obj.Location_ID);
            location.Property(obj => obj.Location_ID)
                .IsRequired();

            location.Property(obj => obj.Location_Details)
                .HasMaxLength(255);
        }

    }
}
