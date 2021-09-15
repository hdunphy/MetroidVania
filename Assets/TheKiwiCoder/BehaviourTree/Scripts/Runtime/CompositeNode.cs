using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder {
    public abstract class CompositeNode : Node {
        [HideInInspector] public List<Node> children = new List<Node>();

        public override Node Clone() {
            CompositeNode node = Instantiate(this);
            node.children = children.ConvertAll(c => c.Clone());
            return node;
        }

        public override void Bind(EnemyContext _context, Blackboard _blackboard)
        {
            base.Bind(_context, _blackboard);
            children.ForEach(n => n.Bind(_context, _blackboard));
        }
    }
}