using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSystem : MonoBehaviour {
    public GameObject girlPlane;
    public GameObject boyPlane;

    public static AvatarSystem instance;
    //girl
    private GameObject girlTarget;//骨骼物体
    //girl所有的资源信息
    private Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> girlData = new Dictionary<string, Dictionary<string, SkinnedMeshRenderer>>();
    private Transform[] girlHips;//girl骨骼信息
    private Transform girlTrans;
    Dictionary<string, SkinnedMeshRenderer> girlSMR = new Dictionary<string, SkinnedMeshRenderer>();//换装骨骼上的MeshReneder信息
    string[,] girlStr = new string[,] { { "eyes", "1" }, { "hair", "1" }, { "top", "1" }, { "pants", "1" }, { "shoes", "1" }, { "face", "1" } };

    //boy
    private GameObject boyTarget;//骨骼物体
    //boy所有的资源信息
    private Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> boyData = new Dictionary<string, Dictionary<string, SkinnedMeshRenderer>>();
    private Transform[] boyHips;//boy骨骼信息
    private Transform boyTrans;
    Dictionary<string, SkinnedMeshRenderer> boySMR = new Dictionary<string, SkinnedMeshRenderer>();//换装骨骼上的MeshReneder信息
    string[,] boyStr = new string[,] { { "eyes", "1" }, { "hair", "1" }, { "top", "1" }, { "pants", "1" }, { "shoes", "1" }, { "face", "1" } };
    public int sex = 0;//0 girl,1 boy

    void Awake() {
        instance = this;
    }
    void Start() {
        Girl();
        Boy();
        boyTarget.SetActive(false);
    }

    void Update() {

    }

    void Girl() {
        InstantiateGirlAvatar();
        DataSave(girlTrans, girlTarget, girlData, girlSMR);
        InitAvatar(girlData, girlHips, girlSMR, girlStr);
    }
    void Boy() {
        InstantiateBoyAvatar();
        DataSave(boyTrans, boyTarget, boyData, boySMR);
        InitAvatar(boyData, boyHips, boySMR, boyStr);
    }
    //初始化Model资源和Target模板
    void InstantiateGirlAvatar() {
        GameObject source = Instantiate(Resources.Load<GameObject>("FemaleModel"), transform.position, Quaternion.identity);
        girlTrans = source.transform;
        source.SetActive(false);
        girlTarget = Instantiate(Resources.Load<GameObject>("FemaleTarget"), transform.position, Quaternion.identity);
        girlHips = girlTarget.GetComponentsInChildren<Transform>();//存储骨骼信息
    }
    void InstantiateBoyAvatar() {
        GameObject source = Instantiate(Resources.Load<GameObject>("MaleModel"), transform.position, Quaternion.identity);
        boyTrans = source.transform;
        source.SetActive(false);
        boyTarget = Instantiate(Resources.Load<GameObject>("MaleTarget"), transform.position, Quaternion.identity);
        boyHips = boyTarget.GetComponentsInChildren<Transform>();//存储骨骼信息
    }

    //存储人物模型的信息
    void DataSave(Transform trans, GameObject tar, Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> data, Dictionary<string, SkinnedMeshRenderer> smr) {
        if (trans == null) {
            return;
        }
        //遍历所有子物体的SkinnedMeshRenderer组件添加到数组中存储
        SkinnedMeshRenderer[] parts = trans.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var part in parts) {
            string[] names = part.name.Split('-');
            if (!data.ContainsKey(names[0])) {
                //生成对应部位 且只生成一个
                GameObject partGo = new GameObject {
                    name = names[0]
                };
                partGo.transform.parent = tar.transform;
                //把骨骼target身上的SkinnedMeshReneder信息存储
                smr.Add(names[0], partGo.AddComponent<SkinnedMeshRenderer>());

                data.Add(names[0], new Dictionary<string, SkinnedMeshRenderer>());
            }
            data[names[0]].Add(names[1], part);//存储所有的SkinnedMeshReneder信息
        }
    }

    void MeshReplace(Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> data, Transform[] hips, Dictionary<string, SkinnedMeshRenderer> smr, string part, string num) {
        SkinnedMeshRenderer smrTemp = data[part][num];//部位资源
        //获取骨骼
        List<Transform> bones = new List<Transform>();
        foreach (Transform skmBone in smrTemp.bones) {
            foreach (Transform bone in hips) {
                if (bone.name == skmBone.name) {
                    bones.Add(bone);
                    break;
                }
            }
        }
        //更换mesh
        smr[part].bones = bones.ToArray();//绑定骨骼
        smr[part].materials = smrTemp.materials;
        smr[part].sharedMesh = smrTemp.sharedMesh;
    }
    //初始化
    void InitAvatar(Dictionary<string, Dictionary<string, SkinnedMeshRenderer>> data, Transform[] hips, Dictionary<string, SkinnedMeshRenderer> smr, string[,] obj) {
        int length = obj.GetLength(0);//获取行数
        for (int i = 0; i < length; i++) {
            MeshReplace(data, hips, smr, obj[i, 0], obj[i, 1]);
        }
    }

    //更换衣服
    public void ClothesReplace(string part, string num) {
        if (sex == 0) {
            MeshReplace(girlData, girlHips, girlSMR, part, num);
        }
        else {
            MeshReplace(boyData, boyHips, boySMR, part, num);
        }
    }
    //更改模型性别
    public void SetSexToGirl() {
        sex = 0;
        boyTarget.SetActive(false);
        girlTarget.SetActive(true);
        girlPlane.SetActive(true);
        boyPlane.SetActive(false);
    }

    public void SetSexToBoy() {
        sex = 1;
        boyTarget.SetActive(true);
        girlTarget.SetActive(false);
        girlPlane.SetActive(false);
        boyPlane.SetActive(true);
    }
}
