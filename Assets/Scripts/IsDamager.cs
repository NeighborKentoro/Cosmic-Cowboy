using UnityEngine;
using System.Collections;
//common interface for classes that can deal damage
public interface IsDamager<T> {

	//sets damage of object
	void setDamage(T damage);
	//returns the damage of the object
	T getDamage();
}
