using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phyllotaxis : MonoBehaviour
{
    public GameObject floret;
    [SerializeField]
    private float degree;            //divergence angle
    [SerializeField]
    private float c;           
    [SerializeField]
    private float floretScale;
    [SerializeField]
    private float upscaling;        //upscaling parameter
    [SerializeField]
    private float floretY;          //y-position of floret 
    [SerializeField]
    private int n;                  //floret

    private Vector2 CalculatePhyllotaxis(float _degree, float _scale, int _count)
    {
        double angle = _count * (_degree * Mathf.Deg2Rad);     //double for higher precicion
        float r = _scale * Mathf.Sqrt(_count);                 //distance between the center of the head and the center of the floret

        float x = r * (float)System.Math.Cos(angle);
        float z = r * (float)System.Math.Sin(angle);

        return new Vector2(x, z);
    }

    private Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            position = CalculatePhyllotaxis(degree, c, n);
            GameObject floretInstance = (GameObject)Instantiate(floret);
            floretInstance.transform.position = new Vector3(position.x, floretY, position.y);
            floretInstance.transform.localScale = new Vector3(floretScale, floretScale, floretScale);
            n++;
            floretScale += upscaling;
            floretY -= 0.01f;

        }
    }
}
