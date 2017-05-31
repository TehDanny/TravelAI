using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    class NeuralLayer : INeuralLayer
    {
        List<INeuron> m_neurons;

        public NeuralLayer()
        {
            m_neurons = new List<INeuron>();
        }
        INeuron IList<INeuron>.this[int index]
        {
            get { return m_neurons[index]; }
            set { m_neurons[index] = value; }
        }

        int ICollection<INeuron>.Count
        {
            get { return m_neurons.Count; }
        }

        bool ICollection<INeuron>.IsReadOnly
        {
            get { return false; }
        }

        void ICollection<INeuron>.Add(INeuron item)
        {
            m_neurons.Add(item);
        }

        void INeuralLayer.ApplyLearning(INeuralNet net)
        {
            foreach (INeuron n in m_neurons)
                n.ApplyLearning(this);
        }

        void ICollection<INeuron>.Clear()
        {
            m_neurons.Clear();
        }

        bool ICollection<INeuron>.Contains(INeuron item)
        {
            return m_neurons.Contains(item);
        }

        void ICollection<INeuron>.CopyTo(INeuron[] array, int arrayIndex)
        {
            m_neurons.CopyTo(array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_neurons.GetEnumerator();
        }

        IEnumerator<INeuron> IEnumerable<INeuron>.GetEnumerator()
        {
            return m_neurons.GetEnumerator();

        }

        int IList<INeuron>.IndexOf(INeuron item)
        {
            return m_neurons.IndexOf(item);
        }

        void IList<INeuron>.Insert(int index, INeuron item)
        {
            m_neurons.Insert(index, item);
        }

        void INeuralLayer.Pulse(INeuralNet net)
        {
            foreach (INeuron n in m_neurons)
                n.Pulse(this);
        }

        bool ICollection<INeuron>.Remove(INeuron item)
        {
            return m_neurons.Remove(item);
        }

        void IList<INeuron>.RemoveAt(int index)
        {
            m_neurons.RemoveAt(index);
        }
    }
}
