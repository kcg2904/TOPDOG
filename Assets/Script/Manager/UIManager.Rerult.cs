using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class UIManager : MonoBehaviour
{
    
    public void setScore(int _value)
    {
        mExp = mTrunManager.GetPlayerExp();
        mTrunManager.SetPlayerExp(mExp + _value);
    }
    public void onScore()
    {
        if (playerHP > 0)
        {
            setScore(1);
        }
        SceneManager.LoadScene("MainScene");
    }
    public void setResult()
    {
        if (playerHP <= 0)
        {
            ResultPanel.SetActive(true);
            Gameresult.text = "던전 클리어 실패!!";
            Drop.text = "던전 클리어 실패로 " + "\n" +
                "아무것도 얻지 못하였습니다.";
        }
        else if(monsterHP <= 0)
        {
            ResultPanel.SetActive(true);
            Gameresult.text = "던전 클리어 성공!!";
            Drop.text = "던전클리어 성공으로"  + "\n" +
                "경험치 1을 획득 하셨습니다";
        }
    }

}
