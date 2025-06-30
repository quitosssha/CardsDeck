namespace CardsDeck;

/// <summary>
/// Represents a thread-safe deck of playing cards 
/// </summary>
public class Deck
{
    private Stack<Card> cards;

    #region Initialization

    /// <summary>
    /// Initializes empty deck
    /// </summary>
    public Deck() => cards = [];

    /// <summary>
    /// Initializes deck with specified cards
    /// </summary>
    public Deck(IEnumerable<Card> cards) => this.cards = new Stack<Card>(cards);

    /// <summary>
    /// Initializes classic deck.
    /// </summary>
    /// <param name="shuffled">Specifies whether the cards should be shuffled.
    /// If false, deck will contain cards in A,K,Q... of spades -> A,K,Q... of hearts -> ...diamonds -> ...clubs</param>
    /// <param name="shortDeck">Specifies whether the deck contains cards 2-5</param>
    public static Deck NewDeck(bool shuffled = true, bool shortDeck = false)
    {
        var deck = new Deck(GetOrderedCards(shortDeck));
        if (shuffled) deck.Shuffle();
        return deck;
    }

    private static IEnumerable<Card> GetOrderedCards(bool shortDeck)
    {
        foreach (var suit in Enum.GetValues<Suit>())
        foreach (var rank in Enum.GetValues<Rank>().Reverse())
            if (!shortDeck || (int)rank >= 6)
                yield return new Card(rank, suit);
    }

    #endregion

    #region Deck operations

    public void Shuffle(Random random = null)
    {
        random ??= new Random();
        lock (cards)
        {
            var newCards = new Stack<Card>();
            var remainingCards = cards.ToList();
            for (var i = remainingCards.Count - 1; i > 0; i--)
            {
                var pos = random.Next(i);
                newCards.Push(remainingCards[pos]);
                remainingCards.RemoveAt(pos);
            }

            cards = newCards;
        }
    }

    /// <summary>
    /// Draws top card from the deck
    /// </summary>
    /// <exception cref="InvalidOperationException">Deck contains no cards</exception>
    public Card Draw()
    {
        lock (cards)
        {
            if (cards.Count == 0)
                throw new InvalidOperationException("Deck is empty");
            return cards.Pop();
        }
    }

    public bool TryDraw(out Card card)
    {
        lock (cards)
        {
            return cards.TryPop(out card);
        }
    }

    #endregion
}