using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleScript : MonoBehaviour
{
    public TextMeshProUGUI _titleText;


    // Start is called before the first frame update
    void Start()
    {
        //this._titleText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTitle(string title)
    {

        this._titleText.text = "" + title;
    }
}
