using UnityEngine;
using System.Collections;
namespace mytimer
{
    public class Timer : MonoBehaviour
    {

        float curTime = 9;// 24 hour;
        const float oneHour = 1f;//sec;
                                  //const float oneHour=1f;//sec;

        float div_onehour;
        // Use this for initialization
        public Light light;
        public void save()
        {
            PlayerPrefs.SetFloat("curTime_time", curTime);
        }
        public int getRealTimeSpendHour()
        {
            return (int)((curTime - 9) * oneHour * div_hour_sec);
        }
        public bool getSun()
        {
            return sun;
        }
      
        float div_hour_sec;
        static Timer instance;
        void Awake()
        {
           
            div_hour_sec = 1f / 3600f;
            instance = this;
            float delay = curTime - (float)((int)curTime);
            div_onehour = 1f / oneHour;
         

            float tcurTime = curTime % 24;
            if ((tcurTime < 8 || tcurTime > 20) && sun)
            {
                sun = false;
                transform.Rotate(0, 0, 180);
                light.intensity = 0.25f;
            }

        }

        public delegate void callBack();
        ArrayList OnDayStateChange = new ArrayList();

        public void AddDayChangeListener(callBack _OnChange)
        {
            //OnDig = _OnDig;
            OnDayStateChange.Add(_OnChange);
        }
        public void removeDayChangeListener(callBack _OnChange)
        {
            OnDayStateChange.Remove(_OnChange);
        }

        static public Timer getInstance()
        {
            return instance;
        }
        bool sun = true;


        void Update()
        {
            curTime += Time.deltaTime * div_onehour;
            float tcurTime = curTime % 24;

            if (tcurTime > 8 && tcurTime < 20 && !sun)
            {
                sun = true;
                
                for (int i = 0; i < OnDayStateChange.Count; i++)
                {
                    callBack t = (callBack)OnDayStateChange[i];
                    t();
                }
            }
            else if ((tcurTime < 8 || tcurTime > 20) && sun)
            {
                sun = false;
               
                for (int i = 0; i < OnDayStateChange.Count; i++)
                {
                    callBack t = (callBack)OnDayStateChange[i];
                    t();
                }
            }
        }




    }
}