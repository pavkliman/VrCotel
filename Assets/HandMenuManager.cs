using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject panel;
    //private Button button;
    private TextMeshProUGUI textInfoModel;
    bool currentState = false;

    private void Start()
    {
        panel = GameObject.Find("Panel Info");
        textInfoModel = GameObject.Find("TextInfoModel").GetComponent<TextMeshProUGUI>();
        //button = GameObject.Find("Button Info").GetComponent<Button>();
        panel.SetActive(false);
        
        
        bool currentState = panel.activeSelf;
    }

    private void Update()
    {
        if(textInfoModel.text != "")
        {
            
            panel.SetActive(true);
            //Debug.Log(textInfoModel.text);
        }
        else if (textInfoModel.text == "")
        {
            //Debug.Log(textInfoModel.text);
            panel.SetActive(false);
        }
    }

    public void ToggleActiveState()
    {
        /*if (panel != null)
        {
            bool currentState = panel.activeSelf;
            panel.SetActive(!currentState);
        }*/
    }
}
