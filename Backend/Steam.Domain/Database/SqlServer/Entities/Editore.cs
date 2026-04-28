using System;
using System.Collections.Generic;

namespace Steam.Domain.Database.SqlServer.Entities;

public partial class Editore
{
    public int EditorId { get; set; }

    public string Name { get; set; } = null!;
}
