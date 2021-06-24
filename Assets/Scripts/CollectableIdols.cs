using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

public class CollectableIdols : MonoBehaviour
{
    [SerializeField] bool[] idolsCollected;
    [SerializeField] EventText eventText;
    [SerializeField] string EventTextWhenAllIdolsAreCollected;
        
    public void CollectIdol(int idolCollected)
    {
        idolsCollected[idolCollected] = true;
        IdolsRemainingText();
    }

    public bool CheckIfAllIdolsWereCollected()
    {
        foreach(bool idol in idolsCollected)
        {
            if(!idol) return false;
        }
        return true;
    }

    public bool[] GetIdolsArray()
    {
        return idolsCollected;
    }

    private int IdolsRemaining()
    {
        int remaining = 0;
        foreach(bool idol in idolsCollected)
        {
            if(!idol) remaining++;
        }
        return remaining;
    }

    public void IdolsRemainingText()
    {
        eventText.SetEventText("You need to collect " + IdolsRemaining() + " idols in order to advance.");
    }

    public void AllIdolsCollectedText()
    {
        eventText.SetEventText(EventTextWhenAllIdolsAreCollected);
    }

    public void SetEventText()
    {
        eventText = GameObject.FindObjectOfType<EventText>();
        if(eventText != null) IdolsRemainingText();
    }

    public EventText GetEventText()
    {
        return eventText;
    }

    public IEnumerator setEventTextCo()
    {
        while(eventText != null)
        {
            SetEventText();
            Debug.Log(eventText);
        } 
        yield return null;
    }
}
