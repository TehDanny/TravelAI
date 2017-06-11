using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    class Neuron : INeuron
    {
        NeuralFactor m_bias;
        double m_biasWeight;
        double m_error;
        double m_lastError;
        Dictionary<INeuronSignal, NeuralFactor> m_input;
        double m_output;
        public Neuron(double bias)
        {
            m_bias = new NeuralFactor(bias);
            m_error = 0;
            m_input = new Dictionary<INeuronSignal, NeuralFactor>();
        }
        public Neuron()
        {
            m_error = 0;
            m_input = new Dictionary<INeuronSignal, NeuralFactor>();

        }
        public NeuralFactor Bias
        {
            get { return m_bias; }
            set { m_bias = value; }
        }

        public double BiasWeight
        {
            get { return m_biasWeight; }
            set { m_biasWeight = value; }
        }

        public double Error
        {
            get { return m_error; }
            set
            {
                m_lastError = m_error;
                m_error = value;
            }
        }
        public double DeltaError
        {
            get { return m_lastError - m_error; }
        }

        public Dictionary<INeuronSignal, NeuralFactor> Input
        {
            get
            {
                return m_input;
            }
        }

        public double Output
        {
            get { return m_output; }
            set { m_output = value; }
        }

        public void ApplyLearning(INeuralLayer layer)
        {
            foreach (KeyValuePair<INeuronSignal, NeuralFactor> m in m_input)
                m.Value.ApplyDelta();

            m_bias.ApplyDelta();
        }

        public void Pulse(INeuralLayer layer)
        {
            lock (this)
            {
                m_output = 0;

                foreach (KeyValuePair<INeuronSignal, NeuralFactor> item in m_input)
                    m_output += item.Key.Output * item.Value.Weight;

                m_output += m_bias.Weight * BiasWeight;

                m_output = Sigmoid(m_output);
                //m_output = Logistic(m_output);
            }
        }
        private static double Sigmoid(double value)
        {
            double sigmoid = 1 / (1 + Math.Exp(-value));
            return sigmoid;
        }
        double Logistic(double x)
        {
            return 1 / (1 + Math.Pow(Math.E, -x));
        }


    }
}
