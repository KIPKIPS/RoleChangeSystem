using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateModel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        if (AvatarSystem.instance.sex==0) {
            AvatarSystem.instance.Girl();
        }
        else {
            AvatarSystem.instance.Boy();
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
