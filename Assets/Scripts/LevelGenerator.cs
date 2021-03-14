using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //private string[] PiValue = // 0번부터 999번까지.
    //    {
    //    "1415926535897932384626433832795028841971693993751058209749445923078164062862089986280348253421170679",
    //    "8214808651328230664709384460955058223172535940812848111745028410270193852110555964462294895493038196",
    //    "4428810975665933446128475648233786783165271201909145648566923460348610454326648213393607260249141273",
    //    "7245870066063155881748815209209628292540917153643678925903600113305305488204665213841469519415116094",
    //    "3305727036575959195309218611738193261179310511854807446237996274956735188575272489122793818301194912",
    //    "9833673362440656643086021394946395224737190702179860943702770539217176293176752384674818467669405132",
    //    "0005681271452635608277857713427577896091736371787214684409012249534301465495853710507922796892589235",
    //    "4201995611212902196086403441815981362977477130996051870721134999999837297804995105973173281609631859",
    //    "5024459455346908302642522308253344685035261931188171010003137838752886587533208381420617177669147303",
    //    "5982534904287554687311595628638823537875937519577818577805321712268066130019278766111959092164201989"
    //    };

    public GameObject LastNode;
    public GameObject NodePrefab;
    public int RandMaxValue = 7;
    public int RandMinValue = 2;

    /// <summary>
    /// private member vars
    /// </summary>
    private Vector3 nextRightPos = new Vector3(1.0f, 0.0f, 1.0f);
    private Vector3 nextLeftPos = new Vector3(-1.0f, 0.0f, 1.0f);
    private int lvIndex = 0;
    private int blockNodeSum = 0;
    private int[] randValues = new int[50];

    void Start()
    {
        setRandValue();
        GenerateNextNodes();
    }

    private void setRandValue()
    {
        for (int i = 0; i < 50; i++)
        {
            randValues[i] = Random.Range(RandMinValue, RandMaxValue);
        }
    }

    public void GenerateNextNodes()
    {
        int randInd = 0;
        int nextVal = randValues[randInd];
        bool dirFlag = true;
        while(randInd < 50)
        {
            nextVal = randValues[randInd];
            for (int i = 0; i < nextVal; i++)
            {
                generateSingleNode(dirFlag);
            }

            dirFlag = !dirFlag;
            randInd++;
        }
    }

    private void generateSingleNode(bool isRight)
    {
        Vector3 nextPos;
        GameObject newNode;

        if(isRight)
        {
            nextPos = LastNode.transform.position + nextRightPos;
            newNode = Instantiate(NodePrefab, nextPos, Quaternion.Euler(0.0f, 45.0f, 0.0f)) as GameObject;

            LastNode = newNode;
        }
        else
        {
            nextPos = LastNode.transform.position + nextLeftPos;
            newNode = Instantiate(NodePrefab, nextPos, Quaternion.Euler(0.0f, 45.0f, 0.0f)) as GameObject;

            LastNode = newNode;
        }

    }


}
