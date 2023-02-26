using System.Collections;

namespace Algorithms
{
    public class LinkedList<T> : IEnumerable<T>
    {
        DoubleNode<T> head; // головной/первый элемент
        DoubleNode<T> tail; // последний/хвостовой элемент
        int count;  // количество элементов в списке

        // добавление элемента
        public void Add(T data)
        {
            DoubleNode<T> node = new DoubleNode<T>(data);

            if (head == null)
                head = node;
            else
            {
                tail.Next = node;
                node.Previous = tail;
            }
            tail = node;
            count++;
        }

        public void AddNodeAfter(T searchData, T data)
        {
            DoubleNode<T> current = head;
            DoubleNode<T> node = new DoubleNode<T>(searchData);
            DoubleNode<T> newNode = new DoubleNode<T>(data);
            while (current != null)
            {
                if (current.Data.Equals(node.Data))
                    if (current.Next != null)
                    {
                        newNode.Previous = current;
                        newNode.Next = current.Next;
                        current.Next = newNode;
                        current.Next.Previous = newNode;
                        count++;
                        break;
                    }
                    else if (current.Next == null)
                    {
                        newNode.Previous = current;
                        current.Next = newNode;
                        tail = newNode;
                        count++;
                        break;
                    }
                current = current.Next;
            }
        }

        public void RemoveNodeByIndex(int index) //удаление ноды по порядковому номеру
        {
            DoubleNode<T> current = head;
            if (index > count)
                return;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            if (current != null)
            {
                // если узел не последний
                if (current.Next != null)
                {
                    current.Next.Previous = current.Previous;
                }
                else
                {
                    // если последний, переустанавливаем tail
                    tail = current.Previous;
                }

                // если узел не первый
                if (current.Previous != null)
                {
                    current.Previous.Next = current.Next;
                }
                else
                {
                    // если первый, переустанавливаем head
                    head = current.Next;
                }
                count--;
            }
        }

        public void AddFirst(T data)
        {
            DoubleNode<T> node = new DoubleNode<T>(data);
            DoubleNode<T> temp = head;
            node.Next = temp;
            head = node;
            if (count == 0)
                tail = head;
            else
                temp.Previous = node;
            count++;
        }
        // удаление
        public bool Remove(T data)
        {
            DoubleNode<T> current = head;

            // поиск удаляемого узла
            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    break;
                }
                current = current.Next;
            }
            if (current != null)
            {
                // если узел не последний
                if (current.Next != null)
                {
                    current.Next.Previous = current.Previous;
                }
                else
                {
                    // если последний, переустанавливаем tail
                    tail = current.Previous;
                }

                // если узел не первый
                if (current.Previous != null)
                {
                    current.Previous.Next = current.Next;
                }
                else
                {
                    // если первый, переустанавливаем head
                    head = current.Next;
                }
                count--;
                return true;
            }
            return false;
        }

        public int Count { get { return count; } }
        public bool IsEmpty { get { return count == 0; } }

        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public DoubleNode<T> FindNode(int searchValue)
        {
            DoubleNode<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(searchValue))
                    return current;
                current = current.Next;
            }
            return null;
        }

        public bool Contains(T data)
        {
            DoubleNode<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                    return true;
                current = current.Next;
            }
            return false;
        }

        public void Reverse()
        {

            if (head == null) return;
            DoubleNode<T> prev = null, current = head, next = null;

            while (current.Next != null)
            {
                next = current.Next;
                current.Next = prev;
                prev = current;
                current = next;
            }

            current.Next = prev;
            head = current;
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            DoubleNode<T> current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        public IEnumerable<T> FrontEnumerator()
        {
            DoubleNode<T> current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        public IEnumerable<T> BackEnumerator()
        {
            DoubleNode<T> current = tail;
            while (current != null)
            {
                yield return current.Data;
                current = current.Previous;
            }
        }
    }
}

