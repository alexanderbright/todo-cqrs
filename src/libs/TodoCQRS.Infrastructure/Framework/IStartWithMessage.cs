namespace TodoCQRS.Infrastructure.Framework
{
  public interface IStartWithMessage<in T> where T : Message
  {
    void Handle(T message);
  }
}