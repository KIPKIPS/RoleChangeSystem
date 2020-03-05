using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSystem : MonoBehaviour {
    private GameObject girlSource;//资源模型
    private GameObject girlTarget;//骨骼物体
    //girl所有的资源信息
    private Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> girlData = new Dictionary<string, Dictionary<string, SkinnedMeshRenderer>>();

    private Transform[] girlHips;//girl骨骼信息
    private Transform gsTrans;
    // Start is called before the first frame update
    void Start()
    {
        InstantiateSource();
        InstantiateTarget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstantiateSource() {
        girlSource = Instantiate(Resources.Load<GameObject>("FemaleModel"));
        gsTrans = girlSource.transform;
        girlSource.SetActive(false);
    }

    void InstantiateTarget() {
        girlTarget = Instantiate(Resources.Load<GameObject>("FemaleTarget"));

    }
}
