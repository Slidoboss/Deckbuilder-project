using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SoloudoFiles
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public Sprite cardSprite;
        public string cardName;
        public List<CardType> cardType;
        public int health;
        public int damageMin;
        public int damageMax;
        public List<DamageType> damageType;
        public GameObject prefab;
        public int range;
        public AttackPattern attackPattern;
        public PriorityTarget priorityTarget;


        public enum CardType
        {
            Fire, Earth, Water, Dark, Light, Air
        }
        public enum DamageType
        {
            Fire, Earth, Water, Dark, Light, Air
        }
        public enum AttackPattern
        {
            Single, Multitarget, Cross, Column, Row, TwoByTwo, FourByFour
        }
        public enum PriorityTarget
        {
            Far, Close, LeastCurrentHealth, MostCurrentHealth, MostMaxHealth, MostDamage,
        }
    }
}

