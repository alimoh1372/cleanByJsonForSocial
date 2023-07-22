using Common;

namespace Infrastructure.DateTimeService;

public class MachineDateTime:IDateTime 
{
    public DateTime Now =>DateTime.Now;
    public int CurrentYear=> DateTime.Now.Year;
}