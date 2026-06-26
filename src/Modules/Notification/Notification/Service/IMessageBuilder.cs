using FirebaseAdmin.Messaging;

namespace Notification.Service;

public interface IMessageBuilder<out T> where T : class
{
    IMessageBuilder<T> WithAndroid();
    IMessageBuilder<T> WithApns();
    IMessageBuilder<T> WithNotification(string title, string body);
    IMessageBuilder<T> WithData(Dictionary<string, string>?data);
    T Build();
}

public class FireBaseMessagBuilder: IMessageBuilder<Message>
{
    private readonly Message _message =new Message();

    public FireBaseMessagBuilder(string topic)
    {
        _message.Topic = topic;
    }
    public IMessageBuilder<Message> WithAndroid()
    {
        _message.Android = new()
        {
            Priority = Priority.High
        };
        return this;
    }

    public IMessageBuilder<Message> WithApns()
    {
        _message.Apns = new()
        {
            Headers = new Dictionary<string, string>() { { "apns-priority", "10" } },
            Aps = new Aps()
            {
                Sound = "default",
                ContentAvailable = true,
                MutableContent = true
            }
        };
        return this;
    }

    public IMessageBuilder<Message> WithNotification(string title, string body)
    {
        _message.Notification = new FirebaseAdmin.Messaging.Notification()
        {
            Title = title,
            Body = body,
        };
        return this;
    }

    public IMessageBuilder<Message> WithData(Dictionary<string, string>? data)
    {
        _message.Data = data;
        return this;
    }

    public Message Build()
    {
        return _message;
    }
}


public class FireBaseMulticastBuilder:IMessageBuilder<MulticastMessage>
{
    private readonly MulticastMessage _message = new MulticastMessage();

    public FireBaseMulticastBuilder(IReadOnlyList<string> tokens)
    {
        _message.Tokens = tokens;
    }

    public IMessageBuilder<MulticastMessage> WithAndroid()
    {
        _message.Android = new()
        {
            Priority = Priority.High
        };
        return this;
    }

    public IMessageBuilder<MulticastMessage> WithApns()
    {
        _message.Apns = new()
        {
            Headers = new Dictionary<string, string>() { { "apns-priority", "10" } },
            Aps = new Aps()
            {
                Sound = "default",
                ContentAvailable = true,
                MutableContent = true
            }
        };
        return this;
    }

    public IMessageBuilder<MulticastMessage> WithNotification(string title, string body)
    {
        _message.Notification = new FirebaseAdmin.Messaging.Notification()
        {
            Title = title,
            Body = body,
        };
        return this;
    }

    public IMessageBuilder<MulticastMessage> WithData(Dictionary<string, string>? data)
    {
        _message.Data = data;
        return this;
    }

    //public IMessageBuilder<MulticastMessage> WithTokens(IReadOnlyList<string> tokens)
    //{
    //    _message.Tokens = tokens;
    //    return this;
    //}

    public MulticastMessage Build()
    {
        return _message;
    }
}