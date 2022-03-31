using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenuScript : MonoBehaviour
{
    Image menuImage;

    public Sprite BaseMenu, jumpingMenu, glidingMenu;
    // Start is called before the first frame update
    void Start()
    {
        menuImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.mousePosition.x >= Screen.width / 2)
        {
            menuImage.sprite = glidingMenu;
        }

        else
        {
            menuImage.sprite = jumpingMenu;
        }
    }
}
