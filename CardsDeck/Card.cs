namespace CardsDeck;

public struct Card : IComparable<Card>
{
    public Card(Rank rank, Suit suit)
    {
        Rank = rank;
        Suit = suit;
    }

    public Rank Rank { get; init; }
    
    public Suit Suit { get; init; }

    public int CompareTo(Card other) => Rank.CompareTo(other.Rank);
}