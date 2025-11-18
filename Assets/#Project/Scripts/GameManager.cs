using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private UIManager uI;
    private CameraFollow cam;
    GameObject background;
    MovingPlatefromBehavior movingPlatforms;
    private RaccoonController player;
    private GameObject prizes;
    ChestNutsManager chesnut;
    public void Initialize(CameraFollow cam, RaccoonController player, GameObject prizes, ChestNutsManager chesnut, UIManager uI, GameObject background, MovingPlatefromBehavior movingPlatforms)
    {
        this.uI = uI;   
        this.cam = cam;   
        this.player = player;   
        this.prizes = prizes;   
        this.chesnut = chesnut;   
        this.background = background;   
        this.movingPlatforms = movingPlatforms;   
    }
    void Update()
    {
        cam.Process();
        player.Process();
        movingPlatforms.Process();

        // for (int i = 0; i < movingPlatforms.Length; i++)
        // {
        //     movingPlatforms[i].Process();
        // }

        for (int i = 0; i < background.transform.childCount; i++)
        {
            background.transform.GetChild(i).gameObject.TryGetComponent<ParallaxEffect>(out ParallaxEffect pe);
            pe.Process();
        }
    }
    
    public void IncreaseCounter()
    {

        uI.IncreaseCounter();
    }
    public void Finish()
    {
        SceneManager.LoadScene("TheEnd");
    }
}
