using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCollider : MonoBehaviour
{

    public void Jump()
    {
        GetComponent<Animator>().SetTrigger("jump");
    }
    

  

    
}
