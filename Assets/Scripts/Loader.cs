using UnityEngine;
using System.Collections;


public class Loader : MonoBehaviour
{
    public GameObject gameManager;          //GameManager prefab to instantiate.


    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        if (GameManager.instance == null)
            Instantiate(gameManager);
    }
}