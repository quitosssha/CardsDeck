namespace CardsDeck;

public class Deck
{
    private Stack<Card> cards;

    #region Initialization

    public Deck() => cards = [];
    
    public Deck(IEnumerable<Card> cards) => this.cards = new Stack<Card>(cards);
    
    public static Deck New(bool shuffled = true)
    {
        var deck = new Deck(GetOrderedCards());
        if (shuffled) deck.Shuffle();
        return deck;
    }

    private static IEnumerable<Card> GetOrderedCards()
    {
        foreach (var suit in Enum.GetValues<Suit>())
        foreach (var rank in Enum.GetValues<Rank>())
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
        card = null;
        lock (cards)
        {
            if (cards.Count == 0)
                return false;
            card = cards.Pop();
        }
        return true;
    }
    
    #endregion
}