using UnityEngine;
using System.Collections;

public class OnClick : MonoBehaviour {
	public int type;
	private Script1 s;
	private Transform t;

	void Start(){
		t = transform;
		s = t.parent.GetComponent<Script1> ();
		type = Random.Range (1, 4);
		switch (type) {
			case 1:{
				renderer.material.color=Color.red;
				break;
			}case 2:{
				renderer.material.color=Color.green;
				break;
			}case 3:{
				renderer.material.color=Color.yellow;
				break;
			}
		}
	}

	public void Check(){
		s.CheckSides (t.position, type);
	}

	void OnMouseDown(){
		Check();
	}
}
