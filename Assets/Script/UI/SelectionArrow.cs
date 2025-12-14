
using UnityEngine;
using UnityEngine.UI;


public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] optons;
    [SerializeField] private AudioClip selectionSound;
    [SerializeField] private AudioClip interactSound;
    private RectTransform rect;
    private int currentPositon;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        //Change position of the selection arrow
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePositon(-1);
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangePositon(1);

        //interact with options
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
            Interact();
            
    }

    private void ChangePositon(int _change)
    {
        currentPositon += _change;
        if(_change != 0) 
            SoundManager.instance.PlaySound(selectionSound);

        if (currentPositon < 0)
            currentPositon = optons.Length - 1;
        else if (currentPositon > optons.Length - 1)
            currentPositon = 0;
        //assign the y position of the current option to the arrow(basically moving up and down)

        rect.position = new Vector3(rect.position.x, optons[currentPositon].position.y, 0);
    }


    private void Interact() {

        SoundManager.instance.PlaySound(interactSound);

        //access the button component on each option and call it's function
        optons[currentPositon].GetComponent<Button>().onClick.Invoke();
    }


}
