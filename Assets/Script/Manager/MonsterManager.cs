using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterManager : MonoBehaviour
{
    [Header("매니저 리스트")]
    public TrunManager mTrunManager;
    public FloorManager mFloorManager;
    public PlayerManager mPlayerManager;
    public UIManager mUimanager;
    [Header("프리펩")]
    public GameObject mMonsterPrefab;

    [Header("게임 오브젝트")]
    public GameObject Monster;
    public GameObject mPlayer;

    [Header("오디오")]
    public AudioSource mAudio;

    [Header("")]

    public bool MonsterTrun;
    public GameObject mMonsterPosition;
    public List<List<GameObject>> mMonsterList;

    public int mX;
    public int mY;

    int mHp;


    public bool CheckX1;
    public bool CheckX2;
    public bool CheckY1;
    public bool CheckY2;

    public int DelayA = 0;
    public int DelayB = 0;
    public int DelayC = 0;

    public Vector3 pos;
    public Vector3 point;
    // Start is called before the first frame update
    void Start()
    {
        Monster = GameObject.Instantiate(mMonsterPrefab);
        
       
        Monster.name = "Monster";
        mMonsterPosition = mFloorManager.mFloorList[7][2];
        mMonsterList = mFloorManager.mFloorList;
        Monster.transform.position = mMonsterPosition.transform.position;

        mPlayer = GameObject.Find("Player");
        Monster.transform.LookAt(mPlayer.transform.position);
        pos = Monster.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Setting();
        Trun();
        //StartCoroutine(@GetHit(mPlayer.transform.position));
    }
    private void Setting()
    {
        CheckX1 = true;
        CheckX2 = true;
        CheckY1 = true;
        CheckY2 = true;
        mHp = mUimanager.monsterHP;
        for (int i = 0; i < mFloorManager.mMax_X; i++)
        {
            for (int j = 0; j < mFloorManager.mMax_Y; j++)
            {
                if (mFloorManager.mFloorList[i][j].transform.position == Monster.transform.position)
                {
                    mMonsterPosition = mFloorManager.mFloorList[i][j];
                }
            }
        }
        mPlayer = GameObject.Find("Player");
        MonsterTrun = mTrunManager.monstertrun;
        mX = mMonsterPosition.GetComponent<MoveFloor>().mX;
        mY = mMonsterPosition.GetComponent<MoveFloor>().mY;
        if (mX == 7)
        {
            CheckX1 = false;
        }
        else if (mX == 0)
        {
            CheckX2 = false;
        }

        if (mY == 4)
        {
            CheckY1 = false;
        }
        else if (mY == 0)
        {
            CheckY2 = false;
        }


        //x,y 0,0
        //x,y+1 /x,y-1 /x+1,y-1 /x+1,y+1/ x+1,y / x-1,y/ x-1,y-1/ x-1 ,y+1

    }
    /*IEnumerator @GetHit(Vector3 _pos)
    {
        Hit = GameObject.Instantiate(mHitPrefab);
        Hit.name = "Hit";
        Hit.transform.position = _pos;
        yield return new 
        Destroy(Hit);
    }
    */
    public void Trun()
    {
        if (MonsterTrun && mTrunManager.playertrun == false)
        {
            if (CheckX1 == false && CheckY1 == false)
            {
                MoveCheckXY1();
            }
            else if (CheckX1 == false && CheckY2 == false)
            {
                MoveCheckXY3();
            }
            else if (CheckX2 == false && CheckY1 == false)
            {
                MoveCheckXY4();
            }
            else if (CheckX2 == false && CheckY2 == false)
            {
                MoveCheckXY2();
            }
            else if (CheckX1 == false)
            {
                MoveCheckX1();
            }
            else if (CheckX2 == false)
            {
                MoveCheckX2();
            }
            else if (CheckY1 == false)
            {
                MoveCheckY1();
            }
            else if (CheckY2 == false)
            {
                MoveCheckY2();
            }
            else if (CheckX1 && CheckX2 && CheckY1 && CheckY2)
            {
                MoveCheck();
            }
        }
    }

    IEnumerator @MonsterMovementSound()
    {
        if (SoundManager.m_instance.m_SFX_Player.isPlaying == false)
        {
            SoundManager.m_instance.PlaySFXAudio("Sound_Slime_movement");
        }
        yield return new WaitForSeconds(2.5f);
        SoundManager.m_instance.StopSFX();
        StopCoroutine(@MonsterMovementSound());
    }

    #region 몬스터 이동
    private void Move() { 
        //print("이동");
        pos = Monster.transform.position;

        StartCoroutine(@MonsterMovementSound());

        if (mY - mPlayerManager.mY > 0)
        {
            point = mFloorManager.mFloorList[mX][mY - 1].transform.position;
            Monster.transform.LookAt(point);
            Monster.GetComponent<Animator>().SetBool("Move", true);
            Monster.transform.position = Vector3.Lerp(Monster.transform.position,
                point, Time.deltaTime);
            

        }
        else if (mY - mPlayerManager.mY < 0)
        {
            point = mFloorManager.mFloorList[mX][mY + 1].transform.position;
            Monster.transform.LookAt(point);
            Monster.GetComponent<Animator>().SetBool("Move", true);
            Monster.transform.position = Vector3.Lerp(Monster.transform.position,
                point, Time.deltaTime);

        }
        else if ((mY - mPlayerManager.mY) == 0)
        {
            //print("Y 맞춤");
            if (mX - mPlayerManager.mX > 0)
            {
                point = mFloorManager.mFloorList[mX - 1][mY].transform.position;
                Monster.transform.LookAt(point);
                Monster.GetComponent<Animator>().SetBool("Move", true);
                Monster.transform.position = Vector3.Lerp(Monster.transform.position,
                    point, Time.deltaTime);

            }
            else if (mX - mPlayerManager.mX < 0)
            {
                point = mFloorManager.mFloorList[mX + 1][mY].transform.position;
                Monster.transform.LookAt(point);
                Monster.GetComponent<Animator>().SetBool("Move", true);
                Monster.transform.position = Vector3.Lerp(Monster.transform.position,
                    point, Time.deltaTime);

            }
        }


    }
    private void MoveCheckX1()
    {//x = 7
        if (mMonsterList[mX][mY + 1].transform.position != mPlayer.transform.position &&
            mMonsterList[mX][mY - 1].transform.position != mPlayer.transform.position &&
            mMonsterList[mX - 1][mY].transform.position != mPlayer.transform.position &&
            mMonsterList[mX - 1][mY + 1].transform.position != mPlayer.transform.position &&
            mMonsterList[mX - 1][mY - 1].transform.position != mPlayer.transform.position
            )
        {
            //print("x1");

            
            Move();
            
            if (Vector3.Distance(pos, point) <= GlobalValue.monsterDist)
            {
                Monster.transform.position = point;
                Monster.GetComponent<Animator>().SetBool("Move", false);
                Monster.transform.LookAt(mPlayer.transform.position);
                point = Vector3.zero;
                mTrunManager.Reset1 = true;
            }
        }
        else
        {
            skill();
        }
    }
    private void MoveCheckX2()
    {//x = 0
        if (mMonsterList[mX][mY - 1].transform.position != mPlayer.transform.position &&
            mMonsterList[mX][mY + 1].transform.position != mPlayer.transform.position &&
            mMonsterList[mX + 1][mY].transform.position != mPlayer.transform.position &&
            mMonsterList[mX + 1][mY + 1].transform.position != mPlayer.transform.position &&
            mMonsterList[mX + 1][mY - 1].transform.position != mPlayer.transform.position
            )
        {
            //print("x2");
            Move();
            if (Vector3.Distance(pos, point) <= GlobalValue.monsterDist)
            {
                Monster.transform.position = point;
                Monster.GetComponent<Animator>().SetBool("Move", false);
                Monster.transform.LookAt(mPlayer.transform.position);

                point = Vector3.zero;
                mTrunManager.Reset1 = true;
            }
        }
        else
        {
            skill();
        }
    }
    private void MoveCheckY1()
    {//y = 4
        if (mMonsterList[mX][mY - 1].transform.position != mPlayer.transform.position &&
            mMonsterList[mX + 1][mY].transform.position != mPlayer.transform.position &&
            mMonsterList[mX + 1][mY - 1].transform.position != mPlayer.transform.position &&
            mMonsterList[mX - 1][mY].transform.position != mPlayer.transform.position &&
            mMonsterList[mX - 1][mY - 1].transform.position != mPlayer.transform.position
            )
        {
            //print("y1");
            Move(); 
            if (Vector3.Distance(pos, point) <= GlobalValue.monsterDist)
            {
                Monster.transform.position = point;
                Monster.GetComponent<Animator>().SetBool("Move", false);
                Monster.transform.LookAt(mPlayer.transform.position);

                point = Vector3.zero;
                mTrunManager.Reset1 = true;
            }
        }
        else
        {
            skill();
        }
    }
    private void MoveCheckY2()
    {//y = 0
        if (mMonsterList[mX][mY + 1].transform.position != mPlayer.transform.position &&
            mMonsterList[mX + 1][mY].transform.position != mPlayer.transform.position &&
            mMonsterList[mX + 1][mY + 1].transform.position != mPlayer.transform.position &&
            mMonsterList[mX - 1][mY].transform.position != mPlayer.transform.position &&
            mMonsterList[mX - 1][mY + 1].transform.position != mPlayer.transform.position
            )
        {
            //print("y2");
            Move();
            if (Vector3.Distance(pos, point) <= GlobalValue.monsterDist)
            {
                Monster.transform.position = point;
                Monster.GetComponent<Animator>().SetBool("Move", false);
                Monster.transform.LookAt(mPlayer.transform.position);

                point = Vector3.zero;
                mTrunManager.Reset1 = true;
            }
        }
        else
        {
            skill();
        }
    }
    private void MoveCheckXY1()
    {//7,4
        if (mMonsterList[mX][mY - 1].transform.position != mPlayer.transform.position &&
            mMonsterList[mX - 1][mY].transform.position != mPlayer.transform.position &&
            mMonsterList[mX - 1][mY - 1].transform.position != mPlayer.transform.position
            )
        {
            //print("xy1");
            Move();
            if (Vector3.Distance(pos, point) <= GlobalValue.monsterDist)
            {
                Monster.transform.position = point;
                Monster.GetComponent<Animator>().SetBool("Move", false);
                Monster.transform.LookAt(mPlayer.transform.position);

                point = Vector3.zero;
                mTrunManager.Reset1 = true;
            }
        }
        else
        {
            skill();
        }
    }
    private void MoveCheckXY2()
    {// 0,0
        if (mMonsterList[mX][mY + 1].transform.position != mPlayer.transform.position &&
            mMonsterList[mX + 1][mY].transform.position != mPlayer.transform.position &&
            mMonsterList[mX + 1][mY + 1].transform.position != mPlayer.transform.position
            )
        {
            //print("xy2");
            Move();
            if (Vector3.Distance(pos, point) <= GlobalValue.monsterDist)
            {
                Monster.transform.position = point;
                Monster.GetComponent<Animator>().SetBool("Move", false);
                Monster.transform.LookAt(mPlayer.transform.position);

                point = Vector3.zero;
                mTrunManager.Reset1 = true;
            }
        }
        else
        {
            skill();
        }
    }
    private void MoveCheckXY3()
    { //7,0
        if (mMonsterList[mX][mY + 1].transform.position != mPlayer.transform.position &&
            mMonsterList[mX - 1][mY].transform.position != mPlayer.transform.position &&
            mMonsterList[mX - 1][mY + 1].transform.position != mPlayer.transform.position
            )
        {
            //print("xy3");
            Move();
            if (Vector3.Distance(pos, point) <= GlobalValue.monsterDist)
            {
                Monster.transform.position = point;
                Monster.GetComponent<Animator>().SetBool("Move", false);
                Monster.transform.LookAt(mPlayer.transform.position);

                point = Vector3.zero;
                mTrunManager.Reset1 = true;
            }
        }
        else
        {
            skill();
        }
    }
    private void MoveCheckXY4()
    { //0,4
        if (mMonsterList[mX][mY - 1].transform.position != mPlayer.transform.position &&
            mMonsterList[mX + 1][mY].transform.position != mPlayer.transform.position &&
            mMonsterList[mX + 1][mY - 1].transform.position != mPlayer.transform.position
            )
        {
            //print("xy4");
            Move();
            if (Vector3.Distance(pos, point) <= GlobalValue.monsterDist)
            {
                Monster.transform.position = point;
                Monster.GetComponent<Animator>().SetBool("Move", false);
                Monster.transform.LookAt(mPlayer.transform.position);

                point = Vector3.zero;
                mTrunManager.Reset1 = true;
            }
        }
        else
        {
            skill();
        }
    }
    private void MoveCheck()
    {
        if (mMonsterList[mX][mY + 1].transform.position != mPlayer.transform.position &&
            mMonsterList[mX][mY - 1].transform.position != mPlayer.transform.position &&
            mMonsterList[mX - 1][mY - 1].transform.position != mPlayer.transform.position &&
            mMonsterList[mX - 1][mY - 1].transform.position != mPlayer.transform.position &&
            mMonsterList[mX - 1][mY].transform.position != mPlayer.transform.position &&
            mMonsterList[mX + 1][mY].transform.position != mPlayer.transform.position &&
            mMonsterList[mX + 1][mY - 1].transform.position != mPlayer.transform.position &&
            mMonsterList[mX + 1][mY + 1].transform.position != mPlayer.transform.position
            )
        {
            //print("xy2");
            Move();
            if (Vector3.Distance(pos, point) <= GlobalValue.monsterDist)
            {
                Monster.transform.position = point;
                Monster.GetComponent<Animator>().SetBool("Move", false);
                Monster.transform.LookAt(mPlayer.transform.position);

                point = Vector3.zero;
                mTrunManager.Reset1 = true;
            }
        }
        else
        {
            skill();
        }
    }

    #endregion
}
