using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 자식 메쉬를 부모 메쉬에 복사
// CombineInstance로 메쉬들을 결합
// 그 후 메쉬에 할당

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CombineMeshes : MonoBehaviour
{
    void Start()
    {
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        for(int i = 0; i < meshFilters.Length; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.SetActive(false);
        }

        GetComponent<MeshFilter>().mesh = new Mesh();
        GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        GetComponent<MeshRenderer>().material = GetComponentInChildren<MeshRenderer>().material;
        gameObject.SetActive(true);
    }
}
