//Damir Zababuryn ID : 22022843
//Marko Sudar ID: 22022844
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA {

	List<int> genes = new List<int>();
	int dnaL = 0;
	int maxV = 0;
	
	//constructor sets random values to genes
	public DNA(int l, int v)
	{
		dnaL = l;
		maxV = v;
		SetRandom();
	}
	
	//sets random values for genes
	public void SetRandom()
	{
		genes.Clear();
		for(int i = 0; i < dnaL; i++)
		{
			//because we need to store force which is both up and down force, we need both positive and negative values
			genes.Add(Random.Range(-maxV, maxV));
		}
	}
	
	//combines genes from parents 
	public void Combine(DNA dna1, DNA dna2)
	{
		for(int i = 0; i < dnaL; i++)
		{
			//genes are picked randomly 
			genes[i] = Random.Range(0,20) < 10 ? dna1.genes[i] : dna2.genes[i];
		}
	}

	//getter that returns specific gene
	public int GetGene(int position)
	{
		return genes[position];
	}
	
	//mutation as in real life occurs randomly in generations and can have either positive or negative affect
	public void Mutation()
	{
		//selects random gene and changes it values to random as well
		genes[Random.Range(0,dnaL)] = Random.Range(-maxV, maxV);
	}
}
