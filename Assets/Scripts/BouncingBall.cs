using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    [Header("资源")]
    [Tooltip("材质")]
    public List<Material> materialColors = new List<Material>();
    [Tooltip("黑色材质")]
    public Material materialBlack;

    [Tooltip("生成的预制体")]
    public GameObject totalPrefab;
    private GameObject curPrefab;
  //  [HideInInspector]
    public  List<GameObject> curprefabList = new List<GameObject>();


    [Tooltip("底部的预制体地形")]
    public GameObject wallPrefab;

    private Rigidbody rig;

    [Header("生成的数据")]
    [Tooltip("场景中预制体的总个数")]
    public int TotalPrefabsNumbers;
    private int startProduceNunber = 5;

    [Header("当前生成的预制体个数")]
    private int curProduceNunbers;
    private int curCanprodeuceNunbers;

    [Tooltip("生成的预制体的par")]
    private GameObject ProducePoint;
    private int addY = 0;
    private Vector3 lastPrefabsPoint = new Vector3(0, 0, 0);

    [Tooltip("生成的预制体的里面的子物体")]
    // private List<GameObject> curPrefabChilds = new List<GameObject>();

    private bool isGetMouseButton;

    [Header("可以踩踏的颜色最小和最大几个")]
    [Range(0, 8)]
    public int minWhileChildNumbers;
    [Range(0, 8)]
    public int maxWhileChildNumbers;

    [Tooltip("该子物体白色的子物体个数")]
    private int curTotalWhileChildNumbers;

    [Tooltip("当前白色的子物体个数")]
    private int curWhileChildNumbers;
    private int taterialNumber = 0;

    [Header("旋转的参数")]
    //旋转
    [Range(0, 8)]
    public int rotaAngle;
    public float rotaspeed;
    public float rotottimer;
    public float curtimer = 10;
    public float nunber = 1;
    private float   refi=0;
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        ProducePoint = GameObject.Find("ProducePoint");
        curProduceNunbers = 0;
        curWhileChildNumbers = 0;

    }

    // Update is called once per frame
    void Update()
    {
        
        //c当前场景的数量小余，
        if (curprefabList.Count<10&& curCanprodeuceNunbers< TotalPrefabsNumbers)
        {
            Produce();
        }

        ProducePointRotor();
    }
   

    /// <summary>
    /// 生成，位置、方向、材质、Tap的赋值
    /// </summary>
    private void Produce()
    {

        curProduceNunbers += 1;
        curCanprodeuceNunbers += 1;
        //生成、位置、方向、赋值父物体
        curPrefab = Instantiate(totalPrefab);
        //添加到列表当中

        curprefabList.Add(curPrefab);

        curPrefab.transform.parent = ProducePoint.transform;
        lastPrefabsPoint = new Vector3(0, addY -= 1, 0);
        curPrefab.transform.localPosition = lastPrefabsPoint;
        curPrefab.transform.localRotation = Quaternion.Euler(90, 0, Random.Range(1, 8) * 45);


        //里面的子物体
        Transform[] gas = curPrefab.GetComponentsInChildren<Transform>();


        //得到该子物体里的白色和黑色物体的个数的概率
        curTotalWhileChildNumbers = Random.Range(minWhileChildNumbers, maxWhileChildNumbers);
        //   Debug.Log(curTotalWhileChildNumbers);
        foreach (Transform child in gas)
        {


            //排除自身
            if (child.GetComponent<MeshRenderer>())
            {


                //子物体Tap赋值、材质赋值
                if (Random.Range(1, 8) <= curTotalWhileChildNumbers && curWhileChildNumbers < curTotalWhileChildNumbers)
                {

                    taterialNumber = Random.Range(0, materialColors.Count - 1);
                    child.GetComponent<MeshRenderer>().material = materialColors[taterialNumber];
                    child.tag = "WhileChild";
                    curWhileChildNumbers += 1;

                }
                else
                {
                    child.GetComponent<MeshRenderer>().material = materialBlack;
                    child.tag = "BlackChild";
                }
            }

            if (child == gas[gas.Length - 1])
            {
                curWhileChildNumbers = 0;
            }
        }

    }


    /// <summary>
    /// 整体的旋转
    /// </summary>
    private void ProducePointRotor()
    {
        curtimer += Time.deltaTime;

        if (curtimer >= Random.Range(rotottimer - 1, rotottimer + 1))
        {
            nunber = -nunber;
            nunber = Mathf.SmoothDamp(nunber, -nunber, ref refi, 0.6f);

            curtimer = 0;
        }
        ProducePoint.transform.Rotate(Vector3.up * rotaspeed * nunber);
    }
}
