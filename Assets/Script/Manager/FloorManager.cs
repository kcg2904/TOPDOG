using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [Header("매니저 리스트")]
    public MonsterManager mMonsterManager;
    public PlayerManager mPlayerManager;


    [Header("오브젝트")]
    public MoveFloor mMoveFloor;
    public GameObject mFloorPrefab; //복제할 프리펩
    public Transform mFloorParent; //프리팹의 부모의 자리


    public int mMax_X = 8;
    public int mMax_Y = 5;


    GameObject mPlayerOnFloor;


    [HideInInspector] public List<List<GameObject>> mFloorList = new List<List<GameObject>>();
    [HideInInspector] public List<int> yList;
    [HideInInspector] public List<int> xList;

    Material mMat_Defult;
    Material mMat_Green;

    [HideInInspector] public bool mIn_Monster = false;

    void Awake()
    {
        Create_MoveFloor();
    }

    // Start is called before the first frame update
    void Start()
    {
        

        mPlayerManager.mPlayer.transform.position = mFloorList[0][2].transform.position;

        mMat_Defult = Resources.Load("d", typeof(Material)) as Material;
        mMat_Green = Resources.Load("g", typeof(Material)) as Material;
    }

    // 무브플로어 생성
    #region Create MoveFloor List

    public void Create_MoveFloor()
    {
        for (int i = 0; i < mMax_X; i++)
        {
            mFloorList.Add(new List<GameObject>());
            for (int j = 0; j < mMax_Y; j++)
            {
                //생성부
                GameObject obj = GameObject.Instantiate(mFloorPrefab);
                //변형부
                mMoveFloor = obj.GetComponent<MoveFloor>();
                mMoveFloor.mX = i;
                mMoveFloor.mY = j;
                obj.name = "MoveFloor [" + i + "], [" + j + "]";
                obj.transform.parent = mFloorParent;
                obj.transform.position = new Vector3(2.5f * i, 0.1f, 2.5f * j);
                //저장부
                mFloorList[i].Add(obj);
            }
        }
        mFloorParent.transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    #endregion

    public void Get_Player_On_Floor()
    {
        Vector3 mPlayer_pos = mPlayerManager.mPlayer.transform.position;

        for (int i = 0; i < mMax_X; i++)
        {
            for (int j = 0; j < mMax_Y; j++)
            {
                if (mFloorList[i][j].transform.position == mPlayer_pos)
                    mPlayerOnFloor = mFloorList[i][j];
            }
        }
    }



    public void Defult_To_MoveFloor()
    {

        for (int i = 0; i < mMax_X; i++)
        {
            for (int j = 0; j < mMax_Y; j++)
            {
                mFloorList[i][j].GetComponent<BoxCollider>().enabled = false;
                mFloorList[i][j].GetComponent<SpriteRenderer>().material = mMat_Defult;
            }
        }
    }
 

    public void Enable_Collider()
    {

        Get_Player_On_Floor();
        
        int mX = mPlayerOnFloor.GetComponent<MoveFloor>().mX;
        int mY = mPlayerOnFloor.GetComponent<MoveFloor>().mY;
        
        xList = new List<int>();
        yList = new List<int>();


        if(mX + 1 > 7)
        {
            xList.Add(mX - 1);
            xList.Add(mX);
        }
        else if(mX-1 < 0)
        {
            xList.Add(mX);
            xList.Add(mX + 1);
        }
        else
        {
            xList.Add(mX - 1);
            xList.Add(mX);
            xList.Add(mX + 1);
        }
        
        if(mY + 1 > 4)
        {
            yList.Add(mY - 1);
            yList.Add(mY);
        }
        else if(mY - 1 < 0)
        {
            yList.Add(mY);
            yList.Add(mY + 1);
        }
        else
        {
            yList.Add(mY - 1);
            yList.Add(mY);
            yList.Add(mY + 1);
        }


        foreach (int x in xList)
        {
            foreach (int y in yList)
            {
                if(mFloorList[x][y].transform.position != mPlayerManager.mPlayer.transform.position)
                {
                    if (mFloorList[x][y].transform.position != mPlayerManager.mMonster.transform.position)
                    {
                        mFloorList[x][y].GetComponent<BoxCollider>().enabled = true;
                        mFloorList[x][y].GetComponent<SpriteRenderer>().material = mMat_Green;
                    }
                    else
                    {
                        mIn_Monster = true;
                        
                    }
                }

            }
        }




    }

}
