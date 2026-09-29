// SoundCore Engine v2.0 - TecNM Campus Monclova
// Integrantes: [Nombre completo] - [No. de control]
// Fecha: 28/09/2026 | Versión: 2.0

using System.Collections;

namespace SoundCore.EstructurasPropias
{
    public class Nodo<T>
    {
        public T Valor { get; set; }
        public Nodo<T>? Siguiente { get; set; }

        public Nodo(T valor)
        {
            Valor = valor;
            Siguiente = null;
        }
    }

    public class ListaSimpleEnlazada<T> : IEnumerable<T>
    {
        public Nodo<T>? Cabeza { get; private set; }
        public int Conteo { get; private set; }

        public bool EstaVacia => Cabeza == null;

        // 1. Inserción al final: O(n)
        public void AgregarAlFinal(T valor)
        {
            var nuevoNodo = new Nodo<T>(valor);
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

        // 2. Up Next: inserción inmediata tras la cabeza: O(1)
        public void ReproducirSiguiente(T valor)
        {
            var nuevoNodo = new Nodo<T>(valor);
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

        // 3. Desencolar pista actual (eliminar cabeza): O(1)
        public T AvanzarPista()
        {
            if (EstaVacia)
                throw new InvalidOperationException("La cola de reproducción está vacía.");

            T valor = Cabeza!.Valor;
            Cabeza = Cabeza.Siguiente;
            Conteo--;
            return valor;
        }

        // 4. Inversión in-place: O(n) tiempo, O(1) memoria auxiliar (solo se redirigen enlaces)
        public void Invertir()
        {
            Nodo<T>? previo = null;
            Nodo<T>? actual = Cabeza;
            Nodo<T>? siguiente = null;

            while (actual != null)
            {
                siguiente = actual.Siguiente; // Guardar puntero al resto de la lista
                actual.Siguiente = previo;    // Invertir la referencia
                previo = actual;              // Desplazar previo
                actual = siguiente;           // Desplazar actual
            }

            Cabeza = previo;
        }

        // 5. Inserción ordenada por criterio (ej. BPM): O(n)
        public void InsertarOrdenado(T valor, Comparison<T> comparador)
        {
            var nuevo = new Nodo<T>(valor);

            if (EstaVacia || comparador(valor, Cabeza!.Valor) < 0)
            {
                nuevo.Siguiente = Cabeza;
                Cabeza = nuevo;
                Conteo++;
                return;
            }

            var actual = Cabeza;
            while (actual.Siguiente != null && comparador(valor, actual.Siguiente.Valor) >= 0)
                actual = actual.Siguiente;

            nuevo.Siguiente = actual.Siguiente;
            actual.Siguiente = nuevo;
            Conteo++;
        }

        // 6. Depurar duplicados sin estructuras externas: O(n^2) tiempo, O(1) espacio
        public void DepurarDuplicados(Func<T, T, bool> sonIguales)
        {
            var actual = Cabeza;

            while (actual != null)
            {
                var corredor = actual;
                while (corredor.Siguiente != null)
                {
                    if (sonIguales(actual.Valor, corredor.Siguiente.Valor))
                    {
                        corredor.Siguiente = corredor.Siguiente.Siguiente; // desconectar duplicado
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

        // Extra: ordenamiento por inserción in-place (estable) relinkeando nodos: O(n^2), O(1) espacio
        public void Ordenar(Comparison<T> comparador)
        {
            Nodo<T>? ordenada = null;
            var actual = Cabeza;

            while (actual != null)
            {
                var siguiente = actual.Siguiente;

                if (ordenada == null || comparador(actual.Valor, ordenada.Valor) < 0)
                {
                    actual.Siguiente = ordenada;
                    ordenada = actual;
                }
                else
                {
                    var p = ordenada;
                    while (p.Siguiente != null && comparador(actual.Valor, p.Siguiente.Valor) >= 0)
                        p = p.Siguiente;
                    actual.Siguiente = p.Siguiente;
                    p.Siguiente = actual;
                }
                actual = siguiente;
            }

            Cabeza = ordenada;
        }

        public void Limpiar()
        {
            Cabeza = null;
            Conteo = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var actual = Cabeza;
            while (actual != null)
            {
                yield return actual.Valor;
                actual = actual.Siguiente;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
