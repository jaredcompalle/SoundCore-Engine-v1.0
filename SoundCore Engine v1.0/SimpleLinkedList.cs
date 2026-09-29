// SoundCore Engine v2.0 - TecNM Campus Monclova
// Integrantes: Rosembert Jared Ortiz Reyes - I25050406
// Fecha: 28/09/2026 | Versión: 1.0

using System.Collections;

namespace SoundCore.EstructurasPropias
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T>? Siguiente { get; set; }

        public Node(T valor)
        {
            Value = valor;
            Siguiente = null;
        }
    }

    public class SimpleLinkedList<T> : IEnumerable<T>
    {
        public Node<T>? Cabeza { get; private set; }
        public int Conteo { get; private set; }

        public bool EstaVacia => Cabeza == null;

        public void AddToFinal(T valor)
        {
            var nuevoNodo = new Node<T>(valor);
            if (EstaVacia)
            {
                Cabeza = nuevoNodo;
            }
            else
            {
                var actual = Cabeza!;
                while (actual.Siguiente != null)
                    actual = actual.Siguiente;
                actual.Siguiente = nuevoNodo;
            }
            Conteo++;
        }

        public void PlayNext(T valor)
        {
            var nuevoNodo = new Node<T>(valor);
            if (EstaVacia)
            {
                Cabeza = nuevoNodo;
            }
            else
            {
                nuevoNodo.Siguiente = Cabeza!.Siguiente;
                Cabeza.Siguiente = nuevoNodo;
            }
            Conteo++;
        }

        public T AdvanceTrack()
        {
            if (EstaVacia)
                throw new InvalidOperationException("La cola de reproducción está vacía.");

            T valor = Cabeza!.Value;
            Cabeza = Cabeza.Siguiente;
            Conteo--;
            return valor;
        }

        public void Invest()
        {
            Node<T>? previo = null;
            Node<T>? actual = Cabeza;
            Node<T>? siguiente = null;

            while (actual != null)
            {
                siguiente = actual.Siguiente; 
                actual.Siguiente = previo;   
                previo = actual;            
                actual = siguiente;          
            }

            Cabeza = previo;
        }

        public void InsertSort(T valor, Comparison<T> comparador)
        {
            var nuevo = new Node<T>(valor);

            if (EstaVacia || comparador(valor, Cabeza!.Value) < 0)
            {
                nuevo.Siguiente = Cabeza;
                Cabeza = nuevo;
                Conteo++;
                return;
            }

            var actual = Cabeza;
            while (actual.Siguiente != null && comparador(valor, actual.Siguiente.Value) >= 0)
                actual = actual.Siguiente;

            nuevo.Siguiente = actual.Siguiente;
            actual.Siguiente = nuevo;
            Conteo++;
        }

        public void DebugDuplicates(Func<T, T, bool> sonIguales)
        {
            var actual = Cabeza;

            while (actual != null)
            {
                var corredor = actual;
                while (corredor.Siguiente != null)
                {
                    if (sonIguales(actual.Value, corredor.Siguiente.Value))
                    {
                        corredor.Siguiente = corredor.Siguiente.Siguiente;
                        Conteo--;
                    }
                    else
                    {
                        corredor = corredor.Siguiente;
                    }
                }
                actual = actual.Siguiente;
            }
        }

        public void Sort(Comparison<T> comparador)
        {
            Node<T>? ordenada = null;
            var actual = Cabeza;

            while (actual != null)
            {
                var siguiente = actual.Siguiente;

                if (ordenada == null || comparador(actual.Value, ordenada.Value) < 0)
                {
                    actual.Siguiente = ordenada;
                    ordenada = actual;
                }
                else
                {
                    var p = ordenada;
                    while (p.Siguiente != null && comparador(actual.Value, p.Siguiente.Value) >= 0)
                        p = p.Siguiente;
                    actual.Siguiente = p.Siguiente;
                    p.Siguiente = actual;
                }
                actual = siguiente;
            }

            Cabeza = ordenada;
        }

        public void Clean()
        {
            Cabeza = null;
            Conteo = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var actual = Cabeza;
            while (actual != null)
            {
                yield return actual.Value;
                actual = actual.Siguiente;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
