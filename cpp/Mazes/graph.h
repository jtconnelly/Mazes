namespace Boots
{
    template <typename T>
    class iGraph
    {
        public:
            virtual ~iGraph() = default;
            virtual void addEdge(T to, T from) = 0;
            virtual void removeEdge(T to, T from) = 0;
            virtual bool hasEdge(T to, T from) const = 0;
    };
}