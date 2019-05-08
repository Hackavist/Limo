
namespace Limo.Models
{
    public class Request : BaseModel
    {
        public int UserId { get; set; }
        public int? DriverId { get; set; }
        public int CarId { get; set; }
        public double Price { get; set; }
    }
}
