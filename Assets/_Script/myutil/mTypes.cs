using System;
using System.Text.RegularExpressions;
using UnityEngine;
namespace mTypes
{


    public delegate void callbackVoid();
    public delegate void callbackInt(int num);
    public delegate void callbackFloat(float val);
    public delegate void callbackBool(bool val);
    public delegate void callbackStringString(string val1, string val2);
    public delegate void callbackString(string val);
    public delegate void callbackVector2(Vector2 v);
    public static class myMath
    {
        public static Vector3 vectorY = new Vector3(0, 1, 0);
    }

    static class PlusString
    {
        public static String numberToStringWithComma(this ulong input)
        {
            return String.Format("{0:n0}", input);
            //return  String.Format("{0:#,###}", input);
        }
    }

}
