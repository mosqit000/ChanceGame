namespace ChanceGame.CustomExceptions;

public class InsufficientBalanceException : Exception
{
    public long CurrentBalance { get; }

    public InsufficientBalanceException(long currentBalance)
        : base($"Insufficient balance. Current balance is {currentBalance}.")
    {
        CurrentBalance = currentBalance;
    }
}