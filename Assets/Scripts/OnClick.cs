using UnityEngine;
using System.Collections;

public class OnClick : MonoBehaviour {
	public int type;
	private Game g;
	private Transform t;

	void Start(){
		t = transform;
		g = t.parent.GetComponent<Game> ();
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
		g.CheckSides (t.position, type);
	}

	void OnMouseDown(){
		Check();
	}
}
