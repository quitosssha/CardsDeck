namespace CardsDeck;

public class Card : IComparable<Card>
{
    public Card(Rank rank, Suit suit)
    {
        Rank = rank;
        Suit = suit;
    }

    public Rank Rank { get; init; }
    
    public Suit Suit { get; init; }

    public int CompareTo(Card other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (other is null) return 1;
        return Rank.CompareTo(other.Rank);
    }
}