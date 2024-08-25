using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SoloudoFiles;

public class CardDisplay : MonoBehaviour
{
    public Card cardData;
    public Image cardImage;
    public Image damageImage;
    public TMP_Text nameText;
    public TMP_Text healthText;
    public TMP_Text damageText;
    public Image[] typeImages;
    public Image displayImage;

    private Color[] _cardColors =
    {
        new Color(0.57f,0.02f,0.02f), //Fire
        new Color(0.61f, 0.39f, 0.17f), //Earth
        new Color(0.02f,0.44f,0.58f),//Water
        new Color(0.23f,0.06f,0.21f), //Dark
        new Color(0.76f,0.54f,0.02f), //Light
        new Color(0f,0.68f,0.77f,1f), //Air
    };

    private Color[] _typeColors =
    {
        Color.red, //Fire
        new Color(0.8f, 0.52f, 0.24f), //Earth
        Color.blue,//Water
        new Color(0.6f,0f,0.5f), //Dark
        Color.yellow, //Light
        Color.cyan, //Air
    };

    void Start()
    {
        UpdateCardDisplay();
    }

    public void UpdateCardDisplay()
    {
        cardImage.color = _cardColors[(int)cardData.cardType[0]];
        damageImage.color = _typeColors[(int)cardData.damageType[0]];

        nameText.text = cardData.cardName;
        healthText.text = cardData.health.ToString();
        damageText.text = $"{cardData.damageMin}-{cardData.damageMax}";
        displayImage.sprite = cardData.cardSprite;

        for (int i = 0; i < typeImages.Length; i++)
        {
            if (i < cardData.cardType.Count)
            {
                typeImages[i].gameObject.SetActive(true);
                typeImages[i].color = _typeColors[(int)cardData.cardType[i]];
            }
            else
            {
                typeImages[i].gameObject.SetActive(false);
            }
        }

    }
}
