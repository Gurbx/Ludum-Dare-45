using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelectHandler : MonoBehaviour
{
    [SerializeField] RoomTransitionHandler roomTransition;
    [SerializeField] private Button left, middle, right;
    private RoomCard card1, card2, card3;



    private void BuildRoomCard(RoomCard card)
    {
        roomTransition.TransitionToRoom(card);
    }

    public void Activate(RoomCard card1, RoomCard card2, RoomCard card3)
    {
        this.card1 = card1;
        this.card2 = card2;
        this.card3 = card3;

        if (card1 != null) left.gameObject.SetActive(true);
        if (card2 != null) middle.gameObject.SetActive(true);
        if (card3 != null) right.gameObject.SetActive(true);
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
        left.gameObject.SetActive(false);
        middle.gameObject.SetActive(false);
        right.gameObject.SetActive(false);
    }
}
