using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCards : MonoBehaviour
{
    // Initialize required variables
    [SerializeField]
    private Transform panel;

    [SerializeField]
    private GameObject card;

    // Awake is called before the first frame update
    // Populate scene with cards displayed on a panel
    // using a for loop
    void Awake()
    {
        for(int i = 0; i < 16; i++)
        {
            GameObject gameCard = Instantiate(card);
            gameCard.name = "card" + i;
            gameCard.transform.SetParent(panel, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}