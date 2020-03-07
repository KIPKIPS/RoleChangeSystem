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
        }
        
    }
}
