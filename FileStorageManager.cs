using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace project
{
    public class FileStorageManager
    {
        private const string BOOKS_FILE = "Books.txt";
        private const string READERS_FILE = "Readers.txt";
        private const string LOANS_FILE = "Loans.txt";
        private static char SEP = ';';

        // Helper method for loading data from file
        private static List<T> LoadFromFile<T>(string filePath, Func<string[], T> createItem)
        {
            List<T> items = new List<T>();

            if (!File.Exists(filePath)) return items;

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(SEP);
                if (parts.Length >= 1)
                {
                    items.Add(createItem(parts));
                }
            }
            return items;
        }

        // KSIĄŻKI
        public static List<Book> LoadBooks()
        {
            return LoadFromFile(BOOKS_FILE, parts =>
            {
                return new Book
                {
                    Id = int.Parse(parts[0]),
                    Title = parts[1],
                    Author = parts[2],
                    PublicationYear = int.Parse(parts[3]),
                    IsAvailable = bool.Parse(parts[4])
                };
            });
        }

        public static void SaveBooks(List<Book> books)
        {
            SaveToFile(BOOKS_FILE, books, b => $"{b.Id}{SEP}{b.Title}{SEP}{b.Author}{SEP}{b.PublicationYear}{SEP}{b.IsAvailable}");
        }

        // CZYTELNICY
        public static List<Reader> LoadReaders()
        {
            return LoadFromFile(READERS_FILE, parts =>
            {
                return new Reader
                {
                    Id = int.Parse(parts[0]),
                    FName = parts[1],
                    LName = parts[2]
                };
            });
        }

        public static void SaveReaders(List<Reader> readers)
        {
            SaveToFile(READERS_FILE, readers, r => $"{r.Id}{SEP}{r.FName}{SEP}{r.LName}");
        }

        // POŻYCZKI
        public static List<Loan> LoadLoans()
        {
            return LoadFromFile(LOANS_FILE, parts =>
            {
                return new Loan
                {
                    LoanId = int.Parse(parts[0]),
                    BookId = int.Parse(parts[1]),
                    ReaderId = int.Parse(parts[2]),
                    BorrowDate = DateTime.Parse(parts[3]),
                    DueDate = DateTime.Parse(parts[4]),
                    ReturnDate = string.IsNullOrEmpty(parts[5]) ? (DateTime?)null : DateTime.Parse(parts[5]),
                    Penalty = double.Parse(parts[6])
                };
            });
        }

        public static void SaveLoans(List<Loan> loans)
        {
            SaveToFile(LOANS_FILE, loans, ln => $"{ln.LoanId}{SEP}{ln.BookId}{SEP}{ln.ReaderId}{SEP}{ln.BorrowDate}{SEP}{ln.DueDate}{SEP}{(ln.ReturnDate?.ToString() ?? "")}{SEP}{ln.Penalty}");
        }

        // Helper method for saving data to a file
        private static void SaveToFile<T>(string filePath, List<T> items, Func<T, string> formatItem)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                foreach (var item in items)
                {
                    sw.WriteLine(formatItem(item));
                }
            }
        }
    }
}
