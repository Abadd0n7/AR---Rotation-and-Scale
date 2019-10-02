using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitOnEscape : MonoBehaviour
{
    [SerializeField]
    private Text _exitText;
    private bool areYouSure;
    // Start is called before the first frame update
    void Start()
    {
        _exitText.gameObject.SetActive(false);

        areYouSure = false;
    }

    // Update is called once per frame
    void Update()
    {
        Exit();
    }

    private void Exit()
    {
        if (Input.touchCount > 1)
        {
            areYouSure = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && areYouSure == true)
        {
            Application.Quit();
        }

        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                areYouSure = true;
                _exitText.gameObject.SetActive(true);
            }
            else
            {
                areYouSure = false;
            }
        }
    }
}
