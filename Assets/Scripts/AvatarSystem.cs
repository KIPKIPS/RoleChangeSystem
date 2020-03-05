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
    Dictionary<string, SkinnedMeshRenderer> girlSMR = new Dictionary<string, SkinnedMeshRenderer>();//换装骨骼上的MeshReneder信息
    // Start is called before the first frame update
    void Start() {
        InstantiateSource();
        InstantiateTarget();
        girlHips = girlTarget.GetComponentsInChildren<Transform>();
        DataSave();
        //foreach (KeyValuePair<string,Dictionary<string,SkinnedMeshRenderer>> gd in girlData) {
        //    foreach (var smr in gd.Value) {
        //        Debug.Log(gd.Key+" "+smr.Key+" "+smr.Value);
        //    }
        //}

    }

    // Update is called once per frame
    void Update() {
    }

    void InstantiateSource() {
        girlSource = Instantiate(Resources.Load<GameObject>("FemaleModel"));
        gsTrans = girlSource.transform;
        girlSource.SetActive(false);
    }

    void InstantiateTarget() {
        girlTarget = Instantiate(Resources.Load<GameObject>("FemaleTarget"));
    }

    void DataSave() {
        if (gsTrans == null) {
            return;
        }
        //遍历所有子物体的SkinnedMeshRenderer组件添加到数组中存储
        SkinnedMeshRenderer[] parts = gsTrans.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var part in parts) {
            string[] names = part.name.Split('-');
            if (!girlData.ContainsKey(names[0])) {
                //生成对应部位 且只生成一个
                GameObject partGo = new GameObject {
                    name = names[0]
                };
                partGo.transform.parent = girlTarget.transform;
                //把骨骼target身上的SkinnedMeshReneder信息存储
                girlSMR.Add(names[0],partGo.AddComponent<SkinnedMeshRenderer>());

                girlData.Add(names[0],new Dictionary<string, SkinnedMeshRenderer>());
            }
            girlData[names[0]].Add(names[1],part);//存储所有的SkinnedMeshReneder信息
        }
    }
}
