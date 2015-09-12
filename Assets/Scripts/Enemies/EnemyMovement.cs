using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public float Speed = 5f;

	Transform PlayerBase;
	NavMeshAgent nav;
	bool BaseInRange = false;
	bool TowerInRange = false;
	EnemyHealth enemyhealth;

	GameObject TowerGO;
	// Use this for initialization
	void Start () {
		PlayerBase = GameObject.FindGameObjectWithTag ("Base").transform;
		nav = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (TowerInRange && TowerGO != null) {
			nav.SetDestination (TowerGO.transform.position);
			return;
		} 

		nav.SetDestination (PlayerBase.position);

	}

	void OnTriggerStay(Collider Other)
	{
//		if (Other.gameObject.tag.Equals("Base")) {
//			BaseInRange = true;
//		}
		switch (Other.gameObject.tag) {
		case "Base":
			BaseInRange = true;
			break;
		case "Tower":
			TowerInRange = true;
			TowerGO = Other.gameObject;
			break;
		}
	}
}
