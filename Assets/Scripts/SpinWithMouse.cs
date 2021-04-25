using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWithMouse : MonoBehaviour {
    private bool isDown = false;//鼠标是否按下

    public Vector3 curPos;
    public Vector3 lastPos;

    private float length = 0;//鼠标最小位移
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        curPos = Input.mousePosition;//当前帧鼠标位置
        if (isDown) {
            Vector3 offset = curPos - lastPos;
            if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y) && Mathf.Abs(offset.x) > length) {
                transform.Rotate(Vector3.up, -offset.x);
            }
        }
        lastPos = Input.mousePosition;//上一帧鼠标位置
    }

    void OnMouseUp() {
        isDown = false;
    }
    void OnMouseDown() {
        isDown = true;
    }
}
