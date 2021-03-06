﻿/*
 * Player can order unit in this UI.
*/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class P1_TacticalController : MonoBehaviour {
	//public PlayerController mainController;
	public Button orderButton;

	//This is the list of all button, to choose which unit to control, like swordsman ....
	public List<Button> soldierButtons = new List<Button>();

	public GameObject displayMap;

	//This is the current unit taking the order, be it horseman, or archer ....
	[SerializeField]
	int currentControlledUnit;

	//The attacking order or the moving order 
	//during moving, the unit will ignore all enemy on its path
	[SerializeField]
	bool isAttackingOrder;

	void Awake(){
		displayMap = GameObject.Find ("Player1_DisplayMap");
		isAttackingOrder = true;
		orderButton.image.color = Color.red;

	}

	//On enable this function, turn on the graphic map
	void OnEnable(){
		displayMap.gameObject.transform.position = new Vector3 (0,-4,-3);
	}

	void OnDisable(){
		displayMap.gameObject.transform.position = new Vector3 (0,-4,+3);
	}

	//This function change the currently controlled unit (swordman, archer, horseman ... )
	//This function is called by clicking at the approtiate unit button on the tactical map
	public void changeControlledUnit(int unit){
		currentControlledUnit = unit;

		foreach(Button b in soldierButtons)
			b.image.color = Color.white;

		soldierButtons [unit - 1].image.color = orderButton.image.color;
	}

	//This function is called to change the current type of order, moving or attacking.
	public void changeCurrentControllerOrder(){
		isAttackingOrder = !isAttackingOrder;
		if (isAttackingOrder)
			orderButton.image.color = Color.red;
		else
			orderButton.image.color = Color.green;
		if (currentControlledUnit != 0)
			soldierButtons [currentControlledUnit - 1].image.color = orderButton.image.color;
	}

	//This function is called if player touch a position on the map.
	public void orderUnit(){
		//if player haven't chosen any type of unit yet.
		if (currentControlledUnit == 0) {
			//do nothing.
			Debug.Log("No unit");
		}
		else{
			Vector2 position = Vector2.zero;
			float x = Screen.width;
			float y = Screen.height;

//#if UNITY_EDITOR
			position = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
//#endif
			//This is the touch control.
			foreach (Touch t in Input.touches) {
				if (t.position.x > 0.25f * x && t.position.x < 0.75f* x){
					if (t.position.y < 0.5f * y){
						position = t.position;
						break;
					}
				}
			}


			//The mouse position range from 0,0 to screen width and screen height
			position = new Vector2(position.x / x,position.y / y);
			position += new Vector2(-0.5f,-0.25f);
			position*= 4;			
			//translate the position onto the real world script
			position = new Vector2(position.x * 4.5f,position.y * 8);

			//order the unit to do its approtiate action
			int orderAction = 0;
			if (isAttackingOrder)
				orderAction =2;
			else
				orderAction = 1;
			PlayerController.p1_soldierOrder[currentControlledUnit - 1] = new Vector3(position.x,position.y,orderAction);

			PlayerController.orderSoldier();
		}
	}

}
