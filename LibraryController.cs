namespace project
{
    public class LibraryController
    {
        private List<PublishedBook> _books;
        private List<Patron> _patrons;
        private List<BorrowTransaction> _borrowTransactions;

        private const double PenaltyRate = 2.5;

        public LibraryController()
        {
            _books = DataHandler.LoadBooks();
            _patrons = DataHandler.LoadReaders();
            _borrowTransactions = DataHandler.LoadBorrowings();
        }

        public int GetNextBookId()
        {
            return _books.Count == 0 ? 1 : _books.Max(b => b.Id) + 1;
        }

        public int GetNextPatronId()
        {
            return _patrons.Count == 0 ? 1 : _patrons.Max(p => p.Id) + 1;
        }

        public int GetNextTransactionId()
        {
            return _borrowTransactions.Count == 0 ? 1 : _borrowTransactions.Max(t => t.TransactionId) + 1;
        }

        // Books CRUD
        public void AddBook(PublishedBook book)
        {
            _books.Add(book);
            DataHandler.SaveBooks(_books);
        }

        public bool RemoveBook(int bookId)
        {
            var book = _books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                _books.Remove(book);
                DataHandler.SaveBooks(_books);
                return true;
            }
            return false;
        }

        public List<PublishedBook> GetAllBooks()
        {
            return _books;
        }

        public PublishedBook GetBookById(int bookId)
        {
            return _books.FirstOrDefault(b => b.Id == bookId);
        }

        // Patrons CRUD
        public void AddPatron(Patron patron)
        {
            _patrons.Add(patron);
            DataHandler.SaveReaders(_patrons);
        }

        public bool RemovePatron(int patronId)
        {
            var patron = _patrons.FirstOrDefault(p => p.Id == patronId);
            if (patron != null)
            {
                _patrons.Remove(patron);
                DataHandler.SaveReaders(_patrons);
                return true;
            }
            return false;
        }

        public List<Patron> GetAllPatrons()
        {
            return _patrons;
        }

        public Patron GetPatronById(int patronId)
        {
            return _patrons.FirstOrDefault(p => p.Id == patronId);
        }

        // Borrowing and Returning Books
        public bool BorrowBook(int patronId, int bookId, int days)
        {
            var patron = GetPatronById(patronId);
            var book = GetBookById(bookId);

            if (patron == null || book == null || !book.IsAvailable)
            {
                return false;
            }

            BorrowTransaction newTransaction = new BorrowTransaction
            {
                TransactionId = GetNextTransactionId(),
                BookId = bookId,
                ReaderId = patronId,
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(days),
                ReturnDate = null,
                LateFee = 0
            };

            _borrowTransactions.Add(newTransaction);
            book.IsAvailable = false;
            DataHandler.SaveBooks(_books);
            DataHandler.SaveBorrowings(_borrowTransactions);

            return true;
        }

        public double ReturnBook(int transactionId)
        {
            var transaction = _borrowTransactions.FirstOrDefault(t => t.TransactionId == transactionId);
            if (transaction == null)
            {
                return -1;
            }

            transaction.ReturnDate = DateTime.Now;

            if (transaction.ReturnDate > transaction.DueDate)
            {
                int overdueDays = (transaction.ReturnDate.Value - transaction.DueDate).Days;
                transaction.LateFee = overdueDays * PenaltyRate;
            }

            var book = _books.FirstOrDefault(b => b.Id == transaction.BookId);
            if (book != null)
            {
                book.IsAvailable = true;
            }

            DataHandler.SaveBooks(_books);
            DataHandler.SaveBorrowings(_borrowTransactions);

            return transaction.LateFee;
        }

        public List<BorrowTransaction> GetPatronBorrowings(int patronId)
        {
            return _borrowTransactions.Where(t => t.ReaderId == patronId).ToList();
        }
    }
}
