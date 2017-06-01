using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    public class NeuralNet : INeuralNet
    {
        INeuralLayer m_hiddenLayer;
        INeuralLayer m_inputLayer;
        INeuralLayer m_outputLayer;
        double m_learningRate;
        public NeuralNet()
        {
            m_learningRate = 0.9;
        }
        public INeuralLayer HiddenLayer
        {
            get { return m_hiddenLayer; }
        }

        public INeuralLayer OutputLayer
        {
            get { return m_outputLayer; }
        }

        public INeuralLayer PerceptionLayer
        {
            get { return m_inputLayer; }
        }

        public void ApplyLearning()
        {
            lock (this)
            {
                m_hiddenLayer.ApplyLearning(this);
                m_outputLayer.ApplyLearning(this);
            }
        }

        public void Pulse()
        {
            lock (this)
            {
                m_hiddenLayer.Pulse(this);
                m_outputLayer.Pulse(this);
            }
        }
        public void Initialize(int randomSeed, int inputNeuronCount, int hiddenNeuronCount, int outputNeuronCount)
        {
            int i, j, k, layerCount;
            Random rand;
            INeuralLayer layer;

            // initializations
            rand = new Random(randomSeed);
            m_inputLayer = new NeuralLayer();
            m_outputLayer = new NeuralLayer();
            m_hiddenLayer = new NeuralLayer();

            for (i = 0; i < inputNeuronCount; i++)
                m_inputLayer.Add(new Neuron(0));

            for (i = 0; i < outputNeuronCount; i++)
                m_outputLayer.Add(new Neuron(rand.NextDouble()));

            for (i = 0; i < hiddenNeuronCount; i++)
                m_hiddenLayer.Add(new Neuron(rand.NextDouble()));

            // wire-up input layer to hidden layer
            for (i = 0; i < m_hiddenLayer.Count; i++)
                for (j = 0; j < m_inputLayer.Count; j++)
                    m_hiddenLayer[i].Input.Add(m_inputLayer[j],
                         new NeuralFactor(rand.NextDouble()));

            // wire-up output layer to hidden layer
            for (i = 0; i < m_outputLayer.Count; i++)
                for (j = 0; j < m_hiddenLayer.Count; j++)
                    m_outputLayer[i].Input.Add(m_hiddenLayer[j],
                         new NeuralFactor(rand.NextDouble()));
        }
        private void BackPropogation(double[] desiredResults)
        {
            int i, j;
            double temp, error;

            INeuron outputNode, inputNode, hiddenNode, node, node2;

            // Calcualte output error values
            for (i = 0; i < m_outputLayer.Count; i++)
            {
                temp = m_outputLayer[i].Output;
                m_outputLayer[i].Error = (desiredResults[i] - temp) * temp * (1.0F - temp);
            }

            // calculate hidden layer error values
            for (i = 0; i < m_hiddenLayer.Count; i++)
            {
                node = m_hiddenLayer[i];

                error = 0;

                for (j = 0; j < m_outputLayer.Count; j++)
                {
                    outputNode = m_outputLayer[j];
                    error += outputNode.Error * outputNode.Input[node].Weight * node.Output * (1.0 - node.Output);
                }

                node.Error = error;
                
            }

            // adjust output layer weight change
            for (i = 0; i < m_hiddenLayer.Count; i++)
            {
                node = m_hiddenLayer[i];

                for (j = 0; j < m_outputLayer.Count; j++)
                {
                    outputNode = m_outputLayer[j];
                    outputNode.Input[node].Weight += m_learningRate * m_outputLayer[j].Error * node.Output;
                    outputNode.Bias.Delta += m_learningRate * m_outputLayer[j].Error * outputNode.Bias.Weight;
                }
            }

            // adjust hidden layer weight change
            for (i = 0; i < m_inputLayer.Count; i++)
            {
                inputNode = m_inputLayer[i];

                for (j = 0; j < m_hiddenLayer.Count; j++)
                {
                    hiddenNode = m_hiddenLayer[j];
                    hiddenNode.Input[inputNode].Weight += m_learningRate * hiddenNode.Error * inputNode.Output;
                    hiddenNode.Bias.Delta += m_learningRate * hiddenNode.Error * inputNode.Bias.Weight;
                }
            }
        }
        public void Train(double[] input, double[] desiredResult)
        {
            int i;

            if (input.Length != m_inputLayer.Count)
                throw new ArgumentException(string.Format("Expecting {0} inputs for this net", m_inputLayer.Count));

            // initialize data
            for (i = 0; i < m_inputLayer.Count; i++)
            {
                Neuron n = m_inputLayer[i] as Neuron;

                if (null != n) // maybe make interface get;set;
                    n.Output = input[i];
            }

            Pulse();
            BackPropogation(desiredResult);
        }
        public void Train(double[][] inputs, double[][] expected)
        {
            for (int i = 0; i < inputs.Length; i++)
                Train(inputs[i], expected[i]);
        }
    }
}
