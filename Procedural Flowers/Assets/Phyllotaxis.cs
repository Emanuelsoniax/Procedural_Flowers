using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Phyllotaxis : MonoBehaviour
{
    public GameObject floret;
    [SerializeField]
    private List<GameObject> florets = new List<GameObject>();
    [SerializeField]
    private float degree;            //divergence angle
    [SerializeField]
    private float c;           
    [SerializeField]
    [Range(0.0f, 0.5f)]
    private float floretScale;
    [SerializeField]
    [Range(0.01f, 0.05f)]
    private float upscalingFactor;
    private float currentScale;
    [SerializeField]
    private float startingHeight;
    [SerializeField]
    private float heightFactor;
    [SerializeField]
    [Range(0.1f, 0.5f)]
    private float rotationFactor;
    private float rotationX;
    private float floretY;          //y-position of floret 
    [SerializeField]
    private int n;                  //floret
    [SerializeField]
    private float headRadius;

    private Vector2 CalculatePhyllotaxis(float _degree, float _scale, int _count)
    {
        double angle = _count * (_degree * Mathf.Deg2Rad);     //double for higher precicion
        float r = _scale * Mathf.Sqrt(_count);                 //distance between the center of the head and the center of the floret

        float x = r * (float)System.Math.Cos(angle);
        float z = r * (float)System.Math.Sin(angle);

        return new Vector2(x, z);
    }

    public void ResetFlowerHead()
    {
        for (int i = florets.Count-1; i >= 0; i--)
        {
            DestroyImmediate(florets[i]);
        }
        florets.Clear();
        n = 0;
        currentScale = floretScale;
        floretY = startingHeight;
        rotationX = 1;
    }


    private Vector2 position;
    public void GenerateFlorets()
    {
        while ((c * Mathf.Sqrt(n)) < headRadius)
        {
            position = CalculatePhyllotaxis(degree, c, n);
            GameObject floretInstance = Instantiate(floret, transform);
            florets.Add(floretInstance);
            EditFloret(floretInstance);
            currentScale += upscalingFactor;
            floretY -= heightFactor;
            n++;
        }
    }

    public void EditFloret(GameObject _floretInstance)
    {
        //set location
       _floretInstance.transform.position = new Vector3(position.x, floretY, position.y);
        //set scale
       _floretInstance.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
        //set rotation
        _floretInstance.transform.LookAt(transform);
        _floretInstance.transform.eulerAngles -= new Vector3(rotationX += rotationFactor, 0, 0);

        //generate petal shape
       _floretInstance.GetComponent<FlowerPetal>().ResetPetal();
       _floretInstance.GetComponent<FlowerPetal>().Generate();
    }
}
