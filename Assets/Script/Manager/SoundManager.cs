using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager m_instance;

    [Header("Volume")]
    float masterVolumeBGM = 0.2f;
    float masterVolumeSFX = 0.2f;

    public AudioSource m_BGM_Player;
    public AudioSource m_SFX_Player;

    [Header("AudioClip")]
    public AudioClip[] m_BGMClip;
    public AudioClip[] m_PlyaerSFXClip;
    public AudioClip[] m_MonsterSFXClip;

    Dictionary<string, AudioClip> m_AudioClip_Dic;

    string mBfScneName = null;
    string _BGMClipName = null;
    string mSeneName = null;

    #region 싱글톤 Awake

    private void Awake()
    {
        print("awake");
        if (m_instance != null )
        {
            print("awake1");
        }
        else
        {
            print("awake2");
            m_instance = this;
            DontDestroyOnLoad(this);
            AwakeAfter(); //오브젝트 지정, Setup
            SceneManager.sceneLoaded += OnSceneLoaded; //씬 로드될때마다 델리게이트 체인으로 메서드 참조
        }
    }

    #endregion

    void AwakeAfter()
    {
        m_SFX_Player = GameObject.Find("AudioSound").GetComponent<AudioSource>();
        m_AudioClip_Dic = new Dictionary<string, AudioClip>();

        SetupBGM();
        SetupPlayerSFX();
        SetupMonsterSFX();

    }
    //딕셔너리 추가
    #region Setup
    //배경음
    void SetupBGM()
    {
        print("BGMSetup");
        m_BGM_Player = GameObject.Find("BGM").GetComponent<AudioSource>();
        foreach (AudioClip a in m_BGMClip)
        {
            m_AudioClip_Dic.Add(a.name, a);
        }

        m_BGM_Player.volume = masterVolumeBGM;

    }
     
    //효과음
    void SetupPlayerSFX()
    {
        foreach (AudioClip a in m_PlyaerSFXClip)
        {
            m_AudioClip_Dic.Add(a.name, a);
        }
    }

    void SetupMonsterSFX()
    {
        foreach (AudioClip a in m_MonsterSFXClip)
        {
            m_AudioClip_Dic.Add(a.name, a);
        }
    }
    #endregion


    //재생
    #region 재생
    //BGM
    private void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        print(_scene.name);
        print(_mode);
        switch (_scene.name)
        {
            case "GameScene":
                _BGMClipName = "Sound_BGM_Fight";
                break;
            case "MainScene":
                _BGMClipName = "Sound_BGM_Ready";
                break;
            case "TitleScene":
                _BGMClipName = "Sound_BGM_Relaxing";
                break;
            
        }
        if (m_AudioClip_Dic.ContainsKey(_BGMClipName) == false)
            return;
        StopBGM();
        m_BGM_Player.clip = m_AudioClip_Dic[_BGMClipName];
        m_BGM_Player.loop = true;
        m_BGM_Player.Play();
    }

    //SFX
    public void PlaySFXAudio(string _SFXClipName)
    {
        if (m_AudioClip_Dic.ContainsKey(_SFXClipName) == false)
            return;
        m_SFX_Player.PlayOneShot(m_AudioClip_Dic[_SFXClipName], masterVolumeSFX * 10f);

    }
    public void PlaySFXAudio(string _SFXClipName, string _from)
    {
        if (m_AudioClip_Dic.ContainsKey(_SFXClipName) == false)
            return;
        if (_from == "monster1")
        {
            m_SFX_Player.PlayOneShot(m_AudioClip_Dic[_SFXClipName], masterVolumeSFX*10f);
        }
    }
    #endregion
    

    void Start()
    {
        print("strat");
        //mBfScneName = SceneManager.GetActiveScene().name;

        if (m_BGM_Player != null)
            m_BGM_Player.Play();
    }

    //Stop
    #region 정지

    public void StopBGM()
    {
        m_BGM_Player.Stop();
    }

    public void StopSFX()
    {
        m_SFX_Player.Stop();
    }

    #endregion


    //Volume
    #region 음량
    public void SetVloumeSFX(float _volume)
    {
        masterVolumeSFX = _volume;
    }

    public void SetVloumeBGM(float _volume)
    {
        masterVolumeBGM = _volume;
        m_BGM_Player.volume = masterVolumeBGM;
    }
    #endregion

}

