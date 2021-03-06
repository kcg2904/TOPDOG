using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunManager : SingTun<TrunManager>
{

    public FloorManager mFloorManager;

    public bool playertrun;
    public bool monstertrun;

    public MonsterManager mMonsterManager;
    public PlayerManager mPlayerManager;


    [HideInInspector] public bool Reset1;
    [HideInInspector] public bool Reset2;


    public int trun;

    public int mExp = 0;

    [HideInInspector] public bool mIn_Monster;

    // Start is called before the first frame update
    void Start()
    {
        SaveDataLoad();
        playertrun = true;
        monstertrun = false;
        trun = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //mIn_Monster = mFloorManager.mIn_Monster;
        if (Reset1)
        {
            StartCoroutine("DelayTime1");
        }
        if (Reset2)
        {
            StartCoroutine("DelayTime2");
        }
    }
    //몬스터의 턴 종료시 실행
    IEnumerator DelayTime1() {
        monstertrun = false;
        yield return new WaitForSeconds(2f);
        Reset1 = false;
        trun += 1;

        //print(mIn_Monster);
        mPlayerManager.mMove_OK = true;
        mFloorManager.mIn_Monster = false;
        playertrun = true;

        if (mMonsterManager.DelayA != 0)
        {
            mMonsterManager.DelayA -= 1;
        }
        if (mMonsterManager.DelayB != 0)
        {
            mMonsterManager.DelayB -= 1;
        }
        if (mMonsterManager.DelayC != 0)
        {
            mMonsterManager.DelayC -= 1;
        }


        StopCoroutine("DelayTime1");
    }
    //플레이어의 턴 종료시 실행
    IEnumerator DelayTime2() {
        playertrun = false;
        

        //GameObject.Find("MoveM").GetComponent<MonsterTrun>().MoveCheck = false;
        yield return new WaitForSeconds(2f);
        GameObject.Find("Player").transform.localRotation = Quaternion.Euler(0, 0, 0);
        monstertrun = true;
        Reset2 = false;
        StopCoroutine("DelayTime2");
    }

    public void SaveDataLoad()
    {
        mExp = GetPlayerExp();
    }

    public void SetPlayerExp(int _mExp)
    {
        mExp = _mExp;
        PlayerPrefs.SetInt(GlobalValue.mExp, _mExp);
    }

    public int GetPlayerExp()
    {
        mExp = PlayerPrefs.GetInt(GlobalValue.mExp);
        return mExp;
    }
}
