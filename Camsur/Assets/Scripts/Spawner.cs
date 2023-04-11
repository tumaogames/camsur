using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _time;
    [SerializeField] private float _yClamp;
    [SerializeField] private float _xClamp;
    public GameObject ChatManager;

    private float _elapsedTime;
    private void Awake()
    {
    }
    private void Start()
    {
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _time && !Bird.dead)
        {
            SpawnObject();

            _elapsedTime = 0f;
        }
    }

    private void SpawnObject()
    {
        float offsetY = UnityEngine.Random.Range(-_yClamp, _yClamp);

        Vector2 pos = new Vector2(_xClamp, this.transform.position.y + offsetY);

        Instantiate(_prefab, pos, Quaternion.identity, this.transform);
    }
}
