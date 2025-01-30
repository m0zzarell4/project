using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace project
{
    public class StorageHandler
    {
        private static readonly string BOOKS_DATA = "Books.txt";
        private static readonly string READERS_DATA = "Readers.txt";
        private static readonly string LOANS_DATA = "Loans.txt";
        private static readonly char SEPARATOR = ';';

       
        public static List<Book> LoadBooksFromFile()
        {
            List<Book> booksList = new List<Book>();

            if (!File.Exists(BOOKS_DATA))
            {
                return booksList;
            }

            var fileLines = File.ReadAllLines(BOOKS_DATA);
            foreach (var line in fileLines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(SEPARATOR);
                if (parts.Length >= 5)
                {
                    Book book = new Book
                    {
                        Id = int.Parse(parts[0]),
                        Title = parts[1],
                        Author = parts[2],
                        PublicationYear = int.Parse(parts[3]),
                        IsAvailable = bool.Parse(parts[4])
                    };
                    booksList.Add(book);
                }
            }

            return booksList;
        }

        public static void SaveBooksToFile(List<Book> books)
        {
            using (StreamWriter writer = new StreamWriter(BOOKS_DATA, false))
            {
                foreach (var book in books)
                {
                    writer.WriteLine($"{book.Id}{SEPARATOR}{book.Title}{SEPARATOR}{book.Author}{SEPARATOR}{book.PublicationYear}{SEPARATOR}{book.IsAvailable}");
                }
            }
        }

   
        public static List<Reader> LoadReadersFromFile()
        {
            List<Reader> readersList = new List<Reader>();

            if (!File.Exists(READERS_DATA))
            {
                return readersList;
            }

            var fileLines = File.ReadAllLines(READERS_DATA);
            foreach (var line in fileLines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(SEPARATOR);
                if (parts.Length >= 3)
                {
                    Reader reader = new Reader
                    {
                        Id = int.Parse(parts[0]),
                        FName = parts[1],
                        LName = parts[2]
                    };
                    readersList.Add(reader);
                }
            }

            return readersList;
        }

        public static void SaveReadersToFile(List<Reader> readers)
        {
            using (StreamWriter writer = new StreamWriter(READERS_DATA, false))
            {
                foreach (var reader in readers)
                {
                    writer.WriteLine($"{reader.Id}{SEPARATOR}{reader.FName}{SEPARATOR}{reader.LName}");
                }
            }
        }

    
        public static List<Loan> LoadLoansFromFile()
        {
            List<Loan> loansList = new List<Loan>();

            if (!File.Exists(LOANS_DATA))
            {
                return loansList;
            }

            var fileLines = File.ReadAllLines(LOANS_DATA);
            foreach (var line in fileLines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(SEPARATOR);
                if (parts.Length >= 7)
                {
                    Loan loan = new Loan
                    {
                        LoanId = int.Parse(parts[0]),
                        BookId = int.Parse(parts[1]),
                        ReaderId = int.Parse(parts[2]),
                        BorrowDate = DateTime.Parse(parts[3]),
                        DueDate = DateTime.Parse(parts[4]),
                        ReturnDate = string.IsNullOrEmpty(parts[5]) ? (DateTime?)null : DateTime.Parse(parts[5]),
                        Penalty = double.Parse(parts[6])
                    };
                    loansList.Add(loan);
                }
            }

            return loansList;
        }

        public static void SaveLoansToFile(List<Loan> loans)
        {
            using (StreamWriter writer = new StreamWriter(LOANS_DATA, false))
            {
                foreach (var loan in loans)
                {
                    string returnDate = loan.ReturnDate.HasValue ? loan.ReturnDate.Value.ToString() : "";
                    writer.WriteLine($"{loan.LoanId}{SEPARATOR}{loan.BookId}{SEPARATOR}{loan.ReaderId}{SEPARATOR}{loan.BorrowDate}{SEPARATOR}{loan.DueDate}{SEPARATOR}{returnDate}{SEPARATOR}{loan.Penalty}");
                }
            }
        }
    }
}
