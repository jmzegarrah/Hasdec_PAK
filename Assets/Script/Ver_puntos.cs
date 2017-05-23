using UnityEngine;
using System.Collections;

public class Ver_puntos : MonoBehaviour {
	public GameObject Server;	
	void Start(){
		if(gameObject.GetComponent<Conexion>()){
			Server = gameObject;
		}	
	}
	void OnGUI (){
		GUILayout.BeginArea(new Rect(Screen.width/4,Screen.height/8,Screen.width/2,Screen.height/2));
		GUILayout.Space(50);
		if(GUILayout.Button("Ver")){
			Server.SendMessage("Ver_Puntos");
		}
		GUILayout.EndArea();
	}
}
