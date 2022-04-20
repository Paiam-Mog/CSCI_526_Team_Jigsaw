using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public GameObject[] targetObjects;

    private int numberOfTargets;

    public List<bool> listOfSatisfiedTargets = new List<bool>();

    private bool isComplete ;

    // Start is called before the first frame update
    void Start()
    {
        targetObjects = GameObject.FindGameObjectsWithTag("Target");//Find all Target with Tag 'Target'. All available target can be found in Target Controller
        numberOfTargets = targetObjects.Length;
        isComplete = false;
        //Debug.Log("No of Target " + numberOfTargets);
        for (int i = 0; i < numberOfTargets;i++)
        {
            listOfSatisfiedTargets.Add(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        TargetSatisfied();
        isGameCompleted();
    }

    public void TargetSatisfied ()
    {
            for (int i = 0; i< numberOfTargets; i++)
            {
                var isSatisfied = targetObjects[i].GetComponent<Target>().isTargetSatisfied;
                if (isSatisfied)
                {
                    listOfSatisfiedTargets[i] = true;
                    
                    Debug.Log("Target " + targetObjects[i] + " satisfied");
                }
                else
                {
                    listOfSatisfiedTargets[i] = false;
                    
                    Debug.Log("Target " + targetObjects[i] + " satisfied");
                }
               
       
            }
        
    }

    public bool CheckAllTarget()
    {
        //Debug.Log("List of satisfied target:" + listOfSatisfiedTargets[0] + listOfSatisfiedTargets[1]);
        for (int i = 0; i < numberOfTargets; i++)
        {
            if (listOfSatisfiedTargets[i] == false)
            {
                return false;
            }

        }
        return true;
    }

    public void isGameCompleted ()
    {
        Debug.Log("CheckAllTarget " + CheckAllTarget());
        if (CheckAllTarget() && !isComplete)
        {
            //Change it with a common OnLevelComplete
            // this works if more than 2 targets are available. Level Completion popup doesn't appear but the data is available in console
            targetObjects[0].GetComponent<Target>().OnLevelComplete();
            isComplete = true; 
        }
    }

}
