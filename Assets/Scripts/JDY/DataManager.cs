using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonManager<DataManager>
{

	public int health { get { return health; } set { health = Mathf.Clamp(value, 0, 2); } }
	public int ammo { get { return currentAmmo; } set { currentAmmo = Mathf.Clamp(value, 0, maxAmmo); } }

	private int currentAmmo;
	public int maxAmmo;
	public int money;
	public int jumpCount;
	public bool isHat;
	public bool isAmmoBelt;
	public bool isDoubleJump;

	public float bombFliesSpeed;
	public float fishSpeed;
	public float birdSpeed;
}
