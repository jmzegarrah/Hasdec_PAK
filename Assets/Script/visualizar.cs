using UnityEngine;
using System.Collections;

public class visualizar : MonoBehaviour {
	string result = "";
	public GUISkin Stylo;
	
	public void ObtenerResultado (string resultado){
		result = resultado;
	}
	
	void OnGUI () {
		GUI.skin = Stylo;
		Rect Posicion = new Rect(Screen.width/4,Screen.height/4,Screen.width/2,Screen.height/2);
		Posicion = GUI.Window(1,Posicion,Visualizador,"Visualizador de Resultados!");	
	}
	
	void Visualizador (int WindowID){
		GUI.skin = Stylo;
		GUILayout.Box(result);
	}
}
