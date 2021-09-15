using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder {
    public abstract class DecoratorNode : Node {
        [HideInInspector] public Node child;

        public override Node Clone() {
            DecoratorNode node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }

        public override void Bind(EnemyContext _context, Blackboard _blackboard)
        {
            base.Bind(_context, _blackboard);
            child.Bind(_context, _blackboard);
        }
    }
}
