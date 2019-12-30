using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainPanel : MonoBehaviour {

    // Use this for initialization
    private Button BtntoRecordUI;
    public GameObject myUIrecordUI;
    // Use this for initialization
    void Start()
    {
        BtntoRecordUI = this.transform.Find("BtntoRecordUI").GetComponent<Button>();
        BtntoRecordUI.onClick.AddListener(showRecordUI);
    }

    private void showRecordUI()
    {
        myUIrecordUI.SetActive(true);
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
