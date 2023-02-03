using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(MeshFilter))]
public class FlowerPetal : MonoBehaviour
{
    [SerializeField]
    private float mainInitialLength = 5;
    private float currentMainLength;
    [SerializeField]
    private float mainGrowthRate = 1;
    [SerializeField]
    private float lateralInitialLength = 1;
    private float currentLateralLength;
    [SerializeField]
    private float lateralGrowthRate = 1;
    [SerializeField]
    private float growthPotentialDecrement = 0;
    private float n = 5;           //generations
    [SerializeField]
    private float angle = 60;
    [SerializeField]
    public List<Node> spineNodes = new List<Node>();
    [SerializeField]
    public List<Node> branchNodes = new List<Node>();
    [System.Serializable]
    public class Node
    {
        private float x;
        private float y;
        private float z;
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public Vector3 Position
        {
            get { return new Vector3(x, y, z); }
        }

        public Node(float _x, float _y)
        {
            x = _x;
            y = _y;
        }
    }

    [Header("Mesh")]
    [SerializeField]
    private List<Vector3> vertices = new List<Vector3>();
    [SerializeField]
    private int[] triangles;
    private Mesh mesh;
    public MaterialPropertyBlock propertyBlock;
    public void Generate()
    {
        CreateSpine();
        CreateBranches();
        CreateEndNode();

        CreateShape();
        propertyBlock = new MaterialPropertyBlock();
    }

    public void ResetPetal()
    {
        spineNodes.Clear();
        branchNodes.Clear();
    }

    private void CreateSpine()
    {
        Node baseNode = new Node(0, 0);
        spineNodes.Add(baseNode);
        Node lastNode = baseNode;

        currentMainLength = mainInitialLength;
        for (int i = 0; i < n; i++)
        {
            Node spineNode = CreateSpineNode(lastNode.Position, i + 1);
            spineNodes.Add(spineNode);
            lastNode = spineNode;
        }
    }
    private void CreateEndNode()
    {
        Node endNode = CreateSpineNode(spineNodes[(int)n].Position, 0);
        spineNodes.Add(endNode);
    }
    private Node CreateSpineNode(Vector3 _startpos, int _n) //G
    {
        Vector3 endpos = _startpos + new Vector3(0, currentMainLength *= mainGrowthRate, 0);

        Node newSpineNode = new Node(endpos.x, endpos.y);
        return newSpineNode;
    }
    public void CreateBranches()
    {

        currentLateralLength = lateralInitialLength;
        for (int i = 0; i < spineNodes.Count; i++)
        {
            LateralBranch(i, spineNodes[i].Position, -1);
        }
        currentLateralLength = lateralInitialLength;
        for (int i = 0; i < spineNodes.Count; i++)
        {
            LateralBranch(i, spineNodes[i].Position, 1);
        }
    }
    private void LateralBranch(float _t, Vector3 _spinePos, int _dir)
    {
        //condition
        if (_t > 0)
        {
            Vector3 pos;
            pos.x = _spinePos.x + (currentLateralLength * Mathf.Cos(angle * Mathf.Deg2Rad)) * _dir;
            pos.y = _spinePos.y + (currentLateralLength * Mathf.Sin(angle * Mathf.Deg2Rad));

            if(currentLateralLength > 4)
            {
                currentLateralLength /= lateralGrowthRate - growthPotentialDecrement;
            } 
            else currentLateralLength *= lateralGrowthRate - growthPotentialDecrement;

            Node newLatNode = new Node(pos.x, pos.y);
            branchNodes.Add(newLatNode);
        }
        else
        {
            return;
        }
    }

    public void CreateShape()
    {
        vertices.Clear();

        for(int i = 0; i < spineNodes.Count -1; i++)
        {
            vertices.Add(spineNodes[i].Position);
        }
        for(int i = 0; i < branchNodes.Count; i++)
        {
            vertices.Add(branchNodes[i].Position);
        }
        vertices.Add(spineNodes[6].Position);

        triangles = new int[]
        {
            0,11,1,
            0,1,6,
            1,11,2,
            1,2,6,
            2,11,12,
            2,7,6,
            2,12,3,
            2,3,7,
            3,12,13,
            3,8,7,
            3,13,4,
            3,4,8,
            4,13,14,
            4,9,8,
            4,14,5,
            4,5,9,
            5,14,15,
            5,10,9,
            5,15,16,
            5,16,10
        };

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        UpdateMesh();
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    //private void OnDrawGizmos()
    //{
    //    foreach (Node _node in spineNodes)
    //    {
    //        Gizmos.color = Color.red;
    //        Gizmos.DrawSphere(_node.Position, 0.5f);
    //    }

    //    foreach (Node _node in branchNodes)
    //    {
    //        Gizmos.color = Color.blue;
    //        Gizmos.DrawSphere(_node.Position, 0.5f);
    //    }

    //}

}
