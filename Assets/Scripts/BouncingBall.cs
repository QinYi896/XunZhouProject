using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  enum ProduceEnumWays
{
    One_way,
    two_way,
    Three_way,
}
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
    public GameObject prefabsParent;

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

    public ProduceEnumWays enumWay;

    [Header("第二种生成方式")]

    [Tooltip("每个之间的间距Y角度")]
    public float spacingAngleY;
    [Tooltip("生成一定的个数后换角度")]
    public int otherAngleNumber;
    private int curOtherAngleNumber;
    [Tooltip("Z轴的角度")]
    private float curAngle;

    [Range(1, 6)]
    private int blackNunber;


    [Header("第三种生成方式")]
    [Tooltip("是否生成模板圆形")]
    private bool isProduceExampleCircle;

    private GameObject curExampleCircle;




    [Header("缺失部分")]
    public int min_DelectNuunber;
    public int Max_DelectNuunber;

    [Header("缺失的概率**分之一")]
    public int electDelectNuunber;
    public bool isDelect;


    [Header("分数块的概率**分之一")]
    public int scoreNuunber;
    public bool isScoreProdece;
    public Material scoreMaterial;
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        ProducePoint = GameObject.Find("ProducePoint");
        curProduceNunbers = 0;
        curWhileChildNumbers = 0;
        curOtherAngleNumber = 0;
        curAngle = 0;
        isProduceExampleCircle = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        //生成方式
        if (curprefabList.Count<10&& curCanprodeuceNunbers< TotalPrefabsNumbers)
        {
            switch(enumWay)
            {
                case ProduceEnumWays.One_way:
                    Produce();
                    break;

                case ProduceEnumWays.two_way:
                    ProduceWay2();
                    break;

                case ProduceEnumWays.Three_way:
                    ProduceWay3();
                    break;
            }

          //  Produce();
        }

        ProducePointRotor();
    }


    /// <summary>
    /// 生成，位置、方向、材质、Tap的赋值
    /// </summary>
    private void ProduceWay3()
    {


        //生成模板圆块
        if(isProduceExampleCircle)
        {
            curExampleCircle= Instantiate(totalPrefab);
           //里面的子物体
            Transform[] gas = curExampleCircle.GetComponentsInChildren<Transform>();

            //得到该子物体里的白色和黑色物体的个数的概率
            curTotalWhileChildNumbers = Random.Range(minWhileChildNumbers, maxWhileChildNumbers);

            foreach (Transform child in gas)
            {


                //排除自身
                if (child.GetComponent<MeshRenderer>())
                {

                    //子物体Tap赋值、材质赋值
                    //白色模块的概率，要是白色模块的数量达到数值，剩下的都是黑色模块
                    if (Random.Range(1, 8) <= curTotalWhileChildNumbers && curWhileChildNumbers < curTotalWhileChildNumbers)
                    {

                        //颜色的赋值
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


            isProduceExampleCircle = false;
        }


        //生成 定义的 圆块
        //生成、位置、方向、赋值父物体
        curPrefab = Instantiate(curExampleCircle);
        //添加到列表当中
        curprefabList.Add(curPrefab);

        curPrefab.transform.parent = ProducePoint.transform;
        lastPrefabsPoint = new Vector3(0, addY -= 1, 0);
        curPrefab.transform.localPosition = lastPrefabsPoint;
        curPrefab.transform.localRotation = Quaternion.Euler(90, 0, curAngle);








        curOtherAngleNumber += 1;
        if (curOtherAngleNumber >= Random.Range(otherAngleNumber - 2, otherAngleNumber + 2))
        {
            curOtherAngleNumber = 0;
            curAngle = Random.Range(1, 8) * 45;
            //生成到达一定数量的圆块后  换圆块模板
            isProduceExampleCircle = true;
        }
        else
        {

            curAngle += spacingAngleY;
        }

        
        curProduceNunbers += 1;
        curCanprodeuceNunbers += 1;



        //删除
        //删除
        //删除
        //删除
        Transform[] ga = curPrefab.GetComponentsInChildren<Transform>();

        //得到该子物体里的白色和黑色物体的个数的概率
        curTotalWhileChildNumbers = Random.Range(minWhileChildNumbers, maxWhileChildNumbers);
        foreach (Transform child in ga)
        {


            //排除自身
            if (child.GetComponent<MeshRenderer>())
            {
                //删除一部分
                int k = Random.Range(0, electDelectNuunber);
                if (k <= 1&& isDelect)
                {
                    Destroy(child.gameObject);
                }


            }


            if (child.GetComponent<MeshRenderer>())
            {
                //删除一部分
                int k = Random.Range(0, electDelectNuunber);

                if (k <= 1 && isScoreProdece)
                {
                    child.GetComponent<MeshRenderer>().material =scoreMaterial;
                    child.tag = "ScoreChild";

                }


            }
        }




    }




    /// <summary>
    /// 生成，位置、方向、材质、Tap的赋值
    /// </summary>
    private void ProduceWay2()

    {
        curOtherAngleNumber += 1;
        if(curOtherAngleNumber>=Random.Range(otherAngleNumber-2, otherAngleNumber+2))
        {
            curOtherAngleNumber = 0;
            curAngle = Random.Range(1, 8) * 45;
        }
        else
        {

            curAngle += spacingAngleY;
        }
      

        curProduceNunbers += 1;
        curCanprodeuceNunbers += 1;

        //生成、位置、方向、赋值父物体
        curPrefab = Instantiate(totalPrefab);
        //添加到列表当中

        curprefabList.Add(curPrefab);

        curPrefab.transform.parent = ProducePoint.transform;
        lastPrefabsPoint = new Vector3(0, addY -= 1, 0);
        curPrefab.transform.localPosition = lastPrefabsPoint;
        curPrefab.transform.localRotation = Quaternion.Euler(90, 0, curAngle);


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
