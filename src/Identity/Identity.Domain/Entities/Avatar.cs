using TovarischAndruha.Summary.Identity.Domain.Common;

namespace TovarischAndruha.Summary.Identity.Domain.Entities;

public class Avatar : BaseEntity {
  public byte[] Photo { get; set; } = null!;
  public string Extension { get; set; } = null!;
  public DateTime UploadTime { get; set; }
}