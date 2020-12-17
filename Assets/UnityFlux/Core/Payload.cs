namespace UnityFlux.Core
{
    // ここはちょっとRedux風な構成
    // https://aloerina01.github.io/blog/2018-09-14-1#flux-standard-action
    public class Payload
    {
        public Payload(int actionId, object body, object meta = null)
        {
            ActionId = actionId;
            Body = body;
            Meta = meta;
        }

        public int ActionId { get; }
        public object Body { get; }
        public object Meta { get; }
    }
}