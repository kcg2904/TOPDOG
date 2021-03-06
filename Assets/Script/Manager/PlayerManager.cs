using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour
{
    [Header("CAMERA")]
    public Camera cam;

    [Header("PLAYER")]
    public GameObject mPlayer;

    [Header("MONSTER")]
    public GameObject mMonster;
    
    [Header("ARROW")]
    public GameObject mArrow;

    [Header("MANAGER")]
    public TrunManager mTrunManager;
    public UIManager mUIManager;
    public FloorManager mFloorManager;
    public PopupManager mPopmanager;
    
    [HideInInspector] public int mX;
    [HideInInspector] public int mY;

    //최초 선택시 확인
    bool firstClick = false;

    // 레이저
    RaycastHit hit;

    //이동시 남은 거리
    float distance;

    //현재 턴
    int mTrun;

    //딜레이 끝나는 턴 저장
    [HideInInspector] public int SkillTurn_A = 0;
    [HideInInspector] public int SkillTurn_B = 0;
    [HideInInspector] public int SkillTurn_C = 0;
    [HideInInspector] public int SkillTurn_D = 0;
    [HideInInspector] public int Defanse_ON = 0;

    // 각 스킬마다 주는 딜레이 정의
    int SkillDelay_A = 1;
    int SkillDelay_B = 3;
    int SkillDelay_C = 2;
    int SkillDelay_D = 4;

    [HideInInspector] public bool mMove_OK = true;

    private GameObject firstobj;

    void Awake()
    {
        mPlayer = GameObject.Find("Player");

    }

    void Update()
    {
        Get_Update();
        Player_Trun(mTrunManager.playertrun);
    }


    // 지속적으로 변화된 데이터를 가져옴
    #region Get Update data


    public void Get_Update()
    {
        mMonster = GameObject.Find("Monster");
        Skill_DelaySet();
        Get_PlayerOnFloor();
        mTrun = mTrunManager.trun;
    }

    public void Get_PlayerOnFloor()
    {
        for (int i = 0; i < mFloorManager.mMax_X; i++)
        {
            for (int j = 0; j < mFloorManager.mMax_Y; j++)
            {
                if (mFloorManager.mFloorList[i][j].transform.position == mPlayer.transform.position)
                {
                    mX = mFloorManager.mFloorList[i][j].GetComponent<MoveFloor>().mX;
                    mY = mFloorManager.mFloorList[i][j].GetComponent<MoveFloor>().mY;
                }
            }
        }
    }

    #endregion

    // 플레이어 턴일 경우 이벤트 받을수 있게
    #region Trun

    public void Player_Trun(bool _trun)
    {
        //플레이어 턴일때
        if (_trun)
        {
            //이동 선택시
            if (mMove_OK == true)
            {
                mFloorManager.Enable_Collider();
                mMove_OK = false;
            }

            Player_Move_Input();

            //스킬A 선택시
            if (mUIManager.onSkilla == true)
            {
                mFloorManager.Defult_To_MoveFloor();
                Skill_A();
            }
            //스킬B 선택시
            else if (mUIManager.onSkillb == true)
            {
                mFloorManager.Defult_To_MoveFloor();
                Skill_B();
            }
            //스킬C 선택시
            else if (mUIManager.onSkillc == true)
            {
                mFloorManager.Defult_To_MoveFloor();
                Skill_C();
            }
            //스킬D 선택시
            else if (mUIManager.onSkilld == true)
            {
                mFloorManager.Defult_To_MoveFloor();
                Skill_D();
            }
        }
    }
    #endregion

    // 이동을 위한 input, ray, action 처리
    #region Move input, ray, action

    #region Input

    public void Player_Move_Input()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray_Click();
        }

    }

    #endregion


    #region Ray

    public void Ray_Click()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
       
            if (hit.collider.tag == "MoveFloor")
            {

                //print("tag = moveFloor");
                if (!firstClick)
                {
                    mArrow.transform.position = new Vector3(hit.collider.transform.position.x, 0
                        , hit.transform.position.z);
                    firstobj = hit.collider.gameObject;
                    firstClick = true;
                }
                else if (firstClick)
                {
                    if (hit.collider.name == firstobj.name)
                    {
                        mMove_OK = false;
                        mFloorManager.Defult_To_MoveFloor();
                        mArrow.transform.position = new Vector3(0, 2.1f, -10.62f);
                        Player_Move(hit.transform.position);
                        firstClick = false;
                    }
                    else
                    {
                        mArrow.transform.position = new Vector3(0, 2.1f, -10.62f);
                        firstClick = false;
                    }
                }
            }

        
    }
    #endregion

    //이동
    #region Move

    public void Player_Move(Vector3 _pos)
    {
        

        if (mPlayer_MoveCoroutine != null)
        {
            StopCoroutine(mPlayer_MoveCoroutine);
            mPlayer_MoveCoroutine = null;
        }
        mPlayer_MoveCoroutine = StartCoroutine(@Player_MoveCoroutine(_pos));
    }

    Coroutine mPlayer_MoveCoroutine = null;

    IEnumerator @Player_MoveCoroutine(Vector3 _pos)
    {
        mFloorManager.Defult_To_MoveFloor();

        SoundManager.m_instance.PlaySFXAudio("Sound_Player_Walk");
        

        bool b = true;
        while(b)
        {
            distance = (_pos - mPlayer.transform.position).magnitude;
            
            yield return null;
            
            if (distance > 0.1f)
            {
                //Look At
                mPlayer.transform.LookAt(_pos);

                //Animator
                mPlayer.GetComponent<Animator>().SetBool("Move", true);

                //Translate
                //mPlayer.transform.position = Vector3.Lerp(mPlayer.transform.position, _pos, Time.deltaTime);
                mPlayer.transform.Translate(Vector3.forward * GlobalValue.playerSpeed * Time.deltaTime);

            }

            if (distance <= 0.1f)
            {

                mTrunManager.Reset2 = true;
                
                SoundManager.m_instance.StopSFX();
                //Animator
                mPlayer.GetComponent<Animator>().SetBool("Move", false);
                mPlayer.transform.position = _pos;
                b = false;
            }

            yield return new WaitForFixedUpdate();

        }
    }

    #endregion

    #endregion

    //스킬 딜레이, 행동, 데미지
    #region Skill 

    //스킬 딜레이
    #region Delay

    public void Skill_DelaySet()
    {
        if(SkillTurn_A == mTrun)
        {
            SkillTurn_A = 0;
        }
        if(SkillTurn_B == mTrun)
        {
            SkillTurn_B = 0;
        }
        if(SkillTurn_C == mTrun)
        {
            SkillTurn_C = 0;
        }
        if(SkillTurn_D == mTrun)
        {
            SkillTurn_D = 0;
        }
        if(Defanse_ON == mTrun)
        {
            Defanse_ON = 0;
            mUIManager.Defanse_fill_Control(Defanse_ON);
        }
    }
    #endregion

    //스킬 액션 A, B, C, D, +  데미지
    #region Skill Action

    public void Skill_A()
    {
        mPlayer.transform.LookAt(mMonster.transform.localPosition);
        mPlayer.GetComponent<Animator>().SetTrigger("Attack1");
        print("skill_a");

        if(SoundManager.m_instance.m_SFX_Player.isPlaying == false)
        {
            SoundManager.m_instance.PlaySFXAudio("Sound_Player_Hit_Damage");
        }
        if ((mUIManager.playerAD - mUIManager.monsterDF) > 0)
        {
            //monster Animator
            //mHit.GetComponent<Hit>().OnEnable();
            mMonster.GetComponent<Animator>().SetTrigger("GetHit");
            mUIManager.Damage_To_Monster(mUIManager.playerAD - mUIManager.monsterDF);
            mMonster.GetComponent<Animator>().SetTrigger("Idle");
        }

        mPlayer.GetComponent<Animator>().SetTrigger("Idle_Battle");

        //
        mUIManager.onSkilla = false;

        //next
        mTrunManager.Reset2 = true;

        if (SkillTurn_A == 0)
        {
            SkillTurn_A = mTrun + SkillDelay_A;
        }
    }

    public void Skill_B()
    {
        mPlayer.transform.LookAt(mMonster.transform.localPosition);
        
        mPlayer.GetComponent<Animator>().SetTrigger("Attack2-1");
            mPlayer.GetComponent<Animator>().SetTrigger("Attack2-2");
        StartCoroutine(@SkillBSoundDelaye());
        if ((mUIManager.playerAD - mUIManager.monsterDF) > 0)
        {
            mMonster.GetComponent<Animator>().SetTrigger("GetHit");
            mUIManager.Damage_To_Monster(mUIManager.playerAD * 2 - mUIManager.monsterDF);
            mMonster.GetComponent<Animator>().SetTrigger("Idle");
        }

        mPlayer.GetComponent<Animator>().SetTrigger("Idle_Battle");
        
        mUIManager.onSkillb = false;

        mTrunManager.Reset2 = true;
        

        
        if (SkillTurn_B == 0)
        {

            SkillTurn_B = mTrun + SkillDelay_B;
        }
    }

    IEnumerator @SkillBSoundDelaye()
    {

        SoundManager.m_instance.PlaySFXAudio("Sound_Player_Swish");
        yield return new WaitForSeconds(0.3f);
        SoundManager.m_instance.PlaySFXAudio("Sound_Player_Hit_Damage");
        StopCoroutine(@SkillBSoundDelaye());
    }

    public void Skill_C()
    {
        mPlayer.transform.LookAt(mMonster.transform.localPosition);
        mPlayer.GetComponent<Animator>().SetTrigger("Attack1");


        SoundManager.m_instance.PlaySFXAudio("Sound_Player_Hit_Big_Damage");
        if ((mUIManager.playerAD - mUIManager.monsterDF) > 0)
        {
            
            mMonster.GetComponent<Animator>().SetTrigger("GetHit");
            mUIManager.Damage_To_Monster((int)(mUIManager.playerAD * 1.5 - mUIManager.monsterDF));
            mMonster.GetComponent<Animator>().SetTrigger("Idle");
        }
        mUIManager.onSkillc = false;

        mTrunManager.Reset2 = true;


        if (SkillTurn_C == 0)
        {
            SkillTurn_C = mTrun + SkillDelay_C;
        }
        mPlayer.GetComponent<Animator>().SetTrigger("Idle_Battle");
    }

    public void Skill_D()
    {
        mPlayer.transform.LookAt(mMonster.transform.localPosition);
        SoundManager.m_instance.PlaySFXAudio("Sound_Player_Shield");

        mPlayer.GetComponent<Animator>().SetTrigger("Defend");

        mUIManager.playerDF += 2;

        mPlayer.GetComponent<Animator>().SetTrigger("Idle_Battle");
        
        mUIManager.onSkilld = false;

        mTrunManager.Reset2 = true;


        Defanse_ON = mTrun + 3;
        mUIManager.Defanse_fill_Control(Defanse_ON);

        if (SkillTurn_D == 0)
        {
            SkillTurn_D = mTrun + SkillDelay_D;
        }
    }
    #endregion

    #endregion







}
