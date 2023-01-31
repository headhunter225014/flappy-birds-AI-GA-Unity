//Damir Zababuryn ID : 22022843
//Marko Sudar ID: 22022844
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopManager : MonoBehaviour {
	
	//ref to bot and it's start posr
	public GameObject botPrefab;
	public GameObject startPos;
	public int popSize = 50;
	//list for pop as it could change
	List<GameObject> pop = new List<GameObject>();
	public static float elapsed = 0;
	public float trialTime = 5;
	int generation = 1;
	public int scale = 5;

	GUIStyle gui = new GUIStyle();
	//simple GUI for stats on screen
	void OnGUI()
	{
		gui.fontSize = 20;
		gui.normal.textColor = Color.white;
		GUI.BeginGroup (new Rect (10, 10, 250, 150));
		GUI.Box (new Rect (0,0,140,140), "Stats: ", gui);
		GUI.Label(new Rect (10,25,200,30), "1) Generation: " + generation, gui);
		GUI.Label(new Rect (10,50,200,30), string.Format("2) Time: {0:0.00}",elapsed), gui);
		GUI.Label(new Rect (10,75,200,30), "3) Population: " + pop.Count, gui);
		GUI.EndGroup ();
	}


	// Birds are created
	void Start () {
		for(int i = 0; i < popSize; i++)
		{
			GameObject gobject = Instantiate(botPrefab, startPos.transform.position, this.transform.rotation);
			gobject.GetComponent<Main>().Init();
			pop.Add(gobject);
		}

		Time.timeScale = scale;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//going through trial time
		elapsed += Time.deltaTime;
		if(elapsed >= trialTime)
		{
			BreedNewPopulation();
			elapsed = 0;
		}
	}
	
	void BreedNewPopulation()
	{
		//sorting pop by distance travelled
		List<GameObject> sortedList = pop.OrderBy(o => (o.GetComponent<Main>().distTravelled )).ToList();
		pop.Clear();
		//sorted 25% of the pop
		//sortedList.Count / 2.0f 50% 
		//sortedList.Count / 10.0f 10%
		for (int i = (int) (3 * sortedList.Count / 4.0f) - 1; i < sortedList.Count - 1; i++)
		{
			pop.Add(Breed(sortedList[i], sortedList[i + 1]));
			pop.Add(Breed(sortedList[i + 1], sortedList[i]));
			pop.Add(Breed(sortedList[i], sortedList[i + 1]));
			pop.Add(Breed(sortedList[i + 1], sortedList[i]));
		}
		
		//destroys parents and previous pop
		for(int i = 0; i < sortedList.Count; i++)
		{
			Destroy(sortedList[i]);
		}
		generation++;
	}
	
	//breeding 
	GameObject Breed(GameObject parent1, GameObject parent2)
	{
		//creating offspring and giving it a brain
		GameObject offspring = Instantiate(botPrefab, startPos.transform.position, this.transform.rotation);
		Main b = offspring.GetComponent<Main>();
		if(Random.Range(0,100) == 1) //mutate 1 in 100
		{
			//option for mutation
			b.Init();
			b.dna.Mutation();
		}
		else
		{ 
			//combining paranets dna
			b.Init();
			b.dna.Combine(parent1.GetComponent<Main>().dna,parent2.GetComponent<Main>().dna);
		}
		return offspring;
	}

	
	
	
}
