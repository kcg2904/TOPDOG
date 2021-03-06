using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{

    [Header("매니저 리스트")]
    public TrunManager mtrunManager;
    public UIManager muiManager;
    //public MonsterTrun mMonsterTrun;

    [HideInInspector] public Animator player;
    [HideInInspector] public Animator monster;
    [HideInInspector] public GameObject Player;
    [HideInInspector] public GameObject Monster;



    bool playertrun;
    bool monstertrun;

    int trun;

    int skilla = 0;
    int skillb = 0;
    int skillc = 0;
    int skilld = 0;

    [HideInInspector] public int monstera = 0;
    [HideInInspector] public int monsterb = 0;
    [HideInInspector] public int monsterc = 0;

    bool onSkilla;
    bool onSkillb;
    bool onSkillc;
    bool onSkilld;

    int Dfup = 0;

    int DelayPlayera = 1;
    int DelayPlayerb = 3;
    int DelayPlayerc = 2;
    int DelayPlayerd = 4;

    int DelayMonstera = 1;
    int DelayMonsterb = 3;
    int DelayMonsterc = 4;

    bool playerCheck;

    public bool onMonstera = false;
    public bool onMonsterb = false;
    public bool onMonsterc = false;

    bool skillrich;
    // Start is called before the first frame update
    void Start()
    {
        mtrunManager = GameObject.Find("TrunManager").GetComponent<TrunManager>();
        muiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        player = GameObject.Find("Player").GetComponent<Animator>();
        monster = GameObject.Find("Monster").GetComponent<Animator>();
        Player = GameObject.Find("Player");
        Monster = GameObject.Find("Monster");
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Skill()
    {
        if (onSkilla)
        {
            Skilla(onSkilla);
        }
        if (onSkillb)
        {
            Skillb(onSkillb);
        }
        if (onSkillc)
        {
            Skillc(onSkillc);
        }
        if (onSkilld)
        {
            Skilld(onSkilld);
        }
        if (monstertrun)
        {
            if (onMonstera == true)
            {
                onMonstera = true;
                MonsterSkilla(onMonstera);
            }
            if (onMonsterb)
            {
                MonsterSkillb(onMonsterb);
            }
            if (onMonsterc)
            {
                MonsterSkillc(onMonsterc);
            }
        }
    }
    void SkillSet()
    {
        if (monstera != trun)
        {
            onMonstera = false;
        }
        if (monsterb != trun && monsterb != 0)
        {
            onMonsterb = false;
        }
        if (monsterc != trun)
        {
            onMonsterc = false;
        }

        if (skilla == trun)
        {
            skilla = 0;
            muiManager.btna.interactable = true;
        }
        if (skillb == trun)
        {
            skillb = 0;
            muiManager.btnb.interactable = true;
        }
        if (skillc == trun)
        {
            skillc = 0;
            muiManager.btnc.interactable = true;
        }
        if (skilld == trun)
        {
            skilld = 0;
            muiManager.btnd.interactable = true;
        }
        if(Dfup == trun)
        {
            Dfup = 0;
            muiManager.Defanse.GetComponent<Image>().fillAmount = 0;
            muiManager.playerDF -= 2;
        }



    }
    void Setting()
    {
        //스킬입력 확인
        onSkilla = muiManager.onSkilla;
        onSkillb = muiManager.onSkillb;
        onSkillc = muiManager.onSkillc;
        onSkilld = muiManager.onSkilld;
        //턴상태
        playertrun = mtrunManager.playertrun;
        monstertrun = mtrunManager.monstertrun;
        //현재 턴을 받아옴
        trun = mtrunManager.trun;
        

    }
    public void Skilla(bool _onSkilla)
    {
        if (_onSkilla)
        {
            if (playertrun)
            {
    
                player.SetTrigger("Attack1");
                if (skillrich)
                {
                    if ((muiManager.playerAD - muiManager.monsterDF) > 0)
                    {

                        monster.SetTrigger("GetHit");
                        muiManager.monsterHP -= (muiManager.playerAD - muiManager.monsterDF);
                        monster.SetTrigger("Idle");
                    }
                }
                player.SetTrigger("Idle_Battle");
                muiManager.btna.interactable = false;
                mtrunManager.Reset2 = true;
                muiManager.onSkilla = false;

                if (skilla == 0)
                {
                    skilla = trun + DelayPlayera;
                }
            }
        }
    }
    public void Skillb(bool _onSkillb)
    {
        if (_onSkillb)
        {
            if (playertrun)
            {

                player.SetTrigger("Attack2-1");
                player.SetTrigger("Attack2-2");
                if (skillrich)
                {
                    if ((muiManager.playerAD - muiManager.monsterDF) > 0)
                    {
                        monster.SetTrigger("GetHit");
                        muiManager.monsterHP -= (muiManager.playerAD * 2 - muiManager.monsterDF);
                        monster.SetTrigger("Idle");
                    }
                }
                muiManager.btnb.interactable = false;
                mtrunManager.Reset2 = true;
                muiManager.onSkillb = false;

                if (skillb == 0)
                {
                    skillb = trun + DelayPlayerb;
                }
                player.SetTrigger("Idle_Battle");
            }
        }
    }
    public void Skillc(bool _onSkillc)
    {
        if (_onSkillc)
        {
            if (playertrun)
            {

                player.SetTrigger("Attack1");
                if (skillrich)
                {
                    if ((muiManager.playerAD - muiManager.monsterDF) > 0)
                    {
                        monster.SetTrigger("GetHit");
                        muiManager.monsterHP -= (int)(muiManager.playerAD * 1.5 - muiManager.monsterDF);
                        monster.SetTrigger("Idle");
                    }
                }
                muiManager.btnc.interactable = false;
                mtrunManager.Reset2 = true;
                muiManager.onSkillc = false;

                if (skillc == 0)
                {
                    skillc = trun + DelayPlayerc;
                }
                player.SetTrigger("Idle_Battle");
            }
        }
    }
    public void Skilld(bool _onSkilld)
    {
        if (_onSkilld)
        {
            if (playertrun)
            {
                player.SetTrigger("Defend");
                
                muiManager.btnd.interactable = false;
                muiManager.playerDF += 2;

                mtrunManager.Reset2 = true;
                muiManager.onSkilld = false;
                
                if (skilld == 0)
                {
                
                    skilld = trun + DelayPlayerd;
                }
                if (Dfup == 0)
                {
                    Dfup = trun + 3;
                }
                player.SetTrigger("Idle_Battle");
            }
        }
    }
    public void MonsterSkilla(bool _onMonstera)
    {
        if (_onMonstera)
        {
            monster.SetTrigger("Attack01");
            mtrunManager.Reset1 = true;
            if (skillrich)
            {
                if ((muiManager.monsterAD - muiManager.playerDF) > 0)
                {
                    player.SetTrigger("GetHit");
                    muiManager.playerHP -= (int)(muiManager.monsterAD - muiManager.playerDF);
                    player.SetTrigger("Idle_Battle");
                }
            }
            monster.SetTrigger("Idle");
            if (monstera == 0)
            {
                monstera = trun + DelayMonstera;
            }
        }
    }
    public void MonsterSkillb(bool _onMonsterb)
    {
        if (_onMonsterb)
        {

            monster.SetTrigger("Attack02");
            if ((muiManager.monsterAD - muiManager.playerDF) > 0)
            {
                player.SetTrigger("GetHit");
                muiManager.playerHP -= (int)(muiManager.monsterAD * 2 - muiManager.playerDF);
                player.SetTrigger("Idle_Battle");
            }
            monster.SetTrigger("Idle");
            if (monsterb == 0)
            {
                monsterb = trun + DelayMonsterb;
            }
            mtrunManager.Reset1 = true;
        }
    }
    public void MonsterSkillc(bool _onMonsterc)
    {
        if (_onMonsterc)
        {
            monster.SetTrigger("Defend");
            //체력회복 스킬
            muiManager.monsterHP += 15;
            monster.SetTrigger("Idle");
            if (monsterc == 0)
            {
                monsterc = trun + DelayMonsterc;
            }
            mtrunManager.Reset1 = true;
        }
    }

}
