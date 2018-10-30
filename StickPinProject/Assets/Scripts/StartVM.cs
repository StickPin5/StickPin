using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartVM : MonoBehaviour {
    public Button StartBtn;
    private GameObject gameManager;
    // Use this for initialization
    void Start () {
        //this.gameObject.SetActive(true);
        gameManager = GameObject.Find("GameManager");
        StartBtn.onClick.AddListener(OnClickStartLevelMode);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnClickStartLevelMode()
    {
        Debug.LogError("OnClickStart");
        gameManager.GetComponent<GameManager>().enabled = true;
        this.gameObject.SetActive(false);
        TimerHelper.m_instance.StartTimer();
    }
}
