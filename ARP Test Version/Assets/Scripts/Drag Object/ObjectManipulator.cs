using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 
 * Canal: Unity Adventure
 * Video referencia: https://youtu.be/T-nFqKnhFOA?list=PL9km1tR7SfmTUO2uPW9w7ATiX2-wtnQV0
 */
public class ObjectManipulator : MonoBehaviour
{
    public GameObject aRObject;
    [SerializeField] private Camera aRCamera;
    private bool isARObjectSelected;
    private string aRTag = "ARObject";
    // initialTouchPos: Guarda la posicion del touch dado en la pantalla.
    private Vector2 initialTouchPos;


    [SerializeField] private float speedMovement = 4.0f;
    [SerializeField] private float speedRotation = 5.0f;
    [SerializeField] private float scaleFactor = 0.1f;
    private float screenFactor = 0.0001f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Se verifica primero que se haya tocado la pantalla.
        if (Input.touchCount > 0)
        {
            Touch touchOne = Input.GetTouch(0);
            // Se construye la logica para mover el objeto con solo un dedo.
            if (Input.touchCount == 1)
            {
                if (touchOne.phase == TouchPhase.Began)
                {
                    // Se inicializa cuando inicia.
                    initialTouchPos = touchOne.position;
                    // Se verifica que el objeto se ha tocado sobre un objeto.
                    isARObjectSelected = CheckTouchOnARObject(initialTouchPos);
                    /* Ya a estas alturas se verifica que se haya seleccionado un objeto y se referencia o pasa a aRObject.*/
                }

                // Ahora se procede a mover el objeto.
                if (touchOne.phase == TouchPhase.Moved && isARObjectSelected)
                {
                    /* Para mover el objeto, se procede a tomar la diferencia entre el touch inicial y 
                     * la posicion del touch cuando hemos movido el dedo dobre la pantalla  */
                    // Esta diferencia de posiciones esta dada por la pantalla, por ende hay que reducir su magnitud
                    Vector2 diffPos = (touchOne.position - initialTouchPos) * screenFactor;
                    aRObject.transform.position = aRObject.transform.position + new Vector3(diffPos.x * speedMovement, diffPos.y * speedMovement, 0);
                    // El movimiento en la pantalla es en dos dimensiones. Si se quiere un movimiento en Z, es necesario otro input para este.
                    initialTouchPos = touchOne.position;
                }
            }
        }
        
    }

    private bool CheckTouchOnARObject(Vector2 touchPos)
    {
        // Ray => tambien conocido como RayCast
        Ray ray = aRCamera.ScreenPointToRay(touchPos);

        // Se verifica que el objeto haya impactado.
        if (Physics.Raycast(ray, out RaycastHit hitArObject))
        {
            if (hitArObject.collider.CompareTag(aRTag))
            {
                aRObject = hitArObject.transform.gameObject;
                return true;
            }
        }
        return false;
    }
}
