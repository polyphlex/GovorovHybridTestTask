using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValueDispatcher : MonoBehaviour
{
    public static GlobalValueDispatcher instance;

    private byte playerLives = 2;

    public byte PlayerLivesAmount
    {
        get { return playerLives; }
    }

    public void ResetValueDispatcher()
    {
        playerLives = 2;
    }


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("More than one GlobalValueDispatcher, disposing...");
            Destroy(this.gameObject);
        }
    }

    public void AddLife()
    {
        playerLives++;
    }

    public void RemoveLife()
    {
        playerLives--;
    }
    

}
