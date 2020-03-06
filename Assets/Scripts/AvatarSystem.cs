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
    string[,] girlStr=new string[,]{{"eyes","1"},{"hair","1"},{"top","1"},{"pants","1"},{"shoes","1"},{"face","1"}};
    // Start is called before the first frame update
    void Start() {
        InstantiateSource();
        InstantiateTarget();
        girlHips = girlTarget.GetComponentsInChildren<Transform>();//存储骨骼信息
        DataSave();
        //foreach (KeyValuePair<string,Dictionary<string,SkinnedMeshRenderer>> gd in girlData) {
        //    foreach (var smr in gd.Value) {
        //        Debug.Log(gd.Key+" "+smr.Key+" "+smr.Value);
        //    }
        //}
        InitAvatar();
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

    //存储人物模型的信息
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

    void MeshReplace(string part,string num) {
        SkinnedMeshRenderer skm = girlData[part][num];//部位资源
        List<Transform> bones = new List<Transform>();
        foreach (var trans in skm.bones) {
            foreach (var bone in girlHips) {
                if (bone.name==trans.name) {
                    bones.Add(bone);
                    break;
                }
            }
        }
        //更换mesh
        girlSMR[part].bones = bones.ToArray();
        girlSMR[part].materials = skm.materials;
        girlSMR[part].sharedMesh = skm.sharedMesh;
    }
    //初始化
    void InitAvatar() {
        int length = girlStr.GetLength(0);//获取行数
        for (int i = 0; i < length; i++) {
            MeshReplace(girlStr[i,0],girlStr[i,1]);
        }
    }
}
