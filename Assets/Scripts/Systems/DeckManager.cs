using System.Collections;
using System.Collections.Generic;
using SoloudoFiles;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();
    private int _currentIndex = 0;
    public int startingHandSize = 6;
    public int maxHandSize;
    public int currentHandSize;
    private HandManager _handManager;

    void Start()
    {
        //Load all card assets from resources folder.
        Card[] cards = Resources.LoadAll<Card>("CardData");

        //Adds all loaded cards to allcards list.
        allCards.AddRange(cards);

        _handManager = FindObjectOfType<HandManager>();
        maxHandSize = _handManager.maxHandSize;
        for (int i = 0; i < startingHandSize; i++)
        {
            DrawCard(_handManager);
        }
    }
    void Update()
    {
        if (_handManager != null)
        {
            currentHandSize = _handManager.cardsInHand.Count;
        }
    }
    public void DrawCard(HandManager handManager)
    {
        if (allCards.Count == 0) return;
        if (currentHandSize < maxHandSize)
        {
            Card nextCard = allCards[_currentIndex];
            handManager.AddCardToHand(nextCard);
            _currentIndex = (_currentIndex + 1) % allCards.Count;
        }
    }
}
