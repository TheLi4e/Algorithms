using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public class RedBlackTree<T> where T : IComparable<T>
    {
        public int Count { get; private set; }
        public Node<T> RootNode;

        public RedBlackTree()
        {
            Count = 0;
            RootNode = null;
        }

        public Node<T> FindFloor(T targetValue)
        {
            if (RootNode == null || targetValue.CompareTo(FindMind().Value) < 0)
            {
                return null;
            }
            return FindFloor(targetValue, RootNode, RootNode);
        }

        public Node<T> FindCeiling(T targetValue)
        {
            if (RootNode == null || targetValue.CompareTo(FindMax().Value) > 0)
            {
                return null;
            }
            return FindCeiling(targetValue, RootNode, RootNode);
        }

        private Node<T> FindFloor(T targetValue, Node<T> currentNode, Node<T> bestNode)
        {
            if (currentNode == null)
            {
                return bestNode;
            }
            if (targetValue.CompareTo(currentNode.Value) == 0)
            {
                return currentNode;
            }
            if (targetValue.CompareTo(currentNode.Value) > 0)
            {
                return FindFloor(targetValue, currentNode.RightChild, currentNode);
            }
            else
            {
                return FindFloor(targetValue, currentNode.LeftChild, bestNode);
            }
        }
        private Node<T> FindCeiling(T targetValue, Node<T> currentNode, Node<T> bestNode)
        {
            if (currentNode == null)
            {
                return bestNode;
            }
            if (targetValue.CompareTo(currentNode.Value) == 0)
            {
                return currentNode;
            }
            else if (targetValue.CompareTo(currentNode.Value) < 0)
            {
                return FindCeiling(targetValue, currentNode.LeftChild, currentNode);
            }
            else
            {
                return FindCeiling(targetValue, currentNode.RightChild, bestNode);
            }
        }
        public Node<T> FindMind()
        {
            if (RootNode == null)
            {
                return null;
            }
            Node<T> currentNode = RootNode;
            while (currentNode.LeftChild != null)
            {
                currentNode = currentNode.LeftChild;
            }
            return currentNode;
        }

        public Node<T> FindMax()
        {
            if (RootNode == null)
            {
                return null;
            }
            Node<T> currentNode = RootNode;
            while (currentNode.RightChild != null)
            {
                currentNode = currentNode.RightChild;
            }
            return currentNode;
        }

        private Node<T> FindParent(Node<T> targetNode)
        {
            Node<T> currentNode = RootNode;
            Node<T> previousNode = RootNode;
            while (currentNode != null)
            {
                if (currentNode.Equals(targetNode))
                {
                    return previousNode;
                }
                if (targetNode.IsLessThan(currentNode))
                {
                    currentNode = currentNode.LeftChild;
                }
                else
                {
                    currentNode = currentNode.RightChild;
                }
            }
            return null;
        }

        public Node<T> Contains(T targetValue)
        {
            Node<T> currentNode = RootNode;

            while (currentNode != null)
            {
                if (currentNode.Value.Equals(targetValue))
                {
                    return currentNode;
                }
                else if (targetValue.CompareTo(currentNode.Value) < 0)
                {
                    currentNode = currentNode.LeftChild;
                }
                else
                {
                    currentNode = currentNode.RightChild;
                }
            }
            return null;
        }

        private bool IsNodeRed(Node<T> targetNode)
        {
            if (targetNode == null)
            {
                return false;
            }
            return targetNode.isRed;
        }

        private void FlipColor(Node<T> targetNode)
        {
            if (targetNode == null)
            {
                return;
            }
            targetNode.isRed = !targetNode.isRed;

            if (targetNode.RightChild != null)
            {
                targetNode.RightChild.isRed = !targetNode.RightChild.isRed;
            }

            if (targetNode.LeftChild != null)
            {
                targetNode.LeftChild.isRed = !targetNode.LeftChild.isRed;
            }

            if (targetNode == RootNode)
            {
                targetNode.isRed = false;
            }
        }

        public void Add(T targetValue)
        {
            RootNode = Add(RootNode, targetValue);
            Count++;
            RootNode.isRed = false;
        }

        private Node<T> Add(Node<T> currentNode, T value)
        {
            if (currentNode == null)
            {
                return new Node<T>(value);
            }

            if (currentNode.IsFourNode())
            {
                FlipColor(currentNode);
            }

            if (value.CompareTo(currentNode.Value) < 0)
            {
                currentNode.LeftChild = Add(currentNode.LeftChild, value);
            }
            else
            {
                currentNode.RightChild = Add(currentNode.RightChild, value);
            }

            if (currentNode.RightChild != null && currentNode.RightChild.isRed)
            {
                currentNode = LeftRotation(currentNode);
            }

            if (IsNodeRed(currentNode.LeftChild) && IsNodeRed(currentNode.LeftChild.LeftChild))
            {
                currentNode = RightRotation(currentNode);
            }
            return currentNode;
        }

        public bool Remove(T targetValue)
        {
            Node<T> targetNode = Contains(targetValue);
            if (targetNode == null)
            {
                return false;
            }
            RootNode = Remove(RootNode, targetNode);
            Count--;
            return true;
        }

        private Node<T> Remove(Node<T> currentNode, Node<T> targetNode)
        {
            if (currentNode == null)
            {
                return null;
            }
            if (targetNode.IsLessThan(currentNode))
            {
                if (!IsNodeRed(currentNode.LeftChild) && !IsNodeRed(currentNode.LeftChild.LeftChild))
                {
                    currentNode = MoveRedLeft(currentNode);
                }
                currentNode.LeftChild = Remove(currentNode.LeftChild, targetNode);
            }
            else
            {
                if (IsNodeRed(currentNode.LeftChild))
                {
                    currentNode = RightRotation(currentNode);
                }
                if (currentNode.Equals(targetNode) && currentNode.IsLeafNode())
                {
                    return null;
                }
                if (currentNode.RightChild != null)
                {
                    if (!IsNodeRed(currentNode.RightChild) && !IsNodeRed(currentNode.RightChild.LeftChild))
                    {
                        currentNode = MoveRedRight(currentNode);
                    }
                    if (currentNode.Value.Equals(targetNode.Value))
                    {
                        Node<T> min = currentNode.FindMin();
                        currentNode.ReplaceValue(min);
                        currentNode.RightChild = Remove(currentNode.RightChild, min);
                    }
                    else
                    {
                        currentNode.RightChild = Remove(currentNode.RightChild, targetNode);
                    }
                }
            }
            currentNode = Fixup(currentNode);
            return currentNode;
        }

        public Node<T> Fixup(Node<T> currentNode)
        {
            if (IsNodeRed(currentNode.RightChild))
            {
                currentNode = LeftRotation(currentNode);
            }
            if (IsNodeRed(currentNode.LeftChild) && IsNodeRed(currentNode.LeftChild.LeftChild))
            {
                currentNode.LeftChild = RightRotation(currentNode.LeftChild);
            }
            if (currentNode.IsFourNode())
            {
                FlipColor(currentNode);
            }
            if (IsNodeRed(currentNode.LeftChild) && IsNodeRed(currentNode.LeftChild.RightChild))
            {
                Node<T> leftChild = currentNode.LeftChild;
                if (IsNodeRed(leftChild.RightChild))
                {
                    leftChild = LeftRotation(leftChild);
                }
                if (IsNodeRed(leftChild.LeftChild) && IsNodeRed(leftChild.LeftChild.LeftChild))
                {
                    leftChild.LeftChild = RightRotation(leftChild.LeftChild);
                }
                currentNode.LeftChild = leftChild;
            }
            return currentNode;
        }

        public Node<T> MoveRedLeft(Node<T> currentNode)
        {
            if (IsNodeRed(currentNode.LeftChild) || currentNode == null)
            {
                throw new Exception("MoveRedLeft called improperly");
            }

            FlipColor(currentNode);
            if (IsNodeRed(currentNode.RightChild) && IsNodeRed(currentNode.RightChild.LeftChild))
            {
                currentNode.RightChild = RightRotation(currentNode.RightChild);
                currentNode = LeftRotation(currentNode);
                FlipColor(currentNode);
                if (IsNodeRed(currentNode.RightChild.RightChild))
                {
                    currentNode.RightChild = LeftRotation(currentNode.RightChild);
                }
            }

            return currentNode;
        }

        public Node<T> MoveRedRight(Node<T> currentNode)
        {
            if (IsNodeRed(currentNode.RightChild) || currentNode == null)
            {
                throw new Exception("MoveRedRight called improperly");
            }

            FlipColor(currentNode);
            if (IsNodeRed(currentNode.LeftChild) && IsNodeRed(currentNode.LeftChild.LeftChild))
            {
                currentNode = RightRotation(currentNode);
                FlipColor(currentNode);
            }
            return currentNode;
        }

        private Node<T> RightRotation(Node<T> targetNode)
        {
            Node<T> tempHolder = targetNode.LeftChild;
            targetNode.LeftChild = tempHolder.RightChild;
            tempHolder.RightChild = targetNode;

            tempHolder.isRed = targetNode.isRed;
            targetNode.isRed = true;
            return tempHolder;
        }

        private Node<T> LeftRotation(Node<T> targetNode)
        {
            Node<T> tempHolder = targetNode.RightChild;
            targetNode.RightChild = tempHolder.LeftChild;
            tempHolder.LeftChild = targetNode;

            tempHolder.isRed = targetNode.isRed;
            targetNode.isRed = true;
            return tempHolder;
        }
    }
}