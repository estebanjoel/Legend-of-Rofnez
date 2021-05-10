using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamaManager : Lif
{
    Player loseCondition;
    Boss winCondition;
    void Start()
    {
        loseCondition = GetComponent<Player>();
        winCondition = GetComponent<Boss>();
    }

    // Update is called once per frame
    void Update()  
    {
        if (loseCondition.life <= 0)
        {
            SceneManager.LoadScene(1);
        }
        else if (winCondition.life <= 0)
        {
            SceneManager.LoadScene(1);
        }
        if(loseCondition.getTheKey == true)
        {
            //se habre la puerta del boss
        }
    }
}
