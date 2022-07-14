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
    [Header("��Դ")]
    [Tooltip("����")]
    public List<Material> materialColors = new List<Material>();
    [Tooltip("��ɫ����")]
    public Material materialBlack;

    [Tooltip("���ɵ�Ԥ����")]
    public GameObject totalPrefab;
    private GameObject curPrefab;
  //  [HideInInspector]
    public  List<GameObject> curprefabList = new List<GameObject>();
    public GameObject prefabsParent;

    [Tooltip("�ײ���Ԥ�������")]
    public GameObject wallPrefab;

    private Rigidbody rig;

    [Header("���ɵ�����")]
    [Tooltip("������Ԥ������ܸ���")]
    public int TotalPrefabsNumbers;
    private int startProduceNunber = 5;



    [Header("��ǰ���ɵ�Ԥ�������")]

    private int curProduceNunbers;
    private int curCanprodeuceNunbers;

    [Tooltip("���ɵ�Ԥ�����par")]
    private GameObject ProducePoint;
    private int addY = 0;
    private Vector3 lastPrefabsPoint = new Vector3(0, 0, 0);

    [Tooltip("���ɵ�Ԥ����������������")]
    // private List<GameObject> curPrefabChilds = new List<GameObject>();

    private bool isGetMouseButton;

    [Header("���Բ�̤����ɫ��С����󼸸�")]
    [Range(0, 8)]
    public int minWhileChildNumbers;
    [Range(0, 8)]
    public int maxWhileChildNumbers;

    [Tooltip("���������ɫ�����������")]
    private int curTotalWhileChildNumbers;

    [Tooltip("��ǰ��ɫ�����������")]
    private int curWhileChildNumbers;
    private int taterialNumber = 0;

    [Header("��ת�Ĳ���")]
    //��ת
    [Range(0, 8)]
    public int rotaAngle;
    public float rotaspeed;
    public float rotottimer;
    public float curtimer = 10;
    public float nunber = 1;
    private float   refi=0;

    public ProduceEnumWays enumWay;

    [Header("�ڶ������ɷ�ʽ")]

    [Tooltip("ÿ��֮��ļ��Y�Ƕ�")]
    public float spacingAngleY;
    [Tooltip("����һ���ĸ����󻻽Ƕ�")]
    public int otherAngleNumber;
    private int curOtherAngleNumber;
    [Tooltip("Z��ĽǶ�")]
    private float curAngle;

    [Range(1, 6)]
    private int blackNunber;


    [Header("���������ɷ�ʽ")]
    [Tooltip("�Ƿ�����ģ��Բ��")]
    private bool isProduceExampleCircle;

    private GameObject curExampleCircle;




    [Header("ȱʧ����")]
    public int min_DelectNuunber;
    public int Max_DelectNuunber;

    [Header("ȱʧ�ĸ���**��֮һ")]
    public int electDelectNuunber;
    public bool isDelect;


    [Header("������ĸ���**��֮һ")]
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
        
        //���ɷ�ʽ
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
    /// ���ɣ�λ�á����򡢲��ʡ�Tap�ĸ�ֵ
    /// </summary>
    private void ProduceWay3()
    {


        //����ģ��Բ��
        if(isProduceExampleCircle)
        {
            curExampleCircle= Instantiate(totalPrefab);
           //�����������
            Transform[] gas = curExampleCircle.GetComponentsInChildren<Transform>();

            //�õ�����������İ�ɫ�ͺ�ɫ����ĸ����ĸ���
            curTotalWhileChildNumbers = Random.Range(minWhileChildNumbers, maxWhileChildNumbers);

            foreach (Transform child in gas)
            {


                //�ų�����
                if (child.GetComponent<MeshRenderer>())
                {

                    //������Tap��ֵ�����ʸ�ֵ
                    //��ɫģ��ĸ��ʣ�Ҫ�ǰ�ɫģ��������ﵽ��ֵ��ʣ�µĶ��Ǻ�ɫģ��
                    if (Random.Range(1, 8) <= curTotalWhileChildNumbers && curWhileChildNumbers < curTotalWhileChildNumbers)
                    {

                        //��ɫ�ĸ�ֵ
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


        //���� ����� Բ��
        //���ɡ�λ�á����򡢸�ֵ������
        curPrefab = Instantiate(curExampleCircle);
        //��ӵ��б���
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
            //���ɵ���һ��������Բ���  ��Բ��ģ��
            isProduceExampleCircle = true;
        }
        else
        {

            curAngle += spacingAngleY;
        }

        
        curProduceNunbers += 1;
        curCanprodeuceNunbers += 1;



        //ɾ��
        //ɾ��
        //ɾ��
        //ɾ��
        Transform[] ga = curPrefab.GetComponentsInChildren<Transform>();

        //�õ�����������İ�ɫ�ͺ�ɫ����ĸ����ĸ���
        curTotalWhileChildNumbers = Random.Range(minWhileChildNumbers, maxWhileChildNumbers);
        foreach (Transform child in ga)
        {


            //�ų�����
            if (child.GetComponent<MeshRenderer>())
            {
                //ɾ��һ����
                int k = Random.Range(0, electDelectNuunber);
                if (k <= 1&& isDelect)
                {
                    Destroy(child.gameObject);
                }


            }


            if (child.GetComponent<MeshRenderer>())
            {
                //ɾ��һ����
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
    /// ���ɣ�λ�á����򡢲��ʡ�Tap�ĸ�ֵ
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

        //���ɡ�λ�á����򡢸�ֵ������
        curPrefab = Instantiate(totalPrefab);
        //��ӵ��б���

        curprefabList.Add(curPrefab);

        curPrefab.transform.parent = ProducePoint.transform;
        lastPrefabsPoint = new Vector3(0, addY -= 1, 0);
        curPrefab.transform.localPosition = lastPrefabsPoint;
        curPrefab.transform.localRotation = Quaternion.Euler(90, 0, curAngle);


        //�����������
        Transform[] gas = curPrefab.GetComponentsInChildren<Transform>();


        //�õ�����������İ�ɫ�ͺ�ɫ����ĸ����ĸ���
        curTotalWhileChildNumbers = Random.Range(minWhileChildNumbers, maxWhileChildNumbers);
        //   Debug.Log(curTotalWhileChildNumbers);
        foreach (Transform child in gas)
        {


            //�ų�����
            if (child.GetComponent<MeshRenderer>())
            {


                //������Tap��ֵ�����ʸ�ֵ
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
    /// ���ɣ�λ�á����򡢲��ʡ�Tap�ĸ�ֵ
    /// </summary>
    private void Produce()
    {

        curProduceNunbers += 1;
        curCanprodeuceNunbers += 1;
        //���ɡ�λ�á����򡢸�ֵ������
        curPrefab = Instantiate(totalPrefab);
        //��ӵ��б���

        curprefabList.Add(curPrefab);

        curPrefab.transform.parent = ProducePoint.transform;
        lastPrefabsPoint = new Vector3(0, addY -= 1, 0);
        curPrefab.transform.localPosition = lastPrefabsPoint;
        curPrefab.transform.localRotation = Quaternion.Euler(90, 0, Random.Range(1, 8) * 45);


        //�����������
        Transform[] gas = curPrefab.GetComponentsInChildren<Transform>();


        //�õ�����������İ�ɫ�ͺ�ɫ����ĸ����ĸ���
        curTotalWhileChildNumbers = Random.Range(minWhileChildNumbers, maxWhileChildNumbers);
        //   Debug.Log(curTotalWhileChildNumbers);
        foreach (Transform child in gas)
        {


            //�ų�����
            if (child.GetComponent<MeshRenderer>())
            {


                //������Tap��ֵ�����ʸ�ֵ
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
    /// �������ת
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
