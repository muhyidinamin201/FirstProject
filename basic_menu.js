//****** Donations are greatly appreciated.  ******
//****** You can donate directly to Jesse through paypal at  https://www.paypal.me/JEtzler   ******

var GUIEnabled : boolean = false;
  
function Update () {
	if(Input.GetKeyDown("n")) {
   		GUIEnabled = !GUIEnabled;
   	}
}
     
function OnGUI () {
	if(GUIEnabled) {
     		if (GUI.Button (Rect (Screen.width / 2,Screen.height / 2 - 40,80,20), "Help")) {

		}

    		if (GUI.Button (Rect (Screen.width / 2,Screen.height / 2 - 20,80,20), "Options")) {
		
		}

		if (GUI.Button (Rect (Screen.width / 2,Screen.height / 2,80,20), "Exit")) {
			GUIEnabled = !GUIEnabled;
		}
    	}
}