using System.Collections;
using UnityEngine;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
public class DataBase : Singleton<DataBase> {
    ObscuredString dbXml = "";
    Hashtable hash_sprite = new Hashtable();
   
    
    bool isInit = false;
    void init()
    {
        //StartCoroutine("init_DBXML_FromWeb");
         init_DBXml_FromFile();
        isInit = true;
    }
    
    private void init_DBXml_FromFile()
    {
        TextAsset textFile = (TextAsset)Resources.Load("stat", typeof(TextAsset));
        dbXml = textFile.text;
      
    }
    private IEnumerator init_DBXML_FromWeb()
    {
        string dbPath = "http://jhdream2.dothome.co.kr/aa.txt";
        WWW w = new WWW(dbPath);
        while (!w.isDone)
            yield return null;

        dbXml = w.text;
        
        yield return null;
    }
    Dictionary<string, Dictionary<string, string>> catchTable = new Dictionary<string, Dictionary<string, string>>();
    public Dictionary<string, string> getRecord(string id)
    {
        if (!isInit)
            init();
        if (catchTable.ContainsKey(id))
            return catchTable[id];
        Dictionary<string, string>  table = new Dictionary<string, string>();
       
        XmlDocument xml = new XmlDocument();
        xml.Load(new StringReader(dbXml));

        if (xml.DocumentElement[id] == null)
            return null;
        foreach (XmlNode node in xml.DocumentElement[id].Attributes)
        {
            
            table[node.Name] = node.Value;
            
        }
        catchTable.Add(id, table);
        return table;
    }
    Dictionary<string, object> catchValue = new Dictionary<string, object>();
    public int getIntValue(string id,string key)
    {
        string valueKey = id + "*" + key;
        if (catchValue.ContainsKey(valueKey))
            return (int)catchValue[valueKey];
        
        Dictionary<string, string> table = getRecord(id);
        int value = int.Parse(table[key]);
        catchValue.Add(valueKey, value);
        return (int)value;
    }
    public ulong getUlongValue(string id, string key)
    {
        
        string valueKey = id + "*" + key;
        if(catchValue.ContainsKey(valueKey))
            return (ulong)catchValue[valueKey];
        
        Dictionary<string, string> table = getRecord(id);

        ulong value = ulong.Parse(table[key]);
        catchValue.Add(valueKey, value);
        return (ulong)value;
    }
}
