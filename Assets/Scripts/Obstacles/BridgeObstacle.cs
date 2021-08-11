using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

public class BridgeObstacle : MonoBehaviour
{
    [SerializeField] GameObject Wall;
    [SerializeField] GameObject BridgePlanks;
    bool bridgeIsBuild;
    [SerializeField] int planksToCollect;
    EventText eventText;

    private void Start()
    {
        eventText = GameObject.FindObjectOfType<EventText>();
        SetBridgeEventText("You need to collect " + planksToCollect + " planks in order to build the bridge");
    }

    private void Update()
    {
        if(planksToCollect == 0)
        {
            if(!bridgeIsBuild)
            {
                BuildBridge();
                bridgeIsBuild = true;
            }
        }
    }
    
    public int GetPlanksToCollect()
    {
        return planksToCollect;
    }

    public void CollectPlank()
    {
        planksToCollect-=1;
        SetBridgeEventText("You need to collect " + planksToCollect + " planks in order to build the bridge");
    }

    private void BuildBridge()
    {
        Wall.SetActive(false);
        BridgePlanks.SetActive(true);
        SetBridgeEventText("You can now cross the bridge and fight the king!");
    }

    public void SetBridgeEventText(string myText)
    {
        eventText.SetEventText(myText);
    }
}
