using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class UiBlocks : MonoBehaviour
{
    //public int counter = 0;
    public Image Image;
    public int ButtonPos = 0;
    public PlayerControl playerControl;
    public List<Sprite> sprites = new List<Sprite>();
    public Sprite Getimagefile()
    {
        gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 1, 1);
        List<BlockType> actionList = playerControl.actionList;

        Sprite newsprite = null;
        BlockType block = actionList[ButtonPos];
        switch (block)
        {
            case BlockType.UP:
                newsprite = sprites[0];
                break;
            case BlockType.LEFT:
                newsprite = sprites[1];
                break;
            case BlockType.DOWN:
                newsprite = sprites[2];
                break;
            case BlockType.RIGHT:
                newsprite = sprites[3];
                break;
            case BlockType.JUMP:
                newsprite = sprites[4];

                break;
            case BlockType.CLIMB:
                newsprite = sprites[5];
                break;
            case BlockType.NONE:
                gameObject.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, 0);
                break;
        }

        return newsprite;
    }
    // Update is called once per frame
    void Update()
    {
        if (playerControl != null)
        {
            //counter += 1;
            Getimagefile();
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Getimagefile();
        }
    }
}
