using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerGroundCheck : MonoBehaviour
{
    public int maxJumps;
    public int maxJumpsTemp;
    
    #region Collision
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            maxJumpsTemp = maxJumps;
        }
    }
    #endregion
}
