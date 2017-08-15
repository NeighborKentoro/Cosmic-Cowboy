using UnityEngine;
using System.Collections;
//a common interface for classes that can "die"
public interface IsKillable 
{
	//a function to check if the object is "dead"
	bool isDead();
	//function to deal with object's death
	void Death();


}
