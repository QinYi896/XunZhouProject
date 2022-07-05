using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        //c��ǰ����������С�࣬
        if (curprefabList.Count<10&& curCanprodeuceNunbers< TotalPrefabsNumbers)
        {
            Produce();
        }

        ProducePointRotor();
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
        //���ӵ��б�����

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