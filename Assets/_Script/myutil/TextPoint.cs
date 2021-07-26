using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[ExecuteInEditMode]
public class TextPoint : MonoBehaviour {
    public string id;
    public int maxCharacterCount = -1;
    public bool maxcountSizedown = false;
    void Start()
    {
        string str = Lang.Instance.getString(id);
        if (maxCharacterCount>0)
        {
            if (maxcountSizedown)
            {
                for (int i = maxCharacterCount; i < str.Length; i += maxCharacterCount)
                {
                    str = str.Insert(i++, "\n");

                }
                if (GetComponent<TextMesh>())
                {
                    GetComponent<TextMesh>().fontSize = (int)(GetComponent<TextMesh>().fontSize * 0.6f);
                }
            }
            else
            {
                for (int i = maxCharacterCount; i < str.Length; i += maxCharacterCount)
                {
                    str = str.Insert(i++, "\n");

                }
            }
        }
        if(GetComponent<Text>())
            GetComponent<Text>().text = str;
        if (GetComponent<TextMesh>())
        {
            GetComponent<TextMesh>().text = str;
        }
        if(GetComponent<TextMesh>())
        {
            GetComponent<TextMesh>().text = str;
        }
    }
    public void setText()
    {
        string str = Lang.Instance.getString(id);
        if (maxCharacterCount > 0)
        {
            if (maxcountSizedown)
            {
                for (int i = maxCharacterCount; i < str.Length; i += maxCharacterCount)
                {
                    str = str.Insert(i++, "\n");

                }
                if (GetComponent<TextMesh>())
                {
                    GetComponent<TextMesh>().fontSize = (int)(GetComponent<TextMesh>().fontSize * 0.6f);
                }
            }
            else
            {
                for (int i = maxCharacterCount; i < str.Length; i += maxCharacterCount)
                {
                    str = str.Insert(i++, "\n");

                }
            }
        }
      
        if (GetComponent<Text>())
            GetComponent<Text>().text = str;
        if (GetComponent<TextMesh>())
        {
            GetComponent<TextMesh>().text = str;
            
        }
        if (GetComponent<TextMeshProUGUI>())
        {
            GetComponent<TextMeshProUGUI>().text = str;
            gameObject.name = "Label_" + str;

        }

    }
}
