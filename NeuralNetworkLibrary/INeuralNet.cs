using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkLibrary
{
    public interface INeuralNet
    {
        INeuralLayer HiddenLayer { get; }
        INeuralLayer OutputLayer { get; }
        INeuralLayer PerceptionLayer { get; }

        void ApplyLearning();
        void Pulse();
    }
}
