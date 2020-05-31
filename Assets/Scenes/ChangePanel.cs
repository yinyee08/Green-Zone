using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePanel : MonoBehaviour
{
    public Button easyBtn;
    public Button mediumBtn;
    public Button hardBtn;

    public GameObject easyPanel;
    public GameObject mediumPanel;
    public GameObject hardPanel;

    // Start is called before the first frame update
    void Start()
    {
        easyBtn = GameObject.Find("Easy_Button").GetComponent<Button>();
        mediumBtn = GameObject.Find("Medium_Button").GetComponent<Button>();
        hardBtn = GameObject.Find("Hard_Button").GetComponent<Button>();
    }

    // Update is called once per frame
    public void getEasyPanel()
    {
        if(easyBtn.CompareTag("Easy"))
        {
            easyPanel.SetActive(true);
            mediumPanel.SetActive(false);
            hardPanel.SetActive(false);
        }

    }

    public void getMediumPanel()
    {
        if (mediumBtn.CompareTag("Medium"))
        {
            easyPanel.SetActive(false);
            mediumPanel.SetActive(true);
            hardPanel.SetActive(false);
        }
    }

    public void getHardPanel()
    {
        if (hardBtn.CompareTag("Hard"))
        {
            easyPanel.SetActive(false);
            mediumPanel.SetActive(false);
            hardPanel.SetActive(true);
        }
    }


}
