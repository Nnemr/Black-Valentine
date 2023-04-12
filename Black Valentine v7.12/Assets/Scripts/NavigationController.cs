using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NavigationController : MonoBehaviour {

	static int currentLevel=0;
	public static int getCurrentLevel(){
		return currentLevel;
	}
    public static void addLevel()
    {
        currentLevel++;
        Debug.Log(currentLevel);
    }
	public void GoToMainMenu()
	{
		Debug.Log (currentLevel);

		Debug.Log ("Main");
		Application.LoadLevel (0);
        ++currentLevel;
	}
    public void GoToGameOver()
    {
        currentLevel = 5;
        Application.LoadLevel(currentLevel);
    }
	public void Retry()
	{
		Debug.Log (currentLevel);
		Debug.Log ("Retry");
		Application.LoadLevel (getCurrentLevel());
	} 
	public void GoToFirstLevel1()
	{
		
		currentLevel = 1;
		Debug.Log (currentLevel);
		Debug.Log ("Level 1");
		Application.LoadLevel (currentLevel);

	}
	public void GoToFirstLevel2()
	{
		Debug.Log (currentLevel);
		currentLevel =2;
		Application.LoadLevel (currentLevel);
	}
	public void GoToSecondLevel1()
	{
		Debug.Log (currentLevel);
		currentLevel = 3;
		Application.LoadLevel (currentLevel);
	}
	public void GoToSecondLevel2()
	{
		Debug.Log (currentLevel);
		currentLevel = 4;
		Application.LoadLevel (currentLevel);
	}
	public void GoToThirdLevel1()
	{
		Debug.Log (currentLevel);
		currentLevel = 11;
		Application.LoadLevel (currentLevel);
	}
	public void GoToThirdLevel2()
	{
		Debug.Log (currentLevel);
		currentLevel = 14;
		Application.LoadLevel (currentLevel);
	}
	public void GoToFourthLevel()
	{
		Debug.Log (currentLevel);
		currentLevel = 19;
		Application.LoadLevel (currentLevel);
	}

	public void GoToVictoryScene()
	{
		Debug.Log (currentLevel);
		currentLevel = 6;
		Application.LoadLevel (currentLevel);
	}
	public void Quit()
	{
		
		Application.Quit ();
	}


}
