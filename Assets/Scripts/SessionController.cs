using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SessionController : MonoBehaviour
{
    private ARSession _session;
    private ARSessionOrigin _sessionOrigin;
    private ARRaycastManager _raycastManager;
    private ARPlaneManager _planeManager;

    private List<ARRaycastHit> _hits;
    private List<PrefabsToInstantiate> prefabsToInstantiate;

    private GameStatus _gameStatus;
    private bool isObjectInstantiated;


    [SerializeField]
    private GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        _session = GetComponent<ARSession>();
        _sessionOrigin = GetComponent<ARSessionOrigin>();
        _raycastManager = GetComponent<ARRaycastManager>();
        _planeManager = GetComponent<ARPlaneManager>();

        _hits = new List<ARRaycastHit>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _planeManager.enabled = !_planeManager.enabled;
        _gameStatus = GameStatus.Default;

        if (_gameStatus == GameStatus.Default)
        {
            InstantiateObjectOnDetectedPlane();
        }
        //else if (_gameStatus == GameStatus.Gamemaster)
        //{

        //}
        //else if (_gameStatus == GameStatus.Random)
        //{

        //}
    }

    private void InstantiateObjectOnDetectedPlane()
    {
        if (Input.touchCount > 1)
        {
            Touch touch_1 = Input.GetTouch(0);
            //Touch touch_2 = Input.GetTouch(1);

            //if (touch_1.position == touch_2.position)
            //{
                if (touch_1.phase == TouchPhase.Began && _raycastManager.Raycast(touch_1.position, _hits, TrackableType.PlaneWithinPolygon) && isObjectInstantiated == false)
                {
                    Pose pose = _hits[0].pose;

                    var obj = Instantiate(go, pose.position, pose.rotation);
                    _sessionOrigin.MakeContentAppearAt(obj.transform, obj.transform.position, obj.transform.rotation);

                    isObjectInstantiated = true;

                    SetAllPlanesActive(false);

                    StartCoroutine(AfterInstantiation(obj));
                }
            //}
        }
    }
    
    private void SetAllPlanesActive(bool value)
    {
        foreach(var plane in _planeManager.trackables)
        {
            plane.gameObject.SetActive(value);
        }
    }

    private void ResetButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void GameMasterMode()
    {

    }
    private void AllRandomMode()
    {

    }

    /*
        if (mbs_value == ModeButtonState.Gamemaster)
        {
            CircularMenuButton.SetActive(true);
            ModePanel.SetActive(false);
            BackButton.gameObject.SetActive(true);
            planeManager.enabled = true;
            //planeManager.enabled = !planeManager.enabled;
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began && raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinBounds) && isInstantiated == false)
                {

                    planeManager.enabled = false;
                    Pose pose = hits[0].pose;

                    GameObject btnPrefab = null;

                    if (currentButtonValue == ButtonValue.Rain)
                    {
                        btnPrefab = RainPrefab;
                        
                    }
                    else if (currentButtonValue == ButtonValue.Blizzard)
                    {
                        btnPrefab = BlizzardPrefab;
                    }
                    else if (currentButtonValue == ButtonValue.Fire)
                    {
                        btnPrefab = FirePrefab;
                    }
                    else if (currentButtonValue == ButtonValue.Tornado)
                    {
                        btnPrefab = TornadoPrefab;
                    }
                    else if (currentButtonValue == ButtonValue.Meteor)
                    {
                        btnPrefab = MeteorPrefab;
                    }
                    else if (currentButtonValue == ButtonValue.Tsunami)
                    {
                        //btnPrefab = TsunamiPrefab;
                    }
                    else if (currentButtonValue == ButtonValue.Plague)
                    {
                        btnPrefab = PlaguePrefab;
                    }

                    var go = Instantiate(btnPrefab, pose.position, pose.rotation);
                    sessionOrigin.MakeContentAppearAt(go.transform, go.transform.position, go.transform.rotation);
                    CircularMenuButton.SetActive(false);
                    isInstantiated = true;
                    //foreach (var button in StateButtons)
                    //{
                    //    button.gameObject.SetActive(false);
                    //}

                    audioSource.volume = 0.1f;

                    SetAllPlanesActive(false);

                    StartCoroutine(OnDestroyPrefab(go));
                    StartCoroutine(ChooseNewEffect());
                }
            }
        }
        else if(mbs_value == ModeButtonState.Random)
        {
            CircularMenuButton.SetActive(false);
            ModePanel.SetActive(false);
            BackButton.gameObject.SetActive(true);

            planeManager.enabled = !planeManager.enabled;
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began && raycastManager.Raycast(touch.position, hits, TrackableType.Planes) && isInstantiated == false)
                {

                    planeManager.enabled = false;
                    Pose pose = hits[0].pose;

                    int i = Random.Range(0, PrefabList.Count);
                    var go = Instantiate(PrefabList[i], pose.position, pose.rotation);
                    sessionOrigin.MakeContentAppearAt(go.transform, go.transform.position, go.transform.rotation);
                    isInstantiated = true;
                    audioSource.volume = 0.1f;
                    SetAllPlanesActive(false);
                    StartCoroutine(OnDestroyPrefab(go));
                    StartCoroutine(ChooseNewEffect());
                }
            }
        }
        else if (mbs_value == ModeButtonState.GoBack)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if(mbs_value == ModeButtonState.Nostate)
        {
            BackButton.gameObject.SetActive(false);
        }
     */

    IEnumerator AfterInstantiation(GameObject go)
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(go);
        yield return new WaitForSeconds(2.5f);
        SetAllPlanesActive(true);
        yield return new WaitForSeconds(1f);
        isObjectInstantiated = false;
    }
}
