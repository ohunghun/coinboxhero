using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
public class Lang :Singleton<Lang>{
    bool isKorean = false;
    void Awake()
    {
       


        if (Application.systemLanguage == SystemLanguage.Korean)
        {
            isKorean = true;
            setLanguage("korea");
            //setLanguage("english");
        }
        /*else if (Application.systemLanguage == SystemLanguage.Japanese)
        {
            setLanguage("japanese");
        }
        else if (Application.systemLanguage == SystemLanguage.Thai)
        {
            setLanguage("thai");
        }
        else if (Application.systemLanguage == SystemLanguage.Russian)
        {
            setLanguage("russian");
        }
        else if (Application.systemLanguage == SystemLanguage.Chinese|| Application.systemLanguage == SystemLanguage.ChineseSimplified || Application.systemLanguage == SystemLanguage.ChineseTraditional)
        {
            setLanguage("chinese");
        }*/
        else
        {

            setLanguage("english");
        }


    }
    void Start()
    {
        
    }
    Hashtable Strings=new Hashtable();
#if UNITY_EDITOR
    bool init=false;
    public string  getString(string id)
    {

        if(!init)
        {
            setLanguage("korea");
            init = true;
        }
		if (!Strings.ContainsKey(id)) {
            if (Application.isEditor)
                setLanguage("korea");
            else
                return null;
            

        }

        string str = (string)Strings[id];
        str=str.Replace("\\n", "§");      
        str =str.Replace( '§','\n');
        
        return str;
	}
#else
    public string getString(string id)
    {

        if (!Strings.ContainsKey(id))
        {

            
            
                Debug.LogError("The specified string does not exist: " + id);
                return "";
            
        }

        string str = (string)Strings[id];
        str = str.Replace("\\n", "§");
        str = str.Replace('§', '\n');

        return str;
    }
#endif

    public string getStringWithoutError(string id)
    {

        if (!Strings.ContainsKey(id))
        {

          
            return "";
        }
        string str = (string)Strings[id];
        str = str.Replace("\\n", "§");
        str = str.Replace('§', '\n');

        return str;
    }
    void setLanguage(string language)
    {
        TextAsset textFile = (TextAsset)Resources.Load("lang", typeof(TextAsset));
        XmlDocument xml = new XmlDocument();
        xml.Load(new StringReader(textFile.text));

        Strings = new Hashtable();
        XmlElement element = xml.DocumentElement[language];
        if (element!=null)
        {
            IEnumerator elemEnum  = element.GetEnumerator();
            while (elemEnum.MoveNext())
            {
                XmlElement xmlItem = (XmlElement)elemEnum.Current;
                Strings.Add(xmlItem.GetAttribute("name"), xmlItem.InnerText);
            }
        }
        else
        {
            Debug.LogError("The specified language does not exist: " + language);
        }
    }
    void printText()
    {

        TextAsset textFile = (TextAsset)Resources.Load("lang", typeof(TextAsset));
        XmlDocument xml = new XmlDocument();
        xml.Load(new StringReader(textFile.text));

        Strings = new Hashtable();

        XmlElement element = xml.DocumentElement["korea"];
        XmlElement element_en = xml.DocumentElement["english"];
        string str="";
        IEnumerator elemEnum = element_en.GetEnumerator();
        while (elemEnum.MoveNext())
        {
            XmlElement xmlItem = (XmlElement)elemEnum.Current;
            Strings.Add(xmlItem.GetAttribute("name"), xmlItem.InnerText);
            str += xmlItem.InnerText.Replace("\\n", "\n") + "\n\n";
        }
        Debug.Log(str);
    }
    void checkMissing()
    {
        TextAsset textFile = (TextAsset)Resources.Load("lang", typeof(TextAsset));
        XmlDocument xml = new XmlDocument();
        xml.Load(new StringReader(textFile.text));

        Strings = new Hashtable();
       
        XmlElement element = xml.DocumentElement["korea"];
        XmlElement element_en = xml.DocumentElement["english"];

        IEnumerator elemEnum = element_en.GetEnumerator();
        while (elemEnum.MoveNext())
        {
            XmlElement xmlItem = (XmlElement)elemEnum.Current;
            Strings.Add(xmlItem.GetAttribute("name"), xmlItem.InnerText);
        }
        elemEnum = element.GetEnumerator();
        while (elemEnum.MoveNext())
        {
            XmlElement xmlItem = (XmlElement)elemEnum.Current;
            //Strings.Add(xmlItem.GetAttribute("name"), xmlItem.InnerText);
            if(!Strings.ContainsKey(xmlItem.GetAttribute("name")))
            {
                Debug.Log(xmlItem.GetAttribute("name"));
            }
        }


    }

}
