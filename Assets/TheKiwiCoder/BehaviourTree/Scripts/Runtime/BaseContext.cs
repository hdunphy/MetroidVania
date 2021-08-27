using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder
{
    public class BaseContext
    {
        public GameObject gameObject;
        public Rigidbody2D rigidbody2D;
        public BoxCollider2D boxCollider2D;

        public static BaseContext CreateFromGameObject(GameObject gameObject)
        {
            BaseContext context = new BaseContext();
            context.gameObject = gameObject;
            context.rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            context.boxCollider2D = gameObject.GetComponent<BoxCollider2D>();

            return context;
        }
    }
}
