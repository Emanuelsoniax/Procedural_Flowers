using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Phyllotaxis))]
public class FlowerHeadEditor : Editor
{
    private Phyllotaxis flowerHead;

    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();

            if (check.changed)
            {
                flowerHead.ResetFlowerHead();
                flowerHead.GenerateFlorets();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            flowerHead.ResetFlowerHead();
            flowerHead.GenerateFlorets();
        }

        if (GUILayout.Button("Reset Flower"))
        {
            flowerHead.ResetFlowerHead();
        }
    }

    private void OnEnable()
    {
        flowerHead = (Phyllotaxis)target;
    }
}
