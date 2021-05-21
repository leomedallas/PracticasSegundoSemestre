using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Checkpoint[] checkpoints;
    static int currentCheckpoint;
    public int CurrentCheckpoint
    {
        get
        {
            return currentCheckpoint;
        }
        set
        {
            currentCheckpoint = value;
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (checkpoints[i].id <= currentCheckpoint)
                {
                    checkpoints[i].gameObject.SetActive(false);
                }
            }
        }

    }

    public Checkpoint GetActiveCheckpoint()
    {
        //Opcion A: Necesitan librería de System
        //return Array.Find(checkpoints, (checkpoint) => checkpoint.id==currentCheckpoint);
        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (checkpoints[i].id <= currentCheckpoint)
            {
                return checkpoints[i];
            }
        }

        return null;
    }
}
