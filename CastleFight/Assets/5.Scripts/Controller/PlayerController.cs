﻿/*
This class order unit to go, attack and stuffs.
*/
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using PathFinder;

public class PlayerController : MonoBehaviour {
    public static SimpleWorld2D knownWorld = new SimpleWorld2D(9, 16);

//The attack order for player one - the z dimension of the vector is the order.
//0 = stay idle at current position, 1 is move, 2 is attack
	public static List<Vector3> p1_soldierOrder = new List<Vector3>();

	public static List<Vector3> p2_soldierOrder = new List<Vector3>();

//This lsit contain all the list of soldier. List 0 = swordman, 1 = archer ....
	public static List<List<Soldier>> p1_listOfSoldierLists = new List<List<Soldier>>(); 
	public static List<List<Soldier>> p2_listOfSoldierLists = new List<List<Soldier>>();

//The list of all buildings.
	public static List<List<Building>> p1_buildingList = new List <List<Building>>();
	public static List<List<Building>> p2_buildingList = new List <List<Building>>();
	//castle = 0 ; barrack= 1, tower = 2 , wall = 3
//This list contains all the soldier of player on -- will remove later
//	public static List<SwordMan> p1_swordmanList = new List<SwordMan> ();
//	public static List<Archer> p1_archerList = new List<Archer>();
//	public static List<HorseMan> p1_horsemanList = new List<HorseMan>();
//	public static List<Gladiator> p1_gladiatorList = new List<Gladiator>();
//	public static List<Cannon> p1_cannonList = new List<Cannon>();

//This list contains all the soldiers of player two -- will remove later
//	public static List<SwordMan> p2_swordmanList = new List<SwordMan> ();
//	public static List<Archer> p2_archerList = new List<Archer>();
//	public static List<HorseMan> p2_horsemanList = new List<HorseMan>();
//	public static List<Gladiator> p2_gladiatorList = new List<Gladiator>();
//	public static List<Cannon> p2_cannonList = new List<Cannon>();

//awake void. add all the soldier to all the list
	void Awake(){
		p1_listOfSoldierLists.Clear ();
		p2_listOfSoldierLists.Clear ();
		//There are 5 types of unit so there will be 5 lists of soldier
		for (int i = 0; i < 5; i ++) {
			p1_listOfSoldierLists.Add(new List<Soldier>());
			p2_listOfSoldierLists.Add(new List<Soldier>());
		}

		//there are four types of building icluding wall so we have 4 lists
		for (int i = 0; i < 4; i ++) {
			p1_buildingList.Add(new List<Building>());
			p2_buildingList.Add(new List<Building>());
		}

		updateSoldierList ();
		updateBuildingList ();

		p1_soldierOrder.Clear ();
		p2_soldierOrder.Clear ();
		for (int i = 0; i < 5; i ++) {
			p1_soldierOrder.Add (Vector3.zero);
			p2_soldierOrder.Add (Vector3.zero);
		}
	}


