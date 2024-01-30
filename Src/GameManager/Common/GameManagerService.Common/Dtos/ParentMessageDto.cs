namespace GameManagerService.Common.Dtos {
    public class ParentMessageDto<T> {
        public T Message { get; set; } = default!;
        public Guid? UserId { get; set; }
    }
}
