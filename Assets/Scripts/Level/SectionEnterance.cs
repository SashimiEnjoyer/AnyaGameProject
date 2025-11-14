using UnityEngine;

public class SectionEnterance : MonoBehaviour
{
    private LevelSectionManager thisSection;
    [SerializeField] private Transform startingPoint;
    [SerializeField] private int nextSectionIndex;

    void Awake()
    {
        thisSection = GetComponentInParent<LevelSectionManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {

            TransitionScreen.instance.StartingFullTransition(1f, () =>
            {
                thisSection.SetNextSection(nextSectionIndex);

                if (startingPoint != null)
                    collision.transform.position = startingPoint.position;
                
                thisSection.CleanupSection();
                TransitionScreen.instance.StartingTransition(TransitionPosition.FromBlack, 1f, null);
            });
        }
    }
}
