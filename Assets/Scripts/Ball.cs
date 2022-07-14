using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Ball : MonoBehaviour
{
    private Rigidbody rig;
    public bool isGetMouseButton;
    private Animator animator;
    private Ball ball;
    Vector3 vector;
    private BouncingBall bouncingBall;
    private CamraMove camraMove;
    public ParticleSystem[] downEffect;
    public GameObject[] DownEffectIma;
    public GameObject[] DeathEffectIma;
    private int randNunber;

    public int texNunber=0;

    private bool hasCollied=false;

    //爆炸特效
    public ParticleSystem DeathExplosion;
    // Start is called before the first frame update
    void Start()
    {
        float vy = Mathf.Sqrt(Mathf.Abs(Physics.gravity.y * 1.8f));

        vector = Vector3.up * vy;
        rig = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        ball = GameObject.Find("Sphere").GetComponent<Ball>();
        bouncingBall = GameObject.Find("GameManager").GetComponent<BouncingBall>();
        camraMove = GameObject.Find("GameManager").GetComponent<CamraMove>();
    }

    // Update is called once per frame
    void Update()
    {
        ImputFunction();
        JudgePlayPoint();
    }
    private void LateUpdate()
    {
        hasCollied = false;
    }
    private void ImputFunction()
    {
        //鼠标左键或者长按屏幕
        if (Input.GetMouseButton(0))
        {
            isGetMouseButton = true;

         //   Debug.Log("1111111111");
        }
        else
        {
            isGetMouseButton = false;
        }
    }

    private void JudgePlayPoint()
    {
      //  Debug.Log(bouncingBall);

        if (transform.position.y <= bouncingBall.curprefabList[0].transform.position.y)
        {

            Destroy(bouncingBall.curprefabList[0].gameObject);
            bouncingBall.curprefabList.Remove(bouncingBall.curprefabList[0]);
            camraMove.DistanceCalculate();
        }
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        //只是调用一次
        if(hasCollied==true) { return; }

        if (isGetMouseButton)
        {
            
            if (collision.transform.tag == "WhileChild"|| collision.transform.tag == "ScoreChild")
            {
               
                //    hasCollied = false;
                bouncingBall.curprefabList.Remove(collision.transform.parent.gameObject);
                Destroy(collision.transform.parent.gameObject);
                rig.velocity = -vector * 2;
            }
            //
            else if (collision.transform.tag == "BlackChild")
            {
                randNunber = Random.Range(0, 2);
                //水迹图片
                //z制作特片的透明度变化
                GameObject gaIma = Instantiate(DeathEffectIma[randNunber]);
                int rotor = Random.Range(0, 360);
                gaIma.transform.parent = bouncingBall.curprefabList[0].transform;
                gaIma.transform.localRotation = Quaternion.Euler(0, rotor, 0);

                gaIma.transform.localPosition = new Vector3(0, collision.transform.localPosition.y + 0.1f,-3.9f);
                Image image = gaIma.transform.Find("Image").GetComponent<Image>();
                //  image.DOFade(0, 4f);
               // Destroy(gaIma.gameObject, 4f);


                Debug.Log(collision.transform.tag + ":222222222222");
                ParticleSystem effect = Instantiate(DeathExplosion);
                // effect.transform.localRotation = Quaternion.Euler(-90, 0, 0);
                effect.transform.position = transform.position;
                Destroy(effect.gameObject, 2f);
                Destroy(ball.gameObject);
            }
            camraMove.DistanceCalculate();
        }
        else
        {
            if(collision.gameObject!=null)

            {
                texNunber += 1;

                //随机生成蓝色和黄色的特效和水迹
                randNunber = Random.Range(0, 2);
                Debug.Log(randNunber);
                animator.SetTrigger("OnWall");


                rig.velocity = vector;

                //特效粒子
                ParticleSystem effect = Instantiate(downEffect[randNunber]);
                effect.transform.localRotation = Quaternion.Euler(-90, 0, 0);
                effect.transform.position = transform.position;
                Destroy(effect.gameObject, 2f);


                GameObject gaIma = Instantiate(DownEffectIma[randNunber]);
                int rotor = Random.Range(0, 360);
                gaIma.transform.localRotation = Quaternion.Euler(90, rotor, 0);
                gaIma.transform.position = new Vector3(transform.position.x, collision.transform.position.y + 0.07f, transform.position.z);

                //Image image = gaIma.transform.Find("Image").GetComponent<Image>();
                //image.DOFade(0, 1f);

                Destroy(gaIma.gameObject, 1f);

                hasCollied = true;
            }

              
        }

      
      
    }
}
