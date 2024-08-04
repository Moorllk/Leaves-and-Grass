using System.Collections;
using UnityEngine;

public class FadeObjectOnTrigger : MonoBehaviour
{
    #region Fields
    public GameObject _targetObject; // Объект, который будем делать невидимым
    private Renderer _targetRenderer;
    private Material _targetMaterial;
    private float originalAlphaCutoff = 0.5f;
    private float transitionAlphaCutoff = 0.8f;
    private float transparentAlphaCutoff = 1.1f;
    public float fadeDuration = 1.0f; // Время, за которое объект станет невидимым
    #endregion

    #region Unity Methods
    void Start()
    {
        if (_targetObject != null)
        {
            _targetRenderer = _targetObject.GetComponent<Renderer>();

            if (_targetRenderer != null)
            {
                _targetMaterial = _targetRenderer.material;
                if (!_targetMaterial.HasProperty("_AlphaCutoff"))
                {
                    Debug.LogError("Материал не содержит свойства _AlphaCutoff! | FadeObjectOnTrigger");
                }
            }
            else
            {
                Debug.LogError("_targetObject не содержит компонент для рендера! | FadeObjectOnTrigger");
            }
        }
        else
        {
            Debug.LogError("_targetObject не может найти нужный объект! | FadeObjectOnTrigger");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (_targetMaterial != null && other.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(FadeOut());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_targetMaterial != null && other.gameObject.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(FadeIn());
        }
    }
    #endregion

    #region My Methods
    IEnumerator FadeOut()
    {
        _targetMaterial.SetFloat("_AlphaCutoff", transitionAlphaCutoff);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlphaCutoff = Mathf.Lerp(transitionAlphaCutoff, transparentAlphaCutoff, elapsedTime / fadeDuration);
            _targetMaterial.SetFloat("_AlphaCutoff", newAlphaCutoff);
            yield return null;
        }

        _targetMaterial.SetFloat("_AlphaCutoff", transparentAlphaCutoff);
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        float currentAlphaCutoff = _targetMaterial.GetFloat("_AlphaCutoff");

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlphaCutoff = Mathf.Lerp(currentAlphaCutoff, transitionAlphaCutoff, elapsedTime / fadeDuration);
            _targetMaterial.SetFloat("_AlphaCutoff", newAlphaCutoff);
            yield return null;
        }

        _targetMaterial.SetFloat("_AlphaCutoff", originalAlphaCutoff);
    }
    #endregion
}