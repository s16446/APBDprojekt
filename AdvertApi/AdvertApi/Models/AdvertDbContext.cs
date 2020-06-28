using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertApi.Models
{
	public class AdvertDbContext : DbContext
	{
		public DbSet<Building> Buildings { get; set; }
		public DbSet<Client> Clients { get; set; }
		public DbSet<Campaign> Campaigns { get; set; }
		public DbSet<Banner> Banners { get; set; }

		public AdvertDbContext()
		{

		}

		public AdvertDbContext(DbContextOptions options ) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) 
		{
			base.OnModelCreating(modelBuilder);
			
			modelBuilder.Entity<Building>(opt => 
			{
				opt.HasKey(p => p.IdBuilding);
				opt.Property(p => p.IdBuilding)
					.ValueGeneratedOnAdd();
				
				opt.Property(p => p.Street)
					.HasMaxLength(100)
					.IsRequired();

				opt.Property(p => p.City)
					.HasMaxLength(100)
					.IsRequired();

				opt.Property(p => p.Height)
					.HasColumnType("decimal(6,2)");

				opt.Property(e => e.Height).HasColumnType("decimal(6,2)");

			});

			modelBuilder.Entity<Campaign>(opt => 
			{
				opt.HasKey(c => c.IdCampaign);
				opt.Property(p => p.IdCampaign)
					.ValueGeneratedOnAdd();

                opt.HasOne(m => m.FromBuilding)
					.WithMany(t => t.FromCampaigns)
					.HasForeignKey(m => m.FromIdBuilding)
					.OnDelete(DeleteBehavior.ClientCascade)
                   ;

				opt.HasOne(m => m.ToBuilding)
                    .WithMany(t => t.ToCampaigns)
                    .HasForeignKey(m => m.ToIdBuilding)
					.OnDelete(DeleteBehavior.ClientCascade)
                   ;

				opt.HasOne(m => m.Client)
                    .WithMany(t => t.Campaigns)
                    .HasForeignKey(m => m.IdClient)
					;

				opt.Property(e => e.PricePerSquareMeter).HasColumnType("decimal(6,2)");

				opt.Property(e => e.StartDate).HasColumnType("date");
				opt.Property(e => e.EndDate).HasColumnType("date");
                   
			});

			modelBuilder.Entity<Banner>(opt => // 1 kampania - * bannerów
            {
				opt.HasKey(c => c.IdAdvertisement);
				opt.Property(p => p.IdAdvertisement)
					.ValueGeneratedOnAdd();

				opt.HasOne(m => m.Campaign)
                    .WithMany(t => t.Banners)
                    .HasForeignKey(m => m.IdCampaign)
					
                   ;
				opt.Property(e => e.Price).HasColumnType("decimal(6,2)");
				opt.Property(e => e.Area).HasColumnType("decimal(6,2)");

			});

			modelBuilder.Entity<Client>(opt => 
			{
				opt.HasKey(c => c.IdClient);
				opt.Property(p => p.IdClient)
					.ValueGeneratedOnAdd();

				opt.Property(e => e.FirstName)
					.HasMaxLength(100)
					.IsRequired();
                   ;

				opt.Property(e => e.LastName)
					.HasMaxLength(100)
					.IsRequired();
                   ;

				opt.Property(e => e.Email)
					.HasMaxLength(100)
					.IsRequired();
                   ;

				opt.Property(e => e.Phone)
					.HasMaxLength(100)
					.IsRequired();
                   ;

				opt.Property(e => e.Login)
					.HasMaxLength(100)
					.IsRequired();
                   ;

				opt.Property(e => e.Password)
					.IsRequired();
                   ;

			});

		}

	}
}
