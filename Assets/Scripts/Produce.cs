using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Produce : MonoBehaviour
{
    
    private GameObject ga;
    public GameObject curCircle;
    // Start is called before the first frame update
    void Start()
    {
        ga = (GameObject)Resources.Load("Resources/TotalCircle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  private void PeoduceCircle()
    {
       
    //    curCircle = Instantiate(ga,);
    }
}
