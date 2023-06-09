using EFW_Project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFW_Project
{
    public class Program
    {
        static void Main(string[] args)
        {
            MovieRentalContext db = new MovieRentalContext();
            db.Database.Migrate();

            #region add3Producers
            //db.Producers.Add(new Producer() { CompanyName = "Netflix", Country = "usa" });
            //db.Producers.Add(new Producer() { CompanyName = "HBO", Country = "England" });
            //db.Producers.Add(new Producer() { CompanyName = "AppleTV", Country = "Argentine" });

            //db.SaveChanges();
            #endregion

            #region add7Movies
            //db.Movies.Add(new Movie()
            //{
            //    Title = "The Dark Knight",
            //    Duration = 152,
            //    Rating = "9",
            //    ProducerID = 1
            //});
            //db.Movies.Add(new Movie()
            //{
            //    Title = "The Shawshank Redemption",
            //    Duration = 142,
            //    Rating = "8",
            //    ProducerID = 1
            //});
            //db.Movies.Add(new Movie()
            //{
            //    Title = "The Godfather",
            //    Duration = 175,
            //    Rating = "7",
            //    ProducerID = 3
            //});
            //db.Movies.Add(new Movie()
            //{
            //    Title = "Pulp Fiction",
            //    Duration = 154,
            //    Rating = "9",
            //    ProducerID = 2
            //});
            //db.Movies.Add(new Movie()
            //{
            //    Title = "Fight Club",
            //    Duration = 139,
            //    Rating = "9",
            //    ProducerID = 1
            //});
            //db.Movies.Add(new Movie()
            //{
            //    Title = "Inception",
            //    Duration = 148,
            //    Rating = "9",
            //    ProducerID = 3
            //});
            //db.Movies.Add(new Movie()
            //{
            //    Title = "The Matrix",
            //    Duration = 152,
            //    Rating = "6",
            //    ProducerID = 1
            //});


            //db.SaveChanges();
            #endregion

            #region add3Customers
            //db.Customers.Add(new Customer() { FirstName = "Adel", LastName = "Makram", Address = "Cairo", BirthDate = new DateTime(1998, 9, 30), PhoneNumber = 01275470585 });
            //db.Customers.Add(new Customer() { FirstName = "Osama", LastName = "Mohamed", Address = "Ismailia", BirthDate = new DateTime(1997, 6, 22), PhoneNumber = 01234567890 });
            //db.Customers.Add(new Customer() { FirstName = "Eslam", LastName = "Ahmed", Address = "Alex", BirthDate = new DateTime(2000, 12, 5), PhoneNumber = 01134567287 });

            //db.SaveChanges();
            #endregion

            #region add10Rentals
            //db.Customer_Movies.Add(new Customer_Movie() { CustomerId = 1, MovieID = 2, DateRented = new DateTime(2023, 6, 9), DueDate = new DateTime(2023, 6, 16) });
            //db.Customer_Movies.Add(new Customer_Movie() { CustomerId = 1, MovieID = 5, DateRented = new DateTime(2023, 6, 1), DueDate = new DateTime(2023, 6, 8) });
            //db.Customer_Movies.Add(new Customer_Movie() { CustomerId = 1, MovieID = 6, DateRented = new DateTime(2023, 6, 8), DueDate = new DateTime(2023, 6, 17) });
            //db.Customer_Movies.Add(new Customer_Movie() { CustomerId = 1, MovieID = 7, DateRented = new DateTime(2023, 6, 9), DueDate = new DateTime(2023, 6, 20) });
            //db.Customer_Movies.Add(new Customer_Movie() { CustomerId = 2, MovieID = 1, DateRented = new DateTime(2023, 5, 30), DueDate = new DateTime(2023, 6, 1) });
            //db.Customer_Movies.Add(new Customer_Movie() { CustomerId = 2, MovieID = 4, DateRented = new DateTime(2023, 5, 28), DueDate = new DateTime(2023, 6, 20) });
            //db.Customer_Movies.Add(new Customer_Movie() { CustomerId = 2, MovieID = 5, DateRented = new DateTime(2023, 6, 1), DueDate = new DateTime(2023, 6, 5) });
            //db.Customer_Movies.Add(new Customer_Movie() { CustomerId = 3, MovieID = 7, DateRented = new DateTime(2023, 5, 27), DueDate = new DateTime(2023, 6, 11) });
            //db.Customer_Movies.Add(new Customer_Movie() { CustomerId = 3, MovieID = 2, DateRented = new DateTime(2023, 6, 4), DueDate = new DateTime(2023, 6, 30) });
            //db.Customer_Movies.Add(new Customer_Movie() { CustomerId = 3, MovieID = 6, DateRented = new DateTime(2023, 6, 9), DueDate = new DateTime(2023, 6, 25) });

            //db.SaveChanges();
            #endregion

            #region Queries
            #region Top 3 Rented Movie names 
            var q1 = db.Customer_Movies
                .GroupBy(x => x.Movie)
                .OrderByDescending(x => x.Count())
                .Take(3)
                .Select(x => x.Key.Title);


            foreach (var m in q1)
            {
                Console.WriteLine(m);
            }
            Console.WriteLine($"=========================================");
            Console.WriteLine($"=========================================");

            #endregion

            #region Producer With Most Movies and Movie Counts 
            var q2 = db.Producers
                .Select(p => new
                {
                    ProducerName = p.CompanyName,
                    MovieCount = p.ProducerMovies.Count()
                })
                .OrderByDescending(x => x.MovieCount)
                .FirstOrDefault();

            Console.WriteLine($"Name: {q2.ProducerName}, Movie count: {q2.MovieCount}");

            Console.WriteLine($"=========================================");
            Console.WriteLine($"=========================================");
            #endregion

            #region Customers ordered by there Rental Count 

            var q3 = db.Customers
            .Select(c => new
            {
                CustomerName = c.FirstName,
                RentalCount = c.CustomerMovies.Count()
            })
            .OrderByDescending(c => c.RentalCount);

            foreach (var customer in q3)
            {
                Console.WriteLine($"Name: {customer.CustomerName}, Rental count: {customer.RentalCount}");

            }

            Console.WriteLine($"=========================================");
            Console.WriteLine($"=========================================");
            #endregion

            #region  Full informations [Rentals] 
            var q4 = db.Customer_Movies
                .Select(x => new
                {
                    CustomerName = x.Customer.FirstName,
                    MovieName = x.Movie.Title,
                    ProducerName = x.Movie.Producer.CompanyName,
                    RentDate = x.DateRented,
                    RemainingDays = (DateTime.Now - x.DueDate)
                });

            foreach (var rental in q4)
            {
                Console.WriteLine($"Customer name: {rental.CustomerName}");
                Console.WriteLine($"Movie name: {rental.MovieName}");
                Console.WriteLine($"Producer name: {rental.ProducerName}");
                Console.WriteLine($"Rent date: {rental.RentDate}");
                Console.WriteLine($"Overdue remaining days: {rental.RemainingDays}");
                Console.WriteLine($"=========================================");
            }

            Console.WriteLine($"=========================================");
            Console.WriteLine($"=========================================");

            #endregion

            #region Rent Date sorted bu the oldest Due Date 

            var q5 = db.Customer_Movies
                .Where(r => DateTime.Now > r.DueDate)
                .OrderBy(r => r.DueDate);

            foreach (var rental in q5)
            {
                Console.WriteLine($"Customer Name {rental.Customer.FirstName}");
                Console.WriteLine($"Movie name: {rental.Movie.Title}");
                Console.WriteLine($"Rent date: {rental.DateRented}");
                Console.WriteLine($"Due date: {rental.DueDate}");

            }

            Console.WriteLine($"=========================================");
            Console.WriteLine($"=========================================");
            #endregion
            #endregion
        }
    }
}
