using System;
using System.Collections.Generic;
using System.IO;

namespace LibrarySystem
{
    public class StorageHandler
    {
        private const string BOOKS_FILE = "LibraryBooks.txt";
        private const string READERS_FILE = "LibraryReaders.txt";
        private const string LOANS_FILE = "LibraryLoans.txt";
        private static char DELIMITER = '|';

        public static List<Book> LoadBooks()
        {
            List<Book> books = new List<Book>();
            if (!File.Exists(BOOKS_FILE)) return books;

            foreach (var line in File.ReadLines(BOOKS_FILE))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var data = line.Split(DELIMITER);
                if (data.Length >= 5)
                {
                    books.Add(new Book
                    {
                        Id = int.Parse(data[0]),
                        Title = data[1],
                        Author = data[2],
                        PublicationYear = int.Parse(data[3]),
                        IsAvailable = bool.Parse(data[4])
                    });
                }
            }
            return books;
        }

        public static void SaveBooks(List<Book> books)
        {
            using (StreamWriter writer = new StreamWriter(BOOKS_FILE, false))
            {
                foreach (var book in books)
                {
                    writer.WriteLine($"{book.Id}{DELIMITER}{book.Title}{DELIMITER}{book.Author}{DELIMITER}{book.PublicationYear}{DELIMITER}{book.IsAvailable}");
                }
            }
        }

        public static List<Reader> LoadReaders()
        {
            List<Reader> readers = new List<Reader>();
            if (!File.Exists(READERS_FILE)) return readers;

            foreach (var line in File.ReadLines(READERS_FILE))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var data = line.Split(DELIMITER);
                if (data.Length >= 3)
                {
                    readers.Add(new Reader
                    {
                        Id = int.Parse(data[0]),
                        FirstName = data[1],
                        LastName = data[2]
                    });
                }
            }
            return readers;
        }

        public static void SaveReaders(List<Reader> readers)
        {
            using (StreamWriter writer = new StreamWriter(READERS_FILE, false))
            {
                foreach (var reader in readers)
                {
                    writer.WriteLine($"{reader.Id}{DELIMITER}{reader.FirstName}{DELIMITER}{reader.LastName}");
                }
            }
        }

        public static List<Loan> LoadLoans()
        {
            List<Loan> loans = new List<Loan>();
            if (!File.Exists(LOANS_FILE)) return loans;

            foreach (var line in File.ReadLines(LOANS_FILE))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var data = line.Split(DELIMITER);
                if (data.Length >= 7)
                {
                    loans.Add(new Loan
                    {
                        LoanId = int.Parse(data[0]),
                        BookId = int.Parse(data[1]),
                        ReaderId = int.Parse(data[2]),
                        BorrowDate = DateTime.Parse(data[3]),
                        DueDate = DateTime.Parse(data[4]),
                        ReturnDate = string.IsNullOrEmpty(data[5]) ? (DateTime?)null : DateTime.Parse(data[5]),
                        Penalty = double.Parse(data[6])
                    });
                }
            }
            return loans;
        }

        public static void SaveLoans(List<Loan> loans)
        {
            using (StreamWriter writer = new StreamWriter(LOANS_FILE, false))
            {
                foreach (var loan in loans)
                {
                    string returnDate = loan.ReturnDate.HasValue ? loan.ReturnDate.Value.ToString() : "";
                    writer.WriteLine($"{loan.LoanId}{DELIMITER}{loan.BookId}{DELIMITER}{loan.ReaderId}{DELIMITER}{loan.BorrowDate}{DELIMITER}{loan.DueDate}{DELIMITER}{returnDate}{DELIMITER}{loan.Penalty}");
                }
            }
        }
    }
}
