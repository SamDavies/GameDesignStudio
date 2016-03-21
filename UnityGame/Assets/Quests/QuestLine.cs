using UnityEngine;
using System.Collections;

public class QuestLine : MonoBehaviour {

	public Quest[] questLine;

	private int currentQuestIndex = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// check if any quests remain
		if(currentQuestIndex < questLine.Length) {
			// check if the current quest is complete
			if(questLine[currentQuestIndex].isComplete()) {
				currentQuestIndex += 1;
			}
		}
	}
}
