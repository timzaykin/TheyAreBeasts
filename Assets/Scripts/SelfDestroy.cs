using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour {
    //Уничтожение объекта через указанное время

    public float time;
    public Shader DisolveShader;


    SkinnedMeshRenderer[] meshes;



    private void Start()
    {
        meshes = GetComponentsInChildren<SkinnedMeshRenderer>();
        StartCoroutine("Disolve");
        Destroy(gameObject, time);

    }


    IEnumerator Disolve() {
        yield return new WaitForSeconds(time - 1f);
        foreach (var mesh in meshes)
        {
            mesh.material.shader = DisolveShader;
        }
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForEndOfFrame();
            foreach (var mesh in meshes)
            {
                mesh.material.SetFloat("_Value", i * 0.008f);
            }
        }


    }

}
