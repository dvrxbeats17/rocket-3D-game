using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadDelay = 2f;
    [SerializeField] private ParticleSystem _succesEffect;
    [SerializeField] private ParticleSystem _loseEffect;
    private AudioSource _rocketAudioSource;
    [SerializeField] private AudioClip[] _clips;

    private bool _isTransitioning;

    private void Start()
    {
        _rocketAudioSource = GetComponent<AudioSource>();

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (_isTransitioning)
            return;
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartSuccesSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        _isTransitioning = true;
        _succesEffect.Play();
        _rocketAudioSource.PlayOneShot(_clips[1]);
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), levelLoadDelay);
    }
    private void StartSuccesSequence()
    {
        _isTransitioning = true;
        _loseEffect.Play();
        _rocketAudioSource.PlayOneShot(_clips[0]);
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(LoadNextLevel), levelLoadDelay);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void LoadNextLevel()
    {
        var nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
