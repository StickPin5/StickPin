using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerHelper : MonoBehaviour {
    public float Timer = 5;
    public Text textShowTime;
    public bool isContinue = true;
    public static TimerHelper m_instance;
	// Use this for initialization
	void Start () {
        m_instance = this;
	}
    public void StartTimer()
    {
        StartCoroutine(TimerCountDown());
    }
    public IEnumerator TimerCountDown()
    {
        while (Timer > 0 && isContinue)
        {
            yield return new WaitForSeconds(1);
            textShowTime.text = Timer.ToString() ;
            Timer--;
            if (Timer <= 0 )
            {
                textShowTime.text = "0";
                GameObject RootPanel = GameObject.Instantiate(Resources.Load("Prefabs/FailurePanel")) as GameObject;
                RootPanel.transform.parent = GameObject.FindGameObjectWithTag("Canvas").transform;
                GameObject.Find("GameManager").GetComponent<GameManager>().enabled = false;
            }
        }
       
    }
	// Update is called once per frame
	void Update () {
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
