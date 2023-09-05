namespace AccountApi.Interfaces {
    public interface IBaseConsumer {
        string QueueName { get; }
        Task Start();
        Task Stop();
    }
}
