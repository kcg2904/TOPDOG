using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public partial class UIManager : MonoBehaviour
{
    [Header("매니저 리스트")]
    public TrunManager mTrunManager;
    public SkillManager mSkillManager;
    public PlayerManager mPlayerManager;
    public FloorManager mFloorManager;

    [Header("턴 UI")]
    public Text TrunText;

    //상태창
    Text PlayerHp;
    Image Hpbar;

    [HideInInspector] public Text MonsterHp;
    [HideInInspector] public Text MonsterAd;
    [HideInInspector] public Text MonsterDefense;
    [HideInInspector] public Text Gameresult;
    [HideInInspector] public Text Drop;
    //버튼
    [HideInInspector] public Button btna;
    [HideInInspector] public Button btnb;
    [HideInInspector] public Button btnc;
    [HideInInspector] public Button btnd;
    [HideInInspector] public Button btnm;
    [HideInInspector] public GameObject RewardAdsBtn;


    [HideInInspector] public bool mvok = false;
    //스킬 입력확인
    [HideInInspector] public bool onSkilla = false;
    [HideInInspector] public bool onSkillb = false;
    [HideInInspector] public bool onSkillc = false;
    [HideInInspector] public bool onSkilld = false;


    [HideInInspector] public int playerHP;
    [HideInInspector] public int playerAD;
    [HideInInspector] public int playerDF;
    [HideInInspector] public int monsterHP;
    [HideInInspector] public int monsterAD;
    [HideInInspector] public int monsterDF;
    [HideInInspector] public int mExp;
    public GameObject Defanse;
    public GameObject ResultPanel;

    [Header("MAIN_SCENE")]
    //캐릭터 정보
    public Image mExpbar;
    public Text mPlayerLv;
    public Text mPlayerHp;
    public Text mPlayerAd;
    public Text mPlayerDf;
    public Text mPlayerExp;
    [Header("")]
    //던전정보
    public GameObject mSelect;
    public GameObject mDeSelect;
    public GameObject mGameStartbtn;

    public Text mDungeonHP;
    public Text mDungeonAD;
    public Text mDungeonDF;
    public Text mDungeonReword;

    public Text mDeSelectText;
    //리워드 확인
    [HideInInspector] public bool Reward;

    //레벨 확인
    [HideInInspector] public int Lv = 1;

    //경험치 저장
    int Exp = 0;
    //버튼 확인
    int mbtnname = 0;

    //턴 확인
    int trun;
    // Start is called before the first frame update
    void Start()
    {
        StartSetting();

    }
    public void StartSetting()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            mTrunManager = GameObject.Find("TrunManager").GetComponent<TrunManager>();
            mSkillManager = GameObject.Find("SkillManager").GetComponent<SkillManager>();
            mPlayerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();

            btna = GameObject.Find("skilla").GetComponent<Button>();
            btnb = GameObject.Find("skillb").GetComponent<Button>();
            btnc = GameObject.Find("skillc").GetComponent<Button>();
            btnd = GameObject.Find("skilld").GetComponent<Button>();


            TrunText = GameObject.Find("Trunum").GetComponent<Text>();
            PlayerHp = GameObject.Find("Hpnum").GetComponent<Text>();
            Hpbar = GameObject.Find("Hpbar").GetComponent<Image>();
            MonsterHp = GameObject.Find("HPnum").GetComponent<Text>();
            MonsterAd = GameObject.Find("ADnum").GetComponent<Text>();
            MonsterDefense = GameObject.Find("DEnum").GetComponent<Text>();
            Gameresult = GameObject.Find("Gameresult").GetComponent<Text>();
            Drop = GameObject.Find("Droptbl").GetComponent<Text>();
            ResultPanel = GameObject.Find("ResultPanel");
            Defanse = GameObject.Find("Defanse");

            Defanse.GetComponent<Image>().fillAmount = 0;
            ResultPanel.SetActive(false);


            Exp = mTrunManager.GetPlayerExp();

            if (Exp > 100)
            {
                while (true)
                {
                    Exp -= 100;
                    Lv += 1;
                    if (Exp < 100)
                    {
                        break;
                    }
                }
            }
            if(Lv > 5)
            {
                Lv = 5;
            }
            switch (Lv)
            {
                case 1:
                    playerHP = 100;
                    playerAD = 10;
                    playerDF = 1;
                    break;
                case 2:
                    playerHP = 110;
                    playerAD = 14;
                    playerDF = 3;
                    break;
                case 3:
                    playerHP = 120;
                    playerAD = 18;
                    playerDF = 5;
                    break;
                case 4:
                    playerHP = 130;
                    playerAD = 22;
                    playerDF = 7;
                    break;
                case 5:
                    playerHP = 150;
                    playerAD = 25;
                    playerDF = 10;
                    break;
            }
            monsterHP = GlobalValue.monsterHP;
            monsterAD = GlobalValue.monsterAD;
            monsterDF = GlobalValue.monsterDF;

            PlayerHp.text = playerHP.ToString();
            MonsterHp.text = monsterHP.ToString();
            MonsterAd.text = monsterAD.ToString();
            MonsterDefense.text = monsterDF.ToString();


        }
        else if (SceneManager.GetActiveScene().name == "MainScene")
        {
            mSelect.SetActive(false);
            mGameStartbtn.SetActive(false);

            Exp = mTrunManager.GetPlayerExp();



            if (Exp > 100)
            {
                while (true)
                {
                    Exp -= 100;
                    Lv += 1;
                    if (Exp < 100)
                    {

                        break;
                    }
                }
            }
            if (Lv > 5)
            {
                Lv = 5;
            }
            switch (Lv)
            {
                case 1:
                    playerHP = 100;
                    playerAD = 10;
                    playerDF = 1;
                    break;
                case 2:
                    playerHP = 110;
                    playerAD = 14;
                    playerDF = 3;
                    break;
                case 3:
                    playerHP = 120;
                    playerAD = 18;
                    playerDF = 5;
                    break;
                case 4:
                    playerHP = 130;
                    playerAD = 22;
                    playerDF = 7;
                    break;
                case 5:
                    playerHP = 150;
                    playerAD = 25;
                    playerDF = 10;
                    break;
            }
            mPlayerLv.text = Lv.ToString();
            mPlayerExp.text = Exp.ToString() + "%";
            mExpbar.fillAmount = (float)Exp/100;
            mPlayerAd.text = playerAD.ToString();
            mPlayerDf.text = playerDF.ToString();
            mPlayerHp.text = playerHP.ToString();
        }
    }


    // Update is called once per frame
    void Update()
    {

        Setting();
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            if (mFloorManager.mIn_Monster)
            {
                Button_Interactable_Control();
            }
            else
            {
                Interactable_All_False();
            }
        }
    }

    void Setting()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            trun = mTrunManager.trun;
            TrunText.text = trun.ToString();

            PlayerHp.text = playerHP.ToString();
            MonsterHp.text = monsterHP.ToString();
            MonsterAd.text = monsterAD.ToString();
            MonsterDefense.text = monsterDF.ToString();
            Hpbar.fillAmount = playerHP * 0.01f;

            
            setResult();


        }
        else if (SceneManager.GetActiveScene().name == "MainScene")
        {
            if (mbtnname != 0)
            {
                switch (mbtnname)
                {
                    case 1:
                        mDeSelect.SetActive(false);
                        mSelect.SetActive(true);
                        mDungeonHP.text = GlobalValue.monsterHP.ToString();
                        mDungeonAD.text = GlobalValue.monsterAD.ToString();
                        mDungeonDF.text = GlobalValue.monsterDF.ToString();
                        mDungeonReword.text = "Exp " + GlobalValue.reword.ToString();
                        break;
                    case 2:
                        mDeSelect.SetActive(true);
                        mSelect.SetActive(false);

                        mDeSelectText.text = "추후 업데이트 예정입니다";
                        break;
                }
            }
            if (mDeSelect.active == false)
            {
                mGameStartbtn.SetActive(true);

            }
            else if (mDeSelect.active)
            {
                mGameStartbtn.SetActive(false);
            }
        }
    }

    #region btn event

    public void onbtna()
    {
        onSkilla = true;
    }
    public void onbtnb()
    {
        onSkillb = true;
    }
    public void onbtnc()
    {
        onSkillc = true;
    }
    public void onbtnd()
    {
        onSkilld = true;
    }

    public void Select1()
    {
        mbtnname = 1;
    }
    public void Select2()
    {
        mbtnname = 2;
    }
    public void GameStart()
    {

        switch (mbtnname)
        {
            case 1:
                SceneManager.LoadScene("GameScene");
                break;
        }
    }

    #endregion


    //몬스터 데미지 주는
    #region Damage
    public void Damage_To_Monster(int _damage)
    {
        monsterHP -= _damage;
    }

    #endregion

    //스킬 버튼 활성화 비활성화 제어
    #region button interactable control

    public void Button_Interactable_Control()
    {
        if (mTrunManager.playertrun)
        {
            if (mPlayerManager.SkillTurn_A == 0)
            {
                btna.interactable = true;
            }
            else
                btna.interactable = false;

            if (mPlayerManager.SkillTurn_B == 0)
            {
                btnb.interactable = true;
            }
            else
                btnb.interactable = false;

            if (mPlayerManager.SkillTurn_C == 0)
            {
                btnc.interactable = true;
            }
            else
                btnc.interactable = false;

            if (mPlayerManager.SkillTurn_D == 0)
            {
                btnd.interactable = true;
            }
            else
                btnd.interactable = false;
        }else if (!mTrunManager.playertrun)
        {
            Interactable_All_False();
        }
    }

    public void Interactable_All_False()
    {
        btna.interactable = false;
        btnb.interactable = false;
        btnc.interactable = false;
        btnd.interactable = false;
    }

    #endregion

    //디펜스 Fill Amount 조절
    #region Defanse Image fill Amount Control
    public void Defanse_fill_Control(int _value)
    {
        if (_value == 0)
        {
            Defanse.GetComponent<Image>().fillAmount = 0;
        }
        else
        {
            Defanse.GetComponent<Image>().fillAmount = 1;
        }
    }

    #endregion

}
