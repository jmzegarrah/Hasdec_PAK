using UnityEngine;
using System.Collections;

public class Agregar_Puntos : MonoBehaviour {
	
	public GameObject Server;
	
	string names = "";
	int score = 10;
	
	void Start(){
		if(gameObject.GetComponent<Conexion>()){
			Server = gameObject;
		}	
	}
	
 	void OnGUI (){	
		GUILayout.BeginArea(new Rect(Screen.width/4,Screen.height/8,Screen.width/2,Screen.height/2));
		GUILayout.BeginHorizontal();
		GUILayout.Label("NOMBRE : ");	names = GUILayout.TextArea(names);		
		GUILayout.EndHorizontal();
		if(GUILayout.Button("Enviar")){
			StartCoroutine(Server.GetComponent<Conexion>().Agregar_Puntos(names,score));
			names = "";
		}
		GUILayout.EndArea();
	}
}
