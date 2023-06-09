using EFW_Project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFW_Project
{
	public class MovieRentalContext : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=.;DataBase=MovieRentals;Trusted_Connection=True;Encrypt=False");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			#region Customer

			modelBuilder.Entity<Customer>()
				.HasKey(c => c.Id);

			modelBuilder.Entity<Customer>()
				.Property(c => c.Id)
				.UseIdentityColumn()
				.HasMaxLength(10);

			modelBuilder.Entity<Customer>()
				.Property(c => c.FirstName)
				.IsRequired()
				.HasMaxLength(255);

			modelBuilder.Entity<Customer>()
				.Property(c => c.LastName)
				.HasMaxLength(255);

			modelBuilder.Entity<Customer>()
				.Property(c => c.Address)
				.HasMaxLength(255);

			modelBuilder.Entity<Customer>()
				.Property(c => c.PhoneNumber)
				.HasMaxLength(10);

			#endregion
			#region CustomerMovie
			modelBuilder.Entity<Customer_Movie>()
				.HasKey(x => new { x.CustomerId, x.MovieID });

			modelBuilder.Entity<Customer_Movie>()
				.HasOne(x => x.Customer)
				.WithMany(x => x.CustomerMovies)
				.HasForeignKey(x => x.CustomerId);

			modelBuilder.Entity<Customer_Movie>()
				.HasOne(x => x.Movie)
				.WithMany(x => x.MovieCustomers)
				.HasForeignKey(x => x.MovieID);

			modelBuilder.Entity<Customer_Movie>()
				.Property(x => x.DateRented)
				.IsRequired();

			modelBuilder.Entity<Customer_Movie>()
				.Property(x => x.DueDate);
			#endregion
			#region Movie

			modelBuilder.Entity<Movie>()
				.HasKey(m => m.Id);

			modelBuilder.Entity<Movie>()
				.Property(m => m.Id)
				.UseIdentityColumn();

			modelBuilder.Entity<Movie>()
				.HasOne(m => m.Producer)
				.WithMany(m => m.ProducerMovies)
				.HasForeignKey(m => m.ProducerID);

			modelBuilder.Entity<Movie>()
				.Property(m => m.Title)
				.IsRequired()
				.HasMaxLength(255);

			modelBuilder.Entity<Movie>()
				.Property(m => m.Duration)
				.HasMaxLength(11);

			modelBuilder.Entity<Movie>()
				.Property(m => m.Rating)
				.HasMaxLength(1);

			#endregion
			#region Producer

			modelBuilder.Entity<Producer>()
				.HasKey(p => p.Id);

			modelBuilder.Entity<Movie>()
				.Property(m => m.Id)
				.UseIdentityColumn();

			modelBuilder.Entity<Producer>()
				.Property(p => p.CompanyName)
				.IsRequired()
				.HasMaxLength(255);

			modelBuilder.Entity<Producer>()
				.Property(p => p.Country)
				.HasMaxLength(255);

			#endregion
			#region MovieFeedback
			modelBuilder.Entity<MovieFeedback>()
				.HasKey(f => new { f.CustomerId, f.MovieID });
			modelBuilder.Entity<MovieFeedback>()
				.HasOne(f => f.Customer)
				.WithMany()
				.HasForeignKey(f => f.CustomerId);
			modelBuilder.Entity<MovieFeedback>()
				.HasOne(f => f.Movie)
				.WithMany()
				.HasForeignKey(f => f.MovieID);
			#endregion
		}


        public DbSet<Customer> Customers { get; set; }
		public DbSet<Customer_Movie> Customer_Movies { get; set; }
		public DbSet<Movie> Movies { get; set; }
		public DbSet<Producer> Producers { get; set; }
	}
}
