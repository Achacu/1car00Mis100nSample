using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphDrawer : MonoBehaviour
{
    [SerializeField] private LineRenderer line;    
    [SerializeField] private float lineLength;    
    [SerializeField] private float yScaleFactor = 0.1f;
    

    public void OnValidate()
    {
        line = GetComponent<LineRenderer>();

        float scaleFactor = lineLength / line.positionCount;

        for (int i = 0; i < line.positionCount; i++)
        {
            line.SetPosition(i, Vector3.forward * (i * scaleFactor));
        }
    }
    public void Awake()
    {
        OnValidate();
    }
    // Start is called before the first frame update
    //[SerializeField] private float A = 1f;    
    //[SerializeField] private float w = 0f;    
    //[SerializeField] private float phase = 0;
    
    //void Update()
    //{
    //    GraphFunction((float x) => (A*Mathf.Sin(w*x+phase)));
    //}
    public void GraphFunction(System.Func<float, float> f)
    {
        Vector3 pos;
        //float xScaleFactor = xRes / line.positionCount;
        for(int x = 0; x < line.positionCount; x++)
        {
            pos = line.GetPosition(x);
            pos.y = f(x/(float)line.positionCount) * yScaleFactor;
            line.SetPosition(x, pos);
        }
    }
}
