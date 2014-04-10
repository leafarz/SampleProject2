using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	public GameObject cube;				// prefab
	private OnClick [,]oc;				// OnClick scripts of blocks
	private Vector2 vec2;				// position holder
	private Transform t;				// transform of generator

	private int checkCounter=0;			// counter for ending of checking
	private int sameColorCounter=0;		// counter for same colored adjacents

	void Start () {
		t = transform;
		oc = new OnClick[9,9];
		GameObject []go = new GameObject[81];
		for(int i=8,k=0 ; i>=0 ; i--,k++){
			vec2.x=i;
			for(int j=8 ; j>=0 ; j--){
				vec2.y=j;
				go[k]=Instantiate(cube,vec2,Quaternion.identity) as GameObject;
				go[k].transform.parent=t;
				oc[i,j]=go[k].GetComponent<OnClick>();
			}
		}
	}

	//Check sides
	public void CheckSides(Vector2 pos, int type){
		int x = (int)pos.x;
		int y = (int)pos.y;
		oc [x, y].enabled = false;
		checkCounter++;

		if(x!=0 && oc[x-1,y].enabled && oc[x-1,y].type==type){
			sameColorCounter++;
			oc[x-1,y].Check();
		}
		if(x!=8 && oc[x+1,y].enabled && oc[x+1,y].type==type){
			sameColorCounter++;
			oc[x+1,y].Check();
		}
		if(y<8 && oc[x,y+1].enabled && oc[x,y+1].type==type){
			sameColorCounter++;
			oc[x,y+1].Check();
		}
		if(y>0 && oc[x,y-1].enabled && oc[x,y-1].type==type){
			sameColorCounter++;
			oc[x,y-1].Check();
		}

		checkCounter--;

		if (checkCounter == 0){
			// if there are more than 2 same colored adjacent boxes
			if(sameColorCounter>1) {
				//Delete same colored boxes
				for(int i=8 ; i>=0 ; i--){
					for(int j=8 ; j>=0 ; j--){
						if(!oc[i,j].enabled){
							Destroy(oc[i,j].gameObject);
							oc[i,j]=null;
						}
					}
				}
			}else{
				// return them to normal
				for(int i=8 ; i>=0 ; i--){
					for(int j=8 ; j>=0 ; j--) oc[i,j].enabled=true;
				}
			}
			sameColorCounter=0;
			PullDown();
			Reinit();
		}
	}

	// Pulls blocks down if there are spaces
	private void PullDown(){
		for(int i=0 ; i<9 ; i++){
			for(int j=0 ; j<8 ; j++){

				if(oc[i,j]==null){
					int x=i;
					int y=j;
					while(y<9){
						if(oc[x,y]!=null)break;
						y++;
					}
					if(y<9){
						vec2.x=i;
						vec2.y=j;
						oc[x,y].gameObject.transform.position=vec2;
						oc[i,j]=oc[x,y];
						oc[x,y]=null;
					}
				}
			}
		}
	}

	// refills the deleted boxes
	private void Reinit(){
		for(int i=8 ; i>=0 ; i--){
			for(int j=8 ; j>=0 ; j--){
				if(oc[i,j]==null){
					vec2.x=i;
					vec2.y=j;
					GameObject go = Instantiate(cube,vec2,Quaternion.identity) as GameObject;
					go.transform.parent=t;
					oc[i,j]=go.GetComponent<OnClick>();
				}
			}
		}
	}

}
