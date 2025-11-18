using UnityEngine;
using UnityEngine.InputSystem;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private GameManager gm;
    [SerializeField] private UIManager uI;
    [SerializeField] private CameraFollow cam;
    [SerializeField] private Grid platforms;
    [SerializeField] private GameObject background;
    [SerializeField] private MovingPlatefromBehavior movingPlatforms;
    [SerializeField] private Transform[] movingPlatformsPos;
    [SerializeField] private float movingPlatformSpeed = 2f;
    [SerializeField] private GameObject prizes;
    [SerializeField] private ChestNutsManager chestnut;
    [SerializeField] private RaccoonController player;
    [SerializeField] private InputActionAsset actions;
    [SerializeField] private float playerSpeed = 3f;
    [SerializeField] private float jumpForce = 5f;
    void Start()
    {
        InstantiateObjects();
        InitializeObjects();
    }

    private void InstantiateObjects()
    {
        gm = Instantiate(gm);
        uI = Instantiate(uI);
        cam = Instantiate(cam);
        background = Instantiate(background);
        platforms = Instantiate(platforms);
        // movingPlatforms = Instantiate(movingPlatforms);
        // for (int i = 0; i < movingPlatforms.Length; i++)
        // {
        //     movingPlatforms[i] = Instantiate(movingPlatforms[i]);
        // }
        // movingPlatforms = Instantiate(movingPlatforms);
        prizes = Instantiate(prizes);
        player = Instantiate(player);
        
    }
    private void InitializeObjects()
    {
        gm.Initialize(cam, player, prizes, chestnut, uI, background,  movingPlatforms);
        uI.Initialize(gm);
        cam.Initialize(gm, player.transform);

        for (int i = 0; i < background.transform.childCount; i++)
        {
            background.transform.GetChild(i).gameObject.TryGetComponent<ParallaxEffect>(out ParallaxEffect pe);
            pe.Initialize(gm, cam);
        }
        
        player.Initialize(gm, actions, playerSpeed, jumpForce);
        player.gameObject.SetActive(true);

        movingPlatforms.Initialize(gm, movingPlatformsPos, movingPlatformSpeed);

        // for (int i = 0; i < movingPlatforms.Length; i++)
        // {
        //     movingPlatforms[i].Initialize(gm, cameraSpeed, goRight);
        // }
        
    }
        

}
