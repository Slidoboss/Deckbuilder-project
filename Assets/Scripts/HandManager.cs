using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoloudoFiles;
using System;

public class HandManager : MonoBehaviour
{
    public DeckManager deckManager;
    public GameObject cardPrefab;
    public Transform handTranform; //To get hand position on Screen
    public float fanSpread = -8f; //determines how far apart the card with be in hand.
    public float cardHorizontalSpacing = 150f;
    public float cardVerticalSpacing = 80f;
    public List<GameObject> cardInHand = new List<GameObject>(); //Hold a list of the card objects in hand.

    void Start()
    {
        
    }

    void Update()
    {
        UpdateHandVisuals();
    }

    public void AddCardToHand(Card cardData)
    {
        //Create Card Object and Add to hand.
        GameObject newCard = Instantiate(cardPrefab, handTranform.position, Quaternion.identity, handTranform);
        cardInHand.Add(newCard);
        
        //Setup the data of the card object created.
        newCard.GetComponent<CardDisplay>().cardData = cardData;
     
        UpdateHandVisuals();
    }

    private void UpdateHandVisuals()
    {
        int cardCount = cardInHand.Count;
        if (cardCount == 1)
        {
            cardInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }

        for (int i = 0; i < cardCount; i++)
        {
            float rotationAngle = fanSpread * (i - (cardCount - 1) / 2f); // Gives even rotation for each side
            cardInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float horizontalOffset = cardHorizontalSpacing * (i - (cardCount - 1) / 2f);

            float normalizedPosition = (2f * i / (cardCount - 1)) - 1f; // Gives even results between -1 & 1.
            float verticalOffset = cardVerticalSpacing * (1 - normalizedPosition * normalizedPosition);

            cardInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
        }
    }
}
