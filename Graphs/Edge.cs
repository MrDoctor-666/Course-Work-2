namespace Graphs
{
    public class Edge
    {
        //edge that is between vertex1 and vertex2
        public int vertex1 { get; }
        public int vertex2 { get; }
        public int weight { get; }
        public Edge(int v1, int v2, int weight)
        {
            //vertex1 is smaller then vertex2
            //for simplier use
            if (v1 < v2)
            {
                this.vertex1 = v1;
                this.vertex2 = v2;
            }
            else
            {
                this.vertex1 = v2;
                this.vertex2 = v1;
            }
            this.weight = weight;
        }
    }
}
