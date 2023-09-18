using UnityEngine;

public class InstantiateAndForgetIt : MonoBehaviour
{
    [SerializeField]
    private bool followPlayer = false;
    private Transform player;
    private PlayerController controller;
    GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<PlayerController>();
        player = controller.transform;

        go = new GameObject();
        Destroy(go, 2.1f);

        transform.parent = go.transform;

        go.transform.position = player.position;
        
        if (!controller.isFacingRight)
        {
            go.transform.Rotate(0, 180f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!followPlayer)
            return;

        go.transform.position = player.position;
    }
}
