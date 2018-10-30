using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailurePanelVM : MonoBehaviour {
    public Text textShow;
    public Button logoutBtn;
    public Button shareBtn;
	// Use this for initialization
	void Start () {
        logoutBtn.onClick.AddListener(OnClickLogoutBtn);
        shareBtn.onClick.AddListener(OnClickShareBtn);
	}
    void OnClickLogoutBtn()
    {
        Debug.LogError("LogOut!!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnClickShareBtn()
    {
        Debug.LogError("Share!!!");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
