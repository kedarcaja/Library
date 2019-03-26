public interface IOpenable
{

    void Open();
    void Close();
    bool Opened { get; }
	event OpenHandler OnOpen,OnClose;
}
public delegate void OpenHandler();