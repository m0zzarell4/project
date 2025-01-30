namespace project
{
    public class BorrowTransaction
    {
        public int TransactionId { get; set; }
        public int BookId { get; set; }
        public int ReaderId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public double LateFee { get; set; }
    }
}
