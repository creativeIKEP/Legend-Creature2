using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SmartPhoneUICtrl : MonoBehaviour {
    PlayerMove playerMove;
    PlayerAttack playerAttack;
    InputAttack.InputAttackCheacck inputAttackCheacck;
    FollowCamera followCamera;
    Vector2 prevPosition;
    Vector2 delta = Vector2.zero;
    bool moved = false;
    int touchCount = 0;
    int scrollTouchIndex = 0;

    int scrollButtonTouchCount = 0;

	// Use this for initialization
	void Start () {
        playerMove = FindObjectOfType<PlayerMove>();
        playerAttack = FindObjectOfType<PlayerAttack>();
        inputAttackCheacck = FindObjectOfType<InputAttack.InputAttackCheacck>();
        followCamera = FindObjectOfType<FollowCamera>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!moved) { delta = Vector2.zero; }

        if(scrollButtonTouchCount>=2){
            // 両方のタッチを格納します
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);
            // 各タッチの前フレームでの位置をもとめます
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
            // 各フレームのタッチ間のベクター (距離) の大きさをもとめます
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
            // 各フレーム間の距離の差をもとめます
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
            followCamera.Zoom(deltaMagnitudeDiff/100);
        }
	}

    public bool Clicked()
    {
        if (!moved && Input.GetMouseButtonUp(0))
            return true;
        else
            return false;
    }

    // スライド時のカーソルの移動量.
    public Vector2 GetDeltaPosition()
    {
        return delta;
    }

    // スライド中か.
    public bool Moved()
    {
        return moved;
    }

    public void JoyStickEnter(){
        //touchCount++;
    }
    public void JoyStickExit(){
        //touchCount--;
    }

    public void DashButton(){
        playerMove.DushMove();
    }
    public void DashMoveStop(){
        playerMove.DushMoveStop();
    }

    public void AttackButton(){
        playerAttack.Attack();
        StartCoroutine(inputAttackCheacck.Attack());
    }

    public void AvoidButton(){
        playerMove.AvoidOn();
    }

    public void TouchPinchArea(){
        scrollButtonTouchCount++;
    }

    public void ScrollButtonDown(){
        scrollTouchIndex=Input.touches.Length - 1;
        prevPosition = GetScrollButtonTouchPosition();
    }
    public void ScrollButton()
    {
        delta = GetScrollButtonTouchPosition()-prevPosition;
        prevPosition = GetScrollButtonTouchPosition();
        moved = true;
    }
    public void ScrollButtonUp()
    {
        moved = false;
        scrollButtonTouchCount--;
        if (scrollButtonTouchCount < 0) scrollButtonTouchCount = 0;
    }

    public void JumpButton(){
        playerMove.Jump();
    }

    Vector2 GetScrollButtonTouchPosition(){
        Vector2 result=Vector2.zero;
        if(Input.touchCount>0){
            Touch[] myTouches = Input.touches;

            int diff = myTouches.Length - touchCount;
            touchCount = Input.touchCount;
            if(diff<0){
                for (int i = 0; i < scrollTouchIndex; i++){
                    if(myTouches[i].phase == TouchPhase.Ended){
                        scrollTouchIndex -= 1;
                    }
                }
                result = myTouches[scrollTouchIndex].position;
            }
            else result = myTouches[scrollTouchIndex].position;
        }
        return result;
    }
}
