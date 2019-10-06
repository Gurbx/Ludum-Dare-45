using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectHandler : MonoBehaviour
{
    private int roomCurrency;

    [SerializeField] private List<RoomCard> firstCards;
    [SerializeField] private List<RoomCard> earlyCards;
    [SerializeField] private List<RoomCard> lateCards;


    [SerializeField] RoomTransitionHandler roomTransition;
    [SerializeField] private Button left, middle, right;
    [SerializeField] private Image leftImage, middleImage, rightImage;
    [SerializeField] private Text leftName, middleName, rightName;
    [SerializeField] private Animator cardMenuAnimator;
    private RoomCard card1, card2, card3;


    private bool firstPickup;

    private void Start()
    {
        firstPickup = true;
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

        return card;
    }


    private void BuildRoomCard(RoomCard card)
    {
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
            firstPickup = false;
        }
        else
        {
            card1 = PopRoomCard();
            card2 = PopRoomCard();
            card3 = PopRoomCard();

            if (card1 != null) left.gameObject.SetActive(true);
            if (card2 != null) middle.gameObject.SetActive(true);
            if (card3 != null) right.gameObject.SetActive(true);

            leftImage.color = card1.color;
            middleImage.color = card2.color;
            rightImage.color = card3.color;

            leftName.text = card1.name;
            middleName.text = card2.name;
            rightName.text = card3.name;
        }

        cardMenuAnimator.SetBool("visible", true);
    }


    public void LeftSelected()
    {
        BuildRoomCard(card1);
        HideAll();
    }

    public void MiddleSelected()
    {
        BuildRoomCard(card2);
        HideAll();
    }

    public void RightSelected()
    {
        BuildRoomCard(card3);
        HideAll();
    }


    private void HideAll()
    {
        cardMenuAnimator.SetBool("visible", false);
        left.gameObject.SetActive(false);
        middle.gameObject.SetActive(false);
        right.gameObject.SetActive(false);
    }
}
