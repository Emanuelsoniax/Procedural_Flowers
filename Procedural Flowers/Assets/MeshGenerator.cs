using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FlowerPetal;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    private Mesh mesh;

    [SerializeField]
    private Vector3[] vertices;
    [SerializeField]
    private int[] triangles;


    // Start is called before the first frame update
    private void Start()
    {
    }

    public void CreateShape(List<Node> _spineNodes, List<Node> _branchNodes)
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        //vertices.Clear();

        vertices = new Vector3[]
        {
            _spineNodes[0].Position,
            _spineNodes[1].Position,
            _spineNodes[2].Position,
            _spineNodes[3].Position,
            _spineNodes[4].Position,
            _spineNodes[5].Position,
            _branchNodes[0].Position,
            _branchNodes[1].Position,
            _branchNodes[2].Position,
            _branchNodes[3].Position,
            _branchNodes[4].Position,
            _branchNodes[5].Position,
            _branchNodes[6].Position,
            _branchNodes[7].Position,
            _branchNodes[8].Position,
            _branchNodes[9].Position,
            _branchNodes[10].Position,
            _branchNodes[11].Position,
            _branchNodes[12].Position,
            _branchNodes[13].Position,
            _branchNodes[14].Position,
            _branchNodes[15].Position,
            _spineNodes[6].Position,
        };
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
