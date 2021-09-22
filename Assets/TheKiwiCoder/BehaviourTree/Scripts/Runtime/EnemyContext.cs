using UnityEngine;

namespace TheKiwiCoder
{
    public class EnemyContext
    {
        public GameObject gameObject;
        public EnemyController enemyController;
        public IEntityMovement entityMovement;

        public static EnemyContext CreateFromGameObject(GameObject gameObject)
        {
            EnemyContext context = new EnemyContext
            {
                gameObject = gameObject,
                enemyController = gameObject.GetComponent<EnemyController>(),
                entityMovement = gameObject.GetComponent<IEntityMovement>()
            };

            return context;
        }
    }
}
