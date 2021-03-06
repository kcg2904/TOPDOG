using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PopupManager : MonoBehaviour
{
    [Header("팝업오브젝트")]
    public GameObject mPopup;
    [Header("카메라")]
    public Camera cam;
    bool onPopup = false;
    [Header("UI텍스트")]
    public Text mContents;

    private Canvas mCanvas;
    bool firstClick = false;

    List<RaycastResult> results;
    private GraphicRaycaster mGray;



    void Awake()
    {
        mGray = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();

    }
    // Start is called before the first frame update
    void Start()
    {
        mPopup = GameObject.Find("Popup");
        mContents = GameObject.Find("ContentsPopup").GetComponent<Text>();
        mPopup.SetActive(false);
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        PopupContent();
        GetEscape();

        if (onPopup == true)
        {
            Ray_Click();
        }
    }

    #region 팝업 내용
    public void PopupContent()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            if (!firstClick)
            {
                mContents.text = "메인 화면으로" + "\n" + "돌아가시겠습니까?";
            }
            else if (firstClick)
            {
                mContents.text = "이번 스테이지의 \n 진행 사항은 사라집니다.\n " +
                    "정말 메인으로 \n돌아가시겠습니까?";
            }
        }
        else if (SceneManager.GetActiveScene().name == "MainScene")
        {
            mContents.text = "게임을 종료 \n 하시겟습니까?";
        }
    }
    #endregion
    #region 클릭 이벤트
    public void Ray_Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var Ped = new PointerEventData(null);
            Ped.position = Input.mousePosition;
            results = new List<RaycastResult>();
            mGray.Raycast(Ped, results);

            if (results.Count <= 0) return;
            if (results[0].gameObject.tag == mPopup.tag)
            {
                Time.timeScale = 1;
                mPopup.SetActive(false);
                onPopup = false;
            }
        }

    }

    public void GetEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (!onPopup)
            {

                mPopup.SetActive(true);
                Time.timeScale = 0;
                onPopup = true;
            }
            else
            {
                Time.timeScale = 1;
                mPopup.SetActive(false);
                firstClick = false;
                onPopup = false;
            }
        }
    }

    public void OnCilckOkbtn()
    {

        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            if (firstClick)
            {
                print("종료");
                Time.timeScale = 1;
                SceneManager.LoadSceneAsync("MainScene");


            }
            else
            {
                firstClick = true;

            }
        }
        else if (SceneManager.GetActiveScene().name == "MainScene")
        {
            print("종료");
            Application.Quit();
        }

    }
    public void OnCilckCancelbtn()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            Time.timeScale = 1;
            mPopup.SetActive(false);
            firstClick = false;
            onPopup = false;


        }
        else if (SceneManager.GetActiveScene().name == "MainScene")
        {
            mPopup.SetActive(false);
            onPopup = false;

        }
    }

}
    #endregion
