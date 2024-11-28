using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonManager<DataManager>
{

	public int HP { get { return currentHP; } set { currentHP = Mathf.Clamp(value, 0, 2); } }
	public int currentHP;
	public int ammo { get { return currentAmmo; } set { currentAmmo = Mathf.Clamp(value, 0, maxAmmo); } }

	private int currentAmmo;
	public int maxAmmo;
	public int money;
	public int jumpCount;
	public bool isHat;
	public bool isAmmoBelt;
	public bool isDoubleJump;
	public int triggerOn;


	public float bombFliesSpeed;
	public float fishSpeed;
	public float birdSpeed;

	public int fliesMaxCount;
	public int fishMaxCount;
	public int birdMaxCount;
	public int birdBlackMaxCount;
}
