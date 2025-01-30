using project;
using System;

class LibraryApp
{
    static void Main(string[] args)
    {
        LibraryController controller = new LibraryController();

        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine(
                "Select an option: \n1 - Add a book \n2 - Show all books \n3 - Add a reader \n4 - Show all readers \n" +
                "5 - Borrow a book \n6 - Return a book \n7 - Show reader's borrowings \n8 - Remove a book \n9 - Remove a reader \n0 - Exit"
            );

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": AddBook(controller); break;
                case "2": ShowAllBooks(controller); break;
                case "3": AddPatron(controller); break;
                case "4": ShowAllPatrons(controller); break;
                case "5": BorrowBook(controller); break;
                case "6": ReturnBook(controller); break;
                case "7": ShowPatronBorrowings(controller); break;
                case "8": RemoveBook(controller); break;
                case "9": RemovePatron(controller); break;
                case "0": isRunning = false; Console.WriteLine("Exiting program."); break;
                default: Console.WriteLine("Invalid input"); break;
            }
        }
    }

    // AddBook, ShowAllBooks, AddPatron, ShowAllPatrons, BorrowBook, ReturnBook, etc. follow same pattern
}
