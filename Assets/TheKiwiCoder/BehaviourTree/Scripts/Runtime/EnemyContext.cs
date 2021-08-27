using UnityEngine;

namespace TheKiwiCoder
{
    public class EnemyContext
    {
        public GameObject gameObject;
        public EnemyController enemyController;
        public EntityMovement entityMovement;

        public static EnemyContext CreateFromGameObject(GameObject gameObject)
        {
            EnemyContext context = new EnemyContext
            {
                gameObject = gameObject,
                enemyController = gameObject.GetComponent<EnemyController>(),
                entityMovement = gameObject.GetComponent<EntityMovement>()
            };

            return context;
        }
    }
}
