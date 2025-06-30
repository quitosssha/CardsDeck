Representation of card deck.
### Cards
There are two enums:
```aiignore
public enum Suit
{
    Spades,
    Hearts,
    Diamonds,
    Clubs
}

public enum Rank
{
    Two = 2,
    Three,
    ...
    King,
    Ace
}
```
That specifies playing cards.

### Initialization
Best way to create classic deck:
```aiignore
var deck1 = Deck.NewDeck();
```
You can also customize deck length (52 or 36 cards) and shuffle.
The same deck you can get with:
```aiignore
var deck2 = Deck.NewDeck(shuffled = true, shortDeck = false);
```
Or you can create a custom deck, if you need:
```aiignore
IEnumerable<Card> cards = GetCustomCards(); // your cards
var deck = new Deck(cards);
```
### Operations
Pop top card from the deck:
```aiignore
var card = deck.Draw();
```
or pop it safe (if you invoke `Draw()` at an empty deck, `OperationException` will be thrown)
```aiignore
var canPop = deck.TryDraw(out var card);
```
```aiignore
if (deck.TryDraw(out var card))
{
    your logic
}
```

Shuffle deck (only those cards that were in the deck at that moment will remain)
No cards will appear/disappear.

```aiignore
deck.Shuffle();
```
```aiignore
Random random = GetCustomRandom(); // your random
deck.Shuffle(random);
```