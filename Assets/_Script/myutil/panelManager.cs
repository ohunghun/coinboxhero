
using Doozy.Engine.UI;
using mTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panelManager :Singleton<panelManager>
{
    
    Hashtable table = new Hashtable();
    List<callbackVoid> onCurrentPanelClosed=new List<callbackVoid>();
    panel currentOpenPanel = null;
    private void Awake()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            GameObject g=transform.GetChild(i).gameObject;
            panel p = g.GetComponent<panel>();
            if (p == null)
                Debug.LogError(g.name);
            table.Add(g.name, g);
        }
    }
    
    public bool isPanelOpenned(string id)
    {
        return ((GameObject)table[id]).activeSelf;
    }
    public bool isAniPanelOpenned()
    {
       
        foreach (Object obj in table.Values)
        {
            GameObject panel = (GameObject)obj;
            if (panel.gameObject.activeSelf)
                return true;
        }
        return false;
    }
    bool isLock = false;
    public void Lock()
    {
        isLock = true;
    }
    public void Unlock()
    {
        isLock = false;
    }
    public void openPanel(string id)
    {
        if (isLock)
            return;
        if (table.ContainsKey(id))
        {
            closeCurrent();
            panel panel = ((GameObject)table[id]).GetComponent<panel>();
            panel.gameObject.SetActive(true);
            panel.OnOpended(null);
            currentOpenPanel = panel;

        }
    }
    public void openPanel(string id,params object[] arg)
    {
        if (isLock)
            return;
        if (table.ContainsKey(id))
        {
            closeCurrent();
            panel panel = ((GameObject)table[id]).GetComponent<panel>();
            panel.gameObject.SetActive(true);
            panel.OnOpended(arg);
            currentOpenPanel = panel;
            
        }
    }
    public void openPanel(string id,callbackVoid callbackClosed, params object[] arg)
    {
        if (isLock)
            return;
        if (table.ContainsKey(id))
        {
            closeCurrent();
            panel panel = ((GameObject)table[id]).GetComponent<panel>();
            panel.gameObject.SetActive(true);
            panel.OnOpended(arg);
            currentOpenPanel = panel;
            onCurrentPanelClosed.Add(callbackClosed);
        }
    }
    public void openPopup(Sprite icon,string title,string sub)
    {
        
        UIPopup m_popup = UIPopupManager.GetPopup("AchievementPopup");

        //make sure that a popup clone was actually created
        if (m_popup == null)
            return;

        //set the achievement icon
        m_popup.Data.SetImagesSprites(spritemanager.Instance.getSprite("coin3"));
        //set the achievement title and message
        m_popup.Data.SetLabelsTexts(title, sub);
        

        //show the popup
        UIPopupManager.ShowPopup(m_popup, true, false);
        
    }
    public void closeCurrent()
    {
        Unlock();
        if (currentOpenPanel == null)
            return;
        currentOpenPanel.gameObject.SetActive(false);
        currentOpenPanel.OnClosed();
        currentOpenPanel = null;
        if (onCurrentPanelClosed.Count>0)
        {
            callbackVoid callback = onCurrentPanelClosed[0];
            onCurrentPanelClosed.RemoveAt(0);
            callback();
            //onCurrentPanelClosed = null;
        }
    }
    /*public void closeAllPanel()
    {
        Unlock();
        foreach (Object obj in table.Values)
        {
            GameObject panel = (GameObject)obj;
            if (panel.activeSelf)
            {
                panel.SetActive(false);
                panel.GetComponent<panel>().OnClosed();
                if(onCurrentPanelClosed!=null)
                {
                    onCurrentPanelClosed();
                    onCurrentPanelClosed = null;
                }

            }
        }
    }*/


}
