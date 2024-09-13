using System;

public class Node
{
    public int Value;
    public Node Next;
    public Node Prev;

    public Node(int value)
    {
        Value = value;
        Next = null;
        Prev = null;
    }
}

public class DoublyLinkedList
{
    public Node Head;
    public Node Tail;
    public Node Middle; // Para el problema 3
    private int count;  // Contador de elementos en la lista

    public DoublyLinkedList()
    {
        Head = null;
        Tail = null;
        Middle = null; // Para el problema 3
        count = 0;
    }

    // Método para insertar en orden ascendente (problema 3)
    public void InsertInOrder(int value)
    {
        Node newNode = new Node(value);

        // Caso: La lista está vacía
        if (Head == null)
        {
            Head = newNode;
            Tail = newNode;
            Middle = newNode;
            count++;
            return;
        }

        // Insertar al inicio si el valor es menor que el Head
        if (value <= Head.Value)
        {
            newNode.Next = Head;
            Head.Prev = newNode;
            Head = newNode;
            count++;
            UpdateMiddleAfterInsert();
            return;
        }

        // Insertar al final si el valor es mayor que el Tail
        if (value >= Tail.Value)
        {
            newNode.Prev = Tail;
            Tail.Next = newNode;
            Tail = newNode;
            count++;
            UpdateMiddleAfterInsert();
            return;
        }

        // Insertar en el medio si el valor está en algún lugar entre Head y Tail
        Node current = Head;
        while (current != null && current.Value < value)
        {
            current = current.Next;
        }

        // Inserción del nuevo nodo antes de "current"
        newNode.Prev = current.Prev;
        newNode.Next = current;
        current.Prev.Next = newNode;
        current.Prev = newNode;

        count++;
        UpdateMiddleAfterInsert();
    }

    // Método para actualizar el puntero "Middle" después de una inserción (problema 3)
    private void UpdateMiddleAfterInsert()
    {
        if (count == 1)
        {
            Middle = Head;
        }
        else if (count % 2 != 0)
        {
            Middle = Middle.Next; // Si la lista tiene un número impar de elementos
        }
    }

    // Método para obtener el elemento central (problema 3)
    public int GetMiddle()
    {
        if (count == 0)
        {
            throw new InvalidOperationException("La lista está vacía.");
        }
        return Middle.Value;
    }

    // Método para invertir la lista sin crear una nueva lista (problema 2)
    public void Invert()
    {
        if (Head == null)
        {
            throw new ArgumentException("La lista no puede ser null.");
        }

        if (IsEmpty())
        {
            return;
        }

        Node current = Head;
        Node temp = null;

        while (current != null)
        {
            temp = current.Next;
            current.Next = current.Prev;
            current.Prev = temp;
            current = temp;
        }

        // Intercambiar Head y Tail
        temp = Head;
        Head = Tail;
        Tail = temp;
    }

    // Método para fusionar dos listas en orden (problema 1)
    public static void MergeSorted(DoublyLinkedList listA, DoublyLinkedList listB, SortDirection direction)
    {
        if (listA == null || listB == null)
        {
            throw new ArgumentException("ListA or ListB cannot be null.");
        }

        if (listA.IsEmpty() && listB.IsEmpty())
        {
            return;
        }

        DoublyLinkedList result = new DoublyLinkedList();
        Node currentA = listA.Head;
        Node currentB = listB.Head;

        if (direction == SortDirection.Ascending)
        {
            while (currentA != null && currentB != null)
            {
                if (currentA.Value <= currentB.Value)
                {
                    result.AddLast(currentA.Value);
                    currentA = currentA.Next;
                }
                else
                {
                    result.AddLast(currentB.Value);
                    currentB = currentB.Next;
                }
            }
        }
        else if (direction == SortDirection.Descending)
        {
            currentA = listA.Tail;
            currentB = listB.Tail;

            while (currentA != null && currentB != null)
            {
                if (currentA.Value >= currentB.Value)
                {
                    result.AddLast(currentA.Value);
                    currentA = currentA.Prev;
                }
                else
                {
                    result.AddLast(currentB.Value);
                    currentB = currentB.Prev;
                }
            }
        }

        while (currentA != null)
        {
            if (direction == SortDirection.Ascending)
            {
                result.AddLast(currentA.Value);
                currentA = currentA.Next;
            }
            else
            {
                result.AddLast(currentA.Value);
                currentA = currentA.Prev;
            }
        }

        while (currentB != null)
        {
            if (direction == SortDirection.Ascending)
            {
                result.AddLast(currentB.Value);
                currentB = currentB.Next;
            }
            else
            {
                result.AddLast(currentB.Value);
                currentB = currentB.Prev;
            }
        }

        listA.Head = result.Head;
        listA.Tail = result.Tail;
    }

