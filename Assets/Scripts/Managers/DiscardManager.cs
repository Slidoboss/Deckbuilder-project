using System;
using System.Collections;
using System.Collections.Generic;
using SoloudoFiles;
using TMPro;
using UnityEngine;

public class DiscardManager : MonoBehaviour
{
    [SerializeField] private List<Card> _discardCards = new List<Card>();
    public TextMeshProUGUI discardCount;
    public int discardCardsCount;

    private void Awake()
    {
        UpdateDiscardCount();
    }

    private void UpdateDiscardCount()
    {
        discardCount.text = _discardCards.Count.ToString();
        discardCardsCount = _discardCards.Count;
    }
    public void AddToDiscard(Card card)
    {
        if (card != null)
        {
            _discardCards.Add(card);
            UpdateDiscardCount();
        }
    }
    public Card PullFromDiscard()
    {
        if(_discardCards.Count > 0)
        {
            Card cardToReturn = _discardCards[_discardCards.Count - 1];
            _discardCards.RemoveAt(_discardCards.Count - 1);
            UpdateDiscardCount();
            return cardToReturn;
        }
        else
        {
            return null;
        }
    }
    public bool PullSelectedCardFromDiscard(Card card)
    {
        if(_discardCards.Count > 0 && _discardCards.Contains(card))
        {
            _discardCards.Remove(card);
            UpdateDiscardCount();
            return true;
        }
        else
        {
            return false;
        }
    }
    public List<Card> PullAllFromDiscard()
    {
        if(_discardCards.Count>0)
        {
            List<Card> cardsToReturn = new List<Card>(_discardCards);
            _discardCards.Clear();
            UpdateDiscardCount();
            return cardsToReturn;
        }
        else return new List<Card>();
    }
}
