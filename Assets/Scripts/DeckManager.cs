using System.Collections;
using System.Collections.Generic;
using SoloudoFiles;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();
    private int _currentIndex = 0;

    void Start()
    {
        //Load all card assets from resources folder.
        Card[] cards = Resources.LoadAll<Card>("CardData");

        //Adds all loaded cards to allcards list.
        allCards.AddRange(cards);
    }
    public void DrawCard(HandManager handManager)
    {
        if (allCards.Count == 0) return;

        Card nextCard = allCards[_currentIndex];
        handManager.AddCardToHand(nextCard);
        _currentIndex = (_currentIndex + 1) % allCards.Count;
    }
}
