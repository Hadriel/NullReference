using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	bool TowerInAttackRange= false;
	GameObject TowerGA;
	NavMeshAgent nav;
	// Use this for initialization
	void Start () {
		nav = transform.parent.GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (TowerInAttackRange && TowerGA != null) {
			nav.Stop ();
			Destroy (TowerGA, 3f);
		} else {
			nav.Resume();
		}
	}

	void OnTriggerStay(Collider Other)
	{

		if (Other.gameObject.tag.Equals("Tower")) {
			TowerInAttackRange = true;
			TowerGA = Other.gameObject;
		}
	}
}
