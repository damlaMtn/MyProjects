using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class SwipeTrail : MonoBehaviour
{
    public GameObject trailPrefab;
    public GameObject eraserPrefab;
    public GameObject brushPrefab;
    public GameObject paintBrushPrefab;
    public GameObject flameBrushPrefab;
    public GameObject clearPlane;
    public float limit = 20f;

    GameObject thisTrail;
    GameObject eraserTrail;
    GameObject brushTrail;
    GameObject paintBrushTrail;
    GameObject flameBrushTrail;
    Vector3 startPos;
    Plane objPlane;

    bool pencil = true;
    bool eraser = false;
    bool brush = false;
    bool paintBrush = false;
    bool flameBrush = false;

    //particles
    public ParticleSystem particle;
    public ParticleSystem sprayparticle;
    public ParticleSystem paintroller;
    public ParticleSystem pigtail;

    private ParticleSystem.EmissionModule em;
    private ParticleSystem.EmissionModule em2;
    private ParticleSystem.EmissionModule em3;
    private ParticleSystem.EmissionModule em4;

    private ParticleSystem.MinMaxCurve rate;
    private ParticleSystem.MinMaxCurve rate2;
    private ParticleSystem.MinMaxCurve rate3;
    private ParticleSystem.MinMaxCurve rate4;
    Touch t;
    bool isClick = false;


    void Awake()
    {
        Clear();
    }
    void Start()
    {

        em = particle.emission;
        rate = em.rate;

        em2 = sprayparticle.emission;
        rate2 = em2.rate;

        em3 = paintroller.emission;
        rate3 = em3.rate;

        em4 = pigtail.emission;
        rate4 = em4.rate;

        rate.mode = ParticleSystemCurveMode.Constant;
        rate2.mode = ParticleSystemCurveMode.Constant;
        rate3.mode = ParticleSystemCurveMode.Constant;
        rate4.mode = ParticleSystemCurveMode.Constant;

        rate = em.rate;
        rate.constantMin = 0f;
        rate.constantMax = 0f;
        em.rate = rate;

        rate2 = em2.rate;
        rate2.constantMin = 0f;
        rate2.constantMax = 0f;
        em2.rate = rate2;

        rate3 = em3.rate;
        rate3.constantMin = 0f;
        rate3.constantMax = 0f;
        em3.rate = rate3;

        rate4 = em4.rate;
        rate4.constantMin = 0f;
        rate4.constantMax = 0f;
        em4.rate = rate4;

        objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);

        particle.Stop();
        sprayparticle.Stop();
        paintroller.Stop();
        pigtail.Stop();
    }

    void Update()
    {
        //if (!EventSystem.current.IsPointerOverGameObject(t.fingerId))
        //{
        if (Input.touchCount == 1)
        {
            if (Input.mousePosition.x > Screen.width / 100 * limit && Input.mousePosition.x < Screen.width - (Screen.width / 100 * limit))
                {
                    #region pencil

                    if (pencil)
                    {
                        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) ||
                    Input.GetMouseButtonDown(0))
                        {
                            isClick = true;
                            thisTrail = (GameObject)Instantiate(trailPrefab,
                                               this.transform.position,
                                               Quaternion.identity);
                            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                            float rayDistance;
                            if (objPlane.Raycast(mRay, out rayDistance))
                                startPos = mRay.GetPoint(rayDistance);
                        }
                        else if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                        || Input.GetMouseButton(0)) && isClick)
                        {
                            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                            float rayDistance;
                            if (objPlane.Raycast(mRay, out rayDistance))
                                thisTrail.transform.position = mRay.GetPoint(rayDistance);
                        }
                        else if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                        || Input.GetMouseButton(0) && !isClick))
                        {
                            isClick = true;
                            thisTrail = (GameObject)Instantiate(trailPrefab,
                                               this.transform.position,
                                               Quaternion.identity);
                            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                            float rayDistance;
                            if (objPlane.Raycast(mRay, out rayDistance))
                                startPos = mRay.GetPoint(rayDistance);
                        }
                        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) ||
                        Input.GetMouseButtonUp(0) && isClick)
                        {
                            //if (Vector3.Distance(thisTrail.transform.position, startPos) < 0.1)
                            GameObject[] allObj = GameObject.FindGameObjectsWithTag("pencil");
                            for (int i = 0; i < allObj.Length; i++)
                            {
                                Destroy(allObj[i]);
                            }
                            isClick = false;
                        }
                    }

                    #endregion

                    #region eraser
                    else if (eraser)
                    {
                        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) ||
                Input.GetMouseButtonDown(0))
                        {
                            isClick = true;
                            eraserTrail = (GameObject)Instantiate(eraserPrefab,
                                               this.transform.position,
                                               Quaternion.identity);
                            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                            float rayDistance;
                            if (objPlane.Raycast(mRay, out rayDistance))
                                startPos = mRay.GetPoint(rayDistance);
                        }
                        else if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                        || Input.GetMouseButton(0) && isClick))
                        {
                            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                            float rayDistance;
                            if (objPlane.Raycast(mRay, out rayDistance))
                                eraserTrail.transform.position = mRay.GetPoint(rayDistance);
                        }
                        else if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                       || Input.GetMouseButton(0) && !isClick))
                        {
                            isClick = true;
                            eraserTrail = (GameObject)Instantiate(eraserPrefab,
                                               this.transform.position,
                                               Quaternion.identity);
                            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                            float rayDistance;
                            if (objPlane.Raycast(mRay, out rayDistance))
                                startPos = mRay.GetPoint(rayDistance);
                        }
                        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) ||
                        Input.GetMouseButtonUp(0) && isClick)
                        {
                            GameObject[] allObj = GameObject.FindGameObjectsWithTag("eraser");
                            for (int i = 0; i < allObj.Length; i++)
                            {
                                Destroy(allObj[i]);
                            }
                            isClick = false;
                        }
                    }
                    #endregion

                    #region brush

                    else if (brush)
                    {
                        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) ||
            Input.GetMouseButtonDown(0))
                        {
                            isClick = true;
                            brushTrail = (GameObject)Instantiate(brushPrefab,
                                               this.transform.position,
                                               Quaternion.identity);
                            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                            float rayDistance;
                            if (objPlane.Raycast(mRay, out rayDistance))
                                startPos = mRay.GetPoint(rayDistance);
                        }
                        else if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                        || Input.GetMouseButton(0) && isClick))
                        {
                            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                            float rayDistance;
                            if (objPlane.Raycast(mRay, out rayDistance))
                                brushTrail.transform.position = mRay.GetPoint(rayDistance);
                        }
                        else if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                       || Input.GetMouseButton(0) && !isClick))
                        {
                            isClick = true;
                            brushTrail = (GameObject)Instantiate(brushPrefab,
                                               this.transform.position,
                                               Quaternion.identity);
                            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                            float rayDistance;
                            if (objPlane.Raycast(mRay, out rayDistance))
                                startPos = mRay.GetPoint(rayDistance);
                        }
                        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) ||
                        Input.GetMouseButtonUp(0) && isClick)
                        {
                            GameObject[] allObj = GameObject.FindGameObjectsWithTag("brush");
                            for (int i = 0; i < allObj.Length; i++)
                            {
                                Destroy(allObj[i]);
                            }
                            isClick = false;
                        }
                    }

                    #endregion

                    #region paintbrush

                    else if (paintBrush)
                    {
                        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) ||
            Input.GetMouseButtonDown(0))
                        {
                            isClick = true;
                            paintBrushTrail = (GameObject)Instantiate(paintBrushPrefab,
                                               this.transform.position,
                                               Quaternion.identity);
                            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                            float rayDistance;
                            if (objPlane.Raycast(mRay, out rayDistance))
                                startPos = mRay.GetPoint(rayDistance);
                            paintBrushTrail.SetActive(false);
                        }
                        else if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                        || Input.GetMouseButton(0) && isClick))
                        {
                            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                            float rayDistance;
                            if (objPlane.Raycast(mRay, out rayDistance))
                                paintBrushTrail.transform.position = mRay.GetPoint(rayDistance);
                            paintBrushTrail.SetActive(true);
                        }
                        else if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                       || Input.GetMouseButton(0) && !isClick))
                        {
                            isClick = true;
                            paintBrushTrail = (GameObject)Instantiate(paintBrushPrefab,
                                               this.transform.position,
                                               Quaternion.identity);
                            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                            float rayDistance;
                            if (objPlane.Raycast(mRay, out rayDistance))
                                startPos = mRay.GetPoint(rayDistance);
                            paintBrushTrail.SetActive(false);
                        }
                        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) ||
                        Input.GetMouseButtonUp(0) && isClick)
                        {
                            isClick = false;
                            Destroy(paintBrushTrail);
                            paintBrushTrail.SetActive(false);
                        }
                    }

                    #endregion

                    #region flamebrush

                    else if (flameBrush)
                    {
                        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) ||
                Input.GetMouseButtonDown(0))
                        {
                            isClick = true;
                            flameBrushTrail = (GameObject)Instantiate(flameBrushPrefab,
                                               this.transform.position,
                                               Quaternion.identity);
                            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                            float rayDistance;
                            if (objPlane.Raycast(mRay, out rayDistance))
                                startPos = mRay.GetPoint(rayDistance);
                            flameBrushTrail.SetActive(false);
                        }
                        else if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                        || Input.GetMouseButton(0) && isClick))
                        {
                            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                            float rayDistance;
                            if (objPlane.Raycast(mRay, out rayDistance))
                                flameBrushTrail.transform.position = mRay.GetPoint(rayDistance);
                            flameBrushTrail.SetActive(true);
                        }
                        else if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                       || Input.GetMouseButton(0) && !isClick))
                        {
                            isClick = true;
                            flameBrushTrail = (GameObject)Instantiate(flameBrushPrefab,
                                               this.transform.position,
                                               Quaternion.identity);
                            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                            float rayDistance;
                            if (objPlane.Raycast(mRay, out rayDistance))
                                startPos = mRay.GetPoint(rayDistance);
                            flameBrushTrail.SetActive(false);
                        }
                        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) ||
                        Input.GetMouseButtonUp(0) && isClick)
                        {
                            isClick = false;
                            Destroy(flameBrushTrail);
                            flameBrushTrail.SetActive(false);
                        }
                    }

                    #endregion

                    #region paintroller

                    if (paintroller)
                    {
                        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) ||
                        Input.GetMouseButtonUp(0))
                        {
                            rate3 = em3.rate;
                            rate3.constantMin = 0f;
                            rate3.constantMax = 0f;
                            em3.rate = rate3;
                        }
                        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                        || Input.GetMouseButton(0))
                        {
                            rate3 = em3.rate;
                            rate3.constantMin = 10f;
                            rate3.constantMax = 10f;
                            em3.rate = rate3;
                        }

                        else
                        {
                            rate3 = em3.rate;
                            rate3.constantMin = 0f;
                            rate3.constantMax = 0f;
                            em3.rate = rate3;
                        }
                    }

                    #endregion

                    #region sprayparticle

                    if (sprayparticle)
                    {
                        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) ||
                        Input.GetMouseButtonUp(0))
                        {
                            rate2 = em2.rate;
                            rate2.constantMin = 0f;
                            rate2.constantMax = 0f;
                            em2.rate = rate2;
                        }
                        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                        || Input.GetMouseButton(0))
                        {
                            rate2 = em2.rate;
                            rate2.constantMin = 1000f;
                            rate2.constantMax = 1000f;
                            em2.rate = rate2;
                        }

                        else
                        {
                            rate2 = em2.rate;
                            rate2.constantMin = 0f;
                            rate2.constantMax = 0f;
                            em2.rate = rate2;
                        }
                    }

                    #endregion

                    #region particle

                    if (particle)
                    {
                        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) ||
                        Input.GetMouseButtonUp(0))
                        {
                            rate = em.rate;
                            rate.constantMin = 0f;
                            rate.constantMax = 0f;
                            em.rate = rate;
                        }
                        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                        || Input.GetMouseButton(0))
                        {
                            rate = em.rate;
                            rate.constantMin = 1000f;
                            rate.constantMax = 1000f;
                            em.rate = rate;
                        }

                        else
                        {
                            rate = em.rate;
                            rate.constantMin = 0f;
                            rate.constantMax = 0f;
                            em.rate = rate;
                        }
                    }

                    if (pigtail)
                    {
                        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) ||
                            Input.GetMouseButtonUp(0))
                        {
                            rate4 = em4.rate;
                            rate4.constantMin = 0f;
                            rate2.constantMax = 0f;
                            em4.rate = rate4;
                        }
                        else if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                        || Input.GetMouseButton(0))
                        {
                            rate4 = em4.rate;
                            rate4.constantMin = 300f;
                            rate4.constantMax = 300f;
                            em4.rate = rate4;
                        }

                        else
                        {
                            rate4 = em4.rate;
                            rate4.constantMin = 0f;
                            rate4.constantMax = 0f;
                            em4.rate = rate4;
                        }
                    }

                    #endregion

                    else
                    {
                        pencil = true;
                        eraser = false;
                        brush = false;
                        paintBrush = false;
                        flameBrush = false;
                        Destroy(flameBrushTrail);
                        Destroy(paintBrushTrail);
                        particle.Stop();
                        sprayparticle.Stop();
                        paintroller.Stop();
                        pigtail.Stop();
                    }
                }
            }

            else
            {
                if (pencil)
                {
                    isClick = false;
                    Destroy(thisTrail);
                }
                else if (eraser)
                {
                    isClick = false;
                    Destroy(eraserTrail);
                }
                else if (brush)
                {
                    isClick = false;
                    Destroy(brushTrail);
                }
                else if (paintBrush)
                {
                    isClick = false;
                    Destroy(paintBrushTrail);
                }
                else if (flameBrush)
                {
                    isClick = false;
                    Destroy(flameBrushTrail);
                }

                if (paintroller)
                {
                    rate3 = em3.rate;
                    rate3.constantMin = 0f;
                    rate3.constantMax = 0f;
                    em3.rate = rate3;
                }
                if (sprayparticle)
                {
                    rate2 = em2.rate;
                    rate2.constantMin = 0f;
                    rate2.constantMax = 0f;
                    em2.rate = rate2;
                }
                if (particle)
                {
                    rate = em.rate;
                    rate.constantMin = 0f;
                    rate.constantMax = 0f;
                    em.rate = rate;
                }
                if (pigtail)
                {
                    rate4 = em4.rate;
                    rate4.constantMin = 0f;
                    rate4.constantMax = 0f;
                    em4.rate = rate4;
                }
           // }
        }
    }

    public void Clear()
    {
        clearPlane.gameObject.SetActive(true);
        StartCoroutine(waitforseconds());
    }

    public IEnumerator waitforseconds()
    {
        yield return new WaitForSeconds(0.5f);
        clearPlane.gameObject.SetActive(false);
    }

    public void Erase()
    {
        pencil = false;
        eraser = true;
        brush = false;
        paintBrush = false;
        //paintRoller = false;
        particle.Stop();
        sprayparticle.Stop();
        paintroller.Stop();
        pigtail.Stop();
    }

    public void Paint()
    {
        pencil = true;
        eraser = false;
        brush = false;
        paintBrush = false;
        flameBrush = false;
        //paintRoller = false;
        particle.Stop();
        sprayparticle.Stop();
        paintroller.Stop();
        pigtail.Stop();
    }

    public void Brush()
    {
        brush = true;
        pencil = false;
        eraser = false;
        paintBrush = false;
        flameBrush = false;
        //paintRoller = false;
        particle.Stop();
        sprayparticle.Stop();
        paintroller.Stop();
        pigtail.Stop();
    }

    public void PaintBrush()
    {
        paintBrush = true;
        brush = false;
        pencil = false;
        eraser = false;
        flameBrush = false;
        //paintRoller = false;
        particle.Stop();
        sprayparticle.Stop();
        paintroller.Stop();
        pigtail.Stop();
    }

    public void ParticleActivate()
    {
        particle.Play();
        paintBrush = false;
        brush = false;
        pencil = false;
        eraser = false;
        flameBrush = false;
        //paintRoller = false;
        sprayparticle.Stop();
        paintroller.Stop();
        pigtail.Stop();
    }

    public void SprayActivate()
    {
        sprayparticle.Play();
        paintBrush = false;
        brush = false;
        pencil = false;
        eraser = false;
        flameBrush = false;
        //paintRoller = false;
        particle.Stop();
        paintroller.Stop();
        pigtail.Stop();
    }

    public void PaintRollerActivate()
    {
        //paintRoller = true;
        paintroller.Play();
        paintBrush = false;
        brush = false;
        pencil = false;
        eraser = false;
        flameBrush = false;
        particle.Stop();
        sprayparticle.Stop();
        pigtail.Stop();
    }

    public void flameBrushEffect()
    {
        flameBrush = true;
        brush = false;
        pencil = false;
        eraser = false;
        paintBrush = false;
        //paintRoller = false;
        particle.Stop();
        sprayparticle.Stop();
        paintroller.Stop();
        pigtail.Stop();
    }

    public void pigTailActivate()
    {
        pigtail.Play();
        flameBrush = false;
        brush = false;
        pencil = false;
        eraser = false;
        paintBrush = false;
        //paintRoller = false;
        particle.Stop();
        sprayparticle.Stop();
        paintroller.Stop();
    }

    public void CloseApp()
    {
        Application.Quit();
    }
}
