﻿using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
	public float distance = 3.0f;
	public float horizontalAngle = 0.0f;
	public float rotAngle = 180.0f; // 画面の横幅分カーソルを移動させたとき何度回転するか.
	public float verticalAngle = 10.0f;
	public Transform lookTarget;
	public Vector3 offset = Vector3.zero;

	InputManager inputManager;
    SmartPhoneUICtrl smartPhoneCtrl;

	void Start()
	{
		inputManager = FindObjectOfType<InputManager>();
        smartPhoneCtrl = FindObjectOfType<SmartPhoneUICtrl>();
	}

	// Update is called once per frame
	void LateUpdate () {
		// ドラッグ入力でカメラのアングルを更新する.
        if(smartPhoneCtrl == null){
            if (inputManager.Moved())
            {
                float anglePerPixel = rotAngle / (float)Screen.width;
                Vector2 delta = inputManager.GetDeltaPosition();
                horizontalAngle += delta.x * anglePerPixel;
                horizontalAngle = Mathf.Repeat(horizontalAngle, 360.0f);
                verticalAngle -= delta.y * anglePerPixel;
                verticalAngle = Mathf.Clamp(verticalAngle, -60.0f, 60.0f);
            }
        }
        else{
            if (smartPhoneCtrl.Moved())
            {
                float anglePerPixel = rotAngle / (float)Screen.width;
                Vector2 delta = smartPhoneCtrl.GetDeltaPosition();
                horizontalAngle += delta.x * anglePerPixel;
                horizontalAngle = Mathf.Repeat(horizontalAngle, 360.0f);
                verticalAngle -= delta.y * anglePerPixel;
                verticalAngle = Mathf.Clamp(verticalAngle, -60.0f, 60.0f);
            }
        }
		
		
		// カメラを位置と回転を更新する.
		if (lookTarget != null) {
			Vector3 lookPosition = lookTarget.position + offset;
			// 注視対象からの相対位置を求める.
			Vector3 relativePos = Quaternion.Euler(verticalAngle,horizontalAngle,0) *  new Vector3(0,0,-distance);
			
			// 注視対象の位置にオフセット加算した位置に移動させる.
			transform.position = lookPosition + relativePos ;
			
			// 注視対象を注視させる.
			transform.LookAt(lookPosition);
			
			// 障害物を避ける.
			RaycastHit hitInfo;
			if (Physics.Linecast(lookPosition,transform.position,out hitInfo,1<<LayerMask.NameToLayer("Ground")))
				transform.position = hitInfo.point;
		}
	}

    public void SetTarget(Transform target){
        lookTarget = target;
    }

    public void Zoom(float scroll){
        Debug.Log("zoom");
        float setDistance = distance + scroll;
        if (setDistance < 1.0f) { setDistance = 1; }
        else if (setDistance > 10.0f) { setDistance = 10; }
        distance = setDistance;
    }
}
