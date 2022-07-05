using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CamraMove : MonoBehaviour
{
    private Camera camera;
    private BouncingBall bouncingBall;

    public  float distances;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        bouncingBall = GameObject.Find("GameManager").GetComponent<BouncingBall>();

       
    }

    public void DistanceCalculate()
    {
        if (bouncingBall.curprefabList[0] != null)
        {
            //Vector2 vectCamera = new Vector2(camera.transform.localPosition.x, camera.transform.localPosition.y);
            //Vector2 vectOther = new Vector2(bouncingBall.curprefabList[0].transform.localPosition.x, bouncingBall.curprefabList[0].transform.localPosition.y);
            distances = camera.transform.localPosition.y - bouncingBall.curprefabList[0].transform.localPosition.y;


            camera.transform.DOLocalMoveY(bouncingBall.curprefabList[0].transform.localPosition.y+1.2f,0.3f);
          //  Debug.Log(distances+ ":distances");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

  public void MoveDown()
    {

    }
}