    // Método para agregar nodos al final de la lista (problema 1)
    public void AddLast(int value)
    {
        Node newNode = new Node(value);
        if (Head == null)
        {
            Head = newNode;
            Tail = newNode;
        }
        else
        {
            Tail.Next = newNode;
            newNode.Prev = Tail;
            Tail = newNode;
        }
    }

    // Método para verificar si la lista está vacía
    public bool IsEmpty()
    {
        return Head == null;
    }

    // Método para imprimir la lista
    public void PrintList()
    {
        Node current = Head;
        while (current != null)
        {
            Console.Write(current.Value + " ");
            current = current.Next;
        }
        Console.WriteLine();
    }
}

public enum SortDirection
{
    Ascending,
    Descending
}

class Program
{
    static void Main(string[] args)
    {
        // Problema 1: MergeSorted
        DoublyLinkedList listA = new DoublyLinkedList();
        listA.AddLast(1);
        listA.AddLast(3);
        listA.AddLast(5);

        DoublyLinkedList listB = new DoublyLinkedList();
        listB.AddLast(2);
        listB.AddLast(4);
        listB.AddLast(6);

        Console.WriteLine("Lista A original:");
        listA.PrintList();
        Console.WriteLine("Lista B original:");
        listB.PrintList();

        DoublyLinkedList.MergeSorted(listA, listB, SortDirection.Ascending);

        Console.WriteLine("Lista A fusionada en orden ascendente:");
        listA.PrintList();

        // Problema 2: Invertir lista
        DoublyLinkedList listC = new DoublyLinkedList();
        listC.AddLast(1);
        listC.AddLast(0);
        listC.AddLast(30);
        listC.AddLast(50);
        listC.AddLast(2);

        Console.WriteLine("Lista C original:");
        listC.PrintList();

        listC.Invert();

        Console.WriteLine("Lista C invertida:");
        listC.PrintList();

        // Problema 3: InsertInOrder y GetMiddle
        DoublyLinkedList listD = new DoublyLinkedList();
        listD.InsertInOrder(1);
        Console.WriteLine("Lista D después de insertar 1:");
        listD.PrintList();
        Console.WriteLine("Elemento central: " + listD.GetMiddle());

        listD.InsertInOrder(2);
        Console.WriteLine("Lista D después de insertar 2:");
        listD.PrintList();
        Console.WriteLine("Elemento central: " + listD.GetMiddle());

        listD.InsertInOrder(0);
        Console.WriteLine("Lista D después de insertar 0:");
        listD.PrintList();
        Console.WriteLine("Elemento central: " + listD.GetMiddle());

        listD.InsertInOrder(3);
        Console.WriteLine("Lista D después de insertar 3:");
        listD.PrintList();
        Console.WriteLine("Elemento central: " + listD.GetMiddle());

        listD.InsertInOrder(4);
        Console.WriteLine("Lista D después de insertar 4:");
        listD.PrintList();
        Console.WriteLine("Elemento central: " + listD.GetMiddle());
    }
}