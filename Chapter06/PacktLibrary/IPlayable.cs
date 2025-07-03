namespace Packt.Shared;
public interface IPlayable
{
    void Play();
    void Pause();

    void Stop() // реализация интерфейса по умолчанию
    {
        Console.WriteLine("Default implementation of Stop.");
    }
}