	//this function order every unit to do its destinated task
	public static void orderSoldier(){
//        knownWorld.SetPosition(new Position2D(0, 10), true);
//        knownWorld.SetPosition(new Position2D(1, 10), true);
//        knownWorld.SetPosition(new Position2D(2, 10), true);
//        knownWorld.SetPosition(new Position2D(3, 10), true);
//        knownWorld.SetPosition(new Position2D(4, 10), true);
//        knownWorld.SetPosition(new Position2D(5, 10), true);
//        knownWorld.SetPosition(new Position2D(6, 10), true);
//        knownWorld.SetPosition(new Position2D(6, 7), true);
//        knownWorld.SetPosition(new Position2D(6, 8), true);
		for (int i = 0; i < 5; i ++) {
            Position2D p1End = GridMapUtils.GetTile(p1_soldierOrder[i].x, p1_soldierOrder[i].y);
			//for the player one
			foreach(Soldier s in p1_listOfSoldierLists[i]){
				s.soldierState = (int)p1_soldierOrder[i].z;
                if (p1_soldierOrder[i].z == 0)
                {
                    s.destinatedPos = new Vector2(s.transform.position.x, s.transform.position.y);
                }
                else
                {
                    s.destinatedPos = new Vector2(p1_soldierOrder[i].x, p1_soldierOrder[i].y);
                    Position2D start = GridMapUtils.GetTile(s.transform.position.x, s.transform.position.y);
                    s.nextPathNode = PathFinder.PathFinder.FindPath(knownWorld, start, p1End);
                }
			}
            Position2D p2End = GridMapUtils.GetTile(p1_soldierOrder[i].x, p1_soldierOrder[i].y);
			//for the player 2
			foreach(Soldier s in p2_listOfSoldierLists[i]){
				s.soldierState = (int)p2_soldierOrder[i].z;
                if (p2_soldierOrder[i].z == 0)
                    s.destinatedPos = new Vector2(s.transform.position.x, s.transform.position.y);
                else
                {
                    s.destinatedPos = new Vector2(p2_soldierOrder[i].x, p2_soldierOrder[i].y);
                    Position2D start = GridMapUtils.GetTile(s.transform.position.x, s.transform.position.y);
                    s.nextPathNode = PathFinder.PathFinder.FindPath(knownWorld, start, p2End);
                }
			}

		}

//		foreach (SwordMan s in p1_listOfSoldierLists[0]){
//			s.soldierState = (int)p1_soldierOrder[0].z;
//			if (p1_soldierOrder[0].z==0)
//				s.destinatedPos = new Vector2(s.transform.position.x,s.transform.position.y);
//			else
//				s.destinatedPos = new Vector2(p1_soldierOrder[0].x,p1_soldierOrder[0].y);
//		}
//
//		foreach (Archer s in p1_archerList){
//			s.soldierState = (int)p1_soldierOrder[1].z;
//			if (p1_soldierOrder[1].z==0)
//				s.destinatedPos = new Vector2(s.transform.position.x,s.transform.position.y);
//			else
//				s.destinatedPos = new Vector2(p1_soldierOrder[1].x,p1_soldierOrder[1].y);
//		}
//
//		foreach (HorseMan s in p1_horsemanList){
//			s.soldierState = (int)p1_soldierOrder[2].z;
//			if (p1_soldierOrder[2].z==0)
//				s.destinatedPos = new Vector2(s.transform.position.x,s.transform.position.y);
//			else
//				s.destinatedPos = new Vector2(p1_soldierOrder[2].x,p1_soldierOrder[2].y);
//		}
//
//		foreach (Gladiator s in p1_gladiatorList){
//			s.soldierState = (int)p1_soldierOrder[3].z;
//			if (p1_soldierOrder[3].z==0)
//				s.destinatedPos = new Vector2(s.transform.position.x,s.transform.position.y);
//			else
//				s.destinatedPos = new Vector2(p1_soldierOrder[3].x,p1_soldierOrder[3].y);
//		}
//
//		foreach (Cannon s in p1_cannonList){
//			s.soldierState = (int)p1_soldierOrder[4].z;
//			if (p1_soldierOrder[4].z==0)
//				s.destinatedPos = new Vector2(s.transform.position.x,s.transform.position.y);
//			else
//				s.destinatedPos = new Vector2(p1_soldierOrder[4].x,p1_soldierOrder[4].y);
//		}
//
		//For the player 2
//		foreach (SwordMan s in p2_swordmanList){
//			s.soldierState = (int)p2_soldierOrder[0].z;
//			if (p2_soldierOrder[0].z==0)
//				s.destinatedPos = new Vector2(s.transform.position.x,s.transform.position.y);
//			else
//				s.destinatedPos = new Vector2(p2_soldierOrder[0].x,p2_soldierOrder[0].y);
//		}
//		
//		foreach (Archer s in p2_archerList){
//			s.soldierState = (int)p2_soldierOrder[1].z;
//			if (p2_soldierOrder[1].z==0)
//				s.destinatedPos = new Vector2(s.transform.position.x,s.transform.position.y);
//			else
//				s.destinatedPos = new Vector2(p2_soldierOrder[1].x,p2_soldierOrder[1].y);
//		}
//		
//		foreach (HorseMan s in p2_horsemanList){
//			s.soldierState = (int)p2_soldierOrder[2].z;
//			if (p2_soldierOrder[2].z==0)
//				s.destinatedPos = new Vector2(s.transform.position.x,s.transform.position.y);
//			else
//				s.destinatedPos = new Vector2(p2_soldierOrder[2].x,p2_soldierOrder[2].y);
//		}
//		
//		foreach (Gladiator s in p2_gladiatorList){
//			s.soldierState = (int)p2_soldierOrder[3].z;
//			if (p2_soldierOrder[3].z==0)
//				s.destinatedPos = new Vector2(s.transform.position.x,s.transform.position.y);
//			else
//				s.destinatedPos = new Vector2(p2_soldierOrder[3].x,p2_soldierOrder[3].y);
//		}
//		
//		foreach (Cannon s in p2_cannonList){
//			s.soldierState = (int)p2_soldierOrder[4].z;
//			if (p2_soldierOrder[4].z==0)
//				s.destinatedPos = new Vector2(s.transform.position.x,s.transform.position.y);
//			else
//				s.destinatedPos = new Vector2(p2_soldierOrder[4].x,p2_soldierOrder[4].y);
//		}
//
	}

//This function find all the soldier on the field and add that soldier to its approtiate list.
	void updateSoldierList(){
		//add all the unit on map to the list.
		GameObject[] soldierArray = GameObject.FindGameObjectsWithTag ("Soldier");
		foreach (GameObject s in soldierArray) {
			switch(s.name){
			case "SwordMan":
				if (s.gameObject.GetComponent<Soldier>().isPlayerOne)
					p1_listOfSoldierLists[0].Add(s.gameObject.GetComponent<SwordMan>());
					//p1_swordmanList.Add(s.gameObject.GetComponent<SwordMan>());
				else
					p2_listOfSoldierLists[0].Add(s.gameObject.GetComponent<SwordMan>());
					//p2_swordmanList.Add(s.gameObject.GetComponent<SwordMan>());
				break;
			case "Archer":
				if (s.gameObject.GetComponent<Soldier>().isPlayerOne)
					p1_listOfSoldierLists[1].Add(s.gameObject.GetComponent<Archer>());
					//p1_archerList.Add(s.gameObject.GetComponent<Archer>());
				else
					p2_listOfSoldierLists[1].Add(s.gameObject.GetComponent<Archer>());
				//p2_archerList.Add(s.gameObject.GetComponent<Archer>());
				break;
			case "HorseMan":
				if (s.gameObject.GetComponent<Soldier>().isPlayerOne)
					p1_listOfSoldierLists[2].Add(s.gameObject.GetComponent<HorseMan>());
					//p1_horsemanList.Add(s.gameObject.GetComponent<HorseMan>());
				else
					p2_listOfSoldierLists[2].Add(s.gameObject.GetComponent<HorseMan>());
					//p2_horsemanList.Add(s.gameObject.GetComponent<HorseMan>());
				break;
			case "Gladiator":
				if (s.gameObject.GetComponent<Soldier>().isPlayerOne)
					p1_listOfSoldierLists[3].Add(s.gameObject.GetComponent<Gladiator>());
					//p1_gladiatorList.Add(s.gameObject.GetComponent<Gladiator>());
				else
					p2_listOfSoldierLists[3].Add(s.gameObject.GetComponent<Gladiator>());
					//p2_gladiatorList.Add(s.gameObject.GetComponent<Gladiator>());
				break;
			case "Cannon":
				if (s.gameObject.GetComponent<Soldier>().isPlayerOne)
					p1_listOfSoldierLists[4].Add(s.gameObject.GetComponent<Cannon>());
					//p1_cannonList.Add(s.gameObject.GetComponent<Cannon>());
				else
					p2_listOfSoldierLists[4].Add(s.gameObject.GetComponent<Cannon>());
					//p2_cannonList.Add(s.gameObject.GetComponent<Cannon>());
				break;
			default:
				Debug.Log("Errr,<color=red>WRONG</color>  name");
				break;
			}
		}

	}

