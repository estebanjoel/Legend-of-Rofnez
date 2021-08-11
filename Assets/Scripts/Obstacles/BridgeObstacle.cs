using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeObstacle : MonoBehaviour
{
    [SerializeField] GameObject Wall;
    [SerializeField] GameObject BridgePlanks;
    bool bridgeIsBuild;
    [SerializeField] int planksToCollect;

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
    }

    private void BuildBridge()
    {
        Wall.SetActive(false);
        BridgePlanks.SetActive(true);
    }
}
