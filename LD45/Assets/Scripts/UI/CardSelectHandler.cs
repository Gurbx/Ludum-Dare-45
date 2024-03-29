﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardSelectHandler : MonoBehaviour
{
    private int roomCurrency;

    private int nrRoomsPicked;

    [SerializeField] private List<RoomCard> earlyZeroCards;
    [SerializeField] private List<RoomCard> lateZeroCards;

    [SerializeField] private List<RoomCard> firstCards;
    [SerializeField] private List<RoomCard> earlyCards;
    [SerializeField] private List<RoomCard> lateCards;

    [SerializeField] private RoomCard endCard;

    [SerializeField] private Text questionText;

    [SerializeField] RoomTransitionHandler roomTransition;
    [SerializeField] private Button left, middle, right;
    [SerializeField] private Image leftImage, middleImage, rightImage;
    [SerializeField] private Text leftName, middleName, rightName;
    [SerializeField] private Text leftCost, middleCost, rightCost;
    [SerializeField] private Animator cardMenuAnimator;
    private RoomCard card1, card2, card3;

    [SerializeField] private AudioSource selectSound;


    private bool firstPickup;

    private void Start()
    {
        firstPickup = true;
        nrRoomsPicked = 0;
    }

    private RoomCard PopRoomCard()
    {
        RoomCard card;

        if (firstCards.Count > 0)
        {
            card = firstCards[0];
            firstCards.Remove(card);
        }
        else if (earlyCards.Count > 0)
        {
            int index = Random.Range(0, earlyCards.Count - 1);
            card = earlyCards[index];
            earlyCards.Remove(card);
        }
        else
        {
            int index = Random.Range(0, lateCards.Count - 1);
            card = lateCards[index];
            lateCards.Remove(card);
        }

        if (nrRoomsPicked > 3)
        {
            if (Random.Range(0, 100) < 33) return endCard;
        }

        return card;
    }

    private RoomCard GetZeroCard()
    {
        return earlyZeroCards[Random.Range(0, earlyZeroCards.Count)];

    }


    private void BuildRoomCard(RoomCard card)
    {
        nrRoomsPicked++;
        roomTransition.TransitionToRoom(card);
    }

    public void CardCollected()
    {
        //Only one choise first
        if (firstPickup)
        {
            card2 = PopRoomCard();
            middle.gameObject.SetActive(true);
            middleImage.color = card2.color;
            middleName.text = card2.name;
            middleCost.text = card2.roomCost.ToString();
            firstPickup = false;
        }
        else
        {
            card1 = PopRoomCard();
            card2 = PopRoomCard();
            card3 = GetZeroCard();

            if (card1 != null) left.gameObject.SetActive(true);
            if (card2 != null) middle.gameObject.SetActive(true);
            if (card3 != null) right.gameObject.SetActive(true);

            leftImage.color = card1.color;
            middleImage.color = card2.color;
            rightImage.color = card3.color;

            leftName.text = card1.name;
            middleName.text = card2.name;
            rightName.text = card3.name;

            leftCost.text = card1.roomCost.ToString();
            middleCost.text = card2.roomCost.ToString();
            rightCost.text = card3.roomCost.ToString();

            //Make cost text red if cant afford
            if (card1.roomCost > GameHandler.GetGameHandler().GetRoomCurrency())
                leftCost.color = Color.red;
            else leftCost.color = Color.white;

            if (card2.roomCost > GameHandler.GetGameHandler().GetRoomCurrency())
                middleCost.color = Color.red;
            else middleCost.color = Color.white;

            if (card3.roomCost > GameHandler.GetGameHandler().GetRoomCurrency())
                rightCost.color = Color.red;
            else rightCost.color = Color.white;


        }

        questionText.gameObject.SetActive(true);
        cardMenuAnimator.SetBool("visible", true);

    }


    public void LeftSelected()
    {
        if (card1.roomCost <= GameHandler.GetGameHandler().GetRoomCurrency())
        {
            if (card1.type == RoomCard.RoomType.BOSS)
            {
                HideAll();
                Invoke("Victory", 0.2f);
                return;
            }

            GameHandler.GetGameHandler().RemoveCurrency(card1.roomCost);
            BuildRoomCard(card1);
            HideAll();
            selectSound.Play();
        }
    }

    public void MiddleSelected()
    {
        if (card2.roomCost <= GameHandler.GetGameHandler().GetRoomCurrency())
        {
            if (card2.type == RoomCard.RoomType.BOSS)
            {
                HideAll();
                Invoke("Victory", 0.2f);
                return;
            }

            GameHandler.GetGameHandler().RemoveCurrency(card2.roomCost);
            BuildRoomCard(card2);
            HideAll();
            selectSound.Play();
        }
    }

    public void RightSelected()
    {
        if (card3.roomCost <= GameHandler.GetGameHandler().GetRoomCurrency())
        {
            if (card3.type == RoomCard.RoomType.BOSS)
            {
                HideAll();
                Invoke("Victory", 0.2f);
                return;
            }

            GameHandler.GetGameHandler().RemoveCurrency(card3.roomCost);
            BuildRoomCard(card3);
            HideAll();
            selectSound.Play();
        }
    }


    private void HideAll()
    {
        cardMenuAnimator.SetBool("visible", false);
        left.gameObject.SetActive(false);
        middle.gameObject.SetActive(false);
        right.gameObject.SetActive(false);
        questionText.gameObject.SetActive(false);
    }

    private void Victory()
    {
        SceneManager.LoadScene(1);
    }
}
