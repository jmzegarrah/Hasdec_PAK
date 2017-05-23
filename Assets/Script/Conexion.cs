using UnityEngine;
using System.Collections;
 
public class Conexion : MonoBehaviour{
    string Agregar_Puntos_PHP = "http://localhost/agregarpuntos.php";
    string Ver_Puntos_PHP = "http://localhost/verpuntos.php";
	string Seguridad = "ff4ff8ff99s5s1af5as99vc8d1ny8";
	
    public IEnumerator Agregar_Puntos(string nombre,int puntos){	
        if(nombre != ""){
		string formulario = Agregar_Puntos_PHP + "?" + "name=" + nombre + "&score=" + puntos.ToString() + "&key=" + Seguridad;		
        WWW datos = new WWW(formulario); 
		yield return datos;
		if(datos.error != null) print("Error: " + datos.error); else{ print("Tu Registro fue Exitoso!"); }		
		}else{
			print("No hay nombre!");
		}
    }
 
    public IEnumerator Ver_Puntos(){
        WWW verpuntos = new WWW(Ver_Puntos_PHP);
        yield return verpuntos;
 
        if (verpuntos.error != null){
            print("Error: " + verpuntos.error);
        }
        else{
			print ("Tu peticion Fue Exitosa!");
			gameObject.GetComponent<visualizar>().ObtenerResultado(verpuntos.text); 
        }
    }
 
}