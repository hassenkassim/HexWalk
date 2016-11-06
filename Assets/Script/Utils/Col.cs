/************************************************
 * HAMOTO Production 2016						*
 * Project: HexWalk								*
 * Authors: Tolga, Mohamed, Dursun, Hassen		*
 * Year: 2016									*
 *************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*
 * This class manages the used colors
 * */
public class Col {


	public readonly static  Color ROT = Color.red;
	public readonly static  Color GRUEN = Color.green;
	public readonly static  Color BLAU = Color.blue;
	public readonly static  Color MAGENTA = Color.magenta;
	public readonly static  Color GRAU = Color.gray;
	public readonly static  Color GELB = Color.yellow;
	public readonly static  Color SCHWARZ = Color.black;
	public readonly static  Color WEISS = Color.white;
	public readonly static Color CYAN = Color.cyan;

	public readonly static Color SELECTEDCOLOR = CYAN;
	public readonly static Color COMPLETEDCOLOR = GRUEN;
	public readonly static Color BLOCKEDCOLOR = GRAU;
	public readonly static Color WORLDBLOCKCOLOR = SCHWARZ;
	public readonly static Color WORLDUNLOCKEDCOLOR = WEISS;
	public readonly static Color ENABLEDCOLOR = WEISS;

	public readonly static Color STANDARDFIELDCOLOR = WEISS;

	public readonly static List<Color> colors = new List<Color>(new Color[]{ Color.green, Color.blue, Color.magenta, Color.yellow}); //This List is used to index the colors and to easily get the next color



	//this gives the nextColor, considering the count of active colors
	public static Color nextColor(Color col, int colorCount){
		int colidx = colors.FindIndex(x => x == col); //finds the index of the current color

		if (colidx == colorCount - 1) { //if the color already reached the amount of used color, set index back to 0 again, otherwise just increment the index
			colidx = 0;
		} else {
			colidx++;
		}
		return colors [colidx];
	}

}
