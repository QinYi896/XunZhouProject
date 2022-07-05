using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _gameManager;
    private void Awake()
    {
        _gameManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
