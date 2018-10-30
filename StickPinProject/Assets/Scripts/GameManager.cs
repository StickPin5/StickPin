using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour {

    private Transform startPoint;
    private Transform spawnPoint;
    private Pin currentPin;
    private bool isGameOver = false;
    private bool canClick = true;
    // 引入关卡信息；
    private List<string> levelName;
    private List<float> rotateSpeed;
    private List<int> pinNum;
    private Camera mainCamera;

    public Text scoreText;
    public Text LevelMesh;
    public GameObject Circle;
    public GameObject pinPrefab;
    public float speed = 3;
    // 当前所处的关卡；
    public int nowLevelIndex = 0;
    public static GameManager _instance;
	// Use this for initialization
	void Start () {
        _instance = this;
        startPoint = GameObject.Find("StartPoint").transform;
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        mainCamera = Camera.main;
        // 初始化
        LevelMesh.gameObject.SetActive(false);
        LevelHelper level = gameObject.GetComponent<LevelHelper>();
        levelName = level.m_stringParams1.ToList<string>();
        rotateSpeed = level.m_floatParams1.ToList<float>();
        pinNum = level.m_intParams1.ToList<int>();

        SpawnPin();
	}

    private void Update()
    {
        if (isGameOver) return;
        if (Input.GetMouseButtonDown(0) && canClick)
        {
            pinNum[nowLevelIndex]--;
            if (pinNum[nowLevelIndex] <= 0)
            {
                canClick = false;
                LevelMesh.gameObject.SetActive(true);
                if (!isGameOver)
                    LevelMesh.text = "恭喜过关！1秒后进入下一关";
                StartCoroutine(GameContinue());
            }
            scoreText.text = levelName[nowLevelIndex].ToString();
            currentPin.StartFly();
            SpawnPin();
        }
    }

    void SpawnPin()
    {
        if (levelName[nowLevelIndex] == null)
        {
            Debug.LogError("关卡不存在");
        }
        else
        {
            currentPin = GameObject.Instantiate(pinPrefab, spawnPoint.position, pinPrefab.transform.rotation).GetComponent<Pin>();
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;
        Circle.GetComponent<RotateSelf>().IsRotate = false;
        LevelMesh.text = "闯关失败！！！";
        TimerHelper.m_instance.isContinue = false;
        StartCoroutine(GameOverAnimation());
        isGameOver = true;
    }
    public void GameNext()
    {
        nowLevelIndex++;
        if (nowLevelIndex >= 3 && !isGameOver)
        {
            LevelMesh.text = "通关啦！";
            TimerHelper.m_instance.isContinue = false;
            Circle.GetComponent<RotateSelf>().IsRotate = false;
            return;
        }
        canClick = true;
        scoreText.text = levelName[nowLevelIndex].ToString();
        LevelMesh.gameObject.SetActive(false);
        if (Circle != null)
        {
            if (Circle.transform.childCount != 0 && !isGameOver)
            {
                for (int i = 0; i < Circle.transform.childCount; i++)
                {
                    Destroy(Circle.transform.GetChild(i).gameObject);
                }
            }
        }
    }
    IEnumerator GameContinue()
    {
        yield return new WaitForSeconds(1);
        GameNext();
    }
    IEnumerator GameOverAnimation()
    {
        while (true)
        {
            mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, Color.red, speed * Time.deltaTime);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 4, speed * Time.deltaTime);
            if( Mathf.Abs( mainCamera.orthographicSize-4 )<0.01f)
            {
                break;
            }
            yield return 0;
        }
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
