using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarToggle : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
    }

    private string last = "girl";
    public void OnValueChanged(bool isOn) {
        if (isOn) {
            if (name=="girl"||name=="boy") {
                if (name=="girl") {
                    AvatarSystem.instance.SetSexToGirl();
                    return;
                }
                else {
                    AvatarSystem.instance.SetSexToBoy();
                    return;
                }
            }
            string[] names = this.name.Split('-');//name数组索引0为部位,索引1为部位序号
            AvatarSystem.instance.ClothesReplace(names[0],names[1]);
            switch (names[0]) {
                case "pants":
                    AnimationPlay("item_pants");
                    break;
                case "shoes":
                    AnimationPlay("item_boots");
                    break;
                case "top":
                    AnimationPlay("item_shirt");
                    break;
                default:
                    break;
            }
        }
    }

    public void AnimationPlay(string animName) {
        //Debug.Log(animName);
        Animation anim = GameObject.FindGameObjectWithTag("Model").GetComponent<Animation>();
        if (!anim.IsPlaying(animName)) {
            anim.Play(animName);
            anim.PlayQueued("idle1");
        }
    }
}
