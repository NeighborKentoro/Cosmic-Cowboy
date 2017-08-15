using UnityEngine;
using System.Collections;

//interface for object that can be "damaged", they have health
public interface IsDamagable<T> 
{
	//set object's health
	void setHealth(T health);
	//return object's health
	T getHealth();
	//a function for when the object takes damage
	void takeDamage(T damageTaken);


}
