namespace People.Domain.DTOs;

public class ReadUserDto
{
    public Guid id { get; set; }
    public string userName { get; set; }
    public string normalizedUserName { get; set; }
}
