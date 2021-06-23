using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

public class CollectableIdols : MonoBehaviour
{
    [SerializeField] bool[] idolsCollected;
    EventText eventText;
    [SerializeField] string EventTextWhenAllIdolsAreCollected;

    private void Start()
    {
        eventText = GameObject.FindObjectOfType<EventText>();
    }
        
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
            if(!idol) remaining ++;
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
}
