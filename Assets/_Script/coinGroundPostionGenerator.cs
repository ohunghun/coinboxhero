using UnityEngine;
using System.Collections;

public class coinGroundPostionGenerator 
{
    Vector2[,] table1 = new Vector2[500,1000];
    myRandom mrrandomf = new myRandom(-1f, 1f);
    myRandomInt mrandom = new myRandomInt(0, 1000);
    public coinGroundPostionGenerator(float screenWorldLength)
    {
        screenlength = screenWorldLength;
        screenHalf = (screenlength * 0.5f);
        setTable();
    }
    public float screenHalf;
    public float padding = -4;
    public float val2 = 0.4f;
    public Vector2 get(float randding)
    {
   
        return table1[(int)((randding + screenHalf+4) * 5f), mrandom.get()];
    }
 
    float screenlength = 20;
    float indexTolandingPos(int index)
    {
        return (index / 5f) - screenHalf-4;
    }
    float getRandingPos(float randding)
    {
        float arg=Mathf.Clamp( Mathf.Abs(randding), 2.5f, 4.5f);
        
        if(Random.Range(0,2)==1)
            return randding + screenlength * (Mathf.Exp((-10f / arg) * Random.Range(0f, 2f)))*0.5f;
        else
            return randding - screenlength * (Mathf.Exp((-10f / arg) * Random.Range(0f, 2f)))*0.5f;

    }
    void setTable()
    {
        for(int i=0;i<table1.GetLength(0);i++)
        {
            for(int j=0;j<table1.GetLength(1);j++)
            {
                table1[i, j] = new Vector2(-getRandingPos(indexTolandingPos(i)), mrrandomf.get() * val2 + padding);
            }
        }
    }
    
}
