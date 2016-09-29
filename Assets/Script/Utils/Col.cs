using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Col {

	public readonly static  Color ROT = Color.red;
	public readonly static  Color GRUEN = Color.green;
	public readonly static  Color BLAU = Color.blue;
	public readonly static  Color MAGENTA = Color.magenta;
	public readonly static  Color GRAU = Color.gray;
	public readonly static  Color GELB = Color.yellow;
	public readonly static  Color SCHWARZ = Color.black;
	public readonly static  Color WEISS = Color.white;

	public readonly static List<Color> colors = new List<Color>(new Color[]{ Color.green, Color.blue, Color.magenta, Color.yellow}); 


	public static Color nextColor(Color col){
		if (col.Equals (GRUEN))
			return BLAU;
		if (col.Equals (BLAU))
			return MAGENTA;
		if (col.Equals (MAGENTA))
			return GELB;
		if (col.Equals (GELB))
			return GRUEN;

		Debug.Log ("COLOR: " + col + " !!");
		return SCHWARZ;
	}


}
