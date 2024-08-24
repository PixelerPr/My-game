using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Item_Random_Spawn : MonoBehaviour
{
    [SerializeField] private Transform[] spawnpoints;
    [SerializeField] public Transform[] items;
    [SerializeField] public List<int> tabu_pos;




    // Start is called before the first frame update
    void Start()
    {
        Random_Spawns();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Random_Spawns()
    {
        foreach(Transform item in items) 
        {
            bool is_Placed = false;
            while (!is_Placed)
            {
				int temp = Random.Range(0, spawnpoints.Length);
				if (!tabu_pos.Contains(temp))
				{
					item.transform.position = spawnpoints[temp].position;
                    is_Placed = true;
                    tabu_pos.Add(temp);
				}
			}
        }

    }
    
    
}
