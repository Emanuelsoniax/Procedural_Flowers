using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FlowerPetal))]
public class FlowerPetalEditor : Editor
{
    private FlowerPetal flowerPetal;

    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();

            if (check.changed)
            {
                flowerPetal.ResetPetal();
                flowerPetal.Generate();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            flowerPetal.ResetPetal();
            flowerPetal.Generate();
        }

        if (GUILayout.Button("Reset Petal"))
        {
            flowerPetal.ResetPetal();
        }
    }

    private void OnEnable()
    {
        flowerPetal = (FlowerPetal)target;
        flowerPetal.ResetPetal();
        flowerPetal.Generate();
    }

}
