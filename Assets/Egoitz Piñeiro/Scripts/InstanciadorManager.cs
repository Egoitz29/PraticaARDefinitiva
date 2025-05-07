using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using System.Collections.Generic;

public class InstanciadorManager : MonoBehaviour
{
    public ARRaycastManager raycastManager;   // Para detectar clics o toques sobre planos
    public ARPlaneManager planeManager;
    public List<GameObject> prefabsAInstanciar;  // Lista de prefabs
    public TMP_Dropdown selectorPrefab;          // El dropdown

    private List<GameObject> instancias = new List<GameObject>();
    public float alturaSobrePlano = 0.05f;

    private void Update()
    {
        // --- CLIC CON RATÓN (para PC) ---
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 posicionPantalla = Input.mousePosition;
            IntentarInstanciar(posicionPantalla);
        }

        // --- TOQUE EN MÓVIL ---
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);

            if (toque.phase == TouchPhase.Began)
            {
                Vector2 posicionPantalla = toque.position;
                IntentarInstanciar(posicionPantalla);
            }
        }
    }

    private void IntentarInstanciar(Vector2 posicionPantalla)
    {
        List<ARRaycastHit> resultados = new List<ARRaycastHit>();
        if (raycastManager.Raycast(posicionPantalla, resultados, TrackableType.PlaneWithinPolygon))
        {
            Pose poseImpacto = resultados[0].pose;

            InstanciarEnPosicion(poseImpacto.position);
        }
    }

    private void InstanciarEnPosicion(Vector3 posicion)
    {
        int indice = selectorPrefab.value;

        if (indice < 0 || indice >= prefabsAInstanciar.Count)
        {
            Debug.LogWarning("Índice de prefab no válido.");
            return;
        }

        GameObject prefabSeleccionado = prefabsAInstanciar[indice];

        posicion.y += alturaSobrePlano;

        GameObject nuevaInstancia = Instantiate(prefabSeleccionado, posicion, Quaternion.identity);
        instancias.Add(nuevaInstancia);

        Debug.Log("Instanciado prefab: " + prefabSeleccionado.name);
    }

    public void EliminarObjetos()
    {
        foreach (GameObject obj in instancias)
        {
            Destroy(obj);
        }
        instancias.Clear();
    }
}
