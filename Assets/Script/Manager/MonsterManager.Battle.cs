using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterManager : MonoBehaviour
{
    public void skill()
    {

        if (mHp > 70)
        {
            MonsterSkilla(DelayA);
        }
        else if (mHp > 30 && mHp <= 70)
        {
            if (DelayB == 0)
            {
                MonsterSkillb(DelayB);
            }
            else
            {
                MonsterSkilla(DelayA);
            }
        }else if(mHp <= 30)
        {
            if(DelayC == 0)
            {
                MonsterSkillc(DelayC);
            }
            else
            {
                if (DelayB == 0)
                {
                    MonsterSkillb(DelayB);
                }
                else
                {
                    MonsterSkilla(DelayA);
                }
            }
        }
    }
    #region 스킬 설정
    public void MonsterSkilla(int _DelayA)
    {
        if (_DelayA == 0)
        {
            Monster.transform.LookAt(mPlayer.transform.position);
            Monster.GetComponent<Animator>().SetTrigger("Attack01");
            SoundManager.m_instance.PlaySFXAudio("Sound_Slime_Small_Hit", "monster1");
            
            if ((mUimanager.monsterAD - mUimanager.playerDF) > 0)
            {
                
                //Destroy(Hit);
                mPlayer.GetComponent<Animator>().SetTrigger("GetHit");

                mUimanager.playerHP -= (int)(mUimanager.monsterAD - mUimanager.playerDF);
                mPlayer.GetComponent<Animator>().SetTrigger("Idle_Battle");
            }
            Monster.GetComponent<Animator>().SetTrigger("Idle");
            DelayA = 1;
            mTrunManager.Reset1 = true;
        }

    }
    public void MonsterSkillb(int _DelayB)
    {
        if (_DelayB == 0)
        {
            Monster.transform.LookAt(mPlayer.transform.position);
            Monster.GetComponent<Animator>().SetTrigger("Attack02");
            SoundManager.m_instance.PlaySFXAudio("Sound_Slime_Hit", "monster1");
            if ((mUimanager.monsterAD - mUimanager.playerDF) > 0)
            {
                mPlayer.GetComponent<Animator>().SetTrigger("GetHit");
                mUimanager.playerHP -= (int)(mUimanager.monsterAD * 2 - mUimanager.playerDF);
                mPlayer.GetComponent<Animator>().SetTrigger("Idle_Battle");
            }
            Monster.GetComponent<Animator>().SetTrigger("Idle");
            DelayB = 3;
            mTrunManager.Reset1 = true;
        }
    }
    public void MonsterSkillc(int _DelayC)
    {
        if (_DelayC == 0)
        {
            Monster.transform.LookAt(mPlayer.transform.position);
            Monster.GetComponent<Animator>().SetTrigger("Defend");
            mUimanager.monsterHP += 15;
            Monster.GetComponent<Animator>().SetTrigger("Idle");
            DelayC = 4;
            mTrunManager.Reset1 = true;
        }
    }
    #endregion
}
