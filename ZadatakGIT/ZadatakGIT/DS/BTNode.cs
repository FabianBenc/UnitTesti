﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ZadatakGIT
{
   public class BinaryTreeNode<TNode>: IComparable<TNode>
        where TNode: IComparable<TNode>
    {
        public BinaryTreeNode(TNode value)
        {
            Value = value;
        }
        public BinaryTreeNode<TNode> Left { get; set; }
        public BinaryTreeNode<TNode> Right { get; set; }
        public TNode Value { get; private set; }

        public int CompareTo(TNode other)
        {
            return Value.CompareTo(other);
        }
    }
}