	//find all the building on the map and add its to the list. Use when awake.
	void updateBuildingList(){
		GameObject[] buildingArray = GameObject.FindGameObjectsWithTag ("Building");
		foreach (GameObject b in buildingArray) {
			//castle = 0, barrack = 1; tower = 2, wall = 3
			switch(b.name){
			case "Castle":
				if (b.gameObject.GetComponent<Building>().isPlayerOne)
					p1_buildingList[0].Add(b.gameObject.GetComponent<MainCastle>());
				else
					p2_buildingList[0].Add(b.gameObject.GetComponent<MainCastle>());
				break;
			case "Barrack":
				if (b.gameObject.GetComponent<Building>().isPlayerOne)
					p1_buildingList[1].Add(b.gameObject.GetComponent<Barrack>());
				else
					p2_buildingList[1].Add(b.gameObject.GetComponent<Barrack>());
				break;
			case "WatchTower":
				if (b.gameObject.GetComponent<Building>().isPlayerOne)
					p1_buildingList[2].Add(b.gameObject.GetComponent<Tower>());
				else
					p2_buildingList[2].Add(b.gameObject.GetComponent<Tower>());
				break;
			case "Wall":
				if (b.gameObject.GetComponent<Building>().isPlayerOne)
					p1_buildingList[3].Add(b.gameObject.GetComponent<Wall>());
				else
					p2_buildingList[3].Add(b.gameObject.GetComponent<Wall>());
				break;
			default:
				Debug.Log("ERRRR ?????");
				break;
			}
		}

	}

}

