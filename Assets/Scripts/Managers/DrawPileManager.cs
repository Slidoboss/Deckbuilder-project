using System.Collections;
using System.Collections.Generic;
using SoloudoFiles;
using TMPro;
using UnityEngine;

public class DrawPileManager : MonoBehaviour
{
    public List<Card> drawPile = new List<Card>();
    private int _currentIndex = 0;
    public int startingHandSize = 6;
    public int maxHandSize;
    public int currentHandSize;
    private HandManager _handManager;
    private DiscardManager _discardManager;
    public TextMeshProUGUI drawPileCounter;

    void Start()
    {
        _handManager = FindObjectOfType<HandManager>();
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
        if (drawPile.Count == 0) return;
        if (currentHandSize < maxHandSize)
        {
            Card nextCard = drawPile[_currentIndex];
            handManager.AddCardToHand(nextCard);
            _currentIndex = (_currentIndex + 1) % drawPile.Count; //Code for looping.
        }
    }
}
