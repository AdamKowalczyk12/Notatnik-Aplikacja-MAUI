namespace Notes.Models;

public class Note
{
    public string Text { get; set; }
    public DateTime CreatingDate { get; set; }

    public DateTime? ArchiveTime { get; set; }

    public Guid Id { get; set; }

    public Note()
    {

    }


    
}