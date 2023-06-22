using Controllers;
using Keys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velcoityAnim : MonoBehaviour
{
    [SerializeField]
    PlayerAnimationController animationController;
    [SerializeField]
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rb.velocity);
        if (rb.velocity != Vector3.zero)
        {
            animationController.PlayAnimation(new XZInputParams()
            {

                XValue = 0.5f,
                ZValue = 0.5f
                //InputValues = new Vector2(joystick.Horizontal, joystick.Vertical)
            });
        }
        

    }
}
