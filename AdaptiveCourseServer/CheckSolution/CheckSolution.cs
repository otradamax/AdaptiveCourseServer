using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace AdaptiveCourseServer.CheckSolution
{
    public static class CheckSolution
    {
        private static int[,] _X = new int[,]
        {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 1 },
            { 0, 0, 1, 0 },
            { 0, 0, 1, 1 },
            { 0, 1, 0, 0 },
            { 0, 1, 0, 1 },
            { 0, 1, 1, 0 },
            { 0, 1, 1, 1 },
            { 1, 0, 0, 0 },
            { 1, 0, 0, 1 },
            { 1, 0, 1, 0 },
            { 1, 0, 1, 1 },
            { 1, 1, 0, 0 },
            { 1, 1, 0, 1 },
            { 1, 1, 1, 0 },
            { 1, 1, 1, 1 }
        };
        private static int[] _Y = new int[] 
        { 
            0, 0, 0, 0, 
            0, 1, 1, 1, 
            0, 1, 1, 1, 
            0, 1, 1, 1
        };

        private static Dictionary<string, int?> ElementsOutputInitialization(Dictionary<string, List<string>> OrientedGraph, int testNumber)
        {
            Dictionary<string, int?> ElementsOutput = new Dictionary<string, int?>();
            Regex regex = new Regex(@"^X(\w+)");
            foreach (KeyValuePair<string, List<string>> nodesPair in OrientedGraph)
            {
                if (!regex.IsMatch(nodesPair.Key))
                {
                    ElementsOutput.Add(nodesPair.Key, null);
                }
                else 
                {
                    int serialNumber;
                    int.TryParse(nodesPair.Key.Substring("X".Length), out serialNumber);
                    ElementsOutput.Add(nodesPair.Key, _X[testNumber, serialNumber]);
                }
            }
            return ElementsOutput;
        }

        private static int MakeOperation(List<int> numbers, string operation)
        {
            int result = numbers[0];
            if (Regex.IsMatch(operation, @"AND(\d+)"))
            {
                for(int i = 1; i < numbers.Count; i++)
                {
                    result &= numbers[i];
                }
            }
            else if(Regex.IsMatch(operation, @"OR(\d+)"))
            {
                for (int i = 1; i < numbers.Count; i++)
                {
                    result |= numbers[i];
                }
            }
            return result;
        }

        public static bool Solution(Dictionary<string, List<string>> OrientedGraph)
        {
            List<int> resultY = new List<int>();
            for (int i = 0; i < _Y.Length; i++)
            {
                bool isCompleted = false;
                int repeatNum = 0;
                Dictionary<string, int?> ElementsOutput = ElementsOutputInitialization(OrientedGraph, i);
                do
                {
                    bool isOutputChangeForLoop = false;
                    isCompleted = true;
                    foreach (KeyValuePair<string, List<string>> nodesPair in OrientedGraph)
                    {
                        if (nodesPair.Value.Count > 0 && !nodesPair.Value.Any(x => ElementsOutput[x] == null))
                        {
                            List<int> inputs = new List<int>();
                            foreach (string input in nodesPair.Value)
                            {
                                inputs.Add(ElementsOutput[input].Value);
                            }
                            int result = MakeOperation(inputs, nodesPair.Key);
                            ElementsOutput[nodesPair.Key] = result;
                            isOutputChangeForLoop = true;
                        }
                        else if (nodesPair.Value.Count > 0)
                        {
                            isCompleted = false;
                        }
                    }
                    if (repeatNum > 0)
                    {
                        repeatNum--;
                    }
                    if (!isOutputChangeForLoop)
                    {
                        string output = ElementsOutput.Last(t => t.Value == null && t.Key != "Y").Key;
                        ElementsOutput[output] = 0;
                        repeatNum = 3;
                    }
                }
                while (!isCompleted || repeatNum > 0);
                if (_Y[i] != ElementsOutput["Y"])
                {
                    return false;
                }
            }
            return true;
        }


    }
}
