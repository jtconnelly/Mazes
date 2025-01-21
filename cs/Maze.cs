
using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace Mazes
{
    /***********************************************************
     * A Coordinate Struct for representing 2D Coordinates
     * @param x value representing an x coordinate
     * @param y value representing an y coordinate
     * This Coordinate struct will be used in new graph implementations and for the A* algorithm implementations
     * @see CoordinateGraph
     * @see WeightedCoordinateGraph
     ***********************************************************/
    public struct Coordinate2D<T> where T: INumber<T>
    {
        public T x { get; set; }
        public T y { get; set; }

        public bool Equals(Coordinate2D<T> other)
        {
            return this.x == other.x && this.y == other.y;
        }

        public static bool operator ==(Coordinate2D<T> person1, Coordinate2D<T> person2)
        {
            return person1.Equals(person2);
        }
        public static bool operator !=(Coordinate2D<T> person1, Coordinate2D<T> person2)
        {
            return !person1.Equals(person2);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            if (obj is Coordinate2D<T> coordObj)
                return Equals(coordObj);
            return false;
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode();
        }
    }

    /***********************************************************
     * a  graph implementation based on the Graph Class using the coordinate struct.
     * @see Graph
     * @see Coordinate2D
     * Since this implementation is based on the base Graph class, we can use this with any of the iterations of Graph.
     ***********************************************************/   
    public class CoordinateGraph<T> : Graph.Graph<Coordinate2D<T>> where T : INumber<T>
    {

    }

    /***********************************************************
     * a  graph implementation based on the WeightedGraph Class using the coordinate struct.
     * @see WeightedGraph
     * @see Coordinate2D
     * Since this implementation is based on the base WeightedGraph class, we can use this with any of the iterations of WeightedGraph.
     ***********************************************************/
    public class WeightedCoordinateGraph<T> : Graph.WeightedGraph<Coordinate2D<T>> where T : INumber<T>
    {

    }

    /***********************************************************
     * a  graph implementation based on the NonNegativeWeightedGraph Class using the coordinate struct.
     * @see NonNegativeWeightedGraph
     * @see Coordinate2D
     * Since this implementation is based on the base NonNegativeWeightedGraph class, we can use this with any of the iterations of NonNegativeWeightedGraph.
     ***********************************************************/
    public class NonNegativeWeightedCoordinateGraph<T> : Graph.NonNegativeWeightedGraph<Coordinate2D<T>> where T : INumber<T>
    {

    }
}
