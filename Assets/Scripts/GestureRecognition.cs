//made by Laura Unverzagt
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

enum Shape {
    Circle,
    Wave,
    Triangle,
    Empty
}

public class GestureRecognition : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject lightning;

    [SerializeField] private GameObject handLeft;
    [SerializeField] private GameObject handRight;

    private List<Vector3> posLeft = new List<Vector3>();
    private List<Vector3> posRight = new List<Vector3>();

    private Shape shape;
    private float handDistanceThreshold = 0.2f;
    private bool symbolActive = false;
    private bool isInvoking = false;
    private bool gameStart = false;

    private void Start()
    {
        StartMenu.Instance.BeginGame.AddListener(SetUp);
        StartMenu.Instance.EndGame.AddListener(ShutDown);
    }

    private void SetUp()
    {
        handLeft = GameObject.FindGameObjectWithTag("HandLeft");
        handRight = GameObject.FindGameObjectWithTag("HandRight");
        fire = handRight.transform.GetChild(3).gameObject;
        water = handRight.transform.GetChild(3).gameObject;
        lightning = handRight.transform.GetChild(3).gameObject;
        gameStart = true;
    }
    private void ShutDown()
    {
        handLeft = null;
        handRight = null;
        fire = null;
        water = null;
        lightning = null;
        gameStart = false;
        CancelInvoke();
    }

    private void Update()
    {
        if (!gameStart) return;
        CheckHandDistance();
        if (symbolActive && !isInvoking) { 
            InvokeRepeating("PrintPositions", 0.2f, 0.07f);
            isInvoking = true;
            symbolActive = false;
            Debug.Log("I am starting my test");
        }
    }

    private void IsInvokingTrue()
    {
        isInvoking = false;
    }

    private void PrintPositions()
    {
        posLeft.Add(handLeft.transform.position);
        posRight.Add(handRight.transform.position);
        if (posLeft.Count >= 32 || posRight.Count >= 32) {
            symbolActive = false;
            CancelInvoke(methodName: "PrintPositions");
            Invoke("IsInvokingTrue", 3);
            CheckShapes();
            posLeft = new List<Vector3>();
            posRight = new List<Vector3>();
        }
    }

    private void CheckHandDistance()
    {
        float dist = Vector3.Distance(handLeft.transform.position, handRight.transform.position);
        if (dist <= handDistanceThreshold) symbolActive = true;
    }
    private void CheckShapes()
    {
        Debug.Log("I am checking shapes");
        bool rightMoved = false;
        bool leftMoved = false;
        for (int i = 0; i < posLeft.Count-2; i++)
        {
            if (Vector3.Distance(posLeft[i], posLeft[i + 1]) >= handDistanceThreshold) leftMoved = true;
            if (Vector3.Distance(posRight[i], posRight[i + 1]) >= handDistanceThreshold) rightMoved = true;
        }
        if (rightMoved && leftMoved)
        {
            if (IsTriangleShape()) shape = Shape.Triangle;
        }
        else if (IsCircleShape()) shape = Shape.Circle;
        else if (IsWaveShape()) shape = Shape.Wave;
        else {
            shape = Shape.Empty;
            Debug.Log("WTF "); 
        }
        CastSpell();
    }
    private bool IsCircleShape()
    {
        Vector3 startPos = posRight[0];
        Vector3 vec = new Vector3(0,0,0);
        float dst = 0;
        foreach(Vector3 pos in posRight){
            if (Vector3.Distance(startPos, pos) >= dst){
                vec = pos;
                dst = Vector3.Distance(startPos, pos);
            }
        }
        //Debug.Log(startPos + " " + vec);
        Vector3 midPos = Vector3.Lerp(startPos, vec, 0.5f);
        Debug.DrawRay(startPos, vec, Color.red, 1.0f);

        dst = Vector3.Distance(midPos, posRight[4]); // use any point to get reference distance value
        foreach (Vector3 pos in posRight)
        {
            float curr = Vector3.Distance(midPos, pos);
            //if the circle is too small
            if (curr <= 0.4) return false;
            //if the distance to the middle point is too different
            if (curr <= dst - 0.5 || curr >= dst + 0.5) return false;
        }
        Debug.Log("It's a circle");
        return true;
        /*
         * CIRCLE
            Take first point
            connect to all points
            take longest distance points, half it to get the middle point of the circle
            (repeat the procedure with another point for higher accuracy?)
            check if distance from the middle point is the same for all (-> RADIUS with offset)
         */
    }
    private bool IsWaveShape()
    {
        Vector3 start = posRight[0];
        bool above = false;
        bool below = false;
        //check if points are significantly above and below the line
        foreach (Vector3 pos in posRight)
        {
            if (pos.y > start.y + 0.3) above = true;
            if (pos.y < start.y - 0.3) below = true;
        }
        if (above && below)
        {
            Debug.Log("It's a Wave");
            return true;
        }
        else return false;
        /*
         * WAVE
            take first and (last?) point 
            see if they overlap any path between points
         */
    }
    private bool IsTriangleShape()
    {
        for (int i = 0; i < posLeft.Count - 2; i++)
        {
            if (posLeft[i].y-posLeft[i + 1].y <= -0.5)
            {
                return false;
            }
            if (posRight[i].y - posRight[i + 1].y <= -0.5)
            {
                return false;
            }
        }
        Debug.Log("It's a Triangle");
        return true;
        /*
         * TRIANGLE
            check if both hands are moving
            if they do -> check if y value (value downward) changes
         */
    }
    //permanently running, minumum sizes for shapes 
    //mit beiden hands gleichzeitig casten possible

    private void CastSpell()
    {
        //TODO: activate proper Shader

        Debug.Log("Aiming...");
        switch (shape)
        {
            case Shape.Circle:
                fire.SetActive(true);
                break;
            case Shape.Wave:
                water.SetActive(true);
                break;
            case Shape.Triangle:
                lightning.SetActive(true);
                break;
            case Shape.Empty:
                break;
        }
        InvokeRepeating("IncreaseShader", 0.2f, 0.07f);
        Invoke("FinishSpell", 2);
    }
    private void IncreaseShader()
    {
        //TODO: increase the shader size
    }
    private void FinishSpell()
    {
        Debug.Log("Pew!");
        CancelInvoke(methodName: "IncreaseShader");
        Vector3 direction = handRight.transform.position - handLeft.transform.position;

        GameObject p = Instantiate(projectilePrefab, handRight.transform.position, Quaternion.identity);
        Rigidbody rb = p.GetComponent<Rigidbody>();
        rb.velocity += direction.normalized * Time.deltaTime * 300;

        shape = Shape.Empty;
        //TODO: reset Shader Size
        //TODO: stop particle properly
    }
}
