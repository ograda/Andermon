using UnityEngine;
using System.Collections;

public static class Common {
	//Bunch of common methods

	public static void IntSwap(ref int first, ref int second){
		int swap; //This is faster than XOR swap
		swap = first;
		first = second;
		second = swap;
	}

	public static int IntMax(int first, int second){
		return (first > second) ? first : second;
	}

	public static int IntMin(int first, int second){
		return (first < second) ? first : second;
	}
}
